using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data;
using System.IO;

namespace ZK.Manage
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 登陆事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void subBt_Click(object sender, EventArgs e)
        {

            string strUsername = this.userId.Value;
            string strPwd = this.userPwd.Value ;
            string strRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><request>" +
                                "<ip>" + Page.Request.UserHostAddress + "</ip>" +
                                "<admin>" + Server.HtmlEncode(strUsername) + "</admin>" +
                                "<pwd>" + Server.HtmlEncode(strPwd) + "</pwd>" +
                                "</request> ";
            string strResponse = "";
            bool boolIS = new OpenCom.Command().Execute("Admin.Login", strRequest, ref strResponse, 5000);


            //xml to dataset
            StringReader stream = null;
            XmlTextReader reader = null;
            DataSet dsResponse = new DataSet();

            stream = new StringReader(strResponse);
            //从stream装载到XmlTextReader
            reader = new XmlTextReader(stream);
            dsResponse.ReadXml(reader);

            if (dsResponse.Tables[0].Rows[0][0].ToString() == "0")
            {
                HttpCookie cook = new HttpCookie("SysUserName");
                HttpCookie cookid = new HttpCookie("SysUserId");
                cook.Expires = DateTime.Now.AddMinutes(60);
                cookid.Expires = DateTime.Now.AddMinutes(60);
                cook.Value = "admin";
                cookid.Value = "1";
                Response.Cookies.Add(cook);
                Response.Cookies.Add(cookid);
                Response.Redirect("/Default.aspx");
            }
            else if (dsResponse.Tables[0].Rows[0][0].ToString() == "10")
            {
                this.litTips.Text = "用户名或密码错误！";
            }
            else
            {
                this.litTips.Text = "未知错误，请联系系统运营商！";
            }

        }
    }
}