using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZK.BLL;
using ZK.Model;
using System.Data;
using System.Text;
using ZK.Common;

namespace ZK.Controllers
{
    [ZKAuthAttributeFilter(purl = "/Home/Index")]
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public string strCurrent = "index";
        public ActionResult Index()
        {
            //精品内容
            string strJPTitle = new BLL.ZK_FileJPType().GetList("isopen=1").Tables[0].Rows[0]["typename"].ToString();
            List<Model.ZK_FileJP> modelFileJPList = new List<Model.ZK_FileJP>();
            modelFileJPList = new BLL.ZK_FileJP().GetModelList("typeid=(select id from ZK_FileJPType where isopen=1)");
            if (modelFileJPList != null && modelFileJPList.Count > 0)
            {
                ViewData["jpResouceList"] = modelFileJPList;
            }
            else
            {
                ViewData["jpResouceList"] = new Model.ZK_FileJP();
            }
            ViewData["JPTitle"] = strJPTitle;

            ViewData["JPTypeID"] = new BLL.ZK_FileJPType().GetList("isopen=1").Tables[0].Rows[0]["id"].ToString();

            //ViewData["JPTitle"] = (new TohtmlServices.To()).FileToHtml("c:\\doc\\test.doc");
            ViewData["video"] = new BLL.ZK_FileList().GetList("fileTypeID=1").Tables[0].Rows.Count;
            ViewData["doc"] = new BLL.ZK_FileList().GetList("fileTypeID=2").Tables[0].Rows.Count;
            ViewData["pic"] = new BLL.ZK_FileList().GetList("fileTypeID=3").Tables[0].Rows.Count;
            ViewData["audio"] = new BLL.ZK_FileList().GetList("fileTypeID=4").Tables[0].Rows.Count;
            ViewData["other"] = new BLL.ZK_FileList().GetList("fileTypeID=5").Tables[0].Rows.Count;
            ViewData["current"] = strCurrent;
            BLL.View_AllFileList bllv_AllFile = new BLL.View_AllFileList();
            StringBuilder builder = new StringBuilder();

            #region 资源排行 日排行

            ViewData["v_allfilelistDay"] = GetResourceList(1);
            #endregion

            #region 资源排行 周排行

            ViewData["v_allfilelistWeek"] = GetResourceList(2);
            #endregion

            #region 资源排行 月排行

            ViewData["v_allfilelistMonth"] = GetResourceList(3);

            #endregion

            #region 专题图片
            ZK.BLL.ZK_FileJPPic bllImgJP = new BLL.ZK_FileJPPic();
            string strImg = " fileJPTypeID in (select id from ZK_FileJPType where isOpen=1)";
            List<Model.ZK_FileJPPic> imgList = bllImgJP.GetModelList(strImg);
            ViewData["imgList"] = imgList;
            #endregion
            return View("IndexM");

            //获取统计数据


        }
        /// <summary>
        /// 按照频道获取文档数据
        /// </summary>
        /// <returns></returns>
        public ActionResult getByType()
        {
            string filetype = Request.Form["typeID"];
            System.Data.DataSet dsFileList = new DataSet();
            try
            {
                if (filetype == "0")
                {
                    //group by fileOldID ,User
                    dsFileList = new BLL.View_AllFileList().GetList(20, "1=1 ", "createtime desc");
                    List<Model.View_AllFileList> listss = new BLL.View_AllFileList().GetModelList(" 1=1 order by createtime desc ");
                    return Json(new BLL.View_AllFileList().DataTableToList(dsFileList.Tables[0]));

                }
                else if (filetype == "1")
                {
                    dsFileList = new BLL.View_TeachFileList().GetList(20, "channelID=1 ", "createtime desc");
                    return Json(new BLL.View_TeachFileList().DataTableToList(dsFileList.Tables[0]));
                }
                else if (filetype == "2")
                {
                    dsFileList = new BLL.View_OtherFileList().GetList(20, "channelID=2 ", "createtime desc");
                    return Json(new BLL.View_OtherFileList().DataTableToList(dsFileList.Tables[0]));
                }
                else if (filetype == "3")
                {
                    dsFileList = new BLL.View_OtherFileList().GetList(20, "channelID=3 ", "createtime desc");
                    return Json(new BLL.View_OtherFileList().DataTableToList(dsFileList.Tables[0]));
                }
                return Json("");

                //JsonResult jsList = Json(new BLL.View_AllFileList().DataTableToList(dsFileList.Tables[0]));
                //return jsList;
            }
            catch
            {
                return Json("");
            }

        }

        /// <summary>
        /// 获取排行榜列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetRankList()
        {
            string flag = Request.Form["flag"].ToLower();
            DataTable dt = new DataTable();
            string result = "";
            switch (flag)
            {
                case "week":
                    result = Common.JSONHelper.DataTableToJson("DataList", GetResourceList(2));
                    break;
                case "month":
                    result = Common.JSONHelper.DataTableToJson("DataList", GetResourceList(3));
                    break;
                default:
                    break;
            }
            return Json(result);
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
            builder.Append("  group by a.fileID,a.[fileName] ");

            string strselect = " a.fileID,a.[fileName],COUNT(b.USERID) as clickNum ";
            string strtable = " View_AllFileList a left join ZK_FileVisitors b on a.fileID=b.fileID ";
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

        /// <summary>
        /// 获取XML文档数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetXMLData()
        {
            try
            {
                string XMLFilePath = Server.MapPath(ZK.Common.ModelSettings.Pre_SysSettingXMLPath);
                string strImgURL = XMLHelper.GetXmlAttribute(XMLFilePath, "Settings/Logo", "value").Value.ToString();
                //strImgURL = strImgURL.Replace("images", "Files/Logo");
                ////得到用户的头像
                //string pathFace = "/Files/Faces/";
                ////string userid = Request.Form["UID"];
                //string userid = ((ZK.Model.USERS)(Session["user"])).USERID.ToString();
                //if (userid != null && userid != "")
                //{
                //    ZK.Model.USERS user = new ZK.BLL.USERS().GetModel(Convert.ToInt32(userid));
                //    if (user != null)
                //    {
                //        string userFace = user.FACEFILE;
                //        if (String.IsNullOrEmpty(userFace))
                //        {
                //            pathFace = "";
                //        }
                //        else
                //            pathFace = "/Files/Faces/" + userFace;
                //    }

                //    ViewData["face"] = pathFace;
                //}
                return Json(strImgURL);
            }
            catch
            {
                return Json("");
            }

        }

        /// <summary>
        /// 用户资源贡献量排行榜
        /// </summary>
        /// <returns></returns>
        public ActionResult UserResCountRank()
        {
            #region 贡献量 总排行
            StringBuilder builder = new StringBuilder();
            builder.Clear();
            string select_list = "  USERNAME ,COUNT(fileid) as FCount  ";
            string tablename = " View_AllFileList   ";
            string where = " 1=1 group by USERNAME  ";
            string primary_key = " ";
            string order_by = " FCount desc ";
            int page_size = ZK.Common.ModelSettings.UserResCountRank;
            int page_index = 1;
            int bl_page = 1;
            DataTable dt = new ZK.BLL.CommondBase().GetList(select_list, tablename, primary_key, order_by, page_size, page_index, where, bl_page).Tables[0];
            Dictionary<string, string> mora_dic = new Dictionary<string, string>();
            foreach (DataRow dr in dt.Rows)
            {
                mora_dic.Add(dr[0].ToString(), dr[1].ToString());
            }
            ViewData["dicForCount"] = mora_dic;


            #endregion

            #region 贡献 周排行

            string where2 = " DATEDIFF(DD,Convert(varchar(10),[createTime],102),Convert(varchar(10),getdate(),102))<=7  group by USERNAME ";

            DataTable dtw = new ZK.BLL.CommondBase().GetList(select_list, tablename, primary_key, order_by, page_size, page_index, where2, bl_page).Tables[0];
            Dictionary<string, int> dicForCountWeek = new Dictionary<string, int>();
            foreach (DataRow dr in dtw.Rows)
            {
                dicForCountWeek.Add(dr[0].ToString(), Convert.ToInt32(dr[1]));
            }
            //如果数量少 则填充数据
            if (dtw.Rows.Count < ZK.Common.ModelSettings.UserResCountRank)
            {
                foreach (var item in mora_dic)
                {
                    if (dicForCountWeek.Count > ZK.Common.ModelSettings.UserResCountRank)
                    {
                        break;
                    }
                    if (!dicForCountWeek.ContainsKey(item.Key))
                    {
                        dicForCountWeek.Add(item.Key, Convert.ToInt32(item.Value));
                    }
                }
            }


            ViewData["dicForCountWeek"] = dicForCountWeek;

            #endregion
            return PartialView();
        }
    }
}
