using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;
using System.Web.UI;
using System.Configuration;
using System.Xml;
using System.Data;

namespace ZK.WebService
{
    /// <summary>
    /// SendMessage 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class SendMessage : System.Web.Services.WebService
    {
        ZK.BLL.USERS bllUser = new BLL.USERS();
        ZK.BLL.OFFLINEMESSAGES bllMsg = new BLL.OFFLINEMESSAGES();

        /// <summary>
        /// 发送即时消息
        /// </summary>
        /// <param name="fromUserId">发送人用户ID</param>
        /// <param name="toUserId">接收人用户ID</param>
        /// <param name="message">发送内容</param>
        /// <param name="sysToken ">系统验证码</param>
        /// <returns>成功|返回"ok"</returns>
        [WebMethod]
        public String SendChatInfo(string fromUserId, string toUserId, string message, string sysToken)
        {
            try
            {
                if (!string.IsNullOrEmpty(fromUserId))
                {
                    if (!string.IsNullOrEmpty(toUserId))
                    {
                        if (!string.IsNullOrEmpty(message))
                        {

                            string jcbstr = sysToken;
                            string strFromUserID = fromUserId;
                            string strrToUserId = toUserId;
                            string strMessage = message;

                            //strMessage = "aaytrt";
                            //strFromUserID = "10022";
                            //strrToUserId = "10026";
                            string strResponse = "";
                            string strRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><request>" +
                                 "<ip>" + "127.0.0.1" + "</ip>" +
                                 "<key>" + ConfigurationManager.AppSettings["IMIdentity"] + "</key>" +
                                 "<from>" + strFromUserID + "</from>" +
                                 "<sendto>" + strrToUserId + "</sendto>" +
                                  "<content>" + strMessage + "</content>" +
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
                                return "oksdf";
                            }
                            else
                            {
                                return "error";
                            }
                        }
                        else
                        {
                            return "不能发送空消息！";
                        }
                    }
                    else
                    {
                        return "接收人不能为空！";
                    }
                }
                else
                {
                    return "发送人不能为空！";
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }


        /// <summary>
        /// 发送通知、公告等消息
        /// </summary>
        /// <param name="userId">发送人用户ID</param>
        /// <param name="title">内容标题</param>
        /// <param name="content">发送内容</param>
        /// <param name="toUserIds">接收人用户ID（多个用逗号隔开）</param>
        /// <param name="online">1-只发送给在线的用户，0-发送所有用户</param>
        /// <param name="linkUrl">链接地址（注:选中消息时跳转到该地址）</param>
        /// <param name="sysToken ">系统验证码</param>
        /// <returns>成功返回"ok"</returns>
        [WebMethod]
        public string SendNotice(string userId, string title, string content, string toUserIds, string onlion, string linkUrl, string sysToken)
        {
            ZK.BLL.DEPARTUSERS bllDepUser = new BLL.DEPARTUSERS();
            try
            {
                //userId = "10000";
                //title = "www";
                //content = "oiuewori";
                //toUserIds = "10011";
                //onlion = "1";
                //linkUrl = "http://www.baidu.com";
                //sysToken = "www";
            
                if (!string.IsNullOrEmpty(userId))
                {
                    if (!string.IsNullOrEmpty(toUserIds))
                    {
                        string sendRange = toUserIds;
                        string strResponse = "";
                        string strRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><request>" +
                                     "<ip>" + "127.0.0.1" + "</ip>" +
                                     "<forusertype>" + -1 + "</forusertype>" +
                                     "<title>" + strtoxml(title) + "</title>" +
                                     "<content>" + strtoxml(content) + "</content>" +
                                       "<link>" + strtoxml(linkUrl) + "</link>" +
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

                        if (boolIS == true)
                        {
                            return "ok";
                        }
                        else
                        {
                            return "error";
                        }
                    }
                    else
                    {
                        return "接收人不能为空！";
                    }
                }
                else
                {
                    return "发送人不能为空！";
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="token">token(用于验证用户信息)</param>
        /// <param name="sysToken ">系统验证码</param>
        /// <returns>成功返回"userID"</returns>
        [WebMethod]
        public String CheckByToken(string token, string sysToken)
        {
            try
            {
                if (!string.IsNullOrEmpty(token))
                {

                    string strRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><request>" +
                                       "<ip>" + "127.0.0.1" + "</ip>" +
                                       "<key>" + ConfigurationManager.AppSettings["IMIdentity"] + "</key>" +
                                       "<token>" + token + "</token>" +
                                       "</request> ";
                    string strResponse = "";
                    bool boolIS = new OpenCom.Command().Execute("OpenApi.ValidateToken", strRequest, ref strResponse, 5000);
                    //xml to dataset
                    StringReader stream = null;
                    XmlTextReader reader = null;
                    DataSet dsResponse = new DataSet();

                    stream = new StringReader(strResponse);
                    //从stream装载到XmlTextReader
                    reader = new XmlTextReader(stream);
                    dsResponse.ReadXml(reader);

                    if (dsResponse.Tables[0].Rows.Count > 0)
                    {
                        string userID = dsResponse.Tables[0].Rows[0]["USERID"].ToString();
                        return userID;
                    }
                    else
                    {
                        return "error";
                    }
                }
                else
                {
                    return "token值不能为空！";
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        
        //转义变量
        private string strtoxml(string str)
        {
            string strBack=str;
            strBack=strBack.Replace("&","&amp;");
             strBack=strBack.Replace("<","&lt;");
             strBack=strBack.Replace(">","&gt;");
             strBack=strBack.Replace("\"","&quot;");
             strBack=strBack.Replace("'","&apos;");
            return strBack;
        }

    }
}
