using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Text;
using ZK.Common;

namespace ZK.Controllers
{
    [ZKAuthAttributeFilter(purl = "/")]
    public class AdministrationController : Controller
    {
        //
        // GET: /Administration/

        BLL.View_OtherFileList bllv_otherFile = new BLL.View_OtherFileList();
        Model.View_OtherFileList mdlv_otherFile = new Model.View_OtherFileList();
        BLL.View_OtherFileList bllv_otherfile = new BLL.View_OtherFileList();
        BLL.ZK_ChannelGroup bllchannelGroup = new BLL.ZK_ChannelGroup();
        BLL.View_AllFileList bllv_AllFile = new BLL.View_AllFileList();
        BLL.ZK_ChannelGroupAndFileList bllgroupfilelist = new BLL.ZK_ChannelGroupAndFileList();

        public string strCurrent = "admin"; 
  
        public ActionResult Index()
        {
            ViewData["current"] = strCurrent;
            #region 资源左下框展示数据列表
            StringBuilder builder = new StringBuilder();
            builder.Append(" channelID=3 ");
            List<Model.View_OtherFileList> v_otherfilelist = bllv_otherfile.GetModelList(builder.ToString());

            ViewData["v_otherfilelist"] = v_otherfilelist;

            #endregion

            #region 资源排行 日排行

            ViewData["v_allfilelistDay"] = GetResourceList(1);
            #endregion

            #region 资源排行 周排行

            ViewData["v_allfilelistWeek"] = GetResourceList(2);
            #endregion

            #region 资源排行 月排行

            ViewData["v_allfilelistMonth"] = GetResourceList(3);

            #endregion

            #region 获取groupName 和 文件数 的列表
            string strWhere = "channelID=3 ";
            //string strWhere1 = "channelID=3";
            Dictionary<int, string> dic = new Dictionary<int, string>();
            Dictionary<int, int> xzList = new Dictionary<int, int>();

            List<Model.ZK_ChannelGroup> lists = bllchannelGroup.GetModelList(strWhere);
            List<Model.View_OtherFileList> lists1 = bllv_otherFile.GetModelList(strWhere);
            foreach (var item in lists)
            {
                dic.Add(item.channelGroupID, item.channelGroupName);

                string strcount = " channelGroupID in (select channelGroupID from ZK_ChannelGroup where channelID=3 and channelGroupID=" + item.channelGroupID + ")";
                List<Model.ZK_ChannelGroupAndFileList> listcount = bllgroupfilelist.GetModelList(strcount);

                xzList.Add(item.channelGroupID, listcount.Count);

            }
            ViewData["GroupNameList"] = dic;
            ViewData["ChannelGroupCount"] = xzList;

            #endregion

            #region 获取列表

            DataSet dsAdministration = new DataSet();

            string strSql = "";
            int strid = 0;
            BLL.View_AllFileList bllv_allfilelist = new BLL.View_AllFileList();
            bool b = int.TryParse(Request.QueryString["cid"], out strid);
            if (strid != 0)
            {
                string typeStr = Request.QueryString["type"];
                if (typeStr == "video")
                {
                    strSql = "  fileID in (select fileID from ZK_ChannelGroupAndFileList where channelGroupID=" + strid + " and filetypeid=1)";
                }
                else if (typeStr == "img")
                {
                    strSql = "  fileID in (select fileID from ZK_ChannelGroupAndFileList where channelGroupID=" + strid + " and filetypeid=3)";
                }
                else if (typeStr == "doc")
                {
                    strSql = "  fileID in (select fileID from ZK_ChannelGroupAndFileList where channelGroupID=" + strid + " and filetypeid=2)";
                }
                else
                    strSql = "  fileID in (select fileID from ZK_ChannelGroupAndFileList where channelGroupID=" + strid + ")";
            }
            else
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
                    strSql = "  channelID=3";
            }
            dsAdministration = bllv_otherFile.GetList(strSql);

            if (dsAdministration.Tables[0].Rows.Count > 0)
            {
                List<Model.View_OtherFileList> allfilemodel = bllv_otherFile.DataTableToList(dsAdministration.Tables[0]);
                ViewData["FileInfo"] = allfilemodel;
            }
            else
                ViewData["FileInfo"] = new List<Model.View_OtherFileList>();

            ViewData["totlecount"] = bllv_otherFile.GetList("  channelID=3").Tables[0].Rows.Count;

            #region 注释
            ////添加一个字段
            //dsAdministration.Tables[0].Columns.Add(new DataColumn("owner", typeof(String)));
            //dsAdministration.Tables[0].Columns.Add(new DataColumn("fileTypeName", typeof(String)));
            //dsAdministration.Tables[0].Columns.Add(new DataColumn("relevantNumxz", typeof(Int32)));
            ////dsAdministration.Tables[0].Columns.Add(new DataColumn("createTime", typeof(DateTime)));

            ////遍历并填充字段  
            //foreach (DataRow k in dsAdministration.Tables[0].Rows)
            //{
            //    ZK.Model.USERS modelUser = new ZK.BLL.USERS().GetModel(int.Parse(k["ownerID"].ToString()));
            //    k["owner"] = modelUser.USERNAME;

            //     //ZK.Model.ZK_FileJP mdl = new ZK.BLL.ZK_FileJP().GetModel(int.Parse(k["fileTypeID"].ToString()));
            //     //k["fileTypeName"] = mdl.fileType;

            //    DataSet dt = new DataSet();

            //    dt = ftbll.GetList("fileid=" + k["fileID"]);
            //    if (dt.Tables[0].Rows.Count > 0)
            //    {
            //        k["createTime"] = tagbll.GetList("tagID=" + ftbll.GetList("fileid=" + k["fileID"]).Tables[0].Rows[0]["tagID"]).Tables[0].Rows[0]["createTime"];
            //        k["relevantNumxz"] = tagbll.GetList("tagID=" + ftbll.GetList("fileid=" + k["fileID"]).Tables[0].Rows[0]["tagID"]).Tables[0].Rows[0]["relevantNumxz"];
            //    }
            //}
            #endregion

            #endregion

            #region 根据行政分类筛选列表

            #endregion

            ViewData["video"] = new BLL.ZK_FileList().GetList("fileTypeID=1").Tables[0].Rows.Count;
            ViewData["doc"] = new BLL.ZK_FileList().GetList("fileTypeID=2").Tables[0].Rows.Count;
            ViewData["pic"] = new BLL.ZK_FileList().GetList("fileTypeID=3").Tables[0].Rows.Count;
            //ViewData["Administrative_ResourcesList"] = dsAdministration; 

            return View("IndexN");
        }


        static string XMLFilePath = "SystemSetting.xml";//xml路径E:\智慧教育系统\trunk\ZK.Manage\js\SystemSetting.xml

        int pageSize = 25;
        /// <summary>
        /// 文件列表页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AdministrationFileList(int? page)
        {
            string strSql = "";
            int strid = 0;

            bool b = int.TryParse(Request.QueryString["cid"], out strid);
            if (strid != 0)
            {
                int typeStr = Convert.ToInt32(Request.QueryString["type"]);
                if (typeStr == 3)
                {
                    strSql = "  fileID in (select fileID from ZK_ChannelGroupAndFileList where channelGroupID=" + strid + " and filetypeid=1)";
                }
                else if (typeStr == 2)
                {
                    strSql = "  fileID in (select fileID from ZK_ChannelGroupAndFileList where channelGroupID=" + strid + " and filetypeid=3)";
                }
                else if (typeStr == 1)
                {
                    strSql = "  fileID in (select fileID from ZK_ChannelGroupAndFileList where channelGroupID=" + strid + " and filetypeid=2)";
                }
                else
                    strSql = "  fileID in (select fileID from ZK_ChannelGroupAndFileList where channelGroupID=" + strid + ")";
            }
            else
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
                    strSql = " channelID=3 and filetypeid in (2,6,7,8)";
                }
                else
                    strSql = "  channelID=3";
            }
            DataSet dsAdministration = new DataSet();

            //string path = System.IO.Path.GetDirectoryName(Server.MapPath("../"));
            //string path2 = System.IO.Path.GetFileName(path);
            //string path3 = path.Substring(0, path.Length - path2.Length);
            //string path4 = path3 + "ZK.Manage";

            //XMLFilePath = path4 + "/SystemSetting.xml";
            string XMLFilePath = Server.MapPath(ZK.Common.ModelSettings.Pre_SysSettingXMLPath);
            pageSize = Convert.ToInt32(XMLHelper.GetXmlAttribute(XMLFilePath, "Settings/PageCount/Admin", "value").Value.ToString());

            dsAdministration = bllv_otherFile.GetList(pageSize, page.HasValue ? page.Value : 1, strSql);

            if (dsAdministration.Tables[0].Rows.Count > 0)
            {
                List<Model.View_OtherFileList> allfilemodel = bllv_otherFile.DataTableToList(dsAdministration.Tables[0]);
                ViewData["FileInfo"] = allfilemodel;
            }
            else
                ViewData["FileInfo"] = new List<Model.View_OtherFileList>();

            ViewData["page"] = "page";
            ViewData["pagesize"] = pageSize;
            ViewData["totlecount"] = bllv_otherFile.GetList(strSql).Tables[0].Rows.Count;

            return View();
        }

        /// <summary>
        /// 根据类型 获取 当日 七天内 一个月内的点击量排行榜
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private DataTable GetResourceList(int type)
        {

            StringBuilder builder = new StringBuilder();
            if (type == 1)
            {
                builder.Append(" DATEDIFF(DD,Convert(varchar(10),b.visitTime,102),Convert(varchar(10),getdate(),102))=0 ");
            }
            else if (type == 2)
            {
                builder.Append(" DATEDIFF(DD,Convert(varchar(10),b.visitTime,102),Convert(varchar(10),getdate(),102))<7 ");
            }
            else
            {
                builder.Append(" DATEDIFF(DD,Convert(varchar(10),b.visitTime,102),Convert(varchar(10),getdate(),102))<30 ");
            }
            builder.Append("and channelID=3  group by a.fileID,a.[fileName] ");

            string strselect = " a.fileID,a.[fileName],COUNT(b.USERID) as clickNum ";
            string strtable = " View_OtherFileList a left join ZK_FileVisitors b on a.fileID=b.fileID ";
            string orderby = " clickNum desc ";
            string strWhere = builder.ToString();

            DataSet ds = new BLL.CommondBase().GetList(strselect, strtable, "", orderby, ZK.Common.ModelSettings.ResourceCountList, 1, strWhere, 1);
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            string strwhere = "";
            foreach (DataRow item in dt.Rows)
            {
                strwhere += item["fileID"].ToString() + ",";
            }
            if (!string.IsNullOrEmpty(strwhere))
            {
                strwhere = strwhere.TrimEnd(',');
            }
            else
            {
                strwhere = "0";
            }
            //如果数量不够 则添加 已补充数量
            if (dt.Rows.Count < ZK.Common.ModelSettings.ResourceCountList)
            {
                builder.Clear();
                builder.Append(" a.fileID not in (" + strwhere + ") group by a.fileID,a.[fileName], a.createtime ");
                strwhere = builder.ToString();
                DataSet ds2 = new BLL.CommondBase().GetList(strselect, strtable, "", " a.createTime desc ", ZK.Common.ModelSettings.ResourceCountList - dt.Rows.Count, 1, strwhere, 1);
                foreach (DataRow item in ds2.Tables[0].Rows)
                {

                    DataRow dr = dt.NewRow();
                    dr[0] = item[0];
                    dr[1] = item[1];
                    dr[2] = item[2];
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
    }
}
