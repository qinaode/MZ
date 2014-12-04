using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace ZK.Common
{
    public class WebHint
    {
        /// <summary>
        /// 页面错误提示信息
        /// </summary>
        /// <param name="ErrMsg">错误信息</param>
        /// <param name="Url">返回管理员地址  默认可以填写:""或"0"</param>
        /// 更新时间2007-3-7
        static public void ShowError(string ErrMsg, string Url, bool returnUrl)
        {
            PageRender(ErrMsg, Url, false, returnUrl);
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="StrUrl"></param>
        /// <returns></returns>
        static private string UserUrl(string StrUrl)
        {
            if (StrUrl.Trim() != string.Empty && StrUrl.Trim().Length > 5)
            {
                StrUrl = "<a href=\"" + StrUrl + "\"><font color=\"red\">返回管理</font></a>";
            }
            return StrUrl;
        }

        /// <summary>
        /// 页面操作成功提示信息
        /// </summary>
        /// <param name="RightMsg">操作成功信息</param>
        /// <param name="Url">返回管理员地址</param>
        /// 更新时间2007-3-7
        static internal void ShowRight(string RightMsg, string Url, bool returnUrl)
        {
            PageRender(RightMsg, Url, true, returnUrl);
        }
        static internal void PageRender(string Msg, string Url, bool Succeed, bool returnUrl)
        {
            string cssDir = "/images/";
            string STitle = "操作结果!";
            string ReUrlStr = "";
            string _tmp = "<img src=\"" + cssDir + "success.gif\" border=\"0\">";
            string SCaption = "恭喜！操作成功";
            if (!Succeed)
            {
                STitle = "操作失败信息";
                _tmp = "<img src=\"" + cssDir + "error.gif\" border=\"0\">";
                SCaption = "<font color=\"blue\">抱歉！操作失败。系统出现故障，给您造成不便还请谅解！请将一下错误描述发送给系统管理员，谢谢！</font>";
            }
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.Write("<html xmlns=\"http://www.w3.org/1999/xhtml\">\r<head>\r");
            System.Web.HttpContext.Current.Response.Write("<title>" + STitle + "_hoolian Inc.</title>\r");
            System.Web.HttpContext.Current.Response.Write("<link href=\"/css/css.css\" rel=\"stylesheet\" type=\"text/css\" />\r");
            System.Web.HttpContext.Current.Response.Write("\r</head>\r");
            if (returnUrl)
            {
                if (Url != string.Empty && Url != null)
                {
                    System.Web.HttpContext.Current.Response.Write("<body onload=\"returnPage('" + Url + "');\" style=\"margin-top:50px;\">\r");
                    ReUrlStr = "<li><span style=\"color:blue\">2秒后自动转向...</span></li>";
                }
            }
            else
            {
                System.Web.HttpContext.Current.Response.Write("<body style=\"margin-top:50px;\">\r");
            }
            System.Web.HttpContext.Current.Response.Write("    <table style=\"width:65%;height:180px;\"  border=\"0\" align=\"center\" cellspacing=\"1\" cellpadding=\"5\" class=\"table\">\r   <tr style=\"background: #f5f8fd;\"><td class=\"sysmain_navi\" style=\"height:38px;\" colspan=\"2\">" + SCaption + "</td>\r");
            System.Web.HttpContext.Current.Response.Write("</tr><tr style=\"background: #f5f8fd;\"><td style=\"list_link\" align=\"center\" style=\"40%\">" + _tmp + "<br /><br /></td><td class=\"list_link\"><font color=red>操作描述：</font>\r");
            System.Web.HttpContext.Current.Response.Write("    <ul>\r");
            System.Web.HttpContext.Current.Response.Write("        <li><span style=\"word-wrap:bread-word;word-break:break-all;font-size:11.5px;\">" + Msg + "</span></li>\r         <li><a href='javascript:history.back();'><font color=\"red\">返回上一级</font></a>&nbsp;&nbsp;&nbsp;&nbsp;" + UserUrl(Url) + "</li>" + ReUrlStr + "\r");
            System.Web.HttpContext.Current.Response.Write("     </ul></td></tr>\r    </table>\r");
            System.Web.HttpContext.Current.Response.Write("</body>\r</html>\r");
            System.Web.HttpContext.Current.Response.End();
        }
    }
}
