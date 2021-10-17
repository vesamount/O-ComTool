using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace O_ComTool_Pro
{
    public partial class QuickSend : UserControl
    {
        public QuickSend()
        {
            InitializeComponent();
        }

        private void labName_Click(object sender, EventArgs e)
        {

        }

        private void QuickSend_Load(object sender, EventArgs e)
        {

        }

        private void labName_DoubleClick(object sender, EventArgs e)
        {
            timer1.Start();
            labName.Visible = false;
            txtName.Text = labName.Text;
            txtName.Visible = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (((this.PointToClient(Cursor.Position).X < (txtName.Location.X))
                || (this.PointToClient(Cursor.Position).X > (txtName.Location.X + txtName.Width))
                || (this.PointToClient(Cursor.Position).Y < (txtName.Location.Y))
                || (this.PointToClient(Cursor.Position).Y > (txtName.Location.Y + txtName.Height)))
                && (Control.MouseButtons == MouseButtons.Left))
            {
                timer1.Stop();
                labName.Text = txtName.Text;
                txtName.Visible = false;
                labName.Visible = true;
                //MessageBox.Show("MousePosition（" + this.PointToClient(Cursor.Position).X + "," + this.PointToClient(Cursor.Position).Y + ")\n" +
                //                "txtName.Location (" + txtName.Location.X + "," + txtName.Location.Y + ")\n" +
                //                "this.Location (" + this.Location.X + "," + this.Location.Y + ")\n" +
                //                "min_point(" + (txtName.Location.X) + "," + (txtName.Location.Y) + ")\n" +
                //                "max_point(" + (txtName.Location.X + txtName.Width) + "," + (txtName.Location.Y + txtName.Height) + ")");
            }
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))//回车响应
            {
                e.Handled = true;
                labName.Text = txtName.Text;
                txtName.Visible = false;
                labName.Visible = true;
            }
        }
        public short Index
        {
            //get { return labIndex.Text; }
            set { labIndex.Text = value.ToString().PadLeft(2,'0') + "."; }
        }

        public string ItemName
        {
            get { return labName.Text; }
            set { labName.Text = value; }
        }

        public string Data
        {
            get { return txtData.Text; }
            set { txtData.Text = value; }
        }

        public bool DelVisible
        {
            get { return picDelete.Visible; }
            set { picDelete.Visible = value; }
        }


        public delegate void BtnClickHandle(object sender, EventArgs e); //定义事件

        public event BtnClickHandle SendClicked;
        private void btnSend_Click(object sender, EventArgs e)
        {
            if (SendClicked != null)
                SendClicked(sender, new EventArgs());//把按钮自身作为参数传递
        }

        public event BtnClickHandle DelClicked;
        private void picDelete_Click(object sender, EventArgs e)
        {
            this.Focus();
            if (DelClicked != null)
                DelClicked(sender, new EventArgs());//把按钮自身作为参数传递
        }

        private void picDelete_MouseDown(object sender, MouseEventArgs e)
        {
            picDelete.Image = Properties.Resources.SubDown_52px;
        }

        private void picDelete_MouseUp(object sender, MouseEventArgs e)
        {
            picDelete.Image = Properties.Resources.Sub_48px;
        }

    }
}
