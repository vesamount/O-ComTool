namespace O_ComTool_Pro
{
    partial class CheckUpdate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckUpdate));
            this.labMessage = new System.Windows.Forms.Label();
            this.btnGoToUpdate = new System.Windows.Forms.Button();
            this.btnSkipCurrentVersion = new System.Windows.Forms.Button();
            this.labLatestVersion = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labMessage
            // 
            this.labMessage.AutoSize = true;
            this.labMessage.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labMessage.Location = new System.Drawing.Point(102, 13);
            this.labMessage.Name = "labMessage";
            this.labMessage.Size = new System.Drawing.Size(172, 27);
            this.labMessage.TabIndex = 0;
            this.labMessage.Text = "有新版本更新啦！";
            // 
            // btnGoToUpdate
            // 
            this.btnGoToUpdate.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnGoToUpdate.Location = new System.Drawing.Point(41, 80);
            this.btnGoToUpdate.Name = "btnGoToUpdate";
            this.btnGoToUpdate.Size = new System.Drawing.Size(110, 35);
            this.btnGoToUpdate.TabIndex = 1;
            this.btnGoToUpdate.Text = "前往更新";
            this.btnGoToUpdate.UseVisualStyleBackColor = true;
            this.btnGoToUpdate.Click += new System.EventHandler(this.btnGoToUpdate_Click);
            // 
            // btnSkipCurrentVersion
            // 
            this.btnSkipCurrentVersion.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSkipCurrentVersion.Location = new System.Drawing.Point(239, 80);
            this.btnSkipCurrentVersion.Name = "btnSkipCurrentVersion";
            this.btnSkipCurrentVersion.Size = new System.Drawing.Size(110, 35);
            this.btnSkipCurrentVersion.TabIndex = 2;
            this.btnSkipCurrentVersion.Text = "跳过当前版本";
            this.btnSkipCurrentVersion.UseVisualStyleBackColor = true;
            this.btnSkipCurrentVersion.Click += new System.EventHandler(this.btnSkipCurrentVersion_Click);
            // 
            // labLatestVersion
            // 
            this.labLatestVersion.AutoSize = true;
            this.labLatestVersion.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labLatestVersion.Location = new System.Drawing.Point(123, 47);
            this.labLatestVersion.Name = "labLatestVersion";
            this.labLatestVersion.Size = new System.Drawing.Size(124, 19);
            this.labLatestVersion.TabIndex = 3;
            this.labLatestVersion.Text = "最新版本：V2.0.1";
            // 
            // CheckUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(384, 134);
            this.Controls.Add(this.labLatestVersion);
            this.Controls.Add(this.btnSkipCurrentVersion);
            this.Controls.Add(this.btnGoToUpdate);
            this.Controls.Add(this.labMessage);
            this.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CheckUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "O-ComTool 更新提示";
            this.Load += new System.EventHandler(this.CheckUpdate_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labMessage;
        private System.Windows.Forms.Button btnGoToUpdate;
        private System.Windows.Forms.Button btnSkipCurrentVersion;
        private System.Windows.Forms.Label labLatestVersion;
    }
}