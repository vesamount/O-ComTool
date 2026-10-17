using System.Runtime.InteropServices;
using System.Text;
using O_ComTool.Core.Text;

namespace O_ComTool.Core.Config
{
    /**
     * @brief 读写旧版 O-ComTool_cfg.ini，导入后写入 JSON
     */
    public static class IniConfig
    {
        /**
         * @brief 从 ini 填充应用设置和高亮规则
         * @param path: ini 路径
         * @param settings: 要覆盖的设置
         * @param highlight: 要覆盖的高亮
         */
        public static void Import(string path, AppSettings settings, HighlightFile highlight)
        {
            settings.BaudRate = Read(path, "SerialPort", "cmbBaudRate", settings.BaudRate);
            settings.DataBitIndex = ReadInt(path, "SerialPort", "cmbDataBitIndex", settings.DataBitIndex);
            settings.ParityIndex = ReadInt(path, "SerialPort", "cmbParityBitIndex", settings.ParityIndex);
            settings.StopBitIndex = ReadInt(path, "SerialPort", "cmbStopBitIndex", settings.StopBitIndex);
            settings.FlowControlIndex = ReadInt(path, "SerialPort", "cmbFlowCtrlIndex", settings.FlowControlIndex);

            settings.ReceiveAscii = ReadBool(path, "Receive", "receEncodeAscii", settings.ReceiveAscii);
            settings.AutoLine = ReadBool(path, "Receive", "chkAutoLine", settings.AutoLine);
            settings.ShowTime = ReadBool(path, "Receive", "chkShowTime", settings.ShowTime);
            settings.AutoReplyDelay = ReadInt(path, "Receive", "AutoReplyDelay", settings.AutoReplyDelay);

            settings.SendAscii = ReadBool(path, "Send", "sendEncodeAscii", settings.SendAscii);
            settings.AppendNewLine = ReadBool(path, "Send", "chkAppendNewLine", settings.AppendNewLine);
            settings.RepeatInterval = ReadInt(path, "Send", "RepeatInterval", settings.RepeatInterval);

            settings.GeneralSendData = Read(path, "GeneralSend", "GeneralSendData", settings.GeneralSendData);
            settings.LoadFileEnable = ReadBool(path, "GeneralSend", "LoadFileEnable", settings.LoadFileEnable);
            settings.LoadFilePath = Read(path, "GeneralSend", "LoadFilePath", settings.LoadFilePath);

            int count = ReadInt(path, "QuickSend", "QuickSendCount", settings.QuickSends.Count);
            count = Math.Clamp(count, 0, AppSettings.MaxQuickSend);
            var items = new List<QuickSendEntry>();
            for (int i = 0; i < count; i++)
            {
                items.Add(new QuickSendEntry
                {
                    Name = Read(path, "QuickSend", "QuickSendTitle" + i, "Name"),
                    Data = Read(path, "QuickSend", "QuickSendData" + i, "Data"),
                });
            }

            settings.QuickSends = items;

            settings.MinToTray = ReadBool(path, "Option", "MinToTray", settings.MinToTray);
            settings.CloseToTray = ReadBool(path, "Option", "CloseToTray", settings.CloseToTray);
            settings.QuitConfirm = ReadBool(path, "Option", "QuitConfirm", settings.QuitConfirm);
            settings.HighlightEnable = ReadBool(path, "Option", "HightLightEnable", settings.HighlightEnable);
            settings.SendDisplayEnable = ReadBool(path, "Option", "SendDisplayEnable", settings.SendDisplayEnable);
            settings.CommentEnable = ReadBool(path, "Option", "CommentEnable", settings.CommentEnable);
            settings.FrameInterval = ReadInt(path, "Option", "FrameInterval", settings.FrameInterval);
            settings.TimeNewLine = ReadBool(path, "Option", "TimeNewline", settings.TimeNewLine);
            settings.SendToFileEnable = ReadBool(path, "Option", "Send2FileEnable", settings.SendToFileEnable);
            settings.SendToNewLineEnable = ReadBool(path, "Option", "Send2NewLineEnable", settings.SendToNewLineEnable);
            settings.DisplayPlan1 = ReadBool(path, "Option", "DisplayPlan1Enable", settings.DisplayPlan1);
            settings.ReceiveBackground1 = NormalizeColor(Read(path, "Option", "ReceBackColor1", settings.ReceiveBackground1));
            settings.ReceiveForeground1 = NormalizeColor(Read(path, "Option", "ReceForeColor1", settings.ReceiveForeground1));
            settings.SendBackground1 = NormalizeColor(Read(path, "Option", "SendBackColor1", settings.SendBackground1));
            settings.SendForeground1 = NormalizeColor(Read(path, "Option", "SendForeColor1", settings.SendForeground1));
            settings.ReceiveBackground2 = NormalizeColor(Read(path, "Option", "ReceBackColor2", settings.ReceiveBackground2));
            settings.ReceiveForeground2 = NormalizeColor(Read(path, "Option", "ReceForeColor2", settings.ReceiveForeground2));
            settings.SendBackground2 = NormalizeColor(Read(path, "Option", "SendBackColor2", settings.SendBackground2));
            settings.SendForeground2 = NormalizeColor(Read(path, "Option", "SendForeColor2", settings.SendForeground2));

            highlight.Enable = settings.HighlightEnable;
            ReplaceRule(highlight, "error", Read(path, "Option", "HighLightRed", DefaultHighlight.Error), "#F44747");
            ReplaceRule(highlight, "success", Read(path, "Option", "HighLightGreen", DefaultHighlight.Success), "#89D185");
            ReplaceRule(highlight, "warning", Read(path, "Option", "HighLightYellow", DefaultHighlight.Warning), "#CCA700");
            ReplaceRule(highlight, "debug", Read(path, "Option", "HighLightBlue", DefaultHighlight.Debug), "#3794FF");
            ReplaceRule(highlight, "address", Read(path, "Option", "HighLightMagenta", DefaultHighlight.Address), "#C586C0");
            ReplaceRule(highlight, "info", Read(path, "Option", "HighLightCyan", DefaultHighlight.Info), "#29B8DB");
            ReplaceRule(highlight, "time", Read(path, "Option", "HighLightOrange", DefaultHighlight.Time), "#CE9178");
            settings.ClampQuickSends();
        }

        /**
         * @brief 把当前设置导出为旧版 ini
         * @param path: 目标路径
         * @param settings: 应用设置
         * @param highlight: 高亮配置
         */
        public static void Export(string path, AppSettings settings, HighlightFile highlight)
        {
            Write(path, "SerialPort", "cmbBaudRate", settings.BaudRate);
            Write(path, "SerialPort", "cmbDataBitIndex", settings.DataBitIndex.ToString());
            Write(path, "SerialPort", "cmbParityBitIndex", settings.ParityIndex.ToString());
            Write(path, "SerialPort", "cmbStopBitIndex", settings.StopBitIndex.ToString());
            Write(path, "SerialPort", "cmbFlowCtrlIndex", settings.FlowControlIndex.ToString());

            Write(path, "Receive", "receEncodeAscii", settings.ReceiveAscii.ToString());
            Write(path, "Receive", "chkAutoLine", settings.AutoLine.ToString());
            Write(path, "Receive", "chkShowTime", settings.ShowTime.ToString());
            Write(path, "Receive", "AutoReplyDelay", settings.AutoReplyDelay.ToString());

            Write(path, "Send", "sendEncodeAscii", settings.SendAscii.ToString());
            Write(path, "Send", "chkAppendNewLine", settings.AppendNewLine.ToString());
            Write(path, "Send", "RepeatInterval", settings.RepeatInterval.ToString());

            Write(path, "GeneralSend", "GeneralSendData", settings.GeneralSendData);
            Write(path, "GeneralSend", "LoadFileEnable", settings.LoadFileEnable.ToString());
            Write(path, "GeneralSend", "LoadFilePath", settings.LoadFilePath);

            Write(path, "QuickSend", "QuickSendCount", settings.QuickSends.Count.ToString());
            for (int i = 0; i < settings.QuickSends.Count; i++)
            {
                Write(path, "QuickSend", "QuickSendTitle" + i, settings.QuickSends[i].Name);
                Write(path, "QuickSend", "QuickSendData" + i, settings.QuickSends[i].Data);
            }

            Write(path, "Option", "MinToTray", settings.MinToTray.ToString());
            Write(path, "Option", "CloseToTray", settings.CloseToTray.ToString());
            Write(path, "Option", "QuitConfirm", settings.QuitConfirm.ToString());
            Write(path, "Option", "HightLightEnable", settings.HighlightEnable.ToString());
            Write(path, "Option", "SendDisplayEnable", settings.SendDisplayEnable.ToString());
            Write(path, "Option", "CommentEnable", settings.CommentEnable.ToString());
            Write(path, "Option", "FrameInterval", settings.FrameInterval.ToString());
            Write(path, "Option", "TimeNewline", settings.TimeNewLine.ToString());
            Write(path, "Option", "Send2FileEnable", settings.SendToFileEnable.ToString());
            Write(path, "Option", "Send2NewLineEnable", settings.SendToNewLineEnable.ToString());
            Write(path, "Option", "DisplayPlan1Enable", settings.DisplayPlan1.ToString());
            Write(path, "Option", "ReceBackColor1", settings.ReceiveBackground1);
            Write(path, "Option", "ReceForeColor1", settings.ReceiveForeground1);
            Write(path, "Option", "SendBackColor1", settings.SendBackground1);
            Write(path, "Option", "SendForeColor1", settings.SendForeground1);
            Write(path, "Option", "ReceBackColor2", settings.ReceiveBackground2);
            Write(path, "Option", "ReceForeColor2", settings.ReceiveForeground2);
            Write(path, "Option", "SendBackColor2", settings.SendBackground2);
            Write(path, "Option", "SendForeColor2", settings.SendForeground2);
            Write(path, "Option", "HighLightRed", FindPattern(highlight, "error"));
            Write(path, "Option", "HighLightGreen", FindPattern(highlight, "success"));
            Write(path, "Option", "HighLightYellow", FindPattern(highlight, "warning"));
            Write(path, "Option", "HighLightBlue", FindPattern(highlight, "debug"));
            Write(path, "Option", "HighLightMagenta", FindPattern(highlight, "address"));
            Write(path, "Option", "HighLightCyan", FindPattern(highlight, "info"));
            Write(path, "Option", "HighLightOrange", FindPattern(highlight, "time"));
        }

        private static void ReplaceRule(HighlightFile highlight, string name, string pattern, string color)
        {
            HighlightRule? rule = highlight.Rules.FirstOrDefault(item => item.Name == name);
            if (rule == null)
            {
                highlight.Rules.Add(new HighlightRule
                {
                    Name = name,
                    Pattern = pattern,
                    Foreground = color,
                    FontStyle = name == "error" ? "bold" : "normal",
                });
                return;
            }

            rule.Pattern = pattern;
            if (string.IsNullOrWhiteSpace(rule.Foreground))
            {
                rule.Foreground = color;
            }
        }

        private static string FindPattern(HighlightFile highlight, string name)
        {
            return highlight.Rules.FirstOrDefault(item => item.Name == name)?.Pattern ?? string.Empty;
        }

        private static string NormalizeColor(string value)
        {
            return ColorRgba.TryParse(value, out _) ? value : value;
        }

        private static string Read(string path, string section, string key, string fallback)
        {
            var buffer = new StringBuilder(8192);
            GetPrivateProfileString(section, key, fallback, buffer, buffer.Capacity, path);
            string value = buffer.ToString();
            return string.IsNullOrEmpty(value) ? fallback : value;
        }

        private static int ReadInt(string path, string section, string key, int fallback)
        {
            return int.TryParse(Read(path, section, key, fallback.ToString()), out int value) ? value : fallback;
        }

        private static bool ReadBool(string path, string section, string key, bool fallback)
        {
            return bool.TryParse(Read(path, section, key, fallback.ToString()), out bool value) ? value : fallback;
        }

        private static void Write(string path, string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value ?? string.Empty, path);
        }

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
    }
}
