using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Data;
using System.Web;
using System.Web.SessionState;
using System.Web.Script.Serialization;
using System.IO;
using RESTfulServices;
using System.Xml;
using ZK.Model;
using ZK.Common;
using System.Text.RegularExpressions;
using ICSharpCode.SharpZipLib.Zip;

namespace ZK.Controllers
{
    public class DiskNController : Controller, IRequiresSessionState
    {

        #region 公共静态字段

        private static BLL.miniyun_files bll_miniyun_files = new BLL.miniyun_files();
        private static BLL.miniyun_events bll_miniyun_events = new BLL.miniyun_events();
        private static BLL.miniyun_file_versions bll_miniyun_versions = new BLL.miniyun_file_versions();
        private static BLL.miniyun_share_files bll_miniyun_share_files = new BLL.miniyun_share_files();
        private static BLL.file_collection bll_file_collection = new BLL.file_collection();
        private static BLL.miniyun_file_metas bll_file_metas = new BLL.miniyun_file_metas();
        private static BLL.public_files bll_public_files = new BLL.public_files();
        private static BLL.ZK_RoleList bll_RoleList = new BLL.ZK_RoleList();
        private static BLL.ZK_RoleToUser bll_RoleToUser = new BLL.ZK_RoleToUser();
        private static string username = "";
        #endregion

        #region Actions

        /// <summary>
        /// 检查用户是否登录
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckUser()
        {

            string username = Request["username"];
            if (username == GetCurrentUserName())
            {
                Session["username"] = username;
                return RedirectToAction("Index");
                // return View("Index");
            }
            else
            {
                return Redirect("/account/login/");
            }

        }

        public dynamic GetLoginUser()
        {

            if (Session["uid"] != null && Session["username"] != null && Session["user"] != null)
            {
                //这里将用户转换一下,历史遗留问题，等待优化
                return new { uid = ChangeUserId(Convert.ToInt32(Session["uid"].ToString())).ToString(), username = Session["username"].ToString() };
            }
            else
            {
                Response.Redirect("/account/login/");
                return null;
            }

        }
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            /*调试使为了方便不登录*/
            //判断用户是否登录
            var loginuser = GetLoginUser();
            string XMLFilePath = Request.PhysicalApplicationPath + ZK.Common.ModelSettings.BH_SysSettingXMLPath;
            string webtitle = Common.XMLHelper.GetXmlAttribute(XMLFilePath, "Settings/WebTitle", "value").Value;
            ViewData["webtitle"] = webtitle;
            ViewData["username"] = loginuser.username;
            ViewData["uid"] = loginuser.uid;
            return View("index");
        }

        /// <summary>
        /// 根据类型获取相关类型下的资源
        /// </summary>
        /// <returns></returns>
        public ActionResult GetResContentByType()
        {
            //文件类型
            string type = Request.Form["type"];
            string parent_id = Request.Form["parent_id"];
            string rankflag = Request.Form["rankflag"];
            StringBuilder builder = new StringBuilder();
            builder.Append(" user_id=" + GetCurrentDiskUserID() + " and  is_deleted = 0  ");
            if (type == "all")
            {
                builder.Append(" and parent_file_id=" + parent_id);
            }
            //文档
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

            List<Model.miniyun_files> miniyun_files = GetDataByConditions(builder.ToString(), rankflag);
            string strUserName = Session["username"].ToString();
            foreach (var item in miniyun_files)
            {
                //原来  wuyanyan 2014-03-12 15:36
                // item.file_path = ZK.Common.GetFileImage.getListImageForDisk(Convert.ToInt32(ZK.Common.GetFileImage.Con_mime_path_filetype(item.mime_type)));
                int iMark = Convert.ToInt32(ZK.Common.GetFileImage.Con_mime_path_filetype(item.mime_type));
                if (item.file_type == 2)//文件夹共享
                {
                    iMark = 12;
                }
                else if (!string.IsNullOrEmpty(item.mime_type) && item.parent_file_id == 0)//文件共享
                {
                    if (RESTfulServices.AddInterface.FileAndFolder.bShareFolderId(strUserName, item))
                    {
                        iMark = 13;
                    }
                }
                item.file_path = ZK.Common.GetFileImage.getListImageForDisk(iMark);
            }
            JavaScriptSerializer jss = new JavaScriptSerializer();

            return Json(jss.Serialize(miniyun_files));
        }

        /// <summary>
        /// 根据类型获取相关类型下的资源
        /// </summary>
        /// <returns></returns>
        public ActionResult GetResContentByType_2(int? page)
        {
            //文件类型
            string type = Request.Form["type"];
            string parent_id = Request.Form["parent_id"];
            string rankflag = Request.Form["rankflag"];

            StringBuilder builder = new StringBuilder();
            builder.Append(" user_id=" + GetCurrentDiskUserID() + " and  is_deleted = 0  ");
            if (type == "all")
            {
                builder.Append(" and parent_file_id=" + parent_id);
            }
            //文档
            else if (type == "application")
            {
                builder.Append(" and parent_file_id=" + parent_id + " and mime_type like '" + type + "/ms%'");
            }
            //我分享给别人的
            else if (type == "toother")
            {
                builder.Append(" and parent_file_id=" + parent_id + " and file_type=2");
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

            // int totalcount = 0;
            //  List<Model.miniyun_files> miniyun_files = GetDataByConditions(ZK.Common.ModelSettings.DiskFileListCount, page.HasValue ? page.Value : 1, builder.ToString(), rankflag, out totalcount);
            List<Model.miniyun_files> miniyun_files = GetDataByConditions(builder.ToString(), rankflag);
            string strUserName = Session["username"].ToString();
            foreach (var item in miniyun_files)
            {
                //用来显示 image_path
                // item.file_path = ZK.Common.GetFileImage.getListImageForDisk(Convert.ToInt32(ZK.Common.GetFileImage.Con_mime_path_filetype(item.mime_type)));

                int iMark = Convert.ToInt32(ZK.Common.GetFileImage.Con_mime_path_filetype(item.mime_type));
                if (item.file_type == 2)//文件夹共享
                {
                    iMark = 12;
                }
                else if (!string.IsNullOrEmpty(item.mime_type) && item.parent_file_id == 0)
                {
                    #region 下面注销的方法为查找文件的标识
                    //string pathDel = "file_name='" + item.file_name + "(" + strUserName + ")'";
                    //List<ZK.Model.miniyun_files> lissdels = bll_miniyun_files.GetModelList(pathDel);
                    //if (lissdels.Count > 0)
                    //{
                    //    iMark = 12;
                    //}
                    #endregion
                    if (RESTfulServices.AddInterface.FileAndFolder.bShareFolderId(strUserName, item))
                    {
                        iMark = 13;
                    }
                }
                item.file_path = ZK.Common.GetFileImage.getListImageForDisk(iMark);
            }

            var data = from c in miniyun_files
                       select new
                       {
                           created_at = c.created_at.ToString("yyyy-MM-dd hh:mm"),
                           //c.event_uuid,
                           c.file_create_time,
                           c.file_name,
                           c.file_path,
                           //如果是文件夹，则不需要计算大小
                           file_size = string.IsNullOrEmpty(c.mime_type) ? "-" : CountSize(c.file_size),
                           c.file_type,
                           c.file_update_time,
                           c.id,
                           //c.is_deleted,
                           c.mime_type,
                           c.parent_file_id,
                           c.sort,
                           c.updated_at,
                           c.user_id,
                           c.version_id,
                       };
            JavaScriptSerializer jss = new JavaScriptSerializer();

            return Json(jss.Serialize(data));
        }

        /// <summary>
        /// 按照标识更新相应的字段
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdateFileInfo()
        {
            string Flag = Request.Form["Flag"];
            string FileID = Request.Form["FileID"];
            string Value = Request.Form["Value"];
            string[] fileids = FileID.Split(',');

            for (int i = 0; i < fileids.Length; i++)
            {
                if (UpdateFileModelByConditon(fileids[i], Flag, Value).ToLower() == "false")
                {
                    return Content("部分文件操作出错");
                }
            }
            return Content("true");
        }

        /// <summary>
        /// 从回收站删除相应的文件或 清空回收站
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteFileInfo()
        {

            string FileID = Request.Form["FileID"];

            if (string.IsNullOrEmpty(FileID))
            {
                return Content("false");
            }
            if (FileID == "-1")
            {
                string ids = "0";
                List<Model.miniyun_files> lists = bll_miniyun_files.GetModelList(" user_id=" + GetCurrentDiskUserID() + " and is_deleted = 1 ");
                foreach (var item in lists)
                {
                    ids = ids + "," + item.id.ToString();
                }
                return Content(bll_miniyun_files.DeleteList(ids).ToString().ToLower());
            }
            else
            {
                string[] strids = FileID.Split(',');

                for (int i = 0; i < strids.Length; i++)
                {
                    if (!bll_miniyun_files.Delete(Convert.ToInt32(strids[i])))
                    {
                        return Content("false");
                    }
                }
                return Content("true");
            }
        }

        /// <summary>
        /// 添加新文件夹
        /// </summary>
        /// <returns></returns>
        public ActionResult AddNewFoler()
        {
            string foldername = Request.Form["foldername"];
            string parent_id = Request.Form["parent_id"];
            string hashpath = "";
            return Content(AddNewFile(foldername, 0, parent_id, "", false, out hashpath));

        }


        /// <summary>
        /// 获取文件夹列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetFolderList()
        {
            string parent_id = Request.Form["parent_id"];

            StringBuilder builder = new StringBuilder(" 1=1 ");
            builder.Append(" and parent_file_id=" + parent_id);
            builder.Append(" and file_type=1 ");
            builder.Append(" and is_deleted = 0 ");
            builder.Append(" and user_id=" + GetCurrentDiskUserID());
            List<Model.miniyun_files> modellists = bll_miniyun_files.GetModelList(builder.ToString());
            return Json(modellists);
        }

        /// <summary>
        /// 移动文件或文件夹到其他文件夹
        /// </summary>
        /// <returns></returns>
        public ActionResult MoveToOtherFolder()
        {
            string file_id = Request.Form["file_id"];
            string folder_id = Request.Form["folder_id"];
            Model.miniyun_files filemodel = bll_miniyun_files.GetModel(Convert.ToInt32(file_id));
            filemodel.parent_file_id = Convert.ToInt32(folder_id);
            return Content(bll_miniyun_files.Update(filemodel).ToString().ToLower());
        }

        /// <summary>
        /// 测试用
        /// </summary>
        /// <returns></returns>
        public ActionResult test()
        {
            return View();
        }

        /// <summary>
        /// 根据文件夹id获取文件夹名
        /// </summary>
        /// <returns></returns>
        public ActionResult GetFolderName()
        {

            string folderid = Request.Form["folder_id"];
            Model.miniyun_files model = bll_miniyun_files.GetModel(int.Parse(folderid));
            if (model != null)
            {
                return Content(model.file_name);
            }
            else
                return Content("暂无获取文件夹名");
        }

        /// <summary>
        /// 通过filepath获取新的filepath和parentid
        /// </summary>
        /// <returns></returns>
        public ActionResult GetFilePathAndParentID()
        {
            string filepath = Request.Form["file_path"];
            string flag = Request.Form["flag"];
            string[] temps = filepath.Split(',');

            if (flag == "open")
            {

            }
            else if (flag == "back")
            {
                if (temps.Count() > 1)
                {
                    filepath = "";
                    for (int i = 0; i < temps.Length - 1; i++)
                    {
                        if (i == temps.Length - 2)
                        {
                            filepath += temps[i];
                        }
                        else
                        {
                            filepath += temps[i] + " > ";
                        }
                    }
                }
            }
            return Content(filepath);
        }

        /// <summary>
        /// 多文件打包
        /// </summary>
        /// <returns></returns>
        public ActionResult DownLoadFiles_M()
        {
            string ids = Request["ids"];
            string verids = Request["verids"];

            string[] idlist = ids.Split(',');
            string[] veridlist = verids.Split(',');
            if (idlist.Length != veridlist.Length)
            {
                return View();
            }
            //文件复制

            List<string> ResPaths = new List<string>();
            List<string> TarPaths = new List<string>();
            string directoryhashname = ZK.Common.CommonFunction.GetRandomHashs(20);
            string dirPath = Server.MapPath(ZK.Common.ModelSettings.TempFilesDefaultPath + "/" + directoryhashname);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            for (int i = 0; i < idlist.Length; i++)
            {
                string filename = "";
                string hashname = "";
                string path = GetFilePathByFileID(idlist[i], out filename, out hashname);

                ResPaths.Add(Server.MapPath(ZK.Common.ModelSettings.CreateFileDefaultPath + "/" + path + "/" + hashname));
                TarPaths.Add(dirPath + "\\" + filename);
            }
            ZK.Common.CommonFunction.CopyFiles(ResPaths, TarPaths);

            //打包
            List<FileInfo> filelist = new List<FileInfo>();
            foreach (var file in Directory.GetFiles(dirPath))
            {
                FileInfo fi = new FileInfo(file);
                filelist.Add(fi);
            }
            string GzipFileName = ZK.Common.ModelSettings.TempFilesDefaultPath + "/" + directoryhashname + ".zip";
            ZK.Common.FileCompression.Compress(filelist, Server.MapPath(GzipFileName), ZK.Common.ModelSettings.CompressionLevel, ZK.Common.ModelSettings.SleepTimer);

            ZK.Common.DownLoad.DownLoadFile(Response, Server.MapPath(GzipFileName), ".zip", "网盘资源下载包" + directoryhashname + ".zip");
            System.IO.File.Delete(Server.MapPath(GzipFileName));
            System.IO.Directory.Delete(dirPath, true);
            return View();
        }

        /// <summary>
        /// 单文件下载
        /// </summary>
        /// <returns></returns>
        public ActionResult DownLoadFile()
        {
            string ids = Request["ids"];
            string verids = Request["verids"];

            string[] idlist = ids.Split(',');
            try
            {
                if (idlist.Count() == 1)
                {
                    string filename = "";
                    string hashname = "";
                    string filepath = ZK.Common.ModelSettings.CreateFileDefaultPath + "/" + GetFilePathByFileID(ids, out filename, out hashname) + "/" + hashname;
                    ZK.Common.DownLoad.DownLoadFile(Response, Server.MapPath(filepath), System.IO.Path.GetExtension(filename), filename);
                }
                return View();
            }
            catch
            {
                return View();
            }


        }


        #endregion

        #region 私有方法

        /// <summary>
        /// 根据条件 获取 数据分页列表
        /// </summary>
        /// <param name="pagesize"></param>
        /// <param name="pageindex"></param>
        /// <param name="strwhere"></param>
        /// <returns></returns>
        private List<Model.miniyun_files> GetDataByConditions(int pagesize, int pageindex, string strwhere, string orderby, out int totalcount)
        {

            DataSet ds = bll_miniyun_files.GetList(pagesize, pageindex, strwhere + " ORDER BY file_type DESC ," + orderby + " desc,created_at desc ");
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
        private List<Model.miniyun_files> GetDataByConditions(string strwhere, string orderby)
        {

            List<Model.miniyun_files> list = bll_miniyun_files.GetModelList(strwhere + " ORDER BY file_type DESC , " + orderby + " desc,created_at desc ");
            return list;
        }

        /// <summary>
        /// 获取当前的用户名
        /// </summary>
        /// <returns></returns>
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
            if (Session["username"] != null)
            {
                return Session["username"].ToString();
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 获取当前的使用网盘的用户id
        /// </summary>
        /// <returns></returns>
        private string GetCurrentDiskUserID()
        {
            if (username == "")
            {
                username = GetCurrentUserName();
            }

            return new BLL.miniyun_users().GetList(" user_name='" + username + "'").Tables[0].Rows[0]["id"].ToString();
        }

        /// <summary>
        /// 根据字段名更新相应model的字段
        /// </summary>
        /// <param name="fileid">fileid</param>
        /// <param name="Key">字段标识</param>
        /// <param name="value">字段的值</param>
        /// <returns>返回结果</returns>
        private string UpdateFileModelByConditon(string fileid, string Key, string value)
        {
            int id = 0;
            if (string.IsNullOrEmpty(fileid) || !int.TryParse(fileid, out id))
            {
                return "false";
            }
            Model.miniyun_files model = bll_miniyun_files.GetModel(id);
            if (Key.ToLower() == "delete")
            {
                model.is_deleted = Convert.ToInt32(value);
            }
            else if (Key.ToLower() == "filename")
            {

                model.file_name = value == "" ? model.file_name : value;
                Encoding encod = Encoding.Default;
                byte[] bytes = Encoding.Default.GetBytes(value);
                model.file_name = Encoding.GetEncoding("utf-8").GetString(bytes);
            }

            return bll_miniyun_files.Update(model).ToString();
        }

        /// <summary>
        /// 添加新的文件或文件夹
        /// </summary>
        /// <param name="filename">文件或文件夹名</param>
        /// <param name="parent_id">父文件夹id</param>
        /// <param name="isfile">是否为文件 ture 文件 false  文件夹</param>
        /// <returns></returns>
        private string AddNewFile(string filename, int file_size, string parent_id, string mime_type, bool isfile, out string hashpath)
        {
            string CreateFileDefaultPath = "/" + GetCurrentDiskUserID();
            DateTime TimeNow = DateTime.Now;
            hashpath = "";
            //先查询出该文件或文件夹的父文件夹
            string folderpath = CreateFileDefaultPath;
            int pid = 0;
            if (parent_id != "0" && int.TryParse(parent_id, out pid))
            {
                Model.miniyun_files parentmodel = bll_miniyun_files.GetModel(Convert.ToInt32(parent_id));
                folderpath = parentmodel.file_path;

            }



            string event_uuid = System.Guid.NewGuid().ToString();

            //文件表
            Model.miniyun_files model = new Model.miniyun_files();
            model.file_name = filename;
            model.user_id = Convert.ToInt32(GetCurrentDiskUserID());
            model.file_path = folderpath + "/" + filename;
            model.parent_file_id = Convert.ToInt32(parent_id);
            model.file_size = file_size;
            //判断是否存在该文件夹
            StringBuilder builder = new StringBuilder();
            builder.Append(" 1=1 ");
            builder.Append(" and file_name='" + model.file_name + "' ");
            builder.Append(" and user_id=" + model.user_id + " ");
            builder.Append(" and file_path='" + model.file_path + "' ");
            builder.Append(" and parent_file_id=" + model.parent_file_id + " ");
            builder.Append(" and is_deleted=0 ");

            List<Model.miniyun_files> listmodel = bll_miniyun_files.GetModelList(builder.ToString());
            if (listmodel != null && listmodel.Count > 0)
            {
                if (isfile)
                {
                    return "已存在同名文件";
                }
                else
                {
                    return "已存在同名文件夹";
                }
            }
            model.file_type = 0;
            //不用创建该文件夹
            if (!isfile)
            {
                // System.IO.Directory.CreateDirectory(Request.MapPath(folderpath + "/" + filename));
                model.file_type = 1;
            }
            else
            {
                //版本表
                Model.miniyun_file_versions versionmodel = new Model.miniyun_file_versions();
                versionmodel.file_signature = ZK.Common.CommonFunction.GetRandomHashs(40);
                versionmodel.file_size = file_size;
                versionmodel.created_at = TimeNow;
                versionmodel.mime_type = mime_type;
                versionmodel.ref_count = 0;
                versionmodel.block_ids = "模拟块儿信息";
                bll_miniyun_versions.Add(versionmodel);
                hashpath = versionmodel.file_signature;

            }

            model.mime_type = mime_type;
            model.version_id = bll_miniyun_versions.GetMaxId() - 1;
            model.created_at = TimeNow;
            model.event_uuid = event_uuid;
            model.updated_at = TimeNow;
            //  model.version_id
            bool bresult1 = bll_miniyun_files.Add(model);

            //事件表
            Model.miniyun_events eventmodel = new Model.miniyun_events();
            eventmodel.user_id = Convert.ToInt32(GetCurrentDiskUserID());
            eventmodel.user_device_id = 1;
            eventmodel.action = 0;
            eventmodel.file_path = folderpath + "/" + filename;
            eventmodel.context = folderpath + "/" + filename;
            eventmodel.event_uuid = event_uuid;
            eventmodel.created_at = TimeNow;
            eventmodel.updated_at = TimeNow;

            bool bresult2 = bll_miniyun_events.Add(eventmodel);
            if (bresult1 && bresult2)
            {
                return "true";
            }
            else
            {
                return "添加出错";
            }
        }

        /// <summary>
        /// 通过hashname来获取该文件的地址 
        /// </summary>
        /// <param name="hashname"></param>
        /// <returns>返回 12/34/56/78</returns>
        private string GetFilePathByHash(string hashname)
        {
            string filepath = "";
            string Firstdir = hashname.Substring(0, 2);
            string Seconddir = hashname.Substring(2, 2);
            string Thriddir = hashname.Substring(4, 2);
            string Forthdir = hashname.Substring(6, 2);
            filepath = Firstdir + "/" + Seconddir + "/" + Thriddir + "/" + Forthdir;
            return filepath;
        }


        /// <summary>
        /// 通过fileid来获取该文件的地址和文件的文件名
        /// </summary>
        /// <param name="fileid">文件id</param>
        /// <param name="filename">文件名</param>
        /// <returns>返回 12/34/56/78</returns>
        private string GetFilePathByFileID(string fileid, out string filename, out string hashname)
        {
            string filepath = "";
            Model.miniyun_files filemodel = bll_miniyun_files.GetModel(Convert.ToInt32(fileid));
            BLL.miniyun_file_versions bll_version = new BLL.miniyun_file_versions();
            Model.miniyun_file_versions model = bll_version.GetModel(filemodel.version_id);
            hashname = model.file_signature;
            string Firstdir = hashname.Substring(0, 2);
            string Seconddir = hashname.Substring(2, 2);
            string Thriddir = hashname.Substring(4, 2);
            string Forthdir = hashname.Substring(6, 2);
            filepath = Firstdir + "/" + Seconddir + "/" + Thriddir + "/" + Forthdir;
            filename = filemodel.file_name;

            return filepath;
        }

        /// <summary>
        /// 通过versionid来获取该文件的地址和文件的hash文件名
        /// </summary>
        /// <param name="versionid"></param>
        /// <returns>返回 12/34/56/78/12345678</returns>
        private string GetFilePathWithNameByVersionID(string versionid, out string hashfilename)
        {
            string filepath = "";
            BLL.miniyun_file_versions bll_version = new BLL.miniyun_file_versions();
            Model.miniyun_file_versions model = bll_version.GetModel(Convert.ToInt32(versionid));
            string hashname = model.file_signature;
            string Firstdir = hashname.Substring(0, 2);
            string Seconddir = hashname.Substring(2, 2);
            string Thriddir = hashname.Substring(4, 2);
            string Forthdir = hashname.Substring(6, 2);
            filepath = Firstdir + "/" + Seconddir + "/" + Thriddir + "/" + Forthdir + "/" + hashname;

            hashfilename = hashname;
            return filepath;
        }

        #endregion

        #region Bojc 2013/12/13

        #region 定义
        BLL.USERS bllUser = new BLL.USERS();
        BLL.DEPARTMENTS bllDep = new BLL.DEPARTMENTS();
        BLL.DEPARTUSERS bllDepAndUser = new BLL.DEPARTUSERS();
        BLL.miniyun_users bll_miniyun_users = new BLL.miniyun_users();
        #endregion

        #region 接口
        //获取所有部门
        public string AllDepInfoJson()
        {
            String JsonString = "";
            JsonString = RESTfulServices.AddInterface.DepAndUsers.AllDepInfo();
            return JsonString;
        }

        //获取所有部门ids列表
        public string GetAllDepInfoJson()
        {
            String JsonString = "";
            JsonString = RESTfulServices.AddInterface.DepAndUsers.GetDepIds();
            return JsonString;
        }
        //获取所有人员
        public string AllUserInfoJson()
        {
            string userName = Request["username"];
            //   userName = "zyingbo";
            String JsonString = "";
            JsonString = RESTfulServices.AddInterface.DepAndUsers.AllUserInfo1(userName);
            return JsonString;
        }

        /// <summary>
        /// 根据部门id获取对应人员列表
        /// </summary>
        /// <param name="depId"> depId</param>
        /// <returns></returns>
        //public string UserOfDepInfoJson()
        //{
        //    string depId = Request["depId"];
        //    string userName = Request["username"];
        //    userName = "zyingbo";
        //    String JsonString = "";
        //    JsonString = RESTfulServices.AddInterface.DepAndUsers.UsersOfDep(depId, userName);
        //    return JsonString;
        //}
        //获取所有人员信息json数据
        public ContentResult UserOfDepInfoJson(int? depId, string username)
        {
            //通过pid查询所有子部门
            var depList = bllDep.GetModelList("PARENTDEPARTID=" + depId);
            //保存所有子部门id和本身id
            List<string> idsList = new List<string>();
            idsList.Add(depId.ToString());
            foreach (var d in depList)
            {
                idsList.Add(d.DEPARTID.ToString());
            }

            //从部门人员表中获取所有人员
            var depuserList = bllDepAndUser.GetModelList("DEPARTID in(" + string.Join(",", idsList) + ")");
            List<string> useridsList = new List<string>();
            foreach (var duser in depuserList)
            {
                useridsList.Add(duser.USERID.ToString());
            }
            var userList = bllUser.GetModelList("USERID in(" + string.Join(",", useridsList) + ")");
            var data = from c in userList
                       select new
                           {
                               id = c.USERID,
                               name = c.ACTUALNAME
                           };
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return Content(jss.Serialize(new { list = data }));
            //return Json(jss.Serialize(new { list = data },JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 按条件查询人员
        /// </summary>
        /// <param name="strName" > strName</param>
        /// <returns></returns>
        public string SearchUserInfoJson()
        {
            string strName = Request["strName"];
            String JsonString = "";
            JsonString = RESTfulServices.AddInterface.DepAndUsers.SearchUserInfo(strName);
            return JsonString;
        }
        //执行共享文件操作
        /// <summary>
        /// 共享文件或文件夹
        /// </summary>
        /// <param name="userId" > userId</param>
        /// <param name="shareIdsAndGrants" > shareIdsAndGrants</param>
        /// <param name="folderIds" > folderIds</param>
        /// <param name="flag" > flag</param>
        /// <returns></returns>
        public string ShareFolderInfoJson()
        {
            String JsonString = "";
            string userName = Request["userName"];
            string shareIdsAndGrants = Request["shareIdsAndGrants"];
            string folderIds = Request["folderIds"];
            string flag = Request["flag"];
            string userId = "";
            userId = GetUserId(userName);
            if (userId != "")
            {
                if (flag == "1")
                {
                    JsonString = RESTfulServices.AddInterface.FileAndFolder.ShareFolderAction(userId, userName, shareIdsAndGrants, folderIds);

                }

                if (flag == "0")
                {

                    JsonString = RESTfulServices.AddInterface.FileAndFolder.ShareFileAction(userId, userName, shareIdsAndGrants, folderIds);
                }
            }

            return JsonString;
        }

        /// <summary>
        ///  加载已共享过的共享文件夹
        /// </summary>
        /// <param name="folderId" > folderId</param>
        /// <returns></returns>
        public string LoadSharedUserInfo()
        {
            string folderId = Request["folderId"];
            //folderId = "1498";
            String JsonString = "";
            JsonString = RESTfulServices.AddInterface.FileAndFolder.GetShareFolderInfo(Convert.ToInt32(folderId));
            return JsonString;
        }


        #endregion

        #region 方法

        //获取用户Id
        private string GetUserId(string username)
        {
            string userId = "";
            DataSet ds = bll_miniyun_users.GetList("user_name='" + username + "'");
            if (ds.Tables[0].Rows.Count > 0)
            {
                List<Model.miniyun_users> list = new List<Model.miniyun_users>();
                list = bll_miniyun_users.DataTableToList(ds.Tables[0]);
                userId = list[0].id.ToString();
            }
            return userId;
        }

        #endregion

        #endregion


        #region 文件大小单位转换
        /// <summary>
        /// 计算文件大小函数(保留两位小数),Size为字节大小
        /// </summary>
        /// <param name="Size">初始文件大小</param>
        /// <returns></returns>
        public static string CountSize(long Size)
        {
            string m_strSize = "";
            long FactSize = 0;
            FactSize = Size;
            if (FactSize == 0)
            {
                m_strSize = "0";
            }
            else if (FactSize < 1024.00)
            {
                m_strSize = FactSize.ToString("F1") + " B";
            }
            else if (FactSize >= 1024.00 && FactSize < 1048576)
            {
                m_strSize = (FactSize / 1024.00).ToString("F1") + " KB";
            }
            else if (FactSize >= 1048576 && FactSize < 1073741824)
            {
                m_strSize = (FactSize / 1024.00 / 1024.00).ToString("F1") + " M";
            }
            else if (FactSize >= 1073741824)
            {
                m_strSize = (FactSize / 1024.00 / 1024.00 / 1024.00).ToString("F1") + " G";
            }
            return m_strSize;
        }
        #endregion
        #region By Ao

















        #endregion

        #region 为了不影响之前的共享操作单独写共享功能(过渡) 需要传入file_id,flietype


        /// <summary>
        /// 共享文件和文件夹的操作.flag:1为共享文件夹,0为共享文件
        /// 共享文件夹能够执行取消共享的操作
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="shareIdsAndGrants"></param>
        /// <param name="folderIds"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public string NShareFile(string userName, string shareIdsAndGrants, string folderIds, string flag)
        {
            var loginuser = GetLoginUser();
            String JsonString = "";
            //获取当前登录用户id
            string userId = loginuser.uid;
            if (userId != "")
            {
                if (flag == "1")
                {
                    JsonString = RESTfulServices.AddInterface.FileAndFolder.ShareFolderAction(userId, userName, shareIdsAndGrants, folderIds);

                }

                if (flag == "0")
                {

                    JsonString = RESTfulServices.AddInterface.FileAndFolder.ShareFileAction(userId, userName, shareIdsAndGrants, folderIds);
                }
            }

            return JsonString;
        }
        #endregion

        #region 改变用户的id
        //转换成MySql中的UserId
        private int ChangeUserId(int userId)
        {
            int netUserId = userId;
            string userName = "";
            ZK.Model.USERS user = bllUser.GetModelList("userid=" + Convert.ToInt32(userId)).FirstOrDefault();
            if (user != null)
            {
                userName = user.USERNAME;
                miniyun_users miniyunUser = bll_miniyun_users.GetModelList("user_name ='" + userName + "'").FirstOrDefault();
                if (miniyunUser != null)
                {
                    netUserId = miniyunUser.id;
                }
                return netUserId;
            }
            return 0;//未找到return 0
        }
        #endregion

        #region 整理后的方法
        public ActionResult mydoc()
        {
            var loginuser = GetLoginUser();
            ViewData["uid"] = loginuser.uid;
            return View();
        }
        public ActionResult publicdoc()
        {
            var loginuser = GetLoginUser();
            ViewData["uid"] = loginuser.uid;
            //公共资料访问权限
            ViewData["Role"] = "";
            ZK_RoleList rolelist = bll_RoleList.GetModelList("roleDesc ='publicText'").FirstOrDefault();
            if (rolelist != null)
            {
                var roletouserid = bll_RoleToUser.GetModelList("roleID=" + rolelist.roleID + " and userID=" + Session["uid"].ToString()).FirstOrDefault();
                ViewData["Role"] = roletouserid == null ? "" : "1";
            }
            return View();
        }
        //我要收资料文件夹页面
        public ActionResult wyszl_folder()
        {
            var loginuser = GetLoginUser();
            ViewData["uid"] = loginuser.uid;
            return View();
        }
        //我要收资料文件列表页面
        public ActionResult wyszl_files(string pid)
        {
            var loginuser = GetLoginUser();
            ViewData["uid"] = loginuser.uid;
            ViewData["pid"] = pid;
            return View();
        }
        //新增收集夹删除功能 2014-11-1 by ao
        [HttpPost]
        public ActionResult wyjzl_delcfolder(string idStr)
        {
            if (string.IsNullOrEmpty(idStr))
            {
                return Content("error");
            }
            if (bll_file_collection.DeleteList(idStr))
            {
                //删除收集夹中的上交文件
                var cfileList = bll_file_collection.GetModelList("parent_file_id in(" + idStr + ")");
                if (cfileList.Count > 0)
                {
                    List<string> fidList = new List<string>();
                    foreach (var cf in cfileList)
                    {
                        fidList.Add(cf.id.ToString());
                    }
                    bll_file_collection.DeleteList(string.Join(",", fidList));
                }

                return Content("ok");
            }
            return Content("error");
        }
        //我要交资料文件夹页面
        public ActionResult wyjzl_folder()
        {
            var loginuser = GetLoginUser();
            ViewData["uid"] = loginuser.uid;
            return View();
        }
        //我要交资料文件列表页面
        public ActionResult wyjzl_files(string pid)
        {
            var loginuser = GetLoginUser();
            ViewData["uid"] = loginuser.uid;
            ViewData["pid"] = pid;
            return View();
        }

        public ActionResult recycle()
        {
            return View();
        }

        //通过pid 和 文件类型获取文件json数据
        public JsonResult GetFileDataList(string actions, int? pid, string type, string orderby, string uid, string filter, string keyword)
        {
            var loginuser = GetLoginUser();
            string userid = loginuser.uid;
            if (uid != null)
            {
                userid = uid;//如果传入uid说明是获取直接共享文件产生的共享文件夹下的文件
            }
            string where = "";
            switch (actions)
            {
                case "all":
                    //我共享给别人的type=2 别人共享给我的type=3
                    where = " user_id=" + userid + " and parent_file_id=" + pid + " and is_deleted=0";
                    if (!string.IsNullOrEmpty(filter))
                    {
                        where += " and file_type=" + filter;
                    }
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        where += " and file_name like '%" + keyword + "%'";
                    }
                    break;
                case "pic":
                    where = " user_id=" + userid + " and parent_file_id=" + pid + " and mime_type like '" + type + "image/%'" + " and is_deleted=0";
                    break;
                case "deleted":
                    where = " user_id=" + userid + " and is_deleted=1";
                    break;
                case "document":
                    where = " user_id=" + userid + " and parent_file_id=" + pid + " and mime_type like '" + type + "%/ms%'" + " and is_deleted=0";
                    break;
                case "collection":
                    return CollectionFolderList();
                default:
                    break;
            }
            var fileList = bll_miniyun_files.GetModelList(where);
            //先列文件夹，然后按照上传时间倒序排列
            var data = from c in fileList
                       orderby c.file_type descending, c.updated_at descending
                       select new
                       {
                           id = c.file_type == 3 ? GetShareFolderPid(c.file_path) == 0 ? c.id : GetShareFolderPid(c.file_path) : c.id,//对于两种不同的共享文件夹要区分
                           pid = c.parent_file_id,
                           ft = c.file_type,   //文件类型(前台会根据文件类型有不同的操作)
                           icon = GetFileIcon(c.file_type, c.mime_type),      //图标类型
                           fn = c.file_name,   //文件名
                           n = (c.file_type == 1 || c.file_type == 2 || c.file_type == 3) ? c.file_name : Path.GetFileNameWithoutExtension(c.file_name),//文件去掉扩展名的名称(用于改名时使用)
                           fz = (c.file_type == 1 || c.file_type == 2 || c.file_type == 3) ? "—" : CountSize(c.file_size),   //文件大小
                           ut = c.updated_at.ToString("yyyy-MM-dd HH:mm:ss"), //修改时间
                           uid = c.file_type == 3 ? GetShareFolderUid(c.file_path) : 0//对于直接共享文件夹得到的共享文件夹里面的文件不属于当前用户，所有把真正文件夹的用户返回
                       };

            return Json(data);
        }
        //获得直接共享文件夹的真实用户
        public int GetShareFolderUid(string filepath)
        {
            var file_metas = bll_file_metas.GetModelList("file_path='" + filepath + "'").FirstOrDefault();
            if (file_metas != null)
            {
                string meta_value = file_metas.meta_value;
                var m = Regex.Match(meta_value, @"a:\d+:{s:\d+:""master"";i:(\d+);.+""path"";s:\d+:""(.+)"";");
                string masteruid = m.Groups[1].Value;
                return int.Parse(masteruid);
            }
            //如果没找到说明此共享文件夹是共享文件而产生。
            return 0;
        }
        //得到共享文件夹真实Pid
        public int GetShareFolderPid(string filepath)
        {
            var file_metas = bll_file_metas.GetModelList("file_path='" + filepath + "'").FirstOrDefault();
            if (file_metas != null)
            {
                string meta_value = file_metas.meta_value;
                var m = Regex.Match(meta_value, @"a:\d+:{s:\d+:""master"";i:(\d+);.+""path"";s:\d+:""(.+)"";");
                string masteruid = m.Groups[1].Value;
                string path = m.Groups[2].Value;
                //通过path 和 uid得到pid
                var file = bll_miniyun_files.GetModelList("user_id=" + masteruid + " and file_path='" + path + "'").FirstOrDefault();
                return file.id;
            }
            //如果没找到说明此共享文件夹是共享文件而产生。
            return 0;
        }

        //通过文件类型和文件头得到文件的图标
        public string GetFileIcon(int filetype, string mime_type)
        {
            string icon = "unknow";
            if (filetype == 0)
            {
                switch (mime_type)
                {
                    case "image/jpeg":     //图片
                    case "image/png":
                    case "image/gif":
                    case "image/bmp":
                        icon = "pic";
                        break;
                    case "application/msword": //word
                        icon = "word";
                        break;
                    case "application/msexcel":  //excel
                        icon = "excel";
                        break;
                    case "application/pdf":    //pdf
                        icon = "pdf";
                        break;
                    case "text/plain":    //text
                        icon = "text";
                        break;
                    case "application/zip":    //压缩文件
                    case "application/x-rar-compressed":
                        icon = "rar";
                        break;
                    case "application/x-msdownload":
                        icon = "exe";
                        break;
                }
            }
            else if (filetype == 1)
            {
                icon = "folder";
            }
            else if (filetype == 2)
            {
                icon = "share"; //共享给别人的
            }
            else if (filetype == 3)
            {
                icon = "share"; //别人共享过来的
            }
            return icon;
        }
        //上传文件的操作 （根路径在web.config中配置uploaddir 节点）
        [HttpPost]
        public ContentResult UploadFile(int pid, HttpPostedFileBase Filedata, string uid, string uptype)
        {
            //uploadify 不能把客户端的Cookie带回，所以导致登录Session失效
            // var loginuser = GetLoginUser();



            var uploadfile = Filedata;

            string md5 = Common.CommonFunction.StreamToMd5(uploadfile.InputStream);
            //对文件进行分部式存储（通过md5值得到存储路径）
            string filepath = Path.Combine(ConfigHelper.GetConfigString("uploaddir"), md5.Substring(0, 2));
            //相对路径（不包含文件名）
            filepath = Path.Combine(filepath, md5.Substring(2, 2));
            filepath = Path.Combine(filepath, md5.Substring(4, 2));
            filepath = Path.Combine(filepath, md5.Substring(6, 2));
            //物理保存路径(不包含文件名)
            string savepath = Server.MapPath(filepath);
            string filename = uploadfile.FileName;
            string ext = Path.GetExtension(filename).ToString().Substring(1);
            string mime_type = Common.CommonFunction.ExtTomimetype(ext);
            int file_size = uploadfile.ContentLength;
            //0.通过文件md5值在版本表中查找文件是否存在
            var exit = bll_miniyun_versions.GetModelList("file_signature='" + md5 + "'").FirstOrDefault();
            if (exit != null) //如果存在则直接写入用户文件表
            {
                if (uptype == "upcfile")//如果上传上交资料
                {
                    //3.写用户文件表
                    AddCUserFile(uid, exit.id, pid, filename, Path.Combine(filepath, md5), file_size, mime_type);
                }
                else if (uptype == "pubdoc")
                {
                    //公共文件上传
                    AddPUserFile(uid, exit.id, pid, filename, Path.Combine(filepath, md5), file_size, mime_type);
                }
                else if (uptype == "wyszl")
                {
                    //我要收资料，写入用户文件表
                    AddSUserFile(uid, exit.id, pid, filename, Path.Combine(filepath, md5), file_size, mime_type);
                }
                else
                {
                    //3.写用户文件表
                    AddUserFile(uid, exit.id, pid, filename, Path.Combine(filepath, md5), file_size, mime_type);
                }
                return Content("ok");
            }
            //

            miniyun_file_versions miniyun_file_versions_model = new Model.miniyun_file_versions()
            {
                block_ids = "模拟块儿信息",
                created_at = DateTime.Now,
                file_signature = md5,
                file_size = file_size,
                mime_type = mime_type,
                ref_count = 0,
                updated_at = DateTime.Now
            };
            //1.先在服务器上保存文件
            try
            {

                if (!System.IO.File.Exists(savepath))
                {
                    Directory.CreateDirectory(savepath);
                }
                uploadfile.SaveAs(Path.Combine(savepath, md5));

            }
            catch (Exception)
            {
                throw;
            }
            //2.将上传的文件写入文件版本表
            bll_miniyun_versions.Add(miniyun_file_versions_model);//add方法不能获取新增数据主键(Id),这是需要改进的地方
            //通过md5去查询获取新增数据的id
            int versionid = bll_miniyun_versions.GetModelList("file_signature='" + md5 + "'").FirstOrDefault().id;
            if (uptype == "upcfile")//如果上传上交资料
            {
                //3.写用户文件表
                AddCUserFile(uid, versionid, pid, filename, Path.Combine(filepath, md5), file_size, mime_type);
            }
            else if (uptype == "pubdoc")
            {
                //公共文件上传
                AddPUserFile(uid, versionid, pid, filename, Path.Combine(filepath, md5), file_size, mime_type);
            }
            else if (uptype == "wyszl")
            {
                //我要收资料，写入用户文件表
                AddSUserFile(uid, versionid, pid, filename, Path.Combine(filepath, md5), file_size, mime_type);
            }
            else
            {
                //3.写用户文件表
                AddUserFile(uid, versionid, pid, filename, Path.Combine(filepath, md5), file_size, mime_type);
            }

            return Content("");

        }

        //将文件信息写入用户文件表
        public void AddUserFile(string uid, int versionid, int pid, string filename, string savepath, int file_size, string mime_type)
        {
            //目前对同层次，同名文件采用替换覆盖的处理方式
            //通过文件夹的id得到用户id
            if (pid != 0)
            {
                var folder = bll_miniyun_files.GetModel(pid);
                if (folder != null)
                {
                    uid = folder.user_id.ToString();
                }
            }


            //1.查询同层次下是否有同名文件
            miniyun_files exits = bll_miniyun_files.GetModelList("user_id=" + uid + " and parent_file_id=" + pid + " and file_name='" + filename + "'").FirstOrDefault();
            if (exits != null)//存在则替换
            {
                exits.version_id = versionid;
                exits.file_size = file_size;
                exits.updated_at = DateTime.Now;
                exits.is_deleted = 0;
                bll_miniyun_files.Update(exits);
            }
            else
            {
                var userfile = new miniyun_files()
                {
                    created_at = DateTime.Now,
                    event_uuid = Guid.NewGuid().ToString(),
                    file_create_time = 0,
                    file_name = filename,
                    file_path = savepath,
                    file_size = file_size,
                    file_type = 0,//上传的都是文件
                    file_update_time = 0,
                    is_deleted = 0,
                    mime_type = mime_type,
                    parent_file_id = pid,
                    sort = 0,
                    updated_at = DateTime.Now,
                    user_id = Convert.ToInt32(uid),
                    version_id = versionid

                };
                bll_miniyun_files.Add(userfile);
            }

        }
        //将文件信息写入用户文件表
        public void AddCUserFile(string uid, int versionid, int pid, string filename, string savepath, int file_size, string mime_type)
        {
            //上交资料夹中，同一用户只能保留一个文件，再次上传时为替换。

            //先删除
            var exits = bll_file_collection.GetModelList("user_id=" + uid + " and parent_file_id=" + pid).FirstOrDefault();
            if (exits != null)
            {
                bll_file_collection.Delete(exits.id);
            }

            //按照用户名-文件夹名-时间 格式
            string rfilename = string.Format("{0}-{1}-{2}{3}", GetUserNameByUid(Convert.ToInt32(uid)), bll_file_collection.GetModel(pid).file_name, DateTime.Now.ToString("yyyyMMdd"), Path.GetExtension(filename));
            bll_file_collection.Add(new Model.file_collection
             {
                 create_at = DateTime.Now,
                 cuserids = " ",
                 cuseridss = " ",
                 fie_size = file_size,
                 file_create_time = 0,
                 file_name = rfilename,
                 file_path = savepath,
                 file_type = 0,
                 file_update_time = 0,
                 is_deleted = 0,
                 mime_type = mime_type,
                 parent_file_id = pid,
                 updated_at = DateTime.Now,
                 sort = 0,
                 user_id = Convert.ToInt32(uid),
                 version_id = versionid
             });



        }

        //上传公共资料文件
        public void AddPUserFile(string uid, int versionid, int pid, string filename, string savepath, int file_size, string mime_type)
        {
            //目前对同层次，同名文件采用替换覆盖的处理方式
            //1.查询同层次下是否有同名文件

            var exits = bll_public_files.GetModelList("user_id=" + uid + " and parent_file_id=" + pid + " and file_name='" + filename + "'").FirstOrDefault();
            if (exits != null)//存在则替换
            {
                exits.version_id = versionid;
                exits.fie_size = file_size;
                exits.updated_at = DateTime.Now;
                bll_public_files.Update(exits);
            }
            else
            {
                var b = bll_public_files.Add(new Model.public_files
                {
                    create_at = DateTime.Now,
                    cuserids = " ",
                    cuseridss = " ",
                    fie_size = file_size,
                    file_create_time = 0,
                    file_name = filename,
                    file_path = savepath,
                    file_type = 0,
                    file_update_time = 0,
                    is_deleted = 0,
                    mime_type = mime_type,
                    parent_file_id = pid,
                    updated_at = DateTime.Now,
                    sort = 0,
                    user_id = Convert.ToInt32(uid),
                    version_id = versionid

                });
            }

        }
        //我要收资料，写入用户文件表(files)部分
        public void AddSUserFile(string uid, int versionid, int pid, string filename, string savepath, int file_size, string mime_type)
        {

            //目前对同层次，同名文件采用替换覆盖的处理方式
            //1.查询同层次下是否有同名文件
            var exits = bll_file_collection.GetModelList("user_id=" + uid + " and parent_file_id=" + pid + " and file_name='" + filename + "'").FirstOrDefault();
            if (exits != null)//存在则替换
            {
                exits.version_id = versionid;
                exits.fie_size = file_size;
                exits.updated_at = DateTime.Now;
                bll_file_collection.Update(exits);
            }
            else
            {
                var b = bll_file_collection.Add(new Model.file_collection
                {
                    create_at = DateTime.Now,
                    cuserids = " ",
                    cuseridss = " ",
                    fie_size = file_size,
                    file_create_time = 0,
                    file_name = filename,
                    file_path = savepath,
                    file_type = 0,
                    file_update_time = 0,
                    is_deleted = 0,
                    mime_type = mime_type,
                    parent_file_id = pid,
                    updated_at = DateTime.Now,
                    sort = 0,
                    user_id = Convert.ToInt32(uid),
                    version_id = versionid

                });
            }
        }
        //新建文件夹的操作
        public JsonResult DoNewFolder(string foldername, int pid)
        {
            //文件夹用 uid 和path做联合主键判断重复，用id进行数据唯一性操作。
            var loginuser = GetLoginUser();
            string uid = loginuser.uid;

            var pfolder = bll_miniyun_files.GetModel(pid);
            string filepath = pfolder == null ? string.Format("/{0}/{1}", uid, foldername) : string.Format("{0}/{1}", pfolder.file_path, foldername);
            miniyun_files exitsmodel = bll_miniyun_files.GetModelList("file_name='" + foldername + "' and parent_file_id=" + pid + " and user_id=" + uid + " and is_deleted=0").FirstOrDefault();
            if (exitsmodel != null)
            {
                return Json(new { error = 3, msg = "文件夹已存在！" });
            }
            bool b = bll_miniyun_files.Add(new Model.miniyun_files
            {
                created_at = DateTime.Now,

                event_uuid = Guid.NewGuid().ToString(),
                file_size = 0,
                file_create_time = 0,
                file_name = foldername,
                file_path = filepath,
                file_type = 1,
                file_update_time = 0,
                is_deleted = 0,
                mime_type = "folder",
                parent_file_id = pid,
                updated_at = DateTime.Now,
                sort = 0,
                user_id = Convert.ToInt32(loginuser.uid),
                version_id = 0
            });
            if (b)
            {
                return Json(new { error = 0, msg = "新建文件夹成功！" });
            }
            else
            {
                return Json(new { error = 1, msg = "新建文件夹失败！" });
            }

        }
        //单个文件下载操作 type标志不同栏目下载(我的资料:mydoc,我要交资料:jzl,我要收资料:szl,公共资料:pubdoc)
        public ActionResult DownLoad(int? id, string type)
        {
            dynamic model = null;
            if (type == "mydoc")
            {
                model = bll_miniyun_files.GetModelList("id=" + id).FirstOrDefault();
            }
            else if (type == "jzl" || type == "szl")
            {
                model = bll_file_collection.GetModelList("id=" + id).FirstOrDefault();
            }
            else if (type == "pubdoc")
            {

            }

            int version_id = model.version_id;
            var file_version = bll_miniyun_versions.GetModel(version_id);
            string s = file_version.file_signature;
            string shitp = s.Substring(0, 2) + @"\";
            shitp += s.Substring(2, 2) + @"\";
            shitp += s.Substring(4, 2) + @"\";
            shitp += s.Substring(6, 2) + @"\" + s;
            string filepath = Path.Combine(ConfigHelper.GetConfigString("uploaddir"), shitp);
            string filename = model.file_name;
            filepath = Server.MapPath(filepath);
            if (!System.IO.File.Exists(filepath))
            {
                throw new Exception("对不起您下载的文件不存在，或被删除！");
            }
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
            Response.TransmitFile(filepath);
            return Content("");
        }

        /// <summary>
        /// 多文件打包下载 addby ao 2014-11-11
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type">我的资料:mydoc,我要交资料:jzl,我要收资料:szl,公共资料:pubdoc</param>
        /// <returns></returns>
        public ActionResult DownLoad_M(string ids, string type)
        {
            var idList = ids.Split(',');
            string tempPath = ProcessFolder(Server.MapPath(@"\") + @"\temp\" + Guid.NewGuid().ToString("N"));
            foreach (var id in idList)
            {
                CopyFile(id, tempPath);
            }
            string zipPath=CompressFile(tempPath);
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment;filename=网盘打包下载"+DateTime.Now.ToString("yyyyMMddHHmmss")+".zip");
            Response.TransmitFile(zipPath);
            return Content("");
        }

        //修改文件名的操作.
        public ContentResult ReName(int id, string filename)
        {
            miniyun_files model = bll_miniyun_files.GetModelList(" id=" + id.ToString()).FirstOrDefault();
            if (model != null)
            {
                model.updated_at = DateTime.Now;
                model.file_name = filename + Path.GetExtension(model.file_name);
                if (bll_miniyun_files.Update(model))
                {
                    return Content("ok");
                }

            }
            return Content("error");
        }

        //更新文件删除状态.
        //value:0代表未删除,1代表删除 //type标志更新数据的栏目默认为我的资料 type==2 我共享给别人的
        //wyszl:我要收资料 pubdoc:公共资料库
        public ContentResult UpdateFile(string ids, int value, string type)
        {
            string[] idss = ids.Split(',');
            try
            {
                if (type == "wyszl")
                {
                    foreach (var id in idss)
                    {
                        var model = bll_file_collection.GetModel(Convert.ToInt32(id));
                        //判断恢复位置是否有同名的文件夹
                        //miniyun_files exitsmodel = bll_miniyun_files.GetModelList("file_name='" + foldername + "' and parent_file_id=" + pid + " and user_id=" + uid + " and is_deleted=0").FirstOrDefault();
                        model.updated_at = DateTime.Now;
                        model.is_deleted = value;
                        bll_file_collection.Update(model);
                    }
                }
                else
                {
                    foreach (var id in idss)
                    {
                        var model = bll_miniyun_files.GetModel(Convert.ToInt32(id));
                        model.updated_at = DateTime.Now;
                        model.is_deleted = value;
                        bll_miniyun_files.Update(model);
                    }
                }

                return Content("ok");
            }
            catch (Exception)
            {

                return Content("error");
            }

        }


        [HttpGet]
        public ActionResult Move(int pid)
        {
            ViewData["pid"] = pid;
            return View();
        }
        [HttpPost]
        public ContentResult Move(string ids, int pid)
        {
            var fileList = bll_miniyun_files.GetModelList(" id in(" + ids + ")");
            //设置所有文件的pid为移动后文件夹的id
            foreach (var item in fileList)
            {
                item.parent_file_id = pid;
                bll_miniyun_files.Update(item);
            }
            return Content("ok");
        }
        //获取文件夹的json数据
        public JsonResult GetFolderJson(string folderids)//选中的文件夹不列出
        {
            var loginuser = GetLoginUser();
            string uid = loginuser.uid;
            //查询出用户所有未删除文件夹
            var allfolder = bll_miniyun_files.GetModelList("is_deleted=0 and user_id=" + uid + " and file_type in(1,2,3)");
            //查找是否具有子节点
            var temp = from c in allfolder
                       orderby c.created_at descending
                       select new
                       {
                           id = c.id,
                           pId = c.parent_file_id,
                           name = c.file_name
                       };
            var result = temp.ToList();
            //添加根目录数据
            result.Insert(0, new { id = 0, pId = -1, name = "全部文件" });
            return Json(result);
        }

        //文件共享页面
        //DiskN/Share?dddd=ddd
        public ActionResult Share(string file_id)
        {
            var loginuser = GetLoginUser();
            ViewData["fileid"] = file_id;
            ViewData["username"] = loginuser.username;
            return View();
        }
        /// <summary>
        /// 共享文件页面
        /// </summary>
        /// <param name="file_id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult NShare(string file_id)
        {
            var loginuser = GetLoginUser();
            ViewData["fileid"] = file_id;
            ViewData["username"] = loginuser.username;
            return View();
        }
        /// <summary>
        /// 取消共享操作
        /// </summary>
        /// <param name="file_id"></param>
        /// <returns></returns>
        [HttpPost]
        public ContentResult UnShare(int file_id)
        {
            var loginuser = GetLoginUser();
            // 取消共享文件夹：1.将该文件状态变为普通文件夹 2.删除共享文件夹时在对应用户中生成的文件夹
            var folder = bll_miniyun_files.GetModel(file_id);
            if (folder != null && folder.file_type == 2)
            {
                folder.file_type = 1;
                if (bll_miniyun_files.Update(folder))
                {
                    //删除共享产生的共享文件夹 type==3 name格式：新建文件夹(qinao)
                    string foldername = folder.file_name + "(" + loginuser.username + ")";
                    var shareFolderList = bll_miniyun_files.GetModelList("file_type=3 and file_name='" + foldername + "'");
                    List<string> idList = new List<string>();
                    if (shareFolderList.Count > 0)
                    {
                        foreach (var fd in shareFolderList)
                        {
                            idList.Add(fd.id.ToString());
                        }
                        bll_miniyun_files.DeleteList(string.Join(",", idList));
                    }
                    return Content("ok");
                }
            }

            return Content("error");
        }
        //得到删除的数据
        public JsonResult DeletedFileList()
        {
            var loginuser = GetLoginUser();
            string userid = loginuser.uid;
            string where = " user_id=" + userid + " and is_deleted=1"; ;

            var fileList = bll_miniyun_files.GetModelList(where);
            var data = from c in fileList
                       orderby c.file_type descending, c.updated_at descending //默认按照文件类型排序
                       select new
                       {
                           c.id,
                           pid = c.parent_file_id,
                           ft = c.file_type,   //类型
                           icon = GetFileIcon(c.file_type, c.mime_type),      //图标类型
                           fn = c.file_name,   //文件名
                           fz = c.mime_type == "folder" ? "—" : CountSize(c.file_size),   //文件大小
                           ut = c.updated_at.ToString("yyyy-MM-dd HH:mm:ss") //删除时间
                       };

            return Json(data);
        }

        //清空回收站的操作
        public ActionResult ClearALLFiles()
        {
            var loginuser = GetLoginUser();
            string uid = loginuser.uid;
            var filesList = bll_miniyun_files.GetModelList(" is_deleted =1 and user_id=" + uid);
            try
            {
                foreach (var item in filesList)
                {
                    bll_miniyun_files.Delete(item.id);
                }
                return Content("ok");
            }
            catch (Exception)
            {

                return Content("error");
            }
        }
        //新建收集夹
        public JsonResult DoNewCFolder(string foldername)
        {
            var loginuser = GetLoginUser();
            string uid = loginuser.uid;
            //1.查找是否具有同名的收集夹,因为是一级所以不需要判断层级(pid)
            string path = string.Format("/{0}/{1}", uid, foldername);
            var exitsmodel = bll_file_collection.GetModelList("user_id=" + uid + " and file_name='" + foldername + "'").FirstOrDefault();
            if (exitsmodel != null)
            {
                return Json(new { error = 3, msg = "收集夹已存在！" });
            }
            bool b = bll_file_collection.Add(new Model.file_collection
            {
                create_at = DateTime.Now,
                cuserids = "",
                cuseridss = "",
                fie_size = 0,
                file_create_time = 0,
                file_name = foldername,
                file_path = path,
                file_type = 1,
                file_update_time = 0,
                is_deleted = 0,
                mime_type = "cfolder",
                parent_file_id = 0,
                updated_at = DateTime.Now,
                sort = 0,
                user_id = Convert.ToInt32(uid),
                version_id = 1
            });
            if (b)
            {
                return Json(new { error = 0, msg = "新建收集夹成功！" });
            }
            else
            {
                return Json(new { error = 1, msg = "新建收集夹失败！" });
            }

        }
        //列举收集夹
        //file_collection file_type 0文件 1收集夹
        public JsonResult CollectionFolderList()
        {
            var loginuser = GetLoginUser();
            string uid = loginuser.uid;
            var file_collection_list = bll_file_collection.GetModelList("file_type=1 and user_id=" + uid);
            //默认按照创建时间排序
            var temp = from c in file_collection_list
                       orderby c.create_at descending
                       select new
                       {
                           c.id,
                           icon = GetFileIcon(c.file_type, c.mime_type),      //图标类型
                           fn = c.file_name,   //文件名
                           qx_state = string.IsNullOrEmpty(c.cuseridss) ? "未设置" : "已设置",//权限设置情况
                           c_state = c.is_end == 0 ? "收集中" : "结束收集",
                           ct = c.create_at.ToString("yyyy-MM-dd HH:mm:ss") //修改时间
                       };
            return Json(temp);
        }
        //获取收集夹中的文件数据
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pid">收集夹id</param>
        /// <returns></returns>
        public JsonResult CollectionFileList(string pid)
        {
            var loginuser = GetLoginUser();
            string uid = loginuser.uid;
            var file_collection_list = bll_file_collection.GetModelList("file_type=0 and is_deleted=0 and parent_file_id=" + pid);
            string foldername = bll_file_collection.GetModel(Convert.ToInt32(pid)).file_name;
            //默认按照创建时间排序
            var temp = from c in file_collection_list
                       orderby c.create_at descending
                       select new
                       {
                           c.id,
                           pid = c.parent_file_id,
                           ft = c.file_type,   //类型
                           icon = GetFileIcon(c.file_type, c.mime_type),      //图标类型
                           // fn = c.file_name+"("+GetUserNameByUid(c.user_id)+")",   //文件名
                           //fn = GetUserNameByUid(c.user_id) + "-" + foldername + "-" + c.create_at.ToString("yyyyMMdd") + Path.GetExtension(c.file_name),   //文件名
                           fn = c.file_name,
                           fz = CountSize(c.fie_size),   //文件大小
                           ct = c.create_at.ToString("yyyy-MM-dd HH:mm:ss") //修改时间
                       };
            return Json(temp);
        }
        //通过用户id得到用户名
        string GetUserNameByUid(int uid)
        {
            miniyun_users user = bll_miniyun_users.GetModel(uid);
            if (user != null)
            {
                return user.user_name;
            }
            return "未知用户";
        }
        //结束收集
        public ActionResult SetCollectionState(int folderid, int value)
        {
            var model = bll_file_collection.GetModel(folderid);
            if (model != null)
            {
                model.is_end = value;
                if (bll_file_collection.Update(model))
                {
                    return Content("ok");
                }
            }
            return Content("error");
        }
        //列举上交资料夹
        public JsonResult UpCollectionFolderList()
        {
            var loginuser = GetLoginUser();
            string uid = loginuser.uid;
            //不列出自己的收集夹,是因为前台可以给自己设置权限(╮(╯▽╰)╭)
            var file_collection_list = bll_file_collection.GetModelList("user_id<>" + uid + "  and is_end =0 and file_type=1 and cuseridss like '%[" + uid + "]%'");
            //为上交资料的文件夹排最前面，然后按照时间倒序排列
            var temp = (from c in file_collection_list
                        select new
                        {
                            c.id,
                            uped = FolderHasUserFile(uid, c.id),
                            pid = c.parent_file_id,
                            ft = c.file_type,   //类型
                            icon = FolderHasUserFile(uid, c.id) ? "folder" : "folder_msg",      //图标类型
                            fn = c.file_name + "(" + GetUserNameByUid(c.user_id) + ")",   //文件夹名(该文件夹所属用户名)
                            fz = "—",   //文件大小
                            ct = c.create_at.ToString("yyyy-MM-dd HH:mm:ss") //修改时间
                        }).OrderBy(m => m.uped).ThenByDescending(m => m.ct);
            return Json(temp);
        }
        //列举上交资料文件（显示当前登录用户的文件和文件所有者的文件[发起收集的人]）
        public JsonResult UpCollectionFileList(int pid)
        {
            var loginuser = GetLoginUser();
            string uid = loginuser.uid;
            //获取文件所有者的id
            int folderownerid = bll_file_collection.GetModel(pid).user_id;
            var file_collection_list = bll_file_collection.GetModelList("file_type=0 and is_deleted=0 and parent_file_id=" + pid + " and user_id in(" + folderownerid + "," + uid + ")");
            //默认按照创建时间排序
            var temp = from c in file_collection_list
                       orderby c.create_at descending
                       select new
                       {
                           c.id,
                           pid = c.parent_file_id,
                           ft = c.file_type,   //类型
                           icon = GetFileIcon(c.file_type, c.mime_type),      //图标类型
                           fn = c.file_name,   //文件夹名(该文件夹所属用户名)
                           fz = CountSize(c.fie_size),   //文件大小
                           ut = c.create_at.ToString("yyyy-MM-dd HH:mm:ss") //修改时间
                       };
            return Json(temp);
        }

        //权限设置页面
        [HttpGet]
        public ActionResult SetUsers(string fids)
        {
            var loginuser = GetLoginUser();
            ViewData["fids"] = fids;
            ViewData["username"] = loginuser.username;
            return View();
        }

        [HttpPost]
        public ContentResult SetUsers(string fids, string uids)
        {

            var collection_files_list = bll_file_collection.GetModelList("id in(" + fids + ")");
            var idList = new List<string>();
            var mysqlidList = new List<string>();
            string[] temp_uids = uids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var id in temp_uids)
            {
                string cid = ChangeUserId(Convert.ToInt32(id)).ToString();
                idList.Add("[" + cid + "]");
                mysqlidList.Add(cid);
            }
            try
            {
                foreach (var item in collection_files_list)
                {
                    item.cuseridss = string.Join(",", idList);
                    item.cuserids = string.Join(",", mysqlidList);
                    bll_file_collection.Update(item);
                }
                return Content("ok");
            }
            catch (Exception ex)
            {
                return Content("error");
                throw;
            }

        }

        //初始化权限的函数
        public ActionResult GetDefaulAccessData()
        {
            return Content("default_1_0_0_0_1_1_0_0_0@1");
        }
        #endregion
        #region 分享操作
        //分享对话框页面
        public ActionResult FxDialog(string ids, string password)
        {
            string[] idArray = ids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            List<string> urls = new List<string>();
            foreach (var id in idArray)
            {
                string sharekey = Guid.NewGuid().ToString().Substring(0, 8);
                miniyun_share_files model = new miniyun_share_files()
                {
                    created_at = DateTime.Now,
                    down_count = 0,
                    expires = 0,
                    file_id = Convert.ToInt32(id),
                    password = string.IsNullOrEmpty(password) ? "-1" : ZK.Common.StringPlus.StringToMD5(password),
                    share_key = sharekey,
                    updated_at = DateTime.Now
                };
                bll_miniyun_share_files.Add(model);
                var reg = Regex.Match(HttpContext.Request.Url.ToString(), @"(http://.+?)/.+");
                string root = reg.Groups[1].Value;
                urls.Add(string.Format("{0}/DiskN/Files/{1}", root, sharekey));
            }
            //bll_miniyun_share_files
            ViewData["urls"] = urls;
            return View();
        }
        public ActionResult FxDialog1(string filecount)
        {
            ViewData["filecount"] = filecount;
            return View();
        }
        //执行分享的操作(生成分享连接地址)
        public ActionResult Fx(string ids, string password)
        {
            return Content("");
        }

        //进入分享文件的地址
        [HttpGet]
        public ActionResult Files(string id)
        {
            string sharekey = id;
            ViewData["filename"] = "";
            ViewData["updatetime"] = "";
            ViewData["filesize"] = "";
            ViewData["downloadcount"] = "";
            ViewData["link"] = "";
            ViewData["downloadlink"] = "";
            var model = bll_miniyun_share_files.GetModelList("share_key='" + sharekey + "'").FirstOrDefault();
            if (model != null)
            {
                miniyun_files file = bll_miniyun_files.GetModelList("id=" + model.file_id).FirstOrDefault();
                ViewData["downloadcount"] = model.down_count;
                ViewData["filename"] = file.file_name;
                ViewData["filesize"] = CountSize(file.file_size);
                ViewData["updatetime"] = file.created_at.ToString("yyyy-MM-dd HH:mm:ss");
                var reg = Regex.Match(HttpContext.Request.Url.ToString(), @"(http://.+?)/.+");
                string root = reg.Groups[1].Value;
                ViewData["link"] = string.Format("{0}/DiskN/Files/{1}", root, sharekey);
                ViewData["downloadlink"] = "/diskn/DownLoad?id=" + file.id + "&type=mydoc";
                if (model.password != "-1")
                {
                    ViewData["fileid"] = id;
                    return View("FileKey");
                }
                else
                {
                    return View();
                }
            }

            return Content("对不起！分享地址不存在！");
        }
        [HttpPost]
        public ActionResult Files(string pwd, string id)
        {
            string sharekey = id;
            ViewData["filename"] = "";
            ViewData["updatetime"] = "";
            ViewData["filesize"] = "";
            ViewData["downloadcount"] = "";
            ViewData["link"] = "";
            ViewData["downloadlink"] = "";
            var model = bll_miniyun_share_files.GetModelList("share_key='" + sharekey + "'").FirstOrDefault();
            if (model != null)
            {

                if (model.password == Common.StringPlus.StringToMD5(pwd))
                {
                    miniyun_files file = bll_miniyun_files.GetModelList("id=" + model.file_id).FirstOrDefault();
                    ViewData["downloadcount"] = model.down_count;
                    ViewData["filename"] = file.file_name;
                    ViewData["filesize"] = CountSize(file.file_size);
                    ViewData["updatetime"] = file.created_at.ToString("yyyy-MM-dd HH:mm:ss");
                    var reg = Regex.Match(HttpContext.Request.Url.ToString(), @"(http://.+?)/.+");
                    string root = reg.Groups[1].Value;
                    ViewData["link"] = string.Format("{0}/DiskN/Files/{1}", root, sharekey);
                    ViewData["downloadlink"] = "/diskn/DownLoad?id=" + file.id + "&type=mydoc";
                    return View("");
                }
                else
                {
                    ViewData["msg"] = "访问密码错误";
                    ViewData["fileid"] = id;
                    return View("FileKey");
                }
            }
            else
            {
                return Content("");
            }
        }
        #endregion
        #region 以下是公共资料库
        //列举文件
        public JsonResult PGetFileList(int pid)
        {
            string where = "is_deleted=0 and parent_file_id=" + pid;

            var fileList = bll_public_files.GetModelList(where);
            var data = from c in fileList
                       orderby c.file_type descending, c.updated_at descending //默认按照文件类型排序
                       select new
                       {
                           c.id,
                           pid = c.parent_file_id,
                           ft = c.file_type,   //类型
                           icon = GetFileIcon(c.file_type, c.mime_type),      //图标类型
                           fn = c.file_name,   //文件名
                           n = c.mime_type == "folder" ? c.file_name : Path.GetFileNameWithoutExtension(c.file_name),//文件去掉扩展名的名称(用于改名时使用)
                           fz = c.mime_type == "folder" ? "—" : CountSize(c.fie_size),   //文件大小
                           ut = c.updated_at.ToString("yyyy-MM-dd HH:mm:ss") //修改时间
                       };

            return Json(data);
        }
        //新建公共文件夹
        public JsonResult PNewFolder(string foldername, int pid)
        {

            var loginuser = GetLoginUser();
            string uid = loginuser.uid;
            //1.查找是否具有同名的收集夹,因为是一级所以不需要判断层级(pid)
            string path = string.Format("/{0}/{1}", uid, foldername);
            var exitsmodel = bll_public_files.GetModelList("parent_file_id=" + pid + " and file_name='" + foldername + "'").FirstOrDefault();
            if (exitsmodel != null)
            {
                return Json(new { error = 3, msg = "文件夹已存在！" });
            }
            bool b = bll_public_files.Add(new Model.public_files
            {
                create_at = DateTime.Now,
                cuserids = " ",
                cuseridss = " ",
                fie_size = 0,
                file_create_time = 0,
                file_name = foldername,
                file_path = path,
                file_type = 1,
                file_update_time = 0,
                is_deleted = 0,
                mime_type = "pfolder",
                parent_file_id = pid,
                updated_at = DateTime.Now,
                sort = 0,
                user_id = Convert.ToInt32(uid),
                version_id = 1
            });
            if (b)
            {
                return Json(new { error = 0, msg = "新建文件夹成功！" });
            }
            else
            {
                return Json(new { error = 1, msg = "新建文件夹失败！" });
            }

        }
        //value:0代表未删除,1代表删除
        public ContentResult PUpdateFile(string ids, int value)
        {

            string[] idss = ids.Split(',');
            try
            {
                foreach (var id in idss)
                {
                    var model = bll_public_files.GetModel(Convert.ToInt32(id));
                    model.updated_at = DateTime.Now;
                    model.is_deleted = value;
                    bll_public_files.Update(model);
                }
                return Content("ok");
            }
            catch (Exception)
            {

                return Content("error");
            }

        }
        //修改文件名的操作.
        public ContentResult PReName(int id, string filename)
        {

            var model = bll_public_files.GetModelList(" id=" + id.ToString()).FirstOrDefault();
            if (model != null)
            {
                model.updated_at = DateTime.Now;
                model.file_name = filename + Path.GetExtension(model.file_name);
                if (bll_public_files.Update(model))
                {
                    return Content("ok");
                }

            }
            return Content("error");
        }
        //搜索操作
        public JsonResult search(string key)
        {
            var datalist = bll_public_files.GetModelList("file_name like'%" + key + "%'");
            var data = from c in datalist
                       orderby c.file_type descending, c.mime_type, c.id descending //默认按照文件类型排序
                       select new
                       {
                           c.id,
                           pid = c.parent_file_id,
                           ft = c.file_type,   //类型
                           icon = GetFileIcon(c.file_type, c.mime_type),      //图标类型
                           fn = c.file_name,   //文件名
                           n = c.mime_type == "folder" ? c.file_name : Path.GetFileNameWithoutExtension(c.file_name),//文件去掉扩展名的名称(用于改名时使用)
                           fz = c.mime_type == "folder" ? "—" : CountSize(c.fie_size),   //文件大小
                           ut = c.updated_at.ToString("yyyy-MM-dd HH:mm:ss") //修改时间
                       };
            return Json(data);
        }
        #endregion

        #region 辅助函数
        /// <summary>
        /// 判断收集夹中id为uid的用户是否上交了资料
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="folderid"></param>
        /// <returns></returns>
        private bool FolderHasUserFile(string uid, int folderid)
        {
            var file = bll_file_collection.GetModelList("parent_file_id=" + folderid + " and user_id=" + uid).FirstOrDefault();
            return file != null;
        }

        private void CopyFile(string id, string tempPath)
        {
           
            var model = bll_miniyun_files.GetModelList("id=" + id).FirstOrDefault();

            if (model.file_type == 1 || model.file_type == 2 || model.file_type == 3) //如果是文件夹需要递归处理
            {
                string folder = ProcessFolder(Path.Combine(tempPath, model.file_name));//建立文件夹
                var subfiles = bll_miniyun_files.GetModelList("parent_file_id=" + id);
                foreach (var item in subfiles)
                {
                    CopyFile(item.id.ToString(), folder);
                }
                return;
            }
            int version_id = model.version_id;
            var file_version = bll_miniyun_versions.GetModel(version_id);
            string s = file_version.file_signature;
            string shitp = s.Substring(0, 2) + @"\";
            shitp += s.Substring(2, 2) + @"\";
            shitp += s.Substring(4, 2) + @"\";
            shitp += s.Substring(6, 2) + @"\" + s;
            string filepath = Path.Combine(ConfigHelper.GetConfigString("uploaddir"), shitp);
            string filename = model.file_name;
            filepath = Server.MapPath(filepath);
            string desPath = Path.Combine(tempPath, filename);
            System.IO.File.Copy(filepath, desPath, true);
        }

        //打包文件函数 addby ao 2014-11-11
        public string CompressFile(string path)
        {
            FastZip fz = new FastZip();
            fz.CreateEmptyDirectories = true;
            string desPath = Path.Combine(ProcessFolder(Server.MapPath(@"\") + @"\zip"), path.Substring(path.LastIndexOf(@"\") + 1, 32) + ".zip");
            fz.CreateZip(desPath, path, true, "");
            fz = null;
            return desPath;

        }
        /// <summary>
        /// 保证文件夹存在
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string ProcessFolder(string path)
        {
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            return path;
        }
        #endregion
    }

}
