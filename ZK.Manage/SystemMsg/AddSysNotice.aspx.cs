using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZK.Common;
using System.Data;
using System.IO;
using System.Xml;
using System.Configuration;

namespace ZK.Manage.SystemMsg
{
    public partial class AddSysNotice : System.Web.UI.Page
    {
    //    #region define
    //    ZK.BLL.DEPARTUSERS bllDepUser = new BLL.DEPARTUSERS();
    //    #endregion
    //    protected void Page_Load(object sender, EventArgs e)
    //    {

    //    }
    //    #region 发送按钮
    //     protected void btnSave_Click(object sender, EventArgs e)
    //    {
    //        string title = txtTitle.Text;//公告标题
    //        string content = txtConnent.Text;//公告内容
    //        string sendTo = ids.Value;//公告范围
    //        string link;//标示网页链接
    //        int online; //标示是否只发给在线用户
    //        if (content == string.Empty)
    //        {
    //            MessageBox.Show(this, "公告标题不能为空！");
    //            return;
    //        }
    //        if (title == string.Empty)
    //        {
    //            MessageBox.Show(this, "公告内容不能为空！");
    //            return;
    //        }
    //        if (ids.Value == string.Empty)
    //        {
    //            MessageBox.Show(this, "发送范围不能为空！");
    //            return;
    //        }
    //        if (checkedOnline.Checked == true)
    //        {
    //            online = 1;
    //        }
    //        else
    //        {
    //            online = 0;
    //        }


    //        #region 处理发送范围
    //        string[] idsList = sendTo.Split(',');
    //        List<int> listdis = new List<int>();
    //        for (int i = 0; i < idsList.Length; i++)
    //        {
    //            int id = Convert.ToInt32(idsList[i]);
    //            if (id < 1000)
    //            {
    //                List<Model.DEPARTUSERS> list = bllDepUser.GetModelList("DEPARTID=" + id);
    //                if (list != null)
    //                {
    //                    for (int n = 0; n < list.Count; n++)
    //                    {
    //                        listdis.Add(list[n].USERID);
    //                    }
    //                }
    //            }
    //            else
    //            {
    //                if (listdis.Count > 0)
    //                {
    //                    bool flag = false;
    //                    for (int j = 0; j < listdis.Count; j++)
    //                    {
    //                        if (id == listdis[j])
    //                        {
    //                            flag = false;
    //                            break;
    //                        }
    //                        flag = true;
    //                    }
    //                    if (flag)
    //                    {
    //                        listdis.Add(id);
    //                    }
    //                }
    //                else
    //                {
    //                    listdis.Add(id);
    //                }
    //            }
    //        }

    //        for (int m = 0; m < listdis.Count; m++)
    //        {
    //            if (m == listdis.Count - 1)
    //            {
    //                sendTo += listdis[m].ToString();
    //            }
    //            else
    //            {
    //                sendTo += listdis[m].ToString() + ",";
    //            }
    //        }
    //        #endregion


           
    //       #region 无用的
    //        /*
    //        if (Radio2.Checked == true)
    //        {
    //            link = txtLinkAddress.Text;//外部网页链接

    //        }
            
    //        else if (Radio3.Checked == true)//自定义网页内容
    //        {
    //            link = Guid.NewGuid().ToString() + ".html";
    //            string htmlFile = "./content/" + link;
    //            string htmlCode = this.txthtml.InnerText;
    //            string htmlPage = "<html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" />" +
    //        "<title>" + title + "</title></head><body style=\"font-size:13px;\">" +
    //        "标题: " + title + "<br>" +
    //        "发布时间: " + DateTime.Now + "<br>" +
    //        "<hr>" + htmlCode + "</body></html>";

    //            System.IO.StreamWriter sw;
    //            sw = new System.IO.StreamWriter(Server.MapPath(htmlFile), false, System.Text.Encoding.Default);
    //            sw.Write(htmlPage);
    //            sw.Close();

    //         }
    //        else
    //            link = "";
    //        */
    //#endregion 

    //        if (Radio2.Checked == true)
    //        {
    //            link = txtLinkAddress.Text; ;//外部网页链接
    //            if (link.Length >= 7)
    //            {
    //                if (link.Trim().Substring(0, 7) != "http://" && link.Trim().Substring(0, 8) != "https://")
    //                {
    //                    MessageBox.Show(this, "链接地址必须以'http://'或'https://'开头");
    //                    return;
    //                }
    //            }
    //            else
    //            {
    //                MessageBox.Show(this, "链接地址必须以http://或https://开头");
    //                return;
    //            }
    //        }
    //        else if (Radio3.Checked == true)//自定义网页内容
    //        {
    //            link = Guid.NewGuid().ToString() + ".html";
    //            string htmlFile = "./content/" + link;
    //            string htmlCode = this.txthtml.InnerText;
    //            string htmlPage = "<html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" />" +
    //        "<title>" + title + "</title></head><body style=\"font-size:13px;\">" +
    //        "标题: " + title + "<br>" +
    //        "发布时间: " + DateTime.Now + "<br>" +
    //        "<hr>" + htmlCode + "</body></html>";

    //            System.IO.StreamWriter sw;
    //            sw = new System.IO.StreamWriter(Server.MapPath(htmlFile), false, System.Text.Encoding.Default);
    //            sw.Write(htmlPage);
    //            sw.Close();

    //         }
    //        else
    //        {
    //            link = "";
    //        }
    //        string strResponse = "";

    //        if (ExistsMsg(title) == false)
    //        {//需要修改的
    //            string strRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><request>" +
    //                    "<ip>" + Page.Request.UserHostAddress + "</ip>" +
    //                    "<forusertype>" +0 + "</forusertype>" +
    //                    "<title>" + title + "</title>" +
    //                    "<content>" + content + "</content>" +
    //                    "<link>" + link + "</link>" +
    //                        "<sendto>" + sendTo + "</sendto>" +
    //                        "<online>" + online + "</online>" +
    //                    "</request> ";
                

    //            bool boolIS = new OpenCom.Command().Execute("Admin.SendSysMsg", strRequest, ref strResponse, 5000);


    //                //xml to dataset
    //                StringReader stream = null;
    //                XmlTextReader reader = null;
    //                DataSet dsResponse = new DataSet();

    //                stream = new StringReader(strResponse);
    //                //从stream装载到XmlTextReader
    //                reader = new XmlTextReader(stream);
    //                dsResponse.ReadXml(reader);
    //       }
    //       else
    //       {
    //           MessageBox.Show(this, "已存在相同名称的公告！");
    //           return;
    //       }

    //        //Response.Write("<script>window.open('..\\SystemMsg\\MsgManager.aspx?curp=notice', '_parent', '');var api = frameElement.api, W = api.opener; api.reload();api.close();</script>");
    //        Response.Redirect("/SystemMsg/MsgManager.aspx?curp=notice");
    //    }
    //    #endregion
      

    //    #region 判断是否存在同名的公告标题
    //    private bool ExistsMsg(string title)
    //    {
    //        bool flag = false;
    //        //ZK.Model.SYSMSGS sysmsg = new Model.SYSMSGS();
    //        string where = "";
    //        DataSet ds = new ZK.BLL.SYSMSGS().GetList(where);
    //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //        {
    //            DataRow row = ds.Tables[0].Rows[i];
    //            if (title.Equals(row["TITLE"].ToString()))
    //            {
    //                flag = true;
    //                break;
    //            }
    //        }
    //        return flag;
    //    }
    //    #endregion

    //    #region 发送即时消息按钮
    //    protected void btnSendMessage_Click(object sender, EventArgs e)
    //    {
    //        string title = txtTitle.Text;
    //        string content = txtConnent.Text;
    //        //string sendTo = txtUser.Value;
    //        string sendTo = "";
    //        string strResponse = "";

    //        string strRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><request>" +
    //             "<ip>" + Page.Request.UserHostAddress + "</ip>" +
    //             "<key>" + ConfigurationManager.AppSettings["IMIdentity"] + "</key>" +
    //             "<from>10012</from>" +
    //             "<sendto>" + sendTo + "</sendto>" +
    //              "<content>" + content + "</content>" +
    //             "</request> ";

    //        bool boolIS = new OpenCom.Command().Execute("OpenApi.SendMessage", strRequest, ref strResponse, 5000);


    //        //xml to dataset
    //        StringReader stream = null;
    //        XmlTextReader reader = null;
    //        DataSet dsResponse = new DataSet();

    //        stream = new StringReader(strResponse);
    //        //从stream装载到XmlTextReader
    //        reader = new XmlTextReader(stream);
    //        dsResponse.ReadXml(reader);
    //    }
    //    #endregion
    }
}