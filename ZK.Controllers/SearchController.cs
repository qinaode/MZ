using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Data;
using System.Web;
using ZK.Common;
using System.Configuration;

namespace ZK.Controllers
{

        [ZKAuthAttributeFilter(purl = "/")]
    public class SearchController : Controller
    {
        BLL.View_AllFileList bllv_ALLFile = new BLL.View_AllFileList();
        Model.View_OtherFileList mdlv_otherFile = new Model.View_OtherFileList();
        
        public ActionResult Index()
        {         
            //分页查询列表
            string strKey = Request.QueryString["key"];
            string strcateID = Request.QueryString["cateID"];
                               
            ZK.BLL.ZK_FileList bllalllist = new BLL.ZK_FileList();
            StringBuilder sBuilder = new StringBuilder();
            sBuilder.Append("filename like'%" + strKey + "%'");

            #region  注释
            //if (strcateID != null)
            //{
            //    sBuilder.Append(" & channelID="+strKey);
            //}
            //DataSet dsFileList = bllalllist.GetList(pageSize, page.HasValue ? page.Value : 1, sBuilder.ToString());

            //ViewData["Administrative_ResourcesList"] = dsFileList;
            //ViewData["page"] = "page";
            //ViewData["pagesize"] = pageSize;
            //ViewData["totlecount"] = bllalllist.GetList(sBuilder.ToString()).Tables[0].Rows.Count;
            #endregion

            #region 获取列表
            string strSql="";
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

            dsAdministration = bllv_ALLFile.GetList( strSql);

            if (dsAdministration.Tables[0].Rows.Count > 0)
            {
                List<Model.View_AllFileList> allfilemodel = bllv_ALLFile.DataTableToList(dsAdministration.Tables[0]);
                ViewData["FileInfo"] = allfilemodel;
            }
            else
                ViewData["FileInfo"] = new List<Model.View_AllFileList>();

            //ViewData["totlecount"] = bllv_ALLFile.GetList(strSql).Tables[0].Rows.Count;
            //ViewData["Administrative_ResourcesList"] = dsAdministration;
            #endregion

          
            #region 绑定 按频道分类 总数
            //string strt = "fileID in (select fileID from View_AllFileList where " + sBuilder.ToString() + " and channelid=1)";
            //int Teach_TotalNum = bllalllist.GetRecordCount("fileID in (select fileID from View_AllFileList where " + sBuilder.ToString() + " and channelid=1)");
            //int Moral_TotalNum = bllalllist.GetRecordCount("fileID in (select fileID from View_AllFileList where " + sBuilder.ToString() + " and channelid=2)");
            //int Admin_TotalNum = bllalllist.GetRecordCount("fileID in (select fileID from View_AllFileList where " + sBuilder.ToString() + " and channelid=3)");
            //ViewData["TotalNum_TMA"] = new List<int>() { Teach_TotalNum, Moral_TotalNum, Admin_TotalNum };

            int teachcount = bllv_ALLFile.GetList("channelid=1").Tables[0].Rows.Count;
            int moralcount = bllv_ALLFile.GetList("channelid=2").Tables[0].Rows.Count;
            int admincount = bllv_ALLFile.GetList("channelid=3").Tables[0].Rows.Count;
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

            string getType = Request.QueryString["gettype"];
            ViewData["type1"] = getType;

            return View("IndexN");
        }

        //static string XMLFilePath = "SystemSetting.xml";//xml路径E:\智慧教育系统\trunk\ZK.Manage\SystemSetting.xml
        int pageSize = 36;

        #region 文件列表展现页面
        /// <summary>
        /// 文件列表展现页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Searchfilelist(int? page)
        {
            string strSql = "";
            string strSearch = Request.QueryString["search"];
            if (strSearch == null)
            {
                strSearch = "";
            }
            int type = Convert.ToInt32(Request.QueryString["type"]);
            int type_ExtType = 0;

            if (type == 8 || type == 7)
            {
                type_ExtType = type;
                type = 1;
            }

            if (strSearch == "")
            {
                #region 德育频道
                if (Request.QueryString["cid"] == "dypd")
                {
                    if (type == 3)//视频1
                    {
                        strSql = " channelID=2 and filetypeid=1";
                    }
                    else if (type == 2) //图片
                    {
                        strSql = " channelID=2 and filetypeid=3";
                    }
                    else if (type == 1)//文档
                    {
                        if (type_ExtType != 0)//ppt或pdf
                        {
                            strSql = " channelID=2 and filetypeid =" + type_ExtType;
                        }
                        else//word
                        {
                            strSql = " channelID=2 and filetypeid in (2,6)";
                        }
                    }

                    else
                        strSql = "  channelID=2 ";

                }
                #endregion

                #region 行政频道
                else if (Request.QueryString["cid"] == "xzpd")
                {
                    if (type == 3)
                    {
                        strSql = " channelID=3 and filetypeid=1";
                    }
                    else if (type == 2)
                    {
                        strSql = " channelID=3 and filetypeid=3";
                    }

                    else if (type == 1)//文档
                    {
                        if (type_ExtType != 0)//ppt或pdf
                        {
                            strSql = " channelID=3 and filetypeid =" + type_ExtType;
                        }
                        else//word
                        {
                            strSql = " channelID=3 and filetypeid in (2,6)";
                        }
                    }
                    else
                        strSql = "  channelID=3 ";
                }
                #endregion

                #region 教学频道
                else if (Request.QueryString["cid"] == "jxpd")
                {
                    if (type == 3)
                    {
                        strSql = " channelID=1 and filetypeid=1";
                    }
                    else if (type == 2)
                    {
                        strSql = " channelID=1 and filetypeid=3";
                    }
                    else if (type == 1)//文档
                    {
                        if (type_ExtType != 0)//ppt或pdf
                        {
                            strSql = " channelID=1 and filetypeid =" + type_ExtType;
                        }
                        else//word
                        {
                            strSql = " channelID=1 and filetypeid in (2,6)";
                        }
                    }
                    else
                        strSql = "  channelID=1 ";

                }
                #endregion

                #region 首页
                else
                {
                    if (type == 3)
                    {
                        strSql = " channelID in (1,2,3) and filetypeid=1";
                    }
                    else if (type == 2)
                    {
                        strSql = " channelID in (1,2,3) and filetypeid=3";
                    }
                    else if (type == 1)//文档
                    {
                        if (type_ExtType != 0)//ppt或pdf
                        {
                            strSql = " channelID in(1,2,3) and filetypeid =" + type_ExtType;
                        }
                        else//word
                        {
                            strSql = " channelID in (1,2,3) and filetypeid in (2,6)";
                        }
                    }

                    else
                        strSql = "  channelID in (1,2,3) ";
                }
                #endregion


            }
            else
            {
                #region 德育频道
                if (Request.QueryString["cid"] == "dypd")
                {
                    if (Convert.ToInt32(Request.QueryString["type"]) == 3)//视频
                    {
                        strSql = " channelID=2 and filetypeid=1 and  fileName like '%" + strSearch + "%'";
                    }
                    else if (Convert.ToInt32(Request.QueryString["type"]) == 2)//图片
                    {
                        strSql = " channelID=2 and filetypeid=3 and  fileName like '%" + strSearch + "%'";
                    }
                    else if (Convert.ToInt32(Request.QueryString["type"]) == 1)//文档
                    {
                        if (type_ExtType != 0)//ppt或pdf
                        {
                            strSql = " channelID=2 and filetypeid=" + type_ExtType + "and  fileName like '%" + strSearch + "%'";
                        }
                        else
                        {
                            strSql = " channelID=2 and filetypeid in (2,6) and  fileName like '%" + strSearch + "%'";
                        }
                    }
                    else
                        strSql = "  channelID=2 and  fileName like '%" + strSearch + "%'";
                }
                #endregion

                #region 行政频道
                else if (Request.QueryString["cid"] == "xzpd")
                {
                    if (Convert.ToInt32(Request.QueryString["type"]) == 3)
                    {
                        strSql = " channelID=3 and filetypeid=1 and  fileName like '%" + strSearch + "%'";
                    }
                    else if (Convert.ToInt32(Request.QueryString["type"]) == 2)
                    {
                        strSql = " channelID=3 and filetypeid=3 and  fileName like '%" + strSearch + "%'";
                    }
                    else if (Convert.ToInt32(Request.QueryString["type"]) == 1)
                    {
                        if (type_ExtType != 0)//ppt或pdf
                        {
                            strSql = " channelID=3 and filetypeid=" + type_ExtType + "and  fileName like '%" + strSearch + "%'";
                        }
                        strSql = " channelID=3 and filetypeid in (2,6) and  fileName like '%" + strSearch + "%'";
                    }
                    else
                        strSql = "  channelID=3 and  fileName like '%" + strSearch + "%' ";
                }
                #endregion

                #region 教学频道
                else if (Request.QueryString["cid"] == "jxpd")
                {
                    if (type == 3)
                    {
                        strSql = " channelID=1 and filetypeid=1 and  fileName like '%" + strSearch + "%'";
                    }
                    else if (type == 2)
                    {
                        strSql = " channelID=1 and filetypeid=3 and  fileName like '%" + strSearch + "%'";
                    }
                    else if (type == 1)//文档
                    {
                        if (type_ExtType != 0)//ppt或pdf
                        {
                            strSql = " channelID=1 and filetypeid =" + type_ExtType + " and  fileName like '%" + strSearch + "%'";
                        }
                        else//word
                        {
                            strSql = " channelID=1 and filetypeid in (2,6) and  fileName like '%" + strSearch + "%'";
                        }
                    }
                    else
                        strSql = "  channelID=1 and  fileName like '%" + strSearch + "%' ";

                }
                #endregion

                #region 首页
                else
                {
                    if (Convert.ToInt32(Request.QueryString["type"]) == 3)
                    {
                        strSql = " channelID in (1,2,3) and filetypeid=1 and  fileName like '%" + strSearch + "%'";
                    }
                    else if (Convert.ToInt32(Request.QueryString["type"]) == 2)
                    {
                        strSql = " channelID in (1,2,3) and filetypeid=3 and  fileName like '%" + strSearch + "%'";
                    }
                    else if (Convert.ToInt32(Request.QueryString["type"]) == 1)
                    {
                        if (type_ExtType != 0)//ppt或pdf
                        {
                            strSql = " channelID in (1,2,3) and filetypeid=" + type_ExtType + "and  fileName like '%" + strSearch + "%'";
                        }
                        else
                        {
                            strSql = " channelID in (1,2,3) and filetypeid in (2,6) and  fileName like '%" + strSearch + "%'";
                        }
                    }
                    else
                        strSql = "  channelID in (1,2,3) and  fileName like '%" + strSearch + "%'";
                }
                #endregion
            }


            DataSet dsAdministration = new DataSet();

            string XMLFilePath = Server.MapPath(ZK.Common.ModelSettings.Pre_SysSettingXMLPath);

            pageSize = Convert.ToInt32(XMLHelper.GetXmlAttribute(XMLFilePath, "Settings/PageCount/Search", "value").Value.ToString());

            dsAdministration = bllv_ALLFile.GetList(pageSize, page.HasValue ? page.Value : 1, strSql);

            if (dsAdministration.Tables[0].Rows.Count > 0)
            {
                List<Model.View_AllFileList> allfilemodel = bllv_ALLFile.DataTableToList(dsAdministration.Tables[0]);
                ViewData["FileInfo"] = allfilemodel;
            }
            else
                ViewData["FileInfo"] = new List<Model.View_AllFileList>();


            string v1 = HttpUtility.UrlEncode(strSearch, Encoding.Default);
            string v2 = HttpUtility.UrlDecode(v1, Encoding.Default);
            ViewData["txt1"] = v2;

            ViewData["page"] = "page";
            ViewData["pagesize"] = pageSize;
            ViewData["totlecount"] = bllv_ALLFile.GetList(strSql).Tables[0].Rows.Count;

            return View();
        }
        #endregion
 

        int pageSize0 = 20;
        #region 文件列表页面
        /// <summary>
        /// 文件列表页面
        /// </summary>
        /// <returns></returns>
        public ActionResult SearchTableList(int? page)
        {
            int type=Convert.ToInt32(Request.QueryString["type"]);
            int type_ExtType = 0;
            if (type == 8 || type == 7)
            {
                type_ExtType = type;
                type = 1;
            }
            string strSql = "";
            string strSearch = Request.QueryString["search"];
            if (strSearch == null)
            {
                strSearch = "";
            }
            if (strSearch == "")
            {
                #region 德育频道
                if (Request.QueryString["cid"] == "dypd")//德育频道
                {
                    if (Convert.ToInt32(Request.QueryString["type"]) == 3)
                    {
                        strSql = " channelID=2 and filetypeid=1";
                    }
                    else if (Convert.ToInt32(Request.QueryString["type"]) == 2)
                    {
                        strSql = " channelID=2 and filetypeid=3";
                    }
                    else if (type== 1)
                    {
                        if (type_ExtType != 0)//ppt或pdf
                        {
                            strSql = " channelID =2 and filetypeid=" + type_ExtType;
                        }
                        else
                        {
                            strSql = " channelID=2 and filetypeid in (2,6)";
                        }
                    }
                    else
                        strSql = "  channelID=2 ";

                }
                #endregion

                #region 行政频道
                else if (Request.QueryString["cid"] == "xzpd")//行政频道
                {
                    if (Convert.ToInt32(Request.QueryString["type"]) == 3)
                    {
                        strSql = " channelID=3 and filetypeid=1";
                    }
                    else if (Convert.ToInt32(Request.QueryString["type"]) == 2)
                    {
                        strSql = " channelID=3 and filetypeid=3";
                    }
                    else if (Convert.ToInt32(Request.QueryString["type"]) == 1)
                    {
                        if (type_ExtType != 0)//ppt或pdf
                        {
                            strSql = " channelID =3 and filetypeid=" + type_ExtType;
                        }
                        else
                        {
                            strSql = " channelID=3 and filetypeid in (2,6)";
                        }
                    }
                    else
                        strSql = "  channelID=3 ";
                }
                #endregion

                #region 教学频道
                else if (Request.QueryString["cid"] == "jxpd") //教学频道
                {
                    if (Convert.ToInt32(Request.QueryString["type"]) == 3)
                    {
                        strSql = " channelID=1 and filetypeid=1";
                    }
                    else if (Convert.ToInt32(Request.QueryString["type"]) == 2)
                    {
                        strSql = " channelID=1 and filetypeid=3";
                    }
                    else if (Convert.ToInt32(Request.QueryString["type"]) == 1)
                    {
                        if (type_ExtType != 0)//ppt或pdf
                        {
                            strSql = " channelID=1 and filetypeid=" + type_ExtType;
                        }
                        else
                        {
                            strSql = " channelID=1 and filetypeid in (2,6)";
                        }
                    }
                    else
                        strSql = "  channelID=1 ";

                }
                #endregion

                #region 首页中搜索
                else
                {
                    if (Convert.ToInt32(Request.QueryString["type"]) == 3)
                    {
                        strSql = " channelID in (1,2,3) and filetypeid=1";
                    }
                    else if (Convert.ToInt32(Request.QueryString["type"]) == 2)
                    {
                        strSql = " channelID in (1,2,3) and filetypeid=3";
                    }
                    else if (Convert.ToInt32(Request.QueryString["type"]) == 1)
                    {
                        if (type_ExtType != 0)//ppt或pdf
                        {
                            strSql = " channelID in (1,2,3) and filetypeid=" + type_ExtType;
                        }
                        else
                        {
                            strSql = " channelID in (1,2,3) and filetypeid in (2,6)";
                        }
                    }
                    else
                        strSql = "  channelID in (1,2,3) ";
                }
                #endregion

                DataSet dsAdministration = new DataSet();

                dsAdministration = bllv_ALLFile.GetList(pageSize0, page.HasValue ? page.Value : 1, strSql);

                ViewData["Administrative_ResourcesList"] = dsAdministration;
                //if (dsAdministration.Tables[0].Rows.Count > 0)
                //{
                //    List<Model.View_AllFileList> allfilemodel = bllv_ALLFile.DataTableToList(dsAdministration.Tables[0]);
                //    ViewData["FileInfo"] = allfilemodel;
                //}
                //else
                //    ViewData["FileInfo"] = new List<Model.View_AllFileList>();

            }
            else
            {
                #region  德育频道
                if (Request.QueryString["cid"] == "dypd")
                {
                    if (Convert.ToInt32(Request.QueryString["type"]) == 3)
                    {
                        strSql = " channelID=2 and filetypeid=1 and  fileName like '%" + strSearch + "%'";
                    }
                    else if (Convert.ToInt32(Request.QueryString["type"]) == 2)
                    {
                        strSql = " channelID=2 and filetypeid=3 and  fileName like '%" + strSearch + "%'";
                    }
                    else if (Convert.ToInt32(Request.QueryString["type"]) == 1)
                    {
                        if (type_ExtType != 0)//ppt或pdf
                        {
                            strSql = " channelID =2 and filetypeid=" + type_ExtType + "and  fileName like '%" + strSearch + "%'";
                        }
                        else
                        {
                            strSql = " channelID=2 and filetypeid in (2,6) and  fileName like '%" + strSearch + "%'";
                        }
                    }
                    else
                        strSql = "  channelID=2 ";

                }
                #endregion

                #region 行政频道
		        else if (Request.QueryString["cid"] == "xzpd")
                {
                    if (Convert.ToInt32(Request.QueryString["type"]) == 3)
                    {
                        strSql = " channelID=3 and filetypeid=1 and  fileName like '%" + strSearch + "%'";
                    }
                    else if (Convert.ToInt32(Request.QueryString["type"]) == 2)
                    {
                        strSql = " channelID=3 and filetypeid=3 and  fileName like '%" + strSearch + "%'";
                    }
                    else if (Convert.ToInt32(Request.QueryString["type"]) == 1)
                    {
                        if (type_ExtType != 0)//ppt或pdf
                        {
                            strSql = " channelID =3 and filetypeid=" + type_ExtType + "and  fileName like '%" + strSearch + "%'";
                        }
                        else
                        {
                            strSql = " channelID=3 and filetypeid in (2,6) and  fileName like '%" + strSearch + "%'";
                        }
                    }
                    else
                        strSql = "  channelID=3 and  fileName like '%" + strSearch + "%' ";
                }
	           #endregion
               
                #region 教学频道
		         else if (Request.QueryString["cid"] == "jxpd")
                {
                    if (Convert.ToInt32(Request.QueryString["type"]) == 3)
                    {
                        strSql = " channelID=1 and filetypeid=1 and  fileName like '%" + strSearch + "%'";
                    }
                    else if (Convert.ToInt32(Request.QueryString["type"]) == 2)
                    {
                        strSql = " channelID=1 and filetypeid=3 and  fileName like '%" + strSearch + "%'";
                    }
                    else if (Convert.ToInt32(Request.QueryString["type"]) == 1)
                    {
                        if (type_ExtType != 0)//ppt或pdf
                        {
                            strSql = " channelID =1 and filetypeid=" + type_ExtType + "and  fileName like '%" + strSearch + "%'";
                        }
                        else
                        {
                        strSql = " channelID=1 and filetypeid in (2,6) and  fileName like '%" + strSearch + "%'";
                        }
                    }
                    else
                        strSql = "  channelID=1 and  fileName like '%" + strSearch + "%'";

                }

	           #endregion

                #region 首页
                else
                {
                    if (Convert.ToInt32(Request.QueryString["type"]) == 3)
                    {
                        strSql = " channelID in (1,2,3) and filetypeid=1 and  fileName like '%" + strSearch + "%'";
                    }
                    else if (Convert.ToInt32(Request.QueryString["type"]) == 2)
                    {
                        strSql = " channelID in (1,2,3) and filetypeid=3 and  fileName like '%" + strSearch + "%'";
                    }
                    else if (Convert.ToInt32(Request.QueryString["type"]) == 1)
                    {
                        if (type_ExtType != 0)//ppt或pdf
                        {
                            strSql = " channelID in (1,2,3)  and filetypeid=" + type_ExtType + "and  fileName like '%" + strSearch + "%'";
                        }
                        else
                        {
                            strSql = " channelID in (1,2,3) and filetypeid in (2,6) and  fileName like '%" + strSearch + "%'";
                        }
                    }
                    else
                        strSql = "  channelID in (1,2,3) and  fileName like '%" + strSearch + "%'";
                }
                #endregion

                
                DataSet dsAdministration = new DataSet();

                dsAdministration = bllv_ALLFile.GetList(pageSize0, page.HasValue ? page.Value : 1, strSql);

                ViewData["Administrative_ResourcesList"] = dsAdministration;

                #endregion
            }

            string v1 = HttpUtility.UrlEncode(strSearch, Encoding.Default);
            string v2 = HttpUtility.UrlDecode(v1, Encoding.Default);
            ViewData["txt1"] = v2;

            ViewData["page"] = "page";
            ViewData["pagesize"] = pageSize0;
            ViewData["totlecount"] = bllv_ALLFile.GetList(strSql).Tables[0].Rows.Count;

            return View();
        }
        
    }
}
