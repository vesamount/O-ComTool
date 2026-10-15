using System.Diagnostics;
using System.Windows;
using O_ComTool.Core.Config;

namespace O_ComTool.App.Views
{
    public partial class OptionsWindow : Window
    {
        private readonly string _highlightPath;

        public OptionsWindow(AppSettings settings, string highlightPath)
        {
            InitializeComponent();
            Settings = settings;
            _highlightPath = highlightPath;
            DataContext = settings;
        }

        public AppSettings Settings { get; }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void OpenHighlight_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo(_highlightPath) { UseShellExecute = true });
        }
    }
}
