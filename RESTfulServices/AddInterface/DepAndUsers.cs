using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;

namespace RESTfulServices.AddInterface
{
    public class DepAndUsers
    {
        #region 定义
        static ZK.BLL.DEPARTMENTS bllDep = new ZK.BLL.DEPARTMENTS();
        static ZK.BLL.USERS bllUser = new ZK.BLL.USERS();
        static ZK.BLL.DEPARTUSERS bllUserOfDep = new ZK.BLL.DEPARTUSERS();
        #endregion

        #region 接口
        /// <summary>
        /// 获取所有部门
        /// </summary>
        /// <returns></returns>
        public static string AllDepInfo()
        {
            try
            {
                StringBuilder JsonString = new StringBuilder();
                DataSet dsDep = bllDep.GetList("PARENTDEPARTID=0 order by ORDERVALUE");
                if (dsDep.Tables[0].Rows.Count > 0)
                {
                    //JsonString.Append("{item:");
                    List<ZK.Model.DEPARTMENTS> listDep = bllDep.DataTableToList(dsDep.Tables[0]);
                    for (int i = 0; i < listDep.Count; i++)
                    {

                        JsonString.Append("{\"id\":" + listDep[i].DEPARTID + ",\"pid\":" + listDep[i].PARENTDEPARTID + ",\"name\":\"" + listDep[i].DEPARTNAME + "\"");

                        int rootDepId = listDep[i].DEPARTID;
                        DataSet dsChildDep = bllDep.GetList("PARENTDEPARTID=" + rootDepId + " order by ORDERVALUE");
                        if (dsChildDep.Tables[0].Rows.Count > 0)
                        {
                            List<ZK.Model.DEPARTMENTS> listChildDep = bllDep.DataTableToList(dsChildDep.Tables[0]);
                            JsonString.Append(",\"list\":[");
                            for (int j = 0; j < listChildDep.Count; j++)
                            {
                                JsonString.Append("{\"id\":" + listChildDep[j].DEPARTID + ",\"pid\":" + listChildDep[i].PARENTDEPARTID + ",\"name\":\"" + listChildDep[j].DEPARTNAME + "\"");

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

                    //JsonString.Append("}");
                    return JsonString.ToString();
                }
                else
                {
                    return "";
                }

            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 按条件查询人员
        /// </summary>
        /// <returns></returns>
        public static string AllUserInfo1(string username)
        {
            try
            {
                string JsonString = "";
                DataSet ds = bllUser.GetAllList();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    JsonString = CreateJsonParameters(ds.Tables[0], username);
                }

                return JsonString;
            }
            catch
            {
                return "";
            }

        }

        /// <summary>
        /// 按条件查询人员
        /// </summary>
        /// <returns></returns>
        public static string AllUserInfo()
        {
            try
            {
                string JsonString = "";
                DataSet ds = bllUser.GetAllList();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    JsonString = CreateJsonParameters1(ds.Tables[0]);
                }

                return JsonString;
            }
            catch
            {
                return "";
            }

        }

        /// <summary>
        /// 按条件查询人员
        /// </summary>
        /// <param name="strSearchName" > strSearchName</param>
        /// <returns></returns>
        public static string SearchUserInfo(string strSearchName)
        {
            try
            {
                string JsonString = "";
                if (strSearchName.Trim() != "")
                {
                    string strSql = "ACTUALNAME like '%" + strSearchName + "%'";
                    DataSet ds = bllUser.GetList(strSql);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        JsonString = CreateJsonParameters1(ds.Tables[0]);
                    }
                }
                return JsonString;
            }
            catch
            {
                return "";
            }

        }

        

        //modify by ao 2014-03-20
        /// <summary>
        /// 根据部门id获取对应人员列表
        /// </summary>
        /// <param name="depid"> depid</param>
        /// <returns></returns>
        public static string UsersOfDep(string depid, string userName)
        {
            try
            {
                string depId = depid;
                string JsonString = "";
                //根据pid得到所有子部门
                DataSet dsDep = bllDep.GetList("PARENTDEPARTID=" + Convert.ToInt32(depId));
                if (dsDep.Tables[0].Rows.Count > 0)
                {
                    List<ZK.Model.DEPARTMENTS> listDeps = bllDep.DataTableToList(dsDep.Tables[0]);
                    List<int> listId = new List<int>();
                    for (int i = 0; i < listDeps.Count; i++)
                    {
                        listId.Add(listDeps[i].DEPARTID);
                    }
                    if (listId.Count > 0)
                    {
                        for (int m = 0; m < listId.Count; m++)
                        {
                            DataSet dsUserOfDepartment = bllUserOfDep.GetList("DEPARTID=" + listId[m]);
                            if (dsUserOfDepartment.Tables[0].Rows.Count > 0)
                            {
                                JsonString = UserOfDepartment(dsUserOfDepartment, userName);
                            }
                        }
                    }
                }

                {
                    DataSet ds = bllUserOfDep.GetList("DEPARTID=" + Convert.ToInt32(depId));
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        JsonString = UserOfDepartment(ds, userName);
                    }
                    else
                    {
                        return "";
                    }
                }
                return JsonString;
            }
            catch
            {
                return "";
            }
        }

        public static string GetDepIds()
        {
            try
            {
                string JsonString = "";
                DataSet ds = bllDep.GetAllList();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    JsonString = CreateJsonParametersdep(ds.Tables[0]);
                }

                return JsonString;
            }
            catch
            {
                return "";
            }
        }
        #endregion

        #region 方法

        public static string CreateJsonParametersdep(DataTable dt)
        {
            StringBuilder JsonString = new StringBuilder();
            //Exception Handling        
            if (dt != null && dt.Rows.Count > 0)
            {
                JsonString.Append("{\"list\":[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    JsonString.Append("{");
                    if (i < dt.Rows.Count)
                    {
                        JsonString.Append("\"id\":" + dt.Rows[i][0].ToString());
                        JsonString.Append(",\"pid\":" + "\"" + dt.Rows[i][3].ToString() + "\"");
                        JsonString.Append(",\"name\":" + "\"" + dt.Rows[i][1].ToString() + "\"");
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

                JsonString.Append("]}");
                return JsonString.ToString();
            }
            else
            {
                return null;
            }
        }
        private static StringBuilder LoadChildNodes(int depId, StringBuilder strJson)
        {
            StringBuilder JsonString = strJson;
            DataSet dsDepOrUser = bllDep.GetList("PARENTDEPARTID=" + depId);
            if (dsDepOrUser.Tables[0].Rows.Count > 0)
            {
                List<ZK.Model.DEPARTMENTS> listChildDep = bllDep.DataTableToList(dsDepOrUser.Tables[0]);

                JsonString.Append(",\"list\":[");
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

            return JsonString;
        }

        public static StringBuilder LoadChildDeps(int depId, StringBuilder strJson)
        {
            StringBuilder JsonString = strJson;
            DataSet dsDepOrUser = bllDep.GetList("PARENTDEPARTID=" + depId);
            if (dsDepOrUser.Tables[0].Rows.Count > 0)
            {
                List<ZK.Model.DEPARTMENTS> listChildDep = bllDep.DataTableToList(dsDepOrUser.Tables[0]);
                //JsonString.Append(",\"list\":[");
                for (int i = 0; i < listChildDep.Count; i++)
                {
                    JsonString.Append("{\"id\":" + listChildDep[i].DEPARTID + ",\"name\":\"" + listChildDep[i].DEPARTNAME + "\"");

                    int childDepId = listChildDep[i].DEPARTID;
                    LoadChildNodes(childDepId, JsonString).ToString();

                    DataSet dsDep = bllDep.GetList("PARENTDEPARTID=" + childDepId);
                    if (dsDep == null)
                    {
                        if (i < listChildDep.Count - 1)
                        {
                            JsonString.Append("},");
                        }
                        else
                        {
                            JsonString.Append("}");
                        }
                    }
                }
                JsonString.Append("]");
            }

            return JsonString;
        }

        public static string CreateJsonParameters(DataTable dt, string username)
        {
            StringBuilder JsonString = new StringBuilder();
            //Exception Handling        
            if (dt != null && dt.Rows.Count > 0)
            {
                JsonString.Append("{\"list\":[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (username == dt.Rows[i][1].ToString())
                    {

                        if (i == dt.Rows.Count - 1)
                        {
                            //  JsonString.ToString().TrimEnd(',');
                            JsonString.Remove(JsonString.Length - 2, 1);
                        }
                        continue;
                    }

                    else
                    {
                        JsonString.Append("{");
                        if (i < dt.Rows.Count)
                        {
                            JsonString.Append("\"" + dt.Columns[0].ColumnName.ToString() + "\":" + dt.Rows[i][0].ToString());
                            JsonString.Append(",\"" + dt.Columns[6].ColumnName.ToString() + "\":" + "\"" + dt.Rows[i][6].ToString() + "\"");
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
                }

                JsonString.Append("]}");
                return JsonString.ToString();
            }
            else
            {
                return null;
            }
        }

        public static string CreateJsonParameters1(DataTable dt)
        {
            StringBuilder JsonString = new StringBuilder();
            //Exception Handling        
            if (dt != null && dt.Rows.Count > 0)
            {
                JsonString.Append("{\"list\":[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    JsonString.Append("{");
                    if (i < dt.Rows.Count)
                    {
                        JsonString.Append("\"" + dt.Columns[0].ColumnName.ToString() + "\":" + dt.Rows[i][0].ToString());
                        JsonString.Append(",\"" + dt.Columns[6].ColumnName.ToString() + "\":" + "\"" + dt.Rows[i][6].ToString() + "\"");
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

                JsonString.Append("]}");
                return JsonString.ToString();
            }
            else
            {
                return null;
            }
        }

        private static string UserOfDepartment(DataSet ds, string userName)
        {
            string JsonString = "";
            List<ZK.Model.DEPARTUSERS> listUserOfDep = new List<ZK.Model.DEPARTUSERS>();
            listUserOfDep = bllUserOfDep.DataTableToList(ds.Tables[0]);
            JsonString = "{\"list\":[";
            for (int j = 0; j < listUserOfDep.Count; j++)
            {
                int userId = listUserOfDep[j].USERID;

                ZK.Model.USERS mdlUser = bllUser.GetModel(userId);
                if (mdlUser != null)
                {
                    if (userName == mdlUser.USERNAME)
                    {

                        if (j == listUserOfDep.Count - 1)
                        {
                            //  JsonString.ToString().TrimEnd(',');
                            JsonString.Remove(JsonString.Length - 2, 1);
                        }
                        continue;
                    }

                    else
                    {
                        if (j == listUserOfDep.Count - 1)
                        {
                            JsonString += "{\"id\":" + mdlUser.USERID + ",\"name\":\"" + mdlUser.ACTUALNAME + "\"}";
                        }
                        else
                        {
                            JsonString += "{\"id\":" + mdlUser.USERID + ",\"name\":\"" + mdlUser.ACTUALNAME + "\"},";
                        }
                    }
                }
            }
            JsonString += "]}";

            return JsonString;
        }

        #endregion
    }
}