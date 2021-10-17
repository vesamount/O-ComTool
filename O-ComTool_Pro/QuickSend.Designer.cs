namespace O_ComTool_Pro
{
    partial class QuickSend
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtData = new System.Windows.Forms.TextBox();
            this.labName = new System.Windows.Forms.Label();
            this.labIndex = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.picDelete = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picDelete)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSend.Location = new System.Drawing.Point(509, 5);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(41, 21);
            this.btnSend.TabIndex = 20;
            this.btnSend.TabStop = false;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(26, 5);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(61, 21);
            this.txtName.TabIndex = 19;
            this.txtName.TabStop = false;
            this.txtName.Visible = false;
            this.txtName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtName_KeyPress);
            // 
            // txtData
            // 
            this.txtData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtData.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtData.Location = new System.Drawing.Point(95, 4);
            this.txtData.Name = "txtData";
            this.txtData.Size = new System.Drawing.Size(408, 22);
            this.txtData.TabIndex = 16;
            this.txtData.TabStop = false;
            this.txtData.Tag = "";
            // 
            // labName
            // 
            this.labName.AutoEllipsis = true;
            this.labName.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labName.Location = new System.Drawing.Point(24, 8);
            this.labName.Name = "labName";
            this.labName.Size = new System.Drawing.Size(65, 14);
            this.labName.TabIndex = 22;
            this.labName.Text = "Item_name";
            this.labName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labName.Click += new System.EventHandler(this.labName_Click);
            this.labName.DoubleClick += new System.EventHandler(this.labName_DoubleClick);
            // 
            // labIndex
            // 
            this.labIndex.AutoSize = true;
            this.labIndex.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labIndex.Location = new System.Drawing.Point(3, 9);
            this.labIndex.Name = "labIndex";
            this.labIndex.Size = new System.Drawing.Size(23, 12);
            this.labIndex.TabIndex = 17;
            this.labIndex.Text = "99.";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(27, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(60, 1);
            this.panel1.TabIndex = 23;
            // 
            // picDelete
            // 
            this.picDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picDelete.Image = global::O_ComTool_Pro.Properties.Resources.Sub_48px;
            this.picDelete.Location = new System.Drawing.Point(555, 3);
            this.picDelete.Name = "picDelete";
            this.picDelete.Size = new System.Drawing.Size(25, 25);
            this.picDelete.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picDelete.TabIndex = 21;
            this.picDelete.TabStop = false;
            this.picDelete.Tag = "1";
            this.picDelete.Visible = false;
            this.picDelete.Click += new System.EventHandler(this.picDelete_Click);
            this.picDelete.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picDelete_MouseDown);
            this.picDelete.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picDelete_MouseUp);
            // 
            // QuickSend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtData);
            this.Controls.Add(this.picDelete);
            this.Controls.Add(this.labName);
            this.Controls.Add(this.labIndex);
            this.Controls.Add(this.panel1);
            this.Name = "QuickSend";
            this.Size = new System.Drawing.Size(583, 30);
            this.Load += new System.EventHandler(this.QuickSend_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picDelete)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtData;
        private System.Windows.Forms.PictureBox picDelete;
        private System.Windows.Forms.Label labName;
        private System.Windows.Forms.Label labIndex;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel1;
    }
}
