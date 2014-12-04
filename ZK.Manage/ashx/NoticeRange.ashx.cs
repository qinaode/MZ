using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Xml;
using ZK.Common;

namespace ZK.Manage.ashx
{
    /// <summary>
    /// NoticeRange 的摘要说明
    /// </summary>
    public class NoticeRange : IHttpHandler
    {


        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string Flag = context.Request.Form["Flag"];
            //string Flag = context.Request.QueryString["Flag"];
            string strname = context.Request.Form["strName"];
            string Result = string.Empty;

            string title = context.Request.Form["title"];
            string content = context.Request.Form["content"];
            string range = context.Request.Form["range"];
            string onlion = context.Request.Form["onlion"];
            string linkText = context.Request.Form["linkText"];
            string checkedval = context.Request.Form["checkedval"];

            switch (Flag)
            {
                case "AllDepInfoJson":
                    Result = AllDepInfoJson();
                    break;
                case "GetDepInfoJson":
                    Result = GetDepInfoJson();
                    break;
                case "AllUserInfoJson":
                    Result = AllUserInfoJson();
                    break;
                case "SearchUserInfoJson":
                    Result = SearchUserInfoJson(strname);
                    break;
                case "SendNotice":
                    Result = SendNotice(title, content, range, onlion, linkText, checkedval);
                    break;
            }
            context.Response.Write(Result);
        }

        //#region 方法
        //得到所有部门
        public string AllDepInfoJson()
        {
            String JsonString = "";
            JsonString = RESTfulServices.AddInterface.DepAndUsers.AllDepInfo();
            return JsonString;
        }

        //得到所有部门
        public string GetDepInfoJson()
        {
            String JsonString = "";
            JsonString = RESTfulServices.AddInterface.DepAndUsers.GetDepIds();
            return JsonString;
        }

        //获取所有人员
        public string AllUserInfoJson()
        {
            String JsonString = "";
            JsonString = RESTfulServices.AddInterface.DepAndUsers.AllUserInfo();
            return JsonString;
        }
        //获取查询的人员
        public string SearchUserInfoJson(string strname)
        {
            string strName = strname;
            String JsonString = "";
            JsonString = RESTfulServices.AddInterface.DepAndUsers.SearchUserInfo(strName);
            return JsonString;
        }

        public string SendNotice(string title, string content, string range, string onlion, string linkText, string checkedval)
        {
            //string title = title;
            //string content= content ;
            //string range =  range; 
            //string onlion = onlion ;
            //string linkText = linkText;
            //if (ExistsMsg(title))
            //{
            //    return "0";
            //}
            ZK.BLL.DEPARTUSERS bllDepUser = new BLL.DEPARTUSERS();
            try
            {
                #region 处理发送范围
                string[] idsList = range.Split(',');
                List<int> listdis = new List<int>();
                string sendRange = "";
                for (int i = 0; i < idsList.Length; i++)
                {
                    string ids = idsList[i];
                    string[] strId = ids.Split('_');
                    int id = Convert.ToInt32(strId[1]);
                    bool bools = true;
                    if (strId[0] == "dep")
                    {
                        List<Model.DEPARTUSERS> list = bllDepUser.GetModelList("DEPARTID=" + id);
                        if (list != null)
                        {
                            for (int n = 0; n < list.Count; n++)
                            {
                                for (int m = 0; m < listdis.Count; m++)
                                {
                                    if (listdis[m] == list[n].USERID)
                                    {
                                        bools = false;
                                        continue;
                                    }
                                }
                                if (bools)
                                {
                                    listdis.Add(list[n].USERID);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (listdis.Count > 0)
                        {
                            bool flag = false;
                            for (int j = 0; j < listdis.Count; j++)
                            {
                                if (id == listdis[j])
                                {
                                    flag = false;
                                    break;
                                }
                                flag = true;
                            }
                            if (flag)
                            {
                                listdis.Add(id);
                            }
                        }
                        else
                        {
                            listdis.Add(id);
                        }
                    }
                }

                for (int m = 0; m < listdis.Count; m++)
                {
                    if (m == listdis.Count - 1)
                    {
                        sendRange += listdis[m].ToString();
                    }
                    else
                    {
                        sendRange += listdis[m].ToString() + ",";
                    }
                }
                #endregion

                #region 生成html
                if (checkedval == "2")
                {
                    string link = Guid.NewGuid().ToString() + ".html";
                  
                    string htmlFile = "/SystemMsg/content/" + link;
                    string htmlCode = linkText;
                    string htmlPage = "<html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=gb2312\" />" +
                "<title>" + title + "</title></head><body style=\"font-size:13px;\">" +
                "标题: " + title + "<br>" +
                "发布时间: " + DateTime.Now + "<br>" +
                "<hr>" + linkText + "</body></html>";

                    System.IO.StreamWriter sw;
                    sw = new System.IO.StreamWriter(HttpContext.Current.Server.MapPath(htmlFile), false, System.Text.Encoding.Default);
                    sw.Write(htmlPage);
                    sw.Close();

                    linkText = link;
                }
                #endregion

                string strResponse = "";
                string strRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><request>" +
                             "<ip>" + "127.0.0.1" + "</ip>" +
                             "<forusertype>" + -1 + "</forusertype>" +
                             "<title>" + title + "</title>" +
                             "<content>" + content + "</content>" +
                               "<link>" + linkText + "</link>" +
                                "<sendto>" + sendRange + "</sendto>" +
                                "<online>" + onlion + "</online>" +
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

                //return Json("1");
                return "1";
            }
            catch
            {
                // return Json("");
                return "";
            }

        }


        #region 判断是否存在同名的公告标题
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
        #endregion

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}