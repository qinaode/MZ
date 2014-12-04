using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Data;
using ZK.Common;

namespace ZK.MControllers
{
    public class MoreController : Controller
    {
        /// <summary>
        ///  获取版本
        /// </summary>
        /// <param name="sys">sys </param>
        /// <returns></returns>
        public string VersionUpdateJson()
        {
            string jcbstr = Request["jcb"];
            string strUrlJson = "";
            string sys = Request["sys"];
            string versionNum = Request["versionId"];
            versionNum = "";
            string filename = "";
            string xmlpath = "VersionUpdate/";
            if (sys == "ios")
            {
                filename = "IOSVersion.xml";
            }
            else
            {
                filename = "AndroidVersion.xml";
            }
            xmlpath = Server.MapPath("~") + xmlpath + filename;
            string versionId = XMLHelper.GetXmlAttribute(xmlpath, "results/version", "value").Value.ToString();
            if (versionId != versionNum)
            {
                strUrlJson = XMLHelper.GetXmlAttribute(xmlpath, "results/updateFileUrl", "value").Value.ToString();
            }
            else
            {
                strUrlJson = "0";//当前版本已是最新版本！
            }
            return jcbstr + "({url:\"" + strUrlJson + "\"})"; 
        }
    }
}
