using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Xml;
using System.Data;
using ZK.Common;

namespace ZK.Manage.SysNoticeManagement
{
    public partial class NoticeManagementEdit : System.Web.UI.Page
    {
        private string Id;
        ZK.BLL.SYSMSGS bll = new BLL.SYSMSGS();
        ZK.Model.SYSMSGS mdl = new Model.SYSMSGS();
         
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PageLoad();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string title = txtTitle.Text;
            string content = txtConnent.Text;

            string txtrange = cmbRange.Value;
            int range;
            if (txtrange == "内部账号+外部账号")
            {
                range = -1;
            }
            else if (txtrange == "内部账号")
            {
                range = 1;
            }
            else
            {
                range = 0;
            }

            string sendTo = txtUser.Text;

            int online;
            if (checkedOnline.Checked == true)
            {
                online = 1;
            }
            else
                online = 0;

            string txtlink = RadioButtonList1.SelectedValue;
            string link;
            if (txtlink != "0")
            {
                link = "有";
            }
            else
                link = "无";


            if (content == string.Empty)
            {
                MessageBox.Show(this, "公告标题不能为空！");
                return;
            }
            if (title == string.Empty)
            {
                MessageBox.Show(this, "公告内容不能为空！");
                return;
            }

            string strResponse = "";

            if (Server.HtmlEncode(Request.QueryString["ty"]) == "add" || Server.HtmlEncode(Request.QueryString["ty"]) == "addchild")
            {
                string strRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><request>" +
                      "<ip>" + Page.Request.UserHostAddress + "</ip>" +
                      "<forusertype>" + range + "</forusertype>" +
                      "<title>" + title + "</title>" +
                       "<content>" + content + "</content>" +
                        "<link>" + link + "</link>" +
                         "<sendto>" + sendTo + "</sendto>" +
                         "<online>" + online + "</online>" +
                      "</request> ";

                bool boolIS = new OpenCom.Command().Execute("Admin.SendSysMsg", strRequest, ref strResponse, 5000);

                //xml to dataset
                StringReader stream = null;
                XmlTextReader reader = null;
                DataSet dsResponse = new DataSet();

                stream = new StringReader(strResponse);
                //从stream装载到XmlTextReader
                reader = new XmlTextReader(stream);
                dsResponse.ReadXml(reader);
            }
            if (Server.HtmlEncode(Request.QueryString["ty"]) == "edit")
            {
                mdl.CONTENT = content;
                mdl.FORUSERTYPE = Convert.ToInt32(range);
                mdl.LINK = link;
                mdl.ONLINE = online;
                mdl.SENDTO = Convert.ToInt32(sendTo);
                mdl.SID = long.Parse(Server.HtmlEncode(Request.QueryString["id"]));
                mdl.TITLE = title;
                DateTime now = DateTime.Now;
                mdl.SENDTIME = now;

                bll.Update(mdl);
            }

            Response.Write("<script>window.open('NoticeManagementList.aspx？curp=notice', '_parent', '');var api = frameElement.api, W = api.opener; api.reload();api.close();</script>");
        }

        private void PageLoad()
        {                 
            if (Server.HtmlEncode(Request.QueryString["ty"]) == "edit")
            {
                Id = Server.HtmlEncode(Request.QueryString["id"]);  
                
                mdl = bll.GetModel(Convert.ToInt32(Id));
                
                txtTitle.Text=mdl.TITLE;
                txtConnent.Text = mdl.CONTENT;
                cmbRange.Value = mdl.FORUSERTYPE.ToString();
                txtUser.Text = mdl.SENDTO.ToString();
                
                if (mdl.ONLINE == 1)
                {
                    checkedOnline.Checked = true;
                }

                if( mdl.LINK=="无")
                    RadioButtonList1.SelectedValue ="0";

            }
        }

    }
}