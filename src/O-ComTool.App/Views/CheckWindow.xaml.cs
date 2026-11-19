using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using O_ComTool.Core.Check;
using O_ComTool.Core.Text;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

namespace O_ComTool.App.Views
{
    /**
     * @brief 校验工具：校验和、异或、常用 CRC 模型与自定义 CRC
     */
    public partial class CheckWindow : Window
    {
        private bool _updating = true;

        public CheckWindow()
        {
            InitializeComponent();
            List<AlgorithmItem> algorithms = BuildAlgorithms();
            AlgorithmBox.ItemsSource = algorithms;
            WidthBox.ItemsSource = Enumerable.Range(1, 32).ToArray();
            AlgorithmBox.SelectedItem = algorithms.First(item => item.Model?.Name == "CRC-16/MODBUS");
            _updating = false;
            ApplyAlgorithmState();
        }

        private static List<AlgorithmItem> BuildAlgorithms()
        {
            var algorithms = new List<AlgorithmItem>
            {
                new("校验和", AlgorithmKind.Sum, null),
                new("异或", AlgorithmKind.Xor, null),
            };
            algorithms.AddRange(CrcCatalog.All.Select(model => new AlgorithmItem(model.Name, AlgorithmKind.Crc, model)));
            algorithms.Add(new AlgorithmItem("自定义", AlgorithmKind.Custom, null));
            return algorithms;
        }

        private void Algorithm_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (_updating)
            {
                return;
            }

            ApplyAlgorithmState();
        }

        private void ApplyAlgorithmState()
        {
            if (AlgorithmBox.SelectedItem is not AlgorithmItem item)
            {
                return;
            }

            bool isCrc = item.Kind is AlgorithmKind.Crc or AlgorithmKind.Custom;
            CrcParamsPanel.Visibility = isCrc ? Visibility.Visible : Visibility.Collapsed;

            if (item.Kind == AlgorithmKind.Crc)
            {
                SetParametersEnabled(false);
                LoadModel(item.Model!);
                ParamHint.Text = "内置模型参数固定；需要修改参数时请选择“自定义”。";
            }
            else if (item.Kind == AlgorithmKind.Custom)
            {
                SetParametersEnabled(true);
                ParamHint.Text = "自定义：宽度 1~32 位，多项式 / 初值 / 结果异或为十六进制，可省略 0x。";
            }

            Calculate();
        }

        private void SetParametersEnabled(bool enabled)
        {
            WidthBox.IsEnabled = enabled;
            PolyBox.IsEnabled = enabled;
            InitBox.IsEnabled = enabled;
            XorOutBox.IsEnabled = enabled;
            RefInBox.IsEnabled = enabled;
            RefOutBox.IsEnabled = enabled;
        }

        private void LoadModel(CrcModel model)
        {
            bool wasUpdating = _updating;
            _updating = true;
            WidthBox.SelectedItem = model.Width;
            PolyBox.Text = FormatHex(model.Poly, model.Width);
            InitBox.Text = FormatHex(model.Init, model.Width);
            XorOutBox.Text = FormatHex(model.XorOut, model.Width);
            RefInBox.IsChecked = model.RefIn;
            RefOutBox.IsChecked = model.RefOut;
            _updating = wasUpdating;
        }

        private void InputMode_Changed(object sender, RoutedEventArgs e)
        {
            if (_updating)
            {
                return;
            }

            InputHint.Text = AsciiMode.IsChecked == true
                ? "ASCII：按 UTF-8 编码取字节，可直接输入中文。"
                : "HEX：空格、逗号、0x 前缀均可，例如 01 03 00 00 00 0A。";
            Calculate();
        }

        private void Width_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (_updating)
            {
                return;
            }

            Calculate();
        }

        private void ParamText_Changed(object sender, TextChangedEventArgs e)
        {
            if (_updating)
            {
                return;
            }

            Calculate(quiet: true);
        }

        private void Ref_Changed(object sender, RoutedEventArgs e)
        {
            if (_updating)
            {
                return;
            }

            Calculate();
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            Calculate();
        }

        private void Input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && (Keyboard.Modifiers & ModifierKeys.Control) != 0)
            {
                Calculate();
                e.Handled = true;
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Clear();
            InputBox.Focus();
            Calculate();
        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Clipboard.SetText(ResultHex.Text);
                SetStatus("已复制 " + ResultHex.Text, error: false);
            }
            catch (Exception ex)
            {
                SetStatus("复制失败：" + ex.Message, error: true);
            }
        }

        /**
         * @brief 按当前算法和输入重新计算
         * @param quiet: true 时不显示参数错误，用于正在输入的场景
         */
        private void Calculate(bool quiet = false)
        {
            if (AlgorithmBox.SelectedItem is not AlgorithmItem item)
            {
                return;
            }

            byte[] data = ReadInput(out bool ascii);
            uint value;
            int width;
            CrcModel? model = null;

            switch (item.Kind)
            {
                case AlgorithmKind.Sum:
                    value = Checksum.Sum(data);
                    width = 8;
                    break;
                case AlgorithmKind.Xor:
                    value = Checksum.Xor(data);
                    width = 8;
                    break;
                case AlgorithmKind.Crc:
                    model = item.Model!;
                    value = CrcCalculator.Compute(data, model);
                    width = model.Width;
                    break;
                case AlgorithmKind.Custom:
                    if (!TryBuildCustomModel(out model, out string error))
                    {
                        SetStatus(quiet ? "参数不完整，暂未重新计算。" : error, error: !quiet);
                        return;
                    }

                    value = CrcCalculator.Compute(data, model);
                    width = model.Width;
                    break;
                default:
                    return;
            }

            ResultHex.Text = "0x" + FormatHex(value, width);
            ResultBin.Text = FormatBinary(value, width);
            ResultBytes.Text = FormatBytes(value, width);
            InputInfo.Text = $"{(ascii ? "ASCII" : "HEX")} · {data.Length} 字节";
            ParamSummary.Text = BuildParamSummary(item, model);
            SetStatus(data.Length == 0 ? "输入为空，按 0 字节计算。" : "计算完成", error: false);
        }

        private byte[] ReadInput(out bool ascii)
        {
            ascii = AsciiMode.IsChecked == true;
            if (InputBox.Text.Length == 0)
            {
                return Array.Empty<byte>();
            }

            return ascii ? Encoding.UTF8.GetBytes(InputBox.Text) : HexCodec.Parse(InputBox.Text);
        }

        private bool TryBuildCustomModel(out CrcModel model, out string error)
        {
            model = null!;
            error = string.Empty;
            if (WidthBox.SelectedItem is not int width)
            {
                error = "请选择校验宽度。";
                return false;
            }

            uint mask = CrcModel.MaskOf(width);
            if (!TryParseHex(PolyBox.Text, out uint poly))
            {
                error = "多项式格式不正确，请输入十六进制，例如 8005。";
                return false;
            }

            if (!TryParseHex(InitBox.Text, out uint init))
            {
                error = "初值格式不正确，请输入十六进制，例如 FFFF。";
                return false;
            }

            if (!TryParseHex(XorOutBox.Text, out uint xorOut))
            {
                error = "结果异或值格式不正确，请输入十六进制，例如 0000。";
                return false;
            }

            if (poly > mask || init > mask || xorOut > mask)
            {
                error = $"参数超出 {width} 位范围。";
                return false;
            }

            model = new CrcModel("自定义", width, poly, init, xorOut, RefInBox.IsChecked == true, RefOutBox.IsChecked == true);
            return true;
        }

        private static bool TryParseHex(string? text, out uint value)
        {
            value = 0;
            if (string.IsNullOrWhiteSpace(text))
            {
                return false;
            }

            string cleaned = text.Trim();
            if (cleaned.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            {
                cleaned = cleaned[2..];
            }

            cleaned = cleaned.Replace(" ", string.Empty).Replace("\t", string.Empty).Replace("_", string.Empty);
            return cleaned.Length is > 0 and <= 8
                && uint.TryParse(cleaned, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out value);
        }

        private static string BuildParamSummary(AlgorithmItem item, CrcModel? model)
        {
            if (item.Kind == AlgorithmKind.Sum)
            {
                return "校验和：全部字节累加后取低 8 位（宽度 8）。";
            }

            if (item.Kind == AlgorithmKind.Xor)
            {
                return "异或：全部字节逐位异或（宽度 8）。";
            }

            if (model == null)
            {
                return string.Empty;
            }

            string check = model.Check is uint expected
                ? $"    标准校验值（ASCII 123456789）0x{FormatHex(expected, model.Width)}"
                : string.Empty;
            return $"WIDTH {model.Width}    POLY 0x{FormatHex(model.Poly, model.Width)}    INIT 0x{FormatHex(model.Init, model.Width)}"
                + $"    XOROUT 0x{FormatHex(model.XorOut, model.Width)}    REFIN {BoolText(model.RefIn)}    REFOUT {BoolText(model.RefOut)}{check}";
        }

        private static string BoolText(bool value)
        {
            return value ? "true" : "false";
        }

        private static string FormatHex(uint value, int width)
        {
            int digits = (width + 3) / 4;
            return value.ToString("X" + digits, CultureInfo.InvariantCulture);
        }

        private static string FormatBinary(uint value, int width)
        {
            string bits = Convert.ToString(value, 2).PadLeft(width, '0');
            var builder = new StringBuilder(bits.Length + (bits.Length / 4));
            for (int i = 0; i < bits.Length; i++)
            {
                if (i > 0 && (bits.Length - i) % 4 == 0)
                {
                    builder.Append(' ');
                }

                builder.Append(bits[i]);
            }

            return builder.ToString();
        }

        private static string FormatBytes(uint value, int width)
        {
            int count = (width + 7) / 8;
            var bytes = new byte[count];
            for (int i = 0; i < count; i++)
            {
                bytes[i] = (byte)(value >> (8 * (count - 1 - i)));
            }

            string high = string.Join(" ", bytes.Select(item => item.ToString("X2", CultureInfo.InvariantCulture)));
            if (count == 1)
            {
                return high;
            }

            string low = string.Join(" ", bytes.Reverse().Select(item => item.ToString("X2", CultureInfo.InvariantCulture)));
            return $"高字节在前 {high}    低字节在前 {low}";
        }

        private void SetStatus(string text, bool error)
        {
            StatusText.Text = text;
            StatusText.SetResourceReference(TextBlock.ForegroundProperty, error ? "Brush.Danger" : "Brush.Muted");
        }

        private enum AlgorithmKind
        {
            Sum,
            Xor,
            Crc,
            Custom,
        }

        private sealed record AlgorithmItem(string Name, AlgorithmKind Kind, CrcModel? Model)
        {
            public override string ToString()
            {
                return Name;
            }
        }
    }
}
