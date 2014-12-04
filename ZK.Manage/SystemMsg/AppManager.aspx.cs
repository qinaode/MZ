using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.IO;
using System.Data;

namespace ZK.Manage.SystemMsg
{
    public partial class AppManager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"];
            string flag = Request.QueryString["flag"];
            string del = Request.QueryString["Del"];
            if (flag=="Up")
            {
                Move(id, "Up");
            }
            if (flag == "Down")
            {
                Move(id, "Down");
            }
            if (del == "Del")
            {
                Delect(id);
            }
        }

        /// <summary>
        /// 操作系列按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AppListItem_Commond(object sender, RepeaterCommandEventArgs e)
        {

            if (e.CommandName == "CN_btn_Delete")
            {
                string id = e.CommandArgument.ToString();
                Delect(id);
            }
            if (e.CommandName == "CN_btn_MoveUp")
            {
                string id = e.CommandArgument.ToString();
                Move(id,"Up");
            }
            if (e.CommandName == "CN_btn_MoveDown")
            {
                string id = e.CommandArgument.ToString();
                Move(id,"Down");
            }
        }

                      
        #region 方法
        private void BindAppList()
        {
            int foruserType = 1;

            string strRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><request>" +
                                         "<ip>" + Page.Request.UserHostAddress + "</ip>" +
                                            "<forusertype>" + foruserType + "</forusertype>" +
                                            "</request> ";
            string strResponse = "";
            bool boolIS = new OpenCom.Command().Execute("Admin.GetWebApps", strRequest, ref strResponse, 5000);
            //xml to dataset
            StringReader stream = null;
            XmlTextReader reader = null;
            DataSet dsResponse = new DataSet();

            stream = new StringReader(strResponse);
            //从stream装载到XmlTextReader
            reader = new XmlTextReader(stream);
            dsResponse.ReadXml(reader);


            //rptNoticeList.DataSource = dsResponse.Tables["item"];
            //rptNoticeList.DataBind();
        }

        private void Delect(string id)
        {
            string strRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><request>" +
               "<ip>" + Page.Request.UserHostAddress + "</ip>" +
               "<appid>" + id + "</appid>" +
               "</request> ";
            string strResponse = "";
            bool boolIS = new OpenCom.Command().Execute("Admin.RemoveWebApp", strRequest, ref strResponse, 5000);
            //xml to dataset
            StringReader stream = null;
            XmlTextReader reader = null;
            DataSet dsResponse = new DataSet();

            stream = new StringReader(strResponse);
            //从stream装载到XmlTextReader
            reader = new XmlTextReader(stream);
            dsResponse.ReadXml(reader);

            BindAppList();
        }

        private void Move(string id,string flag)
        {
            #region 不起作用了的
            /*
            string strRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><request>" +
               "<ip>" + Page.Request.UserHostAddress + "</ip>" +
               "<appid>" + id + "</appid>" +
               "</request> ";
            string strResponse = "";
            if (flag == "Up")
            {
                bool boolIS = new OpenCom.Command().Execute("Admin.MoveUpWebApp", strRequest, ref strResponse, 5000);
            }
            if (flag == "Down")
            {
                bool boolIS = new OpenCom.Command().Execute("Admin.MoveDownWebApp", strRequest, ref strResponse, 5000);
            }
            //xml to dataset
            StringReader stream = null;
            XmlTextReader reader = null;
            DataSet dsResponse = new DataSet();

            stream = new StringReader(strResponse);
            //从stream装载到XmlTextReader
            reader = new XmlTextReader(stream);
            dsResponse.ReadXml(reader);
          */
            #endregion
            int appid = Convert.ToInt32(id);

            ZK.Model.WEBAPPS app = new ZK.BLL.WEBAPPS().GetModel(appid);

            int order = app.ORDERVALUE;
            string strSQL = "";
            if (flag.ToLower() == "up")
            {
                strSQL = " ordervalue<" + order.ToString() + " Order by ordervalue desc";
            }
            if (flag.ToLower() == "down")
            {
                strSQL = " ordervalue>" + order.ToString() + " Order by ordervalue asc";
            }

            DataSet ds = new ZK.BLL.WEBAPPS().GetList(strSQL);
            List<ZK.Model.WEBAPPS> depList = new List<Model.WEBAPPS>();
            depList = new ZK.BLL.WEBAPPS().DataTableToList(ds.Tables[0]);
            if (depList.Count > 0)
            {
                int up_ordervalue = Convert.ToInt32(depList[0].ORDERVALUE);
                int up_appid=depList[0].APPID;

                ZK.Model.WEBAPPS up_app = new ZK.Model.WEBAPPS();
                up_app = new BLL.WEBAPPS().GetModel(up_appid);

                ZK.Model.WEBAPPS appA = new Model.WEBAPPS();
                ZK.Model.WEBAPPS appB = new Model.WEBAPPS();

                appA.APPID = app.APPID;
                appA.APPNAME=app.APPNAME;
                appA.APPIMAGE = app.APPIMAGE;
                appA.APPURL = app.APPURL;
                appA.CATEGORY = app.CATEGORY;
                appA.CLIENTWEBBROWSER = app.CLIENTWEBBROWSER;
                appA.CREATETIME = app.CREATETIME;
                appA.FORUSERTYPE = app.FORUSERTYPE;
                appA.INTRODUCTION = app.INTRODUCTION;
                appA.METHOD = app.METHOD;
                appA.POPUP = app.POPUP;
                appA.POSTDATA = app.POSTDATA;
                appA.SHORTCUT = app.SHORTCUT;
                appA.WEBBROWSERHEIGHT = app.WEBBROWSERHEIGHT;
                appA.WEBBROWSERWIDTH = app.WEBBROWSERWIDTH;
                appA.ORDERVALUE = up_ordervalue;


                appB.APPID =up_app.APPID;
                appB.APPNAME = up_app.APPNAME;
                appB.APPIMAGE = up_app.APPIMAGE;
                appB.APPURL = up_app.APPURL;
                appB.CATEGORY = up_app.CATEGORY;
                appB.CLIENTWEBBROWSER = up_app.CLIENTWEBBROWSER;
                appB.CREATETIME = up_app.CREATETIME;
                appB.FORUSERTYPE = up_app.FORUSERTYPE;
                appB.INTRODUCTION = up_app.INTRODUCTION;
                appB.METHOD = up_app.METHOD;
                appB.POPUP = up_app.POPUP;
                appB.POSTDATA = up_app.POSTDATA;
                appB.SHORTCUT = up_app.SHORTCUT;
                appB.WEBBROWSERHEIGHT = up_app.WEBBROWSERHEIGHT;
                appB.WEBBROWSERWIDTH = up_app.WEBBROWSERWIDTH;
                appB.ORDERVALUE = order;

                new BLL.WEBAPPS().Update(appA);
                new BLL.WEBAPPS().Update(appB);

                BindAppList(); 
            }
            
        }

        public string BindBrowser(Object obj)
        {
            string str = "";
            int markIndex = Convert.ToInt32(obj);
            if (markIndex == 2)

            { str = "客户端嵌入（弹出：系统默认）"; }

            else if (markIndex == 1)

            { str = "客户端嵌入"; }

            else

            { str = "系统默认"; }

            return str;   
        }
        public string BindShortCut(Object obj)
        {
            string str = "";
            int markIndex = Convert.ToInt32(obj);
            if (markIndex == 2)

            { str = "快速启动栏"; }

            else if (markIndex == 1)

            { str = "用户信息栏"; }

            else

            { str = "应用列表"; }

            return str;   
        }
        #endregion
    }
}