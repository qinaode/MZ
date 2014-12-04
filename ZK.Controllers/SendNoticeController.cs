using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.IO;
using System.Xml;
using System.Data;
using System.Web.UI;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Diagnostics;

namespace ZK.Controllers
{
    public class SendNoticeController : Controller
    {
        #region define
        ZK.BLL.DEPARTUSERS bllDepUser = new BLL.DEPARTUSERS();
        ZK.Model.USERS model=new Model.USERS();
        List<ZK.Model.USERS> list = new List<Model.USERS>();
        ZK.Model.ZK_NT_MsgExtent memodel = new Model.ZK_NT_MsgExtent();
        static string userID = "";
        #endregion
        public ActionResult SendNotice()
        {
            return View();
        }

        public ActionResult SendRange()
        {
            return View("SendRange");
        }
        /// <summary>
        /// 公告管理
        /// </summary>
        /// <returns></returns>
        public ActionResult NoticeManage()
        {
            string token = Request.QueryString["token"];
            //string userID = "100026";
            string userID = "";
            list = new ZK.BLL.USERS().GetModelList(" TOKEN ='" + token + "'");
            if (list.Count > 0)
            {
                model = list[0];
                userID = model.USERID.ToString();
            }
            
            ViewData["userID"] = userID;
            return View();
        }

        /// <summary>
        /// 发送公告的按钮
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]

        public ActionResult BtnSendNotice()
        {
            try
            {
                string title = Request.Form["title"];
                string content = Request["content"];
                string range = Request["range"];
                string onlion = Request["onlion"];
                string linkText = Request["linkText"];
                string checkedval = Request["checkedval"];
                //得到发送公告的人ID
                string userID = Request.Form["userID"];

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
                    string htmlFile = "/Files/sysmsg/" + link;
                    string htmlCode = linkText;
                    string htmlPage = "<html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=gb2312\" />" +
                "<title>" + title + "</title></head><body style=\"font-size:13px;\">" +
                "标题: " + title + "<br>" +
                "发布时间: " + DateTime.Now + "<br>" +
                "<hr><p>" + htmlCode + "</p></body></html>";

                    System.IO.StreamWriter sw;
                    sw = new System.IO.StreamWriter(Server.MapPath(htmlFile), false, System.Text.Encoding.Default);
                    sw.Write(htmlPage);
                    sw.Close();
                    linkText = link;
                }
                #endregion

                string strResponse = "";
                string strRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><request>" +
                             "<ip>" + Request.UserHostAddress + "</ip>" +
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

                //保存UserID到数据库中
                DataSet ds = new ZK.BLL.CommondBase().GetList(" SID ", " SYSMSGS ", " SID ", "SID desc", 1, 1, "", 0);
                int maxid = Convert.ToInt32(ds.Tables[0].Rows[0]["SID"].ToString());

                memodel.extKey = "userID";
                memodel.extValue = userID;
                memodel.SID=maxid;
                int res=new ZK.BLL.ZK_NT_MsgExtent().Add(memodel);
                return Json("1");
            }
            catch
            {
                return Json("");
            }
        }

        //2014.3.4 by wj

        #region 每个人可以查看发给自己的公告
        /// <summary>
        /// 每个人可以查看发给自己的公告,ZK_UserAndSysMsg表中的userID
        /// 为收公告人的ID，关联SYSMSGS，ZK_UserAndSysMsg
        /// </summary>
        /// <returns></returns>
        public string GetNoticeListByUserID()
        {
            string userID = Request.Form["userID"];
            string PageIndex = Request.Form["PageIndex"] == "" ? "1" : Request.Form["PageIndex"];
            string PageSize =Request.Form["PageSize"] == "" ? "10" : Request.Form["PageSize"];
            int pageindex = Convert.ToInt32(PageIndex);
            int pagesize = Convert.ToInt32(PageSize);
            // 得到通知公告列表
            string selecct = "ZK_NT_MsgExtent.extValue,USERS.ACTUALNAME, SYSMSGS.*,ZK_UserAndSysMsg.userID,ZK_UserAndSysMsg.isSee,(case when ZK_UserAndSysMsg.isSee=0 then '未读'   when ZK_UserAndSysMsg.isSee=1 then '已读' else '暂无' end) as isRead ";
            string table = " SYSMSGS left join ZK_UserAndSysMsg on SYSMSGS.SID=ZK_UserAndSysMsg.sysMsgID left join ZK_NT_MsgExtent on SYSMSGS.SID=ZK_NT_MsgExtent.SID  left join USERS on Users.USERID=ZK_NT_MsgExtent.extValue  ";
            string where = "ZK_UserAndSysMsg.userID= " + userID;
            //得到总页码，   1分页
            DataSet noticeList = new ZK.BLL.CommondBase().GetList(selecct, table, "SYSMSGS.SID", " SYSMSGS.SENDTIME desc ", pagesize, pageindex, where, 1);
            //0不分页
            DataSet ds = new ZK.BLL.CommondBase().GetList(selecct, table, "SYSMSGS.SID", "", pagesize, pageindex, where, 0);
            string dataJson = ToJson(noticeList.Tables[0]);
            return SerializeJsonString(dataJson, ds.Tables[0].Rows.Count);
        }
        #endregion

        #region 序列化为json字符串
        private static string SerializeJsonString(string dataList, int totalNumber)
        {
            return "{ \"DataList\":" + dataList + ",\"totalNumber\":" + totalNumber + "}";

        }
        #endregion

        #region Datatable转换为Json
        /// <summary>     
        /// Datatable转换为Json     
        /// </summary>    
        /// <param name="table">Datatable对象</param>     
        /// <returns>Json字符串</returns>     
        public static string ToJson(DataTable dt)
        {
            StringBuilder jsonString = new StringBuilder();
            jsonString.Append("[");
            DataRowCollection drc = dt.Rows;
            if (drc.Count > 0)
            {
                for (int i = 0; i < drc.Count; i++)
                {
                    jsonString.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        string strKey = dt.Columns[j].ColumnName;
                        string strValue = drc[i][j].ToString();
                        Type type = dt.Columns[j].DataType;
                        jsonString.Append("\"" + strKey + "\":");
                        strValue = StringFormat(strValue, type);
                        if (j < dt.Columns.Count - 1)
                        {
                            jsonString.Append(strValue + ",");
                        }
                        else
                        {
                            jsonString.Append(strValue);
                        }
                    }
                    jsonString.Append("},");
                }
                jsonString.Remove(jsonString.Length-1, 1);
                jsonString.Append("]");
            }
            else
            {
                jsonString.Append("]");
            }
            return jsonString.ToString();
        }
        #endregion

        #region 格式化字符型、日期型、布尔型
        /// <summary>
        /// 格式化字符型、日期型、布尔型
        /// </summary>
        /// <param name="str"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static string StringFormat(string str, Type type)
        {
            if (type == typeof(string))
            {
                str = String2Json(str);
                str = "\"" + str + "\"";
            }
            else if (type == typeof(DateTime))
            {
                str = "\"" + str + "\"";
            }
            else if (type == typeof(bool))
            {
                str = str.ToLower();
            }
            else if (type != typeof(string) && string.IsNullOrEmpty(str))
            {
                str = "\"" + str + "\"";
            }
            return str;
        }

        #endregion

        #region 过滤特殊字符

        /// <summary>
        /// 过滤特殊字符
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns>json字符串</returns>
        private static string String2Json(String s)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                char c = s.ToCharArray()[i];
                switch (c)
                {
                    case '\"':
                        sb.Append("\\\""); break;
                    case '\\':
                        sb.Append("\\\\"); break;
                    case '/':
                        sb.Append("\\/"); break;
                    case '\b':
                        sb.Append("\\b"); break;
                    case '\f':
                        sb.Append("\\f"); break;
                    case '\n':
                        sb.Append("\\n"); break;
                    case '\r':
                        sb.Append("\\r"); break;
                    case '\t':
                        sb.Append("\\t"); break;
                    default:
                        sb.Append(c); break;
                }
            }
            return sb.ToString();
        }
        #endregion

        public ActionResult testIframe()
        {
            return View("testIframe");
        }


        #region 删除数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string DelNotice()
        {
            string id = Request.Form["id"];
            string flag = "false";
            int sid = Convert.ToInt32(id);
            ZK.Model.SYSMSGS model = new ZK.Model.SYSMSGS();
            model = new ZK.BLL.SYSMSGS().GetModel(sid);
            if (model == null)
            {
                return flag;
            }
            else
            {
                flag = new ZK.BLL.SYSMSGS().Delete(sid).ToString();
            }
            return flag;
        }
        #endregion

        #region 依据SID得到NOTICE的内容
        /// <summary>
        /// 依据SID得到NOTICE的内容，依据SYSMSGS表中的SID并修改isSee的状态
        /// </summary>
        /// <returns></returns>
        public string GetNoticeItem()
        {   
            string userID=Request.Form["userID"];
            string strid = Request.Form["strid"];
            int id = Convert.ToInt32(strid);
            ZK.Model.ZK_UserAndSysMsg model = new ZK.BLL.ZK_UserAndSysMsg().GetModelList(" sysMsgID="+strid +" and userID="+userID)[0];
            model.isSee = true;
            new ZK.BLL.ZK_UserAndSysMsg().Update(model);
            Model.SYSMSGS sysmodel=new ZK.BLL.SYSMSGS().GetModel(id);
            string content = sysmodel.CONTENT;
            string hrefa = sysmodel.LINK;
            string forusertype=sysmodel.FORUSERTYPE.ToString();
            if (forusertype == "-1")
            {
                if (hrefa == null||hrefa=="")
                {
                    return content;
                }
                else if (hrefa.Substring(0, 6).Trim().ToLower() == "http://")
                {
                    return content + "<a href='" + hrefa + "'>请点击查看</a>";
                }
                else//自定义网页的公告
                {
                    string htmlFile = "/Files/sysmsg/" + hrefa;
                    return content + "<a href='" + htmlFile + "' TARGET='_blank     '>请点击查看</a>";
                    //return content + "<a href='#' name='" + htmlFile + "' onclick='ViewContentHtml(this.name)'>请点击查看</a>"; 
                }
            }
            else if (forusertype == "0")
            {
                string[] url = hrefa.Split('?');
                string urlstr = url[1].Substring(15);
                string tokenstr = new ZK.BLL.USERS().GetModel(Convert.ToInt32(userID)).TOKEN;
                string href = url[0] + "?token=" + tokenstr + urlstr;
                return content + "<a href='" + href + "' TARGET='_blank '>请点击查看</a>";
                //return content + "<a href='#' name='" + hrefa + "' onclick='ViewContentHtml(this.name)'>请点击查看</a>";
            }
            else
            {
                return content;
            }
            
        }
        #endregion

        #region  通过权限管理发送的通知公告！
        /// <summary>
        /// 通过权限管理发送的通知公告！
        /// </summary>
        /// <returns></returns>
        public string ManageNoticeByRole()
        {
            string userID = Request.Form["userID"];
            string selecct = " SYSMSGS.TITLE,SYSMSGS.SENDTIME,SYSMSGS.SID ";
            string PageIndex = Request.Form["PageIndex"] == "" ? "1" : Request.Form["PageIndex"];
            string PageSize = Request.Form["PageSize"] == "" ? "10" : Request.Form["PageSize"];

            string table = " SYSMSGS left join ZK_NT_MsgExtent on ZK_NT_MsgExtent.SID=SYSMSGS.SID  ";
            string where = "  ZK_NT_MsgExtent.extKey='userID' and ZK_NT_MsgExtent.extValue=" + userID;
            int pagesize = Convert.ToInt32(PageSize);
            int pageindex = Convert.ToInt32(PageIndex);
            //0不分页
            DataSet ds = new ZK.BLL.CommondBase().GetList(selecct, table, "SYSMSGS.SID", "", pagesize, pageindex, where, 0);
            //1分页
            DataSet data = new ZK.BLL.CommondBase().GetList(selecct, table, "SYSMSGS.SID", " SYSMSGS.SENDTIME desc ", pagesize, pageindex, where, 1);
            return SerializeJsonString(ToJson(data.Tables[0]), ds.Tables[0].Rows.Count);
        }
        #endregion

        #region 用户是否已读
        /// <summary>
        /// 用户是否已读
        /// </summary>
        /// <returns></returns>
        public string ViewStateList()
        {
            string SID = Request.Form["strid"];
            string selecct = " SYSMSGS.*,ZK_UserAndSysMsg.userID,ZK_UserAndSysMsg.isSee,USERS.ACTUALNAME ";
            string table = " SYSMSGS left join ZK_UserAndSysMsg on SYSMSGS.SID=ZK_UserAndSysMsg.sysMsgID left join Users on ZK_UserAndSysMsg.userID= Users.USERID  ";
            string where = " ZK_UserAndSysMsg.sysMsgID= " + SID;
            int pagesize = 10;
            int pageindex = 1;
            DataSet noticeList = new ZK.BLL.CommondBase().GetList(selecct, table, "SYSMSGS.SID", "", pagesize, pageindex, where, 1);
            string dataJson = ToJson(noticeList.Tables[0]);
            return SerializeJsonString(dataJson, noticeList.Tables[0].Rows.Count);
        }

        #endregion
       
        #region 得到用户的权限
        /// <summary>
        /// 得到用户的权限
        /// </summary>
        /// <returns></returns>
        public string GetUserRoleByUserID()
        {
            string userID = Request.Form["userID"];
            string resu = "false";
            //select ZK_RoleList.roleDesc from ZK_RoleList left join ZK_RoleToUser on ZK_RoleList.roleID=ZK_RoleToUser.roleID where userID='10024';
            string select = " ZK_RoleList.roleDesc   ";
            string table = " ZK_RoleList left join ZK_RoleToUser on ZK_RoleList.roleID=ZK_RoleToUser.roleID  ";
            string where = "  userID= " + userID;
            DataSet ds = new ZK.BLL.CommondBase().GetList(select, table, "", "", 1, 1, where, 1);
            if (ds.Tables[0].Rows.Count<= 0)
            {
                //return "false";
                return resu;
            }
            else
            {
                for (int i=0;i<ds.Tables[0].Rows.Count;i++)
                {
                    if (ds.Tables[0].Rows[i][0].ToString() == "noticeManage")
                    {
                        resu= "true";
                    }
                }
                return resu;
            }

        }
        #endregion

        public void Test(string url)
        {
          url = " http://localhost:19183" + url;
          //Response.Redirect(url);
        }
    }
       
}
