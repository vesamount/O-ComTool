using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace O_ComTool.Core.Text
{
    /**
     * @brief HEX 与可见文本之间的转换
     */
    public static partial class HexCodec
    {
        /**
         * @brief 从文本中抽取连续的十六进制字节
         * @param text: 含 HEX 的文本
         * @retval 字节数组
         */
        public static byte[] Parse(string? text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return Array.Empty<byte>();
            }

            MatchCollection matches = HexPair().Matches(text);
            var bytes = new byte[matches.Count];
            for (int i = 0; i < matches.Count; i++)
            {
                bytes[i] = byte.Parse(matches[i].Value, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }

            return bytes;
        }

        /**
         * @brief 把字节格式化成以空格分隔的大写 HEX
         * @param data: 字节
         * @retval HEX 文本
         */
        public static string ToHex(byte[] data)
        {
            if (data.Length == 0)
            {
                return string.Empty;
            }

            var builder = new StringBuilder(data.Length * 3);
            foreach (byte value in data)
            {
                builder.Append(value.ToString("X2", CultureInfo.InvariantCulture));
                builder.Append(' ');
            }

            return builder.ToString();
        }

        /**
         * @brief 按 UTF-8 把字节转成文本
         * @param data: 字节
         * @retval 文本
         */
        public static string ToAscii(byte[] data)
        {
            return Encoding.UTF8.GetString(data);
        }

        [GeneratedRegex("(?i)[0-9a-f]{2}")]
        private static partial Regex HexPair();
    }
}
