using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;
using System.Data;
using ZK.Common;
using System.Web.Script.Serialization;
using System.IO;
using System.Xml;

namespace ZK.MControllers
{
    public class NetworkDiskController : Controller
    {
        #region 定义
        BLL.miniyun_files bllNetFile = new BLL.miniyun_files();
        Model.miniyun_files mdlNetFile = new Model.miniyun_files();

        BLL.miniyun_users bllNetUser = new BLL.miniyun_users();
        Model.miniyun_users mdlNetUser = new Model.miniyun_users();

        BLL.miniyun_file_versions bll_miniyun_versions = new BLL.miniyun_file_versions();
        Model.miniyun_file_versions mdl_miniyun_versions = new Model.miniyun_file_versions();

        BLL.miniyun_events bll_miniyun_events = new BLL.miniyun_events();
        Model.miniyun_events mdl_miniyun_events = new Model.miniyun_events();

        BLL.USERS bllUser = new BLL.USERS();
        Model.USERS mdlUser = new Model.USERS();
        #endregion

        #region 定义
        ZK.BLL.miniyun_files bll_miniyun_files = new ZK.BLL.miniyun_files();
        ZK.BLL.miniyun_file_versions bll_miniyun_file_versions = new ZK.BLL.miniyun_file_versions();
        ZK.BLL.miniyun_user_privilege bll_miniyun_user_privilege = new ZK.BLL.miniyun_user_privilege();
        ZK.BLL.miniyun_users bll_miniyun_users = new ZK.BLL.miniyun_users();
        ZK.BLL.miniyun_file_metas bll_miniyun_file_metas = new ZK.BLL.miniyun_file_metas();
        ZK.BLL.miniyun_file_metas bll_file_metas = new ZK.BLL.miniyun_file_metas();

        int folderId;
        #endregion

        #region 接口

        /// <summary>
        /// 根据 用户Id 获取用户网盘文件 类型及文件数和大小
        /// jcb为Json回调函数名，回传数据时需要
        /// userID为IM登录用户Id
        /// </summary>
        /// <param name="userId"> userId</param>
        /// <returns></returns>
        public string NetworkDiskListJson()
        {
            string jcbstr = Request["jcb"];
            try
            {
                string userID = Request["userID"];

                #region 返回数据定义
                int docNum = 0;
                int picNum = 0;
                int videoNum = 0;
                int audioNum = 0;
                int otherNum = 0;

                Double docSize = 0;
                Double picSize = 0;
                Double videoSize = 0;
                Double audioSize = 0;
                Double otherSize = 0;
                #endregion
                //userID = "10040";
                if (userID != null && userID != "")
                {
                    //得到im用户id
                    DataSet dsUser = bllUser.GetList("userid=" + Convert.ToInt32(userID));
                    List<Model.USERS> listUser = bllUser.DataTableToList(dsUser.Tables[0]);
                    //得到im用户名(用于和网盘中的MYSql数据用户关联,因为历史遗留问题！汗！！)
                    string userName = listUser[0].USERNAME;

                    DataSet dsNetUser = bllNetUser.GetList("user_name='" + userName + "'");
                    if (dsNetUser.Tables[0].Rows.Count > 0)
                    {
                        List<Model.miniyun_users> listNetUser = bllNetUser.DataTableToList(dsNetUser.Tables[0]);
                        int netUserId = listNetUser[0].id;

                        string strWhere = "1=1";

                        strWhere += " and user_id=" + netUserId;

                        strWhere += " order by mime_type";

                        DataSet ds = bllNetFile.GetList(strWhere);
                        List<Model.miniyun_files> listNetFile = new List<Model.miniyun_files>();
                        listNetFile = bllNetFile.DataTableToList(ds.Tables[0]);
                        if (listNetFile.Count > 0)
                        {
                            for (int i = 0; i < listNetFile.Count; i++)
                            {
                                string fileType = CommonFunction.GetTypeByMimeType(listNetFile[i].mime_type);
                                switch (fileType)
                                {
                                    case "2":
                                    case "6":
                                    case "7":
                                    case "8":
                                        docNum++;
                                        docSize += listNetFile[i].file_size;
                                        docSize = ReNum(docSize);
                                        break;
                                    case "3":
                                        picNum++;
                                        picSize += listNetFile[i].file_size;
                                        picSize = ReNum(picSize);
                                        break;
                                    case "1":
                                        videoNum++;
                                        videoSize += listNetFile[i].file_size;
                                        videoSize = ReNum(videoSize);
                                        break;
                                    case "4":
                                        audioNum++;
                                        audioSize += listNetFile[i].file_size;
                                        audioSize = ReNum(audioSize);
                                        break;
                                    default:
                                        otherNum++;
                                        otherSize += listNetFile[i].file_size;
                                        otherSize = ReNum(otherSize);
                                        break;
                                }
                            }
                        }
                    }
                }
               
                //string strJson = jcbstr + "({list:[{\"doc\":\"doc\",\"docNum\":" + docNum + ",\"docSize\":" + docSize + "},{\"pic\":\"pic\",\"picNum\":" + picNum + ",\"picSize\":" + picSize + "}, {\"video\":\"video\",\"videoNum\":" + videoNum + ",\"videoSize\":" + videoSize + "},{\"audio\":\"audio\",\"audioNum\":" + docNum + ",\"docSize\":" + docSize + "},{\"others\":\"others\",\"otherNum\":" + otherNum + ",\"otherSize\":" + otherSize + "}]})";
                string strJson = jcbstr + "({doc:{\"Num\":" + docNum + ",\"Size\":" + docSize + "},pic:{\"Num\":" + picNum + ",\"Size\":" + picSize + "}, video:{\"Num\":" + videoNum + ",\"Size\":" + videoSize + "},audio:{\"Num\":" + docNum + ",\"Size\":" + docSize + "},others:{\"Num\":" + otherNum + ",\"Size\":" + otherSize + "} })";
                return strJson;
            }
            catch
            {
                return jcbstr + "({})";
            }
        }

        /// <summary>
        /// 根据 用户Id 文件Id 下载 多个文件下载有问题
        /// </summary>
        /// <param name="userId"> userId</param>        
        /// <param name="fileId"> fileId</param>
        /// <returns></returns>
        public string DownLoadFile()
        {
            string jcbstr = Request["jcb"];
            string fileIds = Request["fileid"];
            //string verids = Request["verids"];
            //fileIds = "184";
            string[] idlist = fileIds.Split(',');

            if (idlist.Length > 0)
            {
                for (int i = 0; i < idlist.Length; i++)
                {
                    string filename = "";
                    string hashname = "";
                    string filepath = ZK.Common.ModelSettings.CreateFileDefaultPath + "/" + GetFilePathByFileID(idlist[i], out filename, out hashname) + "/" + hashname;
                    string otherWebFilePath = GetFileFromOtherWeb(Server.MapPath("../"), "ZK.MVCWeb", filepath);
                    //ZK.Common.DownLoad.DownLoadFile(Response, Server.MapPath(filepath), System.IO.Path.GetExtension(filename), filename);
                    ZK.Common.DownLoad.DownLoadFile(Response, otherWebFilePath, System.IO.Path.GetExtension(filename), filename);
                }

                return jcbstr + "({code : 1})";
            }
            else
            {
                return jcbstr + "({code : 0})";
            }

        }

        /// <summary>
        /// 根据 用户Id 文件Id 删除
        /// </summary>
        /// <param name="userId"> userId</param>        
        /// <param name="fileIds"> fileIds</param>
        /// </summary>
        public string DeleteFiles()
        {
            string jcbstr = Request["jcb"];
            string userID = Request["userID"];

            //userID="10040";
            string fileIds = Request["fileIds"];
            //fileIds = "145";
            string[] fileidlist = fileIds.Split(',');
            if (userID != null && userID != "")
            {
                DataSet dsUser = bllUser.GetList("userid=" + Convert.ToInt32(userID));
                List<Model.USERS> listUser = bllUser.DataTableToList(dsUser.Tables[0]);
                string userName = listUser[0].USERNAME;
                StringBuilder JsonString = new StringBuilder();
                DataSet dsNetUser = bllNetUser.GetList("user_name='" + userName + "'");
                if (dsNetUser.Tables[0].Rows.Count > 0)
                {
                    List<Model.miniyun_users> listNetUser = bllNetUser.DataTableToList(dsNetUser.Tables[0]);
                    int netUserId = listNetUser[0].id;

                    bool canRun = true;
                    DataSet ds = bllNetFile.GetList("user_id=" + netUserId);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        List<Model.miniyun_files> list = bllNetFile.DataTableToList(ds.Tables[0]);
                        for (int i = 0; i < list.Count; i++)
                        {
                            if (fileidlist.Length > 0)
                            {
                                for (int j = 0; j < fileidlist.Length; j++)
                                {
                                    if (list[i].id == Convert.ToInt32(fileidlist[j]))
                                    {
                                        bool MsgError = bllNetFile.Delete(Convert.ToInt32(fileidlist[j]));
                                        if (MsgError == true)
                                        {
                                            canRun = false;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                return jcbstr + "({code : 0})";
                            }
                        }
                        if (canRun)
                        {
                            return jcbstr + "({code : 0})";
                        }
                        else
                        {
                            return jcbstr + "({code : 1})";
                        }
                    }
                    else
                    {
                        return jcbstr + "({code : 0})";
                    }
                }
                else
                {
                    return jcbstr + "({code : 0})";
                }
            }
            else
            {
                return jcbstr + "({code : 0})";
            }
        }

        /// <summary>
        /// 根据 用户Id 获取文件或文件夹列表
        /// </summary>
        /// <param name="userId"> userId</param>
        /// <returns></returns>
        public string FileListInfo()
        {
            string jcbstr = Request["jcb"];
            try
            {
                string userID = Request["userID"];
                //userID = "10040";
                string folderId = Request["folderId"];
                //folderId = "1736";
                string strJson = "";
                if (userID != null && userID != "")
                {
                    DataSet dsUser = bllUser.GetList("userid=" + Convert.ToInt32(userID));
                    List<Model.USERS> listUser = bllUser.DataTableToList(dsUser.Tables[0]);
                    string userName = listUser[0].USERNAME;
                    StringBuilder JsonString = new StringBuilder();
                    DataSet dsNetUser = bllNetUser.GetList("user_name='" + userName + "'");
                    if (dsNetUser.Tables[0].Rows.Count > 0)
                    {
                        List<Model.miniyun_users> listNetUser = bllNetUser.DataTableToList(dsNetUser.Tables[0]);
                        int netUserId = listNetUser[0].id;
                        string strwhere = "1=1";
                        if (folderId == null || folderId == "")
                        {
                            strwhere += " and user_id=" + netUserId + "  and parent_file_id=0 order by file_type desc";
                        }
                        else
                        {
                            Model.miniyun_files mdlFileInfo = bllNetFile.GetModel(Convert.ToInt32(folderId));
                            string filename = mdlFileInfo.file_name;
                            int fileType = mdlFileInfo.file_type;
                            int pid = mdlFileInfo.parent_file_id;

                            if (fileType == 3)
                            {
                                Model.miniyun_files mdlParentFile = bllNetFile.GetModel(pid);
                                string folderName = mdlParentFile.file_name;
                                string[] strname = folderName.Split('(');
                                string shareUser = strname[1].Remove(strname[1].Length-2,1);
                                string shareId = GetUserId(shareUser);
                                List<Model.miniyun_files> listShare = bllNetFile.GetModelList("userid=" + Convert.ToInt32(shareId) + " and file_name=" + strname[0]);
                                if (listShare != null && listShare.Count > 0)
                                {
                                    strwhere += " and user_id=" + listShare[0].user_id + "  and parent_file_id=" + listShare[0].id;
                                }
                            }
                            else
                            {
                                strwhere += " and user_id=" + netUserId + "  and parent_file_id=" + Convert.ToInt32(folderId);
                            }
                        }
                        DataSet dsFilelist = bllNetFile.GetList(strwhere);
                        if (dsFilelist.Tables[0].Rows.Count > 0)
                        {
                            strJson = CreateJsonFilesList(dsFilelist.Tables[0]);
                            return jcbstr + "(" + strJson + ")";
                        }
                        else
                        {
                            return jcbstr + "({})";
                        }
                    }
                    else
                    {
                        return jcbstr + "({})";
                    }
                }
                else
                {
                    return jcbstr + "({})";
                }

            }
            catch
            {
                return jcbstr + "({})";
            }
        }

        /// <summary>
        /// 根据 用户Id 文件夹名称 文件夹标识  新建文件夹
        /// </summary>
        /// <param name="userId"> userId</param>
        /// <param name="folderId"> folderId</param>
        /// <param name="folderName"> folderName</param>
        /// <returns></returns>
        public string NewFolderInfo()
        {
            string jcbstr = Request["jcb"];
            try
            {
                string userId = Request["userID"];
                string folderId = Request["folderId"];
                string folderName = Request["folderName"];
                string hashpath = "";
                string strResult = "0";
                if (userId != null && userId != "")
                {
                    DataSet dsUser = bllUser.GetList("userid=" + Convert.ToInt32(userId));
                    List<Model.USERS> listUser = bllUser.DataTableToList(dsUser.Tables[0]);
                    string userName = listUser[0].USERNAME;

                    DataSet dsNetUser = bllNetUser.GetList("user_name='" + userName + "'");
                    if (dsNetUser.Tables[0].Rows.Count > 0)
                    {
                        List<Model.miniyun_users> listNetUser = bllNetUser.DataTableToList(dsNetUser.Tables[0]);
                        int netUserId = listNetUser[0].id;

                        strResult = AddNewFile(netUserId.ToString(), folderName, 0, folderId, "", false, out hashpath);
                    }
                }

                return jcbstr + "({code : \"" + strResult + "\"})";
            }
            catch
            {
                return jcbstr + "({code : 0})";
            }
        }

        #region 共享
        /// <summary>
        /// 根据 当前用户Id 共享文件夹id 共享用户id  共享文件夹
        /// </summary>
        /// <param name="userId"> userId</param>
        /// <param name="shareUserIds"> shareUserIds</param>
        /// <param name="folderIds"> folderIds</param>
        /// <returns></returns>
        public string ShareFolderInfo()
        {
            #region 注释
            //string jcbstr = Request["jcb"];
            //try
            //{
            //    #region
            //    string userId = Request["userID"];
            //    string shareUserIds = Request["shareUserIds"];
            //    string folderIds = Request["folderIds"];
            //    //userId = "10042";
            //    //folderIds = "222,223,217";
            //    //shareUserIds = "10011,10022,10024";
            //    string[] shareUserIdslist = shareUserIds.Split(',');
            //    string[] folderIdslist = folderIds.Split(',');

            //    string hashpath = "";
            //    string strResult = "0";
            //    if (userId != null && userId != "")
            //    {
            //        DataSet dsUser = bllUser.GetList("userid=" + Convert.ToInt32(userId));
            //        List<Model.USERS> listUser = bllUser.DataTableToList(dsUser.Tables[0]);
            //        string userName = listUser[0].USERNAME;

            //        DataSet dsNetUser = bllNetUser.GetList("user_name='" + userName + "'");
            //        if (dsNetUser.Tables[0].Rows.Count > 0)
            //        {
            //            List<Model.miniyun_users> listNetUser = bllNetUser.DataTableToList(dsNetUser.Tables[0]);
            //            int netUserId = listNetUser[0].id;
            //            string netUserName = listNetUser[0].user_name;

            //            for (int i = 0; i < folderIdslist.Length; i++)
            //            {
            //                string strWhere = "user_id=" + netUserId + " and id=" + Convert.ToInt32(folderIdslist[i]) + " and file_type in (0,1,2)";
            //                DataSet dsfolder = bllNetFile.GetList(strWhere);
            //                if (dsfolder.Tables[0].Rows.Count > 0)
            //                {
            //                    List<Model.miniyun_files> folderList = bllNetFile.DataTableToList(dsfolder.Tables[0]);
            //                    string folderName = folderList[0].file_name;
            //                    int folderFlag = folderList[0].file_type;
            //                    long fileSize = folderList[0].file_size;

            //                    for (int j = 0; j < shareUserIdslist.Length; j++)
            //                    {
            //                        int ss = Convert.ToInt32(shareUserIdslist[j]);
            //                        int shareUserId = ChangeUserId(ss);
            //                        //检查是否已经建立了共享目标文件夹，如果没有，则建立并返回ID，如果已经建立，则取出其ID；
            //                        int shareFolderId = GetShareFolderId(shareUserId, userName);

            //                        if (folderFlag == 0)
            //                        {
            //                            strResult = AddNewShareFile(shareUserId.ToString(), folderName, fileSize, shareFolderId.ToString(), "", true, "", out hashpath);
            //                        }
            //                        if (folderFlag == 1)
            //                        {
            //                            strResult = AddNewShareFile(shareUserId.ToString(), folderName, 0, shareFolderId.ToString(), "", false, "", out hashpath);
            //                        }
            //                        #region
            //                        ZK.BLL.miniyun_files bll_miniyun_files = new ZK.BLL.miniyun_files();
            //                        ZK.BLL.miniyun_user_privilege bll_miniyun_user_privilege = new ZK.BLL.miniyun_user_privilege();
            //                        DateTime time = DateTime.Now;
            //                        if (strResult == "1")
            //                        {
            //                            ZK.Model.miniyun_files mdl_miniyunfiles = new ZK.Model.miniyun_files();
            //                            mdl_miniyunfiles = bll_miniyun_files.GetModel(netUserId);
            //                            mdl_miniyunfiles.file_type = 2;
            //                            mdl_miniyunfiles.updated_at = time;
            //                            bool bools1 = bll_miniyun_files.Update(mdl_miniyunfiles);

            //                            string permission = "a:9:{s:13:\"resource.read\";i:1;s:13:\"folder.create\";i:0;s:13:\"folder.rename\";i:0;s:13:\"folder.delete\";i:0;s:11:\"file.create\";i:0;s:11:\"file.modify\";i:0;s:11:\"file.rename\";i:0;s:11:\"file.delete\";i:0;s:16:\"permission.grant\";i:0;}";
            //                            ZK.Model.miniyun_user_privilege mdl_miniyun_user_privilege = new ZK.Model.miniyun_user_privilege();
            //                            mdl_miniyun_user_privilege.user_id = shareUserId;
            //                            mdl_miniyun_user_privilege.file_path = mdl_miniyunfiles.file_path;
            //                            mdl_miniyun_user_privilege.permission = permission;
            //                            mdl_miniyun_user_privilege.created_at = time;
            //                            mdl_miniyun_user_privilege.updated_at = time;

            //                            bool bools2 = bll_miniyun_user_privilege.Add(mdl_miniyun_user_privilege);

            //                            if (bools1 == true && bools2 == true)
            //                            {
            //                                strResult = "1";
            //                            }
            //                        }
            //                        if (strResult == "共享已存在")
            //                        {
            //                            strResult = "1";
            //                        }
            //                        #endregion
            //                    }
            //                }

            //            }
            //        }
            //    }
            //    return jcbstr + "({code : \"" + strResult + "\"})";
            //    #endregion

            //}
            //catch
            //{
            //    return jcbstr + "({code : 0})";
            //}
            #endregion

            string jcbstr = Request["jcb"];
            try
            {
                string userId = Request["userID"];
                string shareUserIds = Request["shareUserIds"];
                string folderIds = Request["folderIds"];
                //userId = "10040";
                //folderIds = "131";
                //shareUserIds = "10022";
                string[] shareUserIdslist = shareUserIds.Split(',');

                string default_permission = "";
                #region 默认权限
                ZK.BLL.miniyun_options bll_miniyun_options = new ZK.BLL.miniyun_options();
                List<ZK.Model.miniyun_options> list_miniyun_options = bll_miniyun_options.GetModelList("option_name='default_permission'");
                if (list_miniyun_options.Count > 0)
                {
                    string str3 = list_miniyun_options[0].option_value;
                    string str4 = str3.Replace("i:", "#");
                    string[] defautpemissionlist = str4.Split('#');
                    default_permission = "_" + defautpemissionlist[1].Substring(0, 1) + "_" + defautpemissionlist[2].Substring(0, 1) + "_" + defautpemissionlist[3].Substring(0, 1) + "_" + defautpemissionlist[4].Substring(0, 1) + "_" + defautpemissionlist[5].Substring(0, 1) + "_" + defautpemissionlist[6].Substring(0, 1) + "_" + defautpemissionlist[6].Substring(0, 1) + "_" + defautpemissionlist[8].Substring(0, 1) + "_" + defautpemissionlist[9].Substring(0, 1);
                }
                string shareIdsAndGrants = "";
                for (int i = 0; i < shareUserIdslist.Length; i++)
                {
                    shareIdsAndGrants += shareUserIdslist[i] + default_permission;
                    if (i < shareUserIdslist.Length - 1)
                    {
                        shareIdsAndGrants += ",";
                    }
                }

                #endregion

                string[] folderIdslist = folderIds.Split(',');
                string strResult = "0";
                #region
                if (userId != null && userId != "")
                {
                    DataSet dsUser = bllUser.GetList("userid=" + Convert.ToInt32(userId));
                    List<Model.USERS> listUser = bllUser.DataTableToList(dsUser.Tables[0]);
                    string userName = listUser[0].USERNAME;

                    DataSet dsNetUser = bllNetUser.GetList("user_name='" + userName + "'");
                    if (dsNetUser.Tables[0].Rows.Count > 0)
                    {
                        List<Model.miniyun_users> listNetUser = bllNetUser.DataTableToList(dsNetUser.Tables[0]);
                        int netUserId = listNetUser[0].id;
                        string netUserName = listNetUser[0].user_name;

                        for (int i = 0; i < folderIdslist.Length; i++)
                        {
                            ZK.Model.miniyun_files mdlminiyunfile = bll_miniyun_files.GetModel(Convert.ToInt32(folderIdslist[i]));
                            if (mdlminiyunfile != null)
                            {
                                int folderFlag = mdlminiyunfile.file_type;

                                if (folderFlag == 0)
                                {
                                    strResult = ShareFileAction(netUserId.ToString(), netUserName, shareUserIds, folderIdslist[i]);
                                }
                                if (folderFlag == 1)
                                {                                    
                                    strResult = ShareFolderAction(netUserId.ToString(), netUserName, shareIdsAndGrants, folderIdslist[i]);
                                }
                            }
                        }
                    }
                }
                #endregion

                return jcbstr + "({code : \"" + strResult + "\"})";
            }
            catch
            {
                return jcbstr + "({code : 0})";
            }
        }

        public string sharefolderinfo1()
        {
            string jcbstr = Request["jcb"];
            try
            {
                string userId = Request["userID"];
                string shareUserIds = Request["shareUserIds"];
                string folderIds = Request["folderIds"];
                //userId = "10042";
                //folderIds = "222,223,217";
                //shareUserIds = "10011,10022,10024";
                string[] shareUserIdslist = shareUserIds.Split(',');
                
                string default_permission = "";
                #region 默认权限
                ZK.BLL.miniyun_options bll_miniyun_options = new ZK.BLL.miniyun_options();
                List<ZK.Model.miniyun_options> list_miniyun_options = bll_miniyun_options.GetModelList("option_name='default_permission'");
                if (list_miniyun_options.Count > 0)
                {
                    string str3 = list_miniyun_options[0].option_value;
                    string str4 = str3.Replace("i:", "#");
                    string[] defautpemissionlist = str4.Split('#');
                    default_permission = "_" + defautpemissionlist[1].Substring(0, 1) + "_" + defautpemissionlist[2].Substring(0, 1) + "_" + defautpemissionlist[3].Substring(0, 1) + "_" + defautpemissionlist[4].Substring(0, 1) + "_" + defautpemissionlist[5].Substring(0, 1) + "_" + defautpemissionlist[6].Substring(0, 1) + "_" + defautpemissionlist[6].Substring(0, 1) + "_" + defautpemissionlist[8].Substring(0, 1) + "_" + defautpemissionlist[9].Substring(0, 1);
                }
                string shareIdsAndGrants = "";
                for (int i = 0; i < shareUserIdslist.Length; i++)
                {
                    shareIdsAndGrants += shareUserIdslist[i] + "_" + default_permission;
                    if (i < shareUserIdslist.Length - 1)
                    {
                        shareIdsAndGrants += ",";
                    }
                }

                #endregion

                string[] folderIdslist = folderIds.Split(',');
                string strResult = "0";
                #region
                if (userId != null && userId != "")
                {
                    DataSet dsUser = bllUser.GetList("userid=" + Convert.ToInt32(userId));
                    List<Model.USERS> listUser = bllUser.DataTableToList(dsUser.Tables[0]);
                    string userName = listUser[0].USERNAME;

                    DataSet dsNetUser = bllNetUser.GetList("user_name='" + userName + "'");
                    if (dsNetUser.Tables[0].Rows.Count > 0)
                    {
                        List<Model.miniyun_users> listNetUser = bllNetUser.DataTableToList(dsNetUser.Tables[0]);
                        int netUserId = listNetUser[0].id;
                        string netUserName = listNetUser[0].user_name;

                        for (int i = 0; i < folderIdslist.Length; i++)
                        {
                            ZK.Model.miniyun_files mdlminiyunfile = bll_miniyun_files.GetModel(Convert.ToInt32(folderIdslist[i]));
                            if (mdlminiyunfile != null)
                            {
                                int folderFlag = mdlminiyunfile.file_type;
                                
                                if (folderFlag == 0)
                                {
                                    strResult = ShareFolderAction(netUserId.ToString(), netUserName, shareIdsAndGrants, folderIdslist[i]);
                                }
                                if (folderFlag == 1)
                                {
                                    strResult = ShareFileAction(userId, userName, shareIdsAndGrants, folderIds);
                                }
                            }
                        }
                    }
                }
                #endregion

                return jcbstr + "({code : \"" + strResult + "\"})";
            }
            catch
            {
                return jcbstr + "({code : 0})";
            }
        }

        /// <summary>
        /// 文件夹的共享
        /// </summary>
        /// <param name="userId">当前用户Id</param>
        /// <param name="shareIdsAndGrants">目标用户Ids及权限</param>
        /// <param name="forlderId">文件夹Id</param>
        /// <returns></returns>
        public string ShareFolderAction(string userId, string userName, string shareIdsAndGrants, string forlderId)
        {
            try
            {
                string strResult = "0";

                #region 共享删除
                ZK.Model.miniyun_files mdl = bll_miniyun_files.GetModel(Convert.ToInt32(forlderId));
                if (mdl != null)
                {
                    string pathDel = "file_name='" + mdl.file_name + "(" + userName + ")'";

                    List<ZK.Model.miniyun_files> lissdels = bll_miniyun_files.GetModelList(pathDel);
                    if (lissdels.Count > 0)
                    {

                        for (int j = 0; j < lissdels.Count; j++)
                        {
                            int userid1 = lissdels[j].user_id;
                            int iddel = lissdels[j].id;
                            string delPath = lissdels[j].file_path;
                            string eventValue = lissdels[j].event_uuid;
                            #region 删除表miniyun_files
                            bool error1 = bll_miniyun_files.Delete(iddel);

                            #endregion

                            #region 删除表miniyun_events
                            string strw1 = "event_uuid='" + eventValue + "'";
                            List<ZK.Model.miniyun_events> listEvents = bll_miniyun_events.GetModelList(strw1);
                            if (listEvents.Count > 0)
                            {
                                int eventId = listEvents[0].id;
                                bool error2 = bll_miniyun_events.Delete(eventId);
                            }
                            #endregion

                            #region 删除表miniyun_file_metas
                            string strw2 = "file_path='" + delPath + "'";
                            List<ZK.Model.miniyun_file_metas> listmetas = bll_miniyun_file_metas.GetModelList(strw2);
                            if (listmetas.Count > 0)
                            {
                                int metasId = listmetas[0].id;
                                bool error3 = bll_miniyun_file_metas.Delete(metasId);
                            }
                            #endregion

                            #region 删除表miniyun_user_privilege
                            string strw3 = "user_id=" + userid1 + " and file_path='" + mdl.file_path + "'";
                            List<ZK.Model.miniyun_user_privilege> listprivilege = bll_miniyun_user_privilege.GetModelList(strw3);
                            if (listprivilege.Count > 0)
                            {
                                int privilegeId = listprivilege[0].id;
                                bool error4 = bll_miniyun_user_privilege.Delete(privilegeId);
                            }

                            #endregion
                        }
                    }

                    mdl.file_type = 1;
                    bool boolss = bll_miniyun_files.Update(mdl);
                    if (boolss == true)
                    {
                        strResult = "2";
                    }
                }
                #endregion

                #region 最新代码
                if (shareIdsAndGrants != null && shareIdsAndGrants != "")
                {
                    #region 参数处理
                    string[] targetUserList = shareIdsAndGrants.Split(',');//取得目标用户id和权限列表

                    int num = 0;//计数器
                    string metaValue2 = "";
                    DateTime time = DateTime.Now;

                    string toUserId = "";
                    string permission = "0";
                    string read = "0";
                    string folderCreat = "0";
                    string folderDel = "0";
                    string folderRename = "0";
                    string fileCreat = "0";
                    string fileModify = "0";
                    string fileDel = "0";
                    string fileRename = "0";
                    string grant = "0";

                    #endregion

                    //等到共享文件或文件夹信息
                    string shareForldName = "";
                    string eventspath = "";
                    string strWhere = "user_id=" + Convert.ToInt32(userId) + " and id=" + Convert.ToInt32(forlderId);
                    DataSet dsFile = bll_miniyun_files.GetList(strWhere);
                    if (dsFile.Tables[0].Rows.Count > 0)
                    {
                        List<ZK.Model.miniyun_files> listFile = bll_miniyun_files.DataTableToList(dsFile.Tables[0]);
                        shareForldName = listFile[0].file_name;
                        folderId = listFile[0].id;


                        //建立共享文件夹
                        for (int i = 0; i < targetUserList.Length; i++)
                        {
                            num++;
                            string tagetUserId = targetUserList[i];
                            string[] toUserInfo = tagetUserId.Split('_');
                            toUserId = toUserInfo[0];
                            int netToUserId = ChangeUserId(Convert.ToInt32(toUserId));

                            read = toUserInfo[1];
                            folderCreat = toUserInfo[2];
                            folderRename = toUserInfo[3];
                            folderDel = toUserInfo[4];
                            fileCreat = toUserInfo[5];
                            fileModify = toUserInfo[6];
                            fileRename = toUserInfo[7];
                            fileDel = toUserInfo[8];
                            grant = toUserInfo[9];
                            permission = "a:9:{s:13:\"resource.read\";i:" + read + ";s:13:\"folder.create\";i:" + folderCreat + ";s:13:\"folder.rename\";i:" + folderRename + ";s:13:\"folder.delete\";i:" + folderDel + ";s:11:\"file.create\";i:" + fileCreat + ";s:11:\"file.modify\";i:" + fileModify + ";s:11:\"file.rename\";i:" + fileRename + ";s:11:\"file.delete\";i:" + fileDel + ";s:16:\"permission.grant\";i:" + grant + ";}";

                            strResult = AddNewFolder(Convert.ToInt32(userId), netToUserId.ToString(), shareForldName, 0, "0", userName, out eventspath);
                            if (strResult == "1")
                            {

                                #region 操作表miniyun_file_metas
                                //处理表中字段meta_value
                                ZK.Model.miniyun_files parentmodel = bll_miniyun_files.GetModel(Convert.ToInt32(folderId));//获取分享人文件夹信息

                                string strSql0 = "event_uuid='" + eventspath + "'";
                                List<ZK.Model.miniyun_files> listfilePath = bll_miniyun_files.GetModelList(strSql0);//获取被分享人文件夹信息

                                string metavalue = "";
                                string metaValue1 = "a:3:{s:6:\"master\";i:" + parentmodel.user_id + ";s:6:\"slaves\";a:" + num + ":{";

                                metaValue2 += "i:" + listfilePath[0].user_id + ";s:" + System.Text.Encoding.UTF8.GetBytes(listfilePath[0].file_path).Length + ":\"" + listfilePath[0].file_path + "\";";

                                string metaValue3 = "}s:4:\"path\";s:" + System.Text.Encoding.UTF8.GetBytes(parentmodel.file_path).Length + ":\"" + parentmodel.file_path + "\";}";

                                metavalue = metaValue1 + metaValue2 + metaValue3;

                                #endregion

                                #region 向表miniyun_file_metas添加共享人信息
                                string files_metasPath1 = parentmodel.file_path;
                                ZK.Model.miniyun_file_metas mdl_miniyun_file_metas1 = new ZK.Model.miniyun_file_metas();
                                string strSql1 = "file_path='" + files_metasPath1 + "'";
                                DataSet dsfiles_metas1 = bll_miniyun_file_metas.GetList(strSql1);
                                if (dsfiles_metas1.Tables[0].Rows.Count > 0)
                                {
                                    List<ZK.Model.miniyun_file_metas> listfileMetas = bll_miniyun_file_metas.DataTableToList(dsfiles_metas1.Tables[0]);
                                    mdl_miniyun_file_metas1.file_path = listfileMetas[0].file_path;
                                    mdl_miniyun_file_metas1.meta_key = listfileMetas[0].meta_key;
                                    mdl_miniyun_file_metas1.meta_value = metavalue;
                                    mdl_miniyun_file_metas1.created_at = listfileMetas[0].created_at;
                                    mdl_miniyun_file_metas1.updated_at = time;

                                    bll_miniyun_file_metas.Update(mdl_miniyun_file_metas1);
                                }
                                else
                                {
                                    mdl_miniyun_file_metas1.file_path = files_metasPath1;
                                    mdl_miniyun_file_metas1.meta_key = "shared_folders";
                                    mdl_miniyun_file_metas1.meta_value = metavalue;
                                    mdl_miniyun_file_metas1.created_at = time;
                                    mdl_miniyun_file_metas1.updated_at = time;
                                    bll_miniyun_file_metas.Add(mdl_miniyun_file_metas1);
                                }
                                #endregion

                                #region 向表miniyun_file_metas添加被共享人信息
                                string files_metasPath = listfilePath[0].file_path;
                                ZK.Model.miniyun_file_metas mdl_miniyun_file_metas = new ZK.Model.miniyun_file_metas();
                                string strSql = "file_path='" + files_metasPath + "'";
                                DataSet dsfiles_metas = bll_miniyun_file_metas.GetList(strSql);
                                if (dsfiles_metas.Tables[0].Rows.Count > 0)
                                {
                                    List<ZK.Model.miniyun_file_metas> listfileMetas = bll_miniyun_file_metas.DataTableToList(dsfiles_metas.Tables[0]);
                                    mdl_miniyun_file_metas.file_path = listfileMetas[0].file_path;
                                    mdl_miniyun_file_metas.meta_key = listfileMetas[0].meta_key;
                                    mdl_miniyun_file_metas.meta_value = metavalue;
                                    mdl_miniyun_file_metas.created_at = listfileMetas[0].created_at;
                                    mdl_miniyun_file_metas.updated_at = time;

                                    bll_miniyun_file_metas.Update(mdl_miniyun_file_metas);
                                }
                                else
                                {
                                    mdl_miniyun_file_metas.file_path = files_metasPath;
                                    mdl_miniyun_file_metas.meta_key = "shared_folders";
                                    mdl_miniyun_file_metas.meta_value = metavalue;
                                    mdl_miniyun_file_metas.created_at = time;
                                    mdl_miniyun_file_metas.updated_at = time;
                                    bll_miniyun_file_metas.Add(mdl_miniyun_file_metas);
                                }
                                #endregion

                                #region 操作表miniyun_user_privilege 文件及文件夹权限
                                ZK.Model.miniyun_files mdl_miniyunfiles = new ZK.Model.miniyun_files();
                                mdl_miniyunfiles = bll_miniyun_files.GetModel(listFile[0].id);
                                ZK.Model.miniyun_user_privilege mdl_miniyun_user_privilege = new ZK.Model.miniyun_user_privilege();
                                mdl_miniyun_user_privilege.user_id = netToUserId;
                                mdl_miniyun_user_privilege.file_path = mdl_miniyunfiles.file_path;
                                mdl_miniyun_user_privilege.permission = permission;
                                mdl_miniyun_user_privilege.created_at = time;
                                mdl_miniyun_user_privilege.updated_at = time;

                                bool bools2 = bll_miniyun_user_privilege.Add(mdl_miniyun_user_privilege);
                                #endregion

                                if (bools2 == true)
                                {
                                    strResult = "1";
                                }
                            }
                            if (strResult == "共享已存在")
                            {
                                #region 操作表miniyun_user_privilege 文件及文件夹权限
                                ZK.Model.miniyun_files mdl_miniyunfiles = new ZK.Model.miniyun_files();
                                mdl_miniyunfiles = bll_miniyun_files.GetModel(listFile[0].id);
                                string strsql = "user_id=" + netToUserId + " and file_path='" + mdl_miniyunfiles.file_path + "'";
                                List<ZK.Model.miniyun_user_privilege> list_miniyun_user_privilege = bll_miniyun_user_privilege.GetModelList(strsql);
                                if (list_miniyun_user_privilege.Count > 0)
                                {
                                    int permissionId = list_miniyun_user_privilege[0].id;
                                    ZK.Model.miniyun_user_privilege mdl_miniyun_user_privilege = bll_miniyun_user_privilege.GetModel(permissionId);
                                    mdl_miniyun_user_privilege.permission = permission;
                                    mdl_miniyun_user_privilege.updated_at = time;

                                    bool bools2 = bll_miniyun_user_privilege.Update(mdl_miniyun_user_privilege);

                                    if (bools2 == true)
                                    {
                                        strResult = "1";
                                    }
                                }
                                #endregion
                            }
                        }
                    }

                    #region 共享文件夹成功后，发送通知
                    if (strResult == "1")
                    {
                        string username = GetUserName(Convert.ToInt32(userId));
                        string userIds = "";
                        for (int i = 0; i < targetUserList.Length; i++)
                        {
                            string tagetUserId = targetUserList[i];
                            string[] toUserInfo = tagetUserId.Split('_');
                            string toUserId1 = toUserInfo[0];
                            userIds += toUserId1;
                            if (i < targetUserList.Length - 1)
                            {
                                userIds += ",";
                            }
                        }
                        string str = SendShareInfo(username, "1", userIds);
                        if (str == "1")
                        {
                            strResult = "1";
                        }
                        else
                        {
                            return "共享文件通知发送失败！";
                        }
                    }
                    #endregion
                }

                if (strResult == "2")
                {
                    strResult = "1";
                }
                return strResult;
                #endregion

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        /// <summary>
        /// 文件的共享
        /// </summary>
        /// <param name="userId">当前用户Id</param>
        /// <param name="forlderId">文件Id</param>
        /// <returns></returns>
        public string ShareFileAction(string userId, string userName, string shareIdsAndGrants, string fileId)
        {
            try
            {
                #region 参数处理
                string[] toUserId = shareIdsAndGrants.Split(',');//取得目标用户id和权限列表
                string strResult = "0";

                DateTime time = DateTime.Now;
                DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                DateTime dtNow = DateTime.Parse(DateTime.Now.ToString());
                TimeSpan toNow = dtNow.Subtract(dtStart);
                string timeStamp = toNow.Ticks.ToString();
                timeStamp = timeStamp.Substring(0, timeStamp.Length - 7);
                long timeLong = long.Parse(timeStamp);
                #endregion

                //得到共享文件或文件夹信息
                string shareFileName = "";
                long fileSize = 0;
                string mime_type = "";

                ZK.Model.miniyun_files mdl_miniyun_files = bll_miniyun_files.GetModel(Convert.ToInt32(fileId));
                if (mdl_miniyun_files != null)
                {
                    shareFileName = mdl_miniyun_files.file_name;
                    fileSize = mdl_miniyun_files.file_size;
                    mime_type = mdl_miniyun_files.mime_type;

                    ZK.Model.miniyun_file_versions mdl_miniyun_file_versions = bll_miniyun_file_versions.GetModel(mdl_miniyun_files.version_id);
                    int version_id = mdl_miniyun_files.version_id;
                    string versionId = "";
                    if (mdl_miniyun_file_versions != null)
                    {
                        versionId = mdl_miniyun_file_versions.file_signature;
                    }

                    //建立共享文件夹
                    for (int i = 0; i < toUserId.Length; i++)
                    {
                        string tagetUserId = toUserId[i];

                        int netToUserId = ChangeUserId(Convert.ToInt32(tagetUserId));
                        ZK.Model.miniyun_users mdlUsers = bll_miniyun_users.GetModel(netToUserId);

                        string event_uuid = System.Guid.NewGuid().ToString();

                        //检查是否已经建立了共享目标文件夹，如果没有，则建立并返回ID，如果已经建立，则取出其ID；
                        int shareFolderId = GetShareFolderId(netToUserId, userName);
                        ZK.Model.miniyun_files mdlfile = bll_miniyun_files.GetModel(shareFolderId);

                        string strSql = "parent_file_id=" + shareFolderId + " and user_id=" + netToUserId + " and file_name='" + shareFileName + "'";
                        List<ZK.Model.miniyun_files> list_miniyun_files = bll_miniyun_files.GetModelList(strSql);
                        //判断共享文件是否存在
                        if (list_miniyun_files.Count > 0)//已有共享文件
                        {
                            int isdelete = list_miniyun_files[0].is_deleted;
                            if (isdelete == 1)//共享文件已删除
                            {
                                #region 更新表miniyun_files
                                ZK.Model.miniyun_files mdl_miniyun_files2 = bll_miniyun_files.GetModel(list_miniyun_files[0].id);
                                mdl_miniyun_files2.is_deleted = 0;
                                mdl_miniyun_files2.updated_at = time;
                                mdl_miniyun_files2.file_create_time = timeLong;
                                mdl_miniyun_files2.event_uuid = event_uuid;
                                mdl_miniyun_files2.version_id = version_id;
                                mdl_miniyun_files2.file_size = fileSize;

                                bool msgError = bll_miniyun_files.Update(mdl_miniyun_files2);
                                #endregion

                                #region 添加表miniyun_events
                                ZK.Model.miniyun_events mdl_miniyun_events2 = new ZK.Model.miniyun_events();
                                mdl_miniyun_events2.user_id = netToUserId;
                                mdl_miniyun_events2.user_device_id = netToUserId;
                                mdl_miniyun_events2.action = 3;
                                mdl_miniyun_events2.file_path = mdl_miniyun_files2.file_path;
                                string context = "a:5:{s:4:\"hash\";s:40:\"" + versionId + "\";s:3:\"rev\";i:" + mdl_miniyun_files.version_id + ";s:5:\"bytes\";i:" + fileSize + ";s:11:\"update_time\";i:" + timeLong + ";s:11:\"create_time\";i:" + timeLong + ";}";
                                mdl_miniyun_events2.context = context;
                                mdl_miniyun_events2.event_uuid = event_uuid;
                                mdl_miniyun_events2.created_at = time;
                                mdl_miniyun_events2.updated_at = time;

                                bool msgError1 = bll_miniyun_events.Add(mdl_miniyun_events2);
                                #endregion

                                if (msgError && msgError1)
                                {
                                    strResult = "1";
                                }
                                else
                                {
                                    return "共享失败！";
                                }
                            }
                            else
                            {
                                #region 更新表miniyun_files

                                ZK.Model.miniyun_files mdl_miniyun_files3 = bll_miniyun_files.GetModel(list_miniyun_files[0].id);
                                mdl_miniyun_files3.is_deleted = 0;
                                mdl_miniyun_files3.updated_at = time;
                                mdl_miniyun_files3.file_create_time = timeLong;
                                mdl_miniyun_files3.event_uuid = event_uuid;
                                mdl_miniyun_files3.version_id = version_id;
                                mdl_miniyun_files3.file_size = fileSize;

                                bool msgError = bll_miniyun_files.Update(mdl_miniyun_files3);
                                #endregion

                                #region 添加表miniyun_events
                                ZK.Model.miniyun_events mdl_miniyun_events3 = new ZK.Model.miniyun_events();
                                mdl_miniyun_events3.user_id = netToUserId;
                                mdl_miniyun_events3.user_device_id = netToUserId;
                                mdl_miniyun_events3.action = 4;
                                mdl_miniyun_events3.file_path = mdl_miniyun_files3.file_path;
                                string context = "a:5:{s:4:\"hash\";s:40:\"" + versionId + "\";s:3:\"rev\";i:" + mdl_miniyun_files.version_id + ";s:5:\"bytes\";i:" + fileSize + ";s:11:\"update_time\";i:" + timeLong + ";s:11:\"create_time\";i:" + timeLong + ";}";
                                mdl_miniyun_events3.context = context;
                                mdl_miniyun_events3.event_uuid = event_uuid;
                                mdl_miniyun_events3.created_at = time;
                                mdl_miniyun_events3.updated_at = time;

                                bool msgError1 = bll_miniyun_events.Add(mdl_miniyun_events3);
                                #endregion

                                #region 更新表miniyun_files_metas
                                List<ZK.Model.miniyun_file_metas> listfile_metas = bll_miniyun_file_metas.GetModelList("file_path='" + list_miniyun_files[0].file_path + "'");
                                ZK.Model.miniyun_file_metas mdl_file_metas = bll_file_metas.GetModel(listfile_metas[0].id);
                                string metas_value = listfile_metas[0].meta_value;

                                #region 截取version_id 的list
                                string metas1 = metas_value.Replace("\"version_id\"", "#");
                                string[] metas2 = metas1.Split('#');
                                List<string> list1 = new List<string>();
                                for (int k = 1; k < metas2.Length; k++)
                                {
                                    string[] versionIds = metas2[k].Split('"');
                                    list1.Add(versionIds[1]);
                                }
                                #endregion

                                bool isVersionQ = true;
                                for (int m = 0; m < list1.Count; m++)
                                {
                                    //判断version_id是否一致
                                    if (version_id == Convert.ToInt32(list1[m]))
                                    {
                                        isVersionQ = false;
                                        break;
                                    }
                                }

                                #region 版本不一致
                                if (isVersionQ)
                                {
                                    string metas_value1 = metas_value.Remove(metas_value.Length - 2, 1);
                                    string aa = metas_value.Substring(5, metas_value.Length - 5);
                                    metas_value1 = aa.Remove(aa.Length - 2, 1);
                                    string metas_valueMiddle = "";
                                    string metas_valueLeft = "";
                                    int flagnum = Convert.ToInt32(metas_value.Substring(2, 1));
                                    switch (flagnum)
                                    {
                                        case 1:
                                            metas_valueLeft = "a:2:{";
                                            metas_valueMiddle = "i:1;a:7:{s:4:\"type\";i:4;s:10:\"version_id\";s:" + System.Text.Encoding.UTF8.GetBytes(mdl_miniyun_files.version_id.ToString()).Length + ":\"" + version_id + "\";s:7:\"user_id\";s:" + System.Text.Encoding.UTF8.GetBytes(netToUserId.ToString()).Length + ":\"" + netToUserId + "\";s:9:\"user_nick\";s:" + System.Text.Encoding.UTF8.GetBytes(mdlUsers.user_name).Length + ":\"" + mdlUsers.user_name + "\";s:11:\"device_name\";s:" + System.Text.Encoding.UTF8.GetBytes(mdlUsers.user_name).Length + ":\"" + mdlUsers.user_name + "\";s:9:\"file_size\";i:\"" + fileSize + "\";s:8:\"datetime\";s:19:\"" + time + "\";}";
                                            break;
                                        case 2:
                                            metas_valueLeft = "a:3:{";
                                            metas_valueMiddle = "i:2;a:7:{s:4:\"type\";i:4;s:10:\"version_id\";s:" + System.Text.Encoding.UTF8.GetBytes(mdl_miniyun_files.version_id.ToString()).Length + ":\"" + version_id + "\";s:7:\"user_id\";s:" + System.Text.Encoding.UTF8.GetBytes(netToUserId.ToString()).Length + ":\"" + netToUserId + "\";s:9:\"user_nick\";s:" + System.Text.Encoding.UTF8.GetBytes(mdlUsers.user_name).Length + ":\"" + mdlUsers.user_name + "\";s:11:\"device_name\";s:" + System.Text.Encoding.UTF8.GetBytes(mdlUsers.user_name).Length + ":\"" + mdlUsers.user_name + "\";s:9:\"file_size\";i:\"" + fileSize + "\";s:8:\"datetime\";s:19:\"" + time + "\";}";
                                            break;
                                        case 3:
                                            metas_valueLeft = "a:4:{";
                                            metas_valueMiddle = "i:3;a:7:{s:4:\"type\";i:4;s:10:\"version_id\";s:" + System.Text.Encoding.UTF8.GetBytes(mdl_miniyun_files.version_id.ToString()).Length + ":\"" + version_id + "\";s:7:\"user_id\";s:" + System.Text.Encoding.UTF8.GetBytes(netToUserId.ToString()).Length + ":\"" + netToUserId + "\";s:9:\"user_nick\";s:" + System.Text.Encoding.UTF8.GetBytes(mdlUsers.user_name).Length + ":\"" + mdlUsers.user_name + "\";s:11:\"device_name\";s:" + System.Text.Encoding.UTF8.GetBytes(mdlUsers.user_name).Length + ":\"" + mdlUsers.user_name + "\";s:9:\"file_size\";i:\"" + fileSize + "\";s:8:\"datetime\";s:19:\"" + time + "\";}";
                                            break;
                                        case 4:
                                            metas_valueLeft = "a:5:{";
                                            metas_valueMiddle = "i:4;a:7:{s:4:\"type\";i:4;s:10:\"version_id\";s:" + System.Text.Encoding.UTF8.GetBytes(mdl_miniyun_files.version_id.ToString()).Length + ":\"" + version_id + "\";s:7:\"user_id\";s:" + System.Text.Encoding.UTF8.GetBytes(netToUserId.ToString()).Length + ":\"" + netToUserId + "\";s:9:\"user_nick\";s:" + System.Text.Encoding.UTF8.GetBytes(mdlUsers.user_name).Length + ":\"" + mdlUsers.user_name + "\";s:11:\"device_name\";s:" + System.Text.Encoding.UTF8.GetBytes(mdlUsers.user_name).Length + ":\"" + mdlUsers.user_name + "\";s:9:\"file_size\";i:\"" + fileSize + "\";s:8:\"datetime\";s:19:\"" + time + "\";}";
                                            break;
                                        default:
                                            break;
                                    }
                                    if (flagnum != 5)
                                    {
                                        metas_value = metas_valueLeft + metas_value1 + metas_valueMiddle + "}";
                                    }
                                    else
                                    {
                                        metas_value = GetFiveMetas(metas_value);
                                        metas_value += "i:4;a:7:{s:4:\"type\";i:4;s:10:\"version_id\";s:" + System.Text.Encoding.UTF8.GetBytes(mdl_miniyun_files.version_id.ToString()).Length + ":\"" + version_id + "\";s:7:\"user_id\";s:" + System.Text.Encoding.UTF8.GetBytes(netToUserId.ToString()).Length + ":\"" + netToUserId + "\";s:9:\"user_nick\";s:" + System.Text.Encoding.UTF8.GetBytes(mdlUsers.user_name).Length + ":\"" + mdlUsers.user_name + "\";s:11:\"device_name\";s:" + System.Text.Encoding.UTF8.GetBytes(mdlUsers.user_name).Length + ":\"" + mdlUsers.user_name + "\";s:9:\"file_size\";i:\"" + fileSize + "\";s:8:\"datetime\";s:19:\"" + time + "\";}}";
                                    }
                                    metas_value = metas_value.Replace("/", "-");
                                    mdl_file_metas.meta_value = metas_value;
                                    mdl_file_metas.updated_at = time;
                                    bool msgError2 = bll_file_metas.Update(mdl_file_metas);
                                }
                                #endregion

                                #endregion
                                if (msgError && msgError1)
                                {
                                    strResult = "1";
                                }
                                else
                                {
                                    return "共享失败！";
                                }
                            }
                        }
                        else
                        {
                            #region 添加表miniyun_files
                            ZK.Model.miniyun_files mdl_miniyun_files1 = new ZK.Model.miniyun_files();
                            mdl_miniyun_files1.user_id = netToUserId;
                            mdl_miniyun_files1.file_type = 0;
                            mdl_miniyun_files1.parent_file_id = shareFolderId;
                            mdl_miniyun_files1.file_create_time = timeLong;
                            mdl_miniyun_files1.file_update_time = timeLong;
                            mdl_miniyun_files1.file_name = shareFileName;
                            mdl_miniyun_files1.version_id = mdl_miniyun_files.version_id;
                            mdl_miniyun_files1.file_size = fileSize;
                            mdl_miniyun_files1.file_path = mdlfile.file_path + "/" + shareFileName;
                            mdl_miniyun_files1.event_uuid = event_uuid;
                            mdl_miniyun_files1.is_deleted = 0;
                            mdl_miniyun_files1.mime_type = mime_type;
                            mdl_miniyun_files1.created_at = time;
                            mdl_miniyun_files1.updated_at = time;
                            mdl_miniyun_files1.sort = bll_miniyun_files.GetMaxId() + 1;

                            bool msgError = bll_miniyun_files.Add(mdl_miniyun_files1);

                            #endregion

                            #region 添加表miniyun_events
                            ZK.Model.miniyun_events mdl_miniyun_events1 = new ZK.Model.miniyun_events();
                            mdl_miniyun_events1.user_id = netToUserId;
                            mdl_miniyun_events1.user_device_id = netToUserId;
                            mdl_miniyun_events1.action = 3;
                            mdl_miniyun_events1.file_path = mdl_miniyun_files1.file_path;
                            string context = "a:5:{s:4:\"hash\";s:40:\"" + versionId + "\";s:3:\"rev\";i:" + mdl_miniyun_files.version_id + ";s:5:\"bytes\";i:" + fileSize + ";s:11:\"update_time\";i:" + timeLong + ";s:11:\"create_time\";i:" + timeLong + ";}";
                            mdl_miniyun_events1.context = context;
                            mdl_miniyun_events1.event_uuid = event_uuid;
                            mdl_miniyun_events1.created_at = time;
                            mdl_miniyun_events1.updated_at = time;

                            bool msgError1 = bll_miniyun_events.Add(mdl_miniyun_events1);

                            #endregion

                            #region 添加表miniyun_files_metas
                            ZK.Model.miniyun_file_metas mdl_file_metas = new ZK.Model.miniyun_file_metas();
                            mdl_file_metas.file_path = mdlfile.file_path + "/" + shareFileName;
                            mdl_file_metas.meta_key = "version";
                            string metas_value = "a:1:{i:0;a:7:{s:4:\"type\";i:3;s:10:\"version_id\";s:" + System.Text.Encoding.UTF8.GetBytes(mdl_miniyun_files.version_id.ToString()).Length + ":\"" + mdl_miniyun_files.version_id + "\";s:7:\"user_id\";s:" + System.Text.Encoding.UTF8.GetBytes(netToUserId.ToString()).Length + ":\"" + netToUserId + "\";s:9:\"user_nick\";s:" + System.Text.Encoding.UTF8.GetBytes(mdlUsers.user_name).Length + ":\"" + mdlUsers.user_name + "\";s:11:\"device_name\";s:" + System.Text.Encoding.UTF8.GetBytes(mdlUsers.user_name).Length + ":\"" + mdlUsers.user_name + "\";s:9:\"file_size\";i:\"" + fileSize + "\";s:8:\"datetime\";s:19:\"" + time + "\";}}";
                            metas_value = metas_value.Replace("/", "-");
                            mdl_file_metas.meta_value = metas_value;
                            mdl_file_metas.created_at = time;
                            mdl_file_metas.updated_at = time;
                            bool msgError2 = bll_file_metas.Add(mdl_file_metas);
                            #endregion

                            if (msgError && msgError1 && msgError2)
                            {
                                strResult = "1";
                            }
                            else
                            {
                                return "共享失败！";
                            }
                        }

                    }
                }

                #region 共享成功后，发送通知
                if (strResult == "1")
                {
                    string username = GetUserName(Convert.ToInt32(userId));
                    string str = SendShareInfo(username, "0", shareIdsAndGrants);
                    if (str == "1")
                    {
                        strResult = "1";
                    }
                    else
                    {
                        return "共享文件通知发送失败！";
                    }
                }
                #endregion

                return strResult;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        //新建文件夹
        private string AddNewFolder(int userId, string toUserId, string filename, long file_size, string parent_id, string userName, out string eventspath)
        {
            try
            {
                DateTime TimeNow = DateTime.Now;
                eventspath = "";
                //先查询出该文件或文件夹的父文件夹
                string folderpath = "";

                //ZK.Model.miniyun_files parentmodel = bll_miniyun_files.GetModel(Convert.ToInt32(parent_id));
                //folderpath = parentmodel.file_path;

                folderpath = "/" + toUserId;

                string folderPath = folderpath + "/" + filename + "(" + userName + ")";
                string event_uuid = System.Guid.NewGuid().ToString();

                #region 向文件表miniyun_files中为被共享人添加文件夹信息
                ZK.Model.miniyun_files model = new ZK.Model.miniyun_files();
                model.file_name = filename + "(" + userName + ")";
                model.user_id = Convert.ToInt32(toUserId);
                model.file_path = folderPath;
                model.parent_file_id = Convert.ToInt32(parent_id);
                model.file_size = 0;
                //判断是否存在该文件夹
                StringBuilder builder = new StringBuilder();
                builder.Append(" 1=1 ");
                builder.Append(" and file_name='" + model.file_name + "' ");
                builder.Append(" and user_id=" + model.user_id + " ");
                builder.Append(" and file_path='" + model.file_path + "' ");
                builder.Append(" and parent_file_id=" + model.parent_file_id + " ");
                builder.Append(" and is_deleted=0 ");
                List<ZK.Model.miniyun_files> listmodel = bll_miniyun_files.GetModelList(builder.ToString());
                if (listmodel != null && listmodel.Count > 0)
                {
                    return "共享已存在";
                }

                model.file_type = 3;

                model.mime_type = "";
                model.version_id = 0;
                model.created_at = TimeNow;
                model.event_uuid = event_uuid;
                model.updated_at = TimeNow;

                bool bresult1 = bll_miniyun_files.Add(model);
                #endregion

                eventspath = event_uuid;

                bool flagHave = false;
                bool flagHave1 = false;

                #region 更新表miniyun_files
                string event_uuid1 = System.Guid.NewGuid().ToString();
                ZK.Model.miniyun_files mdl_miniyunfiles = new ZK.Model.miniyun_files();
                mdl_miniyunfiles = bll_miniyun_files.GetModel(folderId);
                mdl_miniyunfiles.file_type = 2;
                mdl_miniyunfiles.event_uuid = event_uuid1;
                mdl_miniyunfiles.updated_at = TimeNow;
                bool bools1 = bll_miniyun_files.Update(mdl_miniyunfiles);
                #endregion

                #region 向表miniyun_events中添加共享人信息
                //string strS = "event_uuid='" + event_uuid1 + "'";
                string strS = "user_id=" + Convert.ToInt32(userId) + " and action=5 and file_path='/" + userId + "/" + filename + "'";
                DataSet ds1 = bll_miniyun_events.GetList(strS);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    flagHave1 = true;
                }
                else
                {
                    ZK.Model.miniyun_events eventmodel1 = new ZK.Model.miniyun_events();
                    eventmodel1.user_id = Convert.ToInt32(userId);
                    eventmodel1.user_device_id = Convert.ToInt32(userId);
                    eventmodel1.action = 5;
                    eventmodel1.file_path = mdl_miniyunfiles.file_path;
                    eventmodel1.context = mdl_miniyunfiles.file_path;  //"文件夹为path 文件为hash及大小版本等";
                    eventmodel1.event_uuid = event_uuid1;
                    eventmodel1.created_at = TimeNow;
                    eventmodel1.updated_at = TimeNow;
                    flagHave1 = bll_miniyun_events.Add(eventmodel1);
                }
                #endregion

                #region 向表miniyun_events中添加被共享人信息
                string strSql = "user_id=" + Convert.ToInt32(toUserId) + " and action=5 and file_path='" + folderPath + "'";
                DataSet ds = bll_miniyun_events.GetList(strSql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    flagHave = true;
                }
                else
                {
                    ZK.Model.miniyun_events eventmodel = new ZK.Model.miniyun_events();
                    eventmodel.user_id = Convert.ToInt32(toUserId);
                    eventmodel.user_device_id = Convert.ToInt32(userId);
                    eventmodel.action = 5;
                    eventmodel.file_path = folderPath;
                    eventmodel.context = folderPath;  //"文件夹为path 文件为hash及大小版本等";
                    eventmodel.event_uuid = event_uuid;
                    eventmodel.created_at = TimeNow;
                    eventmodel.updated_at = TimeNow;
                    flagHave = bll_miniyun_events.Add(eventmodel);
                }


                #endregion

                if (bresult1 && flagHave)
                {
                    return "1";
                }
                else
                {
                    return "插入表失败！";
                }

            }
            catch (Exception ex)
            {
                eventspath = "";
                return ex.ToString();
            }
        }

        //转换name
        private string GetUserName(int userid)
        {
            string name = "";
            ZK.Model.miniyun_users mdl_miniyun_users = bll_miniyun_users.GetModel(userid);
            string userName = mdl_miniyun_users.user_name;
            List<ZK.Model.USERS> list = bllUser.GetModelList("USERNAME='" + userName + "'");
            if (list.Count > 0)
            {
                name = list[0].ACTUALNAME;
            }
            return name;
        }

        /// <summary>
        /// 发送共享成功消息
        /// </summary>
        /// <param name="userName">共享人用户名</param>
        /// <param name="flag">文件（0）或文件夹（1）标识</param>
        /// <param name="userIds">被共享人Ids</param>
        /// <returns></returns>
        public string SendShareInfo(string userName, string flag, string userIds)
        {
            try
            {
                string strResult = "0";
                string title = "共享消息";
                string content = "";
                if (flag == "0")
                {
                    content = userName + "向您共享了一个文件，请查看";
                }
                else
                {
                    content = userName + "向您共享了一个文件夹，请查看";
                }
                string range = userIds;
                string onlion = "0";
                string linkText = "";
                string strSql = "APPNAME='我的智客云'";
                ZK.BLL.WEBAPPS bll_WEBAPPS = new ZK.BLL.WEBAPPS();
                List<ZK.Model.WEBAPPS> list_WEBAPPS = bll_WEBAPPS.GetModelList(strSql);
                if (list_WEBAPPS.Count > 0)
                {
                    linkText = list_WEBAPPS[0].APPURL.Replace("&", "&amp;");
                }

                string strResponse = "";
                string strRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><request>" +
                             "<ip>" + "127.0.0.1" + "</ip>" +
                             "<forusertype>" + 0 + "</forusertype>" +
                             "<title>" + title + "</title>" +
                              "<content>" + content + "</content>" +
                               "<link>" + linkText + "</link>" +
                                "<sendto>" + range + "</sendto>" +
                                "<online>" + onlion + "</online>" +
                             "</request> ";

                bool boolIS = new OpenCom.Command().Execute("Admin.SendSysMsg", strRequest, ref strResponse, 5000);

                //xml to dataset
                StringReader stream = null;
                XmlTextReader reader = null;
                DataSet dsResponse = new DataSet();

                stream = new StringReader(strResponse);
                //从stream装载到XmlTextReader
                reader = new XmlTextReader(stream);
                dsResponse.ReadXml(reader);
                if (boolIS == true)
                {
                    strResult = "1";
                }
                return strResult;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        private string GetFiveMetas(string str)
        {
            string metas_value = str.Replace("{", "#").Replace("}", "#");
            List<string> listnum = new List<string>();
            List<string> listvalue = new List<string>();
            string[] strmetas = metas_value.Split('#');
            string metasfirst = strmetas[0] + "{";
            string metasMiddle = "";
            for (int i = 1; i < strmetas.Length; i++)
            {
                if (i % 2 == 1)
                {
                    listnum.Add(strmetas[i]);
                }
                else
                {
                    if (i > 3)
                    {
                        listvalue.Add(strmetas[i]);
                    }
                }
            }
            metasMiddle = listnum[0] + "{" + listvalue[0] + "}" + listnum[1] + "{" + listvalue[1] + "}" + listnum[2] + "{" + listvalue[2] + "}" + listnum[3] + "{" + listvalue[3] + "}";
            metas_value = metasfirst + metasMiddle;
            return metas_value;
        }
        #endregion

        /// <summary>
        /// 根据 用户Id 文件类型获取 该类型下的文件信息
        /// </summary>
        /// <param > userId</param>
        /// /// <param > fileType</param>
        /// <returns></returns>
        public string FileListJson()
        {
            string jcbstr = Request["jcb"];
            try
            {
                string userID = Request["userId"];
                string flag = Request["type"];
                int flagType = Convert.ToInt32(flag);
                //userID = "10040";
                //flagType = 2;
                if (userID != null && userID != "")
                {
                    DataSet dsUser = bllUser.GetList("userid=" + Convert.ToInt32(userID));
                    List<Model.USERS> listUser = bllUser.DataTableToList(dsUser.Tables[0]);
                    string userName = listUser[0].USERNAME;

                    DataSet dsNetUser = bllNetUser.GetList("user_name='" + userName + "'");
                    if (dsNetUser.Tables[0].Rows.Count > 0)
                    {
                        List<Model.miniyun_users> listNetUser = bllNetUser.DataTableToList(dsNetUser.Tables[0]);
                        int netUserId = listNetUser[0].id;

                        if (flagType != 0)
                        {
                            string strWhere = "user_id=" + netUserId;
                            switch (flagType)
                            {
                                case 1:
                                    strWhere += " and mime_type in (\"application/msword\",\"application/rtf\",\"application/msexcel\",\"application/mspowerpoint\")";
                                    break;
                                case 2:
                                    strWhere += " and mime_type like 'image%'";
                                    break;
                                case 3:
                                    strWhere += " and mime_type like 'video%'";
                                    break;
                                case 4:
                                    strWhere += " and mime_type like 'audio%'";
                                    break;
                                default:
                                    strWhere += " and mime_type not in (\"application/msword\",\"application/rtf\",\"application/msexcel\",\"application/mspowerpoint\")";
                                    strWhere += " and mime_type not like 'image%'";
                                    strWhere += " and mime_type not like 'video%'";
                                    strWhere += " and mime_type not like 'audio%'";
                                    strWhere += " and file_type =0";
                                    break;
                            }
                            DataSet ds = bllNetFile.GetList(strWhere);
                            string strJson = "";
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                if (flagType == 2)
                                {
                                    strJson = PicListInfo(ds.Tables[0]);
                                    return jcbstr + "(" + strJson + ")";
                                }
                                else
                                {
                                    strJson = CreateJsonParameters(ds.Tables[0]);
                                    return jcbstr + "(" + strJson + ")";
                                }
                            }
                            else
                                return jcbstr + "({})";
                        }
                        else
                        {
                            return jcbstr + "({})";
                        }
                    }
                    else
                    {
                        return jcbstr + "({})";
                    }
                }
                else
                {
                    return jcbstr + "({})";
                }
            }
            catch
            {
                return jcbstr + "({})";
            }
        }
        
        #endregion

        #region 方法
        //转换成MySql中的UserId
        public int ChangeUserId(int userId)
        {
            int netUserId = userId;
            DataSet dsUser = bllUser.GetList("userid=" + Convert.ToInt32(userId));
            List<Model.USERS> listUser = bllUser.DataTableToList(dsUser.Tables[0]);
            string userName = listUser[0].USERNAME;

            DataSet dsNetUser = bllNetUser.GetList("user_name='" + userName + "'");
            if (dsNetUser.Tables[0].Rows.Count > 0)
            {
                List<Model.miniyun_users> listNetUser = bllNetUser.DataTableToList(dsNetUser.Tables[0]);
                netUserId = listNetUser[0].id;
            }
            return netUserId;
        }

        //返回文档，视频，音频，其他信息
        public string CreateJsonParameters(DataTable dt)
        {
            StringBuilder JsonString = new StringBuilder();
            //Exception Handling        
            if (dt != null && dt.Rows.Count > 0)
            {
                JsonString.Append("{list:[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    JsonString.Append("{");
                    if (i < dt.Rows.Count)
                    {
                        JsonString.Append("\"" + dt.Columns[0].ColumnName.ToString() + "\":" + dt.Rows[i][0].ToString());
                        JsonString.Append(",\"" + dt.Columns[6].ColumnName.ToString() + "\":" + "\"" + dt.Rows[i][6].ToString() + "\"");

                        string[] strDate = dt.Rows[i][13].ToString().Split(' ');
                        JsonString.Append(",\"time\":" + "\"" + strDate[0] + "\"");

                        double size = Convert.ToDouble(dt.Rows[i][8]);
                        size = ReNum(size);
                        JsonString.Append(",\"size\":" + "\"" + size + "\"");

                        string filename = "";
                        string hashname = "";
                        string pathFace = "/Files/nfiles";
                        string filepath = pathFace + "/" + GetFilePathByFileID(dt.Rows[i][0].ToString(), out filename, out hashname) + "/" + hashname;
                        //filepath = Server.MapPath(filepath);
                        //string otherWebFilePath = ZK.Common.CommonFunction.GetFileFromOtherWeb(Server.MapPath("../"), "ZK.MVCWeb", filepath);
                        JsonString.Append(",\"path\":\"" + filepath + "\"");
                    }
                    if (i == dt.Rows.Count - 1)
                    {
                        JsonString.Append("}");
                    }
                    else
                    {
                        JsonString.Append("},");
                    }
                }
                JavaScriptSerializer jss = new JavaScriptSerializer();
                JsonString.Append("]}");
                return JsonString.ToString();
            }
            else
            {
                return null;
            }
        }

        //返回图片的信息
        private string PicListInfo(DataTable dt)
        {
            #region 注释
            //StringBuilder JsonString = new StringBuilder();
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    List<Model.miniyun_files> listPic = bllNetFile.DataTableToList(dt);
            //    JsonString.Append("{list:[");
            //    for (int i = 0; i < listPic.Count; i++)
            //    {
            //        int picId = listPic[i].id;
            //        //picId = 184;

            //        //数据流返回                    
            //        string filename1 = "";
            //        string hashname1 = "";
            //        string filepath1 = ZK.Common.ModelSettings.CreateFileDefaultPath + "/" + GetFilePathByFileID(picId.ToString(), out filename1, out hashname1) + "/" + hashname1;
            //        string otherWebFilePath = GetFileFromOtherWeb(Server.MapPath("../"), "ZK.MVCWeb", filepath1);
            //        string picStream = DownLoadFile1(Response, otherWebFilePath, System.IO.Path.GetExtension(filename1), filename1);

            //        string picName = listPic[i].file_name;

            //        string[] strDate;
            //        strDate = listPic[i].updated_at.ToString().Split(' ');

            //        string filename = "";
            //        string hashname = "";
            //        string filepath = ZK.Common.ModelSettings.CreateFileDefaultPath + "/" + GetFilePathByFileID(picId.ToString(), out filename, out hashname) + "/" + hashname;
            //        #region 生成缩略图
            //        string imageName = ZK.Common.ImageHelper.Image(Server.MapPath(filepath), "_256.png", 72, 72);
            //        //缩略图2
            //        ZK.Common.ImageHelper.Image(Server.MapPath(filepath), "_48.png", 48, 48);
            //        string smallImg = filepath.Substring(0, filepath.Length - System.IO.Path.GetFileName(filepath).Length) + imageName;
            //        #endregion
            //        //filepath = Server.MapPath(filepath);
            //        //string otherWebFilePath = ZK.Common.CommonFunction.GetFileFromOtherWeb(Server.MapPath("../"), "ZK.MVCWeb", filepath);
            //        if (i == listPic.Count - 1)
            //        {
            //            JsonString.Append("{\"id\":" + picId + ",\"smallImg\":\"" + smallImg + "\",\"file_name\":\"" + picName + "\",\"updated_at\":\"" + strDate[0] + "\",\"path\":\"" + filepath + "\",\"picStream\":\"" + picStream + "\"}");
            //        }
            //        else
            //        {
            //            JsonString.Append("{\"id\":" + picId + ",\"smallImg\":\"" + smallImg + "\",\"file_name\":\"" + picName + "\",\"updated_at\":\"" + strDate[0] + "\",\"path\":\"" + filepath + "\"},\"picStream\":\"" + picStream + "\"},");
            //        }

            //    }

            //    JsonString.Append("]}");
            //    return JsonString.ToString();
            //}
            //else
            //{
            //    return null;
            //}
            #endregion

            StringBuilder JsonString = new StringBuilder();
            //Exception Handling        
            if (dt != null && dt.Rows.Count > 0)
            {
                JsonString.Append("{list:[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    JsonString.Append("{");
                    if (i < dt.Rows.Count)
                    {
                        JsonString.Append("\"" + dt.Columns[0].ColumnName.ToString() + "\":" + dt.Rows[i][0].ToString());
                        JsonString.Append(",\"" + dt.Columns[6].ColumnName.ToString() + "\":" + "\"" + dt.Rows[i][6].ToString() + "\"");

                        string[] strDate = dt.Rows[i][14].ToString().Split(' ');
                        JsonString.Append(",\"time\":" + "\"" + strDate[0] + "\"");

                        double size = Convert.ToDouble(dt.Rows[i][8]);
                        size = ReNum(size);
                        JsonString.Append(",\"size\":" + "\"" + size + "\"");

                        string filename = "";
                        string hashname = "";
                        string pathFace = "/Files/nfiles";
                        string filepath = pathFace + "/" + GetFilePathByFileID(dt.Rows[i][0].ToString(), out filename, out hashname) + "/" + hashname;
                        #region 生成缩略图
                        //string filepath1 = Server.MapPath(filepath);
                        //string otherWebFilePath = ZK.Common.CommonFunction.GetFileFromOtherWeb(Server.MapPath("../"), "ZK.MVCWeb", filepath);
                        string imageName = ZK.Common.ImageHelper.Image(Server.MapPath(filepath), "_256.png", 72, 72);
                        string smallImg = filepath.Substring(0, filepath.Length - System.IO.Path.GetFileName(filepath).Length) + imageName;
                        #endregion
                        JsonString.Append(",\"path\":\"" + filepath + "\",\"smallImg\":\"" + smallImg + "\"");
                    }
                    if (i == dt.Rows.Count - 1)
                    {
                        JsonString.Append("}");
                    }
                    else
                    {
                        JsonString.Append("},");
                    }
                }
                JavaScriptSerializer jss = new JavaScriptSerializer();
                JsonString.Append("]}");
                return JsonString.ToString();
            }
            else
            {
                return null;
            }
        }

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
            //string folderpath = ZK.Common.ModelSettings.CreateFileDefaultPath;
            string folderpath = "/" + userId;
            int pid = 0;
            if (parent_id != "0" && int.TryParse(parent_id, out pid))
            {
                Model.miniyun_files parentmodel = bllNetFile.GetModel(Convert.ToInt32(parent_id));
                folderpath = parentmodel.file_path;
            }

            string event_uuid = System.Guid.NewGuid().ToString();

            //文件表
            Model.miniyun_files model = new Model.miniyun_files();
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

            List<Model.miniyun_files> listmodel = bllNetFile.GetModelList(builder.ToString());
            if (listmodel != null && listmodel.Count > 0)
            {
                return "共享已存在";
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
            bool bresult1 = bllNetFile.Add(model);

            //事件表
            Model.miniyun_events eventmodel = new Model.miniyun_events();
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

        private string AddNewShareFile(string userId, string filename, long file_size, string parent_id, string mime_type, bool isfile, string share, out string hashpath)
        {
            DateTime TimeNow = DateTime.Now;
            hashpath = "";
            //先查询出该文件或文件夹的父文件夹
            string folderpath = ZK.Common.ModelSettings.CreateFileDefaultPath;
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
                return "共享已存在";
            }

            if (share == "New")
            {
                // System.IO.Directory.CreateDirectory(Request.MapPath(folderpath + "/" + filename));
                model.file_type = 1;
            }
            else
            {
                model.file_type = 3;
            }
            if (isfile)
            {
                model.file_type = 4;
                #region 版本表
                ZK.Model.miniyun_file_versions versionmodel = new ZK.Model.miniyun_file_versions();
                versionmodel.file_signature = ZK.Common.CommonFunction.GetRandomHashs(40);
                versionmodel.file_size = file_size;
                versionmodel.created_at = TimeNow;
                versionmodel.mime_type = mime_type;
                versionmodel.ref_count = 0;
                versionmodel.block_ids = "模拟块儿信息";
                bll_miniyun_versions.Add(versionmodel);
                hashpath = versionmodel.file_signature;

                #endregion
            }
            model.mime_type = mime_type;
            model.version_id = bll_miniyun_versions.GetMaxId() - 1;
            model.created_at = TimeNow;
            model.event_uuid = event_uuid;
            model.updated_at = TimeNow;
            //  model.version_id
            bool bresult1 = bllNetFile.Add(model);

            #region 事件表
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

            #endregion

            if (bresult1 && bresult2)
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }

        private double ReNum(double d)
        {
            double dNum;
            dNum = d / Convert.ToDouble(1024);
            dNum = dNum / Convert.ToDouble(1024);
            dNum = Math.Round(dNum, 2);
            return dNum;
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
            Model.miniyun_files filemodel = bllNetFile.GetModel(Convert.ToInt32(fileid));
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

        //检查是否已经建立了共享目标文件夹，如果没有，则建立并返回ID，如果已经建立，则取出其ID；
        private int GetShareFolderId(int netToUserId, string userName)
        {
            ZK.BLL.miniyun_files bll_miniyun_files = new ZK.BLL.miniyun_files();
            int folderId = 0;
            string folderName = userName + "的共享";
            string strSql = "user_id=" + netToUserId + " and file_name='" + folderName + "'";
            DataSet ds = bll_miniyun_files.GetList(strSql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                List<ZK.Model.miniyun_files> listfile = bll_miniyun_files.DataTableToList(ds.Tables[0]);

                folderId = listfile[0].id;
            }
            else
            {
                string hashpath = "";
                string strResult = AddNewShareFile(netToUserId.ToString(), folderName, 0, "0", "", false, "New", out hashpath);
                if (strResult == "1")
                {
                    DataSet dsFolder = bll_miniyun_files.GetList(strSql);
                    List<ZK.Model.miniyun_files> listmdl = bll_miniyun_files.GetModelList(strSql);
                    folderId = listmdl[0].id;
                }
            }
            return folderId;
        }

        //获取用户Id
        private string GetUserId(string username)
        {
            ZK.BLL.miniyun_users bll_miniyun_users = new BLL.miniyun_users();
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

        #region 未用
        /// <summary>
        /// 根据 用户Id 文件Id 共享
        /// </summary>
        /// <param name="userId"> userId</param>
        /// <param name="userIds" > userIds</param>
        /// <param name="fileIds"> fileIds</param>
        /// <returns></returns>
        public string FileShareJsonInfo()
        {
            string jcbstr = Request["jcb"];
            try
            {
                string userId = Request["userId"];
                string sharedUserIds = Request["Ids"];
                string fileId = Request["fileId"];
                string flag = "";

                string[] idlist = sharedUserIds.Split(',');


                return jcbstr + "({\"flag\":\"" + flag + "\"})";
            }
            catch
            {
                string flag = "Fail";
                return jcbstr + "({\"flag\":\"" + flag + "\"})";
            }
        }

        private string DownLoadFile1(HttpResponseBase Response, string filePath, string fileext, string filename)
        {

            //客户端保存的文件名

            //以字符流的形式下载文件
            FileStream fs = new FileStream(filePath, FileMode.Open);
            byte[] bytes = new byte[(int)fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            return Encoding.Default.GetString(bytes);

        }

        /// <summary>
        /// 跨网站访问 文件
        /// </summary>
        /// <param name="ServerPath">Server.MappPath("相对路径")</param>
        /// <param name="WebName">要访问的网站名</param>
        /// <param name="ServerPath2">要访问的文件的 相对路径</param>
        /// <returns>目标的路径 绝对路径</returns>
        private string GetFileFromOtherWeb(string ServerPath, string WebName, string TragetPath)
        {
            string path = System.IO.Path.GetDirectoryName(ServerPath);
            string path2 = System.IO.Path.GetFileName(path);
            string path3 = path.Substring(0, path.Length - path2.Length);
            return path3 + WebName + TragetPath;
        }

        /// <summary>
        /// 根据 用户Id 文件夹Id 获取文件或文件夹列表 未用
        /// </summary>
        /// <param name="userID"> userId</param>
        /// <param name="folderId"> folderId</param>
        /// <returns></returns>
        public string FolderIdListInfo()
        {
            string jcbstr = Request["jcb"];
            try
            {
                string userID = Request["userID"];
                //userID = "10040";
                string folderId = Request["folderId"];
                //folderId = "188";
                string strJson = "";
                if (userID != null && userID != "")
                {
                    DataSet dsUser = bllUser.GetList("userid=" + Convert.ToInt32(userID));
                    List<Model.USERS> listUser = bllUser.DataTableToList(dsUser.Tables[0]);
                    string userName = listUser[0].USERNAME;
                    StringBuilder JsonString = new StringBuilder();
                    DataSet dsNetUser = bllNetUser.GetList("user_name='" + userName + "'");
                    if (dsNetUser.Tables[0].Rows.Count > 0)
                    {
                        List<Model.miniyun_users> listNetUser = bllNetUser.DataTableToList(dsNetUser.Tables[0]);
                        int netUserId = listNetUser[0].id;
                        string strwhere = " user_id=" + netUserId + "  and parent_file_id=" + Convert.ToInt32(folderId);
                        DataSet dsFilelist = bllNetFile.GetList(strwhere);
                        if (dsFilelist.Tables[0].Rows.Count > 0)
                        {
                            strJson = CreateJsonFilesList(dsFilelist.Tables[0]);
                            return jcbstr + "(" + strJson + ")";
                        }
                        else
                        {
                            return jcbstr + "({})";
                        }
                    }
                    else
                    {
                        return jcbstr + "({})";
                    }
                }
                else
                {
                    return jcbstr + "({})";
                }

            }
            catch
            {
                return jcbstr + "({})";
            }
        }

        /// <summary>
        /// 根据 用户Id 文件Id 上传
        /// </summary>
        /// <param name="userId"> userId</param>        
        /// <param name="fileData"> fileData</param>
        /// </summary>
        public string UpLoadFile()
        {
            string jcbstr = Request["jcb"];
            HttpPostedFileBase file = Request.Files["fileName"];
            string userId = Request["userId"];
            string mime_type = "";
            try
            {
                if (file != null)
                {
                    string file_ext = System.IO.Path.GetExtension(file.FileName);
                    mime_type = Common.CommonFunction.ExtTomimetype(file_ext.ToString().Substring(1));
                    string parent_id = "0";
                    string hashpath = "";
                    //向数据库添加数据
                    string strResult = AddNewFile(userId, file.FileName, file.ContentLength, parent_id, mime_type, true, out hashpath);

                    //保存文件
                    string directorypath = Server.MapPath(ZK.Common.ModelSettings.CreateFileDefaultPath + "/" + GetFilePathByHash(hashpath));
                    directorypath = Server.MapPath(directorypath);
                    directorypath = GetFileFromOtherWeb(Server.MapPath("../"), "ZK.MVCWeb", directorypath);

                    if (!Directory.Exists(directorypath))
                    {
                        Directory.CreateDirectory(directorypath);
                    }
                    file.SaveAs(directorypath + "\\" + hashpath);
                    //下面这句代码缺少的话，上传成功后上传队列的显示不会自动消失
                    return jcbstr + "({code : \"" + strResult + "\"})";
                }
                else
                {
                    return jcbstr + "({code : 0})";
                }
            }
            catch
            {
                return jcbstr + "({code : 0})";
            }
        }

        //public string ProcessRequest(HttpContext context)
        //{
        //    try
        //    {
        //        context.Response.ContentType = "text/plain";
        //        string jcbstr = Request["jcb"];

        //        string userId = Request["userId"];

        //        string from = context.Request.QueryString["fileName"];
        //        string path = context.Server.MapPath("~/") + "upload";
        //        //if (from == "web")
        //        {
        //            if (context.Request.Files.Count > 0)
        //            {
        //                if (!Directory.Exists(path))
        //                {
        //                    Directory.CreateDirectory(path);
        //                }

        //                string fileName = Path.GetFileName(context.Request.Files["fileName"].FileName);
        //                string end = fileName.Substring(fileName.Length - 4).ToLower();

        //                //if (end.Equals(".jpg") || end.Equals(".png") || end.Equals(".gif") || end.Equals(".mp4"))
        //                {
        //                    context.Request.Files["fileName"].SaveAs(path + "\\" + fileName);
        //                }
        //            }
        //        }
        //        return jcbstr + "({code : 1})";
        //    }
        //    catch
        //    {
        //        return "({code : 0})";
        //    }

        //}

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");

            //try
            //{
            //    string from = context.Request.QueryString["from"];
            //    string path = context.Server.MapPath("~/") + "upload";
            //    if (from == "web")
            //    {
            //        context.Response.Write("<script>alert('1')</script>");
            //        if (context.Request.Files.Count > 0)
            //        {
            //            context.Response.Write("<script>alert('2')</script>");

            //            if (!Directory.Exists(path))
            //            {
            //                Directory.CreateDirectory(path);
            //            }

            //            string fileName = Path.GetFileName(context.Request.Files["fileName"].FileName);
            //            string end = fileName.Substring(fileName.Length - 4).ToLower();


            //            if (end.Equals(".jpg") || end.Equals(".png") || end.Equals(".gif") || end.Equals(".mp4"))
            //            {
            //                context.Request.Files["fileName"].SaveAs(path + "\\" + fileName);
            //            }
            //        }
            //    }
            //    if (from == "client")
            //    {
            //        if (context.Request.Files.Count > 0)
            //        {
            //            if (!Directory.Exists(path))
            //                Directory.CreateDirectory(path);
            //            string fileName = Path.GetFileName(context.Request.Files["file"].FileName);
            //            string end = fileName.Substring(fileName.Length - 4).ToLower();
            //            if (end.Equals(".jpg") || end.Equals(".png") || end.Equals(".gif") || end.Equals(".mp4"))
            //            {
            //                context.Request.Files["file"].SaveAs(path + "\\" + fileName);
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            ZK.BLL.ZK_Channel bll = new BLL.ZK_Channel();
            ZK.Model.ZK_Channel mdl = new Model.ZK_Channel();
            mdl.channelName = "iii";
            mdl.channelDesc = "ttt";
            bll.Add(mdl);
            return Redirect("SchoolConfig");
            //context.Response.Write("The file " + context.Request.Files["filename"].FileName + " has been uploaded");
            //         }

        }
        #endregion 

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        //返回文件列表
        public string CreateJsonFilesList(DataTable dt)
        {
            StringBuilder JsonString = new StringBuilder();
            //Exception Handling        
            if (dt != null && dt.Rows.Count > 0)
            {
                JsonString.Append("{list:[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    JsonString.Append("{");
                    if (i < dt.Rows.Count)
                    {
                        JsonString.Append("\"" + dt.Columns[0].ColumnName.ToString() + "\":" + dt.Rows[i][0].ToString());
                        JsonString.Append(",\"" + dt.Columns[2].ColumnName.ToString() + "\":" + dt.Rows[i][2].ToString());
                        JsonString.Append(",\"" + dt.Columns[6].ColumnName.ToString() + "\":" + "\"" + dt.Rows[i][6].ToString() + "\"");
                        string fileTypeFlag = dt.Rows[i][6].ToString();
                        string file_ext = System.IO.Path.GetExtension(fileTypeFlag);

                        //JsonString.Append(",\"" + dt.Columns[8].ColumnName.ToString() + "\":" + dt.Rows[i][8].ToString() + "");
                        double size = Convert.ToDouble(dt.Rows[i][8]);
                        size = ReNum(size);
                        JsonString.Append(",\"size\":" + "\"" + size + "\"");
                        //JsonString.Append(",\"" + dt.Columns[14].ColumnName.ToString() + "\":\"" + dt.Rows[i][14].ToString() + "\"");
                        string[] strDate;
                        strDate = dt.Rows[i][14].ToString().Split(' ');
                        JsonString.Append(",\"" + dt.Columns[14].ColumnName.ToString() + "\":\"" + strDate[0] + "\"");

                        string filename = "";
                        string hashname = "";
                        string filepath = "";
                        if (dt.Rows[i][2].ToString() == "0")
                        {
                            filepath = ZK.Common.ModelSettings.CreateFileDefaultPath + "/" + GetFilePathByFileID(dt.Rows[i][0].ToString(), out filename, out hashname) + "/" + hashname;
                        }
                        else
                        {
                            filepath = dt.Rows[i][9].ToString();
                        }
                        string extName = System.IO.Path.GetExtension(filename);
                        string miniType = dt.Rows[i][12].ToString();
                        string fileType = CommonFunction.GetTypeByMimeType(miniType);
                        int flagType;
                        switch (fileType)
                        {
                            case "2":
                            case "6":
                            case "7":
                            case "8":
                                flagType = 1;
                                break;
                            case "3":
                                flagType = 2;
                                break;
                            case "1":
                                flagType = 3;
                                break;
                            case "4":
                                flagType = 4;
                                break;
                            default:
                                flagType = 0;
                                break;
                        }

                        //JsonString.Append(",\"extflag\":\"" + flagType + extName + "\"");
                        //filepath = Server.MapPath(filepath);
                        //string otherWebFilePath = ZK.Common.CommonFunction.GetFileFromOtherWeb(Server.MapPath("../"), "ZK.MVCWeb", filepath);
                        JsonString.Append(",\"path\":\"" + filepath + "\"");
                        if (flagType == 2)
                        {
                            #region 生成缩略图
                            string imageName = ZK.Common.ImageHelper.Image(Server.MapPath(filepath), "_72.png", 72, 72);
                            string smallImg = filepath.Substring(0, filepath.Length - System.IO.Path.GetFileName(filepath).Length) + imageName;
                            #endregion
                            JsonString.Append(",\"smallImg\":\"" + smallImg + "\"");
                        }
                        else
                        {
                            JsonString.Append(",\"smallImg\":\"NoImg\"");
                        }
                    }
                    if (i == dt.Rows.Count - 1)
                    {
                        JsonString.Append("}");
                    }
                    else
                    {
                        JsonString.Append("},");
                    }
                }
                JavaScriptSerializer jss = new JavaScriptSerializer();
                JsonString.Append("]}");
                return JsonString.ToString();
            }
            else
            {
                return null;
            }
        }

    }
}
