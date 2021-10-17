using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace O_ComTool_Pro
{
    
    public class UpdateHelper
    {
        public struct check_value
        {
            public bool valid;
            public string version;
            public string link;
            public string feature;
        }

        public static check_value check_update(string url)
        {
            check_value ret_value;
            ret_value.version = "";
            ret_value.link = "";
            ret_value.feature = "";
            ret_value.valid = false;
            
            try
            {
                XmlDocument ver_xml = new XmlDocument();
                ver_xml.Load(url + "/check_version.xml");
                XmlNode ver = ver_xml.SelectSingleNode("checkupdate");
                foreach (XmlNode node in ver)
                {
                    XmlNode verid = ver_xml.SelectSingleNode("checkupdate/version");
                    if (node.Name == "version")
                    {
                        ret_value.version = node.InnerText;
                    }
                    if (node.Name == "link")
                    {
                        ret_value.link = node.InnerText;
                    }
                    if (verid.InnerText == Application.ProductVersion.Substring(0, 5))
                    {
                        ret_value.feature = "已经是最新版本啦！\r\n";
                    }
                    if (node.Name == "feature")
                    {
                        ret_value.feature += node.InnerText;
                    }
                }
                ret_value.valid = true;
                return ret_value;
            }
            catch
            {
                return ret_value;
            }
        }
    }
}
