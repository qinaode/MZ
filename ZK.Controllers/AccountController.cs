using System.Collections.Generic;
using System.Web.Mvc;
using System.IO;
using System.Configuration;
using System.Xml;
using System.Data;
using System.Net;
using System.Web.SessionState;
using System.Web.Script.Serialization;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace ZK.Controllers
{

    public class AccountController : Controller, IRequiresSessionState
    {
        //
        // GET: /Account/

        public ActionResult Login()
        {
            string XMLFilePath = Request.PhysicalApplicationPath + ZK.Common.ModelSettings.BH_SysSettingXMLPath;
            string webtitle = Common.XMLHelper.GetXmlAttribute(XMLFilePath, "Settings/WebTitle", "value").Value;
            ViewData["webtitle"] = webtitle;
            if (CheckForActiveCode())
            {
                ViewData["pu"] = Request.QueryString["pu"];
                return View();

            }
            return View("RegisteCode");
        } 

        public void toDisk()
        {
            if (Session["uid"] != null)
            {
                string str = Session["uid"].ToString();
            }
            string strToken = Request.QueryString["token"];
            string sign = Request.QueryString["sign"];
            string time = Request.QueryString["time"];
            DataSet dsUser = new DataSet();
            bool boolStatus = false;
            if (strToken != null)
            {
                dsUser = CheckByToken(strToken);
                if (dsUser.Tables[0].Rows.Count > 0)
                {
                    boolStatus = true;
                }
            }
            if (boolStatus)
            {
                TempData["uid"] = dsUser.Tables[0].Rows[0]["userID"];
                // TempData["username"] = strUserName;
                ZK.Model.USERS modelUser = new BLL.USERS().GetModel(int.Parse(TempData["uid"].ToString()));
                TempData["user"] = modelUser;
                Session["uid"] = TempData["uid"];
                Session["username"] = modelUser.USERNAME;
                Session["user"] = TempData["user"];
                string url = "/DiskN/CheckUser?username="+Session["username"].ToString();
                Response.Redirect(url);
            }
            else
            {
                Response.Redirect("/Account/Login");
            }
        }

        #region 用户验证主方法

        /// <summary>
        /// 验证用户(响应js)
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckLoginN()
        {
            //string rurl1 = Request.QueryString["pu"];


            string strToken = Request.QueryString["token"];

            string strUserName = Request.Form["username"];
            string strPass = Request.Form["password"];
            bool boolStatus = false;
            System.Data.DataSet dsUser = new DataSet();
            if (strToken != null)
            {
                dsUser = CheckByToken(strToken);
                if (dsUser.Tables[0].Rows.Count > 0)
                {
                    boolStatus = true;
                }

            }
            else
            {
                //dsUser = CheckByPass(strUserName, strPass);
                dsUser = CheckByPass(strUserName);
                if (dsUser.Tables[0].Columns.Count > 1)
                {
                   //if (dsUser.Tables[0].Rows[0]["password"].ToString() == ZK.Common.StringPlus.StringToMD5(strPass))
                    if (dsUser.Tables[0].Rows[0]["PWD"].ToString() == ZK.Common.StringPlus.StringToMD5(strPass))
                    {
                        boolStatus = true;
                    }
                }

            }
            if (boolStatus)
            {
                //string rurl = Request.Form["pu"];
                //if (rurl == "" || rurl == null)
                //{
                //    rurl = "/home/index/";
                //}

                //育新学校特殊定制，直接返回到网盘

                string rurl = "/DiskN/CheckUser?username="+strUserName;

                TempData["uid"] = dsUser.Tables[0].Rows[0]["userID"];
                TempData["username"] = strUserName;
                ZK.Model.USERS modelUser = new BLL.USERS().GetModel(int.Parse(TempData["uid"].ToString()));
                TempData["user"] = modelUser;
                Session["uid"] = TempData["uid"];
                Session["username"] = TempData["username"];
                Session["user"] = TempData["user"];

                #region 注释代码--留着待用 by zyingbo 20130930

                /*---------------------
                System.Net.WebClient wcMini = new System.Net.WebClient();
                string strUrl = string.Format("http://{0}/netdisk/index.php/nbc/forcelogin?username={1}", ConfigurationManager.AppSettings["url"], strUserName);
                string strList=wcMini.DownloadString(new Uri(strUrl));
                JsonResult jsList = Json(strList);
                List<ZK.Model.JsonCommon> lsJson = ZK.Common.Json.JsonDeserializeBySingleData<List<ZK.Model.JsonCommon>>(strList);

                ZK.Model.JsonCommon lsJson = JsonConvert.DeserializeObject<ZK.Model.JsonCommon>(strList);
                Session["user"] = JsonConvert.SerializeObject(lsJson.msg);
                Session["appId"] = 1;
                Session["deviceId"] = 1;
                HttpCookie user = new HttpCookie("user","abc");

                 */

                #endregion

                // return RedirectToAction("Index", "Home");
                return Content("true");

            }
            else
            {
                return Content("fasle");
            }

        }


        /// <summary>
        /// 验证用户
        /// </summary>
        /// <returns></returns>
        public void CheckLogin()
        {
            //string rurl1 = Request.QueryString["pu"];


            string strToken = Request.QueryString["token"];

            string strUserName = Request.Form["username"];
            string strPass = Request.Form["password"];
            bool boolStatus = false;
            System.Data.DataSet dsUser = new DataSet();
            if (strToken != null)
            {
                dsUser = CheckByToken(strToken);
                if (dsUser.Tables[0].Rows.Count > 0)
                {
                    boolStatus = true;
                }

            }
            else
            {
                dsUser = CheckByPass(strUserName, strPass);
              
                if (dsUser.Tables[0].Columns.Count > 1)
                {
                    if (dsUser.Tables[0].Rows[0]["password"].ToString() == ZK.Common.StringPlus.StringToMD5(strPass))
                    {
                        boolStatus = true;
                    }
                }

            }
            if (boolStatus)
            {
                string rurl = Request.Form["pu"];
                if (rurl == "" || rurl == null)
                {
                    rurl = "/home/index/";
                }

                TempData["uid"] = dsUser.Tables[0].Rows[0]["userID"];
                TempData["username"] = dsUser.Tables[0].Rows[0]["username"];
                ZK.Model.USERS modelUser = new BLL.USERS().GetModel(int.Parse(TempData["uid"].ToString()));
                TempData["user"] = modelUser;
                Session["uid"] = TempData["uid"];
                Session["username"] = TempData["username"];
                Session["user"] = TempData["user"];


                #region 注释代码--留着待用 by zyingbo 20130930

                /*---------------------
                System.Net.WebClient wcMini = new System.Net.WebClient();
                string strUrl = string.Format("http://{0}/netdisk/index.php/nbc/forcelogin?username={1}", ConfigurationManager.AppSettings["url"], strUserName);
                string strList=wcMini.DownloadString(new Uri(strUrl));
                JsonResult jsList = Json(strList);
                List<ZK.Model.JsonCommon> lsJson = ZK.Common.Json.JsonDeserializeBySingleData<List<ZK.Model.JsonCommon>>(strList);

                ZK.Model.JsonCommon lsJson = JsonConvert.DeserializeObject<ZK.Model.JsonCommon>(strList);
                Session["user"] = JsonConvert.SerializeObject(lsJson.msg);
                Session["appId"] = 1;
                Session["deviceId"] = 1;
                HttpCookie user = new HttpCookie("user","abc");

                 */

                #endregion

                // return RedirectToAction("Index", "Home");
               // return Content("true");
                //  return
                Response.Redirect(rurl);
            }
            else
            {
                Response.Redirect("/Account/Login");
            }

        }


        public ActionResult RegisteCode()
        {
            string XMLFilePath = Request.PhysicalApplicationPath + ZK.Common.ModelSettings.BH_SysSettingXMLPath;
            string webtitle = Common.XMLHelper.GetXmlAttribute(XMLFilePath, "Settings/WebTitle", "value").Value;
            ViewData["webtitle"] = webtitle;
            return View();
        }

        public ActionResult CheckForCode()
        {
            bool result = false;
            string ActiveCode = Request.Form["ActiveCode"];
            if (ActiveCode == ZK.Common.ReadComputerInfo.GetAllMechinData())
            {
                if (!System.IO.File.Exists(Server.MapPath(ZK.Common.ModelSettings.ActiveCodeXML)))
                {
                    ZK.Common.XMLHelper.CreateXmlDocument(Server.MapPath(ZK.Common.ModelSettings.ActiveCodeXML), "Active", "1.0", "utf-8", "yes");
                    ZK.Common.XMLHelper.CreateOrUpdateXmlNodeByXPath(Server.MapPath(ZK.Common.ModelSettings.ActiveCodeXML), "/Active", "Code", "");
                    ZK.Common.XMLHelper.CreateOrUpdateXmlAttributeByXPath(Server.MapPath(ZK.Common.ModelSettings.ActiveCodeXML), "/Active/Code", "Value", ActiveCode);
                }
                else
                {
                    ZK.Common.XMLHelper.CreateOrUpdateXmlAttributeByXPath(Server.MapPath(ZK.Common.ModelSettings.ActiveCodeXML), "/Active/Code", "Value", ActiveCode);
                }
                result = true;
            }
            return Json(result.ToString().ToLower());
        }

        #endregion

        #region 使用用户名、密码检查用户认证

        /// <summary>
        /// 使用用户名、密码检查用户认证
        /// </summary>
        /// <param name="strUserName"></param>
        /// <param name="strPass"></param>
        /// <returns></returns>
        private DataSet CheckByPass(string strUserName, string strPass)
        {
            string strName = strUserName.Trim();
            //string strRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><request>" +
            //       "<ip>" + Request.UserHostAddress + "</ip>" +
            //       "<key>" + ConfigurationManager.AppSettings["IMIdentity"] + "</key>" +
            //       "<useridorname>" + strName + "</useridorname>" +
            //       "</request> ";
            string strRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><request>" +
                   "<ip>" + Request.UserHostAddress + "</ip>" +
                   "<key>" + ConfigurationManager.AppSettings["IMIdentity"] + "</key>" +
                   "<useridorname>" + strName + "</useridorname>" +
                   "<password>" + strPass + "</password>" +
                   "<loginstatus>6</loginstatus>" +
                   "<loginstatustext>LOGINSTATUS_MOBILE</loginstatustext>" +
                   "</request> ";
            string strResponse = "";
            bool boolIS = new OpenCom.Command().Execute("OpenApi.UserLogin", strRequest, ref strResponse, 5000);
            //xml to dataset
            StringReader stream = null;
            XmlTextReader reader = null;
            DataSet dsResponse = new DataSet();

            stream = new StringReader(strResponse);
            //从stream装载到XmlTextReader
            reader = new XmlTextReader(stream);
            dsResponse.ReadXml(reader);
            return dsResponse;
        }

        #endregion

        #region 使用token检查用户认证

        /// <summary>
        /// 使用token检查用户认证
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private System.Data.DataSet CheckByToken(string token)
        {

            string strRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><request>" +
                               "<ip>" + Request.UserHostAddress + "</ip>" +
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


            return dsResponse;
        }

        #endregion

        #region 网盘Token认证

        public string NetDiskCheckToken()
        {
            string strToken = Request.QueryString["token"];
            string strPass = Request.QueryString["password"];
            string strUserName = Request.QueryString["name"];
            string strSign = Request.QueryString["sign"];
            string strUrl = Request.Url.ToString();
            string strTime = Request.QueryString["t"];

            System.Data.DataSet dsUser = new DataSet();
            // bool boolStatus = false;
            string strJSON = "";
            if (strToken != null)
            {
                dsUser = CheckByToken(strToken);
            }
            else if (strUserName != null && strPass != null)
            {
                dsUser = CheckByPass(strUserName, strPass);

            }
            if (dsUser.Tables[0].Rows.Count > 0)
            {
                strJSON = "{\"success\":1,\"code\":1,\"name\":\"" + dsUser.Tables[0].Rows[0]["username"].ToString() + "\",\"nick\":\"" + dsUser.Tables[0].Rows[0]["username"].ToString() + "\",\"space\":1099511627776,\"email\":\"" + dsUser.Tables[0].Rows[0]["username"].ToString() + "@hoolian.cn\",\"extend\":{\"qq\":\"100000\",\"phone\":\"1888888888\"}}";
            }
            else
            {
                strJSON = "{\"success\":0,\"code\":-1}";
            }

            return strJSON;
        }

        #endregion

        #region  邮箱认证登录 2013/12

        #region 字段

        string agent = "hoolian";
        string emailformat = "@smilecity.cn";
        string test_user = "wuzhanlei@smilecity.cn";

        //access_token
        string access_token_url = "https://cnc.exmail.qq.com/cgi-bin/token";
        string access_token_data = "grant_type=client_credentials&client_id=hoolian&client_secret=c9d69b25bb17af102b00515beeef2a1e";

        //authkey 
        string authkey_url = "http://openapi.exmail.qq.com:12211/openapi/mail/authkey";
        string authkey_data = "access_token={0}&alias={1}";

        //onekey_login
        string OK_Login_url = "https://exmail.qq.com/cgi-bin/login";
        string OK_Login_data = "fun=bizopenssologin&method=bizauth&agent={0}&user={1}&ticket={2}";

        //emailcount 获取新邮件数
        string emailcount_url = "http://openapi.exmail.qq.com:12211/openapi/mail/newcount";
        string emailcount_data = "access_token={0}&alias={1}";

        //listen 维持长连接
        Socket socket = null;
        string Listen_url = "http://openapi.exmail.qq.com:12211/openapi/listen";
        string Listen_doman = "openapi.exmail.qq.com";
        int Listen_Port = 12211;

        string Listen_data = "access_token={0}&alias={1}";

        #endregion

        /// <summary>
        /// 登录到邮箱
        /// </summary>
        public void LoginToEmail()
        {
            try
            {
                string useremail = "";
                List<string> Access_Array = new List<string>();
                string AuthKey = GetAuthKey(out useremail, out Access_Array);
                if (useremail == "" || AuthKey == "" || Access_Array.Count == 0)
                {
                    RedirectToAction("Login");
                }
                //一键登录
                OK_Login_data = string.Format(OK_Login_data, new string[] { agent, useremail, AuthKey });

                //直接转向邮箱界面 不能先返回view再转向 不然 会timeout
                Redirect(OK_Login_url + "?" + OK_Login_data);

            }
            catch
            {
                Redirect("http://cnc.exmail.qq.com/login");
            }
        }

        /// <summary>
        /// 测试方法
        /// </summary>
        /// <returns></returns>
        public ActionResult ToEmail()
        {

            string useremail = "";
            List<string> Access_Array = new List<string>();
            string AuthKey = GetAuthKey(out useremail, out Access_Array);
            if (useremail == "" || AuthKey == "" || Access_Array.Count == 0)
            {
                RedirectToAction("Login");
            }
            string Acc_Token = Access_Array[1];
            string MailID_content = GetEmailData(OK_Login_url, OK_Login_data);

            //获取最新邮件数
            string emailcount_content = "";
            emailcount_content = PostEmailData(emailcount_url, emailcount_data);
            emailcount_content = GetEmailData(emailcount_url, string.Format(emailcount_data, new string[] { Acc_Token, test_user }));
            ViewData["emailcount_content"] = emailcount_content;

            //与邮箱建立长连接
            //  LongConnection_Email();
            //监听 长连接

            //先获取当前维护的版本
            Listen_data = string.Format(Listen_data, new string[] { Acc_Token, test_user });
            HttpWebResponse response = null;
            string Listen_content = GetEmailData(Listen_url, Listen_data, "keeplive", out response);

            return View();
        }

        /// <summary>
        /// 获取authkey  并返回useremail
        /// </summary>
        /// <param name="useremail">用户邮箱</param>
        /// <param name="Access_Array">Access数据组 [0] Access_Type   [1] Access_Token</param>
        /// <returns></returns>
        private string GetAuthKey(out string useremail, out List<string> Access_Array)
        {
            string strToken = Request["token"];
            Access_Array = new List<string>();
            DataSet dsUser = new DataSet();

            if (strToken != null)
            {
                dsUser = CheckByToken(strToken);
            }
            else
            {
                //返回空数据
                useremail = "";
                return "";

            }
            useremail = dsUser.Tables[0].Rows[0]["username"].ToString() + emailformat;
            //获取Access_token
            string Access_Token_content = null;
            Access_Token_content = PostEmailData(access_token_url, access_token_data);
            string[] temp_array = Access_Token_content.Split('"');
            string Acc_Token = "";
            string Acc_Type = "";
            if (temp_array.Length > 3)
            {
                Acc_Token = temp_array[3];
                Acc_Type = temp_array[7];
            }
            Access_Array.Add(Acc_Token);
            Access_Array.Add(Acc_Type);
            //获取AuthKey
            string AuthKey_content = null;
            AuthKey_content = GetEmailData(authkey_url, string.Format(authkey_data, new string[] { Acc_Token, useremail }));

            //分离出authkey
            string AuthKey = AuthKey_content.Split(new char[] { '"' }, StringSplitOptions.RemoveEmptyEntries)[3];

            return AuthKey;

        }

        /// <summary>
        /// 通过Post方式发送数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postdata"></param>
        /// <returns></returns>
        protected string PostEmailData(string url, string postdata)
        {
            // ASCIIEncoding encoding = new ASCIIEncoding();
            Encoding encoding = Encoding.GetEncoding("utf-8");
            byte[] data = encoding.GetBytes(postdata);

            // Prepare web request
            HttpWebRequest myRequest =
            (HttpWebRequest)WebRequest.Create(url);

            myRequest.Method = "Post";
            myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.ContentLength = data.Length;
            using (Stream newStream = myRequest.GetRequestStream())
            {

                // Send the data.
                newStream.Write(data, 0, data.Length);
                newStream.Close();
            }
            // Get response
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            using (StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.Default))
            {
                string content = reader.ReadToEnd();
                myResponse.Close();
                if (myRequest != null)
                {
                    myRequest.Abort();
                }
                return content;
            }
        }

        /// <summary>
        /// 通过Post方式发送数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postdata"></param>
        /// <returns></returns>
        protected string PostEmailData(string url, string postdata, string KeepLive)
        {
            // ASCIIEncoding encoding = new ASCIIEncoding();
            Encoding encoding = Encoding.GetEncoding("utf-8");
            byte[] data = encoding.GetBytes(postdata);

            // Prepare web request
            HttpWebRequest myRequest =
            (HttpWebRequest)WebRequest.Create(url);
            myRequest.KeepAlive = true;
            myRequest.Method = "Post";
            myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.ContentLength = data.Length;
           
            using (Stream newStream = myRequest.GetRequestStream())
            {

                // Send the data.
                newStream.Write(data, 0, data.Length);
                newStream.Close();
            }
            // Get response

            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            
            using (StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.Default))
            {
                string content = reader.ReadToEnd();
                //myResponse.Close();
                //if (myRequest != null)
                //{
                //    myRequest.Abort();
                //}
                return content;
            }
        }

       
   
        /// <summary>
        /// 通过GET方式发送数据
        /// </summary>
        /// <param name="Url">url</param>
        /// <param name="postDataStr">GET数据</param>
        /// <param name="cookie">GET容器</param>
        /// <returns></returns>
        protected string GetEmailData(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }

        /// <summary>
        /// 通过GET方式发送数据
        /// </summary>
        /// <param name="Url">url</param>
        /// <param name="postDataStr">GET数据</param>
        /// <param name="cookie">GET容器</param>
        /// <returns></returns>
        protected string GetEmailData(string Url, string postDataStr, string keeplive, out HttpWebResponse rep)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            request.KeepAlive = true;
            string retString = "";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            while (true)
            {

                rep = response;

                using (Stream myResponseStream = response.GetResponseStream())
                {

                    using (StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8")))
                    {
                        retString = myStreamReader.ReadToEnd();
                        myStreamReader.Close();
                        myResponseStream.Close();

                    }
                }
            }
            return retString;
        }

        protected void LongConnection_Email()
        {
            string addressid = Listen_doman;
            //addressid = "119.147.15.108";
            int port = Listen_Port;

            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(addressid, port);

                ThreadPool.QueueUserWorkItem(new WaitCallback(ReciveData), socket);
            }
            catch
            {
                if (socket != null)
                {
                    socket.Dispose();
                }
            }

        }

        /// <summary>
        /// 连接后监听动作
        /// </summary>
        /// <param name="obj"></param>
        protected void ReciveData(Object obj)
        {
            Socket soc = obj as Socket;
            try
            {
                while (soc != null && soc.Connected)
                {
                    byte[] bytes = new byte[1024 * 1024];
                    int length = soc.Receive(bytes, 0, bytes.Length, SocketFlags.None);
                    if (length <= 0)
                    {
                        break;
                    }
                    string result = Encoding.Default.GetString(bytes, 0, length);
                    //返回为ver时
                    if (result.Contains("ver"))
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                soc.Close();
            }
        }

        #endregion

        public ActionResult Logout()
        {
           

            Session["uid"] = null;
            Session["username"] = null;
            Session["user"] = null;

            if (CheckForActiveCode())
            {
                ViewData["pu"] = Request.QueryString["pu"];
                return RedirectToAction("Login");
            }
            return RedirectToAction("RegisteCode");

        }


        /// <summary>
        /// 检测注册码
        /// </summary>
        /// <returns></returns>
        private bool CheckForActiveCode()
        {
            try
            {
                if (!System.IO.File.Exists(Server.MapPath(ZK.Common.ModelSettings.ActiveCodeXML)))
                {
                    return false;
                }
                string nodevalue = ZK.Common.XMLHelper.GetXmlAttribute(Server.MapPath(ZK.Common.ModelSettings.ActiveCodeXML), "Active/Code", "Value").Value;
                string ActiveCode = ZK.Common.ReadComputerInfo.GetAllMechinData();

                if (ActiveCode != nodevalue)
                {

                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {

                return false;
            }
        }


        public RemoteCertificateValidationCallback CheckValidationResult { get; set; }

        #region add by ao
        private DataSet CheckByPass(string username)
        {
            return new BLL.USERS().GetList(" USERNAME='"+username+"'");
        }
        #endregion

        #region 修改密码

        /// <summary>
        /// 修改密码视图
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangePWD()
        {
            return View();
        }
        /// <summary>
        /// 修改密码主方法
        /// </summary>
        /// <returns></returns>
        public ActionResult postPWD()
        {
            string opass = Request.Form["op"];
            string npass = Request.Form["np"];
            int userID = ((ZK.Model.USERS)(Session["user"])).USERID;

            if (new ZK.BLL.USERS().GetModel(userID).PWD == ZK.Common.StringPlus.StringToMD5(opass))
            {
                string pwd = ZK.Common.StringPlus.StringToMD5(npass);
                string strRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><request>" +
                                    "<ip>" + Request.UserHostAddress + "</ip>" +
                                    "<userid>" + userID + "</userid>" +
                                    "<newpwd>" + pwd + "</newpwd>" +
                                    "</request>";
                string strResponse = "";
                bool boolIS = new OpenCom.Command().Execute("Admin.ResetUserPWD", strRequest, ref strResponse, 5000);
                return Json("sucess");
            }
            else
            {
                return Json("fail");
            }

        }

        #endregion
    }
}
