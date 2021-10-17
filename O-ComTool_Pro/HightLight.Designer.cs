namespace O_ComTool_Pro
{
    partial class HightLight
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HightLight));
            this.labKey = new System.Windows.Forms.Label();
            this.txbKey = new System.Windows.Forms.TextBox();
            this.labColor = new System.Windows.Forms.Label();
            this.btnChoseColor = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.SuspendLayout();
            // 
            // labKey
            // 
            this.labKey.AutoSize = true;
            this.labKey.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labKey.Location = new System.Drawing.Point(6, 12);
            this.labKey.Name = "labKey";
            this.labKey.Size = new System.Drawing.Size(65, 20);
            this.labKey.TabIndex = 0;
            this.labKey.Text = "关键字：";
            // 
            // txbKey
            // 
            this.txbKey.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txbKey.Location = new System.Drawing.Point(67, 9);
            this.txbKey.Name = "txbKey";
            this.txbKey.Size = new System.Drawing.Size(100, 26);
            this.txbKey.TabIndex = 1;
            // 
            // labColor
            // 
            this.labColor.BackColor = System.Drawing.Color.Lime;
            this.labColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labColor.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labColor.Location = new System.Drawing.Point(176, 9);
            this.labColor.Name = "labColor";
            this.labColor.Size = new System.Drawing.Size(26, 26);
            this.labColor.TabIndex = 8;
            // 
            // btnChoseColor
            // 
            this.btnChoseColor.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnChoseColor.Location = new System.Drawing.Point(208, 8);
            this.btnChoseColor.Name = "btnChoseColor";
            this.btnChoseColor.Size = new System.Drawing.Size(29, 29);
            this.btnChoseColor.TabIndex = 9;
            this.btnChoseColor.Text = "...";
            this.btnChoseColor.UseVisualStyleBackColor = true;
            this.btnChoseColor.Click += new System.EventHandler(this.btnChoseColor_Click);
            // 
            // btnOk
            // 
            this.btnOk.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOk.Location = new System.Drawing.Point(81, 43);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 29);
            this.btnOk.TabIndex = 10;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Location = new System.Drawing.Point(162, 43);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 29);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // HightLight
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(244, 80);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnChoseColor);
            this.Controls.Add(this.labColor);
            this.Controls.Add(this.txbKey);
            this.Controls.Add(this.labKey);
            this.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HightLight";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "O-ComTool 高亮";
            this.Load += new System.EventHandler(this.HightLight_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labKey;
        private System.Windows.Forms.TextBox txbKey;
        private System.Windows.Forms.Label labColor;
        private System.Windows.Forms.Button btnChoseColor;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ColorDialog colorDialog1;
    }
}