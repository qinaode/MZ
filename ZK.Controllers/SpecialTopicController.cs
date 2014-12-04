using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;
using System.Data;

namespace ZK.Controllers
{
    public class SpecialTopicController : Controller
    {
        BLL.View_AllFileList bllv_ALLFile = new BLL.View_AllFileList();
        Model.View_OtherFileList mdlv_otherFile = new Model.View_OtherFileList();
        BLL.CommondBase bllCommondBase = new BLL.CommondBase();

        public ActionResult Index()
        {
            //分页查询列表
            string strKey = Request.QueryString["key"];
            string strcateID = Request.QueryString["cateID"];

            ZK.BLL.ZK_FileList bllalllist = new BLL.ZK_FileList();
            StringBuilder sBuilder = new StringBuilder();
            sBuilder.Append("filename like'%" + strKey + "%'");

            #region 获取列表
            string strSql = "";
            if (Request.QueryString["cid"] == "dypd")
            {
                if (Request.QueryString["type"] == "video")
                {
                    strSql = " channelID=2 and filetypeid=1";
                }
                else if (Request.QueryString["type"] == "img")
                {
                    strSql = " channelID=2 and filetypeid=3";
                }
                else if (Request.QueryString["type"] == "doc")
                {
                    strSql = " channelID=2 and filetypeid=2";
                }
                else
                    strSql = "  channelID=2 ";

            }
            else if (Request.QueryString["cid"] == "xzpd")
            {
                if (Request.QueryString["type"] == "video")
                {
                    strSql = " channelID=3 and filetypeid=1";
                }
                else if (Request.QueryString["type"] == "img")
                {
                    strSql = " channelID=3 and filetypeid=3";
                }
                else if (Request.QueryString["type"] == "doc")
                {
                    strSql = " channelID=3 and filetypeid=2";
                }
                else
                    strSql = "  channelID=3 ";
            }
            else
            {
                if (Request.QueryString["type"] == "video")
                {
                    strSql = " channelID=1 and filetypeid=1";
                }
                else if (Request.QueryString["type"] == "img")
                {
                    strSql = " channelID=1 and filetypeid=3";
                }
                else if (Request.QueryString["type"] == "doc")
                {
                    strSql = " channelID=1 and filetypeid=2";
                }
                else
                    strSql = "  channelID=1 ";

            }
            DataSet dsAdministration = new DataSet();

            dsAdministration = bllv_ALLFile.GetList(strSql);

            if (dsAdministration.Tables[0].Rows.Count > 0)
            {
                List<Model.View_AllFileList> allfilemodel = bllv_ALLFile.DataTableToList(dsAdministration.Tables[0]);
                ViewData["FileInfo"] = allfilemodel;
            }
            else
                ViewData["FileInfo"] = new List<Model.View_AllFileList>();
            #endregion


            #region 绑定 按频道分类 总数
            int typeId1 = Convert.ToInt32(Request.QueryString["typeId"]);
            DataSet dsSpecialTopic = new DataSet();
            string strSelect = "*";
            string strTable = " ZK_FileJP a left join ZK_FileList b on a.fileID=b.fileID left join ZK_FileType c on b.fileTypeID=c.fileTypeID left join ZK_ChannelGroupAndFileList d on b.fileID=d.fileID left join ZK_ChannelGroup e on d.channelGroupID=e.channelGroupID left  join USERS f on b.ownerID=f.USERID";
            string strPrimaryKey = "ZK_FileJP.ID";
            string strOrderby = "typeID";
            int PageSize = 10;
            int PageIndex = 1;
            string strWhere = "  typeID=" + typeId1;
            int intBlPage = 1;

            dsSpecialTopic = bllCommondBase.GetList(strSelect, strTable, strPrimaryKey, strOrderby, PageSize, PageIndex, strWhere, intBlPage);
            int teachcount = 0;
            int moralcount = 0;
            int admincount = 0;
            if (dsSpecialTopic.Tables[0].Rows.Count > 0)
            {
                 DataRow [ ] drs1=dsSpecialTopic.Tables[0].Select("channelid=1");
                 teachcount=drs1.Length;
                 DataRow[] drs2 = dsSpecialTopic.Tables[0].Select("channelid=2");
                 moralcount = drs2.Length;
                 DataRow[] drs3 = dsSpecialTopic.Tables[0].Select("channelid=3");
                 admincount = drs3.Length;
            }
            
            ViewData["TotalNum_TMA"] = new List<int>() { teachcount, moralcount, admincount };
            #endregion

            #region 按类型分类 总数
            int All_TotalNum = bllalllist.GetRecordCount("");
            int MV_TotalNum = bllalllist.GetRecordCount("filetypeid=1");
            int Doc_TotalNum = bllalllist.GetRecordCount("filetypeid=2");
            int Photo_TotalNum = bllalllist.GetRecordCount("filetypeid=3");
            int Music_TotalNum = bllalllist.GetRecordCount("filetypeid=4");
            int Other_TotalNum = bllalllist.GetRecordCount("filetypeid=5");
            ViewData["TotalNum_Type"] = new List<int>() { All_TotalNum, MV_TotalNum, Doc_TotalNum, Photo_TotalNum, Music_TotalNum, Other_TotalNum };
            #endregion

            string strSearch = Request.QueryString["search"];

            if (strSearch != null)
            {
                string v1 = HttpUtility.UrlEncode(strSearch, Encoding.Default);
                string v2 = HttpUtility.UrlDecode(v1, Encoding.Default);
                ViewData["txt1"] = v2;
            }
            else
                ViewData["txt1"] = "";

            int typeId = Convert.ToInt32(Request.QueryString["typeId"]);
            ViewData["typeId"] = typeId;

            string getType = Request.QueryString["gettype"];
            ViewData["type1"] = getType;

            return View("IndexN");
        }

        int pageSize = 36;
        /// <summary>
        /// 文件列表展现页面
        /// </summary>
        /// <returns></returns>
        public ActionResult SpecialTopicList(int? page)
        {
            #region 获取列表
            string strSql = "";
            string strSearch = Request.QueryString["search"];
            if (strSearch == null)
            {
                strSearch = "";
            }
            int type = Convert.ToInt32(Request.QueryString["type"]);
            int typeId = Convert.ToInt32(Request.QueryString["typeId"]);

            if (strSearch == "")
            {
                #region
                if (Request.QueryString["cid"] == "dypd")
                {
                    if (type == 3)
                    {
                        strSql = " channelID=2 and c.filetypeid=1 and typeID=" + typeId;
                    }
                    else if (type == 2)
                    {
                        strSql = " channelID=2 and 'ZK_FileType.filetypeid'=3 and typeID=" + typeId;
                    }
                    else if (type == 1)
                    {
                        strSql = " channelID=2 and c.filetypeid in (2,6,7,8) and typeID=" + typeId;
                    }
                    else
                        strSql = "  channelID=2  and typeID=" + typeId;

                }
                else if (Request.QueryString["cid"] == "xzpd")
                {
                    if (type == 3)
                    {
                        strSql = " channelID=3 and c.filetypeid=1 and typeID=" + typeId;
                    }
                    else if (type == 2)
                    {
                        strSql = " channelID=3 and c.filetypeid=3 and typeID=" + typeId;
                    }
                    else if (type == 1)
                    {
                        strSql = " channelID=3 and c.filetypeid in (2,6,7,8) and typeID=" + typeId;
                    }
                    else
                        strSql = "  channelID=3  and typeID=" + typeId;
                }
                else if (Request.QueryString["cid"] == "jxpd")
                {
                    if (type == 3)
                    {
                        strSql = " channelID=1 and c.filetypeid=1 and typeID=" + typeId;
                    }
                    else if (type == 2)
                    {
                        strSql = " channelID=1 and c.filetypeid=3 and typeID=" + typeId;
                    }
                    else if (type == 1)
                    {
                        strSql = " channelID=1 and c.filetypeid in (2,6,7,8) and typeID=" + typeId;
                    }
                    else
                        strSql = "  channelID=1  and typeID=" + typeId;

                }
                else
                {
                    if (type == 3)
                    {
                        strSql = " c.filetypeid=1 and typeID=" + typeId;
                    }
                    else if (type == 2)
                    {
                        strSql = " c.filetypeid=3 and typeID=" + typeId;
                    }
                    else if (type == 1)
                    {
                        strSql = " c.filetypeid in (2,6,7,8) and typeID=" + typeId;
                    }
                    else
                        strSql = "  typeID=" + typeId;
                }

                #endregion
            }
            else
            {
                #region
                if (Request.QueryString["cid"] == "dypd")
                {
                    if (Convert.ToInt32(Request.QueryString["type"]) == 3)
                    {
                        strSql = " channelID=2 and c.filetypeid=1 and  a.fileName like '%" + strSearch + "%' and typeID=" + typeId;
                    }
                    else if (Convert.ToInt32(Request.QueryString["type"]) == 2)
                    {
                        strSql = " channelID=2 and c.filetypeid=3 and  a.fileName like '%" + strSearch + "%' and typeID=" + typeId;
                    }
                    else if (Convert.ToInt32(Request.QueryString["type"]) == 1)
                    {
                        strSql = " channelID=2 and c.filetypeid in (2,6,7,8) and  a.fileName like '%" + strSearch + "%' and typeID=" + typeId;
                    }
                    else
                        strSql = "  channelID=2 ";

                }
                else if (Request.QueryString["cid"] == "xzpd")
                {
                    if (Convert.ToInt32(Request.QueryString["type"]) == 3)
                    {
                        strSql = " channelID=3 and c.filetypeid=1 and  a.fileName like '%" + strSearch + "%' and typeID=" + typeId;
                    }
                    else if (Convert.ToInt32(Request.QueryString["type"]) == 2)
                    {
                        strSql = " channelID=3 and c.filetypeid=3 and  a.fileName like '%" + strSearch + "%' and typeID=" + typeId;
                    }
                    else if (Convert.ToInt32(Request.QueryString["type"]) == 1)
                    {
                        strSql = " channelID=3 and c.filetypeid in (2,6,7,8) and  a.fileName like '%" + strSearch + "%' and typeID=" + typeId;
                    }
                    else
                        strSql = "  channelID=3 and  a.fileName like '%" + strSearch + "%'  and typeID=" + typeId;
                }
                else if (Request.QueryString["cid"] == "jxpd")
                {
                    if (Convert.ToInt32(Request.QueryString["type"]) == 3)
                    {
                        strSql = " channelID=1 and c.filetypeid=1 and  a.fileName like '%" + strSearch + "%' and typeID=" + typeId;
                    }
                    else if (Convert.ToInt32(Request.QueryString["type"]) == 2)
                    {
                        strSql = " channelID=1 and c.filetypeid=3 and  a.fileName like '%" + strSearch + "%' and typeID=" + typeId;
                    }
                    else if (Convert.ToInt32(Request.QueryString["type"]) == 1)
                    {
                        strSql = " channelID=1 and c.filetypeid in (2,6,7,8) and  a.fileName like '%" + strSearch + "%' and typeID=" + typeId;
                    }
                    else
                        strSql = "  channelID=1 and a.fileName like '%" + strSearch + "%' and typeID=" + typeId;

                }

                else
                {
                    if (Convert.ToInt32(Request.QueryString["type"]) == 3)
                    {
                        strSql = " c.filetypeid=1 and  a.fileName like '%" + strSearch + "%' and typeID=" + typeId;
                    }
                    else if (Convert.ToInt32(Request.QueryString["type"]) == 2)
                    {
                        strSql = " c.filetypeid=3 and  a.fileName like '%" + strSearch + "%' and typeID=" + typeId;
                    }
                    else if (Convert.ToInt32(Request.QueryString["type"]) == 1)
                    {
                        strSql = " c.filetypeid in (2,6,7,8) and  a.fileName like '%" + strSearch + "%' and typeID=" + typeId;
                    }
                    else
                        strSql = "  a.fileName like '%" + strSearch + "%' and typeID=" + typeId;
                }

                #endregion
            }

            DataSet dsAdministration = new DataSet();
            string strSelect = "*";
            string strTable = " ZK_FileJP a left join ZK_FileList b on a.fileID=b.fileID left join ZK_FileType c on b.fileTypeID=c.fileTypeID left join ZK_ChannelGroupAndFileList d on b.fileID=d.fileID left join ZK_ChannelGroup e on d.channelGroupID=e.channelGroupID left  join USERS f on b.ownerID=f.USERID";
            string strPrimaryKey = "ZK_FileJP.ID";
            string strOrderby = "typeID";
            int PageSize = 10;
            int PageIndex = 1;
            string strWhere = strSql;
            int intBlPage = 1;               

            dsAdministration = bllCommondBase.GetList(strSelect, strTable, strPrimaryKey, strOrderby, PageSize, PageIndex, strWhere, intBlPage);
            //dsAdministration = bllv_ALLFile.GetList(pageSize, page.HasValue ? page.Value : 1, strSql);

            if (dsAdministration.Tables[0].Rows.Count > 0)
            {                   
                ViewData["FileInfo"] = dsAdministration;
            }
            else
                ViewData["FileInfo"] = new DataSet();

            #endregion

            string v1 = HttpUtility.UrlEncode(strSearch, Encoding.Default);
            string v2 = HttpUtility.UrlDecode(v1, Encoding.Default);
            ViewData["txt1"] = v2;

            ViewData["page"] = "page";
            ViewData["pagesize"] = pageSize;
            ViewData["totlecount"] = dsAdministration.Tables[0].Rows.Count;// bllv_ALLFile.GetList(strSql).Tables[0].Rows.Count;

            return View();
        }

        int pageSize0 = 20;
        /// <summary>
        /// 文件列表页面
        /// </summary>
        /// <returns></returns>
        public ActionResult SpecialTopicTable(int? page)
        {
            #region 获取列表
            string strSql = "";
            string strSearch = Request.QueryString["search"];
            if (strSearch == null)
            {
                strSearch = "";
            }
            int typeId = Convert.ToInt32(Request.QueryString["typeId"]);

            if (strSearch == "")
            {
                #region
                if (Request.QueryString["cid"] == "dypd")
                {
                    if (Convert.ToInt32(Request.QueryString["type"]) == 3)
                    {
                        strSql = " channelID=2 and c.filetypeid=1 and typeID=" + typeId;
                    }
                    else if (Convert.ToInt32(Request.QueryString["type"]) == 2)
                    {
                        strSql = " channelID=2 and c.filetypeid=3 and typeID=" + typeId;
                    }
                    else if (Convert.ToInt32(Request.QueryString["type"]) == 1)
                    {
                        strSql = " channelID=2 and c.filetypeid in (2,6,7,8) and typeID=" + typeId;
                    }
                    else
                        strSql = "  channelID=2  and typeID=" + typeId;

                }
                else if (Request.QueryString["cid"] == "xzpd")
                {
                    if (Convert.ToInt32(Request.QueryString["type"]) == 3)
                    {
                        strSql = " channelID=3 and c.filetypeid=1 and typeID=" + typeId;
                    }
                    else if (Convert.ToInt32(Request.QueryString["type"]) == 2)
                    {
                        strSql = " channelID=3 and c.filetypeid=3 and typeID=" + typeId;
                    }
                    else if (Convert.ToInt32(Request.QueryString["type"]) == 1)
                    {
                        strSql = " channelID=3 and c.filetypeid in (2,6,7,8) and typeID=" + typeId;
                    }
                    else
                        strSql = "  channelID=3  and typeID=" + typeId;
                }
                else if (Request.QueryString["cid"] == "jxpd")
                {
                    if (Convert.ToInt32(Request.QueryString["type"]) == 3)
                    {
                        strSql = " channelID=1 and c.filetypeid=1 and typeID=" + typeId;
                    }
                    else if (Convert.ToInt32(Request.QueryString["type"]) == 2)
                    {
                        strSql = " channelID=1 and c.filetypeid=3 and typeID=" + typeId;
                    }
                    else if (Convert.ToInt32(Request.QueryString["type"]) == 1)
                    {
                        strSql = " channelID=1 and c.filetypeid in (2,6,7,8) and typeID=" + typeId;
                    }
                    else
                        strSql = "  channelID=1  and typeID=" + typeId;

                }
                else
                {
                    if (Convert.ToInt32(Request.QueryString["type"]) == 3)
                    {
                        strSql = " c.filetypeid=1 and typeID=" + typeId;
                    }
                    else if (Convert.ToInt32(Request.QueryString["type"]) == 2)
                    {
                        strSql = " c.filetypeid=3 and typeID=" + typeId;
                    }
                    else if (Convert.ToInt32(Request.QueryString["type"]) == 1)
                    {
                        strSql = " c.filetypeid in (2,6,7,8) and typeID=" + typeId;
                    }
                    else
                        strSql = " typeID=" + typeId;
                }
                            
                #endregion
            }
            else
            {
                #region
                if (Request.QueryString["cid"] == "dypd")
                {
                    if (Convert.ToInt32(Request.QueryString["type"]) == 3)
                    {
                        strSql = " channelID=2 and c.filetypeid=1 and  a.fileName like '%" + strSearch + "%' and typeID=" + typeId;
                    }
                    else if (Convert.ToInt32(Request.QueryString["type"]) == 2)
                    {
                        strSql = " channelID=2 and c.filetypeid=3 and  a.fileName like '%" + strSearch + "%' and typeID=" + typeId;
                    }
                    else if (Convert.ToInt32(Request.QueryString["type"]) == 1)
                    {
                        strSql = " channelID=2 and c.filetypeid in (2,6,7,8) and  a.fileName like '%" + strSearch + "%' and typeID=" + typeId;
                    }
                    else
                        strSql = "  channelID=2  and typeID=" + typeId;

                }
                else if (Request.QueryString["cid"] == "xzpd")
                {
                    if (Convert.ToInt32(Request.QueryString["type"]) == 3)
                    {
                        strSql = " channelID=3 and c.filetypeid=1 and  a.fileName like '%" + strSearch + "%' and typeID=" + typeId;
                    }
                    else if (Convert.ToInt32(Request.QueryString["type"]) == 2)
                    {
                        strSql = " channelID=3 and c.filetypeid=3 and  a.fileName like '%" + strSearch + "%' and typeID=" + typeId;
                    }
                    else if (Convert.ToInt32(Request.QueryString["type"]) == 1)
                    {
                        strSql = " channelID=3 and c.filetypeid in (2,6,7,8) and  a.fileName like '%" + strSearch + "%' and typeID=" + typeId;
                    }
                    else
                        strSql = "  channelID=3 and  a.fileName like '%" + strSearch + "%' and typeID=" + typeId;
                }
                else if (Request.QueryString["cid"] == "jxpd")
                {
                    if (Convert.ToInt32(Request.QueryString["type"]) == 3)
                    {
                        strSql = " channelID=1 and c.filetypeid=1 and  a.fileName like '%" + strSearch + "%' and typeID=" + typeId;
                    }
                    else if (Convert.ToInt32(Request.QueryString["type"]) == 2)
                    {
                        strSql = " channelID=1 and c.filetypeid=3 and  a.fileName like '%" + strSearch + "%' and typeID=" + typeId;
                    }
                    else if (Convert.ToInt32(Request.QueryString["type"]) == 1)
                    {
                        strSql = " channelID=1 and c.filetypeid in (2,6,7,8) and  a.fileName like '%" + strSearch + "%' and typeID=" + typeId;
                    }
                    else
                        strSql = "  channelID=1 and  a.fileName like '%" + strSearch + "%' and typeID=" + typeId;

                }

                else
                {
                    if (Convert.ToInt32(Request.QueryString["type"]) == 3)
                    {
                        strSql = "  c.filetypeid=1 and  a.fileName like '%" + strSearch + "%' and typeID=" + typeId;
                    }
                    else if (Convert.ToInt32(Request.QueryString["type"]) == 2)
                    {
                        strSql = "  c.filetypeid=3 and  a.fileName like '%" + strSearch + "%' and typeID=" + typeId;
                    }
                    else if (Convert.ToInt32(Request.QueryString["type"]) == 1)
                    {
                        strSql = "  c.filetypeid in (2,6,7,8) and  a.fileName like '%" + strSearch + "%' and typeID=" + typeId;
                    }
                    else
                        strSql = "  a.fileName like '%" + strSearch + "%' and typeID=" + typeId;
                }
                
                #endregion
            }
            DataSet dsAdministration = new DataSet();

            string strSelect = "*";
            string strTable = " ZK_FileJP a left join ZK_FileList b on a.fileID=b.fileID left join ZK_FileType c on b.fileTypeID=c.fileTypeID left join ZK_ChannelGroupAndFileList d on b.fileID=d.fileID left join ZK_ChannelGroup e on d.channelGroupID=e.channelGroupID left  join USERS f on b.ownerID=f.USERID";
            string strPrimaryKey = "ZK_FileJP.ID";
            string strOrderby = "typeID";
            int PageSize = 10;
            int PageIndex = 1;
            string strWhere = strSql;
            int intBlPage = 1;

            dsAdministration = bllCommondBase.GetList(strSelect, strTable, strPrimaryKey, strOrderby, PageSize, PageIndex, strWhere, intBlPage);
            //dsAdministration = bllv_ALLFile.GetList(pageSize0, page.HasValue ? page.Value : 1, strSql);

            ViewData["Administrative_ResourcesList"] = dsAdministration;
            ViewData["totlecount"] = dsAdministration.Tables[0].Rows.Count;//bllv_ALLFile.GetList(strSql).Tables[0].Rows.Count;
            #endregion
            string v1 = HttpUtility.UrlEncode(strSearch, Encoding.Default);
            string v2 = HttpUtility.UrlDecode(v1, Encoding.Default);
            ViewData["txt1"] = v2;

            ViewData["page"] = "page";
            ViewData["pagesize"] = pageSize0;             

            return View();
        }
    }
}
