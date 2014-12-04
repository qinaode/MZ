using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Data;
using System.Web.SessionState;
using System.Web;

namespace ZK.Controllers
{
    [ZKAuthAttributeFilter(purl = "/")]
    public class DiskController : Controller, IRequiresSessionState
    {
        private static BLL.miniyun_files bll_miniyun_files = new BLL.miniyun_files();



        public ActionResult CheckUser()
        {
            string username = Request["username"];
            if (username == GetCurrentUserName())
            {
                return View("Index");
            }
            else
            {
                return Redirect("/account/login/");
            }

        }
        #region 空controller

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {

            return View();
        }

        /// <summary>
        /// 首页上面的框架
        /// </summary>
        /// <returns></returns>
        public ActionResult Top()
        {
            return View();
        }

        /// <summary>
        /// 首页左边的框架
        /// </summary>
        /// <returns></returns>
        public ActionResult Left()
        {
            return View();
        }

        /// <summary>
        /// 全部文件展示 图标形式
        /// </summary>
        /// <returns></returns>
        public ActionResult quanbuwenjian()
        {
            return View();
        }

        /// <summary>
        /// 全部文件展示 列表形式
        /// </summary>
        /// <returns></returns>
        public ActionResult quanbuwenjian01()
        {

            return View();
        }

        /// <summary>
        /// 别人分享给我的 列表
        /// </summary>
        /// <returns></returns>
        public ActionResult bierenfenxgeiwod()
        {
            return View();
        }

        /// <summary>
        /// 我的视频 图标
        /// </summary>
        /// <returns></returns>
        public ActionResult wodeshipin()
        {
            return View();
        }

        /// <summary>
        /// 我的视频 列表
        /// </summary>
        /// <returns></returns>
        public ActionResult wodeshipin01()
        {
            return View();
        }

        /// <summary>
        /// 我的文档 图标
        /// </summary>
        /// <returns></returns>
        public ActionResult wodewendang()
        {
            return View();
        }

        /// <summary>
        /// 我的文档 列表
        /// </summary>
        /// <returns></returns>
        public ActionResult wodewendang01()
        {
            return View();
        }

        /// <summary>
        /// 我的相册 图标 
        /// </summary>
        /// <returns></returns>
        public ActionResult xiangce()
        {
            return View();
        }

        /// <summary>
        /// 我的相册 列表
        /// </summary>
        /// <returns></returns>
        public ActionResult wodexiangce01()
        {
            return View();
        }
        #endregion

        /// <summary>
        /// 回收站 图标
        /// </summary>
        /// <returns></returns>
        public ActionResult huishouzhan()
        {

            StringBuilder builder = new StringBuilder();
            builder.Append(" user_id=" + GetCurrentDiskUserID() + " and   is_deleted = 1  ");
            //builder.Append(" and parent_file_id=1 ");

            ViewData["filelist"] = GetdataByConditions(builder.ToString());
            return View();
        }

        /// <summary>
        /// 回收站 列表
        /// </summary>
        /// <returns></returns>
        public ActionResult huishouzhan01(int? page)
        {

            //文件类型

            StringBuilder builder = new StringBuilder();
            builder.Append(" user_id=" + GetCurrentDiskUserID() + " and is_deleted = 1 ");

            int totalcount = 0;
            ViewData["pagesize"] = ZK.Common.ModelSettings.DiskFileListCount;
            ViewData["wenjianlist"] = GetDataByConditions(ZK.Common.ModelSettings.DiskFileListCount, page.HasValue ? page.Value : 1, builder.ToString(), out totalcount);
            ViewData["totalcount"] = totalcount;
            return View();
        }

        /// <summary>
        /// 我分享 图标
        /// </summary>
        /// <returns></returns>
        public ActionResult wofenxiang()
        {
            return View();
        }

        /// <summary>
        /// 文件列表  列表式
        /// </summary>
        /// <returns></returns>
        public ActionResult content_list(int? page)
        {
            //文件类型
            string type = Request["_type"];
            string parent_id = Request["parent_id"];
            StringBuilder builder = new StringBuilder();
            builder.Append(" user_id=" + GetCurrentDiskUserID() + " and is_deleted = 0 ");
            if (type == "all")
            {
                builder.Append(" and parent_file_id=" + parent_id + " ");
            }
            else if (type == "application")
            {
                builder.Append(" and parent_file_id=" + parent_id + " and mime_type like '" + type + "/ms%'");
            }
            else if (type == "delete")
            {
                builder.Clear();
                builder.Append(" user_id=" + GetCurrentDiskUserID() + " and is_deleted = 1 ");
            }
            else
            {
                builder.Append(" and parent_file_id=" + parent_id + " and mime_type like '" + type + "/%'");
            }
            int totalcount = 0;
            ViewData["pagesize"] = ZK.Common.ModelSettings.DiskFileListCount;
            ViewData["wenjianlist"] = GetDataByConditions(ZK.Common.ModelSettings.DiskFileListCount, page.HasValue ? page.Value : 1, builder.ToString(), out totalcount);
            ViewData["totalcount"] = totalcount;
            return View();
        }

        /// <summary>
        /// 文件列表页面  图标式
        /// </summary>
        /// <returns></returns>
        public ActionResult content_pic()
        {
            //文件类型
            string type = Request["_type"];
            string parent_id = Request["parent_id"];
            StringBuilder builder = new StringBuilder();
            builder.Append(" user_id=" + GetCurrentDiskUserID() + " and  is_deleted = 0  ");
            if (type == "all")
            {
                builder.Append(" and parent_file_id=" + parent_id);
            }
            else if (type == "application")
            {
                builder.Append(" and parent_file_id=" + parent_id + " and mime_type like '" + type + "/ms%'");
            }
            else if (type == "delete")
            {
                builder.Clear();
                builder.Append(" user_id=" + GetCurrentDiskUserID() + "  and  is_deleted = 1 ");
            }
            else
            {
                builder.Append(" and parent_file_id=" + parent_id + " and mime_type like '" + type + "/%'");
            }
            ViewData["filelist"] = GetdataByConditions(builder.ToString());
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

        /// <summary>
        /// 根据条件 获取 数据分页列表
        /// </summary>
        /// <param name="pagesize"></param>
        /// <param name="pageindex"></param>
        /// <param name="strwhere"></param>
        /// <returns></returns>
        private List<Model.miniyun_files> GetDataByConditions(int pagesize, int pageindex, string strwhere, out int totalcount)
        {

            DataSet ds = bll_miniyun_files.GetList(pagesize, pageindex, strwhere + " ORDER BY file_type DESC ,created_at desc ");
            totalcount = bll_miniyun_files.GetModelList(strwhere).Count;
            if (ds != null && ds.Tables.Count > 0)
            {
                List<Model.miniyun_files> list = bll_miniyun_files.DataTableToList(ds.Tables[0]);
                return list;
            }
            else
            {
                return new List<Model.miniyun_files>() { };
            }

        }

        /// <summary>
        /// 通过条件获取数据 不分页
        /// </summary>
        /// <returns></returns>
        private List<Model.miniyun_files> GetdataByConditions(string strwhere)
        {

            List<Model.miniyun_files> list = bll_miniyun_files.GetModelList(strwhere + " ORDER BY file_type DESC ,created_at desc ");
            return list;
        }

        /// <summary>
        /// 获取当前用户的ID
        /// </summary>
        /// <returns></returns>
        private string GetCurrentUserID()
        {
            if (TempData["uid"] != null)
            {
                Session["uid"] = TempData["uid"];
                Session["username"] = TempData["username"];
                Session["user"] = TempData["user"];
            }
            else if (Session["uid"] == null)
            {
                Response.Redirect("/account/login/");
            }
            return Session["uid"].ToString();
        }


        private string GetCurrentUserName()
        {
            if (TempData["uid"] != null)
            {
                Session["uid"] = TempData["uid"];
                Session["username"] = TempData["username"];
                Session["user"] = TempData["user"];
            }
            else if (Session["uid"] == null)
            {
                Response.Redirect("/account/login/");
            }
            return Session["username"].ToString();
        }


        ////----------------------------------------
        /// <summary>
        /// 文件列表页面  图标式
        /// </summary>
        /// <returns></returns>
        public ActionResult content_pic2()
        {
            //文件类型
            string type = Request["_type"];
            string parent_id = Request["parent_id"];
            StringBuilder builder = new StringBuilder();
            builder.Append(" user_id=" + GetCurrentDiskUserID() + " and  is_deleted = 0  ");
            if (type == "all")
            {
                builder.Append(" and parent_file_id=" + parent_id);
            }
            else if (type == "application")
            {
                builder.Append(" and parent_file_id=" + parent_id + " and mime_type like '" + type + "/ms%'");
            }
            else if (type == "delete")
            {
                builder.Clear();
                builder.Append(" user_id=" + GetCurrentDiskUserID() + "  and  is_deleted = 1 ");
            }
            else
            {
                builder.Append(" and parent_file_id=" + parent_id + " and mime_type like '" + type + "/%'");
            }
            ViewData["filelist"] = GetdataByConditions(builder.ToString());
            return Json(GetdataByConditions(builder.ToString()));
        }

        /// <summary>
        /// 获取当前的使用网盘的用户id
        /// </summary>
        /// <returns></returns>
        private string GetCurrentDiskUserID()
        {
            string username = GetCurrentUserName();

            return new BLL.miniyun_users().GetList(" user_name='" + username+"'").Tables[0].Rows[0]["id"].ToString();
        }


    }
}
