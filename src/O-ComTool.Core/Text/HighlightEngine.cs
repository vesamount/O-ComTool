using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace O_ComTool.Core.Text
{
    /**
     * @brief 一条高亮规则的命中区间
     */
    public readonly record struct HighlightMatch(int Index, int Length, ColorRgba Foreground, bool Bold);

    /**
     * @brief 按 JSON 规则对纯文本做正则着色。后写的规则覆盖先写的规则
     */
    public sealed class HighlightEngine
    {
        private readonly List<CompiledRule> _rules = new();

        /**
         * @brief 用当前规则重建匹配器，非法正则会被跳过
         * @param rules: 高亮规则
         * @param enabled: 是否启用
         */
        public void Load(IEnumerable<HighlightRule> rules, bool enabled)
        {
            _rules.Clear();
            if (!enabled)
            {
                return;
            }

            foreach (HighlightRule rule in rules)
            {
                if (string.IsNullOrWhiteSpace(rule.Pattern) || !ColorRgba.TryParse(rule.Foreground, out ColorRgba color))
                {
                    continue;
                }

                try
                {
                    var regex = new Regex(rule.Pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
                    _rules.Add(new CompiledRule(regex, color, string.Equals(rule.FontStyle, "bold", StringComparison.OrdinalIgnoreCase)));
                }
                catch (ArgumentException)
                {
                }
            }
        }

        /**
         * @brief 匹配一行文本
         * @param text: 纯文本
         * @retval 命中区间，索引相对于 text
         */
        public IReadOnlyList<HighlightMatch> Match(string text)
        {
            var matches = new List<HighlightMatch>();
            foreach (CompiledRule rule in _rules)
            {
                foreach (Match match in rule.Regex.Matches(text))
                {
                    if (match.Length == 0)
                    {
                        continue;
                    }

                    matches.Add(new HighlightMatch(match.Index, match.Length, rule.Color, rule.Bold));
                }
            }

            return matches;
        }

        private readonly record struct CompiledRule(Regex Regex, ColorRgba Color, bool Bold);
    }

    /**
     * @brief highlight.json 中的一条规则
     */
    public sealed class HighlightRule
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("pattern")]
        public string Pattern { get; set; } = string.Empty;

        [JsonPropertyName("foreground")]
        public string Foreground { get; set; } = "#F44747";

        [JsonPropertyName("fontStyle")]
        public string FontStyle { get; set; } = "normal";
    }
}
