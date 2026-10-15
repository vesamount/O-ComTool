using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ICSharpCode.AvalonEdit;
using Microsoft.Win32;
using O_ComTool.App.Editor;
using O_ComTool.App.ViewModels;
using O_ComTool.App.Views;
using O_ComTool.Core.Check;
using O_ComTool.Core.Config;
using O_ComTool.Core.Text;
using O_ComTool.Core.Update;

namespace O_ComTool.App
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;
        private readonly ReceiveColorizer _colorizer = new();
        private readonly System.Windows.Forms.NotifyIcon _tray = new();
        private int _findIndex;
        private bool _exitRequested;

        public MainWindow(ConfigStore store, AppSettings settings)
        {
            InitializeComponent();
            _viewModel = new MainViewModel(store, settings);
            DataContext = _viewModel;
            BaudBox.ItemsSource = new[] { "1200", "2400", "4800", "9600", "19200", "38400", "57600", "115200" };
            _colorizer.Engine = _viewModel.Highlight;
            ReceiveEditor.TextArea.TextView.LineTransformers.Add(_colorizer);
            ReceiveEditor.ContextMenu = BuildMenu(ReceiveEditor);
            SendBox.ContextMenu = BuildMenu(null);
            _viewModel.ChunkReady += AppendChunk;
            _viewModel.ClearReceiveRequested += ClearReceive;
            _viewModel.HighlightReloaded += () => ReceiveEditor.TextArea.TextView.Redraw();
            _viewModel.PropertyChanged += (_, args) =>
            {
                if (args.PropertyName is nameof(MainViewModel.ReceiveForeground)
                    or nameof(MainViewModel.ReceiveBackground)
                    or nameof(MainViewModel.SendForeground)
                    or nameof(MainViewModel.SendBackground)
                    or nameof(MainViewModel.HighlightEnable))
                {
                    ApplyTheme();
                }
            };
            ApplyTheme();
            UpdateTitle();
            SetupTray();
            Loaded += (_, _) => CheckUpdateOnStartup();
        }

        protected override void OnStateChanged(EventArgs e)
        {
            base.OnStateChanged(e);
            bool maximized = WindowState == WindowState.Maximized;
            WindowFrame.Padding = maximized ? new Thickness(7) : new Thickness(0);
            MaxButton.Content = maximized ? "❐" : "□";
            MaxButton.ToolTip = maximized ? "还原" : "最大化";
            if (_viewModel != null && WindowState == WindowState.Minimized && _viewModel.MinToTray)
            {
                HideToTray();
            }
        }

        /**
         * @brief 最小化主窗口
         * @param sender: 标题栏最小化按钮
         * @param e: 路由事件参数
         */
        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        /**
         * @brief 在最大化和还原之间切换
         * @param sender: 标题栏最大化按钮
         * @param e: 路由事件参数
         */
        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        /**
         * @brief 请求关闭主窗口
         * @param sender: 标题栏关闭按钮
         * @param e: 路由事件参数
         */
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (_exitRequested)
            {
                PersistAndClose();
                base.OnClosing(e);
                return;
            }

            if (_viewModel.CloseToTray)
            {
                e.Cancel = true;
                HideToTray();
                return;
            }

            if (_viewModel.QuitConfirm)
            {
                if (!ThemedDialog.Confirm(this, "确认退出当前串口工具？", "O-ComTool 确认"))
                {
                    e.Cancel = true;
                    return;
                }
            }

            PersistAndClose();
            base.OnClosing(e);
        }

        private void PersistAndClose()
        {
            _viewModel.Save();
            _viewModel.Dispose();
            _tray.Visible = false;
            _tray.Dispose();
        }

        private void SetupTray()
        {
            _tray.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Environment.ProcessPath ?? string.Empty) ?? System.Drawing.SystemIcons.Application;
            _tray.Text = "O-ComTool";
            var menu = new System.Windows.Forms.ContextMenuStrip();
            menu.Items.Add("显示", null, (_, _) => Dispatcher.Invoke(ShowFromTray));
            menu.Items.Add("退出", null, (_, _) => Dispatcher.Invoke(ExitFromTray));
            _tray.ContextMenuStrip = menu;
            _tray.DoubleClick += (_, _) => Dispatcher.Invoke(ShowFromTray);
        }

        private void HideToTray()
        {
            _tray.Visible = true;
            ShowInTaskbar = false;
            Hide();
        }

        private void ShowFromTray()
        {
            Show();
            WindowState = WindowState.Normal;
            ShowInTaskbar = true;
            Activate();
            _tray.Visible = false;
        }

        private void ExitFromTray()
        {
            _exitRequested = true;
            Close();
        }

        private void AppendChunk(DisplayChunk chunk, bool forceScroll)
        {
            TextEditor editor = ReceiveEditor;
            bool nearEnd = editor.ExtentHeight - editor.VerticalOffset - editor.ViewportHeight < 48;
            int offset = editor.Document.TextLength;
            _colorizer.Store.Add(offset, chunk.Runs);
            editor.Document.Insert(offset, chunk.Text);
            if (nearEnd || forceScroll)
            {
                editor.ScrollToEnd();
            }
        }

        private void ClearReceive()
        {
            ReceiveEditor.Clear();
            _colorizer.Store.Clear();
        }

        private void ApplyTheme()
        {
            ThemeChrome.Apply(_viewModel.DisplayPlan1);
            _colorizer.HighlightEnabled = _viewModel.HighlightEnable;
            ReceiveEditor.Foreground = BrushOf(_viewModel.ReceiveForeground, Brushes.Black);
            ReceiveEditor.Background = BrushOf(_viewModel.ReceiveBackground, Brushes.White);
            ReceiveEditor.TextArea.Caret.CaretBrush = ReceiveEditor.Foreground;
            SendBox.Foreground = BrushOf(_viewModel.SendForeground, Brushes.Black);
            SendBox.Background = BrushOf(_viewModel.SendBackground, Brushes.White);
            SendBox.CaretBrush = SendBox.Foreground;
            ReceiveEditor.TextArea.TextView.Redraw();
        }

        private static SolidColorBrush BrushOf(string hex, SolidColorBrush fallback)
        {
            if (!ColorRgba.TryParse(hex, out ColorRgba color))
            {
                return fallback;
            }

            var brush = new SolidColorBrush(Color.FromRgb(color.R, color.G, color.B));
            brush.Freeze();
            return brush;
        }

        private void UpdateTitle()
        {
            string version = ProductVersion();
            Title = string.IsNullOrWhiteSpace(_viewModel.Note)
                ? "O-ComTool V" + version
                : "O-ComTool V" + version + " - " + _viewModel.Note;
            _tray.Text = Title.Length > 63 ? Title[..63] : Title;
        }

        private void CheckUpdateOnStartup()
        {
            if (!DateTime.TryParse(_viewModel.LastCheckTime, out DateTime last) || DateTime.Now - last < TimeSpan.FromDays(1))
            {
                return;
            }

            string version = ProductVersion();
            Task.Run(() =>
            {
                UpdateInfo info = UpdateChecker.Check(version);
                Dispatcher.Invoke(() =>
                {
                    _viewModel.MarkUpdateChecked();
                    if (info.Valid && info.Version != version && info.Version != _viewModel.SkipVersion)
                    {
                        new UpdateWindow(info, skipped =>
                        {
                            if (skipped)
                            {
                                _viewModel.SkipUpdate(info.Version);
                            }
                        }).ShowDialog();
                    }
                });
            });
        }

        private static string ProductVersion()
        {
            string? version = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
            if (string.IsNullOrWhiteSpace(version))
            {
                return "3.0.0";
            }

            int plus = version.IndexOf('+');
            return plus > 0 ? version[..plus] : version;
        }

        private ContextMenu BuildMenu(TextEditor? editor)
        {
            var menu = new ContextMenu();
            menu.Items.Add(Item("全选", () => SelectAll(editor)));
            menu.Items.Add(Item("复制", () => CopySelection(editor)));
            menu.Items.Add(Item("粘贴", () => Paste(editor)));
            menu.Items.Add(new Separator());
            menu.Items.Add(Item("HEX 转 ASCII", () => ReplaceSelection(editor, text => HexCodec.ToAscii(HexCodec.Parse(text)))));
            menu.Items.Add(Item("ASCII 转 HEX", () => ReplaceSelection(editor, text => HexCodec.ToHex(System.Text.Encoding.UTF8.GetBytes(text)))));
            menu.Items.Add(Item("HEX 格式化", () => ReplaceSelection(editor, text => HexFormatter.Format(text, false, false, false, false, true))));
            menu.Items.Add(Item("计算长度", () => ShowResult("长度 " + HexCodec.Parse(SelectedText(editor)).Length + " 字节")));
            menu.Items.Add(Item("校验和", () => ShowResult("0x" + Checksum.Sum(HexCodec.Parse(SelectedText(editor))).ToString("X2"))));
            menu.Items.Add(Item("异或", () => ShowResult("0x" + Checksum.Xor(HexCodec.Parse(SelectedText(editor))).ToString("X2"))));
            return menu;
        }

        private static MenuItem Item(string header, Action action)
        {
            var item = new MenuItem { Header = header };
            item.Click += (_, _) => action();
            return item;
        }

        private string SelectedText(TextEditor? editor)
        {
            if (editor != null)
            {
                return editor.SelectedText ?? string.Empty;
            }

            return SendBox.SelectedText ?? string.Empty;
        }

        private void SelectAll(TextEditor? editor)
        {
            if (editor != null)
            {
                editor.SelectAll();
                return;
            }

            SendBox.SelectAll();
        }

        private void CopySelection(TextEditor? editor)
        {
            string text = SelectedText(editor);
            if (!string.IsNullOrEmpty(text))
            {
                Clipboard.SetText(text);
            }
        }

        private void Paste(TextEditor? editor)
        {
            if (!Clipboard.ContainsText())
            {
                return;
            }

            ReplaceSelection(editor, _ => Clipboard.GetText());
        }

        private void ReplaceSelection(TextEditor? editor, Func<string, string> transform)
        {
            string source = SelectedText(editor);
            string result = transform(source);
            if (editor != null)
            {
                int start = editor.SelectionStart;
                int length = editor.SelectionLength;
                editor.Document.Replace(start, length, result);
                _colorizer.Store.ShiftAfterReplace(start, length, result.Length);
                editor.TextArea.TextView.Redraw();
                return;
            }

            int sendStart = SendBox.SelectionStart;
            SendBox.SelectedText = result;
            SendBox.SelectionStart = sendStart;
            SendBox.SelectionLength = result.Length;
        }

        private void ShowResult(string text)
        {
            MessageBox.Show(this, text, "O-ComTool", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Ports_DropDownOpened(object sender, EventArgs e)
        {
            _viewModel.RefreshPorts();
        }

        private void Packet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox box && box.SelectedItem is string line)
            {
                _viewModel.GeneralSendData = line;
            }
        }

        private void LoadFile_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog { Filter = "文本文件|*.txt|所有文件|*.*" };
            if (dialog.ShowDialog(this) == true)
            {
                _viewModel.LoadPacketFile(dialog.FileName);
            }
        }

        private void AutoSave_Checked(object sender, RoutedEventArgs e)
        {
            if (_viewModel == null || _viewModel.AutoSave)
            {
                return;
            }

            var dialog = new SaveFileDialog
            {
                Filter = "文本文件|*.txt|所有文件|*.*",
                FileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "-Log.txt",
            };
            if (dialog.ShowDialog(this) == true)
            {
                _viewModel.BeginLog(dialog.FileName);
            }
            else if (sender is CheckBox box)
            {
                box.IsChecked = false;
            }
        }

        private void AutoSave_Unchecked(object sender, RoutedEventArgs e)
        {
    if (_viewModel?.AutoSave == true)
    {
        _viewModel.EndLog();
    }
        }

        private void Find_Click(object sender, RoutedEventArgs e)
        {
            string needle = SearchBox.Text;
            if (string.IsNullOrEmpty(needle))
            {
                return;
            }

            string all = ReceiveEditor.Text ?? string.Empty;
            int found = all.IndexOf(needle, Math.Min(_findIndex, all.Length), StringComparison.OrdinalIgnoreCase);
            if (found < 0)
            {
                found = all.IndexOf(needle, StringComparison.OrdinalIgnoreCase);
            }

            if (found < 0)
            {
                return;
            }

            ReceiveEditor.Select(found, needle.Length);
            ICSharpCode.AvalonEdit.Document.DocumentLine line = ReceiveEditor.Document.GetLineByOffset(found);
            ReceiveEditor.ScrollTo(line.LineNumber, 0);
            _findIndex = found + needle.Length;
        }

        private void CountMode_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.ToggleCountModeCommand.Execute(null);
        }

        private void ResetCount_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.ResetCountersCommand.Execute(null);
        }

        private void Theme_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.ToggleThemeCommand.Execute(null);
        }

        /**
         * @brief 展开或收起右侧快捷发送栏
         * @param sender: 标题按钮或收起后的侧栏
         * @param e: 路由事件参数
         */
        private void ToggleQuick_Click(object sender, RoutedEventArgs e)
        {
            bool open = QuickPanel.Visibility != Visibility.Visible;
            QuickPanel.Visibility = open ? Visibility.Visible : Visibility.Collapsed;
            QuickRail.Visibility = open ? Visibility.Collapsed : Visibility.Visible;
            QuickColumn.Width = open ? new GridLength(390) : GridLength.Auto;
        }

        private void Import_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog { Filter = "配置文件|*.ini|所有文件|*.*" };
            if (dialog.ShowDialog(this) == true)
            {
                try
                {
                    _viewModel.ImportIni(dialog.FileName);
                    ApplyTheme();
                    UpdateTitle();
                    MessageBox.Show(this, "导入配置文件成功！", "O-ComTool", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "导入配置文件失败：" + ex.Message, "O-ComTool", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog { Filter = "配置文件|*.ini|所有文件|*.*", FileName = "O-ComTool_cfg.ini" };
            if (dialog.ShowDialog(this) == true)
            {
                _viewModel.ExportIni(dialog.FileName);
                MessageBox.Show(this, "配置文件导出成功！", "O-ComTool", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            _exitRequested = true;
            Close();
        }

        private void Options_Click(object sender, RoutedEventArgs e)
        {
            var window = new OptionsWindow(_viewModel.CaptureSettings(), _viewModel.HighlightPath) { Owner = this };
            if (window.ShowDialog() == true)
            {
                _viewModel.ApplySettings(window.Settings, reloadHighlight: false);
                _viewModel.Save();
                ApplyTheme();
            }
        }

        private void Note_Click(object sender, RoutedEventArgs e)
        {
            var window = new NoteWindow(_viewModel.Note) { Owner = this };
            if (window.ShowDialog() == true)
            {
                _viewModel.Note = window.NoteText;
                UpdateTitle();
            }
        }

        private void Check_Click(object sender, RoutedEventArgs e)
        {
            new CheckWindow { Owner = this }.Show();
        }

        private void Format_Click(object sender, RoutedEventArgs e)
        {
            new FormatWindow { Owner = this }.Show();
        }

        private void Ascii_Click(object sender, RoutedEventArgs e)
        {
            new AsciiWindow { Owner = this }.Show();
        }

        private void Calc_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("calc.exe") { UseShellExecute = true });
        }

        private void DeviceManager_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("devmgmt.msc") { UseShellExecute = true });
        }

        private void OpenLog_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_viewModel.LogPath) && System.IO.File.Exists(_viewModel.LogPath))
            {
                Process.Start(new ProcessStartInfo("explorer.exe", "/select,\"" + _viewModel.LogPath + "\"") { UseShellExecute = true });
                return;
            }

            Process.Start(new ProcessStartInfo("explorer.exe", "/select,\"" + _viewModel.HighlightPath + "\"") { UseShellExecute = true });
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            UpdateInfo info = UpdateChecker.Check(ProductVersion());
            if (!info.Valid)
            {
                MessageBox.Show(this, "检查更新失败。", "O-ComTool", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            new UpdateWindow(info, skipped =>
            {
                if (skipped)
                {
                    _viewModel.SkipUpdate(info.Version);
                }
            })
            { Owner = this }.ShowDialog();
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("http://www.ifreehub.com") { UseShellExecute = true });
        }

        private void Donate_Click(object sender, RoutedEventArgs e)
        {
            new DonateWindow { Owner = this }.ShowDialog();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            new AboutWindow(ProductVersion()) { Owner = this }.ShowDialog();
        }
    }
}
