using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace O_ComTool_Pro
{
    public partial class Option : Form
    {

        MainForm main_form;
        public Option(MainForm form)
        {
            this.main_form = form;
            InitializeComponent();
        }

        private void Option_Load(object sender, EventArgs e)
        {
            // 基本
            chkQuitToTray.Checked = app.Default.CloseToTray;
            chkMinToTray.Checked = app.Default.MinToTray;
            chkQuitConfirm.Checked = app.Default.QuitConfirm;
            chkHightLight.Checked = app.Default.HightLightEnable;
            chkComment.Checked = app.Default.CommentEnable;
            chkAutoLoad.Checked = app.Default.LoadFileEnable;
            nudFrameInterval.Value = app.Default.FrameInterval;
            chkSendDisplay.Checked = app.Default.SendDisplayEnable;
            chkTimeNewLine.Checked = app.Default.chkTimeNewLine;
            chkSend2File.Checked = app.Default.Send2FileEnable;
            chkSend2NewLine.Checked = app.Default.Send2NewLineEnable;

            // 显示
            // 方案1
            labReceFont1.Text = Font2Str(main_form.ReceFont1);
            labReceForeColor1.BackColor = main_form.ReceForeColor1;
            labReceForeColorValue1.Text = Color2Hex(main_form.ReceForeColor1);
            labReceBackColor1.BackColor = main_form.ReceBackColor1;
            labReceBackColorValue1.Text = Color2Hex(main_form.ReceBackColor1);

            labSendFont1.Text = Font2Str(main_form.SendFont1);
            labSendForeColor1.BackColor = main_form.SendForeColor1;
            labSendForeColorValue1.Text = Color2Hex(main_form.SendForeColor1);
            labSendBackColor1.BackColor = main_form.SendBackColor1;
            labSendBackColorValue1.Text = Color2Hex(main_form.SendBackColor1);
            // 方案2
            labReceFont2.Text = Font2Str(main_form.ReceFont2);
            labReceForeColor2.BackColor = main_form.ReceForeColor2;
            labReceForeColorValue2.Text = Color2Hex(main_form.ReceForeColor2);
            labReceBackColor2.BackColor = main_form.ReceBackColor2;
            labReceBackColorValue2.Text = Color2Hex(main_form.ReceBackColor2);

            labSendFont2.Text = Font2Str(main_form.SendFont2);
            labSendForeColor2.BackColor = main_form.SendForeColor2;
            labSendForeColorValue2.Text = Color2Hex(main_form.SendForeColor2);
            labSendBackColor2.BackColor = main_form.SendBackColor2;
            labSendBackColorValue2.Text = Color2Hex(main_form.SendBackColor2);

            //highlight
            txbRedRegex.Text = main_form.hl_red_regex_str;
            txbGreenRegex.Text = main_form.hl_green_regex_str;
            txbYellowRegex.Text = main_form.hl_yellow_regex_str;
            txbBlueRegex.Text = main_form.hl_blue_regex_str;
            txbMagentaRegex.Text = main_form.hl_magenta_regex_str;
            txbCyanRegex.Text = main_form.hl_cyan_regex_str;
            txbOrangeRegex.Text = main_form.hl_orange_regex_str;

            // 应用
            btnApply.Enabled = false;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            // 基本
            app.Default.CloseToTray = chkQuitToTray.Checked == true ? true : false;
            app.Default.MinToTray = chkMinToTray.Checked == true ? true : false;
            app.Default.QuitConfirm = chkQuitConfirm.Checked == true ? true : false;
            main_form.hight_light_enable = app.Default.HightLightEnable = chkHightLight.Checked == true ? true : false;
            main_form.comment_enable = app.Default.CommentEnable = chkComment.Checked == true ? true : false;
            app.Default.LoadFileEnable = chkAutoLoad.Checked == true ? true : false;
            main_form.frame_interval = app.Default.FrameInterval = (int)nudFrameInterval.Value;
            main_form.send_display_enable = app.Default.SendDisplayEnable = chkSendDisplay.Checked == true ? true : false;
            main_form.time_newline_enable = app.Default.chkTimeNewLine = chkTimeNewLine.Checked == true ? true : false;
            main_form.send_2_file_enable = app.Default.Send2FileEnable = chkSend2File.Checked == true ? true : false;
            main_form.send_2_newline_enable = app.Default.Send2NewLineEnable = chkSend2NewLine.Checked == true ? true : false;


            // 显示
            app.Default.ReceFont1 = main_form.ReceFont1 = tmp_rece_font1;
            app.Default.ReceForeColor1 = main_form.ReceForeColor1 = labReceForeColor1.BackColor;
            app.Default.ReceBackColor1 = main_form.ReceBackColor1 = labReceBackColor1.BackColor;

            app.Default.SendFont1 = main_form.SendFont1 = tmp_send_font1;
            app.Default.SendForeColor1 = main_form.SendForeColor1 = labSendForeColor1.BackColor;
            app.Default.SendBackColor1 = main_form.SendBackColor1 = labSendBackColor1.BackColor;

            app.Default.ReceFont2 = main_form.ReceFont2 = tmp_rece_font2;
            app.Default.ReceForeColor2 = main_form.ReceForeColor2 = labReceForeColor2.BackColor;
            app.Default.ReceBackColor2 = main_form.ReceBackColor2 = labReceBackColor2.BackColor; ;

            app.Default.SendFont2 = main_form.SendFont2 = tmp_send_font2;
            app.Default.SendForeColor2 = main_form.SendForeColor2 = labSendForeColor2.BackColor;
            app.Default.SendBackColor2 = main_form.SendBackColor2 = labSendBackColor2.BackColor;

            //new hightlight
            main_form.hl_red_regex_str = app.Default.HighLightRed = txbRedRegex.Text;
            main_form.hl_green_regex_str = app.Default.HighLightGreen = txbGreenRegex.Text;
            main_form.hl_yellow_regex_str = app.Default.HighLightYellow = txbYellowRegex.Text;
            main_form.hl_blue_regex_str = app.Default.HighLightBlue = txbBlueRegex.Text;
            main_form.hl_magenta_regex_str = app.Default.HighLightMagenta = txbMagentaRegex.Text;
            main_form.hl_cyan_regex_str = app.Default.HighLightCyan = txbCyanRegex.Text;
            main_form.hl_orange_regex_str = app.Default.HighLightOrange = txbOrangeRegex.Text;

            app.Default.Save();
            //.UpdateHightLight();
            // 应用
            btnApply.Enabled = false;

        }

        public string Color2Hex(Color s_color)
        {
            string d_color_str;
            d_color_str = "#" + /*s_color.A.ToString("x2") + */s_color.R.ToString("x2") + s_color.G.ToString("x2") + s_color.B.ToString("x2");
            return d_color_str.ToUpper();
        }

        private void chkQuitToTray_CheckedChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        private void chkMinToTray_CheckedChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        private void chkQuitConfirm_CheckedChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        private void chkHightLight_CheckedChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        private void chkComment_CheckedChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        private void chkAutoLoad_CheckedChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        private void nudFrameInterval_ValueChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        private void btnReceForeColor1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Color new_color = colorDialog1.Color;
                labReceForeColor1.BackColor = new_color;
                labReceForeColorValue1.Text = Color2Hex(new_color);
                btnApply.Enabled = true;
            }
        }

        string Font2Str(Font s_font)
        {
            string d_font_str = s_font.Name + "," + s_font.SizeInPoints + "pt";
            return d_font_str;
        }

        Font tmp_rece_font1 = new Font("Consolas", 10.5f, FontStyle.Regular);
        Font tmp_send_font1 = new Font("Consolas", 10.5f, FontStyle.Regular);
        Font tmp_rece_font2 = new Font("Consolas", 10.5f, FontStyle.Regular);
        Font tmp_send_font2 = new Font("Consolas", 10.5f, FontStyle.Regular);

        private void btnReceFont1_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                tmp_rece_font1 = fontDialog1.Font;
                labReceFont1.Text = Font2Str(tmp_rece_font1);
                btnApply.Enabled = true;
            }

            //MessageBox.Show(new_font.ToString() + "\nnew_font = " + Font2Str(new_font));
        }

        private void btnReceBackColor1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Color new_color = colorDialog1.Color;
                labReceBackColor1.BackColor = new_color;
                labReceBackColorValue1.Text = Color2Hex(new_color);
                btnApply.Enabled = true;
            }
        }

        private void btnSendForeColor1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Color new_color = colorDialog1.Color;
                labSendForeColor1.BackColor = new_color;
                labSendForeColorValue1.Text = Color2Hex(new_color);
                btnApply.Enabled = true;
            }
            
        }

        private void btnSendBackColor1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Color new_color = colorDialog1.Color;
                labSendBackColor1.BackColor = new_color;
                labSendBackColorValue1.Text = Color2Hex(new_color);
                btnApply.Enabled = true;
            }
        }

        private void btnReceForeColor2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Color new_color = colorDialog1.Color;
                labReceForeColor2.BackColor = new_color;
                labReceForeColorValue2.Text = Color2Hex(new_color);
                btnApply.Enabled = true;
            }
        }

        private void btnReceBackColor2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Color new_color = colorDialog1.Color;
                labReceBackColor2.BackColor = new_color;
                labReceBackColorValue2.Text = Color2Hex(new_color);
                btnApply.Enabled = true;
            }
        }

        private void btnSendForeColor2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Color new_color = colorDialog1.Color;
                labSendForeColor2.BackColor = new_color;
                labSendForeColorValue2.Text = Color2Hex(new_color);
                btnApply.Enabled = true;
            }
        }

        private void btnSendBackColor2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Color new_color = colorDialog1.Color;
                labSendBackColor2.BackColor = new_color;
                labSendBackColorValue2.Text = Color2Hex(new_color);
                btnApply.Enabled = true;
            }
        }

        private void btnReceFont2_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                tmp_rece_font2 = fontDialog1.Font;
                labReceFont2.Text = Font2Str(tmp_rece_font2);
                btnApply.Enabled = true;
            }
        }

        private void btnSendFont1_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                tmp_send_font1 = fontDialog1.Font;
                labSendFont1.Text = Font2Str(tmp_send_font1);
                btnApply.Enabled = true;
            }
        }

        private void btnSendFont2_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                tmp_send_font2 = fontDialog1.Font;
                labSendFont2.Text = Font2Str(tmp_send_font2);
                btnApply.Enabled = true;
            }
        }


        private void chkSendDisplay_CheckedChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        /// <summary>
        /// 恢复默认设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
            switch (tabOption.SelectedIndex)
            {
                case 0:
                    chkQuitToTray.Checked = false;
                    chkMinToTray.Checked = false;
                    chkQuitConfirm.Checked = true;
                    chkSendDisplay.Checked = false;
                    chkHightLight.Checked = true;
                    chkComment.Checked = false;
                    chkAutoLoad.Checked = false;
                    nudFrameInterval.Value = 20;
                    chkTimeNewLine.Checked = false;
                    chkSend2File.Checked = true;
                    chkSend2NewLine.Checked = false;
                    break;
            
                case 1:
                    tmp_rece_font1 = new Font("Consolas", 10.5f, FontStyle.Regular);
                    labReceFont1.Text = Font2Str(tmp_rece_font1);
                    labReceForeColor1.BackColor = ColorTranslator.FromHtml("#F8F8F2");
                    labReceBackColor1.BackColor = ColorTranslator.FromHtml("#272822");
                    labReceForeColorValue1.Text = "#F8F8F2";
                    labReceBackColorValue1.Text = "#272822";

                    tmp_rece_font2 = new Font("Consolas", 10.5f, FontStyle.Regular);
                    labReceFont2.Text = Font2Str(tmp_rece_font2);
                    labReceForeColor2.BackColor = ColorTranslator.FromHtml("#272822");
                    labReceBackColor2.BackColor = ColorTranslator.FromHtml("#F8F8F2");
                    labReceForeColorValue2.Text = "#272822";
                    labReceBackColorValue2.Text = "#F8F8F2";

                    tmp_send_font1 = new Font("Consolas", 10.5f, FontStyle.Regular);
                    labSendFont1.Text = Font2Str(tmp_send_font1);
                    labSendForeColor1.BackColor = ColorTranslator.FromHtml("#F8F8F2");
                    labSendBackColor1.BackColor = ColorTranslator.FromHtml("#272822");
                    labSendForeColorValue1.Text = "#F8F8F2";
                    labSendBackColorValue1.Text = "#272822";

                    tmp_send_font2 = new Font("Consolas", 10.5f, FontStyle.Regular);
                    labSendFont2.Text = Font2Str(tmp_send_font2);
                    labSendForeColor2.BackColor = ColorTranslator.FromHtml("#272822");
                    labSendBackColor2.BackColor = ColorTranslator.FromHtml("#F8F8F2");
                    labSendForeColorValue2.Text = "#272822";
                    labSendBackColorValue2.Text = "#F8F8F2";
                    break;
                case 2:
                    txbRedRegex.Text = @"([^A-Za-z_&-]((bad|wrong|incorrect|improper|invalid|unsupported|bad)( file| memory)? (descriptor|alloc(ation)?|addr(ess)?|owner(ship)?|arg(ument)?|param(eter)?|setting|length|filename)|not properly|improperly|(operation |connection |authentication |access |permission )?(denied|disallowed|not allowed|refused|problem|failed|failure|not permitted)|no [A-Za-z]+( [A-Za-z]+)? found|invalid|unsupported|not supported|seg(mentation )?fault|corruption|corrupted|corrupt|overflow|underrun|not ok|unimplemented|unsuccessfull|not implemented|permerrors?|fehlers?|errore|errors?|erreurs?|fejl|virhe|grea|erro|fel|\(ee\)|\(ni\))[^A-Za-z_-])";
                    txbGreenRegex.Text = @"([^A-Za-z_&-](ok|accepted|allowed|enabled|connected|erfolgreich|exitoso|successo|sucedido|successfully|successful|succeeded|success)[^A-Za-z_-])";
                    txbYellowRegex.Text = @"[^A-Za-z_&-](\[\-w[A-Za-z-]+\]|caught signal [0-9]+|cannot|(connection (to (remote host|[a-z0-9.]+) )?)?(closed|terminated|stopped|not responding)|exited|no more [A-Za-z] available|unexpected|(command |binary |file )?not found|(o)+ps|out of (space|memory)|low (memory|disk)|unknown|disabled|disconnected|deprecated|refused|disconnect(ion)?|advertencia|avvertimento|attention|warnings?|achtung|exclamation|alerts?|warnungs?|advarsel|pedwarn|aviso|varoitus|upozorenje|peringatan|uyari|varning|avertissement|\(ww\)|\(\?\?\)|could not|unable to)[^A-Za-z_-]";
                    txbBlueRegex.Text = @"[^A-Za-z_&-](debug|test)[^A-Za-z_-]";
                    txbMagentaRegex.Text = @"[^0-9A-Za-z_&-](localhost|([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-4])\.[0-9]+\.[0-9]+\.[0-9]+|null|none)[^0-9A-Za-z_-]";
                    txbCyanRegex.Text = @"[^A-Za-z_&-](last (failed )?login:|launching|checking|loading|creating|building|important|booting|starting|notice|informational|informationen|informazioni|informaction|oplysninger|informations?|info|informaction|informasi|note|\(ii\)|\(\!\!\))[^A-Za-z_-]";
                    txbOrangeRegex.Text = @"([0-1][0-9]|2[0-3]):([0-5][0-9]):([0-5][0-9]):([0-9][0-9][0-9]-(<|>))";
                    //MessageBox.Show("2");
                    break;
                default:
                    break;
            }
        }

        private void nudFrameInterval_Scroll(object sender, ScrollEventArgs e)
        {
            
        }

        private void chkTimeNewLine_CheckedChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        private void txbRedRegex_TextChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        private void txbGreenRegex_TextChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        private void txbYellowRegex_TextChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        private void txbBlueRegex_TextChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        private void txbMagentaRegex_TextChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        private void txbCyanRegex_TextChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        private void txbOrangeRegex_TextChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        private void chkSend2File_CheckedChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        private void chkSend2NewLine_CheckedChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }
    }
}
