using System.Windows;
using System.Windows.Controls;

namespace O_ComTool.App.Views
{
    public class NoteWindow : Window
    {
        private readonly TextBox _box = new() { AcceptsReturn = false, Margin = new Thickness(0, 8, 0, 0) };

        public NoteWindow(string note)
        {
            Title = "窗口备注";
            Width = 360;
            Height = 160;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            ResizeMode = ResizeMode.NoResize;
            _box.Text = note;
            var ok = new Button { Content = "确定", Width = 80, IsDefault = true, Margin = new Thickness(0, 12, 8, 0), HorizontalAlignment = HorizontalAlignment.Right };
            ok.Click += (_, _) => { NoteText = _box.Text; DialogResult = true; };
            var panel = new DockPanel { Margin = new Thickness(12) };
            DockPanel.SetDock(ok, Dock.Bottom);
            panel.Children.Add(ok);
            panel.Children.Add(new TextBlock { Text = "备注会显示在窗口标题中，用来区分多个串口。" });
            panel.Children.Add(_box);
            Content = panel;
        }

        public string NoteText { get; private set; } = string.Empty;
    }
}
