using System.Text;

namespace O_ComTool.Core.Text
{
    /**
     * @brief ANSI CSI SGR 解析后的一段可见文本
     */
    public readonly record struct AnsiSpan(string Text, ColorRgba? Foreground, ColorRgba? Background, bool Bold);

    /**
     * @brief 解析终端 ANSI 颜色序列，供接收区着色和日志去色使用
     */
    public static class AnsiParser
    {
        private static readonly ColorRgba[] NormalColors =
        {
            new(0, 0, 0),
            new(205, 49, 49),
            new(13, 188, 121),
            new(229, 229, 16),
            new(36, 114, 200),
            new(188, 63, 188),
            new(17, 168, 205),
            new(229, 229, 229),
        };

        private static readonly ColorRgba[] BrightColors =
        {
            new(102, 102, 102),
            new(241, 76, 76),
            new(35, 209, 139),
            new(245, 245, 67),
            new(59, 142, 234),
            new(214, 112, 214),
            new(41, 184, 219),
            new(255, 255, 255),
        };

        /**
         * @brief 去掉 ANSI 控制序列，保留可见字符
         * @param text: 可能含 ESC 序列的文本
         * @retval 纯文本
         */
        public static string Strip(string? text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            var builder = new StringBuilder(text.Length);
            foreach (AnsiSpan span in Parse(text))
            {
                builder.Append(span.Text);
            }

            return builder.ToString();
        }

        /**
         * @brief 把 ANSI 文本拆成带颜色的片段
         * @param text: 可能含 ESC 序列的文本
         * @retval 颜色片段列表
         */
        public static IReadOnlyList<AnsiSpan> Parse(string? text)
        {
            var spans = new List<AnsiSpan>();
            if (string.IsNullOrEmpty(text))
            {
                return spans;
            }

            var current = new StringBuilder();
            ColorRgba? foreground = null;
            ColorRgba? background = null;
            bool bold = false;

            void Flush()
            {
                if (current.Length == 0)
                {
                    return;
                }

                spans.Add(new AnsiSpan(current.ToString(), foreground, background, bold));
                current.Clear();
            }

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '\u001b' && i + 1 < text.Length)
                {
                    char next = text[i + 1];
                    if (next == '[')
                    {
                        int end = i + 2;
                        while (end < text.Length && text[end] < 0x40)
                        {
                            end++;
                        }

                        if (end < text.Length)
                        {
                            if (text[end] == 'm')
                            {
                                Flush();
                                ApplySgr(text.Substring(i + 2, end - (i + 2)), ref foreground, ref background, ref bold);
                            }

                            i = end;
                            continue;
                        }
                    }
                    else if (next == ']')
                    {
                        int end = i + 2;
                        while (end < text.Length && text[end] != '\a')
                        {
                            if (text[end] == '\u001b' && end + 1 < text.Length && text[end + 1] == '\\')
                            {
                                end++;
                                break;
                            }

                            end++;
                        }

                        i = Math.Min(end, text.Length - 1);
                        continue;
                    }
                }

                current.Append(text[i]);
            }

            Flush();
            return spans;
        }

        /**
         * @brief 把 0-255 的 ANSI 调色板索引转成 RGB
         * @param index: 调色板索引
         * @retval RGB 颜色
         */
        public static ColorRgba From256(int index)
        {
            index = Math.Clamp(index, 0, 255);
            if (index < 8)
            {
                return NormalColors[index];
            }

            if (index < 16)
            {
                return BrightColors[index - 8];
            }

            if (index >= 232)
            {
                int level = 8 + (index - 232) * 10;
                return new ColorRgba((byte)level, (byte)level, (byte)level);
            }

            int cube = index - 16;
            int red = cube / 36;
            int green = (cube % 36) / 6;
            int blue = cube % 6;
            return new ColorRgba(Cube(red), Cube(green), Cube(blue));
        }

        private static void ApplySgr(string parameterText, ref ColorRgba? foreground, ref ColorRgba? background, ref bool bold)
        {
            if (parameterText.Length == 0)
            {
                foreground = null;
                background = null;
                bold = false;
                return;
            }

            string[] parts = parameterText.Split(';');
            for (int i = 0; i < parts.Length; i++)
            {
                if (!int.TryParse(parts[i], out int code))
                {
                    continue;
                }

                switch (code)
                {
                    case 0:
                        foreground = null;
                        background = null;
                        bold = false;
                        break;
                    case 1:
                        bold = true;
                        break;
                    case 22:
                        bold = false;
                        break;
                    case 39:
                        foreground = null;
                        break;
                    case 49:
                        background = null;
                        break;
                    case >= 30 and <= 37:
                        foreground = NormalColors[code - 30];
                        break;
                    case >= 40 and <= 47:
                        background = NormalColors[code - 40];
                        break;
                    case >= 90 and <= 97:
                        foreground = BrightColors[code - 90];
                        break;
                    case >= 100 and <= 107:
                        background = BrightColors[code - 100];
                        break;
                    case 38:
                    case 48:
                        if (TryReadExtended(parts, ref i, out ColorRgba color))
                        {
                            if (code == 38)
                            {
                                foreground = color;
                            }
                            else
                            {
                                background = color;
                            }
                        }

                        break;
                }
            }
        }

        private static bool TryReadExtended(string[] parts, ref int index, out ColorRgba color)
        {
            color = default;
            if (index + 1 >= parts.Length || !int.TryParse(parts[index + 1], out int mode))
            {
                return false;
            }

            if (mode == 5 && index + 2 < parts.Length && int.TryParse(parts[index + 2], out int palette))
            {
                color = From256(palette);
                index += 2;
                return true;
            }

            if (mode == 2 && index + 4 < parts.Length
                && byte.TryParse(parts[index + 2], out byte red)
                && byte.TryParse(parts[index + 3], out byte green)
                && byte.TryParse(parts[index + 4], out byte blue))
            {
                color = new ColorRgba(red, green, blue);
                index += 4;
                return true;
            }

            return false;
        }

        private static byte Cube(int component)
        {
            return component == 0 ? (byte)0 : (byte)(55 + component * 40);
        }
    }
}
