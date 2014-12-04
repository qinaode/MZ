using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Xml;
using System.Data;

namespace ZK.Manage.SettingManage
{
    public partial class AdminManage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            UserDataBind();

        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="UserID">用户id</param>
        /// <param name="UserName">用户名</param>
        /// <param name="UserDepart">用户机构</param>
        private void UserDataBind()
        {

            string strRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><request>" +
    "<ip>" + Page.Request.UserHostAddress + "</ip>" +
    "</request>";
            string strResponse = "";
            bool boolIS = new OpenCom.Command().Execute("Admin.GetAdmins", strRequest, ref strResponse, 5000);
            //xml to dataset
            StringReader stream = null;
            XmlTextReader reader = null;
            DataSet dsResponse = new DataSet();

            stream = new StringReader(strResponse);
            //从stream装载到XmlTextReader
            reader = new XmlTextReader(stream);
            dsResponse.ReadXml(reader);
            DataTable dt = dsResponse.Tables["item"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["LOGINED"].ToString() == "1")
                {
                    dt.Rows[i]["LOGINED"] = "是";
                }
                else
                {
                    dt.Rows[i]["LOGINED"] = "否";
                }
                if (dt.Rows[i]["ISLOCK"].ToString() == "1")
                {
                    dt.Rows[i]["ISLOCK"] = "是";
                }
                else
                {
                    dt.Rows[i]["ISLOCK"] = "否";
                }

            }
            rptUserList.DataSource = dt;
            rptUserList.DataBind();
        }

        /// <summary>
        /// 操作系列按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UserListItem_Commond(object sender, RepeaterCommandEventArgs e)
        {

            if (e.CommandName == "CN_btn_Delete")
            {
                string adminname = e.CommandArgument.ToString();
                DeleteAdminInfo(adminname);
            }
            if (e.CommandName == "CN_btn_UpdatePWD")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "java", "AddOrEditAdmin('" + e.CommandArgument + "','1','修改密码');", true);
            }
            UserDataBind();
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="AdminID">用户id</param>
        /// <returns>是否成功 true false</returns>
        private bool DeleteAdminInfo(string adminname)
        {
            string strRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><request>" +
                                "<ip>" + Page.Request.UserHostAddress + "</ip>" +
                                "<adminname>" + adminname + "</adminname>" +
                                "</request>";
            string strResponse = "";
            bool boolIS = new OpenCom.Command().Execute("Admin.RemoveAdmin", strRequest, ref strResponse, 5000);
            return boolIS;
        }

    }
}