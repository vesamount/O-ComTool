using System.Text;
using System.Xml;

namespace O_ComTool.Core.Update
{
    /**
     * @brief 版本检查结果
     */
    public readonly record struct UpdateInfo(bool Valid, string Version, string Link, string Feature);

    /**
     * @brief 从 ifreehub 的 check_version.xml 读取新版本
     */
    public static class UpdateChecker
    {
        public const string ServerUrl = "http://www.ifreehub.com/octservice";

        /**
         * @brief 拉取并比较版本
         * @param productVersion: 当前产品版本，例如 3.0.0
         * @retval 检查结果；网络失败时 Valid 为 false
         */
        public static UpdateInfo Check(string productVersion)
        {
            try
            {
                var document = new XmlDocument();
                document.Load(ServerUrl + "/check_version.xml");
                XmlNode? root = document.SelectSingleNode("checkupdate");
                string version = string.Empty;
                string link = string.Empty;
                var feature = new StringBuilder();
                string current = productVersion.Length >= 5 ? productVersion[..5] : productVersion;
                if (root != null)
                {
                    foreach (XmlNode node in root.ChildNodes)
                    {
                        if (node.Name == "version")
                        {
                            version = node.InnerText;
                        }
                        else if (node.Name == "link")
                        {
                            link = node.InnerText;
                        }
                        else if (node.Name == "feature")
                        {
                            feature.Append(node.InnerText);
                        }
                    }
                }

                if (version == current)
                {
                    feature.Insert(0, "已经是最新版本啦！\r\n");
                }

                return new UpdateInfo(true, version, link, feature.ToString());
            }
            catch (Exception)
            {
                return new UpdateInfo(false, string.Empty, string.Empty, string.Empty);
            }
        }
    }
}
