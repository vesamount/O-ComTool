using System.Text.Json.Serialization;
using O_ComTool.Core.Text;

namespace O_ComTool.Core.Config
{
    /**
     * @brief VS Code 风格的高亮配置文件
     */
    public sealed class HighlightFile
    {
        [JsonPropertyName("highlight.enable")]
        public bool Enable { get; set; } = true;

        [JsonPropertyName("highlight.rules")]
        public List<HighlightRule> Rules { get; set; } = new();

        /**
         * @brief 生成与旧版七色正则等价的默认规则
         * @retval 默认高亮文件
         */
        public static HighlightFile CreateDefault()
        {
            return new HighlightFile
            {
                Enable = true,
                Rules = new List<HighlightRule>
                {
                    Rule("error", DefaultHighlight.Error, "#F44747", "bold"),
                    Rule("success", DefaultHighlight.Success, "#89D185", "normal"),
                    Rule("warning", DefaultHighlight.Warning, "#CCA700", "normal"),
                    Rule("debug", DefaultHighlight.Debug, "#3794FF", "normal"),
                    Rule("address", DefaultHighlight.Address, "#C586C0", "normal"),
                    Rule("info", DefaultHighlight.Info, "#29B8DB", "normal"),
                    Rule("time", DefaultHighlight.Time, "#CE9178", "normal"),
                },
            };
        }

        private static HighlightRule Rule(string name, string pattern, string foreground, string fontStyle)
        {
            return new HighlightRule
            {
                Name = name,
                Pattern = pattern,
                Foreground = foreground,
                FontStyle = fontStyle,
            };
        }
    }

    /**
     * @brief 从原 app.settings 迁出的默认高亮正则
     */
    public static class DefaultHighlight
    {
        public const string Error = "([^A-Za-z_&-]((bad|wrong|incorrect|improper|invalid|unsupported|bad)( file| memory)? (descriptor|alloc(ation)?|addr(ess)?|owner(ship)?|arg(ument)?|param(eter)?|setting|length|filename)|not properly|improperly|(operation |connection |authentication |access |permission )?(denied|disallowed|not allowed|refused|problem|failed|failure|not permitted)|no [A-Za-z]+( [A-Za-z]+)? found|invalid|unsupported|not supported|seg(mentation )?fault|corruption|corrupted|corrupt|overflow|underrun|not ok|unimplemented|unsuccessfull|not implemented|permerrors?|fehlers?|errore|errors?|erreurs?|fejl|virhe|grea|erro|fel|\\(ee\\)|\\(ni\\))[^A-Za-z_-])";

        public const string Success = "([^A-Za-z_&-](ok|accepted|allowed|enabled|connected|erfolgreich|exitoso|successo|sucedido|successfully|successful|succeeded|success)[^A-Za-z_-])";

        public const string Warning = "[^A-Za-z_&-](\\[\\-w[A-Za-z-]+\\]|caught signal [0-9]+|cannot|(connection (to (remote host|[a-z0-9.]+) )?)?(closed|terminated|stopped|not responding)|exited|no more [A-Za-z] available|unexpected|(command |binary |file )?not found|(o)+ps|out of (space|memory)|low (memory|disk)|unknown|disabled|disconnected|deprecated|refused|disconnect(ion)?|advertencia|avvertimento|attention|warnings?|achtung|exclamation|alerts?|warnungs?|advarsel|pedwarn|aviso|varoitus|upozorenje|peringatan|uyari|varning|avertissement|\\(ww\\)|\\(\\?\\?\\)|could not|unable to)[^A-Za-z_-]";

        public const string Debug = "[^A-Za-z_&-](debug|test)[^A-Za-z_-]";

        public const string Address = "[^0-9A-Za-z_&-](localhost|([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-4])\\.[0-9]+\\.[0-9]+\\.[0-9]+|null|none)[^0-9A-Za-z_-]";

        public const string Info = "[^A-Za-z_&-](last (failed )?login:|launching|checking|loading|creating|building|important|booting|starting|notice|informational|informationen|informazioni|informaction|oplysninger|informations?|info|informaction|informasi|note|\\(ii\\)|\\(\\!\\!\\))[^A-Za-z_-]";

        public const string Time = "([0-1][0-9]|2[0-3]):([0-5][0-9]):([0-5][0-9]):([0-9][0-9][0-9]-(<|>))";
    }
}
