using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Script.Serialization;
using System.Data;
using System.Text;

namespace ZK.Manage.ashx
{
    /// <summary>
    /// MoralManage_Resource 的摘要说明
    /// </summary>
    public class MoralManage_Resource : IHttpHandler
    {

        private ZK.BLL.ZK_FileList bllfile = new BLL.ZK_FileList();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string Flag = context.Request.Form["Flag"];
            string Result = string.Empty;
            //            this.GetType().GetMethod(Result).Invoke(this, new HttpContext[] { context });
            switch (Flag)
            {
                case "GetMoralResourceListPaging":
                    Result = GetMoralResourceListPaging(context);
                    break;
                case "GetMoralResourceList":
                    Result = GetMoralResourceList(context);
                    break;
                case "DeleteCheckedMoralResource":
                    Result = DeleteCheckedMoralResource(context);
                    break;
                case "Convert_Resource":
                    Result = Convert_Resource(context);
                    break;
                default:
                    Result = "";
                    break;
            }
            context.Response.Write(Result);
        }

        /// <summary>
        /// 获取教学资源列表 分页
        /// </summary>
        /// <param name="context"></param>
        /// <returns>教学资源列表分页数据</returns>
        private string GetMoralResourceListPaging(HttpContext context)
        {
            string PageIndex = context.Request.Form["PageIndex"] == "" ? "1" : context.Request.Form["PageIndex"];
            string PageSize = context.Request.Form["PageSize"] == "" ? "10" : context.Request.Form["PageSize"];
            string strWhere = context.Request.Form["strWhere"];
            string channelGroupName = context.Request.Form["channelGroupName"];
            string sourceids = GetchannelGroupIDsByCondition(channelGroupName);

            string fileIds = GetFileId(sourceids);

            List<ZK.Model.ZK_FileList> List = new List<Model.ZK_FileList>();
            StringBuilder builder = new StringBuilder();
            //builder.Append("fileID in (" + sourceids + ") ");
            builder.Append("fileID in (" + fileIds + ") ");
            if (strWhere != "")
            {
                builder.Append(" and fileName like '%" + strWhere + "%'");
            }

            int startindex = (Convert.ToInt32(PageIndex) - 1) * (Convert.ToInt32(PageSize)) + 1;
            int endindex = Convert.ToInt32(PageSize) * Convert.ToInt32(PageIndex);

            DataSet ds = bllfile.GetListByPage(builder.ToString(), "", startindex, endindex);
            List = bllfile.DataTableToList(ds.Tables[0]);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            int total = bllfile.GetRecordCount(builder.ToString());
            return SerializeJsonString(jss.Serialize(List), total);
        }

        /// <summary>
        /// 获取教学资源列表
        /// </summary>
        /// <param name="context"></param>
        /// <returns>教学资源列表</returns>
        private string GetMoralResourceList(HttpContext context)
        {
            List<ZK.Model.ZK_FileList> List = new List<Model.ZK_FileList>();
            string UserID = "1";
            string strWhere = " ownerID=" + UserID;
            List = bllfile.GetModelList(strWhere);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return SerializeJsonString(jss.Serialize(List), 0);
        }

        /// <summary>
        /// 删除选中的教学资源
        /// </summary>
        /// <param name="context"></param>
        /// <returns>返回删除结果 true false</returns>
        private string DeleteCheckedMoralResource(HttpContext context)
        {
            string ID = context.Request.Form["ID"];
            int rint = 0;
            if (int.TryParse(ID, out rint))
            {
                BLL.ZK_ChannelGroupAndFileList bllcf = new BLL.ZK_ChannelGroupAndFileList();
                List<Model.ZK_ChannelGroupAndFileList> lists = bllcf.GetModelList("fileid=" + rint.ToString());

                if (lists != null && lists.Count > 0)
                {
                    bool res = bllcf.Delete(lists[0].ID);
                    return res.ToString();
                }
                else
                {
                    return "false";
                }
            }
            else
            {
                return "false";
            }
        }
        /// <summary>
        /// 获取相应的groupid下的资源文件
        /// </summary>
        /// <param name="channelGroupID">"" 所有的groupid</param>
        /// <returns></returns>
        private string GetSourceIDs(string channelGroupID)
        {
            string res = "";
            string strWhere = "channelGroupID in (" + channelGroupID + ")";
            BLL.ZK_ChannelGroupAndFileList bllsource = new BLL.ZK_ChannelGroupAndFileList();
            List<Model.ZK_ChannelGroupAndFileList> lists = bllsource.GetModelList(strWhere);
            if (lists != null && lists.Count > 0)
            {
                for (int i = 0; i < lists.Count; i++)
                {
                    res += lists[i].fileID.ToString() + ",";
                }
                return res.TrimEnd(',');
            }
            else
            {
                return "0";
            }
        }

        private string GetFileId(string fileIds)
        {
            //string strWhere = " fileID in (select fileID from ZK_ChannelGroupAndFileList where channelGroupID in (select a.channelGroupID from ZK_ChannelGroupAndFileList a left join ZK_ChannelGroup b on a.channelGroupID=b.channelGroupID left join ZK_FileList c on a.fileID=c.fileID where b.channelID=2))";

            string strWhere = " fileID in (select fileID from ZK_ChannelGroupAndFileList where channelGroupID in (" + fileIds + "))";
            BLL.ZK_ChannelGroupAndFileList bllgroupAndFile = new BLL.ZK_ChannelGroupAndFileList();
            List<Model.ZK_ChannelGroupAndFileList> lists = bllgroupAndFile.GetModelList(strWhere);
            string listid = "";
            if (lists == null || lists.Count < 1)
            {
                return "0";
            }
            foreach (var item in lists)
            {
                listid += item.fileID + ",";
            }
            return listid.TrimEnd(',');
        }
        /// 文件转换 视频 1 word excel ppt
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string Convert_Resource(HttpContext context)
        {
            string result = "";
            string fileid = context.Request.Form["fileid"];
            string filetype = context.Request.Form["filetype"];
            int file_id = 0;

            if (!int.TryParse(fileid, out file_id))
            {
                return "false";
            }
            if (filetype == "1")
            {
                result = ConvertVideo(file_id, context);
            }
            else
            {
                result = ConvertDoc(file_id, context);
            }

            return result;
        }

        /// <summary>
        /// 视频转换
        /// </summary>
        /// <param name="fileid"></param>
        /// <param name="context"></param>
        private string ConvertVideo(int fileid, HttpContext context)
        {

            //查找要转换的文件
            Model.ZK_FileList file = new BLL.ZK_FileList().GetModel(fileid);
            if (file != null)
            {

                string hashfilename = "";
                string versionid = new BLL.miniyun_files().GetModel((int)file.fileOldID).version_id.ToString();
                string ResourcePath = context.Server.MapPath(Common.ModelSettings.CRootPath + "/" + GetFilePathByVersionID(versionid, out hashfilename) + "/" + hashfilename);
                if (ZK.Common.ModelSettings.IsModelData)
                {
                    ResourcePath = context.Server.MapPath(ZK.Common.ModelSettings.VideoPath);
                }
                string TargetPath = ResourcePath + ".flv";
                file.trastatus = 1;
                //修改 该文件的转换状态
                new BLL.ZK_FileList().Update(file);
                string ConvertResult = ZK.Common.VideoHelper.ConvertVideo_Behind(ResourcePath, TargetPath, context.Server.MapPath(ZK.Common.ModelSettings.FFmpegPath));
                if (ConvertResult == "success")
                    return "true";
            }
            return "false";
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

        private string ConvertDoc(int fileid, HttpContext context)
        {
            //获取文件的路径 从源路径中查找html页的路径
            Model.ZK_FileList filelistmodel = new BLL.ZK_FileList().GetModel(Convert.ToInt32(fileid));
            if (filelistmodel != null)
            {

                string versionid = new BLL.miniyun_files().GetModel((int)filelistmodel.fileOldID).version_id.ToString();
                string hashfilename = "";
                string LDPath = Common.ModelSettings.CRootPath + "/" + GetFilePathByVersionID(versionid, out hashfilename) + "/" + hashfilename;

                if (ParseDocumentToHtml(context.Server.MapPath(LDPath), System.IO.Path.GetExtension(filelistmodel.fileName).ToLower()) == "success")
                {
                    return "true";
                }

            }
            return "false";

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
        /// 通过条件获取相应的德育分类的id集合
        /// </summary>
        /// <returns></returns>
        private string GetchannelGroupIDsByCondition(string channelgroupname)
        {
            string strWhere = "channelID=2 ";
            if (channelgroupname != "")
            {
                strWhere += " and channelGroupName like '%" + channelgroupname + "%'";
            }
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
        private static string SerializeJsonString(string DataList, int TotalNumber)
        {

            return "{\"DataList\":" + DataList + ",\"TotalNumber\":" + TotalNumber + "}";
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