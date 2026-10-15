namespace O_ComTool.Core.Config
{
    /**
     * @brief 一条快捷发送
     */
    public sealed class QuickSendEntry
    {
        public string Name { get; set; } = "Name";

        public string Data { get; set; } = "Data";
    }

    /**
     * @brief 保存在 settings.json 中的应用设置
     */
    public sealed class AppSettings
    {
        public const int MaxQuickSend = 100;

        public string PortName { get; set; } = string.Empty;

        public string BaudRate { get; set; } = "9600";

        public int DataBitIndex { get; set; } = 3;

        public int ParityIndex { get; set; }

        public int StopBitIndex { get; set; }

        public int FlowControlIndex { get; set; }

        public bool ReceiveAscii { get; set; } = true;

        public bool AutoLine { get; set; }

        public bool ShowTime { get; set; }

        public bool AutoReply { get; set; }

        public int AutoReplyDelay { get; set; } = 100;

        public bool SendAscii { get; set; } = true;

        public bool AutoCount { get; set; }

        public bool AppendNewLine { get; set; }

        public bool RepeatSend { get; set; }

        public int RepeatInterval { get; set; } = 1000;

        public string GeneralSendData { get; set; } = string.Empty;

        public bool LoadFileEnable { get; set; }

        public string LoadFilePath { get; set; } = string.Empty;

        public string LogFolder { get; set; } = string.Empty;

        public bool MinToTray { get; set; }

        public bool CloseToTray { get; set; }

        public bool QuitConfirm { get; set; } = true;

        public bool HighlightEnable { get; set; } = true;

        public bool CommentEnable { get; set; }

        public int FrameInterval { get; set; } = 20;

        public bool SendDisplayEnable { get; set; }

        public bool TimeNewLine { get; set; }

        public bool SendToFileEnable { get; set; } = true;

        public bool SendToNewLineEnable { get; set; }

        public bool DisplayPlan1 { get; set; } = true;

        public string ReceiveForeground1 { get; set; } = "#F8F8F2";

        public string ReceiveBackground1 { get; set; } = "#272822";

        public string SendForeground1 { get; set; } = "#F8F8F2";

        public string SendBackground1 { get; set; } = "#272822";

        public string ReceiveForeground2 { get; set; } = "#272822";

        public string ReceiveBackground2 { get; set; } = "#F8F8F2";

        public string SendForeground2 { get; set; } = "#272822";

        public string SendBackground2 { get; set; } = "#F8F8F2";

        public double ReceiveFontSize { get; set; } = 14;

        public double SendFontSize { get; set; } = 14;

        public string Note { get; set; } = string.Empty;

        public string LastCheckTime { get; set; } = "2000-01-01 00:00:00";

        public string SkipVersion { get; set; } = string.Empty;

        public List<QuickSendEntry> QuickSends { get; set; } = new()
        {
            new QuickSendEntry { Name = "Title1", Data = "Data1" },
            new QuickSendEntry { Name = "Title2", Data = "Data2" },
            new QuickSendEntry { Name = "Title3", Data = "Data3" },
        };

        /**
         * @brief 把快捷发送条数限制在 100 以内
         */
        public void ClampQuickSends()
        {
            if (QuickSends.Count > MaxQuickSend)
            {
                QuickSends.RemoveRange(MaxQuickSend, QuickSends.Count - MaxQuickSend);
            }
        }
    }
}
