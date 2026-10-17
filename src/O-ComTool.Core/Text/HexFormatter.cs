namespace O_ComTool.Core.Text
{
    /**
     * @brief 按勾选的分隔符重排 HEX 文本
     */
    public static class HexFormatter
    {
        /**
         * @brief 抽取 HEX 字节后按选项重新拼接
         * @param text: 源文本
         * @param prefix0x: 是否加 0x 前缀
         * @param comma: 是否加逗号
         * @param point: 是否加点
         * @param suffixH: 是否加 H 后缀
         * @param space: 是否加空格
         * @retval 格式化结果
         */
        public static string Format(string? text, bool prefix0x, bool comma, bool point, bool suffixH, bool space)
        {
            byte[] bytes = HexCodec.Parse(text);
            var builder = new System.Text.StringBuilder();
            foreach (byte value in bytes)
            {
                if (prefix0x)
                {
                    builder.Append("0x");
                }

                builder.Append(value.ToString("X2"));
                if (comma)
                {
                    builder.Append(',');
                }

                if (point)
                {
                    builder.Append('.');
                }

                if (suffixH)
                {
                    builder.Append('H');
                }

                if (space)
                {
                    builder.Append(' ');
                }
            }

            return builder.ToString();
        }
    }
}
