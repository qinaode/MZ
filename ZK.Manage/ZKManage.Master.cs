using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZK.Manage
{
    public partial class ZKManage : System.Web.UI.MasterPage
    {
        public string curpage = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["SysUserName"] == null)
            {
                Response.Redirect("/login.aspx");
            }
            curpage = Request.QueryString["curp"];
        }
        /// <summary>
        /// 退出系统
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbLoginOut_Click(object sender, EventArgs e)
        {
            HttpCookie cook = Request.Cookies["SysUserName"];
            cook.Expires = DateTime.Now.AddDays(-1);
            HttpCookie cook_i = Request.Cookies["SysUserId"];
            cook_i.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(cook);
            Response.Cookies.Add(cook_i);
            Response.Redirect("/login.aspx");
        }
    }
}