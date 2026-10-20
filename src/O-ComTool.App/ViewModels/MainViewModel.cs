using System.Collections.ObjectModel;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Threading.Channels;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using O_ComTool.Core.Config;
using O_ComTool.Core.Serial;
using O_ComTool.Core.Text;

namespace O_ComTool.App.ViewModels
{
    /**
     * @brief 主窗口状态。串口、编码和高亮都委托给 Core
     */
    public partial class MainViewModel : ObservableObject, IDisposable
    {
        private readonly ConfigStore _store;
        private readonly SerialSession _session = new();
        private readonly SynchronizationContext _ui;
        private readonly DispatcherTimer _repeatTimer;
        private readonly DispatcherTimer _replyTimer;
        private readonly DispatcherTimer _ledTimer;
        private readonly Channel<DisplayChunk> _receiveChannel;
        private Task? _receiveProcessor;
        private StreamWriter? _log;
        private int _sequence;
        private int _txBytes;
        private int _rxBytes;
        private int _txFrames;
        private int _rxFrames;
        private const int MaxReceiveLines = 50000;  // 最大保留行数

        public MainViewModel(ConfigStore store, AppSettings settings)
        {
            _store = store;
            _ui = SynchronizationContext.Current ?? new SynchronizationContext();
            Highlight = new HighlightEngine();
            _session.FrameReceived += (_, data) => _ui.Post(_ => OnFrame(data), null);
            _session.Error += (_, ex) => _ui.Post(_ => OnSerialError(ex), null);
            _repeatTimer = new DispatcherTimer();
            _repeatTimer.Tick += (_, _) => SendCore(GeneralSendData);
            _replyTimer = new DispatcherTimer();
            _replyTimer.Tick += (_, _) =>
            {
                _replyTimer.Stop();
                SendCore(GeneralSendData);
            };
            _ledTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(100) };
            _ledTimer.Tick += (_, _) =>
            {
                ReceiveActive = false;
                _ledTimer.Stop();
            };
            ApplySettings(settings, reloadHighlight: true);
            
            // 创建通道用于缓冲接收数据
            _receiveChannel = Channel.CreateUnbounded<DisplayChunk>(new UnboundedChannelOptions
            {
                SingleReader = true,
                SingleWriter = false
            });
            _receiveProcessor = ProcessReceiveChannel();
            
            store.WatchHighlight(() => _ui.Post(_ =>
            {
                ReloadHighlight();
                HighlightReloaded?.Invoke();
            }, null));
        }

        public HighlightEngine Highlight { get; }

        public ObservableCollection<string> Ports { get; } = new();

        public ObservableCollection<string> PacketLines { get; } = new();

        public ObservableCollection<QuickSendItemViewModel> QuickSends { get; } = new();

        public event Action<DisplayChunk, bool>? ChunkReady;

        public string? LogPath { get; private set; }

        public string LogFolder { get; set; } = string.Empty;

        public event Action? ClearReceiveRequested;

        public event Action? HighlightReloaded;

        /// <summary>
        /// /// 已处理的总字节数(用于统计)
        /// </summary>
        public long TotalProcessedBytes { get; private set; }

        [ObservableProperty] private string _portName = string.Empty;
        [ObservableProperty] private string _baudRate = "9600";
        [ObservableProperty] private int _dataBitIndex = 3;
        [ObservableProperty] private int _parityIndex;
        [ObservableProperty] private int _stopBitIndex;
        [ObservableProperty] private int _flowControlIndex;
        [ObservableProperty] private bool _receiveAscii = true;
        [ObservableProperty] private bool _sendAscii = true;
        [ObservableProperty] private bool _autoLine;
        [ObservableProperty] private bool _showTime;
        [ObservableProperty] private bool _autoReply;
        [ObservableProperty] private int _autoReplyDelay = 100;
        [ObservableProperty] private bool _autoCount;
        [ObservableProperty] private bool _appendNewLine;
        [ObservableProperty] private bool _repeatSend;
        [ObservableProperty] private int _repeatInterval = 1000;
        [ObservableProperty] private string _generalSendData = string.Empty;
        [ObservableProperty] private bool _loadFileEnable;
        [ObservableProperty] private string _loadFilePath = string.Empty;
        [ObservableProperty] private bool _minToTray;
        [ObservableProperty] private bool _closeToTray;
        [ObservableProperty] private bool _quitConfirm = true;
        [ObservableProperty] private bool _highlightEnable = true;
        [ObservableProperty] private bool _commentEnable;
        [ObservableProperty] private int _frameInterval = 20;
        [ObservableProperty] private bool _sendDisplayEnable;
        [ObservableProperty] private bool _timeNewLine;
        [ObservableProperty] private bool _sendToFileEnable = true;
        [ObservableProperty] private bool _sendToNewLineEnable;
        [ObservableProperty] private bool _displayPlan1 = true;
        [ObservableProperty] private string _note = string.Empty;
        [ObservableProperty] private string _lastCheckTime = "2000-01-01 00:00:00";
        [ObservableProperty] private string _skipVersion = string.Empty;
        [ObservableProperty] private string _receiveForeground1 = "#F8F8F2";
        [ObservableProperty] private string _receiveBackground1 = "#272822";
        [ObservableProperty] private string _sendForeground1 = "#F8F8F2";
        [ObservableProperty] private string _sendBackground1 = "#272822";
        [ObservableProperty] private string _receiveForeground2 = "#272822";
        [ObservableProperty] private string _receiveBackground2 = "#F8F8F2";
        [ObservableProperty] private string _sendForeground2 = "#272822";
        [ObservableProperty] private string _sendBackground2 = "#F8F8F2";
        [ObservableProperty] private double _receiveFontSize = 14;
        [ObservableProperty] private double _sendFontSize = 14;
        [ObservableProperty] private bool _isOpen;
        [ObservableProperty] private bool _canSend = true;
        [ObservableProperty] private bool _receiveActive;
        [ObservableProperty] private bool _countFrames = true;
        [ObservableProperty] private bool _autoSave;
        [ObservableProperty] private string _statusText = "就绪";
        [ObservableProperty] private bool _statusOk = true;
        [ObservableProperty] private string _txText = "TX: 0 frames";
        [ObservableProperty] private string _rxText = "RX: 0 frames";
        [ObservableProperty] private string _rateText = "0%";
        [ObservableProperty] private string _portStatus = "COMx: Closed";

        public string OpenButtonText => IsOpen ? "关闭串口" : "打开串口";

        public string ReceiveForeground => DisplayPlan1 ? ReceiveForeground1 : ReceiveForeground2;

        public string ReceiveBackground => DisplayPlan1 ? ReceiveBackground1 : ReceiveBackground2;

        public string SendForeground => DisplayPlan1 ? SendForeground1 : SendForeground2;

        public string SendBackground => DisplayPlan1 ? SendBackground1 : SendBackground2;

        public string HighlightPath => _store.HighlightPath;

        /**
         * @brief 用设置对象刷新界面状态
         * @param settings: 应用设置
         * @param reloadHighlight: 是否同时重载高亮文件
         */
        public void ApplySettings(AppSettings settings, bool reloadHighlight)
        {
            PortName = settings.PortName;
            BaudRate = settings.BaudRate;
            DataBitIndex = settings.DataBitIndex;
            ParityIndex = settings.ParityIndex;
            StopBitIndex = settings.StopBitIndex;
            FlowControlIndex = settings.FlowControlIndex;
            ReceiveAscii = settings.ReceiveAscii;
            SendAscii = settings.SendAscii;
            AutoLine = settings.AutoLine;
            ShowTime = settings.ShowTime;
            AutoReply = settings.AutoReply;
            AutoReplyDelay = settings.AutoReplyDelay;
            AutoCount = settings.AutoCount;
            AppendNewLine = settings.AppendNewLine;
            RepeatSend = settings.RepeatSend;
            RepeatInterval = settings.RepeatInterval;
            GeneralSendData = settings.GeneralSendData;
            LoadFileEnable = settings.LoadFileEnable;
            LoadFilePath = settings.LoadFilePath;
            LogFolder = settings.LogFolder;
            MinToTray = settings.MinToTray;
            CloseToTray = settings.CloseToTray;
            QuitConfirm = settings.QuitConfirm;
            HighlightEnable = settings.HighlightEnable;
            CommentEnable = settings.CommentEnable;
            FrameInterval = settings.FrameInterval;
            SendDisplayEnable = settings.SendDisplayEnable;
            TimeNewLine = settings.TimeNewLine;
            SendToFileEnable = settings.SendToFileEnable;
            SendToNewLineEnable = settings.SendToNewLineEnable;
            DisplayPlan1 = settings.DisplayPlan1;
            Note = settings.Note;
            LastCheckTime = settings.LastCheckTime;
            SkipVersion = settings.SkipVersion;
            ReceiveForeground1 = settings.ReceiveForeground1;
            ReceiveBackground1 = settings.ReceiveBackground1;
            SendForeground1 = settings.SendForeground1;
            SendBackground1 = settings.SendBackground1;
            ReceiveForeground2 = settings.ReceiveForeground2;
            ReceiveBackground2 = settings.ReceiveBackground2;
            SendForeground2 = settings.SendForeground2;
            SendBackground2 = settings.SendBackground2;
            ReceiveFontSize = settings.ReceiveFontSize;
            SendFontSize = settings.SendFontSize;
            QuickSends.Clear();
            foreach (QuickSendEntry entry in settings.QuickSends)
            {
                QuickSends.Add(new QuickSendItemViewModel { Name = entry.Name, Data = entry.Data });
            }

            Renumber();
            RefreshPorts();
            if (LoadFileEnable && File.Exists(LoadFilePath))
            {
                LoadPacketFile(LoadFilePath);
                SetStatus(true, "文件加载成功");
            }
            else if (LoadFileEnable)
            {
                SetStatus(false, "文件加载失败");
            }

            if (reloadHighlight)
            {
                ReloadHighlight();
            }

            NotifyTheme();
        }

        /**
         * @brief 把界面状态写回设置对象
         * @retval 应用设置
         */
        public AppSettings CaptureSettings()
        {
            var settings = new AppSettings
            {
                PortName = PortName,
                BaudRate = BaudRate,
                DataBitIndex = DataBitIndex,
                ParityIndex = ParityIndex,
                StopBitIndex = StopBitIndex,
                FlowControlIndex = FlowControlIndex,
                ReceiveAscii = ReceiveAscii,
                SendAscii = SendAscii,
                AutoLine = AutoLine,
                ShowTime = ShowTime,
                AutoReply = AutoReply,
                AutoReplyDelay = AutoReplyDelay,
                AutoCount = AutoCount,
                AppendNewLine = AppendNewLine,
                RepeatSend = RepeatSend,
                RepeatInterval = RepeatInterval,
                GeneralSendData = GeneralSendData,
                LoadFileEnable = LoadFileEnable,
                LoadFilePath = LoadFilePath,
                LogFolder = LogFolder,
                MinToTray = MinToTray,
                CloseToTray = CloseToTray,
                QuitConfirm = QuitConfirm,
                HighlightEnable = HighlightEnable,
                CommentEnable = CommentEnable,
                FrameInterval = FrameInterval,
                SendDisplayEnable = SendDisplayEnable,
                TimeNewLine = TimeNewLine,
                SendToFileEnable = SendToFileEnable,
                SendToNewLineEnable = SendToNewLineEnable,
                DisplayPlan1 = DisplayPlan1,
                Note = Note,
                LastCheckTime = LastCheckTime,
                SkipVersion = SkipVersion,
                ReceiveForeground1 = ReceiveForeground1,
                ReceiveBackground1 = ReceiveBackground1,
                SendForeground1 = SendForeground1,
                SendBackground1 = SendBackground1,
                ReceiveForeground2 = ReceiveForeground2,
                ReceiveBackground2 = ReceiveBackground2,
                SendForeground2 = SendForeground2,
                SendBackground2 = SendBackground2,
                ReceiveFontSize = ReceiveFontSize,
                SendFontSize = SendFontSize,
                QuickSends = QuickSends.Select(item => new QuickSendEntry { Name = item.Name, Data = item.Data }).ToList(),
            };
            settings.ClampQuickSends();
            return settings;
        }

        /**
         * @brief 保存 settings.json，并让高亮开关与 highlight.json 一致
         */
        public void Save()
        {
            HighlightFile highlight = _store.LoadHighlight();
            highlight.Enable = HighlightEnable;
            _store.SaveHighlight(highlight);
            _store.SaveSettings(CaptureSettings());
            ReloadHighlight();
            HighlightReloaded?.Invoke();
        }

        /**
         * @brief 刷新串口列表并尽量保留当前选择
         */
        public void RefreshPorts()
        {
            string current = PortName;
            Ports.Clear();
            foreach (string name in SerialSession.GetPortNames().OrderBy(item => item, StringComparer.OrdinalIgnoreCase))
            {
                Ports.Add(name);
            }

            if (!string.IsNullOrEmpty(current))
            {
                if (!Ports.Contains(current))
                {
                    Ports.Insert(0, current);
                }

                PortName = current;
            }
            else if (Ports.Count > 0)
            {
                PortName = Ports[0];
            }
        }

        /**
         * @brief 按行加载待发报文
         * @param path: 文本文件路径
         */
        public void LoadPacketFile(string path)
        {
            PacketLines.Clear();
            foreach (string line in File.ReadLines(path, Encoding.Default))
            {
                if (!string.IsNullOrEmpty(line))
                {
                    PacketLines.Add(line);
                }
            }

            LoadFilePath = path;
            LoadFileEnable = true;
        }

        /**
         * @brief 开始把接收内容追加到日志
         * @param path: 日志路径
         */
        public void BeginLog(string path)
        {
            EndLog();
            var stream = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.Read);
            _log = new StreamWriter(stream, new UTF8Encoding(encoderShouldEmitUTF8Identifier: true)) { AutoFlush = true };
            LogPath = path;
            AutoSave = true;
            SetStatus(true, path);
        }

        /**
         * @brief 把接收区里已经显示的内容写入当前日志
         * @param text: 接收区全文
         */
        public void WriteSnapshot(string text)
        {
            if (_log == null || string.IsNullOrEmpty(text))
            {
                return;
            }

            _log.Write(AnsiParser.Strip(text));
            if (!text.EndsWith('\n'))
            {
                _log.WriteLine();
            }
        }

        /**
         * @brief 停止写日志
         */
        public void EndLog()
        {
            _log?.Dispose();
            _log = null;
            AutoSave = false;
        }

        /**
         * @brief 导入旧版 ini 并立即保存为 JSON
         * @param path: ini 路径
         */
        public void ImportIni(string path)
        {
            AppSettings settings = CaptureSettings();
            HighlightFile highlight = _store.LoadHighlight();
            IniConfig.Import(path, settings, highlight);
            _store.SaveHighlight(highlight);
            ApplySettings(settings, reloadHighlight: true);
            Save();
            SetStatus(true, "导入配置文件成功");
        }

        /**
         * @brief 导出旧版 ini
         * @param path: ini 路径
         */
        public void ExportIni(string path)
        {
            IniConfig.Export(path, CaptureSettings(), _store.LoadHighlight());
            SetStatus(true, "配置文件导出成功");
        }

        /**
         * @brief 记录用户跳过的版本号
         * @param version: 版本号
         */
        public void SkipUpdate(string version)
        {
            SkipVersion = version;
            LastCheckTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Save();
        }

        /**
         * @brief 记录本次检查时间
         */
        public void MarkUpdateChecked()
        {
            LastCheckTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            _store.SaveSettings(CaptureSettings());
        }

        [RelayCommand]
        private void ToggleOpen()
        {
            if (IsOpen)
            {
                _session.Close();
                IsOpen = false;
                PortStatus = "COMx: Closed";
                SetStatus(true, "串口已关闭");
                return;
            }

            try
            {
                _session.PortName = PortName;
                _session.BaudRate = int.Parse(BaudRate);
                _session.DataBits = DataBitIndex switch
                {
                    0 => 5,
                    1 => 6,
                    2 => 7,
                    _ => 8,
                };
                _session.Parity = ParityIndex switch
                {
                    1 => Parity.Even,
                    2 => Parity.Odd,
                    _ => Parity.None,
                };
                _session.StopBits = StopBitIndex switch
                {
                    1 => StopBits.Two,
                    2 => StopBits.OnePointFive,
                    _ => StopBits.One,
                };
                _session.Handshake = FlowControlIndex switch
                {
                    1 => Handshake.XOnXOff,
                    2 => Handshake.RequestToSend,
                    _ => Handshake.None,
                };
                _session.FrameIntervalMs = Math.Max(1, FrameInterval);
                _session.Open();
                IsOpen = true;
                PortStatus = $"{PortName}: {BaudRate}, {_session.DataBits}, {_session.Parity}, {_session.StopBits}";
                SetStatus(true, "串口已打开");
            }
            catch (Exception ex)
            {
                _session.Close();
                IsOpen = false;
                PortStatus = "COMx: Closed";
                SetStatus(false, "打开失败: " + ex.Message);
            }
        }

        [RelayCommand]
        private void Send()
        {
            SendCore(GeneralSendData);
        }

        [RelayCommand]
        private void SendQuick(QuickSendItemViewModel? item)
        {
            if (item == null)
            {
                return;
            }

            if (!IsOpen)
            {
                SetStatus(false, "串口未打开");
                return;
            }

            if (string.IsNullOrEmpty(item.Data))
            {
                SetStatus(false, "发送区不能为空");
                return;
            }

            try
            {
                byte[] data = SendEncoder.Encode(item.Data, CommentEnable, AutoCount, ref _sequence, !SendAscii, AppendNewLine);
                WriteAndCount(data, echo: SendDisplayEnable && AutoLine);
            }
            catch (Exception ex)
            {
                SetStatus(false, ex.Message);
            }
        }

        [RelayCommand]
        private void AddQuickSend()
        {
            if (QuickSends.Count >= AppSettings.MaxQuickSend)
            {
                SetStatus(false, "达到最大条数 100");
                return;
            }

            QuickSends.Add(new QuickSendItemViewModel { Name = "Name", Data = "Data" });
            Renumber();
        }

        [RelayCommand]
        private void RemoveQuickSend(QuickSendItemViewModel? item)
        {
            if (item == null)
            {
                return;
            }

            QuickSends.Remove(item);
            Renumber();
        }

        [RelayCommand]
        private void ClearReceive()
        {
            ClearReceiveRequested?.Invoke();
        }

        [RelayCommand]
        private void ClearSend()
        {
            GeneralSendData = string.Empty;
        }

        [RelayCommand]
        private void ToggleCountMode()
        {
            CountFrames = !CountFrames;
            RefreshCounters();
        }

        [RelayCommand]
        private void ToggleTheme()
        {
            DisplayPlan1 = !DisplayPlan1;
            NotifyTheme();
        }

        [RelayCommand]
        private void ResetCounters()
        {
            _txBytes = _rxBytes = _txFrames = _rxFrames = 0;
            RefreshCounters();
        }

        /**
         * @brief 关闭串口、日志和监视
         */
        public void Dispose()
        {
            try
            {
                _repeatTimer.Stop();
                _replyTimer.Stop();
                _ledTimer.Stop();
                
                // 立即关闭串口,停止数据接收
                _session.Dispose();
                
                // 安全地关闭通道和处理程序
                if (_receiveChannel != null)
                {
                    try
                    {
                        _receiveChannel.Writer.Complete();
                    }
                    catch
                    {
                        // Channel may already be closed, ignore
                    }
                    
                    _receiveProcessor = null;
                }
                
                // 关闭日志
                EndLog();
                
                // 最后清理配置
                _store.Dispose();
            }
            catch (Exception)
            {
                // 忽略所有异常,确保关闭成功
            }
        }

        partial void OnRepeatSendChanged(bool value)
        {
            if (!value)
            {
                _repeatTimer.Stop();
                CanSend = true;
            }
        }

        partial void OnFrameIntervalChanged(int value)
        {
            _session.FrameIntervalMs = Math.Max(1, value);
        }

        partial void OnDisplayPlan1Changed(bool value)
        {
            NotifyTheme();
        }

        partial void OnIsOpenChanged(bool value)
        {
            OnPropertyChanged(nameof(OpenButtonText));
        }

        partial void OnHighlightEnableChanged(bool value)
        {
            ReloadHighlight();
            HighlightReloaded?.Invoke();
        }

        private void SendCore(string text)
        {
            if (!IsOpen)
            {
                RepeatSend = false;
                SetStatus(false, "串口未打开");
                return;
            }

            if (string.IsNullOrEmpty(text))
            {
                _replyTimer.Stop();
                AutoReply = false;
                _repeatTimer.Stop();
                RepeatSend = false;
                CanSend = true;
                SetStatus(false, "发送区不能为空");
                return;
            }

            try
            {
                byte[] data = SendEncoder.Encode(text, CommentEnable, AutoCount, ref _sequence, !SendAscii, AppendNewLine);
                CanSend = !RepeatSend;
                WriteAndCount(data, echo: SendDisplayEnable && AutoLine);
                if (RepeatSend)
                {
                    _repeatTimer.Interval = TimeSpan.FromMilliseconds(Math.Max(1, RepeatInterval));
                    _repeatTimer.Start();
                }
                else
                {
                    _repeatTimer.Stop();
                    CanSend = true;
                }
            }
            catch (Exception ex)
            {
                CanSend = true;
                SetStatus(false, ex.Message);
            }
        }

        private void WriteAndCount(byte[] data, bool echo)
        {
            _session.Write(data);
            _txBytes += data.Length;
            _txFrames++;
            RefreshCounters();
            SetStatus(true, data.Length + " 字节已发送");
            if (!echo)
            {
                return;
            }

            DisplayChunk chunk = DisplayText.FormatSend(data, ReceiveAscii, ShowTime, TimeNewLine, AutoLine);
            ChunkReady?.Invoke(chunk, SendToNewLineEnable);
            if (SendToFileEnable)
            {
                WriteLog(chunk.Text);
            }
        }

        private void OnFrame(byte[] data)
        {
            _rxBytes += data.Length;
            _rxFrames++;
            RefreshCounters();
            DisplayChunk chunk = DisplayText.FormatReceive(data, ReceiveAscii, ShowTime, TimeNewLine, AutoLine);
            WriteLog(chunk.Text);
            
            // 写入通道进行异步处理
            if (!_receiveChannel.Writer.TryWrite(chunk))
            {
                // 如果通道满了,在后台线程格式化并直接发送
                Task.Run(() => FormatAndSendBackground(chunk));
            }
            
            ReceiveActive = true;
            _ledTimer.Stop();
            _ledTimer.Start();
            if (AutoReply)
            {
                _replyTimer.Interval = TimeSpan.FromMilliseconds(Math.Max(1, AutoReplyDelay));
                _replyTimer.Start();
            }
        }

        /// <summary>
        /// /// 后台线程处理通道中的数据
        /// </summary>
        private async Task ProcessReceiveChannel()
        {
            await foreach (DisplayChunk chunk in _receiveChannel.Reader.ReadAllAsync())
            {
                FormatAndSend(chunk);
            }
        }

        /// <summary>
        /// /// 后台格式化并发送到UI线程
        /// </summary>
        private void FormatAndSendBackground(DisplayChunk chunk)
        {
            FormatAndSend(chunk);
        }

        /// <summary>
        /// /// 将chunk发送到UI线程
        /// </summary>
        private void FormatAndSend(DisplayChunk chunk)
        {
            TotalProcessedBytes += chunk.Text.Length;
            // 接收数据时自动滚动到底部
            ChunkReady?.Invoke(chunk, true);
        }

        private void OnSerialError(Exception exception)
        {
            _session.Close();
            IsOpen = false;
            PortStatus = "COMx: Closed";
            SetStatus(false, exception.Message);
        }

        private void WriteLog(string text)
        {
            if (_log == null)
            {
                return;
            }

            _log.Write(DateTime.Now.ToString("yyyy-MM-dd ") + AnsiParser.Strip(text));
        }

        private void RefreshCounters()
        {
            if (CountFrames)
            {
                TxText = "TX: " + _txFrames + " frames";
                RxText = "RX: " + _rxFrames + " frames";
                RateText = _txFrames == 0 ? "0%" : Math.Round(_rxFrames * 100.0 / _txFrames, 2) + "%";
            }
            else
            {
                TxText = "TX: " + _txBytes + " bytes";
                RxText = "RX: " + _rxBytes + " bytes";
                RateText = _txBytes == 0 ? "0%" : Math.Round(_rxBytes * 100.0 / _txBytes, 2) + "%";
            }
        }

        private void ReloadHighlight()
        {
            HighlightFile file = _store.LoadHighlight();
            bool enabled = HighlightEnable && file.Enable;
            Highlight.Load(file.Rules, enabled);
        }

        private void Renumber()
        {
            for (int i = 0; i < QuickSends.Count; i++)
            {
                QuickSends[i].Index = i + 1;
            }
        }

        private void SetStatus(bool ok, string message)
        {
            StatusOk = ok;
            StatusText = message;
        }

        private void NotifyTheme()
        {
            OnPropertyChanged(nameof(ReceiveForeground));
            OnPropertyChanged(nameof(ReceiveBackground));
            OnPropertyChanged(nameof(SendForeground));
            OnPropertyChanged(nameof(SendBackground));
        }
    }
}
