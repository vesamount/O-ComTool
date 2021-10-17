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
    public partial class HightLight : Form
    {
        Option option_form;
        public HightLight(Option form)
        {
            InitializeComponent();
            option_form = form;
        }

        private void HightLight_Load(object sender, EventArgs e)
        {
            option_form.hl_valid = false;
            if (option_form.hl_mode == 2)
            {
                //MessageBox.Show("hl_mode2");
                txbKey.Text = option_form.hl_key;
                labColor.BackColor = System.Drawing.ColorTranslator.FromHtml(option_form.hl_color);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txbKey.Text != "")
            {
                option_form.hl_valid = true;
                option_form.hl_key = txbKey.Text;
                option_form.hl_color = option_form.Color2Hex(labColor.BackColor);
                this.Close();
            }
            else 
            {
                MessageBox.Show("请输入关键字！","O-ComTool 错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btnChoseColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Color new_color = colorDialog1.Color;
                labColor.BackColor = new_color;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            option_form.hl_valid = false;
            this.Close();
        }
    }
}
