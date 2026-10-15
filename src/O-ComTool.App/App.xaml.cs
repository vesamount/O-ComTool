using System.Windows;
using O_ComTool.Core.Config;

namespace O_ComTool.App
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            EventManager.RegisterClassHandler(typeof(Window), FrameworkElement.LoadedEvent, new RoutedEventHandler(Window_Loaded));
            var store = new ConfigStore();
            store.EnsureDefaults();
            MainWindow window = new MainWindow(store, store.LoadSettings());
            MainWindow = window;
            window.Show();
        }

        private static void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is not Window window)
            {
                return;
            }

            if (window.ReadLocalValue(Window.BackgroundProperty) == DependencyProperty.UnsetValue)
            {
                window.SetResourceReference(Window.BackgroundProperty, "Brush.Window");
                window.SetResourceReference(Window.ForegroundProperty, "Brush.Text");
                window.FontFamily = new System.Windows.Media.FontFamily("Segoe UI");
            }

            ThemeChrome.ApplyTitle(window);
        }
    }
}
