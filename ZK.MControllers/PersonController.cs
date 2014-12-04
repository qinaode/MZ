using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;
using System.Data;
using System.Web.Script.Serialization;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

namespace ZK.MControllers
{
    public class PersonController : Controller
    {
        #region 定义
        ZK.BLL.SYSMSGS bllSysmsgs = new BLL.SYSMSGS();
        ZK.Model.SYSMSGS mdlSysmsgs = new Model.SYSMSGS();
        ZK.BLL.OFFLINEMESSAGES bllMsg = new BLL.OFFLINEMESSAGES();
        ZK.BLL.USERS bllUser = new BLL.USERS();
        ZK.BLL.OFFGROUPMSGS bllgroupMsg = new BLL.OFFGROUPMSGS();
        #endregion

        /// <summary>
        /// 成功跳转后，根据 用户Id 获取个人中心内容
        /// </summary>
        /// <param > userId</param>
        /// <returns></returns>
        public ActionResult PersonInfoJson()
        {
            try
            {

                return Json("");
            }
            catch
            {
                return Json("");
            }
        }

        /// <summary>
        /// 根据 用户Id 获取用户资料
        /// </summary>
        /// <param > userId</param>
        /// <returns></returns>
        public ActionResult UserInfoJson()
        {
            try
            {

                return Json("");
            }
            catch
            {
                return Json("");
            }
        }

        /// <summary>
        ///  获取系统消息
        /// </summary>
        /// <param > </param>
        /// <returns></returns>
        public ActionResult SystemMsgInfoJson()
        {
            try
            {

                return Json("");
            }
            catch
            {
                return Json("");
            }
        }

        /// <summary>
        /// 根据 用户Id 获取 所有公告信息
        /// </summary>
        /// <param name="userId" > userId</param>
        /// <returns></returns>
        public string AllNoticeInfoJson()
        {
            try
            {
                string jcbstr = Request["jcb"];
                string userID = Request["userId"];
                string strWhere = " 1=1";

                if (userID != null && userID != "")
                {
                    strWhere += " and userid=" + userID;
                }
                DataSet ds = bllSysmsgs.GetList(strWhere);
                List<Model.SYSMSGS> listSysmsgs = new List<Model.SYSMSGS>();
                listSysmsgs = bllSysmsgs.DataTableToList(ds.Tables[0]);

                JavaScriptSerializer jss = new JavaScriptSerializer();
                return SerializeJsonString(jss.Serialize(listSysmsgs), jcbstr);
                //return Json(listSysmsgs,JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 根据 用户Id和时间 获取 新公告信息
        /// </summary>
        /// <param name="userId" > userId</param>
        ///   /// <param name="time" > time</param>
        /// <returns></returns>
        public string NewNoticeInfoJson()
        {
            string jcbstr = Request["jcb"];
            try
            {
                string userID = Request["userId"];
                string time = Request["time"];
                string sid = Request["sid"];
                string psid = Request["psid"];
                string strWhere = " 1=1";
                //userID = "10042";
                //psid = "1340";
                //sid = "0";
                //time = "1390636609";              
                string strResultJson = "";
                string strNoticeJson = "";
                string strMsgJson = "";
                string strGroupMsgJson = "";

                #region 注释
                //DataSet dsgroup = bllgroupMsg.GetList("1=1 order by SENDTIME");
                //if (dsgroup.Tables[0].Rows.Count > 0)
                //{ 
                //}
                //string timeStamp = time;
                //DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                //long lTime = long.Parse(timeStamp + "0000000");
                //TimeSpan toNow = new TimeSpan(lTime);
                //DateTime dtResult = dtStart.Add(toNow);
                #endregion

                if (userID != null && userID != "")
                {
                    #region 公告
                    if (sid != null && sid != "")
                    {
                        int Sid = Convert.ToInt32(sid);
                        strWhere += " and sid>" + Sid;

                        DataSet dsNotice = bllSysmsgs.GetList(strWhere);
                        if (dsNotice.Tables[0].Rows.Count > 0)
                        {
                            //List<Model.SYSMSGS> listSysmsgs = new List<Model.SYSMSGS>();
                            //listSysmsgs = bllSysmsgs.DataTableToList(dsNotice.Tables[0]);
                            strNoticeJson = CreateJsonParametersNotice(dsNotice.Tables[0]);
                            //JavaScriptSerializer jss = new JavaScriptSerializer();
                            //strNoticeJson = SerializeJsonString(jss.Serialize(listSysmsgs), jcbstr);
                        }
                        else
                        {
                            strNoticeJson = "\"Notice\":\"no\"";
                        }
                    }
                    else
                    {
                        strNoticeJson = "\"Notice\":\"no\"";
                    }
                    #endregion

                    #region 单用户消息
                    ZK.Model.USERS mdlUser = bllUser.GetModel(Convert.ToInt32(userID));
                    if (mdlUser.LOGINSTATUS == 6)
                    {
                        DataSet ds = bllMsg.GetList("userid=" + Convert.ToInt32(userID));
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            strMsgJson = CreateJsonParameters(ds.Tables[0]);

                            List<Model.OFFLINEMESSAGES> listMsg = bllMsg.DataTableToList(ds.Tables[0]);
                            for (int i = 0; i < listMsg.Count; i++)
                            {
                                bllMsg.Delete(listMsg[i].MSGID, listMsg[i].USERID);
                            }
                        }
                        else
                        {
                            strMsgJson = "\"Chat\":\"no\"";
                        }
                    }
                    else
                    {
                        strMsgJson = "\"Chat\":\"no\"";
                    }
                    #endregion

                    #region 群聊消息
                    DataSet dsGroupMsg = new DataSet();
                    dsGroupMsg = bllgroupMsg.GetList("GROUPID in (select GROUPID from GROUPMEMBERS where MEMBERID=" + Convert.ToInt32(userID) + ") and SID>'" + Convert.ToInt32(psid) + "' and SENDER_USERID<>" + userID);

                    if (dsGroupMsg.Tables[0].Rows.Count > 0)
                    {
                        strGroupMsgJson = CreateJsonParametersGroupMsg(dsGroupMsg.Tables[0]);
                    }
                    else
                    {
                        strGroupMsgJson = "\"Group\":\"no\"";
                    }

                    #endregion
                }

                #region 注释
                //if (strNoticeJson == "")
                //{
                //    return jcbstr + "({" + strMsgJson + "})";
                //}
                //else if (strMsgJson == "")
                //{
                //    return jcbstr + "({" + strNoticeJson + "})";
                //}
                //else if (strMsgJson == "" && strNoticeJson == "")
                //{
                //    return jcbstr + "({ })";
                //}
                //else
                //{
                //    return jcbstr + "({" + strNoticeJson + "," + strMsgJson + "})";
                //}
                #endregion

                strResultJson = strNoticeJson + "," + strMsgJson + "," + strGroupMsgJson;
                return jcbstr + "({" + strResultJson + "})";
            }
            catch
            {
                return jcbstr + "({ })";
            }
        }

        //修改个性签名
        public string UpdateSign()
        {
            string jcbstr = Request["jcb"];
            string strSign = Request["sign"];
            string userID = Request["userID"];
            ZK.Model.USERS mdluser = bllUser.GetModel(Convert.ToInt32(userID));
            mdluser.MODIFYTIME = DateTime.Now;
            mdluser.SIGNATURE = strSign;
            #region
            if (mdluser.ADDRESS == null)
            {
                mdluser.ADDRESS = "";
            }
            if (mdluser.TELEPHONE == null)
            {
                mdluser.TELEPHONE = "";
            }
            if (mdluser.MOBILE == null)
            {
                mdluser.MOBILE = "";
            }
            if (mdluser.FAX == null)
            {
                mdluser.FAX = "";
            }
            if (mdluser.QQ == null)
            {
                mdluser.QQ = "";
            }
            if (mdluser.MSN == null)
            {
                mdluser.MSN = "";
            }
            if (mdluser.EMAIL == null)
            {
                mdluser.EMAIL = "";
            }
            if (mdluser.HOMEPAGE == null)
            {
                mdluser.HOMEPAGE = "";
            }
            if (mdluser.JOBTITLE == null)
            {
                mdluser.JOBTITLE = "";
            }
            if (mdluser.JOBNUMBER == null)
            {
                mdluser.JOBNUMBER = "";
            }
            if (mdluser.INTRODUCTION == null)
            {
                mdluser.INTRODUCTION = "";
            }
            if (mdluser.PHOTOFILE == null)
            {
                mdluser.PHOTOFILE = "";
            }
            if (mdluser.CLIENTLOCATION == null)
            {
                mdluser.CLIENTLOCATION = "";
            }
            if (mdluser.LASTCLIENTLOCATION == null)
            {
                mdluser.LASTCLIENTLOCATION = "";
            }
            if (mdluser.SALT == null)
            {
                mdluser.SALT = "";
            }
            if (mdluser.JOINQUESTION == null)
            {
                mdluser.JOINQUESTION = "";
            }
            if (mdluser.JOINANSWER == null)
            {
                mdluser.JOINANSWER = "";
            }
            #endregion
            bool boolIS = bllUser.Update(mdluser);
            if (boolIS == true)
            {
                return jcbstr + "({code : 1})";
            }
            else
            {
                return jcbstr + "({code : 0})";
            }
        }

        private static string SerializeJsonString(string DataList, string strjcb)
        {
            string datalist = DataList.Replace("\"\\/Date(", "").Replace(")\\/\"", "");

            return strjcb + "({list:" + datalist + "})";
        }

        private DateTime JsonToDateTime(string jsonDate)
        {
            string value = jsonDate.Substring(6, jsonDate.Length - 8);
            DateTimeKind kind = DateTimeKind.Utc;
            int index = value.IndexOf('+', 1);
            if (index == -1)
                index = value.IndexOf('-', 1);
            if (index != -1)
            {
                kind = DateTimeKind.Local;
                value = value.Substring(0, index);
            }
            long javaScriptTicks = long.Parse(value, System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture);
            long InitialJavaScriptDateTicks = (new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).Ticks;
            DateTime utcDateTime = new DateTime((javaScriptTicks * 10000) + InitialJavaScriptDateTicks, DateTimeKind.Utc);
            DateTime dateTime;
            switch (kind)
            {
                case DateTimeKind.Unspecified:
                    dateTime = DateTime.SpecifyKind(utcDateTime.ToLocalTime(), DateTimeKind.Unspecified);
                    break;
                case DateTimeKind.Local:
                    dateTime = utcDateTime.ToLocalTime();
                    break;
                default:
                    dateTime = utcDateTime;
                    break;
            }
            return dateTime;
        }

        public string CreateJsonParameters(DataTable dt)
        {
            StringBuilder JsonString = new StringBuilder();
            //Exception Handling        
            if (dt != null && dt.Rows.Count > 0)
            {
                JsonString.Append("listchat:[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    JsonString.Append("{");
                    if (i < dt.Rows.Count)
                    {
                        JsonString.Append("\"" + dt.Columns[0].ColumnName.ToString() + "\":\"" + dt.Rows[i][0].ToString() + "\"");

                        string messageInfo = dt.Rows[i][4].ToString();
                        messageInfo = @MsgConvertStr(messageInfo).Replace("\r\n", "<br>");
                        JsonString.Append(",\"" + dt.Columns[4].ColumnName.ToString() + "\":" + "\"" + messageInfo + "\"");
                        JsonString.Append(",\"" + dt.Columns[6].ColumnName.ToString() + "\":" + dt.Rows[i][6].ToString());
                        JsonString.Append(",\"" + dt.Columns[8].ColumnName.ToString() + "\":" + "\"" + dt.Rows[i][8].ToString() + "\"");
                        string strDate;
                        strDate = dt.Rows[i][9].ToString();
                        DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                        DateTime dtNow = DateTime.Parse(strDate);
                        TimeSpan toNow = dtNow.Subtract(dtStart);
                        string timeStamp = toNow.Ticks.ToString();
                        timeStamp = timeStamp.Substring(0, timeStamp.Length - 7);
                        long timeLong = long.Parse(timeStamp);
                        JsonString.Append(",\"" + dt.Columns[9].ColumnName.ToString() + "\":\"" + timeLong.ToString() + "\"");
                    }
                    if (i == dt.Rows.Count - 1)
                    {
                        JsonString.Append("}");
                    }
                    else
                    {
                        JsonString.Append("},");
                    }
                }
                JavaScriptSerializer jss = new JavaScriptSerializer();
                JsonString.Append("]");
                return JsonString.ToString();
            }
            else
            {
                return null;
            }
        }

        public string MsgConvertStr(string messageInfo)
        {
            string msgInfo = messageInfo;
            string strResponse = "";
            string strRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><request>" +
                 "<ip>" + Request.UserHostAddress + "</ip>" +
                 "<key>" + ConfigurationManager.AppSettings["IMIdentity"] + "</key>" +
                  "<message >" + msgInfo + "</message >" +
                 "</request> ";

            bool boolIS = new OpenCom.Command().Execute("OpenApi.GetDesMessage", strRequest, ref strResponse, 5000);

            //xml to dataset
            StringReader stream = null;
            XmlTextReader reader = null;
            DataSet dsResponse = new DataSet();

            stream = new StringReader(strResponse);
            //从stream装载到XmlTextReader
            reader = new XmlTextReader(stream);
            dsResponse.ReadXml(reader);
            return dsResponse.Tables[0].Rows[0][1].ToString();
        }

        public string CreateJsonParametersNotice(DataTable dt)
        {
            StringBuilder JsonString = new StringBuilder();
            //Exception Handling        
            if (dt != null && dt.Rows.Count > 0)
            {
                JsonString.Append("list:[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    JsonString.Append("{");
                    if (i < dt.Rows.Count)
                    {
                        JsonString.Append("\"" + dt.Columns[0].ColumnName.ToString() + "\":" + dt.Rows[i][0].ToString());
                        string aa = dt.Rows[i][2].ToString();
                        string bb = @aa.Replace(":", "#").Replace("：", "#").Replace("\r\n", "<br>");
                        JsonString.Append(",\"" + dt.Columns[2].ColumnName.ToString() + "\":\"" + bb + "\"");

                        string cc = dt.Rows[i][3].ToString();
                        string dd = @cc.Replace(":", "#").Replace("：", "#").Replace("\r\n","<br>");
                        JsonString.Append(",\"" + dt.Columns[3].ColumnName.ToString() + "\":\"" + dd + "\"");
                        string strDate;
                        strDate = dt.Rows[i][7].ToString();
                        JsonString.Append(",\"" + dt.Columns[7].ColumnName.ToString() + "\":\"" + strDate + "\"");
                    }
                    if (i == dt.Rows.Count - 1)
                    {
                        JsonString.Append("}");
                    }
                    else
                    {
                        JsonString.Append("},");
                    }
                }
                JavaScriptSerializer jss = new JavaScriptSerializer();
                JsonString.Append("]");
                return JsonString.ToString();
            }
            else
            {
                return null;
            }
        }

        public string CreateJsonParametersGroupMsg(DataTable dt)
        {
            StringBuilder JsonString = new StringBuilder();
            //Exception Handling        
            if (dt != null && dt.Rows.Count > 0)
            {
                JsonString.Append("listgroup:[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    JsonString.Append("{");
                    if (i < dt.Rows.Count)
                    {
                        JsonString.Append("\"psid\":" + dt.Rows[i][0].ToString());
                        JsonString.Append(",\"" + dt.Columns[1].ColumnName.ToString() + "\":" + dt.Rows[i][1].ToString());
                        JsonString.Append(",\"userId\":" + dt.Rows[i][6].ToString());
                        JsonString.Append(",\"sendName\":\"" + dt.Rows[i][8].ToString() + "\"");
                        JsonString.Append(",\"content\":\"" +ZK.Common.JSONHelper.String2Json(dt.Rows[i][4].ToString().Replace("\r\n", "<br>")) + "\"");
                        string strDate;
                        JavaScriptSerializer jss = new JavaScriptSerializer();
                        strDate = dt.Rows[i][9].ToString();

                        DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                        DateTime dtNow = DateTime.Parse(strDate);
                        TimeSpan toNow = dtNow.Subtract(dtStart);
                        string timeStamp = toNow.Ticks.ToString();
                        timeStamp = timeStamp.Substring(0, timeStamp.Length - 7);

                        JsonString.Append(",\"time\":\"" + timeStamp + "\"");
                    }
                    if (i == dt.Rows.Count - 1)
                    {
                        JsonString.Append("}");
                    }
                    else
                    {
                        JsonString.Append("},");
                    }
                }

                JsonString.Append("]");
                return JsonString.ToString();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据 用户Id 获取我的资料
        /// </summary>
        /// <param > userId</param>
        /// <returns></returns>
        public string MyDataInfoJson()
        {
            string jcbstr = Request["jcb"];
            try
            {
                string JsonString = "";
                string userId = Request["userId"];
                if (userId != null && userId != "")
                {
                    ZK.Model.USERS mdlUser = bllUser.GetModel(Convert.ToInt32(userId));
                    if (mdlUser != null)
                    {
                        JsonString = "\"id\":" + mdlUser.USERID + ",\"name\":\"" + mdlUser.USERNAME + "\"";
                        return jcbstr + "({" + JsonString + "})";
                    }
                    else
                    {
                        return jcbstr + "({})";
                    }
                }
                else
                {
                    return jcbstr + "({})";
                }
            }
            catch
            {
                return jcbstr + "({})";
            }
        }
    }
}
