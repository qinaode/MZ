using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Xml;
using System.Data;

namespace ZK.Manage.SystemMsg
{
    public partial class MsgManager : System.Web.UI.Page
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
            int pageSize = 10;
            int pageIndex = 1;
            ZK.BLL.SYSMSGS bll = new BLL.SYSMSGS();
            System.Data.DataSet ds = new DataSet();
            string str = txtTitle.Text;
            if (str.Trim() != string.Empty)
            {
                string strSQL = "TITLE like '%" + str + "%'";
                ds = bll.GetList( pageSize,pageIndex, strSQL);
                rptNoticeList.DataSource = ds;
                rptNoticeList.DataBind();
            }
            else
            {
                BindNoticeList();
            }

            //Response.Write("<script>window.open('MsgManager.aspx?curp=notice', '_parent', '');</script>");
        }

        /// <summary>
        /// 操作系列按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void NoticeListItem_Commond(object sender, RepeaterCommandEventArgs e)
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
            string strRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><request>" +
                                         "<ip>" + Page.Request.UserHostAddress + "</ip>" +
                                            "<pagesize>" + this.AspNetPager1.PageSize + "</pagesize>" +
                                             "<pageindex>" + this.AspNetPager1.CurrentPageIndex + "</pageindex>" +
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

        public string BindCategory(Object obj)
        {
            string str="";
            int markIndex=Convert.ToInt32(obj);
            if (markIndex ==-1)

            { str = "内部账号+外部账号:"; }

            else if (markIndex == 1)

            { str = "内部账号:"; }

            else

            { str = "外部账号:"; }

            return str;
        }
    }
}