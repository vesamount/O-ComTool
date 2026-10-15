using System.Windows;
using System.Windows.Controls;
using O_ComTool.Core.Check;
using O_ComTool.Core.Text;

namespace O_ComTool.App.Views
{
    public class CheckWindow : Window
    {
        private readonly TextBox _input = new() { AcceptsReturn = true, TextWrapping = TextWrapping.Wrap, Height = 120, VerticalScrollBarVisibility = ScrollBarVisibility.Auto };
        private readonly TextBox _result = new() { IsReadOnly = true, Margin = new Thickness(0, 8, 0, 0) };
        private readonly ComboBox _mode = new();

        public CheckWindow()
        {
            Title = "校验";
            Width = 420;
            Height = 320;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            _mode.Items.Add("校验和");
            _mode.Items.Add("异或");
            _mode.Items.Add("CRC16 Modbus");
            _mode.Items.Add("CRC32");
            _mode.SelectedIndex = 0;
            var button = new Button { Content = "计算", Width = 80, Margin = new Thickness(0, 8, 0, 0), HorizontalAlignment = HorizontalAlignment.Left };
            button.Click += (_, _) => Calculate();
            var panel = new StackPanel { Margin = new Thickness(12) };
            panel.Children.Add(new TextBlock { Text = "HEX 数据" });
            panel.Children.Add(_input);
            panel.Children.Add(_mode);
            panel.Children.Add(button);
            panel.Children.Add(_result);
            Content = panel;
        }

        private void Calculate()
        {
            byte[] data = HexCodec.Parse(_input.Text);
            _result.Text = _mode.SelectedIndex switch
            {
                1 => "0x" + Checksum.Xor(data).ToString("X2"),
                2 => "0x" + Checksum.Crc16Modbus(data).ToString("X4"),
                3 => "0x" + Checksum.Crc32(data).ToString("X8"),
                _ => "0x" + Checksum.Sum(data).ToString("X2"),
            };
        }
    }
}
