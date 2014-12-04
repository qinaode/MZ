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
using System.Configuration;

namespace ZK.Manage.SystemMsg
{
    public partial class MsgManagerEdit : System.Web.UI.Page
    {
        #region 定义
        private string Id;
        ZK.BLL.SYSMSGS bll = new BLL.SYSMSGS();
        ZK.Model.SYSMSGS mdl = new Model.SYSMSGS();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    PageLoad();
            //}
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string title = txtTitle.Text;
            string content = txtConnent.Text;

            string txtrange = cmbRange.Value;

            string sendTo = txtUser.Text;

            int online;
            if (checkedOnline.Checked == true)
            {
                online = 1;
            }
            else
                online = 0;

            string link;
            if (Radio2.Checked == true)
            {
                link = txtLinkAddress.Text;

            }
            else if (Radio3.Checked == true)
            {
                link = Guid.NewGuid().ToString() + ".html";
                string htmlFile = "./content/" + link;
                string htmlCode = this.txthtml.InnerText;
                string htmlPage = "<html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" />" +
            "<title>" + title + "</title></head><body style=\"font-size:13px;\">" +
            "标题: " + title + "<br>" +
            "发布时间: " + DateTime.Now + "<br>" +
            "<hr>" + htmlCode + "</body></html>";

                System.IO.StreamWriter sw;
                sw = new System.IO.StreamWriter(Server.MapPath(htmlFile), false, System.Text.Encoding.Default);
                sw.Write(htmlPage);
                sw.Close();

            }
            else
                link = "";

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
            if (Radio2.Checked == true)
            {
                if (link.Length >= 7)
                {
                    if (link.Trim().Substring(0, 7) != "http://" && link.Trim().Substring(0, 8) != "https://")
                    {
                        MessageBox.Show(this, "链接地址必须以'http://'或'https://'开头");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show(this, "链接地址必须以http://或https://开头");
                    return;
                }
            }

            string strResponse = "";

            if (Server.HtmlEncode(Request.QueryString["ty"]) == "add")
            {

                if (ExistsMsg(title) == false)
                {
                    string strRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><request>" +
                          "<ip>" + Page.Request.UserHostAddress + "</ip>" +
                          "<forusertype>" + txtrange + "</forusertype>" +
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
                else
                {
                    MessageBox.Show(this, "已存在相同名称的公告！");
                    return;
                }
            }

            //if (Server.HtmlEncode(Request.QueryString["ty"]) == "edit")
            //{
            //    mdl.CONTENT = content;
            //    mdl.FORUSERTYPE = Convert.ToInt32(txtrange);
            //    mdl.LINK = link;
            //    mdl.ONLINE = online;
            //    mdl.SENDTO = Convert.ToInt32(sendTo);
            //    mdl.SID = long.Parse(Server.HtmlEncode(Request.QueryString["id"]));
            //    mdl.TITLE = title;
            //    DateTime now = DateTime.Now;
            //    mdl.SENDTIME = now;

            //    bll.Update(mdl);
            //}

            Response.Write("<script>window.open('MsgManager.aspx？curp=notice', '_parent', '');var api = frameElement.api, W = api.opener; api.reload();api.close();</script>");
        }

        private void PageLoad()
        {
        }

        private bool ExistsMsg(string title)
        {
            bool flag = false;
            //ZK.Model.SYSMSGS sysmsg = new Model.SYSMSGS();
            string where = "";
            DataSet ds = new ZK.BLL.SYSMSGS().GetList(where);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow row = ds.Tables[0].Rows[i];
                if (title.Equals(row["TITLE"].ToString()))
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }

        protected void btnSendMessage_Click(object sender, EventArgs e)
        {
            string title = txtTitle.Text;
            string content = txtConnent.Text;
            string txtrange = cmbRange.Value;
            string sendTo = txtUser.Text;
            string strResponse = "";
            string strRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><request>" +
                 "<ip>" + Page.Request.UserHostAddress + "</ip>" +
                 "<key>" + ConfigurationManager.AppSettings["IMIdentity"] + "</key>" +
                 "<from>10012</from>" +
                 "<sendto>" + sendTo + "</sendto>" +
                  "<content>" + content + "</content>" +
                 "</request> ";

            bool boolIS = new OpenCom.Command().Execute("OpenApi.SendMessage", strRequest, ref strResponse, 5000);


            //xml to dataset
            StringReader stream = null;
            XmlTextReader reader = null;
            DataSet dsResponse = new DataSet();

            stream = new StringReader(strResponse);
            //从stream装载到XmlTextReader
            reader = new XmlTextReader(stream);
            dsResponse.ReadXml(reader);
        }
    }
}