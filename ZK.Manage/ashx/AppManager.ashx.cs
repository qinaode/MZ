using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Script.Serialization;
using ZK.Common;

namespace ZK.Manage.ashx
{
    /// <summary>
    /// AppManager 的摘要说明
    /// </summary>
    public class AppManager : IHttpHandler
    {
        ZK.BLL.WEBAPPS bll = new BLL.WEBAPPS();
        ZK.Model.WEBAPPS mdl = new Model.WEBAPPS();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string Flag = context.Request.Form["Flag"];
            string Result = string.Empty;

            switch (Flag)
            {
                case "GetAppListPaging":
                    Result = GetAppListPaging(context);
                    break;
                case "GetSpecialTopicListPaging":
                    Result = GetSpecialTopicListPaging(context);
                    break;
                case "DeleteCheckedMoralResource":
                    Result = DeleteCheckedMoralResource(context);
                    break;
                case "GetSpecialImgListPaging":
                    Result = GetSpecialImgListPaging(context);
                    break;
                case "DeleteCheckedImgResource":
                    Result = DeleteCheckedImgResource(context);
                    break;
                case "MoveImgResource":
                    Result = MoveImgResource(context);
                    break;
                case "MoveImgResource1":
                    Result = MoveImgResource1(context);
                    break;
                case "GetUserMagListPaging":
                    Result = GetUserMagListPaging(context);
                    break;
                case "GetHaveOtherUserListPaging":
                    Result = GetHaveOtherUserListPaging(context);
                    break;
                //case "GetSpecialResListPaging":
                //    Result = GetSpecialResListPaging(context);
                //    break;
                default:
                    Result = "";
                    break;
            }
            context.Response.Write(Result);
        }

        private string MoveImgResource1(HttpContext context)
        {
            string ID = context.Request.Form["ID"];
            string moveflag = context.Request.Form["moveFlag"];
            int rint = 0;
            if (int.TryParse(ID, out rint))
            {
                Move1(ID, moveflag);
                return "true";
            }
            else
            {
                return "false";
            }
        }

        private void Move1(string levelid, string flag)
        {
           
            ZK.BLL.ZK_FileJPType bllFileJP = new BLL.ZK_FileJPType();
            ZK.Model.ZK_FileJPType mdlFileJP = new ZK.Model.ZK_FileJPType();

            ZK.BLL.ZK_FileJPPic bllFileJPPic = new BLL.ZK_FileJPPic();
            ZK.Model.ZK_FileJPPic mdlFileJPPic = new ZK.Model.ZK_FileJPPic();

            int id = Convert.ToInt32(levelid);
            mdlFileJPPic = bllFileJPPic.GetModel(id);
            
            int depOrder = Convert.ToInt32(mdlFileJPPic.sortNum);
            string where = " and fileJPTypeID= ";
            string strSQL = "";
            if (flag == "Up")
            {
                //strSQL = " sortNum<" + depOrder + " Order by sortNum";
                strSQL = " sortNum>" + depOrder + where +mdlFileJPPic.fileJPTypeID.ToString() + " Order by sortNum";
            }

            if (flag == "Down")
            {
                //strSQL = "sortNum>" + depOrder + " Order by sortNum desc";
                strSQL = "sortNum<" + depOrder + where +mdlFileJPPic.fileJPTypeID.ToString() + " Order by sortNum desc";
            }
            System.Data.DataSet ds = bllFileJPPic.GetList(strSQL);

            List<ZK.Model.ZK_FileJPPic> depList = new List<Model.ZK_FileJPPic>();
            depList = bllFileJPPic.DataTableToList(ds.Tables[0]);
            if (depList.Count > 0)
            {
                //int upid = Convert.ToInt32(depList[depList.Count - 1].sortNum);
                int upid = Convert.ToInt32(depList[0].sortNum);

                //int upOrgid = depList[depList.Count - 1].id;
                int upOrgid = depList[0].id;
                ZK.Model.ZK_FileJPPic depmdlB = new Model.ZK_FileJPPic();
                depmdlB = bllFileJPPic.GetModel(upOrgid);

                ZK.Model.ZK_FileJPPic depmdl1 = new Model.ZK_FileJPPic();
                ZK.Model.ZK_FileJPPic depmdl2 = new Model.ZK_FileJPPic();

                depmdl1.id = mdlFileJPPic.id;
                depmdl1.fileJPTypeID = mdlFileJPPic.fileJPTypeID;
                depmdl1.sortNum = upid;
                depmdl1.imageName = mdlFileJPPic.imageName;
                depmdl1.imageURL = mdlFileJPPic.imageURL;

                depmdl2.id = depmdlB.id;
                depmdl2.fileJPTypeID = depmdlB.fileJPTypeID;
                depmdl2.sortNum = depOrder;
                depmdl2.imageName = depmdlB.imageName;
                depmdl2.imageURL = depmdlB.imageURL;

                bllFileJPPic.Update(depmdl1);
                bllFileJPPic.Update(depmdl2);
            }
           
        }

        private string GetHaveOtherUserListPaging(HttpContext context)
        {
            string PageIndex = context.Request.Form["PageIndex"] == "" ? "1" : context.Request.Form["PageIndex"];
            string PageSize = context.Request.Form["PageSize"] == "" ? "10" : context.Request.Form["PageSize"];
            string strWhere = "1=1";
            string struserid = context.Request.Form["struserid"];
            string strname = context.Request.Form["strName"];
            string depId = context.Request.Form["depId"];

            if (strname != null && strname != "")
            {
                strWhere += " and ACTUALNAME like '%" + strname + "%'";
            }
            if (struserid != null && struserid != "")
            {
                strWhere += " and userid like '%" + struserid + "%'";
            }

            if (depId != null && depId != "")
            {
                strWhere += " and DEPARTID <> " + Convert.ToInt32(depId);
            }
                     
            List<ZK.Model.USERS> List = new List<Model.USERS>();
            ZK.BLL.USERS bllRfile = new BLL.USERS();

            int startindex = (Convert.ToInt32(PageIndex) - 1) * (Convert.ToInt32(PageSize)) + 1;
            int endindex = Convert.ToInt32(PageSize) * Convert.ToInt32(PageIndex);

            DataSet ds = bllRfile.GetListByPage(strWhere, "", startindex, endindex);

            List = bllRfile.DataTableToList(ds.Tables[0]);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            int total = bllRfile.GetRecordCount(strWhere);
            
            return SerializeJsonString(jss.Serialize(List), total);
        }

        private string GetUserMagListPaging(HttpContext context)
        {
            string PageIndex = context.Request.Form["PageIndex"] == "" ? "1" : context.Request.Form["PageIndex"];
            string PageSize = context.Request.Form["PageSize"] == "" ? "10" : context.Request.Form["PageSize"];
            string strWhere = "1=1";
            string struserid = context.Request.Form["struserid"];
            string strname = context.Request.Form["strName"];
            string staste = context.Request.Form["staste"];

            if (strname != null && strname != "")
            {
                strWhere += " and ACTUALNAME like '%" + strname + "%'";                 
            }
            if (struserid != null && struserid != "" )
            {
                strWhere += " and userid like '%" + struserid + "%'";
            }

            if (staste != null && staste != ""&&staste!="-1")
            {
                strWhere += " and USERLOCK= " + Convert.ToInt32(staste);
            }               
    
            List<ZK.Model.USERS> List = new List<Model.USERS>();
           
            ZK.BLL.USERS bllRfile = new BLL.USERS();

            int startindex = (Convert.ToInt32(PageIndex) - 1) * (Convert.ToInt32(PageSize)) + 1;
            int endindex = Convert.ToInt32(PageSize) * Convert.ToInt32(PageIndex);

            DataSet ds = bllRfile.GetListByPage(strWhere, "", startindex, endindex);

            List = bllRfile.DataTableToList(ds.Tables[0]);
         
            JavaScriptSerializer jss = new JavaScriptSerializer();
            int total = bllRfile.GetRecordCount(strWhere);
            return SerializeJsonString(jss.Serialize(List), total);
        }

        private string GetSpecialImgListPaging(HttpContext context)
        {
            string PageIndex = context.Request.Form["PageIndex"] == "" ? "1" : context.Request.Form["PageIndex"];
            string PageSize = context.Request.Form["PageSize"] == "" ? "10" : context.Request.Form["PageSize"];
            string strWhere = context.Request.Form["strWhere"];
            if (strWhere != null && strWhere != "")
            {
                strWhere = " fileJPTypeID=" + strWhere;
            }

            List<ZK.Model.ZK_FileJPPic> List = new List<Model.ZK_FileJPPic>();
            ZK.BLL.ZK_FileJPPic bllFileJPPic = new BLL.ZK_FileJPPic();

            int startindex = (Convert.ToInt32(PageIndex) - 1) * (Convert.ToInt32(PageSize)) + 1;
            int endindex = Convert.ToInt32(PageSize) * Convert.ToInt32(PageIndex);
            string order = " sortNum desc";
            //DataSet ds = bllFileJPPic.GetListByPage(strWhere, "", startindex, endindex);
            DataSet ds = bllFileJPPic.GetListByPage(strWhere, order, startindex, endindex);

            List = bllFileJPPic.DataTableToList(ds.Tables[0]);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            int total = bllFileJPPic.GetRecordCount(strWhere);
            return SerializeJsonString(jss.Serialize(List), total);
        }

        private string MoveImgResource(HttpContext context)
        {
            string ID = context.Request.Form["ID"];
            string moveflag = context.Request.Form["moveFlag"];
            int rint = 0;
            if (int.TryParse(ID, out rint))
            {
                Move(ID, moveflag);
                return "true";
            }
            else
            {
                return "false";
            }
        }

        private void Move(string levelid, string flag)
        {
           
            ZK.BLL.ZK_FileJPType bllFileJP = new BLL.ZK_FileJPType();
            ZK.Model.ZK_FileJPType mdlFileJP = new ZK.Model.ZK_FileJPType();

            ZK.BLL.ZK_FileJPPic bllFileJPPic = new BLL.ZK_FileJPPic();
            ZK.Model.ZK_FileJPPic mdlFileJPPic = new ZK.Model.ZK_FileJPPic();

            int id = Convert.ToInt32(levelid);
            mdlFileJPPic = bllFileJPPic.GetModel(id);

            int depOrder = Convert.ToInt32(mdlFileJPPic.sortNum);

            string strSQL = "";
            if (flag == "Up")
            {
                //strSQL = " sortNum<" + depOrder + " Order by sortNum";
                strSQL = " sortNum>" + depOrder + " Order by sortNum";
            }

            if (flag == "Down")
            {
                //strSQL = "sortNum>" + depOrder + " Order by sortNum desc";
                strSQL = "sortNum<" + depOrder +" Order by sortNum desc";
            }
            System.Data.DataSet ds = bllFileJPPic.GetList(strSQL);

            List<ZK.Model.ZK_FileJPPic> depList = new List<Model.ZK_FileJPPic>();
            depList = bllFileJPPic.DataTableToList(ds.Tables[0]);
            if (depList.Count > 0)
            {
                //int upid = Convert.ToInt32(depList[depList.Count - 1].sortNum);
                int upid = Convert.ToInt32(depList[0].sortNum);

                //int upOrgid = depList[depList.Count - 1].id;
                int upOrgid = depList[0].id;
                ZK.Model.ZK_FileJPPic depmdlB = new Model.ZK_FileJPPic();
                depmdlB = bllFileJPPic.GetModel(upOrgid);

                ZK.Model.ZK_FileJPPic depmdl1 = new Model.ZK_FileJPPic();
                ZK.Model.ZK_FileJPPic depmdl2 = new Model.ZK_FileJPPic();

                depmdl1.id = mdlFileJPPic.id;
                depmdl1.fileJPTypeID = mdlFileJPPic.fileJPTypeID;
                depmdl1.sortNum = upid;
                depmdl1.imageName = mdlFileJPPic.imageName;
                depmdl1.imageURL = mdlFileJPPic.imageURL;

                depmdl2.id = depmdlB.id;
                depmdl2.fileJPTypeID = depmdlB.fileJPTypeID;
                depmdl2.sortNum = depOrder;
                depmdl2.imageName = depmdlB.imageName;
                depmdl2.imageURL = depmdlB.imageURL;

                bllFileJPPic.Update(depmdl1);
                bllFileJPPic.Update(depmdl2);
            }
        }
        //private string GetSpecialResListPaging(HttpContext context)
        //{
        //    string PageIndex = context.Request.Form["PageIndex"] == "" ? "1" : context.Request.Form["PageIndex"];
        //    string PageSize = context.Request.Form["PageSize"] == "" ? "10" : context.Request.Form["PageSize"];

        //    string strSelect = "*";
        //    string strTable = " ZK_FileJP a left join ZK_FileList b on a.fileID=b.fileID left join USERS c on b.ownerID=c.USERID";
        //    string strPrimaryKey = "ZK_FileList.ID";
        //    string strOrderby = "sortNum";             

        //    string strWhere = " typeID=" + Convert.ToInt32(Request.QueryString["specialId"]);
        //    int intBlPage = 1;

        //    int startindex = (Convert.ToInt32(PageIndex) - 1) * (Convert.ToInt32(PageSize)) + 1;
        //    int endindex = Convert.ToInt32(PageSize) * Convert.ToInt32(PageIndex);

        //    List<ZK.Model.ZK_FileJPType> List = new List<Model.ZK_FileJPType>();
        //    BLL.CommondBase bll = new BLL.CommondBase();

        //    DataSet ds = new DataSet();
        //    ds = bll.GetList(strSelect, strTable, strPrimaryKey, strOrderby, PageSize, PageIndex, strWhere, intBlPage);

        //    //string strWhere = context.Request.Form["strWhere"];
        //    //List<ZK.Model.ZK_FileJPType> List = new List<Model.ZK_FileJPType>();
        //    //ZK.BLL.ZK_FileJPType bllRfile = new BLL.ZK_FileJPType();

        //    //int startindex = (Convert.ToInt32(PageIndex) - 1) * (Convert.ToInt32(PageSize)) + 1;
        //    //int endindex = Convert.ToInt32(PageSize) * Convert.ToInt32(PageIndex);

        //    //DataSet ds = bllRfile.GetListByPage(strWhere, "", startindex, endindex);

        //    List = bllRfile.DataTableToList(ds.Tables[0]);
        //    JavaScriptSerializer jss = new JavaScriptSerializer();
        //    int total = bllRfile.GetRecordCount("");
        //    return SerializeJsonString(jss.Serialize(List), total);
        //    return "";
        //}

        private string GetAppListPaging(HttpContext context)
        {
            string PageIndex = context.Request.Form["PageIndex"] == "" ? "1" : context.Request.Form["PageIndex"];
            string PageSize = context.Request.Form["PageSize"] == "" ? "10" : context.Request.Form["PageSize"];
            List<ZK.Model.WEBAPPS> List = new List<Model.WEBAPPS>();
            string orerby = " ordervalue asc ";
            int startindex = (Convert.ToInt32(PageIndex) - 1) * (Convert.ToInt32(PageSize)) + 1;
            int endindex = Convert.ToInt32(PageSize) * Convert.ToInt32(PageIndex);

            DataSet ds = bll.GetListByPage("", orerby, startindex, endindex);

            List = bll.DataTableToList(ds.Tables[0]);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            int total = bll.GetRecordCount("");
            return SerializeJsonString(jss.Serialize(List), total);
        }

        private string GetSpecialTopicListPaging(HttpContext context)
        {
            string PageIndex = context.Request.Form["PageIndex"] == "" ? "1" : context.Request.Form["PageIndex"];
            string PageSize = context.Request.Form["PageSize"] == "" ? "10" : context.Request.Form["PageSize"];
            string strWhere = context.Request.Form["strWhere"];
            if (strWhere != "")
            {
                strWhere = " TypeName like '%" + strWhere + "%'";
            }
            List<ZK.Model.ZK_FileJPType> List = new List<Model.ZK_FileJPType>();
            ZK.BLL.ZK_FileJPType bllRfile = new BLL.ZK_FileJPType();

            int startindex = (Convert.ToInt32(PageIndex) - 1) * (Convert.ToInt32(PageSize)) + 1;
            int endindex = Convert.ToInt32(PageSize) * Convert.ToInt32(PageIndex);

            DataSet ds = bllRfile.GetListByPage(strWhere, "", startindex, endindex);

            List = bllRfile.DataTableToList(ds.Tables[0]);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            int total = bllRfile.GetRecordCount("");
            return SerializeJsonString(jss.Serialize(List), total);
        }

        private string DeleteCheckedImgResource(HttpContext context)
        {
            string ID = context.Request.Form["ID"];
            int rint = 0;
            if (int.TryParse(ID, out rint))
            {
                ZK.BLL.ZK_FileJPPic bllFileJPPic = new BLL.ZK_FileJPPic();
                List<Model.ZK_FileJPPic> lists = bllFileJPPic.GetModelList("id=" + rint.ToString());

                if (lists != null && lists.Count > 0)
                {
                    bool res = bllFileJPPic.Delete(lists[0].id);
                    return res.ToString();
                }
                else
                {
                    return "false";
                }
            }
            else
            {
                return "false";
            }
        }

        private string DeleteCheckedMoralResource(HttpContext context)
        {
            string ID = context.Request.Form["ID"];
            int rint = 0;
            if (int.TryParse(ID, out rint))
            {
                ZK.BLL.ZK_FileJP bllRfile = new BLL.ZK_FileJP();
                ZK.Model.ZK_FileJP mdlRfile = new Model.ZK_FileJP();
                ZK.BLL.ZK_FileJPPic bllFileJPImg = new BLL.ZK_FileJPPic();
                ZK.Model.ZK_FileJPPic mdlFileJPImg = new Model.ZK_FileJPPic();
                string strRfile = " typeID in(select id from ZK_FileJPType where id=" + Convert.ToInt32(ID) + ")";
                string strPic = "fileJPTypeID in(select id from ZK_FileJPType where id=" + Convert.ToInt32(ID) + ")";
                DataSet dsR = bllRfile.GetList(strRfile);
                DataSet dsPic = bllFileJPImg.GetList(strPic);
                if (dsR.Tables[0].Rows.Count > 0 || dsPic.Tables[0].Rows.Count > 0)
                {
                    return "文件夹不为空";
                }
                else
                {
                    BLL.ZK_FileJPType bllcf = new BLL.ZK_FileJPType();
                    List<Model.ZK_FileJPType> lists = bllcf.GetModelList("id=" + rint.ToString());

                    if (lists != null && lists.Count > 0)
                    {
                        bool res = bllcf.Delete(lists[0].id);
                        return res.ToString();
                    }
                    else
                    {
                        return "false";
                    }
                }
            }
            else
            {
                return "false";
            }
        }

        private static string SerializeJsonString(string DataList, int TotalNumber)
        {
            string datalist = DataList;
            return "{\"DataList\":" + DataList + ",\"TotalNumber\":" + TotalNumber + "}";
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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}