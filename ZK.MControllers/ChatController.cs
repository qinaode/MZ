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

namespace ZK.MControllers
{
    public class ChatController : Controller
    {
        ZK.BLL.USERS bllUser = new BLL.USERS();
        ZK.BLL.OFFLINEMESSAGES bllMsg = new BLL.OFFLINEMESSAGES();

        //发送个人聊天消息
        public string SendMessageChatInfo()
        {
            string jcbstr = Request["jcb"];
            string userID = Request["userID"];
            string toUserId = Request["toUserId"];
            //string showName = Request["showName"];
            string message = Request["message"];

            //message = "aa";
            //userID = "10040";
            //toUserId = "10022";
            string strResponse = "";
            string strRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><request>" +
                 "<ip>" + Request.UserHostAddress + "</ip>" +
                 "<key>" + ConfigurationManager.AppSettings["IMIdentity"] + "</key>" +
                 "<from>" + userID + "</from>" +
                 "<sendto>" + toUserId + "</sendto>" +
                  "<content>" + message + "</content>" +
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

            if (boolIS == true)
            {
                return jcbstr + "({code : 1})";
            }
            else
            {
                return jcbstr + "({code : 0})";
            }
        }

        //发送群聊消息
        public string SendGroupMsgInfo()
        {
            string jcbstr = Request["jcb"];
            string userID = Request["userID"];
            string groupId = Request["groupId"];
            string context = Request["context"];
            string fontstring = Request["fontstring"];
            string msglevel = "0";
            //userID = "10004";
            //groupId = "10000";
            //context = "436345";
            if (fontstring == null)
            {
                fontstring = "\"微软雅黑\", 9, [], [000000]";
            }
            string strResponse = "";
            string strRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><request>" +
                 "<ip>" + Request.UserHostAddress + "</ip>" +
                 "<key>" + ConfigurationManager.AppSettings["IMIdentity"] + "</key>" +
                 "<userid>" + userID + "</userid>" +
                 "<groupid>" + groupId + "</groupid>" +
                  "<msglevel>" + msglevel + "</msglevel>" +
                  "<content>" + context + "</content>" +
                   "<fontstring>" + fontstring + "</fontstring>" +
                 "</request> ";

            bool boolIS = new OpenCom.Command().Execute("OpenApi.SendGroupMessage", strRequest, ref strResponse, 5000);

            //xml to dataset
            StringReader stream = null;
            XmlTextReader reader = null;
            DataSet dsResponse = new DataSet();

            stream = new StringReader(strResponse);
            //从stream装载到XmlTextReader
            reader = new XmlTextReader(stream);
            dsResponse.ReadXml(reader);

            DataSet dsGroupMsg = new ZK.BLL.OFFGROUPMSGS().GetList("msgid='" + dsResponse.Tables[0].Rows[0]["msgid"].ToString() + "'");

            if (boolIS == true)
            {
                return jcbstr + "({code : 1,psid:" + dsGroupMsg.Tables[0].Rows[0]["sid"].ToString() + "})";
            }
            else
            {
                return jcbstr + "({code : 0})";
            }
        }

        public string NewMessageInfo()
        {
            string jcbstr = Request["jcb"];
            try
            {
                string userID = Request["userID"];
                //userID = "10024";
                string time = Request["time"];
                string strWhere = " 1=1";
                string strJson = "";
                if (userID != "" && userID != null)
                {
                    ZK.Model.USERS mdlUser = bllUser.GetModel(Convert.ToInt32(userID));
                    if (mdlUser.LOGINSTATUS == 0)
                    {
                        if (time != null && time != "")
                        {
                            time = "/Date(" + time + ")/";
                            DateTime dtime = JsonToDateTime(time);
                            strWhere += " and SENDTIME>" + dtime;
                        }
                        DataSet ds = bllMsg.GetList(strWhere + " and userid=" + Convert.ToInt32(userID));
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            strJson = CreateJsonParameters(ds.Tables[0]);
                            List<Model.OFFLINEMESSAGES> listMsg=bllMsg.DataTableToList(ds.Tables[0]);
                            for (int i = 0; i < listMsg.Count; i++)
                            {
                                bllMsg.Delete(listMsg[i].MSGID, listMsg[i].USERID);
                            }

                            return jcbstr + "(" + strJson + ")";
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

        public string CreateJsonParameters(DataTable dt)
        {
            StringBuilder JsonString = new StringBuilder();
            //Exception Handling        
            if (dt != null && dt.Rows.Count > 0)
            {
                JsonString.Append("{list:[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    JsonString.Append("{");
                    if (i < dt.Rows.Count)
                    {
                        JsonString.Append("\"" + dt.Columns[0].ColumnName.ToString() + "\":" + dt.Rows[i][0].ToString());
                        JsonString.Append(",\"" + dt.Columns[4].ColumnName.ToString() + "\":" + "\"" + dt.Rows[i][4].ToString() + "\"");
                        JsonString.Append(",\"" + dt.Columns[6].ColumnName.ToString() + "\":" +  dt.Rows[i][6].ToString());
                        JsonString.Append(",\"" + dt.Columns[8].ColumnName.ToString() + "\":" + "\"" + dt.Rows[i][8].ToString() + "\"");
                        string strDate;
                        strDate = dt.Rows[i][9].ToString();
                        JsonString.Append(",\"" + dt.Columns[9].ColumnName.ToString() + "\":\"" + strDate + "\"");                       
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
                JsonString.Append("]}");
                return JsonString.ToString();
            }
            else
            {
                return null;
            }
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
    }
}
