using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;
using System.IO.Ports;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
using FastColoredTextBoxNS;

namespace O_ComTool_Pro
{
    public partial class MainForm : Form
    {
        string server_url = "http://www.ifreehub.com/octservice"; // 服务器地址，用于检查更新
        public static UpdateHelper.check_value check_version_value;

        bool FrameOrByte = true;
        int spTxCount = 0, spRxCount = 0;   //串口接收发送字节数
        int spFrameTxCount = 0, spFrameRxCount = 0;   //串口接收发送帧数
        int AutoCountNum = 0;// 自动计数值
        
        

        FileStream log_fs;
        string log_save_path = "";// 日志文件保存路径
        string load_file_path = "";// 加载文件路径
        List<QuickSend> quicksend_list = new List<QuickSend>();

        // 显示颜色

        Color cur_color;// 当前文字前景色

        public Font ReceFont1, SendFont1;
        public Color ReceForeColor1, ReceBackColor1, SendForeColor1, SendBackColor1;

        public Font ReceFont2, SendFont2;
        public Color ReceForeColor2, ReceBackColor2, SendForeColor2, SendBackColor2;

        bool display_plan1_enable = !app.Default.DisplayPlan1Enable;

        //
        public int frame_interval;
        public bool comment_enable;

        //highlight
        public bool hight_light_enable;
        public string hl_red_regex_str;
        public string hl_green_regex_str;
        public string hl_yellow_regex_str;
        public string hl_blue_regex_str;
        public string hl_magenta_regex_str;
        public string hl_cyan_regex_str;
        public string hl_orange_regex_str;

        //发送回显
        public bool send_display_enable;

        //显示时间追加新行
        public bool time_newline_enable;

        //发送回显写入文件使能
        public bool send_2_file_enable;

        //发送时接受窗口自动跳转至新行
        public bool send_2_newline_enable;


        public MainForm()
        {
            
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }
        

        /// <summary>
        /// 启动检查软件版本
        /// </summary>
        void StartCheckVersion()
        {
            short check_cycle_days = 1; // 版本检查周期，暂定每天进行一次检查
            DateTime CurrentDateTime = DateTime.Now;
            DateTime LastDateTime = Convert.ToDateTime(app.Default.LastCheckTime);

            DateTime LastDateTime_Cycle = LastDateTime;// LastDateTime.AddDays(check_cycle_days);

            if (CurrentDateTime.CompareTo(LastDateTime_Cycle) >= 0)
            {

                Task check_version = new Task(() =>
                {
                    UpdateHelper.check_value ret_update = UpdateHelper.check_update(server_url);
                    if ((ret_update.valid == true) && (app.Default.SkipVersion != ret_update.version) && (ret_update.version != Application.ProductVersion.Substring(0, 5)))
                    {
                        check_version_value.version = ret_update.version;
                        check_version_value.link = ret_update.link;
                        app.Default.LastCheckTime = DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
                        app.Default.Save();
                        CheckUpdate check_update = new CheckUpdate();
                        check_update.ShowDialog();
                    }
                    else
                    {
                        return;
                    }

                });
                check_version.Start();
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// 显示右下角状态
        /// </summary>
        /// <param name="ok"></param>
        /// <param name="message"></param>
        void ShowCurStatus(bool ok, string message)
        {
            tssCurstatus.Text = message;
            if (ok) 
            {
                tssCurstatus.ForeColor = Color.Green;
            }
            else 
            {
                tssCurstatus.ForeColor = Color.Red;
            }
        }

        /// <summary>
        /// 加载文件
        /// </summary>
        /// <param name="file_path"></param>
        private void load_file(string file_path)
        {
            StreamReader file = new StreamReader(file_path, Encoding.Default);
            string tmp_str = "";

            cmbLoadFile.Items.Clear();
            while (tmp_str != null)
            {
                tmp_str = file.ReadLine();
                if (!string.IsNullOrEmpty(tmp_str))
                    cmbLoadFile.Items.Add(tmp_str);
            }
            if (cmbLoadFile.Items.Count > 0)
                cmbLoadFile.SelectedIndex = 0;
            file.Close();
        }

        void refresh_quick_send_ui()
        {
            int tmp_position_y = -tbpQuick.VerticalScroll.Value - 24;
            
            for (short i = 0; i < quicksend_list.Count; i++)
            {
                //MessageBox.Show("tbpQuick.VerticalScroll.Value = " + tbpQuick.VerticalScroll.Value +
                //            "\ntmp_position_y=" + tmp_position_y);
                quicksend_list[i].Index = i; // 刷新索引值
                quicksend_list[i].Location = new Point(6, tmp_position_y + 30);
                tmp_position_y = quicksend_list[i].Location.Y;
                //add_quick_send(i, quicksend_list[i].Name, quicksend_list[i].Data);
            }
        }

        /// <summary>
        /// 快捷发送删除回调函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void quicksend_del_Click(object sender, EventArgs e)
        {
            PictureBox tmp_send_btn = (PictureBox)sender;
            O_ComTool_Pro.QuickSend tmp_quicksend = (O_ComTool_Pro.QuickSend)tmp_send_btn.Parent;
            quicksend_list.Remove(tmp_quicksend);
            tbpQuick.Controls.Remove(tmp_quicksend);
            refresh_quick_send_ui();
            //MessageBox.Show(tmp_quicksend.Data);
        }

        /// <summary>
        /// 快速发送发送回调函数，不支持重复发送、自动回复等功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void quicksend_send_Click(object sender, EventArgs e)
        {
            Button tmp_send_btn = (Button)sender;
            O_ComTool_Pro.QuickSend tmp_quicksend = (O_ComTool_Pro.QuickSend)tmp_send_btn.Parent;

            // 检查串口是否打开
            if (serialPort1.IsOpen == false)
            {
                chkRepeatSend.Checked = false;
                chkAutoReply.Checked = false;
                MessageBox.Show("串口未打开！", "O-ComTool 错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 检查发送区是否为空
            if (tmp_quicksend.Data == "")
            {
                MessageBox.Show("发送区不能为空！", "O-ComTool 错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            serial_send(tmp_quicksend.Data);
            
            //MessageBox.Show(tmp_quicksend.Data);
        }

        /// <summary>
        /// 添加快速发送控件
        /// </summary>
        /// <param name="index"></param>
        /// <param name="title"></param>
        /// <param name="data"></param>
        void add_quick_send(short index, string title, string data)
        {
            int last_position_y = 0;
            if (quicksend_list.Count > 0)
            {
                QuickSend tmp_quicksend = new QuickSend();
                tmp_quicksend = quicksend_list[quicksend_list.Count - 1];
                last_position_y = tmp_quicksend.Location.Y;
            }
            else {
                last_position_y = -24;
            }

            QuickSend quicksend = new QuickSend();
            quicksend.Name = "quicksend" + index;
            quicksend.Index = index;
            quicksend.ItemName = title;
            quicksend.Data = data;
            quicksend.Width = tbpQuick.Width - 65;
            //quicksend.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            quicksend.Location = new Point(6, last_position_y + 30);
            if (index > 2) {
                quicksend.DelVisible = true;
                quicksend.DelClicked += new O_ComTool_Pro.QuickSend.BtnClickHandle(quicksend_del_Click);
            }
            quicksend.SendClicked += new O_ComTool_Pro.QuickSend.BtnClickHandle(quicksend_send_Click);

            tbpQuick.Controls.Add(quicksend);
            quicksend_list.Add(quicksend);
            
        }

        public void UpdateHightLight()
        {
            //rtbReceive.Settings.Keywords.Clear();
            //rtbReceive.Settings.KeywordColor.Clear();
            //for (int i = 0; i < hight_light_count; i++)
            //{
            //    rtbReceive.Settings.Keywords.Add(hight_light_key[i]);
            //    rtbReceive.Settings.KeywordColor.Add(System.Drawing.ColorTranslator.FromHtml(hight_light_color[i]));
            //}
            //rtbReceive.CompileKeywords();
            CompileKeywords();
        }

        public short hight_light_count = 0;
        public List<string> hight_light_key = new List<string>();
        public List<string> hight_light_color = new List<string>();

        /// <summary>
        /// 加载上次关闭时的配置
        /// </summary>
        void LoadLastConfig()
        {
            bool tmp;
            // 串口设置
            cmbBaudRate.Text = app.Default.cmbBaudRate;
            cmbDataBit.SelectedIndex = app.Default.cmbDataBitIndex;
            cmbStopBit.SelectedIndex = app.Default.cmbStopBitIndex;
            cmbParityBit.SelectedIndex = app.Default.cmbParityBitIndex;
            cmbFlowCtrl.SelectedIndex = app.Default.cmbFlowCtrlIndex;

            // 接收设置
            tmp = app.Default.receEncodeAscii == true ? radAsciiReceive.Checked = true : radAsciiReceive.Checked = false;
            tmp = app.Default.receEncodeAscii == false ? radHexReceive.Checked = true : radHexReceive.Checked = false;
            tmp = app.Default.chkAutoLine == true ? chkAutoLine.Checked = true : chkAutoLine.Checked = false;
            tmp = app.Default.chkShowTime == true ? chkShowTime.Checked = true : chkShowTime.Checked = false;
            tmp = app.Default.chkAutoReply == true ? chkAutoReply.Checked = true : chkAutoReply.Checked = false;
            nudReplyDelay.Value = app.Default.AutoReplyDelay;

            // 发送设置
            tmp = app.Default.sendEncodeAscii == true ? radAsciiSend.Checked = true : radAsciiSend.Checked = false;
            tmp = app.Default.sendEncodeAscii == false ? radHexSend.Checked = true : radHexSend.Checked = false;
            tmp = app.Default.chkAutoCount == true ? chkAutoCount.Checked = true : chkAutoCount.Checked = false;
            tmp = app.Default.chkAppendNewLine == true ? chkNewLine.Checked = true : chkNewLine.Checked = false;
            tmp = app.Default.chkRepeatSend == true ? chkRepeatSend.Checked = true : chkRepeatSend.Checked = false;
            nudRepeatInterval.Value = app.Default.RepeatInterval;

            // 接收
                // 方案1
            ReceFont1 = app.Default.ReceFont1;
            ReceForeColor1 = app.Default.ReceForeColor1;
            ReceBackColor1 = app.Default.ReceBackColor1;
                // 方案2
            ReceFont2 = app.Default.ReceFont2;
            ReceForeColor2 = app.Default.ReceForeColor2;
            ReceBackColor2 = app.Default.ReceBackColor2;

            // 通用发送
                // 方案1
            SendFont1 =app.Default.SendFont1;
            SendForeColor1 = app.Default.SendForeColor1;
            SendBackColor1 = app.Default.SendBackColor1;
                // 方案2
            SendFont2 = app.Default.SendFont2;
            SendForeColor2 = app.Default.SendForeColor2;
            SendBackColor2 = app.Default.SendBackColor2;

                // 是否为方案1
            if (app.Default.DisplayPlan1Enable == true)
            {
                //rtbReceive.Font = ReceFont1;
                //cur_color = rtbReceive.ForeColor = ReceForeColor1;
                //rtbReceive.BackColor = ReceBackColor1;

                fctbReceive.Font = ReceFont1;
                cur_color = fctbReceive.ForeColor = ReceForeColor1;
                fctbReceive.BackColor = ReceBackColor1;

                txbSend.Font = SendFont1;
                txbSend.ForeColor = SendForeColor1;
                txbSend.BackColor = SendBackColor1;
            }
            else 
            {
                //rtbReceive.Font = ReceFont2;
                //cur_color = rtbReceive.ForeColor = ReceForeColor2;
                //rtbReceive.BackColor = ReceBackColor2;

                fctbReceive.Font = ReceFont2;
                cur_color = fctbReceive.ForeColor = ReceForeColor2;
                fctbReceive.BackColor = ReceBackColor2;

                txbSend.Font = SendFont2;
                txbSend.ForeColor = SendForeColor2;
                txbSend.BackColor = SendBackColor2;
            }

            // 选项
                // 基本
            frame_interval = app.Default.FrameInterval;
            comment_enable = app.Default.CommentEnable;
            hight_light_enable = app.Default.HightLightEnable;
            send_display_enable = app.Default.SendDisplayEnable;
            time_newline_enable = app.Default.chkTimeNewLine;
            send_2_file_enable = app.Default.Send2FileEnable;
            send_2_newline_enable = app.Default.Send2NewLineEnable;

            if (app.Default.LoadFileEnable == true ) 
            {
                if (File.Exists(app.Default.LoadFilePath) == true)
                {
                    load_file(app.Default.LoadFilePath);
                    load_file_path = app.Default.LoadFilePath;
                    ShowCurStatus(true, "文件加载成功");
                }
                else 
                {
                    ShowCurStatus(false, "文件加载失败");
                }
            }
            // 高亮
            //hight_light_count = app.Default.HigthLightKeyCount;

            //hight_light_key.Clear();
            //hight_light_color.Clear();
            //for (int i = 0; i < hight_light_count; i++)
            //{
            //    hight_light_key.Add(app.Default.HightLightName[i]);
            //    hight_light_color.Add(app.Default.HightLightColor[i]);
            //}

            //UpdateHightLight();

            //highlight
            hl_red_regex_str = app.Default.HighLightRed;
            hl_green_regex_str = app.Default.HighLightGreen;
            hl_yellow_regex_str = app.Default.HighLightYellow;
            hl_blue_regex_str = app.Default.HighLightBlue;
            hl_magenta_regex_str = app.Default.HighLightMagenta;
            hl_cyan_regex_str = app.Default.HighLightCyan;
            hl_orange_regex_str = app.Default.HighLightOrange;

            // 通用发送
            txbSend.Text = app.Default.GeneralSendData;

            //快捷发送
            tbpQuick.Controls.Clear();
            quicksend_list.Clear();
            for (short i = 0; i < app.Default.QuickSendCount; i++)
            {
                add_quick_send(i, app.Default.QuickSendTitle[i], app.Default.QuickSendData[i]);
            }
        }

        private void tsmCheck_Click(object sender, EventArgs e)
        {
            Check check = new Check();
            check.Show();
        }

        private void tsmAscii_Click(object sender, EventArgs e)
        {
            ASCII ascii = new ASCII();
            ascii.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Text = "O-ComTool V" + Application.ProductVersion.Substring(0, 5);
            StartCheckVersion();
            LoadLastConfig();
        }

        private void quickSend3_Load(object sender, EventArgs e)
        {
            //string strColor = Color.Red.Name.ToString();

            //Color slateBlue = Color.FromName(strColor);
        }

        private void tsmAbout_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        private void tsmUpdate_Click(object sender, EventArgs e)
        {
            Update update = new Update();
            update.ShowDialog();
        }

        private void tsmFormat_Click(object sender, EventArgs e)
        {
            Format format = new Format();
            format.Show();
        }

        private void tsmOption_Click(object sender, EventArgs e)
        {
            Option option = new Option(this);
            option.ShowDialog();

            if (display_plan1_enable == false)
            {
                fctbReceive.Font = ReceFont1;
                cur_color = fctbReceive.ForeColor = ReceForeColor1;
                fctbReceive.BackColor = ReceBackColor1;

                txbSend.Font = SendFont1;
                txbSend.ForeColor = SendForeColor1;
                txbSend.BackColor = SendBackColor1;
            }
            else
            {
                fctbReceive.Font = ReceFont2;
                cur_color = fctbReceive.ForeColor = ReceForeColor2;
                fctbReceive.BackColor = ReceBackColor2;

                txbSend.Font = SendFont2;
                txbSend.ForeColor = SendForeColor2;
                txbSend.BackColor = SendBackColor2;
            }
            
        }

        private void tsmDonate_Click(object sender, EventArgs e)
        {
            Donate donate = new Donate();
            donate.ShowDialog();
        }

        private void tsmAddNote_Click(object sender, EventArgs e)
        {
            Note note = new Note(this);
            note.ShowDialog();
        }

        private void quickSend1_SendClicked(object sender, EventArgs e)
        {

        }

        void tsProgressBar_start()
        {
            tssProgressBar.Value = 0;
            tssProgressBar.Maximum = 100;
            timerProcessBar.Start();
        }

        void tsProgressBar_stop()
        {
            tssProgressBar.Value = 100;
            timerProcessBar.Stop();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="len"></param>
        void SendDisplay(byte[] data, int len)
        {
            string display_str = "";
            if (chkShowTime.Checked == true)
            {
                string data_str = DateTime.Now.ToString("yyyy-MM-dd ");//写入文件时添加年月
                WriteLog(data_str, log_fs);
                string TimeStamp = DateTime.Now.ToString("HH:mm:ss:fff-> ");
                if (time_newline_enable == true) TimeStamp += "\r\n";

                //fctbReceive.AppendText(TimeStamp);
                display_str += TimeStamp;
            }
            if (radHexReceive.Checked == true)
            {
                display_str += GetHexString(data, 0, len).ToString();
                //fctbReceive.AppendText(GetHexString(data, 0, len).ToString());
            }
            else if (radAsciiReceive.Checked == true)
            {
                string tmp_str = Encoding.UTF8.GetString(data, 0, len);
                display_str += tmp_str;
                //fctbReceive.AppendText(tmp_str);
            }
            if (chkAutoLine.Checked == true)
            {
                display_str += "\r\n";
                //fctbReceive.AppendText("\r\n");
            }
            fctbReceive.AppendText(display_str);
            if ((send_2_newline_enable == true) || (o_ScrollBar1.Maximum - (o_ScrollBar1.Value + o_ScrollBar1.ThumbSize)) < 20) fctbReceive.GoEnd(); // 发送自动跳转新行使能或滚动条位于接收框底部

            if (send_2_file_enable == true)
            {
                WriteLog(display_str, log_fs);
            }
        }

        void serial_send(string txb_data)
        {
            string TempStr;
            int index;
            index = txb_data.IndexOf("//");
            tsProgressBar_start();

            
            if (comment_enable == true && index != -1)
            {
                TempStr = txb_data.Substring(0, index);//获得“//”之前的内容，实现注释功能
            }
            else
            {
                TempStr = txb_data;
            }


            if (chkAutoCount.Checked == true)
            {
                TempStr = TempStr + (++AutoCountNum).ToString().PadLeft(6, '0');
            }

            if (radHexSend.Checked)//十六进制发送
            {
                int i = 0;
                MatchCollection mc = Regex.Matches(TempStr, @"(?i)[\da-f]{2}");//正则获取符合十六进制规则的数据，如果数据错误则跳过该数据
                byte[] bytesToWrite = new byte[mc.Count];//定义新bytes[]长度为正则匹配出的长度

                foreach (Match m in mc)//遍历所有mc，并将其转换成十六进制
                {
                    bytesToWrite[i++] = byte.Parse(m.Value, System.Globalization.NumberStyles.HexNumber);//赋值并累加
                }
                serialPort1.Write(bytesToWrite, 0, mc.Count);

                if (send_display_enable == true && chkAutoLine.Checked == true)
                {
                    SendDisplay(bytesToWrite, mc.Count);
                }

                ShowCurStatus(true, mc.Count + "字节已发送");
                spTxCount += mc.Count;
                spFrameTxCount += 1;
                if (FrameOrByte == true)
                {
                    tssTxCount.Text = "TX: " + spFrameTxCount + " frames";
                    tssLabRateValue.Text = Math.Round((spFrameRxCount * 1.0) * 100 / (spFrameTxCount), 2) + "%";
                }
                else
                {
                    tssTxCount.Text = "TX: " + spTxCount + " bytes";
                    tssLabRateValue.Text = Math.Round((spRxCount * 1.0) * 100 / (spTxCount), 2) + "%";
                }
            }
            else//ascii码发送
            {
                if (chkNewLine.Checked == true) TempStr += "\r\n";
                byte[] bytesToWrite = Encoding.UTF8.GetBytes(TempStr);//
                serialPort1.Write(bytesToWrite, 0, bytesToWrite.Length);

                if (send_display_enable == true && chkAutoLine.Checked == true)
                {
                    SendDisplay(bytesToWrite, bytesToWrite.Length);
                }

                ShowCurStatus(true, bytesToWrite.Length + "字节已发送");
                spTxCount += bytesToWrite.Length;
                spFrameTxCount += 1;
                if (FrameOrByte == true)
                {
                    tssTxCount.Text = "TX: " + spFrameTxCount + " frames";
                    tssLabRateValue.Text = Math.Round((spFrameRxCount * 1.0) * 100 / (spFrameTxCount), 2) + "%";
                }
                else
                {
                    tssTxCount.Text = "TX: " + spTxCount + " bytes";
                    tssLabRateValue.Text = Math.Round((spRxCount * 1.0) * 100 / (spTxCount), 2) + "%";
                }
            }

            

            tsProgressBar_stop();
        }


        private void btnSend_Click(object sender, EventArgs e)
        {
            // 检查串口是否打开
            if (serialPort1.IsOpen == false)
            {
                chkRepeatSend.Checked = false;
                MessageBox.Show("串口未打开！", "O-ComTool 错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 检查发送区是否为空
            if (txbSend.Text == "")
            {
                timerAutoReply.Stop();
                chkAutoReply.Checked = false;
                timerRepeat.Stop();
                chkRepeatSend.Checked = false;
                MessageBox.Show("发送区不能为空！", "O-ComTool 错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSend.Enabled = true;
                return;
            }

            // 发送数据时屏蔽发送按钮
            btnSend.Enabled = false;
            serial_send(txbSend.Text);

            // 检查重复发送是否可用
            if (chkRepeatSend.Checked)
            {
                nudRepeatInterval.Enabled = false;
                timerRepeat.Interval = (int)nudRepeatInterval.Value;
                timerRepeat.Start();
                tsProgressBar_stop();
                return;
            }
            
            btnSend.Enabled = true;
        }

        private void btnOpenCom_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == false)
            {
                try
                {
                    serialPort1.PortName = cmbCom.Text;
                    serialPort1.BaudRate = int.Parse(cmbBaudRate.Text);
                    serialPort1.DataBits = int.Parse(cmbDataBit.Text);
                    serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), cmbParityBit.Text);
                    serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), (cmbStopBit.SelectedIndex + 1).ToString());
                    serialPort1.Handshake = (Handshake)Enum.Parse(typeof(Handshake), cmbFlowCtrl.SelectedIndex.ToString());
                    serialPort1.ReceivedBytesThreshold = 1;//接收到一个字节就触发事件
                    //serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);// New一个接收事件SP_DataReceived
                    serialPort1.Open();
                    btnOpenCom.Text = "关闭串口";

                    tssLabCom.ForeColor = Color.Green;
                    tssLabCom.Text = cmbCom.Text + ": " + cmbBaudRate.Text + ", " + cmbDataBit.Text + ", " + cmbParityBit.Text + ", " + cmbStopBit.Text;
                    //ShowCurStatus(true, cmbCom.Text + ": " + cmbBaudRate.Text + ", " + cmbDataBit.Text + ", " + cmbParityBit.Text + ", " + cmbStopBit.Text);
                    picConnectStatus.Image = Properties.Resources.Connected_48px;
                    cmbCom.Enabled = false;
                    cmbBaudRate.Enabled = false;
                    cmbDataBit.Enabled = false;
                    cmbParityBit.Enabled = false;
                    cmbStopBit.Enabled = false;
                    cmbFlowCtrl.Enabled = false;
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("错误:" + ex.Message, "O-ComTool 错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnOpenCom.Text = "打开串口";
                    picConnectStatus.Image = Properties.Resources.Disconnected_48px;
                    cmbCom.Enabled = true;
                    cmbBaudRate.Enabled = true;
                    cmbDataBit.Enabled = true;
                    cmbParityBit.Enabled = true;
                    cmbStopBit.Enabled = true;
                    cmbFlowCtrl.Enabled = true;
                    serialPort1.Close();
                    return;
                }
            }
            else
            {
                try
                {
                    btnOpenCom.Text = "打开串口";
                    picConnectStatus.Image = Properties.Resources.Disconnected_48px;
                    tssLabCom.ForeColor = Color.Red;
                    tssLabCom.Text = "COMx: Closed";
                    cmbCom.Enabled = true;
                    cmbBaudRate.Enabled = true;
                    cmbDataBit.Enabled = true;
                    cmbParityBit.Enabled = true;
                    cmbStopBit.Enabled = true;
                    cmbFlowCtrl.Enabled = true;
                    serialPort1.Close();
                    
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("错误:" + ex.Message, "O-ComTool 错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private static StringBuilder GetHexString(byte[] data, int offset, int length)
        {
            StringBuilder sb = new StringBuilder(length * 3);
            for (int i = offset; i < (offset + length); i++)
            {
                sb.Append(data[i].ToString("X2") + " ");
            }
            return sb;
        }

        void WriteLog(string str, FileStream fs)
        {
            try
            {
                if (chkAutoSave.Checked)
                {
                    StreamWriter fsw = new StreamWriter(fs);
                    fsw.Write(str);
                    fsw.Flush();
                }
                else
                {
                    return;
                }
            }
            catch
            {
                ;
            }
        }

        private string m_strKeywords = "";//正则表达式
        List<Color> m_color = new List<Color>();
        public void CompileKeywords()
        {
            m_strKeywords = "";
            m_color.Clear();
            for (int i = 0; i < hight_light_count; i++)
            {
                string strKeyword = hight_light_key[i];

                if (i == hight_light_count - 1)
                    m_strKeywords += strKeyword;
                else
                    m_strKeywords += strKeyword + "|";
            }

            for (int i = 0; i < hight_light_color.Count; i++)
            {
                m_color.Add(System.Drawing.ColorTranslator.FromHtml(hight_light_color[i]));
            }
        }

        static int last_rtb_len = 0;
        void KeyWordHightLight(RichTextBox rtb, string str_line)
        {
            int m_nLineStart = 0;
            int m_nLineLength = 0;

            str_line = str_line.Replace("\r\n", "\n");
            if (hight_light_enable == false) return;

            // Process this line.
            // Save the position and make the whole line black
            int nPosition = rtb.SelectionStart;

            //m_nLineStart = rtb.Text.Length - str_line.Length;
            m_nLineStart = last_rtb_len;
            if (m_nLineStart < 0) m_nLineStart = 0;

            m_nLineLength = str_line.Length;

            rtb.SelectionStart = m_nLineStart;
            rtb.SelectionLength = m_nLineLength;
            rtb.SelectionColor = cur_color;

            // Process the keywords
            Regex regKeywords = new Regex(m_strKeywords, RegexOptions.Compiled);
            Match regMatch;
            for (regMatch = regKeywords.Match(str_line); regMatch.Success; regMatch = regMatch.NextMatch())
            {
                // Process the words
                int nStart = m_nLineStart + regMatch.Index;
                int nLenght = regMatch.Length;
                rtb.SelectionStart = nStart;
                rtb.SelectionLength = nLenght;
                for (int i = 0; i < m_color.Count; i++)
                {
                    if (regMatch.Value == hight_light_key[i])
                    {
                        rtb.SelectionColor = m_color[i];
                    }
                }
            }
            rtb.SelectionStart = nPosition;
            rtb.SelectionLength = 0;
            rtb.SelectionColor = cur_color;
            //last_rtb_len = rtb.Text.Length;
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                //延时不严谨，解决办法：短时延时，然后判断字长是否变化，变化则等待，不变则退出。
                //最慢的1200bit/s，1字节11bit，最大耗时1/1200*11*1e3=9.17ms
                int RecLen = 0;

                do
                {
                    RecLen = serialPort1.BytesToRead;
                    Thread.Sleep(frame_interval);
                } while (serialPort1.BytesToRead != RecLen && RecLen < 1024);
                if (RecLen == 0) return;

                byte[] RecBuf = new byte[RecLen];//声明一个临时数组存储当前来的串口数据  
                serialPort1.Read(RecBuf, 0, RecLen);//读取缓冲数据 
                spRxCount += RecLen;
                spFrameRxCount += 1;

                if (FrameOrByte == true)
                {
                    tssRxCount.Text = "RX: " + spFrameRxCount.ToString() + " frames";
                    tssLabRateValue.Text = Math.Round((spFrameRxCount * 1.0) * 100 / (spFrameTxCount), 2) + "%";
                }
                else
                {
                    tssRxCount.Text = "RX: " + spRxCount.ToString() + " bytes";
                    tssLabRateValue.Text = Math.Round((spRxCount * 1.0) * 100 / (spTxCount), 2) + "%";
                }

                this.Invoke((EventHandler)(delegate
                {
                    StringBuilder tmp_rx_sb = new StringBuilder(50);
                    if (chkShowTime.Checked == true)
                    {
                        string TimeStamp = DateTime.Now.ToString("HH:mm:ss:fff-< ");
                        if (time_newline_enable == true) TimeStamp += "\r\n";
                        tmp_rx_sb.Append(TimeStamp);
                        TimeStamp = DateTime.Now.ToString("yyyy-MM-dd ");//写入文件时添加年月
                        WriteLog(TimeStamp, log_fs);
                    }
                    if (radHexReceive.Checked == true)
                    {
                        tmp_rx_sb.Append(GetHexString(RecBuf, 0, RecLen).ToString());
                    }
                    else if (radAsciiReceive.Checked == true)
                    {
                        tmp_rx_sb.Append(Encoding.UTF8.GetString(RecBuf, 0, RecLen));
                    }

                    if (chkAutoLine.Checked == true)
                    {
                        tmp_rx_sb.Append("\r\n");
                    }

                    if (chkAutoReply.Checked == true)
                    {
                        timerAutoReply.Interval = (int)nudReplyDelay.Value;
                        timerAutoReply.Start();
                    }

                    WriteLog(tmp_rx_sb.ToString(), log_fs);

                    string tmp_str = tmp_rx_sb.ToString();

                    fctbReceive.AppendText(tmp_str);
                    if (o_ScrollBar1.Maximum - (o_ScrollBar1.Value + o_ScrollBar1.ThumbSize) < 20) fctbReceive.GoEnd();



                    picReceiveLed.Image = Properties.Resources.Led_Green_50px;
                    timerReceiveLed.Interval = 100;
                    timerReceiveLed.Start();
                }));
            }
            catch
            {
                ;
            }
        }

        private void cmbCom_DropDown(object sender, EventArgs e)
        {
            cmbCom.Items.Clear();
            foreach (string com in System.IO.Ports.SerialPort.GetPortNames())  //自动获取串行口名称
                this.cmbCom.Items.Add(com);
        }

        private void picDisplay_Click(object sender, EventArgs e)
        {
          
        }

        /// <summary>
        /// 接收led闪烁
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerReceiveLed_Tick(object sender, EventArgs e)
        {
            picReceiveLed.Image = Properties.Resources.Led_Red_50px;
            timerReceiveLed.Stop();
        }

        /// <summary>
        /// 自动回复功能，仅通用发送支持
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerAutoReply_Tick(object sender, EventArgs e)
        {
            this.btnSend_Click(null, null);
            timerAutoReply.Stop();
        }

        private void timerRepeat_Tick(object sender, EventArgs e)
        {
            this.btnSend_Click(null, null);
        }

        private void chkAutoSave_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoSave.Checked == true)
            {
                //设置文件类型  
                saveFileDialog1.Filter = "文本文件|*.txt|所有文件(*.*)|*.*";
                saveFileDialog1.FileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + "Log.txt";
                //保存对话框是否记忆上次打开的目录  
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    log_save_path = saveFileDialog1.FileName.ToString();
                    toolTip1.SetToolTip(chkAutoSave, log_save_path);
                    log_fs = new FileStream(log_save_path, FileMode.Append);
                    WriteLog(fctbReceive.Text, log_fs);
                }
                else
                {
                    chkAutoSave.Checked = false;
                    return;
                }
            }
            else
            {
                try
                {
                    toolTip1.SetToolTip(chkAutoSave, "保存路径为空");
                    log_fs.Close();
                }
                catch
                {
                    return;
                }
            }
        }

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "请选择文件";
            openFileDialog1.Filter = "文本文件|*.txt|所有文件(*.*)|*.*";
            openFileDialog1.FileName = "";
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                btnEditFile.Enabled = true;
                load_file_path = openFileDialog1.FileName;
                load_file(load_file_path);
            }
            else
            {
                //btnEditFile.Enabled = false;
                //cmbLoadFile.Items.Clear();
                return;
            }
        }

        private void cmbLoadFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            txbSend.Clear();
            txbSend.AppendText(cmbLoadFile.Text);
        }

#region
        private const int WS_HSCROLL = 0x100000;
        private const int WS_VSCROLL = 0x200000;
        private const int GWL_STYLE = (-16);
         [System.Runtime.InteropServices.DllImport("user32",CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private static extern int GetWindowLong(IntPtr hwnd, int nIndex);
        /// <summary>
        /// 判断是否出现垂直滚动条
        /// </summary>
        /// <param name="ctrl"></param>
        /// <returns></returns>
        internal static bool IsVerticalScrollBarVisible(Control ctrl)
        {
            if (!ctrl.IsHandleCreated)
                return false;

            return (GetWindowLong(ctrl.Handle, GWL_STYLE) & WS_VSCROLL) != 0;
        }
        /// <summary>
        /// 判断是否出现水平滚动条
        /// </summary>
        /// <param name="ctrl"></param>
        /// <returns></returns>
        internal static bool IsHorizontalScrollBarVisible(Control ctrl)
        {
            if (!ctrl.IsHandleCreated)
                return false;
            return (GetWindowLong(ctrl.Handle, GWL_STYLE) & WS_HSCROLL) != 0;
        }
#endregion
        private void picAddQuickSend_Click(object sender, EventArgs e)
        {
            if (quicksend_list.Count >= 30)
            {
                MessageBox.Show("达到最大条数！", "O-ComTool 警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            add_quick_send((short)quicksend_list.Count, "Name", "Data");
            // 判断是否出现滚动条
            if (IsVerticalScrollBarVisible(tbpQuick))
            {
                tbpQuick.VerticalScroll.Value = tbpQuick.VerticalScroll.Maximum;
            }
            else
            {
                tbpQuick.VerticalScroll.Value = 0;
            }

        }

        private void tbpQuick_Scroll(object sender, ScrollEventArgs e)
        {
            //MessageBox.Show(tbpQuick.VerticalScroll.Maximum + "");
            //e.NewValue = e.OldValue + 1;
        }

        private void tbpQuick_MouseWheel(object sender, MouseEventArgs e)
        {
            //MessageBox.Show(tbpQuick.VerticalScroll.Value+"");
            //tbpQuick.VerticalScroll.Value++;
        }
        private void tbpQuick_SizeChanged(object sender, EventArgs e)
        {
            foreach (QuickSend tmp_quick_send in quicksend_list)
            {
                tmp_quick_send.Width = tbpQuick.Width - 65;
            }
            
        }

        private void tbpQuick_Click(object sender, EventArgs e)
        {
            tbpQuick.Focus();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                picAddQuickSend.Visible = true;
            }
            else
            {
                picAddQuickSend.Visible = false;
            }
        }

        private void chkShowTime_CheckedChanged(object sender, EventArgs e)
        {
            if ((chkShowTime.Checked == true))
            {
                chkAutoLine.Checked = true;
                if (fctbReceive.TextLength > 0)
                fctbReceive.AppendText("\r\n");
            }
        }

        private void tssImageChange_Click(object sender, EventArgs e)
        {
            if (FrameOrByte == true)
            {
                tssRxCount.Text = "RX: " + spRxCount.ToString() + " bytes";
                tssTxCount.Text = "TX: " + spTxCount.ToString() + " bytes";
                tssLabRateValue.Text = Math.Round((spRxCount * 1.0) * 100 / (spTxCount), 2) + "%";
                FrameOrByte = false;
                return;
            }
            else
            {
                tssRxCount.Text = "RX: " + spFrameRxCount.ToString() + " frames";
                tssTxCount.Text = "TX: " + spFrameTxCount.ToString() + " frames";
                tssLabRateValue.Text = Math.Round((spFrameRxCount * 1.0) * 100 / (spFrameTxCount), 2) + "%";
                FrameOrByte = true;
                return;
            }
        }

        private void tssLabReset_Click(object sender, EventArgs e)
        {
            spRxCount = 0;
            spTxCount = 0;
            spFrameRxCount = 0;
            spFrameTxCount = 0;
            if (FrameOrByte == true)
            {
                tssRxCount.Text = "RX: 0 frames";
                tssTxCount.Text = "TX: 0 frames";
            }
            else
            {
                tssRxCount.Text = "RX: 0 bytes";
                tssTxCount.Text = "TX: 0 bytes";
            }
            tssLabRateValue.Text = "NULL";
        }

        private void chkRepeatSend_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRepeatSend.Checked == false)
            {
                timerRepeat.Stop();
                btnSend.Enabled = true;
                nudRepeatInterval.Enabled = true;
            }
        }

        private void load_file_exit(object sender, EventArgs e)
        {
            if (load_file_path != "")
            {
                load_file(load_file_path);
            }

        }

        private void btnEditFile_Click(object sender, EventArgs e)
        {
            Process Proc = new Process();
            ProcessStartInfo Info = new ProcessStartInfo();
            Info.FileName = "notepad.exe";
            Info.Arguments = load_file_path;
            Info.WorkingDirectory = "C://";

            try
            {
                Proc = Process.Start(Info);
                //MessageBox.Show("pid:"+Proc.Id);
                Proc.EnableRaisingEvents = true;//设置进程终止时触发
                Proc.Exited += new EventHandler(load_file_exit);//发现外部程序关闭即触发方法load_file_exit
            }
            catch
            {
                MessageBox.Show("无法找到notepad.exe，文件打开失败！","O-ComTool 错误", MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
        }

        private void btnClearReceive_Click(object sender, EventArgs e)
        {
            fctbReceive.Clear();
        }

        private void btnClearSend_Click(object sender, EventArgs e)
        {
            txbSend.Clear();
        }

        private void chkAutoCount_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoCount.Checked == true)
            {
                AutoCountNum = 0;
            }
        }

        private void tssImageChange_MouseDown(object sender, MouseEventArgs e)
        {
            tssImageChange.Image = Properties.Resources.ReplaceDown_48px;
        }

        private void tssImageChange_MouseUp(object sender, MouseEventArgs e)
        {
            tssImageChange.Image = Properties.Resources.Replace_48px;
        }

        private void picAddQuickSend_MouseDown(object sender, MouseEventArgs e)
        {
            picAddQuickSend.Image = Properties.Resources.AddDown_52px;
        }

        private void picAddQuickSend_MouseUp(object sender, MouseEventArgs e)
        {
            picAddQuickSend.Image = Properties.Resources.Add_48px;
        }

        bool IsP1Collapsed = false;
        private void splitContainer1_Click(object sender, EventArgs e)
        {
            if (IsP1Collapsed == false)
            {
                splitContainer1.SplitterDistance = 0;
                tsmClearSend.Visible = true;
                tsmClearRece.Visible = true;
                IsP1Collapsed = true;
            }
            else
            {
                splitContainer1.SplitterDistance = 140;
                tsmClearSend.Visible = false;
                tsmClearRece.Visible = false;
                IsP1Collapsed = false;
            }
        }

        private void splitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {
            if (splitContainer2.SplitterDistance > splitContainer2.Height - 60)
            {
                splitContainer2.SplitterDistance = splitContainer2.Height - 5;
            }
            else if ((splitContainer2.SplitterDistance < (splitContainer2.Height - 60)) && (splitContainer2.SplitterDistance > (splitContainer2.Height - 120)))
            {
                splitContainer2.SplitterDistance =  splitContainer2.Height - 120;
            }
        }

        private void tsmCalc_Click(object sender, EventArgs e)
        {
            Process.Start(@"c:\windows\system32\calc.exe");
        }

        private void tsmDevMgmt_Click(object sender, EventArgs e)
        {
            Process.Start("devmgmt.msc");
        }

        private void tsmOpenLogPath_Click(object sender, EventArgs e)
        {
            if (log_save_path != "")
            {
                System.Diagnostics.Process.Start("explorer.exe", Path.GetDirectoryName(log_save_path));
            }
            else
            {
                MessageBox.Show("日志目录未指定！","O-ComTool 错误", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void llabAd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void tsmHomePage_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.ifreehub.com");
        }

        //bool display_plan1_enable = true;
        private void tsmChangeDisplay_Click(object sender, EventArgs e)
        {
            if (display_plan1_enable == true)
            {
                //rtbReceive.Font = ReceFont1;
                //cur_color = rtbReceive.ForeColor = ReceForeColor1;
                //rtbReceive.BackColor = ReceBackColor1;

                fctbReceive.Font = ReceFont1;
                cur_color = fctbReceive.ForeColor = ReceForeColor1;
                fctbReceive.BackColor = ReceBackColor1;

                txbSend.Font = SendFont1;
                txbSend.ForeColor = SendForeColor1;
                txbSend.BackColor = SendBackColor1;

                display_plan1_enable = false;
            }
            else
            {
                //rtbReceive.Font = ReceFont2;
                //cur_color = rtbReceive.ForeColor = ReceForeColor2;
                //rtbReceive.BackColor = ReceBackColor2;

                fctbReceive.Font = ReceFont2;
                cur_color = fctbReceive.ForeColor = ReceForeColor2;
                fctbReceive.BackColor = ReceBackColor2;

                txbSend.Font = SendFont2;
                txbSend.ForeColor = SendForeColor2;
                txbSend.BackColor = SendBackColor2;
                display_plan1_enable = true;
            }
            
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (app.Default.MinToTray == true && this.WindowState == FormWindowState.Minimized)
            {
                //this.ShowInTaskbar = false;
                notifyIcon1.Text = this.Text;
                notifyIcon1.Visible = true;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Minimized)
            {
                notifyIcon1.Visible = false;
                this.WindowState = System.Windows.Forms.FormWindowState.Normal;
                //this.Show();
                this.ShowInTaskbar = true;
            }
            else
            {
                this.Activate();
            }
        }

        void SaveConfig()
        {
            // 串口设置
            app.Default.cmbBaudRate = cmbBaudRate.Text;
            app.Default.cmbDataBitIndex = (short)cmbDataBit.SelectedIndex;
            app.Default.cmbParityBitIndex = (short)cmbParityBit.SelectedIndex;
            app.Default.cmbStopBitIndex = (short)cmbStopBit.SelectedIndex;
            app.Default.cmbFlowCtrlIndex = (short)cmbFlowCtrl.SelectedIndex;

            // 接收设置
            app.Default.receEncodeAscii = radAsciiReceive.Checked == true ? true : false;
            app.Default.chkAutoLine = chkAutoLine.Checked == true ? true : false;
            app.Default.chkShowTime = chkShowTime.Checked == true ? true : false;
            app.Default.AutoReplyDelay = (int)nudReplyDelay.Value;

            // 发送设置
            app.Default.sendEncodeAscii = radAsciiSend.Checked == true ? true : false;
            app.Default.chkAppendNewLine = chkNewLine.Checked == true ? true : false;
            app.Default.RepeatInterval = (int)nudRepeatInterval.Value;

            // 发送数据
            app.Default.GeneralSendData = txbSend.Text;
            
            // 快捷发送保存
            app.Default.QuickSendCount = (short)quicksend_list.Count;
            app.Default.QuickSendTitle.Clear();
            app.Default.QuickSendData.Clear();

            app.Default.DisplayPlan1Enable = !display_plan1_enable;

            for (int i = 0; i < quicksend_list.Count; i++)
            {
                app.Default.QuickSendTitle.Add(quicksend_list[i].ItemName);
                app.Default.QuickSendData.Add(quicksend_list[i].Data); 
            }
            app.Default.Save();

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (app.Default.CloseToTray == true)
            {
                e.Cancel = true;
                notifyIcon1.Text = this.Text;
                this.WindowState = FormWindowState.Minimized;
                //this.Hide();
                notifyIcon1.Visible = true;
                this.ShowInTaskbar = false;
            }
            else
            {
                if (app.Default.QuitConfirm == true)
                {
                    if (MessageBox.Show("确认退出当前串口工具？", "O-ComTool 确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                    }
                }
                
                SaveConfig();
                if (serialPort1.IsOpen == true) serialPort1.Close();
                if (chkAutoSave.Checked == true) log_fs.Close();
                
                notifyIcon1.Dispose();

            }
        }

        private void tsmExit_Click(object sender, EventArgs e)
        {
            SaveConfig();
            notifyIcon1.Dispose();
            System.Environment.Exit(0);
        }

        private void cmsExit_Click(object sender, EventArgs e)
        {
            SaveConfig();
            notifyIcon1.Dispose();
            System.Environment.Exit(0);
        }

        private void cmsAbout_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        private void cmsTitle_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.ifreehub.com");
        }

        private void cmsSelectAll_Click(object sender, EventArgs e)
        {
            ((FastColoredTextBox)contextMenuStrip2.SourceControl).SelectAll();
        }

        private void cmsCopy_Click(object sender, EventArgs e)
        {
            string selectText = ((FastColoredTextBox)contextMenuStrip2.SourceControl).SelectedText;
            if (selectText != "")
            {
                Clipboard.SetText(selectText);
            }
        }

        private void cmsPaste_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                FastColoredTextBox txtBox = (FastColoredTextBox)contextMenuStrip2.SourceControl;
                int index = txtBox.SelectionStart;  //记录下粘贴前的光标位置
                string text = txtBox.Text;
                //删除选中的文本
                text = text.Remove(txtBox.SelectionStart, txtBox.SelectionLength);
                //在当前光标输入点插入剪切板内容
                text = text.Insert(txtBox.SelectionStart, Clipboard.GetText());
                txtBox.Text = text;
                //重设光标位置
                txtBox.SelectionStart = index;
            }
        }

        byte RightKeyCheckSum(byte[] buffer, int length)
        {
            byte CS = 0;
            for (int i = 0; i < length; i++)
                CS += buffer[i];
            return CS;
        }

        private void cmsCheckSum_Click(object sender, EventArgs e)
        {
            int i = 0;
            string selectText = ((FastColoredTextBox)contextMenuStrip2.SourceControl).SelectedText;
            MatchCollection mc = Regex.Matches(selectText, @"(?i)[\da-f]{2}");
            byte[] bytesToCheck = new byte[mc.Count];
            foreach (Match m in mc)//遍历所有mc，并将其转换成十六进制
            {
                bytesToCheck[i++] = byte.Parse(m.Value, System.Globalization.NumberStyles.HexNumber);//赋值并累加
            }
            MessageBox.Show("校验和：0x" + RightKeyCheckSum(bytesToCheck, i).ToString("X2"), "O-ComTool 校验和", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        byte RightKeyXOR(byte[] buffer, int length)
        {
            byte xor = 0;
            for (int i = 0; i < length; i++)
                xor ^= buffer[i];
            return xor;
        }
        private void cmsXor_Click(object sender, EventArgs e)
        {
            int i = 0;
            string selectText = ((FastColoredTextBox)contextMenuStrip2.SourceControl).SelectedText;
            MatchCollection mc = Regex.Matches(selectText, @"(?i)[\da-f]{2}");
            byte[] bytesToCheck = new byte[mc.Count];
            foreach (Match m in mc)//遍历所有mc，并将其转换成十六进制
            {
                bytesToCheck[i++] = byte.Parse(m.Value, System.Globalization.NumberStyles.HexNumber);//赋值并累加
            }
            MessageBox.Show("异或值：0x" + RightKeyXOR(bytesToCheck, i).ToString("X2"), "O-ComTool 异或值", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        private void contextMenuStrip2_Opening(object sender, CancelEventArgs e)
        {
            //没有选择文本时，复制菜单禁用
            string selectText = ((FastColoredTextBox)contextMenuStrip2.SourceControl).SelectedText;
            if (selectText != "")
            {
                cmsCopy.Enabled = true;
                cmsCheckSum.Enabled = true;
                cmsXor.Enabled = true;
                cmsA2H.Enabled = true;
                cmsH2A.Enabled = true;
                cmsHexFormat.Enabled = true;
                cmsCalcLength.Enabled = true;
            }
            else
            {
                cmsCopy.Enabled = false;
                cmsCheckSum.Enabled = false;
                cmsXor.Enabled = false;
                cmsA2H.Enabled = false;
                cmsH2A.Enabled = false;
                cmsHexFormat.Enabled = false;
                cmsCalcLength.Enabled = false;
            }
            //剪切板没有文本内容时，粘贴菜单禁用
            if (Clipboard.ContainsText())
            {
                cmsPaste.Enabled = true;
            }
            else
            {
                cmsPaste.Enabled = false;
            }
        }

        private void cmsCalcLength_Click(object sender, EventArgs e)
        {
            string str;
            string selectText = ((FastColoredTextBox)contextMenuStrip2.SourceControl).SelectedText;
            MatchCollection mc = Regex.Matches(selectText, @"(?i)[\da-f]{2}");
            str = "ASCII长度：" + selectText.Length + " (0x" + selectText.Length.ToString("X2") + ")" + " Bytes\n";
            str += "  HEX长度：" + mc.Count + " (0x" + mc.Count.ToString("X2") + ")" + " Bytes\n";
            
            MessageBox.Show(str, "O-ComTool 字符长度", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        private void cmsH2A_Click(object sender, EventArgs e)
        {
            int i = 0;
            string selectText = ((FastColoredTextBox)contextMenuStrip2.SourceControl).SelectedText;
            MatchCollection mc = Regex.Matches(selectText, @"(?i)[\da-f]{2}");
            byte[] bytesToCheck = new byte[mc.Count];
            foreach (Match m in mc)//遍历所有mc，并将其转换成十六进制
            {
                bytesToCheck[i++] = byte.Parse(m.Value, System.Globalization.NumberStyles.HexNumber);//赋值并累加
            }
            string str = Encoding.UTF8.GetString(bytesToCheck, 0, mc.Count);
            if (MessageBox.Show("Hex2Ascii：" + str, "O-ComTool Hex2Ascii", MessageBoxButtons.OKCancel, MessageBoxIcon.None) == DialogResult.OK)
            {
                Clipboard.SetText(str);
            }

        }

        private void cmsA2H_Click(object sender, EventArgs e)
        {
            string selectText = ((FastColoredTextBox)contextMenuStrip2.SourceControl).SelectedText;
            byte[] ba = System.Text.ASCIIEncoding.Default.GetBytes(selectText);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in ba)
            {
                sb.Append(b.ToString("X2") + " ");
            }
            string str = sb.ToString();
            if (MessageBox.Show("Ascii2Hex：" + str, "O-ComTool Ascii2Hex", MessageBoxButtons.OKCancel, MessageBoxIcon.None) == DialogResult.OK)
            {
                Clipboard.SetText(str);
            }
        }

        private void cmsHexFormat_Click(object sender, EventArgs e)
        {
            int i = 0;
            string selectText = ((FastColoredTextBox)contextMenuStrip2.SourceControl).SelectedText;
            MatchCollection mc = Regex.Matches(selectText, @"(?i)[\da-f]{2}");
            byte[] bytesToCheck = new byte[mc.Count];
            foreach (Match m in mc)//遍历所有mc，并将其转换成十六进制
            {
                bytesToCheck[i++] = byte.Parse(m.Value, System.Globalization.NumberStyles.HexNumber);//赋值并累加
            }

            fctbReceive.SelectedText = GetHexString(bytesToCheck, 0, mc.Count).ToString();
        }

        private void contextMenuStrip3_Opening(object sender, CancelEventArgs e)
        {
            //没有选择文本时，复制菜单禁用
            string selectText = ((TextBox)contextMenuStrip3.SourceControl).SelectedText;
            if (selectText != "")
            {
                cmstbCopy.Enabled = true;
                cmstbCheckSum.Enabled = true;
                cmstbXor.Enabled = true;
                cmstbA2H.Enabled = true;
                cmstbH2A.Enabled = true;
                cmstbHexFormat.Enabled = true;
                cmstbCalcLength.Enabled = true;
            }
            else
            {
                cmstbCopy.Enabled = false;
                cmstbCheckSum.Enabled = false;
                cmstbXor.Enabled = false;
                cmstbA2H.Enabled = false;
                cmstbH2A.Enabled = false;
                cmstbHexFormat.Enabled = false;
                cmstbCalcLength.Enabled = false;
            }
            //剪切板没有文本内容时，粘贴菜单禁用
            if (Clipboard.ContainsText())
            {
                cmsPaste.Enabled = true;
            }
            else
            {
                cmsPaste.Enabled = false;
            }
        }

        private void cmstbSelectAll_Click(object sender, EventArgs e)
        {
            ((TextBox)contextMenuStrip3.SourceControl).SelectAll();
        }

        private void cmstbCopy_Click(object sender, EventArgs e)
        {
            string selectText = ((TextBox)contextMenuStrip3.SourceControl).SelectedText;
            if (selectText != "")
            {
                Clipboard.SetText(selectText);
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                TextBox txtBox = (TextBox)contextMenuStrip3.SourceControl;
                int index = txtBox.SelectionStart;  //记录下粘贴前的光标位置
                string text = txtBox.Text;
                //删除选中的文本
                text = text.Remove(txtBox.SelectionStart, txtBox.SelectionLength);
                //在当前光标输入点插入剪切板内容
                text = text.Insert(txtBox.SelectionStart, Clipboard.GetText());
                txtBox.Text = text;
                //重设光标位置
                txtBox.SelectionStart = index;
            }
        }

        private void cmstbH2A_Click(object sender, EventArgs e)
        {
            //int i = 0;
            //string selectText = ((TextBox)contextMenuStrip3.SourceControl).SelectedText;
            //MatchCollection mc = Regex.Matches(selectText, @"(?i)[\da-f]{2}");
            //byte[] bytesToCheck = new byte[mc.Count];
            //foreach (Match m in mc)//遍历所有mc，并将其转换成十六进制
            //{
            //    bytesToCheck[i++] = byte.Parse(m.Value, System.Globalization.NumberStyles.HexNumber);//赋值并累加
            //}

            //MessageBox.Show("Hex2Ascii：" + Encoding.UTF8.GetString(bytesToCheck, 0, mc.Count), "O-ComTool Hex2Ascii", MessageBoxButtons.OK, MessageBoxIcon.None);

            int i = 0;
            string selectText = ((TextBox)contextMenuStrip3.SourceControl).SelectedText;
            MatchCollection mc = Regex.Matches(selectText, @"(?i)[\da-f]{2}");
            byte[] bytesToCheck = new byte[mc.Count];
            foreach (Match m in mc)//遍历所有mc，并将其转换成十六进制
            {
                bytesToCheck[i++] = byte.Parse(m.Value, System.Globalization.NumberStyles.HexNumber);//赋值并累加
            }
            string str = Encoding.UTF8.GetString(bytesToCheck, 0, mc.Count);
            if (MessageBox.Show("Hex2Ascii：" + str, "O-ComTool Hex2Ascii", MessageBoxButtons.OKCancel, MessageBoxIcon.None) == DialogResult.OK)
            {
                Clipboard.SetText(str);
            }

        }

        private void cmstbA2H_Click(object sender, EventArgs e)
        {
            //string selectText = ((TextBox)contextMenuStrip3.SourceControl).SelectedText;
            //byte[] ba = System.Text.ASCIIEncoding.Default.GetBytes(selectText);
            //StringBuilder sb = new StringBuilder();
            //foreach (byte b in ba)
            //{
            //    sb.Append(b.ToString("X2") + " ");
            //}
            ////rtbReceive.SelectedText = sb.ToString();
            //MessageBox.Show("Ascii2Hex：" + sb.ToString(), "O-ComTool Ascii2Hex", MessageBoxButtons.OK, MessageBoxIcon.None);

            string selectText = ((TextBox)contextMenuStrip3.SourceControl).SelectedText;
            byte[] ba = System.Text.ASCIIEncoding.Default.GetBytes(selectText);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in ba)
            {
                sb.Append(b.ToString("X2") + " ");
            }
            string str = sb.ToString();
            if (MessageBox.Show("Ascii2Hex：" + str, "O-ComTool Ascii2Hex", MessageBoxButtons.OKCancel, MessageBoxIcon.None) == DialogResult.OK)
            {
                Clipboard.SetText(str);
            }

        }

        private void cmstbHexFormat_Click(object sender, EventArgs e)
        {
            int i = 0;
            string selectText = ((TextBox)contextMenuStrip3.SourceControl).SelectedText;
            MatchCollection mc = Regex.Matches(selectText, @"(?i)[\da-f]{2}");
            byte[] bytesToCheck = new byte[mc.Count];
            foreach (Match m in mc)//遍历所有mc，并将其转换成十六进制
            {
                bytesToCheck[i++] = byte.Parse(m.Value, System.Globalization.NumberStyles.HexNumber);//赋值并累加
            }

            txbSend.SelectedText = GetHexString(bytesToCheck, 0, mc.Count).ToString();
        }

        private void cmstbCalcLength_Click(object sender, EventArgs e)
        {
            //string str;
            //string selectText = ((TextBox)contextMenuStrip3.SourceControl).SelectedText;
            //MatchCollection mc = Regex.Matches(selectText, @"(?i)[\da-f]{2}");
            //str = "ASCII长度：" + selectText.Length + " (0x" + selectText.Length.ToString("X2") + ")" + " Bytes\n";
            //str += "  HEX长度：" + mc.Count + " (0x" + mc.Count.ToString("X2") + ")" + " Bytes\n";

            //MessageBox.Show(str, "O-ComTool 字符长度", MessageBoxButtons.OK, MessageBoxIcon.None);

            string str;
            string selectText = ((TextBox)contextMenuStrip3.SourceControl).SelectedText;
            MatchCollection mc = Regex.Matches(selectText, @"(?i)[\da-f]{2}");
            str = "ASCII长度：" + selectText.Length + " (0x" + selectText.Length.ToString("X2") + ")" + " Bytes\n";
            str += "  HEX长度：" + mc.Count + " (0x" + mc.Count.ToString("X2") + ")" + " Bytes\n";

            MessageBox.Show(str, "O-ComTool 字符长度", MessageBoxButtons.OK, MessageBoxIcon.None);

        }

        private void cmstbCheckSum_Click(object sender, EventArgs e)
        {
            int i = 0;
            string selectText = ((TextBox)contextMenuStrip3.SourceControl).SelectedText;
            MatchCollection mc = Regex.Matches(selectText, @"(?i)[\da-f]{2}");
            byte[] bytesToCheck = new byte[mc.Count];
            foreach (Match m in mc)//遍历所有mc，并将其转换成十六进制
            {
                bytesToCheck[i++] = byte.Parse(m.Value, System.Globalization.NumberStyles.HexNumber);//赋值并累加
            }
            MessageBox.Show("校验和：0x" + RightKeyCheckSum(bytesToCheck, i).ToString("X2"), "O-ComTool 校验和", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        private void cmstbXor_Click(object sender, EventArgs e)
        {
            int i = 0;
            string selectText = ((TextBox)contextMenuStrip3.SourceControl).SelectedText;
            MatchCollection mc = Regex.Matches(selectText, @"(?i)[\da-f]{2}");
            byte[] bytesToCheck = new byte[mc.Count];
            foreach (Match m in mc)//遍历所有mc，并将其转换成十六进制
            {
                bytesToCheck[i++] = byte.Parse(m.Value, System.Globalization.NumberStyles.HexNumber);//赋值并累加
            }
            MessageBox.Show("异或值：0x" + RightKeyXOR(bytesToCheck, i).ToString("X2"), "O-ComTool 异或值", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        private void cmsSaveCur_Click(object sender, EventArgs e)
        {
            string tmp_path = "";
            FileStream tmp_fs;
            
            //设置文件类型  
            saveFileDialog1.Filter = "文本文件|*.txt|所有文件(*.*)|*.*";
            saveFileDialog1.FileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + "Log.txt";
            //保存对话框是否记忆上次打开的目录  
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                tmp_path = saveFileDialog1.FileName.ToString();
                tmp_fs = new FileStream(tmp_path, FileMode.Append);
                StreamWriter fsw = new StreamWriter(tmp_fs);
                fsw.Write(fctbReceive.Text);
                fsw.Flush();
                tmp_fs.Close();
            }
            else
            {
                return;
            }
        }

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);

        string Color2Hex(Color s_color)
        {
            string d_color_str;
            d_color_str = "#" + /*s_color.A.ToString("x2") + */s_color.R.ToString("x2") + s_color.G.ToString("x2") + s_color.B.ToString("x2");
            return d_color_str.ToUpper();
        }
        string Font2Str(Font s_font)
        {
            string d_font_str = s_font.Name + "," + s_font.SizeInPoints + "pt";
            return d_font_str;
        }
        private void tsmExportConfig_Click(object sender, EventArgs e)
        {
            string file_path = "";
            string section = "";
            //设置文件类型  
            saveFileDialog1.Filter = "文本文件|*.ini|所有文件(*.*)|*.*";
            saveFileDialog1.FileName = "O-ComTool_cfg.ini";
            //保存对话框是否记忆上次打开的目录  
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                file_path = saveFileDialog1.FileName.ToString();

                // SerialPort
                section = "SerialPort";
                WritePrivateProfileString(section, "cmbBaudRate", cmbBaudRate.Text, file_path);
                WritePrivateProfileString(section, "cmbDataBitIndex", cmbDataBit.SelectedIndex.ToString(), file_path);
                WritePrivateProfileString(section, "cmbParityBitIndex", cmbParityBit.SelectedIndex.ToString(), file_path);
                WritePrivateProfileString(section, "cmbStopBitIndex", cmbStopBit.SelectedIndex.ToString(), file_path);
                WritePrivateProfileString(section, "cmbFlowCtrlIndex", cmbFlowCtrl.SelectedIndex.ToString(), file_path);

                section = "Receive";
                WritePrivateProfileString(section, "receEncodeAscii", radAsciiReceive.Checked == true ? "True" : "False", file_path);
                WritePrivateProfileString(section, "chkAutoLine", chkAutoLine.Checked == true ? "True" : "False", file_path);
                WritePrivateProfileString(section, "chkShowTime", chkShowTime.Checked == true ? "True" : "False", file_path);
                WritePrivateProfileString(section, "AutoReplyDelay", nudReplyDelay.Value.ToString(), file_path);

                section = "Send";
                WritePrivateProfileString(section, "sendEncodeAscii", radAsciiSend.Checked == true ? "True" : "False", file_path);
                WritePrivateProfileString(section, "chkAppendNewLine", chkNewLine.Checked == true ? "True" : "False", file_path);
                WritePrivateProfileString(section, "RepeatInterval", nudRepeatInterval.Value.ToString(), file_path);

                section = "GeneralSend";
                WritePrivateProfileString(section, "GeneralSendData", txbSend.Text, file_path);
                WritePrivateProfileString(section, "LoadFileEnable", app.Default.LoadFileEnable.ToString(), file_path);
                WritePrivateProfileString(section, "LoadFilePath", load_file_path, file_path);

                section = "QuickSend";
                WritePrivateProfileString(section, "QuickSendCount", quicksend_list.Count.ToString(), file_path);
                for (int i = 0; i < quicksend_list.Count; i++)
                {
                    WritePrivateProfileString(section, "QuickSendTitle" + i, quicksend_list[i].ItemName, file_path);
                    WritePrivateProfileString(section, "QuickSendData" + i, quicksend_list[i].Data, file_path);
                }
                
                section = "Option";
                var cvt = new FontConverter();
                WritePrivateProfileString(section, "MinToTray", app.Default.MinToTray.ToString(), file_path);
                WritePrivateProfileString(section, "CloseToTray", app.Default.CloseToTray.ToString(), file_path);
                WritePrivateProfileString(section, "ReceBackColor1", Color2Hex(app.Default.ReceBackColor1), file_path);
                WritePrivateProfileString(section, "ReceForeColor1", Color2Hex(app.Default.ReceForeColor1), file_path);
                WritePrivateProfileString(section, "SendBackColor1", Color2Hex(app.Default.SendBackColor1), file_path);
                WritePrivateProfileString(section, "SendForeColor1", Color2Hex(app.Default.SendForeColor1), file_path);
                WritePrivateProfileString(section, "ReceFont1", cvt.ConvertToString(app.Default.ReceFont1), file_path);
                WritePrivateProfileString(section, "SendFont1", cvt.ConvertToString(app.Default.SendFont1), file_path);
                WritePrivateProfileString(section, "ReceBackColor2", Color2Hex(app.Default.ReceBackColor2), file_path);
                WritePrivateProfileString(section, "ReceForeColor2", Color2Hex(app.Default.ReceForeColor2), file_path);
                WritePrivateProfileString(section, "SendBackColor2", Color2Hex(app.Default.SendBackColor2), file_path);
                WritePrivateProfileString(section, "SendForeColor2", Color2Hex(app.Default.SendForeColor2), file_path);
                WritePrivateProfileString(section, "ReceFont2", cvt.ConvertToString(app.Default.ReceFont2), file_path);
                WritePrivateProfileString(section, "SendFont2", cvt.ConvertToString(app.Default.SendFont2), file_path);
                WritePrivateProfileString(section, "QuitConfirm", app.Default.QuitConfirm.ToString(), file_path);
                WritePrivateProfileString(section, "HightLightEnable", app.Default.HightLightEnable.ToString(), file_path);
                WritePrivateProfileString(section, "SendDisplayEnable", app.Default.HightLightEnable.ToString(), file_path);
                WritePrivateProfileString(section, "CommentEnable", app.Default.HightLightEnable.ToString(), file_path);
                WritePrivateProfileString(section, "FrameInterval", app.Default.FrameInterval.ToString(), file_path);
                WritePrivateProfileString(section, "TimeNewline", app.Default.chkTimeNewLine.ToString(), file_path);
                WritePrivateProfileString(section, "Send2FileEnable", app.Default.Send2FileEnable.ToString(), file_path);
                WritePrivateProfileString(section, "Send2NewLineEnable", app.Default.Send2NewLineEnable.ToString(), file_path);

                WritePrivateProfileString(section, "HighLightRed", app.Default.HighLightRed, file_path);
                WritePrivateProfileString(section, "HighLightGreen", app.Default.HighLightGreen, file_path);
                WritePrivateProfileString(section, "HighLightYellow", app.Default.HighLightYellow, file_path);
                WritePrivateProfileString(section, "HighLightBlue", app.Default.HighLightBlue, file_path);
                WritePrivateProfileString(section, "HighLightMagenta", app.Default.HighLightMagenta, file_path);
                WritePrivateProfileString(section, "HighLightCyan", app.Default.HighLightCyan, file_path);
                WritePrivateProfileString(section, "HighLightOrange", app.Default.HighLightOrange, file_path);

                WritePrivateProfileString(section, "LoadFileEnable", app.Default.LoadFileEnable.ToString(), file_path);
                WritePrivateProfileString(section, "CommentEnable", app.Default.CommentEnable.ToString(), file_path);
                WritePrivateProfileString(section, "DisplayPlan1Enable", app.Default.DisplayPlan1Enable.ToString(), file_path);
                WritePrivateProfileString(section, "SendDisplayEnable", app.Default.SendDisplayEnable.ToString(), file_path);  


                MessageBox.Show("配置文件导出成功！", "O-ComTool 提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                chkAutoSave.Checked = false;
                return;
            }
        }

        TextStyle redStyle = new TextStyle(Brushes.Red, null, FontStyle.Regular);
        TextStyle greenStyle = new TextStyle(Brushes.LightGreen, null, FontStyle.Regular);
        TextStyle yellowStyle = new TextStyle(Brushes.Yellow, null, FontStyle.Regular);
        TextStyle blueStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);
        TextStyle magentaStyle = new TextStyle(Brushes.Magenta, null, FontStyle.Regular);
        TextStyle cyanStyle = new TextStyle(Brushes.Cyan, null, FontStyle.Regular);
        TextStyle orangeStyle = new TextStyle(Brushes.Orange, null, FontStyle.Regular);

        private void fctbReceive_VisibleRangeChanged(object sender, EventArgs e)
        {
            if (hight_light_enable == false) return;//为使能，则退出
            try
            {
                var range = fctbReceive.VisibleRange;
                range.ClearStyle(StyleIndex.All);

                //highlight tags
                if (hl_red_regex_str != null)
                    fctbReceive.VisibleRange.SetStyle(redStyle, hl_red_regex_str, RegexOptions.IgnoreCase);
                if (hl_green_regex_str != null)
                    fctbReceive.VisibleRange.SetStyle(greenStyle, hl_green_regex_str, RegexOptions.IgnoreCase);
                if (hl_yellow_regex_str != null)
                    fctbReceive.VisibleRange.SetStyle(yellowStyle, hl_yellow_regex_str, RegexOptions.IgnoreCase);
                if (hl_blue_regex_str != null)
                    fctbReceive.VisibleRange.SetStyle(blueStyle, hl_blue_regex_str, RegexOptions.IgnoreCase);
                if (hl_cyan_regex_str != null)
                    fctbReceive.VisibleRange.SetStyle(cyanStyle, hl_cyan_regex_str, RegexOptions.IgnoreCase);
                if (hl_magenta_regex_str != null)
                    fctbReceive.VisibleRange.SetStyle(magentaStyle, hl_magenta_regex_str, RegexOptions.IgnoreCase);
                if (hl_orange_regex_str != null)
                    fctbReceive.VisibleRange.SetStyle(orangeStyle, hl_orange_regex_str, RegexOptions.IgnoreCase);


            }
            catch
            {
                MessageBox.Show("Highlight Regex Error, Please Check!");
            }

        }

        private void o_ScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            fctbReceive.OnScroll(e, e.Type != ScrollEventType.ThumbTrack && e.Type != ScrollEventType.ThumbPosition);
        }

        private void fctbReceive_ScrollbarsUpdated(object sender, EventArgs e)
        {
                AdjustScrollbars();
        }

        private string GetPrivateProfileStringFake(string Section, string key, string path)
        {

            StringBuilder temp = new StringBuilder(1024);
            GetPrivateProfileString(Section, key, "", temp, 1024, path);
            return temp.ToString();
        }

        private void tsmImportConfig_Click(object sender, EventArgs e)
        {
            string file_path = "";
            string section = "";
            StringBuilder temp = new StringBuilder(1024);
            openFileDialog1.Title = "请选择文件";
            openFileDialog1.Filter = "文本文件|*.ini|所有文件(*.*)|*.*";
            openFileDialog1.FileName = "";
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                file_path = openFileDialog1.FileName;

                try
                {
                    section = "SerialPort";
                    app.Default.cmbBaudRate = GetPrivateProfileStringFake(section, "cmbBaudRate", file_path);
                    app.Default.cmbDataBitIndex = short.Parse(GetPrivateProfileStringFake(section, "cmbDataBitIndex", file_path));
                    app.Default.cmbParityBitIndex = short.Parse(GetPrivateProfileStringFake(section, "cmbParityBitIndex", file_path));
                    app.Default.cmbStopBitIndex = short.Parse(GetPrivateProfileStringFake(section, "cmbStopBitIndex", file_path));
                    app.Default.cmbFlowCtrlIndex = short.Parse(GetPrivateProfileStringFake(section, "cmbFlowCtrlIndex", file_path));

                    section = "Receive";
                    app.Default.receEncodeAscii = Convert.ToBoolean(GetPrivateProfileStringFake(section, "receEncodeAscii", file_path));
                    app.Default.chkAutoLine = Convert.ToBoolean(GetPrivateProfileStringFake(section, "chkAutoLine", file_path));
                    app.Default.chkShowTime = Convert.ToBoolean(GetPrivateProfileStringFake(section, "chkShowTime", file_path));
                    app.Default.AutoReplyDelay = int.Parse(GetPrivateProfileStringFake(section, "AutoReplyDelay", file_path));

                    section = "Send";
                    app.Default.sendEncodeAscii = Convert.ToBoolean(GetPrivateProfileStringFake(section, "sendEncodeAscii", file_path));
                    app.Default.chkAppendNewLine = Convert.ToBoolean(GetPrivateProfileStringFake(section, "chkAppendNewLine", file_path));
                    app.Default.RepeatInterval = int.Parse(GetPrivateProfileStringFake(section, "RepeatInterval", file_path));

                    section = "GeneralSend";
                    app.Default.GeneralSendData = GetPrivateProfileStringFake(section, "GeneralSendData", file_path);
                    app.Default.LoadFileEnable = Convert.ToBoolean(GetPrivateProfileStringFake(section, "LoadFileEnable", file_path));
                    app.Default.LoadFilePath = GetPrivateProfileStringFake(section, "LoadFilePath", file_path);

                    section = "QuickSend";
                    app.Default.QuickSendData.Clear();
                    app.Default.QuickSendTitle.Clear();
                    app.Default.QuickSendCount = short.Parse(GetPrivateProfileStringFake(section, "QuickSendCount", file_path));
                    for (short i = 0; i < app.Default.QuickSendCount; i++)
                    {
                        app.Default.QuickSendTitle.Add(GetPrivateProfileStringFake(section, "QuickSendTitle" + i, file_path));
                        app.Default.QuickSendData.Add(GetPrivateProfileStringFake(section, "QuickSendData" + i, file_path));
                    }

                    section = "Option";
                    var cvt = new FontConverter();
                    app.Default.MinToTray = Convert.ToBoolean(GetPrivateProfileStringFake(section, "MinToTray", file_path));
                    app.Default.CloseToTray = Convert.ToBoolean(GetPrivateProfileStringFake(section, "CloseToTray", file_path));
                    app.Default.ReceBackColor1 = System.Drawing.ColorTranslator.FromHtml(GetPrivateProfileStringFake(section, "ReceBackColor1", file_path));
                    app.Default.ReceForeColor1 = System.Drawing.ColorTranslator.FromHtml(GetPrivateProfileStringFake(section, "ReceForeColor1", file_path));
                    app.Default.SendBackColor1 = System.Drawing.ColorTranslator.FromHtml(GetPrivateProfileStringFake(section, "SendBackColor1", file_path));
                    app.Default.SendForeColor1 = System.Drawing.ColorTranslator.FromHtml(GetPrivateProfileStringFake(section, "SendForeColor1", file_path));
                    app.Default.ReceFont1 = cvt.ConvertFromString(GetPrivateProfileStringFake(section, "ReceFont1", file_path)) as Font;
                    app.Default.SendFont1 = cvt.ConvertFromString(GetPrivateProfileStringFake(section, "SendFont1", file_path)) as Font;

                    app.Default.ReceBackColor2 = System.Drawing.ColorTranslator.FromHtml(GetPrivateProfileStringFake(section, "ReceBackColor2", file_path));
                    app.Default.ReceForeColor2 = System.Drawing.ColorTranslator.FromHtml(GetPrivateProfileStringFake(section, "ReceForeColor2", file_path));
                    app.Default.SendBackColor2 = System.Drawing.ColorTranslator.FromHtml(GetPrivateProfileStringFake(section, "SendBackColor2", file_path));
                    app.Default.SendForeColor2 = System.Drawing.ColorTranslator.FromHtml(GetPrivateProfileStringFake(section, "SendForeColor2", file_path));
                    app.Default.ReceFont2 = cvt.ConvertFromString(GetPrivateProfileStringFake(section, "ReceFont2", file_path)) as Font;
                    app.Default.SendFont2 = cvt.ConvertFromString(GetPrivateProfileStringFake(section, "SendFont2", file_path)) as Font;
                    app.Default.QuitConfirm = Convert.ToBoolean(GetPrivateProfileStringFake(section, "QuitConfirm", file_path));
                    app.Default.HightLightEnable = Convert.ToBoolean(GetPrivateProfileStringFake(section, "HightLightEnable", file_path));
                    app.Default.SendDisplayEnable = Convert.ToBoolean(GetPrivateProfileStringFake(section, "SendDisplayEnable", file_path));
                    app.Default.CommentEnable = Convert.ToBoolean(GetPrivateProfileStringFake(section, "CommentEnable", file_path));

                    app.Default.FrameInterval = int.Parse(GetPrivateProfileStringFake(section, "FrameInterval", file_path));
                    app.Default.chkTimeNewLine = Convert.ToBoolean(GetPrivateProfileStringFake(section, "TimeNewline", file_path));
                    app.Default.Send2FileEnable = Convert.ToBoolean(GetPrivateProfileStringFake(section, "Send2FileEnable", file_path));
                    app.Default.Send2NewLineEnable = Convert.ToBoolean(GetPrivateProfileStringFake(section, "Send2NewLineEnable", file_path));

                    app.Default.HighLightRed = GetPrivateProfileStringFake(section, "HighLightRed", file_path);
                    app.Default.HighLightGreen = GetPrivateProfileStringFake(section, "HighLightGreen", file_path);
                    app.Default.HighLightYellow = GetPrivateProfileStringFake(section, "HighLightYellow", file_path);
                    app.Default.HighLightBlue = GetPrivateProfileStringFake(section, "HighLightBlue", file_path);
                    app.Default.HighLightMagenta = GetPrivateProfileStringFake(section, "HighLightMagenta", file_path);
                    app.Default.HighLightCyan = GetPrivateProfileStringFake(section, "HighLightCyan", file_path);
                    app.Default.HighLightOrange = GetPrivateProfileStringFake(section, "HighLightOrange", file_path);

                    LoadLastConfig();
                    MessageBox.Show("导入配置文件成功！", "O-ComTool 提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch 
                {
                    MessageBox.Show("导入配置文件失败！","O-ComTool 错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            else
            {
                //btnEditFile.Enabled = false;
                //cmbLoadFile.Items.Clear();
                return;
            }
        }

        private void cmsOption_Click(object sender, EventArgs e)
        {
            tsmOption_Click(sender, e);
        }

        private void tsmClearSend_Click(object sender, EventArgs e)
        {
            txbSend.Clear();
        }

        private void tsmClearRece_Click(object sender, EventArgs e)
        {
            fctbReceive.Clear();
        }

        private void AdjustScrollbars()
        {
            AdjustScrollbar(o_ScrollBar1, fctbReceive.VerticalScroll.Maximum, fctbReceive.VerticalScroll.Value, fctbReceive.ClientSize.Height);
            //AdjustScrollbar(fctbReceive, fctbReceive.VerticalScroll.Maximum, fctbReceive.VerticalScroll.Value, fctbReceive.ClientSize.Height);
        }

        /// <summary>
        /// This method for MyScrollBar
        /// </summary>
        private void AdjustScrollbar(O_ScrollBar scrollBar, int max, int value, int clientSize)
        {
            scrollBar.Maximum = max;
            scrollBar.Visible = max > 0;
            scrollBar.Value = Math.Min(scrollBar.Maximum, value);
        }

        /// <summary>
        /// This method for System.Windows.Forms.ScrollBar and inherited classes
        /// </summary>
        private void AdjustScrollbar(ScrollBar scrollBar, int max, int value, int clientSize)
        {
            scrollBar.LargeChange = clientSize / 3;
            scrollBar.SmallChange = clientSize / 11;
            scrollBar.Maximum = max + scrollBar.LargeChange;
            scrollBar.Visible = max > 0;
            scrollBar.Value = Math.Min(scrollBar.Maximum, value);
        }

        

        

    }
}
