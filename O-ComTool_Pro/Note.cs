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
    public partial class Note : Form
    {
        MainForm main_form;//声明一个MainForm对象
        public Note(MainForm form)
        {
            this.main_form = form;//将MainForm传过来的Form赋值到声明的main_form
            InitializeComponent();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "")
            {
                main_form.Text = "O-ComTool V" + Application.ProductVersion.Substring(0, 5) + " [" + txtName.Text + "]";
            }
            else
            {
                main_form.Text = "O-ComTool V" + Application.ProductVersion.Substring(0, 5);
            }
            this.Close();
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))//回车响应
            {
                btnApply_Click(sender, e);
                e.Handled = true;
            }
        }

        private void Note_Load(object sender, EventArgs e)
        {

        }
    }
}
