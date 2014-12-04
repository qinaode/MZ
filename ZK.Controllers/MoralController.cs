using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Text;
using ZK.Common;
using System.Xml;

namespace ZK.Controllers
{
    [ZKAuthAttributeFilter(purl = "/")]
    public class MoralController : Controller
    {
        //
        // GET: /Moral/
        BLL.ZK_FileList bllfilelist = new BLL.ZK_FileList();
        BLL.ZK_ChannelGroup bllchannelGroup = new BLL.ZK_ChannelGroup();
        BLL.ZK_ChannelGroupAndFileList bllgroupfilelist = new BLL.ZK_ChannelGroupAndFileList();
        BLL.View_AllFileList bllv_AllFile = new BLL.View_AllFileList();
        BLL.View_OtherFileList bllv_otherfile = new BLL.View_OtherFileList();
        public string strCurrent = "moral"; 
        public ActionResult Index()
        {
            ViewData["current"] = strCurrent;
            try
            {
                #region 资源总数
                int TotalFileNum = bllv_AllFile.GetRecordCount(" channelID=2");
                ViewData["TotalFileNum"] = TotalFileNum;


                #endregion

                #region 获取groupName 和 文件数 的列表
                string strWhere = "channelID=2 and channelGroupParent=1 ";
                Dictionary<string, int> dic = new Dictionary<string, int>();
                Dictionary<string, int> dic1 = new Dictionary<string, int>();
                Dictionary<string, string> dic2 = new Dictionary<string, string>();
                List<Model.ZK_ChannelGroup> lists = bllchannelGroup.GetModelList(strWhere);

                foreach (var item in lists)
                {
                    dic.Add(item.channelGroupName, item.channelGroupID);

                    //string strWhere1 = "channelID=2 and channelGroupParent= " + item.channelGroupID;    
                    //List<Model.ZK_ChannelGroup> lists1 = bllchannelGroup.GetModelList(strWhere1);

                    //foreach (var item1 in lists1)
                    //{
                    //    dic1.Add(item1.channelGroupName, item1.channelGroupID); 
                    //}                     

                    dic2.Add(item.channelGroupName, item.channelGroupDesc);
                }
                ViewData["GroupNameList"] = dic;
                ViewData["GroupMoralNameList"] = lists;

                ViewData["GroupDescList"] = dic2;
                #endregion

                #region 资源左下框展示数据列表
                StringBuilder builder = new StringBuilder();
                builder.Append(" channelID=2 ");
                List<Model.View_OtherFileList> v_otherfilelist = bllv_otherfile.GetModelList(builder.ToString());

                ViewData["v_otherfilelist"] = v_otherfilelist;

                #endregion

                #region 文档排行 日排行

                ViewData["v_allfilelistDay"] = GetResourceList(1);

                #endregion

                #region 文档排行 周排行

                ViewData["v_allfilelistWeek"] = GetResourceList(2);

                #endregion

                #region 文档排行 月排行

                ViewData["v_allfilelistMonth"] = GetResourceList(3);

                #endregion

                #region 幻灯片切换


                //ViewData["ppt_img"] = readImgPath();
                ViewData["ppt_img"] = ReadPptImg();

                #endregion

                return View("IndexN");
            }
            catch
            {
                return View("IndexN");
            }
        }
        /// <summary>
        /// 通过条件获取相应的德育分类的id集合
        /// </summary>
        /// <returns></returns>
        private string GetchannelGroupIDs()
        {
            string strWhere = "channelID=2 ";

            BLL.ZK_ChannelGroup bllgroup = new BLL.ZK_ChannelGroup();
            List<Model.ZK_ChannelGroup> lists = bllgroup.GetModelList(strWhere);
            string listid = "";
            if (lists == null || lists.Count < 1)
            {
                return "0";
            }
            foreach (var item in lists)
            {
                listid += item.channelGroupID + ",";
            }
            return listid.TrimEnd(',');
        }


        /// <summary>
        /// 按照频道分类
        /// </summary>
        /// <returns></returns>
        public ActionResult getByType()
        {
            int filetype = Convert.ToInt32(Request.Form["typeID"]);
            System.Data.DataSet dsFileList = new DataSet();
            try
            {
                string strWhere = "channelID=2 and channelGroupParent= " + filetype;
                //Dictionary<string, int> dic1 = new Dictionary<string, int>();

                //List<Model.ZK_ChannelGroup> lists = bllchannelGroup.GetModelList(strWhere);

                //foreach (var item in lists)
                //{
                //    dic1.Add(item.channelGroupName, item.channelGroupID);

                //}
                //ViewData["GroupMoralNameList"] = dic1;

                dsFileList = new BLL.ZK_ChannelGroup().GetList(strWhere);
                if (dsFileList.Tables[0].Rows.Count > 0)
                {
                    return Json(new BLL.ZK_ChannelGroup().DataTableToList(dsFileList.Tables[0]));
                }
                else
                    return Json("");
            }
            catch
            {
                return Json("");
            }

        }

        public List<ZK.Model.ZK_ChannelGroup> GetLessonByUnit(string itemstr)
        {
            List<ZK.Model.ZK_ChannelGroup> lessonList3 = bllchannelGroup.GetModelList("channelGroupParent=" + itemstr);
            return lessonList3;
        }

        static string XMLFilePath = "SystemSetting.xml";//xml路径E:\智慧教育系统\trunk\ZK.Manage\js\SystemSetting.xml

        int pageSize = 25;
        /// <summary>
        /// 文件列表页面
        /// </summary>
        /// <returns></returns>
        public ActionResult MoralFileList(int? page)
        {

            string strSql = "";
            int strid = 0;
            int moralId = 0;

            bool a = int.TryParse(Request.QueryString["mid"], out moralId);
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
            else if (moralId != 0)
            {
                int typeStr = Convert.ToInt32(Request.QueryString["type"]);
                if (typeStr == 3)
                {
                    strSql = "  fileID in (select fileID from ZK_ChannelGroupAndFileList where channelGroupID=" + moralId + " and filetypeid=1)";
                }
                else if (typeStr == 2)
                {
                    strSql = "  fileID in (select fileID from ZK_ChannelGroupAndFileList where channelGroupID=" + moralId + " and filetypeid=3)";
                }
                else if (typeStr == 1)
                {
                    strSql = "  fileID in (select fileID from ZK_ChannelGroupAndFileList where channelGroupID=" + moralId + " and filetypeid=2)";
                }
                else
                    strSql = "  fileID in (select fileID from ZK_ChannelGroupAndFileList where channelGroupID=" + moralId + ")";
            }
            else
            {
                if (Convert.ToInt32(Request.QueryString["type"]) == 3)
                {
                    strSql = " channelID=2 and filetypeid=1";
                }
                else if (Convert.ToInt32(Request.QueryString["type"]) == 2)
                {
                    strSql = " channelID=2 and filetypeid=3";
                }
                else if (Convert.ToInt32(Request.QueryString["type"]) == 1)
                {
                    strSql = " channelID=2 and filetypeid in (2,6,7,8)";
                }
                else
                    strSql = "  channelID=2";
            }
            DataSet dsAdministration = new DataSet();

            //string path = System.IO.Path.GetDirectoryName(Server.MapPath("../"));
            //string path2 = System.IO.Path.GetFileName(path);
            //string path3 = path.Substring(0, path.Length - path2.Length);
            //string path4 = path3 + "ZK.Manage";

            //XMLFilePath = path4 + "/SystemSetting.xml";
            string XMLFilePath = Server.MapPath(ZK.Common.ModelSettings.Pre_SysSettingXMLPath);
            pageSize = Convert.ToInt32(XMLHelper.GetXmlAttribute(XMLFilePath, "Settings/PageCount/Moral", "value").Value.ToString());

            dsAdministration = bllv_otherfile.GetList(pageSize, page.HasValue ? page.Value : 1, strSql);

            if (dsAdministration.Tables[0].Rows.Count > 0)
            {
                List<Model.View_OtherFileList> allfilemodel = bllv_otherfile.DataTableToList(dsAdministration.Tables[0]);
                ViewData["FileInfo"] = allfilemodel;
            }
            else
                ViewData["FileInfo"] = new List<Model.View_OtherFileList>();

            ViewData["page"] = "page";
            ViewData["pagesize"] = pageSize;
            ViewData["totlecount"] = bllv_otherfile.GetList(strSql).Tables[0].Rows.Count;

            return View();
        }

       
        /// <summary>
        /// 读取德育页面ppt图像路径
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> ReadPptImg()
        {
            Dictionary<string, string> pptdic = new Dictionary<string, string>(); 
            string XMLFilePath = Server.MapPath( "/Files/Moral/Images.xml");
            //string XMLFilePath = "/files/bfiles/Moral/Images.xml";
            //string XMLNodePath = "Images/Moral/img";
   
            string XMLNodePath = "Images/Moral/img";
            XmlNodeList imgPath = XMLHelper.GetXmlNodeListByXpath(XMLFilePath, XMLNodePath);
            int i = 0;
             
           
            foreach (XmlNode node in imgPath)
            {
                if (node.Attributes["pic_state"].Value == "0")
                {
                    
                    pptdic.Add(node.InnerText, node.Attributes["pic_link"].Value);           
                    i++;                 
                }
            }
            return pptdic;

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
            builder.Append("and channelID=2  group by a.fileID,a.[fileName] ");

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
            if (dt.Rows.Count < 6)
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
