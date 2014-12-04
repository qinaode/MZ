using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using ZK.BLL;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.Script.Serialization;
using System.Configuration;
using System.IO;
using System.Xml;


namespace ZK.MControllers
{
    public class UserController : Controller
    {
        #region 定义
        BLL.USERS bllUser = new BLL.USERS();
        Model.USERS mdlUser = new Model.USERS();

        BLL.DEPARTMENTS bllDep = new DEPARTMENTS();
        Model.DEPARTMENTS mdlDep = new Model.DEPARTMENTS();

        BLL.DEPARTUSERS bllDepAndUser = new BLL.DEPARTUSERS();
        Model.DEPARTUSERS mdlDepAndUser = new Model.DEPARTUSERS();

        BLL.GROUPS bllGroup = new GROUPS();
        BLL.GROUPMEMBERS bllGroupMembers = new GROUPMEMBERS();

        BLL.CONTACTGROUPS bllContactsGroup = new CONTACTGROUPS();
        BLL.CONTACTS bllContacts = new CONTACTS();
        #endregion        

        #region 接口
        /// <summary>
        /// 判断 用户登录信息及获取用户Id
        /// </summary>
        /// <param name="txtUserNme"> txtUserNmeOrUserId</param>
        /// <param name="txtPWD">txtPWD</param>
        /// <returns></returns>
        public string LoginInfoJosn()
        {
            try
            {
                string msgInfo = "";

                string Account = Request["txtUserNmeOrUserId"];
                string userPWD = Request["txtPWD"];
                string jcbstr = Request["jcb"];
                int strid = 0;
                string strNick = "NoNick";
                string school = "";
                string strPic = "NoPic";
                string strSign = "NoSign";
                string pathFace = "/Files/Faces/";
                int sex = -1;
                string birthday = "";
                //Account = "xujiancheng";
                //userPWD = "123456";
                DataSet ds = CheckByPass(Account, userPWD);
                if (ds.Tables[0].Rows[0][0].ToString() == "0")
                {
                    DataSet dsSchool = bllDep.GetList("DEPARTID=1 and PARENTDEPARTID=0");
                    if (dsSchool.Tables[0].Rows.Count > 0)
                    {
                        List<Model.DEPARTMENTS> listSchool = bllDep.DataTableToList(dsSchool.Tables[0]);
                        school = listSchool[0].DEPARTNAME;
                    }

                    mdlUser = bllUser.GetModel(Convert.ToInt32(ds.Tables[0].Rows[0][1].ToString()));

                    if (mdlUser != null)
                    {
                        strid = Convert.ToInt32(mdlUser.USERID.ToString());
                        strNick = mdlUser.NICKNAME;
                        strPic = mdlUser.FACEFILE;
                        strSign = mdlUser.SIGNATURE;
                        sex = mdlUser.SEX;
                        birthday = mdlUser.BIRTH_YEAR.ToString() + "_" + mdlUser.BIRTH_MONTH.ToString() + "_" + mdlUser.BIRTH_DAY.ToString();
                        if (strPic != null && strPic != "")
                        {
                            strPic = pathFace + strPic;
                        }
                        else
                        {
                            strPic = "NoFaces";
                        }
                        msgInfo = "OK";
                    }
                }
                else if (ds.Tables[0].Rows[0][0].ToString() == "4")
                {
                    msgInfo = "ErrorPwd";//密码不正确
                }
                else
                {
                    msgInfo = "NoUser";//用户不存在
                }

                string strJson = jcbstr + "({\"LoginInfo\":\"" + msgInfo + "\",\"userId\":" + strid + ",\"strnick\":\"" + strNick + "\",\"strSign\":\"" + strSign + "\",\"strPic\":\"" + strPic + "\"" + ",\"sex\":" + sex + ",\"birthday\":\"" + birthday + "\"})";

                return strJson;
            }
            catch
            {
                return "";
            }
        }

        //返回人员列表
        public string UserListLoad()
        {
            string jcbstr = Request["jcb"];
            try
            {
                string userId = Request["userId"];
                StringBuilder JsonString = new StringBuilder();
                DataSet dsDep = bllDep.GetList("PARENTDEPARTID=0 order by ORDERVALUE");
                if (dsDep.Tables[0].Rows.Count > 0)
                {
                    JsonString.Append("{item:");
                    List<Model.DEPARTMENTS> listDep = bllDep.DataTableToList(dsDep.Tables[0]);
                    for (int i = 0; i < listDep.Count; i++)
                    {

                        JsonString.Append("{\"id\":" + listDep[i].DEPARTID + ",\"name\":\"" + listDep[i].DEPARTNAME + "\"");

                        int rootDepId = listDep[i].DEPARTID;
                        DataSet dsChildDep = bllDep.GetList("PARENTDEPARTID=" + rootDepId + " order by ORDERVALUE");
                        if (dsChildDep.Tables[0].Rows.Count > 0)
                        {
                            List<Model.DEPARTMENTS> listChildDep = bllDep.DataTableToList(dsChildDep.Tables[0]);
                            JsonString.Append(",list:[");
                            for (int j = 0; j < listChildDep.Count; j++)
                            {
                                JsonString.Append("{\"id\":" + listChildDep[j].DEPARTID + ",\"name\":\"" + listChildDep[j].DEPARTNAME + "\"");

                                int childDepId = listChildDep[j].DEPARTID;
                                LoadChildNodes(childDepId, JsonString);

                                if (j < listChildDep.Count - 1)
                                {
                                    JsonString.Append("},");
                                }
                                else
                                {
                                    JsonString.Append("}");
                                }
                            }

                            //加载没有部门的人员
                            LoadOtherUser(JsonString);

                            JsonString.Append("]");
                        }
                        if (i < listDep.Count - 1)
                        {
                            JsonString.Append("},");
                        }
                        else
                        {
                            JsonString.Append("}");
                        }
                    }

                    JsonString.Append("}");
                    return jcbstr + "(" + JsonString + ")";
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

        /// <summary>
        /// 根据查询条件获取对应人员
        /// </summary>
        /// <param name="txtSearch"> txtSearch</param>
        /// <returns></returns>
        public string SearchJsonInfo()
        {
            string jcbstr = Request["jcb"];
            try
            {
                string txtSearch = Request["txtSearch"];
                //txtSearch = "超";
                DataSet ds = bllUser.GetList("ACTUALNAME like '%" + txtSearch + "%'");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<ZK.Model.USERS> listUser = new List<Model.USERS>();
                    listUser = bllUser.DataTableToList(ds.Tables[0]);

                    return jcbstr + "({" + CreateJsonParametersSearch(ds.Tables[0]) + "})";
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

        /// <summary>
        /// 根据 用户Id 获取 用户群组
        /// </summary>
        /// <param name="userId" > userId</param>
        /// <returns></returns>
        public string MyGroupInfoJson()
        {
            string jcbstr = Request["jcb"];
            try
            {
                string userID = Request["userId"];
                //userID = "10022";
                string strWhere = " 1=1";
                StringBuilder JsonString = new StringBuilder();
                if (userID != null && userID != "")
                {
                    strWhere += " and MEMBERID=" + Convert.ToInt32(userID);

                    DataSet ds = bllGroupMembers.GetList(strWhere);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        JsonString.Append("{groups:[");
                        List<Model.GROUPMEMBERS> listGroups = new List<Model.GROUPMEMBERS>();
                        listGroups = bllGroupMembers.DataTableToList(ds.Tables[0]);
                        for (int i = 0; i < listGroups.Count; i++)
                        {
                            //JsonString.Append("\"id\":" + listGroups[i].CONTACTGROUPID + ",\"name\":\"" + listGroups[i].CONTACTGROUPNAME + "\"");
                            int groupId = listGroups[i].GROUPID;
                            DataSet dsContctUserId = bllGroup.GetList("GROUPID=" + groupId);
                            //JsonString.Append(",listuser:[");
                            if (dsContctUserId.Tables[0].Rows.Count > 0)
                            {
                                List<Model.GROUPS> listContactUserId = new List<Model.GROUPS>();
                                listContactUserId = bllGroup.DataTableToList(dsContctUserId.Tables[0]);
                                for (int j = 0; j < listContactUserId.Count; j++)
                                {
                                    if (mdlUser != null)
                                    {
                                        JsonString.Append("{\"id\":" + listContactUserId[0].GROUPID + ",\"name\":\"" + listContactUserId[0].GROUPNAME + "\"");
                                        if (i < listGroups.Count - 1)
                                        {
                                            JsonString.Append("},");
                                        }
                                        else
                                        {
                                            JsonString.Append("}");
                                        }
                                    }
                                }
                            }
                        }
                        JsonString.Append("]}");
                        return jcbstr + "(" + JsonString + ")";
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

        #endregion

        #region 群组管理
        //获得群组下成员
        public string GetUsersOfGroup()
        {
            StringBuilder JsonString = new StringBuilder();
            string jcbstr = Request["jcb"];
            string groupId = Request["groupId"];
            //groupId = "10003";
            DataSet ds = bllUser.GetList("USERID in (select MEMBERID from GROUPMEMBERS where GROUPID=" + Convert.ToInt32(groupId) + ")");
            if(ds.Tables[0].Rows.Count>0)
            {
                JsonString.Append(CreateJsonParametersUser(ds.Tables[0]));
            }
            else
            {
                return jcbstr + "({})";
            }
            return jcbstr + "(" + JsonString + ")";
        }

        //新建群组
        public string AddNewGroup()
        {
            string jcbstr = Request["jcb"];

            return jcbstr + "({code : 1})";
        }
        #endregion

        #region 方法
        /// <summary>
        /// 使用用户名、密码检查用户认证
        /// </summary>
        /// <param name="strUserName"></param>
        /// <param name="strPass"></param>
        /// <returns></returns>
        public DataSet CheckByPass(string strUserName, string strPass)
        {
            string strName = strUserName.Trim();            
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

        private StringBuilder LoadChildNodes(int depId, StringBuilder strJson)
        {
            StringBuilder JsonString = strJson;
            int num = 1;
            DataSet dsDepOrUser = bllDep.GetList("PARENTDEPARTID=" + depId);
            if (dsDepOrUser.Tables[0].Rows.Count > 0)
            {
                List<Model.DEPARTMENTS> listChildDep = bllDep.DataTableToList(dsDepOrUser.Tables[0]);
                JsonString.Append(",list" + num + ":[");
                for (int i = 0; i < listChildDep.Count; i++)
                {                    
                    JsonString.Append("{\"id\":" + listChildDep[i].DEPARTID + ",\"name\":\"" + listChildDep[i].DEPARTNAME + "\"");

                    int childDepId = listChildDep[i].DEPARTID;
                    LoadChildNodes(childDepId, JsonString);

                    if (i < listChildDep.Count - 1)
                    {
                        JsonString.Append("},");
                    }
                    else
                    {
                        JsonString.Append("}");
                    }
                }
                JsonString.Append("]");
            }
            //else
            {
                DataSet dsUserOfDep = bllDepAndUser.GetList("DEPARTID=" + depId);
                if (dsUserOfDep.Tables[0].Rows.Count > 0)
                {
                    List<Model.DEPARTUSERS> listUserOfDep = bllDepAndUser.DataTableToList(dsUserOfDep.Tables[0]);

                    string userId = "";
                    for (int i = 0; i < listUserOfDep.Count; i++)
                    {
                        if (i < listUserOfDep.Count - 1)
                        {
                            userId += listUserOfDep[i].USERID + ",";
                        }
                        else
                        {
                            userId += listUserOfDep[i].USERID;
                        }
                    }
                    DataSet dsUser = bllUser.GetList("userid in (" + userId + ")");
                    if (dsUser.Tables[0].Rows.Count > 0)
                    {
                        //JsonString.Append(",userlist:[");
                        JsonString.Append(CreateJsonParametersUser(dsUser.Tables[0]));
                        //JsonString.Append("]");
                    }
                }
                //else
                //{
                //    JsonString.Append(",userlist:[{}]");
                //}
            }

            return JsonString;
        }

        private StringBuilder LoadOtherUser(StringBuilder strJson)
        {
            StringBuilder JsonString = strJson;

            #region 无部门人员
            List<int> arrryDepList = new List<int>();
            string arryStr = "";
            DataSet dsChildDep = bllDepAndUser.GetAllList();
            if (dsChildDep.Tables[0].Rows.Count > 0)
            {
                List<Model.DEPARTUSERS> listChildDep = new List<Model.DEPARTUSERS>();
                listChildDep = bllDepAndUser.DataTableToList(dsChildDep.Tables[0]);
                for (int m = 0; m < listChildDep.Count; m++)
                {
                    arrryDepList.Add(listChildDep[m].DEPARTID);
                }
                for (int n = 0; n < arrryDepList.Count; n++)
                {
                    if (n < arrryDepList.Count - 1)
                    {
                        arryStr += arrryDepList[n] + ",";
                    }
                    else
                        arryStr += arrryDepList[n];
                }
            }
            DataSet dsUser = bllUser.GetList("DEPARTID not in (" + arryStr + ")");
            if (dsUser.Tables[0].Rows.Count > 0)
            {
                string listOtherUser = CreateJsonParametersUser(dsUser.Tables[0]);
                return JsonString.Append(",{\"id\":\"otherId\",\"name\":\"otherName\"" + listOtherUser + "}");
            }
            #endregion

            return JsonString;
        }

        public string CreateJsonParametersUser(DataTable dt)
        {
            StringBuilder JsonString = new StringBuilder();
            string pathFace = "/Files/Faces/";
            if (dt != null && dt.Rows.Count > 0)
            {
                JsonString.Append(",list:[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    JsonString.Append("{");
                    if (i < dt.Rows.Count)
                    {
                        JsonString.Append("\"" + dt.Columns[29].ColumnName.ToString() + "\":\"" + pathFace + dt.Rows[i][29].ToString() + "\"");
                        JsonString.Append(",\"" + dt.Columns[0].ColumnName.ToString() + "\":" + dt.Rows[i][0].ToString());
                        JsonString.Append(",\"" + dt.Columns[6].ColumnName.ToString() + "\":" + "\"" + dt.Rows[i][6].ToString() + "\"");
                        JsonString.Append(",\"" + dt.Columns[7].ColumnName.ToString() + "\":" + dt.Rows[i][7].ToString());
                    }
                    if (i == dt.Rows.Count - 1)
                    {
                        JsonString.Append("} ");
                    }
                    else
                    {
                        JsonString.Append("}, ");
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

        public string CreateJsonParametersSearch(DataTable dt)
        {
            StringBuilder JsonString = new StringBuilder();
            string pathFace = "/Files/Faces/";
            if (dt != null && dt.Rows.Count > 0)
            {
                JsonString.Append("listTest:[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    JsonString.Append("{");
                    if (i < dt.Rows.Count)
                    {
                        JsonString.Append("\"" + dt.Columns[29].ColumnName.ToString() + "\":\"" + pathFace + dt.Rows[i][29].ToString() + "\"");
                        JsonString.Append(",\"" + dt.Columns[0].ColumnName.ToString() + "\":" + dt.Rows[i][0].ToString());
                        JsonString.Append(",\"" + dt.Columns[6].ColumnName.ToString() + "\":" + "\"" + dt.Rows[i][6].ToString() + "\"");
                        JsonString.Append(",\"" + dt.Columns[7].ColumnName.ToString() + "\":" + dt.Rows[i][7].ToString());
                    }
                    if (i == dt.Rows.Count - 1)
                    {
                        JsonString.Append("} ");
                    }
                    else
                    {
                        JsonString.Append("}, ");
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

        #endregion
       
        #region 未用

        #region
        public string ChildDepLoad(string depId)
        {
            string jcbstr = Request["jcb"];
            try
            {
                DataSet dsDep = bllDep.GetList("PARENTDEPARTID=" + Convert.ToInt32(depId));

                if (dsDep.Tables[0].Rows.Count > 0)
                {
                    List<Model.DEPARTMENTS> listdep = bllDep.DataTableToList(dsDep.Tables[0]);
                    listdep = bllDep.DataTableToList(dsDep.Tables[0]);

                    return jcbstr + "(" + CreateJsonParametersDep(dsDep.Tables[0]) + ")";
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

        public string CreateJsonParametersDep(DataTable dt)
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
                        JsonString.Append("\"" + dt.Columns[0].ColumnName.ToString() + "\":" + "\"" + dt.Rows[i][0].ToString() + "\"");
                        JsonString.Append("\"" + dt.Columns[1].ColumnName.ToString() + "\":" + "\"" + dt.Rows[i][1].ToString() + "\"");
                    }
                    if (i == dt.Rows.Count - 1)
                    {
                        JsonString.Append("} ");
                    }
                    else
                    {
                        JsonString.Append("}, ");
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

        public string OtherUserJson(string jcbstr, string otherId)
        {
            #region 无部门人员
            List<int> arrryDepList = new List<int>();
            string arryStr = "";
            DataSet dsAllDep = bllDep.GetAllList();
            if (dsAllDep.Tables[0].Rows.Count > 0)
            {
                List<Model.DEPARTMENTS> listAllDep = new List<Model.DEPARTMENTS>();
                listAllDep = bllDep.DataTableToList(dsAllDep.Tables[0]);
                for (int m = 0; m < listAllDep.Count; m++)
                {
                    arrryDepList.Add(listAllDep[m].DEPARTID);
                }
                for (int n = 0; n < arrryDepList.Count; n++)
                {
                    if (n < arrryDepList.Count - 1)
                    {
                        arryStr += arrryDepList[n] + ",";
                    }
                    else
                        arryStr += arrryDepList[n];
                }
            }
            DataSet dsUser = bllUser.GetList("DEPARTID not in (" + arryStr + ")");
            if (dsUser.Tables[0].Rows.Count > 0)
            {
                string listOtherUser = CreateJsonParameters(dsUser.Tables[0]);
                return listOtherUser;
            }
            else
            {
                return jcbstr + "({})";
            }

            #endregion
        }

        public string DepJsonInfo1()
        {
            string jcbstr = Request["jcb"];
            try
            {
                BLL.DEPARTMENTS blldep0 = new BLL.DEPARTMENTS();
                Model.DEPARTMENTS mdldep0 = new Model.DEPARTMENTS();
                Dictionary<string, string> dicJsonInfo = new Dictionary<string, string>();
                DataSet ds = blldep0.GetList("PARENTDEPARTID = 0");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<Model.DEPARTMENTS> listdep0 = new List<Model.DEPARTMENTS>();
                    listdep0 = blldep0.DataTableToList(ds.Tables[0]);

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (i < 1)
                        {
                            string depInfo = listdep0[i].DEPARTNAME;
                            string depfirst = "";

                            DataSet ds1 = blldep0.GetList("PARENTDEPARTID =" + listdep0[i].DEPARTID + " order by ORDERVALUE");
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                //string depstr = "{\"DEPARTID\":" + listdep0[i].DEPARTID + ",\"DEPARTNAME\":\"" + listdep0[0].DEPARTNAME + "\"}";
                                //dicJsonInfo.Add("公司", depstr);

                                List<Model.DEPARTMENTS> listdep1 = blldep0.DataTableToList(ds1.Tables[0]);
                                for (int j = 0; j < listdep1.Count; j++)
                                {
                                    if (j < listdep1.Count - 1)
                                    {
                                        depfirst += "{\"DEPARTID\":" + listdep1[j].DEPARTID + ",\"DEPARTNAME\":\"" + listdep1[j].DEPARTNAME + "\"},";
                                    }
                                    else
                                    {
                                        depfirst += "{\"DEPARTID\":" + listdep1[j].DEPARTID + ",\"DEPARTNAME\":\"" + listdep1[j].DEPARTNAME + "\"}";
                                    }
                                }
                                dicJsonInfo.Add("子部门", depfirst);
                            }

                        }
                    }
                }

                dicJsonInfo.Add("其他", "{\"DEPARTID\":\"otherId\",\"DEPARTNAME\":\"其他人员\"}");

                string strJson = "listTest:[";
                int sum = 0;
                foreach (var item in dicJsonInfo)
                {
                    sum++;
                    if (sum < dicJsonInfo.Count)
                    {
                        strJson += "" + item.Value + ",";
                    }
                    else
                    {
                        strJson += "" + item.Value;
                    }
                }
                strJson += "]";
                return jcbstr + "({" + strJson + "})";
            }
            catch
            {
                return jcbstr + "({})";
            }
        }



        public string UserListLoad1()
        {
            string jcbstr = Request["jcb"];
            try
            {
                string userId = Request["userId"];
                DataSet dsUser = bllUser.GetList("userid=" + userId);
                if (dsUser.Tables[0].Rows.Count > 0)
                {
                    List<Model.USERS> listUser = bllUser.DataTableToList(dsUser.Tables[0]);
                    int depId = listUser[0].DEPARTID;


                }

                return jcbstr + "({})";
            }
            catch
            {
                return jcbstr + "({})";
            }
        }
        #endregion

        #region
        /// <summary>
        ///  获取用户列表
        /// </summary>
        /// <param name="txtSearch"> txtSearch</param>
        /// <returns></returns>
        public string UserListJosn()
        {
            try
            {
                ArrayList Dep = new ArrayList();
                ArrayList DepUser = new ArrayList();

                string txtSearch = Request["txtSearch"];
                string jcbstr = Request["jcb"];
                string strWhere = "1=1";

                if (txtSearch != null && txtSearch != "")
                {
                    strWhere += " and username like '%" + txtSearch + "%'";
                }
                strWhere += " order by DEPARTID";

                Dictionary<string, string> dicJsonInfo = new Dictionary<string, string>();

                List<Model.DEPARTMENTS> listDep = new List<Model.DEPARTMENTS>();
                Dictionary<int, string> dicDepartment = new Dictionary<int, string>();
                DataSet dsDep = bllDep.GetList("PARENTDEPARTID <> 0");
                if (dsDep.Tables[0].Rows.Count > 0)
                {
                    listDep = bllDep.DataTableToList(dsDep.Tables[0]);
                    for (int j = 0; j < listDep.Count; j++)
                    {
                        List<ZK.Model.USERS> listUser = new List<Model.USERS>();
                        DataSet dsUser = bllUser.GetList(" DEPARTID=" + listDep[j].DEPARTID);
                        if (dsUser.Tables[0].Rows.Count > 0)
                        {
                            listUser = bllUser.DataTableToList(dsUser.Tables[0]);
                            string depInfo = listDep[j].DEPARTNAME;
                            string userInfo = " list:[";
                            for (int i = 0; i < listUser.Count; i++)
                            {

                                if (i < listUser.Count - 1)
                                {
                                    userInfo += "{USERID:" + listUser[i].USERID + ",USERNAME:\"" + listUser[i].USERNAME + "\",SEX:" + listUser[i].SEX + "},";
                                }
                                else
                                {
                                    userInfo += "{USERID:" + listUser[i].USERID + ",USERNAME:\"" + listUser[i].USERNAME + "\",SEX:" + listUser[i].SEX + "}]}";
                                }
                            }

                            dicJsonInfo.Add(depInfo, userInfo);
                        }

                    }
                }

                #region
                //List<ZK.Model.USERS> listUser = new List<Model.USERS>();
                //DataSet dsUser = bllUser.GetList(strWhere);
                //if (dsUser.Tables[0].Rows.Count > 0)
                //{
                //    listUser = bllUser.DataTableToList(dsUser.Tables[0]);
                //    int reDepid=99990; 
                //    if (listUser.Count > 0)
                //    {
                //        for (int j = 0; j < listUser.Count; j++)
                //        {   
                //            if (listUser[j].DEPARTID != reDepid)
                //            {
                //                dicDepartment.Add(listUser[j].DEPARTID, listUser[j].DEPARTNAME);                               
                //            }

                //            reDepid = listUser[j].DEPARTID;
                //        }
                //        dicDepartment.Add(987654, "other");
                //        for (int i = 0; i < listUser.Count; i++)
                //        {
                //            foreach (var item in dicDepartment)
                //            {
                //                string depInfo = "";
                //                string userInfo = "";
                //                if (listUser[i].DEPARTID == item.Key)
                //                {
                //                    depInfo = dicDepartment.Values.ToString();
                //                    userInfo += "{USERID:" + listUser[i].USERID + "USERNAME:" + listUser[i].USERNAME + "SEX:" + listUser[i].SEX+"},";
                //                    dicJsonInfo.Add(depInfo, userInfo);
                //                }
                //                else
                //                {
                //                    depInfo = "987654";
                //                    userInfo = "list:[{USERID:" + listUser[i].USERID + "USERNAME:" + listUser[i].USERNAME + "SEX:" + listUser[i].SEX;
                //                    dicJsonInfo.Add(depInfo, userInfo);
                //                }
                //            }
                //        }
                //    }
                //}
                #endregion

                string strJson = "listTest:[";
                int sum = 0;
                foreach (var item in dicJsonInfo)
                {
                    sum++;
                    if (sum < dicJsonInfo.Count)
                    {
                        strJson += "{gn:\"" + item.Key + "\"," + item.Value + ",";
                    }
                    else
                    {
                        strJson += "{gn:\"" + item.Key + "\"," + item.Value;
                    }
                }
                strJson += "]";
                return jcbstr + "({" + strJson + "})";

                //JavaScriptSerializer jss = new JavaScriptSerializer();
                //return SerializeJsonString(jss.Serialize(dicJsonInfo), jcbstr);

            }
            catch
            {
                return "";
            }

        }

        public string UserAllJsonInfo()
        {
            string jcbstr = Request["jcb"];
            try
            {
                ZK.BLL.DEPARTUSERS bllUserDep = new DEPARTUSERS();
                ZK.Model.DEPARTUSERS mdlUser = new Model.DEPARTUSERS();

                DataSet dsdep = bllUserDep.GetAllList();
                if (dsdep.Tables[0].Rows.Count > 0)
                {
                    List<Model.DEPARTUSERS> listUserDep = new List<Model.DEPARTUSERS>();
                    listUserDep = bllUserDep.DataTableToList(dsdep.Tables[0]);
                    for (int i = 0; i < listUserDep.Count; i++)
                    {

                    }

                    return jcbstr + "({})";
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
        #endregion


        private static string SerializeJsonString(string DataList, string jcbstr)
        {
            return jcbstr + "({listTest:[" + DataList + "]})";
        }

        public string CreateJsonParameters(DataTable dt)
        {
            StringBuilder JsonString = new StringBuilder();
            //Exception Handling        
            if (dt != null && dt.Rows.Count > 0)
            {
                JsonString.Append("{listTest:[");
                //JsonString.Append("\"listTest\":[");
                //JsonString.Append("{gn:\"" + dt.Rows[0][25].ToString()+"\",");  
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    JsonString.Append("{");
                    if (i < dt.Rows.Count)
                    {
                        //JsonString.Append("\"" + dt.Columns[25].ColumnName.ToString() + "\":" + "\"" + dt.Rows[i][25].ToString() + "\"");
                        JsonString.Append("\"" + dt.Columns[0].ColumnName.ToString() + "\":" + dt.Rows[i][0].ToString());
                        JsonString.Append(",\"" + dt.Columns[6].ColumnName.ToString() + "\":" + "\"" + dt.Rows[i][6].ToString() + "\"");
                        JsonString.Append(",\"" + dt.Columns[7].ColumnName.ToString() + "\":" + dt.Rows[i][7].ToString());
                    }
                    if (i == dt.Rows.Count - 1)
                    {
                        JsonString.Append("} ");
                    }
                    else
                    {
                        JsonString.Append("}, ");
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

        /// <summary>
        /// 判断 用户登录信息及获取用户Id
        /// </summary>
        /// <param name="txtUserNme"> txtUserNmeOrUserId</param>
        /// <param name="txtPWD">txtPWD</param>
        /// <returns></returns>
        public string LoginInfoJosn1()
        {
            try
            {
                string msgInfo = "";

                string Account = Request["txtUserNmeOrUserId"];
                string userPWD = Request["txtPWD"];
                string jcbstr = Request["jcb"];
                int strid = 0;
                string strNick = "NoNick";
                string mdPwd = "";
                string school = "";
                string strPic = "NoPic";
                string strSign = "NoSign";
                string pathFace = "/Files/Faces/";
                int sex = -1;
                string birthday = "";
                //Account = "bojianchao";
                //userPWD = "123456";
                if (userPWD != null && userPWD != "")
                {
                    mdPwd = ZK.Common.StringPlus.StringToMD5(userPWD);//转换成MD5
                }
                string strWhere = "  USERNAME='" + Account + "'";

                DataSet ds = bllUser.GetList(strWhere);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataSet dsSchool = bllDep.GetList("DEPARTID=1 and PARENTDEPARTID=0");
                    if (dsSchool.Tables[0].Rows.Count > 0)
                    {
                        List<Model.DEPARTMENTS> listSchool = bllDep.DataTableToList(dsSchool.Tables[0]);
                        school = listSchool[0].DEPARTNAME;
                    }

                    List<ZK.Model.USERS> listUser = new List<Model.USERS>();
                    listUser = bllUser.DataTableToList(ds.Tables[0]);

                    for (int i = 0; i < listUser.Count; i++)
                    {
                        if (mdPwd == listUser[i].PWD)
                        {
                            //htUserId.Add("userid", listUser[i].USERID);
                            strid = Convert.ToInt32(listUser[i].USERID.ToString());
                            strNick = listUser[i].NICKNAME;
                            strPic = listUser[i].FACEFILE;
                            strSign = listUser[i].SIGNATURE;
                            sex = listUser[i].SEX;
                            birthday = listUser[i].BIRTH_YEAR.ToString() + "_" + listUser[i].BIRTH_MONTH.ToString() + "_" + listUser[i].BIRTH_DAY.ToString();
                            if (strPic != null && strPic != "")
                            {
                                strPic = pathFace + strPic;
                            }
                            else
                            {
                                strPic = "NoFaces";
                            }
                            msgInfo = "OK";
                            break;
                        }
                    }
                    if (msgInfo != "OK")
                    {
                        msgInfo = "ErrorPwd";//密码不正确
                    }
                }
                else
                {
                    msgInfo = "NoUser";//用户不存在
                }

                string strJson = jcbstr + "({\"LoginInfo\":\"" + msgInfo + "\",\"userId\":" + strid + ",\"strnick\":\"" + strNick + "\",\"strSign\":\"" + strSign + "\",\"strPic\":\"" + strPic + "\"" + ",\"sex\":" + sex + ",\"birthday\":\"" + birthday + "\"})";

                return strJson;

                #region 注释
                //htLoginInfo.Add("username", msgInfo);

                //loginInfo.Add(htLoginInfo);
                //if (msgInfo == "OK")
                //{
                //    loginInfo.Add(htUserId);
                //}

                //string strJson = "{\"msgInfo\":222\"}";
                //string str = "jcb({\"CustomerID\" : 11})";
                //JavaScriptSerializer jss = new JavaScriptSerializer();
                //return Json(strJson,JsonRequestBehavior.AllowGet);
                //return Json(jss.Serialize(loginInfo), JsonRequestBehavior.AllowGet);
                #endregion

            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 根据 用户Id 获取 用户群组
        /// </summary>
        /// <param name="userId" > userId</param>
        /// <returns></returns>
        public string GroupInfoJson()
        {
            string jcbstr = Request["jcb"];
            try
            {
                string userID = Request["userId"];
                userID = "10022";
                string strWhere = " 1=1";
                StringBuilder JsonString = new StringBuilder();
                if (userID != null && userID != "")
                {
                    strWhere += " and userid=" + Convert.ToInt32(userID);

                    DataSet ds = bllGroup.GetList(strWhere);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        JsonString.Append("{groups:[{");
                        List<Model.CONTACTGROUPS> listGroups = new List<Model.CONTACTGROUPS>();
                        listGroups = bllContactsGroup.DataTableToList(ds.Tables[0]);
                        for (int i = 0; i < listGroups.Count; i++)
                        {
                            JsonString.Append("\"id\":" + listGroups[i].CONTACTGROUPID + ",\"name\":\"" + listGroups[i].CONTACTGROUPNAME + "\"");
                            int groupId = listGroups[i].CONTACTGROUPID;
                            DataSet dsContctUserId = bllContacts.GetList("CONTACTGROUPID=" + groupId + " and userid=" + userID);
                            JsonString.Append(",listuser:[");
                            if (dsContctUserId.Tables[0].Rows.Count > 0)
                            {
                                List<Model.CONTACTS> listContactUserId = new List<Model.CONTACTS>();
                                listContactUserId = bllContacts.DataTableToList(dsContctUserId.Tables[0]);
                                for (int j = 0; j < listContactUserId.Count; j++)
                                {
                                    int userId = listContactUserId[i].CONTACTID;
                                    ZK.Model.USERS mdlUser = bllUser.GetModel(userId);
                                    if (mdlUser != null)
                                    {
                                        JsonString.Append("{\"id\":" + mdlUser.USERID + ",\"name\":\"" + mdlUser.USERNAME + "\"");
                                        if (j < listContactUserId.Count - 1)
                                        {
                                            JsonString.Append("},");
                                        }
                                        else
                                        {
                                            JsonString.Append("}]");
                                        }
                                    }
                                    else
                                    {
                                        JsonString.Append("{}]");
                                    }
                                }
                            }
                            else
                            {
                                JsonString.Append("{}]");
                            }
                            if (i < listGroups.Count - 1)
                            {
                                JsonString.Append("},{");
                            }
                        }
                        JsonString.Append("}]}");
                        return jcbstr + "(" + JsonString + ")";
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

        /// <summary>
        /// 获得一级部门
        /// </summary>
        /// <returns></returns>
        public string DepJsonInfo()
        {
            string jcbstr = Request["jcb"];
            try
            {
                BLL.DEPARTMENTS blldep0 = new BLL.DEPARTMENTS();
                Model.DEPARTMENTS mdldep0 = new Model.DEPARTMENTS();
                Dictionary<string, string> dicJsonInfo = new Dictionary<string, string>();
                DataSet ds = blldep0.GetList("PARENTDEPARTID = 0");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<Model.DEPARTMENTS> listdep0 = new List<Model.DEPARTMENTS>();
                    listdep0 = blldep0.DataTableToList(ds.Tables[0]);

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string depInfo = listdep0[i].DEPARTNAME;
                        string depfirst = " list:[";

                        DataSet ds1 = blldep0.GetList("PARENTDEPARTID =" + listdep0[i].DEPARTID + " order by ORDERVALUE");
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            List<Model.DEPARTMENTS> listdep1 = blldep0.DataTableToList(ds1.Tables[0]);
                            for (int j = 0; j < listdep1.Count; j++)
                            {
                                if (j < listdep1.Count - 1)
                                {
                                    depfirst += "{\"DEPARTID\":" + listdep1[j].DEPARTID + ",\"DEPARTNAME\":\"" + listdep1[j].DEPARTNAME + "\"},";
                                }
                                else
                                {
                                    depfirst += "{\"DEPARTID\":" + listdep1[j].DEPARTID + ",\"DEPARTNAME\":\"" + listdep1[j].DEPARTNAME + "\"}]}";
                                }
                            }
                            dicJsonInfo.Add(depInfo, depfirst);
                        }
                        else
                        {
                            dicJsonInfo.Add(depInfo, "list:[{\"DEPARTID\":NoId,\"DEPARTNAME\":\"NoDep\"}]}");
                        }
                    }
                }

                dicJsonInfo.Add("其他", "list:[{\"DEPARTID\":otherId,\"DEPARTNAME\":\"未分部门人员\"}]}");

                string strJson = "listTest:[";
                int sum = 0;
                foreach (var item in dicJsonInfo)
                {
                    sum++;
                    if (sum < dicJsonInfo.Count)
                    {
                        strJson += "{gn:\"" + item.Key + "\"," + item.Value + ",";
                    }
                    else
                    {
                        strJson += "{gn:\"" + item.Key + "\"," + item.Value;
                    }
                }
                strJson += "]";
                return jcbstr + "({" + strJson + "})";
            }
            catch
            {
                return jcbstr + "({})";
            }
        }

        /// <summary>
        /// 根据部门id获取对应人员列表
        /// </summary>
        /// <param name="depId"> depId</param>
        /// <returns></returns>
        public string UsersOfDep()
        {
            string jcbstr = Request["jcb"];
            try
            {
                string depId = Request["depId"];

                if (depId != "otherId")
                {
                    DataSet dsDep = bllDep.GetList("PARENTDEPARTID=" + Convert.ToInt32(depId));
                    if (dsDep.Tables[0].Rows.Count > 0)
                    {
                        return ChildDepLoad(depId);
                    }
                    else
                    {
                        List<Model.DEPARTMENTS> listdep = bllDep.DataTableToList(dsDep.Tables[0]);

                        DataSet ds = bllUser.GetList("DEPARTID=" + Convert.ToInt32(depId));
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            List<ZK.Model.USERS> listUser = new List<Model.USERS>();
                            listUser = bllUser.DataTableToList(ds.Tables[0]);

                            return jcbstr + "(" + CreateJsonParameters(ds.Tables[0]) + ")";
                        }
                        else
                        {
                            return jcbstr + "({})";
                        }
                    }
                }
                else
                {
                    string otherUserStr = OtherUserJson(jcbstr, depId);
                    return jcbstr + "(" + otherUserStr + ")";
                }
            }
            catch
            {
                return jcbstr + "({})";
            }
        }

        #endregion

    }
}
