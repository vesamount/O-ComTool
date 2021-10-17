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
    public partial class Update : Form
    {
        string update_url = "";
        string server_url = "http://www.ifreehub.com/octservice/";

        UpdateHelper.check_value ret_update;

        public Update()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void Update_Load(object sender, EventArgs e)
        {
            labCurrentVersion.Text = "当前版本：V" + Application.ProductVersion.Substring(0, 5);
            Task check_version = new Task(() =>
            {
                ret_update = UpdateHelper.check_update(server_url);
                if (ret_update.valid == true)
                {
                    labLatestVersion.Text = "最新版本：V" + ret_update.version;
                    update_url = ret_update.link;
                    txbInformation.Text = ret_update.feature;
                    if (ret_update.version != Application.ProductVersion.Substring(0, 5))
                    {
                        btnGoToUpdate.Enabled = true;
                        btnSkipCurrentVersion.Enabled = true;
                    }
                    app.Default.LastCheckTime = DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
                    app.Default.Save();
                }
                else
                {
                    labLatestVersion.Text = "最新版本：啊哦！更新检查失败！";
                    txbInformation.Text = "可能的原因：\r\n1.当前网络状态异常；\r\n2.打开的姿势不对；\r\n3.作者的服务器断供；\r\n4.其他未知原因。";
                }

            });
            check_version.Start();
        }

        private void btnGoToUpdate_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(update_url);
        }

        private void btnSkipCurrentVersion_Click(object sender, EventArgs e)
        {
            app.Default.SkipVersion = ret_update.version;
            app.Default.Save();
        }


    }
}
