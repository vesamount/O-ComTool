namespace O_ComTool_Pro
{
    partial class Update
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Update));
            this.labCurrentVersion = new System.Windows.Forms.Label();
            this.labLatestVersion = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txbInformation = new System.Windows.Forms.TextBox();
            this.btnSkipCurrentVersion = new System.Windows.Forms.Button();
            this.btnGoToUpdate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labCurrentVersion
            // 
            this.labCurrentVersion.AutoSize = true;
            this.labCurrentVersion.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labCurrentVersion.Location = new System.Drawing.Point(12, 10);
            this.labCurrentVersion.Name = "labCurrentVersion";
            this.labCurrentVersion.Size = new System.Drawing.Size(90, 22);
            this.labCurrentVersion.TabIndex = 0;
            this.labCurrentVersion.Text = "当前版本：";
            // 
            // labLatestVersion
            // 
            this.labLatestVersion.AutoSize = true;
            this.labLatestVersion.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labLatestVersion.Location = new System.Drawing.Point(12, 37);
            this.labLatestVersion.Name = "labLatestVersion";
            this.labLatestVersion.Size = new System.Drawing.Size(169, 22);
            this.labLatestVersion.TabIndex = 1;
            this.labLatestVersion.Text = "最新版本：正在获取...";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(12, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 22);
            this.label3.TabIndex = 2;
            this.label3.Text = "更新说明";
            // 
            // txbInformation
            // 
            this.txbInformation.BackColor = System.Drawing.Color.White;
            this.txbInformation.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txbInformation.Location = new System.Drawing.Point(2, 96);
            this.txbInformation.Multiline = true;
            this.txbInformation.Name = "txbInformation";
            this.txbInformation.ReadOnly = true;
            this.txbInformation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txbInformation.Size = new System.Drawing.Size(463, 128);
            this.txbInformation.TabIndex = 3;
            // 
            // btnSkipCurrentVersion
            // 
            this.btnSkipCurrentVersion.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSkipCurrentVersion.Location = new System.Drawing.Point(318, 234);
            this.btnSkipCurrentVersion.Name = "btnSkipCurrentVersion";
            this.btnSkipCurrentVersion.Size = new System.Drawing.Size(110, 35);
            this.btnSkipCurrentVersion.TabIndex = 7;
            this.btnSkipCurrentVersion.Text = "跳过当前版本";
            this.btnSkipCurrentVersion.UseVisualStyleBackColor = true;
            this.btnSkipCurrentVersion.Click += new System.EventHandler(this.btnSkipCurrentVersion_Click);
            // 
            // btnGoToUpdate
            // 
            this.btnGoToUpdate.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnGoToUpdate.Location = new System.Drawing.Point(33, 234);
            this.btnGoToUpdate.Name = "btnGoToUpdate";
            this.btnGoToUpdate.Size = new System.Drawing.Size(110, 35);
            this.btnGoToUpdate.TabIndex = 6;
            this.btnGoToUpdate.Text = "前往更新";
            this.btnGoToUpdate.UseVisualStyleBackColor = true;
            this.btnGoToUpdate.Click += new System.EventHandler(this.btnGoToUpdate_Click);
            // 
            // Update
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(466, 281);
            this.Controls.Add(this.btnSkipCurrentVersion);
            this.Controls.Add(this.btnGoToUpdate);
            this.Controls.Add(this.txbInformation);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labLatestVersion);
            this.Controls.Add(this.labCurrentVersion);
            this.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Update";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "O-ComTool 软件更新";
            this.Load += new System.EventHandler(this.Update_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labCurrentVersion;
        private System.Windows.Forms.Label labLatestVersion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txbInformation;
        private System.Windows.Forms.Button btnSkipCurrentVersion;
        private System.Windows.Forms.Button btnGoToUpdate;
    }
}