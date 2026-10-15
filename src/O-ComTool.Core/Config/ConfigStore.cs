using System.Text.Json;
using System.Text.Json.Serialization;

namespace O_ComTool.Core.Config
{
    /**
     * @brief 读写 %AppData%/O-ComTool 下的 JSON 配置，并监视高亮文件变化
     */
    public sealed class ConfigStore : IDisposable
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.Never,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        };

        private FileSystemWatcher? _watcher;
        private Timer? _debounce;
        private Action? _highlightChanged;

        public ConfigStore()
        {
            Directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "O-ComTool");
            SettingsPath = Path.Combine(Directory, "settings.json");
            HighlightPath = Path.Combine(Directory, "highlight.json");
        }

        public string Directory { get; }

        public string SettingsPath { get; }

        public string HighlightPath { get; }

        /**
         * @brief 确保目录和默认高亮文件存在
         */
        public void EnsureDefaults()
        {
            System.IO.Directory.CreateDirectory(Directory);
            if (!File.Exists(HighlightPath))
            {
                SaveHighlight(HighlightFile.CreateDefault());
            }
        }

        /**
         * @brief 读取应用设置，文件不存在时返回默认值
         * @retval 应用设置
         */
        public AppSettings LoadSettings()
        {
            if (!File.Exists(SettingsPath))
            {
                return new AppSettings();
            }

            try
            {
                string json = File.ReadAllText(SettingsPath);
                AppSettings? settings = JsonSerializer.Deserialize<AppSettings>(json, JsonOptions);
                settings ??= new AppSettings();
                settings.ClampQuickSends();
                return settings;
            }
            catch (Exception)
            {
                return new AppSettings();
            }
        }

        /**
         * @brief 保存应用设置
         * @param settings: 应用设置
         */
        public void SaveSettings(AppSettings settings)
        {
            System.IO.Directory.CreateDirectory(Directory);
            settings.ClampQuickSends();
            File.WriteAllText(SettingsPath, JsonSerializer.Serialize(settings, JsonOptions));
        }

        /**
         * @brief 读取高亮配置，损坏时回退到默认规则
         * @retval 高亮配置
         */
        public HighlightFile LoadHighlight()
        {
            if (!File.Exists(HighlightPath))
            {
                return HighlightFile.CreateDefault();
            }

            try
            {
                string json = File.ReadAllText(HighlightPath);
                return JsonSerializer.Deserialize<HighlightFile>(json, JsonOptions) ?? HighlightFile.CreateDefault();
            }
            catch (Exception)
            {
                return HighlightFile.CreateDefault();
            }
        }

        /**
         * @brief 保存高亮配置
         * @param highlight: 高亮配置
         */
        public void SaveHighlight(HighlightFile highlight)
        {
            System.IO.Directory.CreateDirectory(Directory);
            File.WriteAllText(HighlightPath, JsonSerializer.Serialize(highlight, JsonOptions));
        }

        /**
         * @brief 高亮文件保存后回调。短时间内的连续写入只会触发一次
         * @param onChanged: 变更回调
         */
        public void WatchHighlight(Action onChanged)
        {
            _highlightChanged = onChanged;
            _watcher?.Dispose();
            _watcher = new FileSystemWatcher(Directory, "highlight.json")
            {
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.Size,
                EnableRaisingEvents = true,
            };
            FileSystemEventHandler handler = (_, _) => Debounce();
            _watcher.Changed += handler;
            _watcher.Created += handler;
            _watcher.Renamed += (_, _) => Debounce();
        }

        /**
         * @brief 释放文件监视
         */
        public void Dispose()
        {
            _watcher?.Dispose();
            _debounce?.Dispose();
        }

        private void Debounce()
        {
            _debounce?.Dispose();
            _debounce = new Timer(_ => _highlightChanged?.Invoke(), null, 250, Timeout.Infinite);
        }
    }
}
