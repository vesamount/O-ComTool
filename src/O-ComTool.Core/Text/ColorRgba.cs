namespace O_ComTool.Core.Text
{
    /**
     * @brief 不依赖界面库的 RGB 颜色
     */
    public readonly record struct ColorRgba(byte R, byte G, byte B)
    {
        /**
         * @brief 解析 #RRGGBB 或 #RGB
         * @param text: 颜色文本
         * @param color: 解析结果
         * @retval 是否解析成功
         */
        public static bool TryParse(string? text, out ColorRgba color)
        {
            color = default;
            if (string.IsNullOrWhiteSpace(text))
            {
                return false;
            }

            string hex = text.Trim();
            if (hex.StartsWith('#'))
            {
                hex = hex[1..];
            }

            if (hex.Length == 3)
            {
                hex = string.Concat(hex[0], hex[0], hex[1], hex[1], hex[2], hex[2]);
            }

            if (hex.Length != 6 || !int.TryParse(hex, System.Globalization.NumberStyles.HexNumber, null, out int value))
            {
                return false;
            }

            color = new ColorRgba((byte)((value >> 16) & 0xFF), (byte)((value >> 8) & 0xFF), (byte)(value & 0xFF));
            return true;
        }

        /**
         * @brief 打包为 0xRRGGBB
         * @retval 整型颜色
         */
        public int ToRgb()
        {
            return (R << 16) | (G << 8) | B;
        }
    }
}
