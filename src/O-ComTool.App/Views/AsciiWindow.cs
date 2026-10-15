using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace O_ComTool.App.Views
{
    public class AsciiWindow : Window
    {
        public AsciiWindow()
        {
            Title = "ASCII 表";
            Width = 420;
            Height = 480;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            var grid = new DataGrid { AutoGenerateColumns = false, IsReadOnly = true, ItemsSource = Build() };
            grid.Columns.Add(new DataGridTextColumn { Header = "Dec", Binding = new Binding("Dec"), Width = 60 });
            grid.Columns.Add(new DataGridTextColumn { Header = "Hex", Binding = new Binding("Hex"), Width = 60 });
            grid.Columns.Add(new DataGridTextColumn { Header = "Char", Binding = new Binding("Char"), Width = 80 });
            grid.Columns.Add(new DataGridTextColumn { Header = "Name", Binding = new Binding("Name"), Width = 160 });
            Content = grid;
        }

        private static List<AsciiRow> Build()
        {
            string[] names =
            {
                "NUL", "SOH", "STX", "ETX", "EOT", "ENQ", "ACK", "BEL", "BS", "TAB", "LF", "VT", "FF", "CR", "SO", "SI",
                "DLE", "DC1", "DC2", "DC3", "DC4", "NAK", "SYN", "ETB", "CAN", "EM", "SUB", "ESC", "FS", "GS", "RS", "US",
            };
            var rows = new List<AsciiRow>();
            for (int i = 0; i < 128; i++)
            {
                string visible = i switch
                {
                    < 32 => names[i],
                    127 => "DEL",
                    _ => ((char)i).ToString(),
                };
                rows.Add(new AsciiRow(i, i.ToString("X2"), visible, i < 32 ? "控制字符" : i == 127 ? "删除" : "可打印"));
            }

            return rows;
        }

        private sealed record AsciiRow(int Dec, string Hex, string Char, string Name);
    }
}
