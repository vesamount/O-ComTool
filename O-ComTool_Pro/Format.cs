using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace O_ComTool_Pro
{
    public partial class Format : Form
    {
        public Format()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string txt_dest_str = "";
            string txt_src_clr = System.Text.RegularExpressions.Regex.Replace(txtSrc.Text, "0x|0X|[, Hh]", "");

            MatchCollection mc = Regex.Matches(txt_src_clr, @"(?i)[\da-f]{2}");//正则获取符合十六进制规则的数据，如果数据错误则跳过该数据

            foreach (Match m in mc)//遍历所有mc，并将其转换成十六进制
            {
                if (chk0x.Checked)
                {
                    txt_dest_str += "0x";
                }
                txt_dest_str += m.Value;
                if (chkComma.Checked)
                {
                    txt_dest_str += ",";
                }
                if (ChkPoint.Checked)
                {
                    txt_dest_str += ".";
                }
                if (chkH.Checked)
                {
                    txt_dest_str += "H";
                }
                if (chkSpace.Checked)
                {
                    txt_dest_str += " ";
                }
            }
            txtDest.Text = txt_dest_str;
        }
    }
}
