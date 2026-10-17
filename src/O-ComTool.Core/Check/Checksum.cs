namespace O_ComTool.Core.Check
{
    /**
     * @brief 报文校验，算法与原 WinForms 工具保持一致
     */
    public static class Checksum
    {
        /**
         * @brief 计算字节累加和的低 8 位
         * @param data: 数据
         * @retval 校验和
         */
        public static byte Sum(byte[] data)
        {
            byte sum = 0;
            foreach (byte value in data)
            {
                sum += value;
            }

            return sum;
        }

        /**
         * @brief 计算逐字节异或
         * @param data: 数据
         * @retval 异或结果
         */
        public static byte Xor(byte[] data)
        {
            byte xor = 0;
            foreach (byte value in data)
            {
                xor ^= value;
            }

            return xor;
        }

        /**
         * @brief 计算 Modbus CRC16
         * @param data: 数据
         * @retval CRC16
         */
        public static ushort Crc16Modbus(byte[] data)
        {
            ushort crc = 0xFFFF;
            foreach (byte value in data)
            {
                crc ^= value;
                for (int bit = 0; bit < 8; bit++)
                {
                    if ((crc & 0x01) == 1)
                    {
                        crc = (ushort)((crc >> 1) ^ 0xA001);
                    }
                    else
                    {
                        crc >>= 1;
                    }
                }
            }

            return crc;
        }

        /**
         * @brief 计算 CRC32
         * @param data: 数据
         * @retval CRC32
         */
        public static uint Crc32(byte[] data)
        {
            uint value = 0xFFFFFFFF;
            foreach (byte item in data)
            {
                value = (value >> 8) ^ Table[(value ^ item) & 0xFF];
            }

            return value ^ 0xFFFFFFFF;
        }

        private static readonly uint[] Table = BuildTable();

        private static uint[] BuildTable()
        {
            var table = new uint[256];
            for (uint i = 0; i < table.Length; i++)
            {
                uint crc = i;
                for (int bit = 0; bit < 8; bit++)
                {
                    if ((crc & 1) == 1)
                    {
                        crc = (crc >> 1) ^ 0xEDB88320;
                    }
                    else
                    {
                        crc >>= 1;
                    }
                }

                table[i] = crc;
            }

            return table;
        }
    }
}
