using System.IO.Ports;

namespace O_ComTool.Core.Serial
{
    /**
     * @brief 串口会话。读线程只抛出帧数据，不接触界面
     */
    public sealed class SerialSession : IDisposable
    {
        private readonly object _gate = new();
        private SerialPort? _port;
        private Thread? _reader;
        private volatile bool _running;

        /**
         * @brief 收到一帧数据
         */
        public event EventHandler<byte[]>? FrameReceived;

        /**
         * @brief 读线程发生错误
         */
        public event EventHandler<Exception>? Error;

        public string PortName { get; set; } = string.Empty;

        public int BaudRate { get; set; } = 9600;

        public int DataBits { get; set; } = 8;

        public Parity Parity { get; set; } = Parity.None;

        public StopBits StopBits { get; set; } = StopBits.One;

        public Handshake Handshake { get; set; } = Handshake.None;

        public int FrameIntervalMs { get; set; } = 20;

        public bool IsOpen
        {
            get
            {
                lock (_gate)
                {
                    return _port?.IsOpen == true;
                }
            }
        }

        /**
         * @brief 枚举当前串口名
         * @retval 端口名
         */
        public static string[] GetPortNames()
        {
            return SerialPort.GetPortNames();
        }

        /**
         * @brief 按当前参数打开串口并启动读线程
         */
        public void Open()
        {
            lock (_gate)
            {
                if (_port?.IsOpen == true)
                {
                    return;
                }

                var port = new SerialPort(PortName, BaudRate, Parity, DataBits, StopBits)
                {
                    Handshake = Handshake,
                    ReadTimeout = 500,
                    WriteTimeout = 500,
                };
                port.Open();
                _port = port;
                _running = true;
                _reader = new Thread(ReadLoop)
                {
                    IsBackground = true,
                    Name = "O-ComTool.Serial",
                };
                _reader.Start();
            }
        }

        /**
         * @brief 停止读线程并关闭串口
         */
        public void Close()
        {
            Thread? reader;
            lock (_gate)
            {
                _running = false;
                reader = _reader;
                _reader = null;
                if (_port != null)
                {
                    try
                    {
                        if (_port.IsOpen)
                        {
                            _port.Close();
                        }
                    }
                    catch (Exception)
                    {
                    }

                    _port.Dispose();
                    _port = null;
                }
            }

            reader?.Join(1000);
        }

        /**
         * @brief 写入字节
         * @param data: 待发送数据
         */
        public void Write(byte[] data)
        {
            if (data.Length == 0)
            {
                return;
            }

            lock (_gate)
            {
                if (_port?.IsOpen != true)
                {
                    throw new InvalidOperationException("串口未打开");
                }

                _port.Write(data, 0, data.Length);
            }
        }

        /**
         * @brief 关闭串口并释放资源
         */
        public void Dispose()
        {
            Close();
        }

        private void ReadLoop()
        {
            while (_running)
            {
                try
                {
                    byte[] frame = FrameAssembler.ReadFrame(BytesAvailable, ReadExact, FrameIntervalMs);
                    if (frame.Length == 0)
                    {
                        if (!_running)
                        {
                            break;
                        }

                        Thread.Sleep(10);
                        continue;
                    }

                    FrameReceived?.Invoke(this, frame);
                }
                catch (Exception ex) when (ex is TimeoutException or InvalidOperationException or OperationCanceledException)
                {
                    if (!_running)
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    if (_running)
                    {
                        Error?.Invoke(this, ex);
                    }

                    break;
                }
            }
        }

        private int BytesAvailable()
        {
            lock (_gate)
            {
                return _running && _port?.IsOpen == true ? _port.BytesToRead : 0;
            }
        }

        private byte[] ReadExact(int length)
        {
            lock (_gate)
            {
                return ReadExactLocked(length);
            }
        }

        private byte[] ReadExactLocked(int length)
        {
            if (_port == null || length <= 0 || !_port.IsOpen)
            {
                return Array.Empty<byte>();
            }

            var buffer = new byte[length];
            int offset = 0;
            while (offset < length)
            {
                int read = _port.Read(buffer, offset, length - offset);
                if (read <= 0)
                {
                    break;
                }

                offset += read;
            }

            if (offset == length)
            {
                return buffer;
            }

            var actual = new byte[offset];
            Buffer.BlockCopy(buffer, 0, actual, 0, offset);
            return actual;
        }
    }
}
