using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Data;
using System.Web;

namespace ZK.Controllers
{
    [ZKAuthAttributeFilter(purl = "")]
    public class PushController : Controller
    {
        BLL.ZK_ChannelGroup bllchannelgroup = new BLL.ZK_ChannelGroup();
        /// <summary>
        /// 推送分类界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {

            ViewData["url_Param"] = "";
            //cate_id 文件夹ID file_id 文件ID
            //string Cate_ID = Request["id"];
            string File_ID = Request["file_id"];
            if (File_ID == "")
            {
                return View("IndexN");
            }
            ViewData["url_Param"] = "file_id=" + File_ID;
            return View("IndexN");
        }

        /// <summary>
        /// 行政推送页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Admin()
        {

            BLL.ZK_Tags blltags = new BLL.ZK_Tags();

            #region 绑定类别

            string strWhere = " channelID=3 ";
            DataTable AdminCGlist = bllchannelgroup.GetList(strWhere).Tables[0];
            ViewData["AdminCGlist"] = AdminCGlist;

            #endregion

            #region 关键字绑定

            DataTable listtags = blltags.GetAllList().Tables[0];
            ViewData["listtags"] = listtags;

            #endregion

            return View();
        }

        /// <summary>
        /// 教学推送页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Teach()
        {
            BLL.ZK_Course bllcourse = new BLL.ZK_Course();
            BLL.ZK_Grade bllgrade = new BLL.ZK_Grade();
            BLL.ZK_Edition blledition = new BLL.ZK_Edition();
            BLL.ZK_Tags blltags = new BLL.ZK_Tags();
            BLL.ZK_JXCategory blljxcategory = new BLL.ZK_JXCategory();
            BLL.ZK_LessonClass blllescla = new BLL.ZK_LessonClass();
            Model.ZK_LessonClass lesClass=new  Model.ZK_LessonClass();
            #region 科目

            DataSet courseds = bllcourse.GetAllList();
            if (courseds.Tables.Count > 0)
            {
                List<Model.ZK_Course> CourseList = bllcourse.DataTableToList(courseds.Tables[0]);
                ViewData["Teach_CourseList"] = CourseList;
            }
            else
            {
                ViewData["Teach_CourseList"] = new List<Model.ZK_Course>();
            }

            #endregion

            #region 年级

            DataSet gradds = bllgrade.GetAllList();
            if (gradds.Tables.Count > 0)
            {
                List<Model.ZK_Grade> gradeList = bllgrade.DataTableToList(gradds.Tables[0]);
                ViewData["Teach_GradeList"] = gradeList;
            }
            else
            {
                ViewData["Teach_GradeList"] = new List<Model.ZK_Grade>();
            }
            #endregion

            #region 版本

            DataSet editionds = blledition.GetAllList();
            if (editionds.Tables.Count > 0)
            {
                List<Model.ZK_Edition> editionList = blledition.DataTableToList(editionds.Tables[0]);
                ViewData["Teach_EditionList"] = editionList;
            }
            else
            {
                ViewData["Teach_EditionList"] = new List<Model.ZK_Edition>();
            }
            #endregion

            #region 教学资源分类
            DataSet teachcategory = blljxcategory.GetAllList();
            if (teachcategory.Tables.Count > 0)
            {
                List<Model.ZK_JXCategory> categroyList = blljxcategory.DataTableToList(teachcategory.Tables[0]);
                ViewData["Teach_CategoryList"] = categroyList;
            }
            else
            {
                ViewData["Teach_CategoryList"] = new List<Model.ZK_JXCategory>();
            }
            #endregion

            #region 关键字绑定

            DataTable listtags = blltags.GetAllList().Tables[0];
            ViewData["listtags"] = listtags;

            #endregion

            return View();
        }
        /// <summary>
        /// 获取课程列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Teach_GetLessonList()
        {

            BLL.ZK_LessonClass blllessonclass = new BLL.ZK_LessonClass();
            BLL.ZK_Lesson blllesson = new BLL.ZK_Lesson();
            string courseid = Request.Form["courseid"];
            string gradeid = Request.Form["gradeid"];
            string editionid = Request.Form["editionid"];
            try
            {
                string strWhere = " courseID=" + courseid + " and gradeID=" + gradeid + " and editionID=" + editionid;
                List<Model.ZK_LessonClass> listclass = blllessonclass.GetModelList(strWhere);

                strWhere = " classid=" + listclass[0].classID.ToString();
                List<Model.ZK_Lesson> listlesson = blllesson.GetModelList(strWhere);
                
                return Json(listlesson);
            }
            catch
            {
                return Json(new Model.ZK_Lesson());
            }
        }

        /// <summary>
        /// 推送文件到教学频道
        /// </summary>
        /// <returns></returns>
        public ActionResult Push_TeachFile()
        {//"courseid": courseid, "gradeid": gradeid, "editionid": editionid 
            string lessonid = Request.Form["lessonid"];
            string keyword = Request.Form["keyword"];
            string File_IDs = Request.Form["file_id"];
            int teachcateid = Convert.ToInt32(Request.Form["teachcateid"]);//所属分类ID

            string courseid = Request.Form["courseid"];
            string gradeid = Request.Form["gradeid"];
            string editionid = Request.Form["editionid"];
            //得到classID
            string classID ="";
            List<Model.ZK_LessonClass> listclass = new ZK.BLL.ZK_LessonClass().GetModelList(" courseId=" + courseid + " and gradeID= " + gradeid + " and editionID= " + editionid);

            int leid = 0;
            if (listclass.Count > 0)
            {

                classID = listclass[0].classID.ToString();
                ZK.Model.ZK_Lesson lesson = new Model.ZK_Lesson();
                lesson.classID = Convert.ToInt32(classID);
                lesson.lessonParent = -1;
                leid = new ZK.BLL.ZK_Lesson().Add(lesson);
              
            }
            
            if (File_IDs == "")
            {
                return Json("推送失败");
            }
            List<string> list_ids = File_IDs.Split(',').ToList();
            List<string> temps = new List<string>();
            //在资源列表表中存在的资源
            List<int> list_ids2 = new List<int>();
            foreach (var item in list_ids)
            {
                //查看是否有相同的文件存在
                List<ZK.Model.ZK_FileList> modelfileck = new BLL.ZK_FileList().GetModelList("fileOldID=" + item);
                if (modelfileck != null && modelfileck.Count > 0)
                {
                    //return Json("成功");
                    temps.Add(item);
                    list_ids2.Add(modelfileck[0].fileID);
                }
            }
            foreach (var item in temps)
            {
                list_ids.Remove(item);
            }

            foreach (var ids in list_ids)
            {
                //推送资源

                ZK.Model.miniyun_files modelFile = new ZK.BLL.miniyun_files().GetModel(int.Parse(ids));

                #region 对 ZK_FileList 操作

                Model.ZK_FileList filelistmodel = new Model.ZK_FileList();
                filelistmodel.fileName = modelFile.file_name;
                filelistmodel.filePath = modelFile.file_path;
                filelistmodel.createTime = modelFile.created_at;
                filelistmodel.trastatus = GetFileStatus((int)modelFile.id);
                filelistmodel.isTraf = 0;
                //filelistmodel.fileDesc = modelFile.file_desc;
                string filetype = modelFile.mime_type;

                //类型转换    
                filelistmodel.fileTypeID = OperateForFiles(filetype, filelistmodel, modelFile);

                filelistmodel.fileOldID = modelFile.id;
                filelistmodel.isDir = modelFile.file_type;
                filelistmodel.ownerID = GetOwnerIDBYOldUserID(modelFile.user_id);
                filelistmodel.parentID = modelFile.parent_file_id;
                int fileid = new BLL.ZK_FileList().Add(filelistmodel);//返回的是fileID

                if (fileid == 0)
                {
                    return Json("部分文件推送错误，请重试");
                }
                if (filelistmodel.isTraf == 1)
                {
                    filelistmodel.fileID = fileid;
                    //添加到要转换的表中
                    int resint = AddConvertVideosToFileListTra(filelistmodel);
                    new BLL.ZK_FileList().Update(filelistmodel);
                }
                #endregion

                #region 对 ZK_LessonAndFileList 操作

                Model.ZK_LessonAndFileList lessonfilelist = new Model.ZK_LessonAndFileList();
                lessonfilelist.fileID = fileid;
                lessonfilelist.lessonID = leid;
                lessonfilelist.CategoryId = teachcateid;
                if (new BLL.ZK_LessonAndFileList().Add(lessonfilelist) == 0)
                {
                    //回滚 删除相应的文件 
                    new BLL.ZK_FileList().Delete(fileid);
                    return Json("部分文件推送错误，请重试");
                }
                #endregion


                #region 对 ZK_FileAndTags 操作
                if (keyword != "")//关键字不为空
                {
                    Model.ZK_FileAndTags fileandtagsmodel = new Model.ZK_FileAndTags();
                    BLL.ZK_Tags blltags = new BLL.ZK_Tags();
                    fileandtagsmodel.fileID = fileid;
                    string[] strtags = keyword.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    List<Model.ZK_Tags> taglist = blltags.DataTableToList(blltags.GetAllList().Tables[0]);
                    Dictionary<string, int> dictags = new Dictionary<string, int>();
                    foreach (var item in taglist)
                    {
                        dictags.Add(item.tagName, item.tagID);
                    }
                    Model.ZK_Tags tagmodel = new Model.ZK_Tags();
                    for (int i = 0; i < strtags.Length; i++)
                    {
                        //如果没有关键字 则插入该关键字
                        if (!dictags.ContainsKey(strtags[i]))
                        {
                            tagmodel.tagName = strtags[i];
                            tagmodel.createTime = DateTime.Now.Date;
                            //tagmodel.ownerID = 10010;//默认的用户名 需要获取
                            dictags.Add(strtags[i], blltags.Add(tagmodel));
                        }
                        //项fileandtag里添加数据
                        fileandtagsmodel.tagID = dictags[strtags[i]];
                        fileandtagsmodel.fileID = fileid;
                        new BLL.ZK_FileAndTags().Add(fileandtagsmodel);
                    }
                }
                //else { //关键字为空

                //}
                #endregion
            }

            foreach (var fileid in list_ids2)
            {
                //推送资源

                #region 对 ZK_LessonAndFileList 操作
                string strwhere = "";
                Model.ZK_LessonAndFileList lessonfilelist = new Model.ZK_LessonAndFileList();
                if (lessonid == "")//+ " and lessonID=" + lessonid
                {
                  strwhere = " fileID=" + fileid;
                }
                else {
                    strwhere = " fileID=" + fileid + " and lessonID=" + lessonid;
                }
                List<Model.ZK_LessonAndFileList> list = new BLL.ZK_LessonAndFileList().GetModelList(strwhere);
                if (list != null && list.Count > 0)
                {
                    continue;
                }
                lessonfilelist.fileID = fileid;
                if (lessonid == "")
                {
                    lessonid =(new ZK.BLL.ZK_LessonAndFileList().GetMaxId()+1).ToString();
                }
                lessonfilelist.lessonID = Convert.ToInt32(lessonid);
                new BLL.ZK_LessonAndFileList().Add(lessonfilelist);
                #endregion

                #region 对 ZK_FileAndTags 操作
                if (keyword != "")
                {
                    Model.ZK_FileAndTags fileandtagsmodel = new Model.ZK_FileAndTags();
                    BLL.ZK_Tags blltags = new BLL.ZK_Tags();
                    fileandtagsmodel.fileID = fileid;
                    string[] strtags = keyword.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    List<Model.ZK_Tags> taglist = blltags.DataTableToList(blltags.GetAllList().Tables[0]);
                    Dictionary<string, int> dictags = new Dictionary<string, int>();
                    foreach (var item in taglist)
                    {
                        dictags.Add(item.tagName, item.tagID);
                    }
                    Model.ZK_Tags tagmodel = new Model.ZK_Tags();
                    for (int i = 0; i < strtags.Length; i++)
                    {
                        //如果没有关键字 则插入该关键字
                        if (!dictags.ContainsKey(strtags[i]))
                        {
                            tagmodel.tagName = strtags[i];
                            tagmodel.createTime = DateTime.Now.Date;
                            //tagmodel.ownerID = 10010;//默认的用户名 需要获取
                            dictags.Add(strtags[i], blltags.Add(tagmodel));
                        }
                        //项fileandtag里添加数据
                        string tagwhere = " tagID=" + dictags[strtags[i]] + " and fileID=" + fileid;
                        List<Model.ZK_FileAndTags> fileandtaglist = new BLL.ZK_FileAndTags().GetModelList(tagwhere);
                        if (fileandtaglist != null && fileandtaglist.Count > 0)
                        {
                            continue;
                        }
                        fileandtagsmodel.tagID = dictags[strtags[i]];
                        fileandtagsmodel.fileID = fileid;
                        new BLL.ZK_FileAndTags().Add(fileandtagsmodel);
                    }
                }
                #endregion
            }



            return Json("成功");
        }

        /// <summary>
        /// 德育推送页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Moral()
        {
            BLL.ZK_Tags blltags = new BLL.ZK_Tags();

            #region 绑定类别

            string strWhere = " channelID=2 ";
            DataTable MoralCGlist = bllchannelgroup.GetList(strWhere).Tables[0];
            ViewData["MoralCGlist"] = MoralCGlist;

            #endregion

            #region 关键字绑定

            DataTable listtags = blltags.GetAllList().Tables[0];
            ViewData["listtags"] = listtags;

            #endregion

            return View();
        }

        /// <summary>
        /// 推送行政 德育 文件
        /// </summary>
        /// <returns></returns>
        public ActionResult Push_OtherFile()
        {
            //获取当前用户
            GetCurrentUser();
            string channelgroupid = Request.Form["channelgroupid"];
            string keyword = Request.Form["keyword"];
            string File_IDs = Request.Form["file_id"];
            if (File_IDs == "")
            {
                return Json("推送失败");
            }

            List<string> list_ids = File_IDs.Split(',').ToList();
            List<string> temps = new List<string>();
            //在资源列表表中存在的资源
            List<int> list_ids2 = new List<int>();
            foreach (var item in list_ids)
            {
                //查看是否有相同的文件存在
                List<ZK.Model.ZK_FileList> modelfileck = new BLL.ZK_FileList().GetModelList("fileOldID=" + item);
                if (modelfileck != null && modelfileck.Count > 0)
                {
                    //return Json("成功");
                    temps.Add(item);
                    list_ids2.Add(modelfileck[0].fileID);
                }
            }
            foreach (var item in temps)
            {
                list_ids.Remove(item);
            }

            foreach (var ids in list_ids)
            {
                //推送资源

                ZK.Model.miniyun_files modelFile = new ZK.BLL.miniyun_files().GetModel(int.Parse(ids));

                #region 对 ZK_FileList 操作

                Model.ZK_FileList filelistmodel = new Model.ZK_FileList();
                filelistmodel.fileName = modelFile.file_name;
                filelistmodel.filePath = modelFile.file_path;
                filelistmodel.createTime = modelFile.created_at;
                filelistmodel.trastatus = GetFileStatus((int)modelFile.id);
                filelistmodel.isTraf = 0;
                //filelistmodel.fileDesc = modelFile.file_desc;
                string filetype = modelFile.mime_type;

                //类型转换    
                filelistmodel.fileTypeID = OperateForFiles(filetype, filelistmodel, modelFile);

                filelistmodel.fileOldID = modelFile.id;
                filelistmodel.isDir = modelFile.file_type;
                //filelistmodel.ownerID = Convert.ToInt32(Session["uid"]);
                filelistmodel.ownerID = GetOwnerIDBYOldUserID(modelFile.user_id);
                filelistmodel.parentID = modelFile.parent_file_id;
                int fileid = new BLL.ZK_FileList().Add(filelistmodel);
                if (fileid == 0)
                {
                    return Json("失败");
                }
                if (filelistmodel.isTraf == 1)
                {
                    filelistmodel.fileID = fileid;
                    //添加到要转换的表中
                    int resint = AddConvertVideosToFileListTra(filelistmodel);
                    new BLL.ZK_FileList().Update(filelistmodel);
                }
                #endregion

                #region 对 ChannelGroupAndFileList 操作

                Model.ZK_ChannelGroupAndFileList CGAFmodel = new Model.ZK_ChannelGroupAndFileList();
                CGAFmodel.fileID = fileid;
                CGAFmodel.channelGroupID = Convert.ToInt32(channelgroupid);
                if (new BLL.ZK_ChannelGroupAndFileList().Add(CGAFmodel) == 0)
                {
                    new BLL.ZK_FileList().Delete(fileid);
                    return Json("失败");
                }
                #endregion

                #region 对 ZK_FileAndTags 操作

                if (keyword != "")
                {
                    Model.ZK_FileAndTags fileandtagsmodel = new Model.ZK_FileAndTags();
                    BLL.ZK_Tags blltags = new BLL.ZK_Tags();
                    fileandtagsmodel.fileID = fileid;
                    string[] strtags = keyword.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    List<Model.ZK_Tags> taglist = blltags.DataTableToList(blltags.GetAllList().Tables[0]);
                    Dictionary<string, int> dictags = new Dictionary<string, int>();
                    foreach (var item in taglist)
                    {
                        dictags.Add(item.tagName, item.tagID);
                    }
                    Model.ZK_Tags tagmodel = new Model.ZK_Tags();
                    for (int i = 0; i < strtags.Length; i++)
                    {
                        //如果没有关键字 则插入该关键字
                        if (!dictags.ContainsKey(strtags[i]))
                        {
                            tagmodel.tagName = strtags[i];
                            tagmodel.createTime = DateTime.Now.Date;
                            //tagmodel.ownerID = 10010;//默认的用户名 需要获取
                            dictags.Add(strtags[i], blltags.Add(tagmodel));
                        }
                        //项fileandtag里添加数据
                        fileandtagsmodel.tagID = dictags[strtags[i]];
                        fileandtagsmodel.fileID = fileid;
                        new BLL.ZK_FileAndTags().Add(fileandtagsmodel);
                    }
                }
                #endregion

            }
            foreach (var fileid in list_ids2)
            {
                //推送资源

                #region 对 ChannelGroupAndFileList 操作

                Model.ZK_ChannelGroupAndFileList CGAFmodel = new Model.ZK_ChannelGroupAndFileList();
                string strwhere = " fileID=" + fileid + " and channelGroupID=" + channelgroupid;
                List<Model.ZK_ChannelGroupAndFileList> list = new BLL.ZK_ChannelGroupAndFileList().GetModelList(strwhere);
                if (list != null && list.Count > 0)
                {
                    continue;
                }
                CGAFmodel.fileID = fileid;
                CGAFmodel.channelGroupID = Convert.ToInt32(channelgroupid);
                if (new BLL.ZK_ChannelGroupAndFileList().Add(CGAFmodel) == 0)
                {
                    new BLL.ZK_FileList().Delete(fileid);
                    return Json("失败");
                }
                #endregion

                #region 对 ZK_FileAndTags 操作

                if (keyword != "")
                {
                    Model.ZK_FileAndTags fileandtagsmodel = new Model.ZK_FileAndTags();
                    BLL.ZK_Tags blltags = new BLL.ZK_Tags();
                    fileandtagsmodel.fileID = fileid;
                    string[] strtags = keyword.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    List<Model.ZK_Tags> taglist = blltags.DataTableToList(blltags.GetAllList().Tables[0]);
                    Dictionary<string, int> dictags = new Dictionary<string, int>();
                    foreach (var item in taglist)
                    {
                        dictags.Add(item.tagName, item.tagID);
                    }
                    Model.ZK_Tags tagmodel = new Model.ZK_Tags();
                    for (int i = 0; i < strtags.Length; i++)
                    {
                        //如果没有关键字 则插入该关键字
                        if (!dictags.ContainsKey(strtags[i]))
                        {
                            tagmodel.tagName = strtags[i];
                            tagmodel.createTime = DateTime.Now.Date;
                            //tagmodel.ownerID = 10010;//默认的用户名 需要获取
                            dictags.Add(strtags[i], blltags.Add(tagmodel));
                        }
                        //项fileandtag里添加数据
                        string tagwhere = " tagID=" + dictags[strtags[i]] + " and fileID=" + fileid;
                        List<Model.ZK_FileAndTags> fileandtaglist = new BLL.ZK_FileAndTags().GetModelList(tagwhere);
                        if (fileandtaglist != null && fileandtaglist.Count > 0)
                        {
                            continue;
                        }
                        fileandtagsmodel.tagID = dictags[strtags[i]];
                        fileandtagsmodel.fileID = fileid;
                        new BLL.ZK_FileAndTags().Add(fileandtagsmodel);
                    }
                }
                #endregion

            }
            return Json("成功");
        }


        /// <summary>
        /// 转换视频格式
        /// </summary>
        /// <param name="filepath">待转换路径</param>
        /// <param name="fileID">文件id</param>
        private string ConvertVideo(string filepath, string targetPath)
        {
            string convertpath = Request.MapPath(targetPath);

            return ZK.Common.VideoHelper.ConvertVideo(filepath, convertpath, Request.MapPath(ZK.Common.ModelSettings.FFmpegPath)).ToString();
        }


        /// <summary>
        /// 获取当前用户名
        /// </summary>
        private void GetCurrentUser()
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
        }

        /// <summary>
        /// 为filelisttra添加数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private int AddConvertVideosToFileListTra(Model.ZK_FileList filemodel)
        {
            BLL.ZK_FileListTra bll_filelisttra = new BLL.ZK_FileListTra();
            Model.ZK_FileListTra model = new Model.ZK_FileListTra();
            model.createTime = filemodel.createTime;
            model.fileID = filemodel.fileID;
            return bll_filelisttra.Add(model);
        }

        /// <summary>
        /// 转换文件typeid 
        /// </summary>
        /// <param name="filetype"></param>
        /// <param name="filelistmodel"></param>
        /// <returns>filetypeid</returns>
        private int OperateForFiles(string filetype, Model.ZK_FileList filelistmodel, Model.miniyun_files minimodel)
        {

            if (!string.IsNullOrEmpty(filetype))
            {

                //文档分类
                if (filetype.Split('/')[0] == "application")
                {
                    string doctype = filetype.Split('/')[1];
                    switch (doctype)
                    {
                        case "mspowerpoint":
                            filetype = "7";
                            break;
                        case "msword":
                            filetype = "2";
                            break;
                        case "msexcel":
                            filetype = "6";
                            break;
                        case "pdf":
                            filetype = "8";
                            break;
                        default:
                            filetype = "10";
                            break;
                    }
                    //rar
                    if (new string[] { "zip", "x-rar-compressed", "x-msdownload", "x-msdownload", "vnd.ms-cab-compressed" }.Contains(doctype))
                    {
                        filetype = "9";
                    }
                }//视频
                else if (filetype.Split('/')[0] == "video")
                {

                    //判断是否可以转换
                    if (!new string[] { "x-sgi-movie", "" }.Contains(filetype.Split('/')[1]))
                    {
                        filelistmodel.isTraf = 1;

                    }
                    filetype = "1";
                }
                else if (filetype.Split('/')[0] == "audio")
                {
                    filetype = "4";
                }
                // 图片
                else if (filetype.Split('/')[0] == "image")
                {
                    filetype = "3";
                }
                //text
                else if (filetype.Split('/')[0] == "txt")
                {
                    filetype = "10";
                }
                else
                {
                    filetype = "10";
                }
            }
            else
            {
                filetype = "10";
            }
            string LVpath = "";
            string LIpath = "";
            string hashfilename = "";
            if (ZK.Common.ModelSettings.IsModelData)
            {
                LVpath = ZK.Common.ModelSettings.LVpath;
                LIpath = ZK.Common.ModelSettings.LIpath;
            }
            else
            {
                LVpath = ZK.Common.ModelSettings.CRootPath + "/" + GetFilePathByVersionID(minimodel.version_id.ToString(), out hashfilename) + "/" + hashfilename;
                LIpath = ZK.Common.ModelSettings.CRootPath + "/" + GetFilePathByVersionID(minimodel.version_id.ToString(), out hashfilename) + "/" + hashfilename;
            }

            // 1 视频  2 word  3 图片  4 音频  
            // 6 excel  7 ppt  8 pdf  9	rar  10	其他
            switch (Convert.ToInt32(filetype))
            {
                case 1: //视频转码
                    //视频file_path
                    //截图
                    ZK.Common.VideoHelper.CutImage(Server.MapPath(LVpath), Server.MapPath(ZK.Common.CommonFunction.GetPathWithoutExtension(LVpath) + ".png"), Request.MapPath(ZK.Common.ModelSettings.FFmpegPath));
                    //截取缩略图
                    // string imageName256 = ZK.Common.ImageHelper.Image(Server.MapPath(LVpath + ".png"), "_256.png", 548, 369);
                    string imageName48 = ZK.Common.ImageHelper.Image(Server.MapPath(LVpath + ".png"), "_48.png", 48, 48);

                    filelistmodel.imageURL = LVpath + "_48.png";

                    break;
                case 3: //图片截取缩略图
                    //图片的file_path
                    //缩略图1
                    string imageName = ZK.Common.ImageHelper.Image(Server.MapPath(LIpath), "_256.png", 548, 369);
                    //缩略图2
                    ZK.Common.ImageHelper.Image(Server.MapPath(LIpath), "_48.png", 48, 48);
                    filelistmodel.imageURL = LIpath.Substring(0, LIpath.Length - System.IO.Path.GetFileName(LIpath).Length) + imageName;
                    break;
                default:
                    filelistmodel.imageURL = ZK.Common.ModelSettings.imageURL;
                    break;
            }

            return Convert.ToInt32(filetype);
        }

        /// <summary>
        /// 通过versionid来获取该文件的地址和文件的hash文件名
        /// </summary>
        /// <param name="versionid"></param>
        /// <returns></returns>
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
        /// 获取推送的相同文件的转换状态
        /// </summary>
        /// <param name="fileoldid"></param>
        /// <returns></returns>
        private int GetFileStatus(int fileoldid)
        {
            List<Model.ZK_FileList> modellist = new BLL.ZK_FileList().GetModelList(" fileOldID=" + fileoldid);
            if (modellist != null && modellist.Count > 0)
            {
                return (int)modellist[0].trastatus;
            }
            else
            {
                return 0;
            }
        }

        private int GetOwnerIDBYOldUserID(int oldid)
        {
            Model.miniyun_users user = new BLL.miniyun_users().GetModel(oldid);
            List<Model.USERS> thisuser = new BLL.USERS().GetModelList("username='" + user.user_name + "'");
            return thisuser[0].USERID;
        }

    }
}
