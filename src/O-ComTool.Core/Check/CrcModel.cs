namespace O_ComTool.Core.Check
{
    /**
     * @brief 一组 CRC 模型参数，含义与 CRC RevEng / 在线校验工具一致
     */
    public sealed class CrcModel
    {
        /**
         * @brief 创建 CRC 模型
         * @param name: 模型名称
         * @param width: 校验宽度，1~32 位
         * @param poly: 生成多项式，忽略最高位
         * @param init: 寄存器初值
         * @param xorOut: 结果异或值
         * @param refIn: 输入字节是否按位反转
         * @param refOut: 结果是否按位反转
         * @param check: 对字符串 "123456789" 的标准校验值，可为空
         */
        public CrcModel(string name, int width, uint poly, uint init, uint xorOut, bool refIn, bool refOut, uint? check = null)
        {
            if (width < 1 || width > 32)
            {
                throw new ArgumentOutOfRangeException(nameof(width), "CRC 宽度必须在 1~32 之间");
            }

            uint mask = MaskOf(width);
            Name = name;
            Width = width;
            Poly = poly & mask;
            Init = init & mask;
            XorOut = xorOut & mask;
            RefIn = refIn;
            RefOut = refOut;
            Check = check;
        }

        public string Name { get; }

        public int Width { get; }

        public uint Poly { get; }

        public uint Init { get; }

        public uint XorOut { get; }

        public bool RefIn { get; }

        public bool RefOut { get; }

        /**
         * @brief 对 "123456789" 的标准校验值，自定义模型为空
         */
        public uint? Check { get; }

        public uint Mask => MaskOf(Width);

        /**
         * @brief 计算指定位宽的有效位掩码
         * @param width: 位宽
         * @retval 掩码
         */
        public static uint MaskOf(int width)
        {
            if (width < 1 || width > 32)
            {
                throw new ArgumentOutOfRangeException(nameof(width), "CRC 宽度必须在 1~32 之间");
            }

            return width >= 32 ? uint.MaxValue : (1u << width) - 1;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
