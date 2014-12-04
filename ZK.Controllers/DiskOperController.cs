using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.SessionState;
using System.Web;

namespace ZK.Controllers
{
    public class DiskOperController : Controller, IRequiresSessionState
    {
        //公共字段
        #region 公共字段

        BLL.miniyun_files bll_miniyun_files = new BLL.miniyun_files();
        BLL.miniyun_events bll_miniyun_events = new BLL.miniyun_events();

        #endregion


        /// <summary>
        /// 按照标识更新相应的字段
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdateFileInfo()
        {
            string Flag = Request.Form["Flag"];
            string FileID = Request.Form["FileID"];
            string Value = Request.Form["Value"];

            return Content(UpdateFileModelByConditon(FileID, Flag, Value).ToLower());
        }

        /// <summary>
        /// 从回收站删除相应的文件或 清空回收站
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteFileInfo()
        {

            string FileID = Request.Form["FileID"];
            int id = 0;
            if (string.IsNullOrEmpty(FileID) || !int.TryParse(FileID, out id))
            {
                return Content("false");
            }
            if (id == -1)
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
                return Content(bll_miniyun_files.Delete(id).ToString().ToLower());
            }
        }

        public ActionResult AddNewFoler()
        {
            string foldername = Request.Form["foldername"];
            string parent_id = Request.Form["parent_id"];

            return Content(AddNewFile(foldername, parent_id, "", false));

        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        public ActionResult UploadFiles()
        {

            HttpPostedFileBase file = Request.Files["Filedata"];

            string savepath = Request["folder"];
            string _parm = Request["_parm"];
            string mime_type = _parm.Split('_')[0];
            string parent_id = _parm.Split('_')[1];
            string uploadPath = Server.MapPath(savepath);
            try
            {
                if (file != null)
                {
                    if (!System.IO.Directory.Exists(uploadPath))
                    {
                        System.IO.Directory.CreateDirectory(uploadPath);
                    }
                    file.SaveAs(uploadPath + "/" + file.FileName);
                    string file_ext = System.IO.Path.GetExtension(file.FileName);
                    mime_type = Common.CommonFunction.ExtTomimetype(file_ext.ToString().Substring(1));
                    //向数据库添加数据
                    AddNewFile(file.FileName, parent_id, mime_type, true);

                    //下面这句代码缺少的话，上传成功后上传队列的显示不会自动消失
                    return Content("1");
                }
                else
                {
                    return Content("0");
                }
            }
            catch
            {
                return Content("0");
            }
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
        private string AddNewFile(string filename, string parent_id, string mime_type, bool isfile)
        {

            //先查询出该文件或文件夹的父文件夹
            string folderpath = ZK.Common.ModelSettings.CreateFileDefaultPath;
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

            //判断是否存在该文件夹
            StringBuilder builder = new StringBuilder();
            builder.Append(" 1=1 ");
            builder.Append(" and file_name='" + model.file_name + "' ");
            builder.Append(" and user_id=" + model.user_id + " ");
            builder.Append(" and file_path='" + model.file_path + "' ");
            builder.Append(" and parent_file_id=" + model.parent_file_id + " ");

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
            //创建该文件夹
            if (!isfile)
            {
                System.IO.Directory.CreateDirectory(Request.MapPath(folderpath + "/" + filename));
                model.file_type = 1;
            }
            //model.mime_type = mime_type;
            model.mime_type = mime_type;

            model.created_at = DateTime.Now;
            model.event_uuid = event_uuid;
            model.updated_at = DateTime.Now;
            bool bresult1 = bll_miniyun_files.Add(model);

            //事件表
            Model.miniyun_events eventmodel = new Model.miniyun_events();
            eventmodel.user_id = Convert.ToInt32(GetCurrentDiskUserID());
            eventmodel.user_device_id = 1;
            eventmodel.action = 0;
            eventmodel.file_path = folderpath + "/" + filename;
            eventmodel.context = "文件夹为path 文件为hash及大小版本等";
            eventmodel.event_uuid = event_uuid;
            eventmodel.created_at = DateTime.Now;
            eventmodel.updated_at = DateTime.Now;

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
        /// 获取当前用户名
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
            return Session["username"].ToString();
        }


        /// <summary>
        /// 获取当前的使用网盘的用户id
        /// </summary>
        /// <returns></returns>
        private string GetCurrentDiskUserID()
        {
            string username = GetCurrentUserName();

            return new BLL.miniyun_users().GetList(" user_name='" + username + "'").Tables[0].Rows[0]["id"].ToString();
        }

    }
}
