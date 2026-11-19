namespace O_ComTool.Core.Check
{
    /**
     * @brief 内置 CRC 模型表，参数与 metools.info「CRC 校验工具」一致
     */
    public static class CrcCatalog
    {
        /**
         * @brief 所有内置模型，顺序与在线工具的下拉列表一致
         */
        public static IReadOnlyList<CrcModel> All { get; } = new CrcModel[]
        {
            new("CRC-4/ITU", 4, 0x03, 0x00, 0x00, true, true, 0x07),
            new("CRC-5/EPC", 5, 0x09, 0x09, 0x00, false, false, 0x00),
            new("CRC-5/ITU", 5, 0x15, 0x00, 0x00, true, true, 0x07),
            new("CRC-5/USB", 5, 0x05, 0x1F, 0x1F, true, true, 0x19),
            new("CRC-6/ITU", 6, 0x03, 0x00, 0x00, true, true, 0x06),
            new("CRC-7/MMC", 7, 0x09, 0x00, 0x00, false, false, 0x75),

            new("CRC-8", 8, 0x07, 0x00, 0x00, false, false, 0xF4),
            new("CRC-8/ITU", 8, 0x07, 0x00, 0x55, false, false, 0xA1),
            new("CRC-8/ROHC", 8, 0x07, 0xFF, 0x00, true, true, 0xD0),
            new("CRC-8/MAXIM", 8, 0x31, 0x00, 0x00, true, true, 0xA1),
            new("CRC-8/CDMA2000", 8, 0x9B, 0xFF, 0x00, false, false, 0xDA),
            new("CRC-8/WCDMA", 8, 0x9B, 0x00, 0x00, true, true, 0x25),

            new("CRC-16", 16, 0x8005, 0x0000, 0x0000, true, true, 0xBB3D),
            new("CRC-16/ARC", 16, 0x8005, 0x0000, 0x0000, true, true, 0xBB3D),
            new("CRC-16/MAXIM", 16, 0x8005, 0x0000, 0xFFFF, true, true, 0x44C2),
            new("CRC-16/USB", 16, 0x8005, 0xFFFF, 0xFFFF, true, true, 0xB4C8),
            new("CRC-16/MODBUS", 16, 0x8005, 0xFFFF, 0x0000, true, true, 0x4B37),
            new("CRC-16/CCITT", 16, 0x1021, 0x0000, 0x0000, true, true, 0x2189),
            new("CRC-16/CCITT-FALSE", 16, 0x1021, 0xFFFF, 0x0000, false, false, 0x29B1),
            new("CRC-16/X25", 16, 0x1021, 0xFFFF, 0xFFFF, true, true, 0x906E),
            new("CRC-16/XMODEM", 16, 0x1021, 0x0000, 0x0000, false, false, 0x31C3),
            new("CRC-16/DNP", 16, 0x3D65, 0x0000, 0xFFFF, true, true, 0xEA82),

            new("CRC-32", 32, 0x04C11DB7, 0xFFFFFFFF, 0xFFFFFFFF, true, true, 0xCBF43926),
            new("CRC-32/MPEG-2", 32, 0x04C11DB7, 0xFFFFFFFF, 0x00000000, false, false, 0x0376E6E7),
            new("CRC-32/BZIP2", 32, 0x04C11DB7, 0xFFFFFFFF, 0xFFFFFFFF, false, false, 0xFC891918),
            new("CRC-32/POSIX", 32, 0x04C11DB7, 0x00000000, 0xFFFFFFFF, false, false, 0x765E7680),
            new("CRC-32/JAMCRC", 32, 0x04C11DB7, 0xFFFFFFFF, 0x00000000, true, true, 0x340BC6D9),
        };
    }
}
