using System.Windows;
using System.Windows.Controls;
using O_ComTool.Core.Text;

namespace O_ComTool.App.Views
{
    public class FormatWindow : Window
    {
        private readonly TextBox _source = new() { AcceptsReturn = true, Height = 100, TextWrapping = TextWrapping.Wrap, VerticalScrollBarVisibility = ScrollBarVisibility.Auto };
        private readonly TextBox _dest = new() { AcceptsReturn = true, Height = 100, TextWrapping = TextWrapping.Wrap, VerticalScrollBarVisibility = ScrollBarVisibility.Auto, IsReadOnly = true };
        private readonly CheckBox _prefix = new() { Content = "0x" };
        private readonly CheckBox _comma = new() { Content = "逗号" };
        private readonly CheckBox _point = new() { Content = "点" };
        private readonly CheckBox _suffix = new() { Content = "H" };
        private readonly CheckBox _space = new() { Content = "空格", IsChecked = true };

        public FormatWindow()
        {
            Title = "HEX 格式化";
            Width = 480;
            Height = 360;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            var row = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 8, 0, 0) };
            row.Children.Add(_prefix);
            row.Children.Add(_comma);
            row.Children.Add(_point);
            row.Children.Add(_suffix);
            row.Children.Add(_space);
            var button = new Button { Content = "转换", Width = 80, Margin = new Thickness(0, 8, 0, 0), HorizontalAlignment = HorizontalAlignment.Left };
            button.Click += (_, _) => _dest.Text = HexFormatter.Format(_source.Text, _prefix.IsChecked == true, _comma.IsChecked == true, _point.IsChecked == true, _suffix.IsChecked == true, _space.IsChecked == true);
            var panel = new StackPanel { Margin = new Thickness(12) };
            panel.Children.Add(new TextBlock { Text = "源数据" });
            panel.Children.Add(_source);
            panel.Children.Add(row);
            panel.Children.Add(button);
            panel.Children.Add(new TextBlock { Text = "结果", Margin = new Thickness(0, 8, 0, 0) });
            panel.Children.Add(_dest);
            Content = panel;
        }
    }
}
