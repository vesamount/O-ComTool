namespace O_ComTool.Core.Serial
{
    /**
     * @brief 等字节数稳定后再读出一帧，避免把半包拆开
     */
    public static class FrameAssembler
    {
        /**
         * @brief 读取当前已经到齐的一帧
         * @param bytesToRead: 查询缓冲区字节数
         * @param read: 按长度读取，返回实际读到的字节
         * @param frameIntervalMs: 两次采样之间的等待
         * @param maxLength: 单帧上限
         * @retval 帧数据；缓冲区为空时返回空数组
         */
        public static byte[] ReadFrame(Func<int> bytesToRead, Func<int, byte[]> read, int frameIntervalMs, int maxLength = 1024)
        {
            int length = bytesToRead();
            if (length == 0)
            {
                return Array.Empty<byte>();
            }

            int wait = Math.Max(1, frameIntervalMs);
            int limit = Math.Max(1, maxLength);
            do
            {
                length = bytesToRead();
                Thread.Sleep(wait);
            }
            while (bytesToRead() != length && length < limit);

            if (length == 0)
            {
                return Array.Empty<byte>();
            }

            return read(Math.Min(length, limit));
        }
    }
}
