namespace O_ComTool.Core.Config
{
    /**
     * @brief 校验工具上次使用的算法、输入模式和自定义参数
     */
    public sealed class CheckToolState
    {
        public string Algorithm { get; set; } = "CRC-16/MODBUS";

        public bool Ascii { get; set; } = true;

        public int Width { get; set; } = 16;

        public string Poly { get; set; } = "8005";

        public string Init { get; set; } = "FFFF";

        public string XorOut { get; set; } = "0000";

        public bool RefIn { get; set; } = true;

        public bool RefOut { get; set; } = true;
    }
}
