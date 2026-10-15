using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using O_ComTool.Core.Update;

namespace O_ComTool.App.Views
{
    public class UpdateWindow : Window
    {
        public UpdateWindow(UpdateInfo info, Action<bool> onSkip)
        {
            Title = "检查更新";
            Width = 420;
            Height = 280;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            var text = new TextBox
            {
                Text = "版本 " + info.Version + "\r\n" + info.Feature,
                IsReadOnly = true,
                AcceptsReturn = true,
                TextWrapping = TextWrapping.Wrap,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                Height = 140,
            };
            var open = new Button { Content = "打开下载页", Width = 100, Margin = new Thickness(0, 12, 8, 0) };
            open.Click += (_, _) =>
            {
                if (!string.IsNullOrWhiteSpace(info.Link))
                {
                    Process.Start(new ProcessStartInfo(info.Link) { UseShellExecute = true });
                }
            };
            var skip = new Button { Content = "跳过此版本", Width = 100, Margin = new Thickness(0, 12, 0, 0) };
            skip.Click += (_, _) =>
            {
                onSkip(true);
                DialogResult = true;
            };
            var buttons = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Right };
            buttons.Children.Add(open);
            buttons.Children.Add(skip);
            var panel = new DockPanel { Margin = new Thickness(12) };
            DockPanel.SetDock(buttons, Dock.Bottom);
            panel.Children.Add(buttons);
            panel.Children.Add(text);
            Content = panel;
        }
    }

    public class AboutWindow : Window
    {
        public AboutWindow(string version)
        {
            Title = "关于";
            Width = 360;
            Height = 200;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            ResizeMode = ResizeMode.NoResize;
            Content = new TextBlock
            {
                Margin = new Thickness(16),
                TextWrapping = TextWrapping.Wrap,
                Text = "O-ComTool V" + version + "\r\n.NET 10 WPF 串口调试助手。\r\n接收区支持 ANSI 颜色，高亮规则写在 highlight.json。",
            };
        }
    }

}

namespace O_ComTool.App.Views
{
    /**
     * @brief 与当前主题颜色一致的确认框
     */
    public static class ThemedDialog
    {
        /**
         * @brief 显示确定/取消确认框
         * @param owner: 父窗口
         * @param message: 提示文字
         * @param title: 窗口标题
         * @retval 选择确定时为 true
         */
        public static bool Confirm(Window owner, string message, string title)
        {
            var window = new Window
            {
                Title = title,
                Width = 420,
                SizeToContent = SizeToContent.Height,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                ResizeMode = ResizeMode.NoResize,
                ShowInTaskbar = false,
                Owner = owner,
                FontFamily = new System.Windows.Media.FontFamily("Microsoft YaHei"),
                FontSize = 14,
            };
            var text = new TextBlock
            {
                Text = message,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(20, 18, 20, 0),
            };
            var ok = new Button { Content = "确定", MinWidth = 88, IsDefault = true, Margin = new Thickness(0, 0, 8, 0) };
            if (Application.Current.TryFindResource("PrimaryButton") is Style primary)
            {
                ok.Style = primary;
            }

            var cancel = new Button { Content = "取消", MinWidth = 88, IsCancel = true };
            bool accepted = false;
            ok.Click += (_, _) =>
            {
                accepted = true;
                window.DialogResult = true;
            };
            var buttons = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Right,
                Margin = new Thickness(20, 18, 20, 18),
            };
            buttons.Children.Add(ok);
            buttons.Children.Add(cancel);
            var panel = new DockPanel();
            DockPanel.SetDock(buttons, Dock.Bottom);
            panel.Children.Add(buttons);
            panel.Children.Add(text);
            window.Content = panel;
            window.ShowDialog();
            return accepted;
        }
    }
}
