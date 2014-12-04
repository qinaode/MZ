using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Xml;
using System.Data;

namespace ZK.Manage.SysNoticeManagement
{
    public partial class NoticeManagementList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindNoticeList();
            }
          
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ZK.BLL.SYSMSGS bll = new BLL.SYSMSGS();

            string str=  txtTitle.Text;
            string strSQL = "TITLE=" + "'" + str + "'";
            System.Data.DataSet ds = bll.GetList(strSQL);

            rptNoticeList.DataSource = ds;
            rptNoticeList.DataBind();
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
                string id = e.CommandArgument.ToString();
                Delect(id);
            }           
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            BindNoticeList();
        }

        private void BindNoticeList()
        {
            int pageSize = this.AspNetPager1.PageSize;
            int pageIndex = this.AspNetPager1.CurrentPageIndex;

            string strRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><request>" +
                                         "<ip>" + Page.Request.UserHostAddress + "</ip>" +
                                            "<pagesize>"+pageSize+"</pagesize>" +
                                             "<pageindex>" + pageIndex + "</pageindex>" +
                                            "</request> ";
            string strResponse = "";
            bool boolIS = new OpenCom.Command().Execute("Admin.GetSysMsgs", strRequest, ref strResponse, 5000);
            //xml to dataset
            StringReader stream = null;
            XmlTextReader reader = null;
            DataSet dsResponse = new DataSet();

            stream = new StringReader(strResponse);
            //从stream装载到XmlTextReader
            reader = new XmlTextReader(stream);
            dsResponse.ReadXml(reader);


            rptNoticeList.DataSource = dsResponse.Tables["item"];
            rptNoticeList.DataBind();

            if (dsResponse.Tables["item"] != null)
            {
                this.AspNetPager1.RecordCount = int.Parse(dsResponse.Tables["response"].Rows[0]["allusercount"].ToString());
            }
        }

        private void Delect(string id)
        {
            string strRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><request>" +
               "<ip>" + Page.Request.UserHostAddress + "</ip>" +
               "<sid>" + id + "</sid>" +
               "</request> ";
            string strResponse = "";
            bool boolIS = new OpenCom.Command().Execute("Admin.RemoveSysMsg", strRequest, ref strResponse, 5000);
            //xml to dataset
            StringReader stream = null;
            XmlTextReader reader = null;
            DataSet dsResponse = new DataSet();

            stream = new StringReader(strResponse);
            //从stream装载到XmlTextReader
            reader = new XmlTextReader(stream);
            dsResponse.ReadXml(reader);

            BindNoticeList();
        }
    }
}