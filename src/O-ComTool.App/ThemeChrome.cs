using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using O_ComTool.Core.Text;

namespace O_ComTool.App
{
    /**
     * @brief 切换窗口配色，并同步系统标题栏
     */
    internal static class ThemeChrome
    {
        /**
         * @brief 把界面资源切换到深色或浅色方案
         * @param dark: true 使用深色方案
         */
        public static void Apply(bool dark)
        {
            ResourceDictionary resources = Application.Current.Resources;
            resources["Theme.Dark"] = dark;
            if (dark)
            {
                Set(resources, "Brush.Window", "#1B1C17");
                Set(resources, "Brush.Panel", "#24251F");
                Set(resources, "Brush.PanelBorder", "#3C3D34");
                Set(resources, "Brush.Input", "#1B1C17");
                Set(resources, "Brush.InputBorder", "#4C4D42");
                Set(resources, "Brush.Text", "#F8F8F2");
                Set(resources, "Brush.Muted", "#8F907A");
                Set(resources, "Brush.Accent", "#A6E22E");
                Set(resources, "Brush.AccentText", "#1B1C17");
                Set(resources, "Brush.Danger", "#E06C75");
                Set(resources, "Brush.Hover", "#32332B");
                Set(resources, "Brush.Status", "#14150F");
                Set(resources, "Brush.Ghost", "#2C2D26");
            }
            else
            {
                Set(resources, "Brush.Window", "#F3F3EE");
                Set(resources, "Brush.Panel", "#FFFFFF");
                Set(resources, "Brush.PanelBorder", "#E0E0D6");
                Set(resources, "Brush.Input", "#FFFFFF");
                Set(resources, "Brush.InputBorder", "#D2D2C6");
                Set(resources, "Brush.Text", "#2C2D27");
                Set(resources, "Brush.Muted", "#6E6F64");
                Set(resources, "Brush.Accent", "#5C8A12");
                Set(resources, "Brush.AccentText", "#FFFFFF");
                Set(resources, "Brush.Danger", "#C44536");
                Set(resources, "Brush.Hover", "#EEEEE6");
                Set(resources, "Brush.Status", "#E7E7DF");
                Set(resources, "Brush.Ghost", "#F7F7F2");
            }

            foreach (Window window in Application.Current.Windows)
            {
                ApplyTitle(window, dark);
            }
        }

        /**
         * @brief 按当前方案给单个窗口标题栏上色
         * @param window: 目标窗口
         */
        public static void ApplyTitle(Window window)
        {
            ApplyTitle(window, Application.Current.Resources["Theme.Dark"] is true);
        }

        private static void ApplyTitle(Window window, bool dark)
        {
            IntPtr hwnd = new WindowInteropHelper(window).Handle;
            if (hwnd == IntPtr.Zero)
            {
                return;
            }

            int enabled = dark ? 1 : 0;
            DwmSetWindowAttribute(hwnd, 20, ref enabled, sizeof(int));
            DwmSetWindowAttribute(hwnd, 19, ref enabled, sizeof(int));
            int caption = dark ? ColorRef("#1B1C17") : ColorRef("#F3F3EE");
            int text = dark ? ColorRef("#F8F8F2") : ColorRef("#2C2D27");
            DwmSetWindowAttribute(hwnd, 35, ref caption, sizeof(int));
            DwmSetWindowAttribute(hwnd, 36, ref text, sizeof(int));
        }

        private static void Set(ResourceDictionary resources, string key, string hex)
        {
            if (!ColorRgba.TryParse(hex, out ColorRgba color))
            {
                return;
            }

            var brush = new SolidColorBrush(Color.FromRgb(color.R, color.G, color.B));
            brush.Freeze();
            resources[key] = brush;
        }

        private static int ColorRef(string hex)
        {
            if (!ColorRgba.TryParse(hex, out ColorRgba color))
            {
                return 0;
            }

            return color.R | (color.G << 8) | (color.B << 16);
        }

        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attribute, ref int value, int size);
    }
}
