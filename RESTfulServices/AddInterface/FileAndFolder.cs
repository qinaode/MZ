using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using System.IO;
using System.Xml;
using System.Web.Script.Serialization;

namespace RESTfulServices.AddInterface
{
    public class FileAndFolder
    {
        #region 定义
        static ZK.BLL.miniyun_files bll_miniyun_files = new ZK.BLL.miniyun_files();
        static ZK.BLL.miniyun_events bll_miniyun_events = new ZK.BLL.miniyun_events();
        static ZK.BLL.miniyun_file_versions bll_miniyun_file_versions = new ZK.BLL.miniyun_file_versions();
        static ZK.BLL.miniyun_user_privilege bll_miniyun_user_privilege = new ZK.BLL.miniyun_user_privilege();
        static ZK.BLL.miniyun_users bll_miniyun_users = new ZK.BLL.miniyun_users();
        static ZK.BLL.miniyun_file_metas bll_miniyun_file_metas = new ZK.BLL.miniyun_file_metas();
        static ZK.BLL.USERS bllUser = new ZK.BLL.USERS();
        static ZK.BLL.miniyun_file_metas bll_file_metas = new ZK.BLL.miniyun_file_metas();

        static int folderId;
        #endregion

        #region 接口
        /// <summary>
        /// 文件夹的共享
        /// </summary>
        /// <param name="userId">当前用户Id</param>
        /// <param name="shareIdsAndGrants">目标用户Ids及权限</param>
        /// <param name="forlderId">文件夹Id</param>
        /// <returns></returns>
        public static string ShareFolderAction(string userId, string userName, string shareIdsAndGrants, string forlderId)
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

                            //strResult = AddNewFolder(Convert.ToInt32(userId), netToUserId.ToString(), shareForldName, 0, shareFolderId.ToString(), "", out eventspath);
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
                        //string str = SendShareInfo(username, "1", userIds);
                        //if (str == "1")
                        //{
                        //    strResult = "1";
                        //}
                        //else
                        //{
                        //    return "共享文件通知发送失败！";
                        //}
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
        public static string ShareFileAction(string userId, string userName, string shareIdsAndGrants, string fileId)
        {
            try
            {
                #region 参数处理
                string[] toUserId = shareIdsAndGrants.Split(',');//取得目标用户id和权限列表
                string strResult = "0";
                DateTime time = DateTime.Now;
                long timeLong = GetTimeStamp();
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
                                mdl_miniyun_files3.updated_at = DateTime.Now;
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
                                List<ZK.Model.miniyun_file_metas> listfile_metas = bll_miniyun_file_metas.GetModelList("file_path='" + list_miniyun_files[0].file_path+"'");
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
                    //wuyanyan 2014-03-12 14:49  暂时注销 此方法为向IM发送消息
                    //string username = GetUserName(Convert.ToInt32(userId));
                    //string str = SendShareInfo(username, "0", shareIdsAndGrants);
                    //if (str == "1")
                    //{
                    //    strResult = "1";
                    //}
                    //else
                    //{
                    //    return "共享文件通知发送失败！";
                    //}
                }
                #endregion

                return strResult;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        ///<summary>
        /// 加载已共享过的共享文件夹
        /// </summary>
        /// <param name="folderId">文件夹Id</param>
        /// <returns></returns>
        public static string GetShareFolderInfo(int folderId)
        {
            try
            {
                string strResult = "";
                string default_permission = "default";
                #region 默认权限
                ZK.BLL.miniyun_options bll_miniyun_options = new ZK.BLL.miniyun_options();
                List<ZK.Model.miniyun_options> list_miniyun_options = bll_miniyun_options.GetModelList("option_name='default_permission'");
                if (list_miniyun_options.Count > 0)
                {
                    string str3 = list_miniyun_options[0].option_value;
                    string str4 = str3.Replace("i:", "#");
                    string[] defautpemissionlist = str4.Split('#');
                    default_permission += "_" + defautpemissionlist[1].Substring(0, 1) + "_" + defautpemissionlist[2].Substring(0, 1) + "_" + defautpemissionlist[3].Substring(0, 1) + "_" + defautpemissionlist[4].Substring(0, 1) + "_" + defautpemissionlist[5].Substring(0, 1) + "_" + defautpemissionlist[6].Substring(0, 1) + "_" + defautpemissionlist[6].Substring(0, 1) + "_" + defautpemissionlist[8].Substring(0, 1) + "_" + defautpemissionlist[9].Substring(0, 1);
                }

                #endregion

                ZK.Model.miniyun_files mdl_miniyun_files = bll_miniyun_files.GetModel(folderId);
                int fileType = mdl_miniyun_files.file_type;

                if (mdl_miniyun_files != null)
                {
                    if (fileType == 2)
                    {
                        string filePath = mdl_miniyun_files.file_path;
                        string strSql = "file_path='" + filePath + "'";
                        List<ZK.Model.miniyun_user_privilege> list_miniyun_user_privilege = bll_miniyun_user_privilege.GetModelList(strSql);
                        if (list_miniyun_user_privilege.Count > 0)
                        {
                            for (int i = 0; i < list_miniyun_user_privilege.Count; i++)
                            {
                                string str1 = list_miniyun_user_privilege[i].permission;
                                string str2 = str1.Replace("i:", "#");
                                string[] pemissionlist = str2.Split('#');
                                //UserId转换和获取用户名
                                string strIdAndName = GetUserId(list_miniyun_user_privilege[i].user_id);

                                if (i == list_miniyun_user_privilege.Count - 1)
                                {
                                    strResult += fileType.ToString() + "_" + strIdAndName;
                                    strResult += "_" + pemissionlist[1].Substring(0, 1) + "_" + pemissionlist[2].Substring(0, 1) + "_" + pemissionlist[3].Substring(0, 1) + "_" + pemissionlist[4].Substring(0, 1) + "_" + pemissionlist[5].Substring(0, 1) + "_" + pemissionlist[6].Substring(0, 1) + "_" + pemissionlist[6].Substring(0, 1) + "_" + pemissionlist[8].Substring(0, 1) + "_" + pemissionlist[9].Substring(0, 1);
                                }
                                else
                                {
                                    strResult += fileType.ToString() + "_" + strIdAndName;
                                    strResult += "_" + pemissionlist[1].Substring(0, 1) + "_" + pemissionlist[2].Substring(0, 1) + "_" + pemissionlist[3].Substring(0, 1) + "_" + pemissionlist[4].Substring(0, 1) + "_" + pemissionlist[5].Substring(0, 1) + "_" + pemissionlist[6].Substring(0, 1) + "_" + pemissionlist[6].Substring(0, 1) + "_" + pemissionlist[8].Substring(0, 1) + "_" + pemissionlist[9].Substring(0, 1);
                                    strResult += ",";
                                }
                            }
                        }
                        else
                        {
                            strResult = "0";
                        }
                    }
                    else
                    {
                        strResult = fileType.ToString();
                    }
                }
                else
                {
                    strResult = "0";
                }

                return default_permission + "@" + strResult;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }

        #endregion

        #region 方法
        //新建文件夹
        private static string AddNewFolder(int userId, string toUserId, string filename, long file_size, string parent_id, string userName, out string eventspath)
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

        //为被共享人建立共享人的文件夹
        private static string AddNewShareFolder(string toUserId, string filename, long file_size, string parent_id, string mime_type, out string hashpath)
        {
            try
            {
                DateTime TimeNow = DateTime.Now;
                hashpath = "";

                string folderpath = "/" + toUserId;
                string event_uuid = System.Guid.NewGuid().ToString();

                //文件表
                ZK.Model.miniyun_files model = new ZK.Model.miniyun_files();
                model.file_name = filename;
                model.user_id = Convert.ToInt32(toUserId);
                model.file_path = folderpath + "/" + filename;
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

                model.file_type = 3;//建立的共享人文件夹

                model.mime_type = mime_type;
                model.version_id = 0;
                model.created_at = TimeNow;
                model.event_uuid = event_uuid;
                model.updated_at = TimeNow;

                bool bresult1 = bll_miniyun_files.Add(model);

                #region 事件表
                ZK.Model.miniyun_events eventmodel = new ZK.Model.miniyun_events();
                eventmodel.user_id = Convert.ToInt32(toUserId);
                eventmodel.user_device_id = Convert.ToInt32(toUserId);//设备Id暂时是userId
                eventmodel.action = 0;
                eventmodel.file_path = folderpath + "/" + filename;
                eventmodel.context = folderpath + "/" + filename;  //"文件夹为path 文件为hash及大小版本等";
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
            catch (Exception ex)
            {
                hashpath = "";
                return ex.ToString();
            }
        }

        /// <summary>
        /// 添加新的文件或文件夹
        /// </summary>
        /// <param name="filename">文件或文件夹名</param>
        /// <param name="parent_id">父文件夹id</param>
        /// <param name="isfile">是否为文件 ture 文件 false  文件夹</param>
        /// <returns></returns>
        private static string AddNewFile(string userId, string filename, long file_size, string parent_id, string mime_type, out string hashpath)
        {
            DateTime TimeNow = DateTime.Now;
            hashpath = "";
            string folderpath = "/" + userId;

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
            List<ZK.Model.miniyun_files> listmodel = bll_miniyun_files.GetModelList(builder.ToString());
            if (listmodel != null && listmodel.Count > 0)
            {
                return "共享文件已存在";
            }

            model.file_type = 0;

            #region 版本表
            ZK.Model.miniyun_file_versions versionmodel = new ZK.Model.miniyun_file_versions();
            versionmodel.file_signature = ZK.Common.CommonFunction.GetRandomHashs(40);
            versionmodel.file_size = file_size;
            versionmodel.created_at = TimeNow;
            versionmodel.mime_type = mime_type;
            versionmodel.ref_count = 1;
            versionmodel.block_ids = "0";
            bll_miniyun_file_versions.Add(versionmodel);
            hashpath = versionmodel.file_signature;

            #endregion

            model.mime_type = mime_type;
            model.version_id = bll_miniyun_file_versions.GetMaxId() - 1;
            model.created_at = TimeNow;
            model.event_uuid = event_uuid;
            model.updated_at = TimeNow;
            //  model.version_id
            bool bresult1 = bll_miniyun_files.Add(model);

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

        //转换成MySql中的UserId
        private static int ChangeUserId(int userId)
        {
            int netUserId = userId;
            DataSet dsUser = bllUser.GetList("userid=" + Convert.ToInt32(userId));
            List<ZK.Model.USERS> listUser = bllUser.DataTableToList(dsUser.Tables[0]);
            string userName = listUser[0].USERNAME;

            DataSet dsNetUser = bll_miniyun_users.GetList("user_name='" + userName + "'");
            if (dsNetUser.Tables[0].Rows.Count > 0)
            {
                List<ZK.Model.miniyun_users> listNetUser = bll_miniyun_users.DataTableToList(dsNetUser.Tables[0]);
                netUserId = listNetUser[0].id;
            }
            return netUserId;
        }

        //检查是否已经建立了共享目标文件夹，如果没有，则建立并返回ID，如果已经建立，则取出其ID；
        private static int GetShareFolderId(int netToUserId, string userName)
        {
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
                string strResult = AddNewShareFolder(netToUserId.ToString(), folderName, 0, "0", "", out hashpath);
                if (strResult == "1")
                {
                    DataSet dsFolder = bll_miniyun_files.GetList(strSql);
                    List<ZK.Model.miniyun_files> listmdl = bll_miniyun_files.GetModelList(strSql);
                    folderId = listmdl[0].id;
                }
            }
            return folderId;
        }
        /// <summary>
        /// 判断某个文件是否曾经共享过
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static bool  bShareFolderId(string userName,ZK.Model.miniyun_files SourceFile)  
        {
            bool bReturn = false;
            string folderName = userName + "的共享";
            string strSql = "   file_name='" + folderName + "'  and file_type='3'";
            DataSet ds = bll_miniyun_files.GetList(strSql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                List<ZK.Model.miniyun_files> listfile = bll_miniyun_files.DataTableToList(ds.Tables[0]);
                foreach (ZK.Model.miniyun_files ms in listfile)
                {
                    int folderId = ms.id;
                    strSql = " parent_file_id={0}  and version_id={1} and file_name='{2}'";
                    strSql = string.Format(strSql, folderId, SourceFile.version_id, SourceFile.file_name);
                    DataSet dsIsHave = bll_miniyun_files.GetList(strSql);
                    if (dsIsHave.Tables[0].Rows.Count > 0)
                    {
                        bReturn = true;
                        break;
                    }
                }
            }
            return bReturn;
        }
        //转换User_id和name
        private static string GetUserId(int userid)
        {
            string idAndName = "";
            ZK.Model.miniyun_users mdl_miniyun_users = bll_miniyun_users.GetModel(userid);
            string userName = mdl_miniyun_users.user_name;
            List<ZK.Model.USERS> list = bllUser.GetModelList("USERNAME='" + userName + "'");
            if (list.Count > 0)
            {
                idAndName = list[0].USERID.ToString() + "_" + list[0].ACTUALNAME;
            }
            return idAndName;
        }

        //转换name
        private static string GetUserName(int userid)
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
        public static string SendShareInfo(string userName, string flag, string userIds)
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

        private static string GetFiveMetas(string str)
        {
            string metas_value = str.Replace("{","#").Replace("}","#");
            List<string> listnum = new List<string>();
            List<string> listvalue = new List<string>();
            string[] strmetas = metas_value.Split('#');
            string metasfirst = strmetas[0]+"{";
            string metasMiddle = "";
            for (int i = 1; i < strmetas.Length;i++ )
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
            metasMiddle = listnum[0] +"{"+ listvalue[0] +"}"+ listnum[1] +"{"+ listvalue[1] +"}"+ listnum[2] +"{"+ listvalue[2] +"}"+ listnum[3] +"{"+ listvalue[3] +"}";
            metas_value = metasfirst + metasMiddle;
            return metas_value;
        }

        #endregion

        #region 函数拆分 针对之前过长的函数进行拆分 by ao 2014-11-2
        /// <summary>
        /// 根据当前时间获取时间戳
        /// </summary>
        /// <returns></returns>
        private static long GetTimeStamp()
        {
            DateTime time = DateTime.Now;
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            DateTime dtNow = DateTime.Parse(DateTime.Now.ToString());
            TimeSpan toNow = dtNow.Subtract(dtStart);
            string timeStamp = toNow.Ticks.ToString();
            timeStamp = timeStamp.Substring(0, timeStamp.Length - 7);
            long timeLong = long.Parse(timeStamp);
            return timeLong;
        }
        #endregion
    }
}