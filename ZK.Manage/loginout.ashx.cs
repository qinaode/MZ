using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZK.Manage
{
    /// <summary>
    /// loginout 的摘要说明
    /// </summary>
    public class loginout : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpCookie cook = context.Request.Cookies["SysUserName"];
            cook.Expires = DateTime.Now.AddDays(-1);
            HttpCookie cook_i = context.Request.Cookies["SysUserId"];
            cook_i.Expires = DateTime.Now.AddDays(-1);
            context.Response.Cookies.Add(cook);
            context.Response.Cookies.Add(cook_i);
            context.Response.Redirect("/login.aspx");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}