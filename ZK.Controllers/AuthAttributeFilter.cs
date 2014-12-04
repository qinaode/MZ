using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

namespace ZK.Controllers
{
    [ZKAuthAttributeFilter(purl = "/")]
    public class ZKAuthAttributeFilter : ActionFilterAttribute
    {
        public string purl { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (filterContext.HttpContext.Session["uid"] == null)
            {
                filterContext.Result = new RedirectResult("/account/login/?pu=" + ZK.Common.UrlOper.getUrl().Replace("http://" + ConfigurationManager.AppSettings["url"], ""));
            }
            else if (filterContext.HttpContext.Session["uid"].ToString() == "")
            {
                filterContext.Result = new RedirectResult("/account/login/?pu=" + ZK.Common.UrlOper.getUrl().Replace("http://" + ConfigurationManager.AppSettings["url"], ""));
            }


        }
        /// <summary>
        /// 获取网站标题
        /// </summary>
        /// <returns></returns>
        public static string getTitle()
        {

            string XMLFilePath = HttpContext.Current.Request.PhysicalApplicationPath + ZK.Common.ModelSettings.BH_SysSettingXMLPath;
            string webtitle = Common.XMLHelper.GetXmlAttribute(XMLFilePath, "Settings/WebTitle", "value").Value;
            return webtitle;

        }
    }
}
