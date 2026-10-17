using System.Text;

namespace O_ComTool.Core.Text
{
    /**
     * @brief 接收区中一段绝对偏移上的颜色
     */
    public readonly record struct ColorRun(int Start, int Length, ColorRgba? Foreground, ColorRgba? Background, bool Bold);

    /**
     * @brief 追加到接收区的纯文本，以及其中的 ANSI 颜色区间
     */
    public sealed class DisplayChunk
    {
        public DisplayChunk(string text, IReadOnlyList<ColorRun> runs)
        {
            Text = text;
            Runs = runs;
        }

        public string Text { get; }

        public IReadOnlyList<ColorRun> Runs { get; }
    }

    /**
     * @brief 把收发字节格式化成接收区文本
     */
    public static class DisplayText
    {
        /**
         * @brief 格式化一帧接收数据
         * @param data: 原始字节
         * @param ascii: true 按 UTF-8 并解析 ANSI，false 按 HEX
         * @param showTime: 是否添加时间戳
         * @param timeNewLine: 时间戳后是否换行
         * @param autoLine: 帧尾是否换行
         * @retval 可追加到编辑器的文本块
         */
        public static DisplayChunk FormatReceive(byte[] data, bool ascii, bool showTime, bool timeNewLine, bool autoLine)
        {
            return Format(data, ascii, showTime, timeNewLine, autoLine, "HH:mm:ss:fff-< ");
        }

        /**
         * @brief 格式化发送回显
         * @param data: 已发送字节
         * @param ascii: true 按 UTF-8，false 按 HEX
         * @param showTime: 是否添加时间戳
         * @param timeNewLine: 时间戳后是否换行
         * @param autoLine: 帧尾是否换行
         * @retval 可追加到编辑器的文本块
         */
        public static DisplayChunk FormatSend(byte[] data, bool ascii, bool showTime, bool timeNewLine, bool autoLine)
        {
            return Format(data, ascii, showTime, timeNewLine, autoLine, "HH:mm:ss:fff-> ");
        }

        private static DisplayChunk Format(byte[] data, bool ascii, bool showTime, bool timeNewLine, bool autoLine, string timeFormat)
        {
            var runs = new List<ColorRun>();
            var builder = new StringBuilder();
            if (showTime)
            {
                string stamp = DateTime.Now.ToString(timeFormat);
                if (timeNewLine)
                {
                    stamp += "\r\n";
                }

                runs.Add(new ColorRun(0, stamp.Length, null, null, false));
                builder.Append(stamp);
            }

            if (ascii)
            {
                foreach (AnsiSpan span in AnsiParser.Parse(Encoding.UTF8.GetString(data)))
                {
                    runs.Add(new ColorRun(builder.Length, span.Text.Length, span.Foreground, span.Background, span.Bold));
                    builder.Append(span.Text);
                }
            }
            else
            {
                string hex = HexCodec.ToHex(data);
                runs.Add(new ColorRun(builder.Length, hex.Length, null, null, false));
                builder.Append(hex);
            }

            if (autoLine)
            {
                builder.Append("\r\n");
            }

            return new DisplayChunk(builder.ToString(), runs);
        }
    }
}
