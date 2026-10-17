using System.Text;

namespace O_ComTool.Core.Text
{
    /**
     * @brief 把发送框文本编码成串口字节
     */
    public static class SendEncoder
    {
        /**
         * @brief 按注释、自动计数和编码方式生成待发送字节
         * @param text: 发送框原文
         * @param commentEnabled: 是否去掉 // 及其后的注释
         * @param autoCount: 是否在末尾追加 6 位序号
         * @param autoCountValue: 当前序号，启用自动计数时会先加一
         * @param hexMode: true 抽取 HEX，false 使用 UTF-8
         * @param appendNewLine: ASCII 模式下是否追加 CR LF
         * @retval 待写入串口的字节
         */
        public static byte[] Encode(string? text, bool commentEnabled, bool autoCount, ref int autoCountValue, bool hexMode, bool appendNewLine)
        {
            string payload = text ?? string.Empty;
            if (commentEnabled)
            {
                int comment = payload.IndexOf("//", StringComparison.Ordinal);
                if (comment >= 0)
                {
                    payload = payload[..comment];
                }
            }

            if (autoCount)
            {
                autoCountValue++;
                payload += autoCountValue.ToString().PadLeft(6, '0');
            }

            if (hexMode)
            {
                return HexCodec.Parse(payload);
            }

            if (appendNewLine)
            {
                payload += "\r\n";
            }

            return Encoding.UTF8.GetBytes(payload);
        }
    }
}
