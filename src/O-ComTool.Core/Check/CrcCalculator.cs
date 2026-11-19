namespace O_ComTool.Core.Check
{
    /**
     * @brief 通用 CRC 计算，支持 1~32 位宽和任意模型参数
     */
    public static class CrcCalculator
    {
        /**
         * @brief 按模型计算 CRC
         * @param data: 数据
         * @param model: CRC 模型
         * @retval 校验值
         */
        public static uint Compute(byte[] data, CrcModel model)
        {
            ArgumentNullException.ThrowIfNull(data);
            return Compute(data.AsSpan(), model);
        }

        /**
         * @brief 按模型计算 CRC
         * @param data: 数据
         * @param model: CRC 模型
         * @retval 校验值
         */
        public static uint Compute(ReadOnlySpan<byte> data, CrcModel model)
        {
            ArgumentNullException.ThrowIfNull(model);

            uint mask = model.Mask;
            uint topBit = 1u << (model.Width - 1);
            uint crc = model.Init;
            uint poly = model.Poly;

            // 逐位长除法：先移入一个输入位，再按最高位与多项式相除
            foreach (byte item in data)
            {
                byte value = model.RefIn ? ReverseBits(item) : item;
                for (int bit = 7; bit >= 0; bit--)
                {
                    bool feedback = ((crc & topBit) != 0) ^ (((value >> bit) & 1) != 0);
                    crc = (crc << 1) & mask;
                    if (feedback)
                    {
                        crc ^= poly;
                    }
                }
            }

            if (model.RefOut)
            {
                crc = ReverseBits(crc, model.Width);
            }

            return (crc ^ model.XorOut) & mask;
        }

        /**
         * @brief 反转一个字节的 8 个比特
         * @param value: 原值
         * @retval 反转后的值
         */
        public static byte ReverseBits(byte value)
        {
            value = (byte)(((value & 0xF0) >> 4) | ((value & 0x0F) << 4));
            value = (byte)(((value & 0xCC) >> 2) | ((value & 0x33) << 2));
            value = (byte)(((value & 0xAA) >> 1) | ((value & 0x55) << 1));
            return value;
        }

        /**
         * @brief 反转低 width 位
         * @param value: 原值
         * @param width: 位宽
         * @retval 反转后的值
         */
        public static uint ReverseBits(uint value, int width)
        {
            if (width < 1 || width > 32)
            {
                throw new ArgumentOutOfRangeException(nameof(width), "CRC 宽度必须在 1~32 之间");
            }

            uint result = 0;
            for (int bit = 0; bit < width; bit++)
            {
                result = (result << 1) | (value & 1u);
                value >>= 1;
            }

            return result;
        }
    }
}
