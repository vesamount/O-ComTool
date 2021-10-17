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
    public partial class CheckUpdate : Form
    {
        string update_url = "";
        public CheckUpdate()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void CheckUpdate_Load(object sender, EventArgs e)
        {
            labLatestVersion.Text = "最新版本: V" + MainForm.check_version_value.version;
            update_url = MainForm.check_version_value.link;
        }

        private void btnGoToUpdate_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(update_url);
            this.Close();
        }

        private void btnSkipCurrentVersion_Click(object sender, EventArgs e)
        {
            app.Default.SkipVersion = MainForm.check_version_value.version;
            app.Default.Save();
            this.Close();
        }


    }
}
