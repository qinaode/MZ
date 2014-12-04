using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Data;

namespace RESTfulServices
{
    /// <summary>
    /// upload 的摘要说明
    /// </summary>
    public class upload : IHttpHandler
    {
        ZK.BLL.miniyun_files bllNetFile = new ZK.BLL.miniyun_files();
        ZK.BLL.miniyun_users bllNetUser = new ZK.BLL.miniyun_users();
        ZK.BLL.miniyun_file_versions bll_miniyun_versions = new ZK.BLL.miniyun_file_versions();
        ZK.BLL.miniyun_events bll_miniyun_events = new ZK.BLL.miniyun_events();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";         
            
            try
            {
                string userId = context.Request.QueryString["userID"];
                string parent_id = context.Request.QueryString["pid"];
                string from = context.Request.QueryString["from"];
                string mime_type = "";
                if (from == "web")
                {
                    if (context.Request.Files.Count > 0)
                    {                       
                        string fileName = Path.GetFileName(context.Request.Files["filename"].FileName);
                        string file_ext = System.IO.Path.GetExtension(fileName);
                        mime_type = ZK.Common.CommonFunction.ExtTomimetype(file_ext.ToString().Substring(1));
                        //string parent_id = "0";
                        string hashpath = "";
                        int netUserId = ChangeUserId(Convert.ToInt32(userId));
                        //向数据库添加数据
                        string strResult = AddNewFile(netUserId.ToString(), fileName, context.Request.Files["filename"].ContentLength, parent_id, mime_type, true, out hashpath);

                        //保存文件                        
                        string directorypath = context.Server.MapPath(ZK.Common.ModelSettings.CreateFileDefaultPath + "/" + GetFilePathByHash(hashpath));
                        if (!Directory.Exists(directorypath))
                        {
                            Directory.CreateDirectory(directorypath);
                        }
                        context.Request.Files["filename"].SaveAs(directorypath + "\\" + hashpath);

                    }
                }
               
            }
            catch (Exception ex)
            {
                context.Response.Write("<script>alert('" + ex.Message + "')</script>");
            }

        }

        /// <summary>
        /// 添加新的文件或文件夹
        /// </summary>
        /// <param name="filename">文件或文件夹名</param>
        /// <param name="parent_id">父文件夹id</param>
        /// <param name="isfile">是否为文件 ture 文件 false  文件夹</param>
        /// <returns></returns>
        private string AddNewFile(string userId, string filename, long file_size, string parent_id, string mime_type, bool isfile, out string hashpath)
        {
            DateTime TimeNow = DateTime.Now;
            hashpath = "";
            //先查询出该文件或文件夹的父文件夹
            string folderpath = "/" + userId;
            int pid = 0;
            if (parent_id != "0" && int.TryParse(parent_id, out pid))
            {
                ZK.Model.miniyun_files parentmodel = bllNetFile.GetModel(Convert.ToInt32(parent_id));
                folderpath = parentmodel.file_path;
            }

            string event_uuid = System.Guid.NewGuid().ToString();

            //文件表
            ZK.Model.miniyun_files model = new ZK.Model.miniyun_files();
            model.file_name = filename;
            model.user_id = Convert.ToInt32(userId);
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

            List<ZK.Model.miniyun_files> listmodel = bllNetFile.GetModelList(builder.ToString());
            if (listmodel != null && listmodel.Count > 0)
            {
                return "文件已存在";
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
                ZK.Model.miniyun_file_versions versionmodel = new ZK.Model.miniyun_file_versions();
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
            bool bresult1 = bllNetFile.Add(model);

            //事件表
            ZK.Model.miniyun_events eventmodel = new ZK.Model.miniyun_events();
            eventmodel.user_id = Convert.ToInt32(userId);
            eventmodel.user_device_id = 1;
            eventmodel.action = 0;
            eventmodel.file_path = folderpath + "/" + filename;
            eventmodel.context = "文件夹为path 文件为hash及大小版本等";
            eventmodel.event_uuid = event_uuid;
            eventmodel.created_at = TimeNow;
            eventmodel.updated_at = TimeNow;

            bool bresult2 = bll_miniyun_events.Add(eventmodel);
            if (bresult1 && bresult2)
            {
                return "1";
            }
            else
            {
                return "0";
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

        //转换成MySql中的UserId
        private int ChangeUserId(int userId)
        {
            int netUserId = userId;
            ZK.BLL.USERS bllUser = new ZK.BLL.USERS();
            DataSet dsUser = bllUser.GetList("userid=" + Convert.ToInt32(userId));
            List<ZK.Model.USERS> listUser = bllUser.DataTableToList(dsUser.Tables[0]);
            string userName = listUser[0].USERNAME;

            DataSet dsNetUser = bllNetUser.GetList("user_name='" + userName + "'");
            if (dsNetUser.Tables[0].Rows.Count > 0)
            {
                List<ZK.Model.miniyun_users> listNetUser = bllNetUser.DataTableToList(dsNetUser.Tables[0]);
                netUserId = listNetUser[0].id;
            }
            return netUserId;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}