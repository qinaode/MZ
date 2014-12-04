using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZK.Common;
using System.IO;
using System.Xml;
using System.Data;

namespace ZK.Manage.SystemMsg
{
    public partial class AppManagerEdit : System.Web.UI.Page
    {
        #region 定义
        private string Id;
        private bool msgError;
        ZK.BLL.WEBAPPS bll = new BLL.WEBAPPS();
        ZK.Model.WEBAPPS mdl = new Model.WEBAPPS();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PageLoad();

                if (Server.HtmlEncode(Request.QueryString["ty"]) == "editicon")
                {
                    div1.Visible = false;
                    div2.Visible = true;
                }
                else
                {
                    div2.Visible = false;
                    div1.Visible = true;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string appName = txtAppName.Text;
            string category = txtAppCategory.Text;
            string introduction = txtAppIntroduce.Text;
            string imag = fPicture.Value;
            string appURL = txtAppURL.Text;
            int method = Convert.ToInt32(selectMode.Value);
            string postData = txtPost.Text;
            int popUp = Convert.ToInt32(selectPopUp.Value);
            int browser = Convert.ToInt32(selectBrowser.Value);
            int withd;
            if (txtWith.Text != string.Empty)
            {
                withd = Convert.ToInt32(txtWith.Text);
            }
            else
                withd = 0;
            int height;
            if (txtHeight.Text != string.Empty)
            {
                height = Convert.ToInt32(txtHeight.Text);
            }
            else
                height = 0;

            int shortCut = Convert.ToInt32(selectShortCut.Value);

            if (appName == string.Empty)
            {
                MessageBox.Show(this, "应用名称不能为空！");
                return;
            }
            if (category == string.Empty)
            {
                MessageBox.Show(this, "应用分类不能为空！");
                return;
            }

            if (Server.HtmlEncode(Request.QueryString["ty"]) == "add")
            {
                if (imag == string.Empty)
                {
                    MessageBox.Show(this, "应用图标不能为空！");
                    return;
                }
                else
                {
                    string fullFileName = this.fPicture.PostedFile.FileName;//要上传文件的全路径；  
                    string fileName = fullFileName.Substring(fullFileName.LastIndexOf("\\") + 1);  //截取当前全路径的最后文字，文件名  
                    string type = fullFileName.Substring(fullFileName.LastIndexOf(".") + 1); //查取.后面的字符，即文件名的扩展名。判断上传格式是否为图片  

                    this.fPicture.PostedFile.SaveAs(Server.MapPath("../AppImg/") + fileName);  //上传    MapPath返回相对路径   
                }
            }

            if (postData == string.Empty)
            {

                MessageBox.Show(this, "POST参数不能为空！");
                return;
            }
            if (appURL.Length >= 7)
            {
                if (appURL.Trim().Substring(0, 7) != "http://" && appURL.Trim().Substring(0, 8) != "https://")
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

            DataSet ds = bll.GetList("1=1 order by ORDERVALUE desc");
            List<Model.WEBAPPS> list = bll.DataTableToList(ds.Tables[0]);

            //string strResponse = "";
            if (Server.HtmlEncode(Request.QueryString["ty"]) == "add")
            {               
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].APPNAME == appName)
                    {
                        MessageBox.Show(this, "该应用名称已存在，请重新命名！");
                        return;
                    }
                }
                List<Model.WEBAPPS> listapps = bll.GetModelList("1=1 order by APPID desc");
                if (listapps.Count > 0)
                {
                    mdl.APPID = listapps[0].APPID + 1;
                }
                else
                {
                    mdl.APPID = 1;
                }
                mdl.FORUSERTYPE = 1;
                mdl.APPIMAGE = imag;
                mdl.APPNAME = appName;
                mdl.APPURL = appURL;
                mdl.CATEGORY = category;
                mdl.CLIENTWEBBROWSER = browser;
                mdl.INTRODUCTION = introduction;
                mdl.METHOD = method;
                mdl.POPUP = popUp;
                mdl.POSTDATA = postData;
                mdl.SHORTCUT = shortCut;
                mdl.WEBBROWSERHEIGHT = height;
                mdl.WEBBROWSERWIDTH = withd;
                mdl.CREATETIME = DateTime.Now;                               

                mdl.ORDERVALUE = list[0].ORDERVALUE + 1;

               msgError=bll.Add(mdl);
               if (msgError == true)
               {
                   MessageBox.Show(this, "添加成功！");
               }
                #region
                //string strRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><request>" +
                //     "<ip>" + Page.Request.UserHostAddress + "</ip>" +
                //     "<forusertype>" + "1" + "</forusertype>" +
                //     "<appname>" + appName + "</appname>" +
                //      "<category>" + category + "</category>" +
                //      "<introduction>" + introduction + "</introduction>" +
                //       "<appimage>" + imag + "</appimage>" +
                //        "<appurl>" + appURL + "</appurl>" +
                //        "<method>" + method + "</method>" +
                //         "<postdata>" + postData + "</postdata>" +
                //          "<popup>" + popUp + "</popup>" +
                //           "<clientwebbrowser>" + browser + "</clientwebbrowser>" +
                //            "<webbrowserwidth>" + withd + "</webbrowserwidth>" +
                //         "<webbrowserheight>" + height + "</webbrowserheight>" +
                //          "<shortcut>" + shortCut + "</shortcut>" +
                //     "</request> ";
                //bool boolIS = new OpenCom.Command().Execute("Admin.AddWebApp", strRequest, ref strResponse, 5000);
                ////xml to dataset
                //StringReader stream = null;
                //XmlTextReader reader = null;
                //DataSet dsResponse = new DataSet();

                //stream = new StringReader(strResponse);
                ////从stream装载到XmlTextReader
                //reader = new XmlTextReader(stream);
                //dsResponse.ReadXml(reader);
                #endregion

            }
            if (Server.HtmlEncode(Request.QueryString["ty"]) == "edit")
            {
                int appId = Convert.ToInt32(Server.HtmlEncode(Request.QueryString["id"]));

                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].APPID != appId)
                    {
                        if (list[i].APPNAME == appName)
                        {
                            MessageBox.Show(this, "该应用名称已存在，请重新命名！");
                            return;
                        }
                    }
                }

                mdl = bll.GetModel(appId);
                mdl.APPNAME = appName;
                mdl.APPURL = appURL;
                mdl.CATEGORY = category;
                mdl.CLIENTWEBBROWSER = browser;
                mdl.INTRODUCTION = introduction;
                mdl.METHOD = method;
                mdl.POPUP = popUp;
                mdl.POSTDATA = postData;
                mdl.SHORTCUT = shortCut;
                mdl.WEBBROWSERHEIGHT = height;
                mdl.WEBBROWSERWIDTH = withd;

                msgError=bll.Update(mdl);
                if (msgError == true)
                {
                    MessageBox.Show(this, "修改成功！");
                }
                #region
                //string strRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><request>" +
                //   "<ip>" + Page.Request.UserHostAddress + "</ip>" +
                //    "<appid>" + appId + "</appid>" +
                //   //"<forusertype>" + "1" + "</forusertype>" +
                //   "<appname>" + appName + "</appname>" +
                //    "<category>" + category + "</category>" +
                //    "<introduction>" + introduction + "</introduction>" +

                //      "<appurl>" + appURL + "</appurl>" +
                //      "<method>" + method + "</method>" +
                //       "<postdata>" + postData + "</postdata>" +
                //        "<popup>" + popUp + "</popup>" +
                //         "<clientwebbrowser>" + browser + "</clientwebbrowser>" +
                //          "<webbrowserwidth>" + withd + "</webbrowserwidth>" +
                //       "<webbrowserheight>" + height + "</webbrowserheight>" +
                //        "<shortcut>" + shortCut + "</shortcut>" +
                //   "</request> ";
                //bool boolIS = new OpenCom.Command().Execute("Admin.ModifyWebApp", strRequest, ref strResponse, 5000);
                #endregion
            }

            Response.Write("<script>window.open('AppManager.aspx?curp=disk', '_parent', '');var api = frameElement.api, W = api.opener; api.reload();api.close();</script>");
        }

        protected void btnUpLoad_Click(object sender, EventArgs e)
        {

            string strResponse = "";

            if (Server.HtmlEncode(Request.QueryString["ty"]) == "editicon")
            {
                string imag = upFile.Value;
                if (imag == string.Empty)
                {
                    MessageBox.Show(this, "应用图标不能为空！");
                    return;
                }
                else
                {
                    string fullFileName = this.upFile.PostedFile.FileName;//要上传文件的全路径；  
                    string fileName = fullFileName.Substring(fullFileName.LastIndexOf("\\") + 1);  //截取当前全路径的最后文字，文件名  
                    string type = fullFileName.Substring(fullFileName.LastIndexOf(".") + 1); //查取.后面的字符，即文件名的扩展名。判断上传格式是否为图片  

                    this.upFile.PostedFile.SaveAs(Server.MapPath("../AppImg/") + fileName);  //上传    MapPath返回相对路径   
                }
                int appId = Convert.ToInt32(Server.HtmlEncode(Request.QueryString["id"]));
                string strRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><request>" +
                   "<ip>" + Page.Request.UserHostAddress + "</ip>" +
                    "<appid>" + appId + "</appid>" +

                     "<appimage>" + imag + "</appimage>" +

                   "</request> ";
                bool boolIS = new OpenCom.Command().Execute("Admin.ChangeWebAppImage", strRequest, ref strResponse, 5000);
            }
            //xml to dataset
            StringReader stream = null;
            XmlTextReader reader = null;
            DataSet dsResponse = new DataSet();

            stream = new StringReader(strResponse);
            //从stream装载到XmlTextReader
            reader = new XmlTextReader(stream);
            dsResponse.ReadXml(reader);

            Response.Write("<script>window.open('AppManager.aspx?curp=disk', '_parent', '');var api = frameElement.api, W = api.opener; api.reload();api.close();</script>");
        }
        private void PageLoad()
        {
            if (Server.HtmlEncode(Request.QueryString["ty"]) == "edit")
            {
                Id = Server.HtmlEncode(Request.QueryString["id"]);

                mdl = bll.GetModel(Convert.ToInt32(Id));

                txtAppName.Text = mdl.APPNAME;
                txtAppCategory.Text = mdl.CATEGORY;
                txtAppIntroduce.Text = mdl.INTRODUCTION;
                tr.Visible = false;
                //fPicture.Value = mdl.APPIMAGE;
                txtAppURL.Text = mdl.APPURL;
                selectMode.Value = mdl.METHOD.ToString();
                txtPost.Text = mdl.POSTDATA;
                selectPopUp.Value = mdl.POPUP.ToString();
                selectBrowser.Value = mdl.CLIENTWEBBROWSER.ToString();
                txtWith.Text = mdl.WEBBROWSERWIDTH.ToString();
                txtHeight.Text = mdl.WEBBROWSERHEIGHT.ToString();
                selectShortCut.Value = mdl.SHORTCUT.ToString();
            }
        }
    }
}