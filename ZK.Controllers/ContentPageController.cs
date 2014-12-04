using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Data;
using ZK.BLL;
using ZK.Model;
using System.IO;

namespace ZK.Controllers
{
    [ZKAuthAttributeFilter(purl = "/")]
    public class ContentPageController : Controller
    {
        //
        // GET: /ContentPage/

        BLL.View_AllFileList bllv_allfilelist = new BLL.View_AllFileList();
        BLL.ZK_FileAndTags bllFAT = new BLL.ZK_FileAndTags();
        BLL.ZK_Tags blltags = new BLL.ZK_Tags();

        public ActionResult Index()
        {

            try
            {
                string fileID = Request["file_id"];
                string historyUrl = Request["url_flag"];//url存放的频道类型 用于返回按钮
                //返回按钮的链接地址
                string url = "#";

                switch (historyUrl)
                {
                    case "Teach":
                        url = "/Teach/Index";
                        break;
                    case "Admin":
                        url = "/Administration/Index";
                        break;
                    case "Moral":
                        url = "/Moral/Index";
                        break;
                    case "Home":
                        url = "/Home/Index";
                        break;
                    case "Search":
                        url = "/Search/Index";
                        break;
                    default:
                        url = "#";
                        break;
                }
                if (!string.IsNullOrEmpty(fileID))
                {

                    // List<Model.ZK_FileList>
                    Model.ZK_FileList filemodel = new BLL.ZK_FileList().GetModel(Convert.ToInt32(fileID));
                    //更新点击量

                    if (filemodel.clickNum.ToString() == "")
                    {
                        filemodel.clickNum = 0;
                    }
                    filemodel.clickNum = filemodel.clickNum + 1;
                    new BLL.ZK_FileList().Update(filemodel);

                    ////在最近访问表里添加数据
                    Model.ZK_FileVisitors visitormodel = new Model.ZK_FileVisitors();
                    visitormodel.fileID = Convert.ToInt32(fileID);
                    visitormodel.USERID = GetCurrentUser();
                    visitormodel.visitTime = DateTime.Now;
                    new BLL.ZK_FileVisitors().Add(visitormodel);


                    ViewData["trastatus"] = filemodel.trastatus;
                    //查询关键字
                    List<Model.ZK_FileAndTags> FATModels = bllFAT.GetModelList("fileID=" + fileID);
                    StringBuilder builder = new StringBuilder();
                    foreach (var item in FATModels)
                    {
                        builder.Append(blltags.GetModelList("tagID=" + item.tagID)[0].tagName + " ");
                    }
                    ViewData["KeyWords"] = builder.ToString();
                    Model.View_AllFileList allfilemodel = bllv_allfilelist.GetModelList("fileID=" + fileID)[0];

                    ViewData["FileInfo"] = allfilemodel;

                    //获取文件路径
                    Model.ZK_FileList model = new BLL.ZK_FileList().GetModel(Convert.ToInt32(fileID));
                    Model.miniyun_files m_filemodel = new BLL.miniyun_files().GetModel(Convert.ToInt32(model.fileOldID));
                    string hashfilename = "";
                    ViewData["filepath"] = Common.ModelSettings.CRootPath + "/" + GetFilePathByVersionID(m_filemodel.version_id.ToString(), out hashfilename) + "/" + hashfilename;
                    ViewData["title"] = model.fileName;
                    ViewData["VideoLogo"] = ZK.Common.ModelSettings.Video_LogoPath;
                }
                else
                {
                    ViewData["FileInfo"] = new Model.View_AllFileList();
                }


                #region 浏览过的其他文件

                //ZK.BLL.View_AllFileList bllView = new BLL.View_AllFileList();
                //ZK.Model.View_AllFileList mdlView = new Model.View_AllFileList();
                //DataSet dsViewed = new DataSet();
                //int viewedFileID = Convert.ToInt32(Request.QueryString["file_id"]);
                //string strwhere = " fileID in(select fileID from zk_fileVisitors where userid in (select USERID from ZK_FileVisitors where fileID=" + viewedFileID + ") group by fileID) and fileid <>" + viewedFileID + " order by clickNum desc ";
                //dsViewed = bllView.GetList(strwhere);
                //List<Model.View_AllFileList> lists = new BLL.View_AllFileList().DataTableToList(dsViewed.Tables[0]);


                int viewedFileID = Convert.ToInt32(Request.QueryString["file_id"]);
                string strselect = " * ";
                string strtable = " View_AllFileList ";
                string orderby = " clickNum desc ";
                string strWhere = " fileID in(select fileID from zk_fileVisitors where userid in (select USERID from ZK_FileVisitors where fileID=" + viewedFileID + ") group by fileID) and fileid <>" + viewedFileID + " ";

                DataSet dsViewed = new BLL.CommondBase().GetList(strselect, strtable, "", orderby, ZK.Common.ModelSettings.VisitedFilesCount, 1, strWhere, 1);
                ViewData["ViewedList"] = dsViewed;


                #endregion

                ViewData["HistoryURL"] = url;
                return View("IndexN");
            }
            catch
            {
                ViewData["FileInfo"] = new Model.View_AllFileList();
                return View("IndexN");
            }

        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <returns></returns>
        public ActionResult DownLoad()
        {
            string filepath = Request["filepath"];
            string filename = Request["filename"];

            filename = HttpUtility.UrlDecode(filename, Encoding.Default);

            string filetype = System.IO.Path.GetExtension(filename);

            //ZK.Common.DownLoad.DownLoadFile(Response, Server.MapPath(filepath), filetype, filename);
            //调用不是读取文件流的下载方式，不然大文件会出现卡死现象
            ZK.Common.DownLoad.DownLoadFile(Response, Server.MapPath(filepath),filename);
            return View();
        }

        /// <summary>
        /// 文档预览页面
        /// </summary>
        /// <returns>页面内容</returns>
        public ActionResult DocumentContent()
        {
            return View();
        }

        /// <summary>
        /// 文档预览页面
        /// </summary>
        /// <returns>页面内容</returns>
        public ActionResult GetDocumentContent()
        {

            string fileid = Request["_id"];
            if (fileid != "")
            {
                fileid = fileid.Substring(1);
                //获取文件的路径 从源路径中查找html页的路径
                Model.ZK_FileList filelistmodel = new BLL.ZK_FileList().GetModel(Convert.ToInt32(fileid));
                if (filelistmodel != null)
                {

                    string versionid = new BLL.miniyun_files().GetModel((int)filelistmodel.fileOldID).version_id.ToString();
                    string hashfilename = "";
                    string LDPath = Common.ModelSettings.CRootPath + "/" + GetFilePathByVersionID(versionid, out hashfilename) + "/" + hashfilename;
                    if (ZK.Common.ModelSettings.IsModelData)
                    {
                        LDPath = ZK.Common.ModelSettings.LDPath;
                    }
                    if (filelistmodel.trastatus == 0)
                    {
                        if (ParseDocumentToHtml(Server.MapPath(LDPath), System.IO.Path.GetExtension(filelistmodel.fileName).ToLower()) == "success")
                        {
                            //修改转换状态
                            filelistmodel.trastatus = 2;
                        }
                        else
                        {
                            //return Content(ParseDocumentToHtml(Server.MapPath(LDPath), System.IO.Path.GetExtension(filelistmodel.fileName).ToLower()));
                            return Content("<script>alert('转码错误，返回资源页面');  window.location='/ContentPage/Index?file_id=" + filelistmodel.fileID + "&url_flag=Home';</script>");
                        }
                    }
                    new BLL.ZK_FileList().Update(filelistmodel);
                    //获取不带扩展名的文件名
                    string targetname = ZK.Common.CommonFunction.GetPathWithoutExtension(LDPath);
                    return Json(targetname + ".html");
                }
            }
            return Json("#");
        }

        /// <summary>
        /// PPT显示页面
        /// </summary>
        /// <returns></returns>
        public ActionResult PPTContent()
        {
            string pptid = Request["_id"].ToString();
            // pptid = pptid.Substring(1);
            int id = 0;
            if (int.TryParse(pptid, out id))
            {


                Model.ZK_FileList model = new BLL.ZK_FileList().GetModel(id);
                if (model != null)
                    ViewData["PPTName"] = model.fileName;
                else
                    ViewData["PPTName"] = "暂未获取数据";
            }
            else
            {
                ViewData["PPTName"] = "暂未获取数据";
            }
            return View();
        }

        /// <summary>
        /// 获取ppt内容
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPPTContent()
        {

            string pptid = Request["_id"].ToString();
            pptid = pptid.Substring(1);
            int id = 0;
            if (int.TryParse(pptid, out id))
            {


                Model.ZK_FileList model = new BLL.ZK_FileList().GetModel(id);

                //ppt的file_path
                string LPpath = model.filePath;
                string versionid = new BLL.miniyun_files().GetModel(Convert.ToInt32(model.fileOldID)).version_id.ToString();
                string hashfilename = "";
                LPpath = Common.ModelSettings.CRootPath + "/" + GetFilePathByVersionID(versionid, out hashfilename) + "/" + hashfilename;

                if (ZK.Common.ModelSettings.IsModelData)
                {
                    LPpath = ZK.Common.ModelSettings.LPpath;
                }
                if (model.trastatus == 0)
                {
                    if (ParseDocumentToHtml(Server.MapPath(LPpath), System.IO.Path.GetExtension(model.fileName).ToLower()) == "success")
                    {
                        model.trastatus = 2;
                    }
                    else
                    {

                        return Content("<script>alert('转码错误，返回资源页面');  window.location='/ContentPage/Index?file_id=" + model.fileID + "&url_flag=Home';</script>");
                    }
                    //修改转换状态 2 转码成功

                }

                new BLL.ZK_FileList().Update(model);
                //模拟路径
                string dirPath = Common.CommonFunction.GetPathWithoutExtension(LPpath) + "img";

                string[] filenames = System.IO.Directory.GetFiles(Server.MapPath(dirPath));

                return Json(filenames.Length + "," + dirPath);
            }

            return Json(",");
        }


        /// <summary>
        /// PPT 图片 word excel 文档
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="targetPath"></param>
        /// <returns></returns>
        private string ParseDocumentToHtml(string filepath, string extname)
        {
            return new ZK.WebService.ToHtml().FileToHtml(filepath, extname);
        }

        /// <summary>
        /// 获取用户的id
        /// </summary>
        /// <returns></returns>
        private int GetCurrentUser()
        {
            if (TempData["uid"] != null)
            {
                Session["uid"] = TempData["uid"];
            }
            else if (Session["uid"] == null)
            {
                Response.Redirect("/account/login/");
            }
            return Convert.ToInt32(Session["uid"]);
        }

        /// <summary>
        /// 通过versionid来获取该文件的地址和文件的hash文件名
        /// </summary>
        /// <param name="versionid"></param>
        /// <returns>返回 12/34/56/78</returns>
        private string GetFilePathByVersionID(string versionid, out string hashfilename)
        {
            string filepath = "";
            BLL.miniyun_file_versions bll_version = new BLL.miniyun_file_versions();
            Model.miniyun_file_versions model = bll_version.GetModel(Convert.ToInt32(versionid));
            string hashname = model.file_signature;
            string Firstdir = hashname.Substring(0, 2);
            string Seconddir = hashname.Substring(2, 2);
            string Thriddir = hashname.Substring(4, 2);
            string Forthdir = hashname.Substring(6, 2);
            filepath = Firstdir + "/" + Seconddir + "/" + Thriddir + "/" + Forthdir;

            hashfilename = hashname;
            return filepath;
        }

        /// <summary>
        /// 根据图片id获取图片的路径
        /// </summary>
        /// <param name="picid"></param>
        /// <param name="type">要获取图片的类型 256 base</param>
        /// <returns></returns>
        public static string GetImageUrlPath(string picid, string type)
        {
            Model.ZK_FileList filelist = new BLL.ZK_FileList().GetModel(Convert.ToInt32(picid));
            if (filelist == null)
            {
                return "";
            }
            Model.miniyun_files file = new BLL.miniyun_files().GetModel(Convert.ToInt32(filelist.fileOldID));
            if (file == null)
            {
                return "";
            }
            string hashfilename = "";
            string urlpath = "";
            string filepath = new ZK.Controllers.ContentPageController().GetFilePathByVersionID(file.version_id.ToString(), out hashfilename);
            if (type == "base")
            {
                urlpath = Common.ModelSettings.CreateFileDefaultPath + "/" + filepath + "/" + hashfilename;
            }
            else if (type == "256")
            {
                urlpath = Common.ModelSettings.CreateFileDefaultPath + "/" + filepath + "/" + hashfilename + "_256.png";
            }
            return urlpath;
        }
    }
}
