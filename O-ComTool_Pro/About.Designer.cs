namespace O_ComTool_Pro
{
    partial class About
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.Lab_Contact = new System.Windows.Forms.Label();
            this.Lab_Author = new System.Windows.Forms.Label();
            this.Lab_Version = new System.Windows.Forms.Label();
            this.Lab_AppName = new System.Windows.Forms.Label();
            this.Pic_Logo = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_Logo)).BeginInit();
            this.SuspendLayout();
            // 
            // Lab_Contact
            // 
            this.Lab_Contact.AutoSize = true;
            this.Lab_Contact.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lab_Contact.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.Lab_Contact.Location = new System.Drawing.Point(148, 130);
            this.Lab_Contact.Name = "Lab_Contact";
            this.Lab_Contact.Size = new System.Drawing.Size(248, 22);
            this.Lab_Contact.TabIndex = 22;
            this.Lab_Contact.Text = "联系：chinafjnpoqc@163.com";
            // 
            // Lab_Author
            // 
            this.Lab_Author.AutoSize = true;
            this.Lab_Author.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lab_Author.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.Lab_Author.Location = new System.Drawing.Point(148, 104);
            this.Lab_Author.Name = "Lab_Author";
            this.Lab_Author.Size = new System.Drawing.Size(141, 22);
            this.Lab_Author.TabIndex = 21;
            this.Lab_Author.Text = "作者：Gustav Ou";
            // 
            // Lab_Version
            // 
            this.Lab_Version.AutoSize = true;
            this.Lab_Version.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lab_Version.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.Lab_Version.Location = new System.Drawing.Point(148, 78);
            this.Lab_Version.Name = "Lab_Version";
            this.Lab_Version.Size = new System.Drawing.Size(58, 22);
            this.Lab_Version.TabIndex = 20;
            this.Lab_Version.Text = "版本：";
            // 
            // Lab_AppName
            // 
            this.Lab_AppName.AutoSize = true;
            this.Lab_AppName.Font = new System.Drawing.Font("微软雅黑", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lab_AppName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.Lab_AppName.Location = new System.Drawing.Point(140, 9);
            this.Lab_AppName.Name = "Lab_AppName";
            this.Lab_AppName.Size = new System.Drawing.Size(307, 64);
            this.Lab_AppName.TabIndex = 19;
            this.Lab_AppName.Text = "O-ComTool";
            // 
            // Pic_Logo
            // 
            this.Pic_Logo.Image = global::O_ComTool_Pro.Properties.Resources.Logo;
            this.Pic_Logo.Location = new System.Drawing.Point(19, 28);
            this.Pic_Logo.Name = "Pic_Logo";
            this.Pic_Logo.Size = new System.Drawing.Size(115, 116);
            this.Pic_Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Pic_Logo.TabIndex = 18;
            this.Pic_Logo.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.Location = new System.Drawing.Point(19, 167);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(410, 84);
            this.textBox1.TabIndex = 23;
            this.textBox1.Text = "    O-ComTool是一款简单易用的串口调试工具，便捷、舒适是它的核心，O-ComTool指在以最顺手的方式解决串口调试过程中出现的问题，希望这款软件能够成" +
    "为您的好帮手。\r\n";
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(452, 263);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Lab_Contact);
            this.Controls.Add(this.Lab_Author);
            this.Controls.Add(this.Lab_Version);
            this.Controls.Add(this.Lab_AppName);
            this.Controls.Add(this.Pic_Logo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "About";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "O-ComTool 关于";
            this.Load += new System.EventHandler(this.About_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Pic_Logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Lab_Contact;
        private System.Windows.Forms.Label Lab_Author;
        private System.Windows.Forms.Label Lab_Version;
        private System.Windows.Forms.Label Lab_AppName;
        private System.Windows.Forms.PictureBox Pic_Logo;
        private System.Windows.Forms.TextBox textBox1;
    }
}