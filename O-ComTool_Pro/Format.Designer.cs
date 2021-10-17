namespace O_ComTool_Pro
{
    partial class Format
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Format));
            this.chkH = new System.Windows.Forms.CheckBox();
            this.ChkPoint = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.chkSpace = new System.Windows.Forms.CheckBox();
            this.chkComma = new System.Windows.Forms.CheckBox();
            this.chk0x = new System.Windows.Forms.CheckBox();
            this.txtDest = new System.Windows.Forms.TextBox();
            this.txtSrc = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // chkH
            // 
            this.chkH.AutoSize = true;
            this.chkH.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkH.Location = new System.Drawing.Point(228, 96);
            this.chkH.Name = "chkH";
            this.chkH.Size = new System.Drawing.Size(51, 24);
            this.chkH.TabIndex = 17;
            this.chkH.Text = "\"H\"";
            this.chkH.UseVisualStyleBackColor = true;
            // 
            // ChkPoint
            // 
            this.ChkPoint.AutoSize = true;
            this.ChkPoint.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ChkPoint.Location = new System.Drawing.Point(174, 96);
            this.ChkPoint.Name = "ChkPoint";
            this.ChkPoint.Size = new System.Drawing.Size(43, 24);
            this.ChkPoint.TabIndex = 16;
            this.ChkPoint.Text = "\".\"";
            this.ChkPoint.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(429, 95);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 27);
            this.button1.TabIndex = 15;
            this.button1.Text = "格式化";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(3, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 20);
            this.label1.TabIndex = 14;
            this.label1.Text = "选项：";
            // 
            // chkSpace
            // 
            this.chkSpace.AutoSize = true;
            this.chkSpace.Checked = true;
            this.chkSpace.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSpace.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkSpace.Location = new System.Drawing.Point(282, 96);
            this.chkSpace.Name = "chkSpace";
            this.chkSpace.Size = new System.Drawing.Size(68, 24);
            this.chkSpace.TabIndex = 13;
            this.chkSpace.Text = "\"空格\"";
            this.chkSpace.UseVisualStyleBackColor = true;
            // 
            // chkComma
            // 
            this.chkComma.AutoSize = true;
            this.chkComma.Checked = true;
            this.chkComma.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkComma.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkComma.Location = new System.Drawing.Point(120, 96);
            this.chkComma.Name = "chkComma";
            this.chkComma.Size = new System.Drawing.Size(43, 24);
            this.chkComma.TabIndex = 12;
            this.chkComma.Text = "\",\"";
            this.chkComma.UseVisualStyleBackColor = true;
            // 
            // chk0x
            // 
            this.chk0x.AutoSize = true;
            this.chk0x.Checked = true;
            this.chk0x.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk0x.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chk0x.Location = new System.Drawing.Point(60, 96);
            this.chk0x.Name = "chk0x";
            this.chk0x.Size = new System.Drawing.Size(55, 24);
            this.chk0x.TabIndex = 11;
            this.chk0x.Text = "\"0x\"";
            this.chk0x.UseVisualStyleBackColor = true;
            // 
            // txtDest
            // 
            this.txtDest.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtDest.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDest.Location = new System.Drawing.Point(0, 128);
            this.txtDest.Multiline = true;
            this.txtDest.Name = "txtDest";
            this.txtDest.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDest.Size = new System.Drawing.Size(506, 90);
            this.txtDest.TabIndex = 10;
            // 
            // txtSrc
            // 
            this.txtSrc.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtSrc.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSrc.Location = new System.Drawing.Point(0, 0);
            this.txtSrc.Multiline = true;
            this.txtSrc.Name = "txtSrc";
            this.txtSrc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSrc.Size = new System.Drawing.Size(506, 90);
            this.txtSrc.TabIndex = 9;
            // 
            // Format
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(506, 218);
            this.Controls.Add(this.chkH);
            this.Controls.Add(this.ChkPoint);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkSpace);
            this.Controls.Add(this.chkComma);
            this.Controls.Add(this.chk0x);
            this.Controls.Add(this.txtDest);
            this.Controls.Add(this.txtSrc);
            this.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Format";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "O-ComTool 报文格式化";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkH;
        private System.Windows.Forms.CheckBox ChkPoint;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkSpace;
        private System.Windows.Forms.CheckBox chkComma;
        private System.Windows.Forms.CheckBox chk0x;
        private System.Windows.Forms.TextBox txtDest;
        private System.Windows.Forms.TextBox txtSrc;
    }
}