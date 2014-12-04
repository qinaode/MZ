using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ZK.Controllers
{
    public class ConvertVideoController : Controller
    {
        //转码状态   0 未转吗 1 正在转码 2 已转码 3 转码失败
        //是否能转码 0 不能   1 能
        public ActionResult Index()
        {
            BLL.ZK_FileListTra bll_filelisttra = new BLL.ZK_FileListTra();
            List<Model.ZK_FileListTra> listmodels = bll_filelisttra.GetModelList(" 1=1 order by createtime ");
            if (listmodels != null && listmodels.Count > 0)
            {
                int fileid = (int)listmodels[0].fileID;
                //查找要转换的文件
                Model.ZK_FileList file = new BLL.ZK_FileList().GetModel(fileid);
                if (file != null)
                {
                    if (file.trastatus == 0)
                    {
                        string hashfilename = "";
                        string versionid = new BLL.miniyun_files().GetModel((int)file.fileOldID).version_id.ToString();
                        string ResourcePath = Server.MapPath(Common.ModelSettings.CRootPath + "/" + GetFilePathByVersionID(versionid, out hashfilename) + "/" + hashfilename);
                        if (ZK.Common.ModelSettings.IsModelData)
                        {
                            ResourcePath = Server.MapPath(ZK.Common.ModelSettings.VideoPath);
                        }
                        string TargetPath = ResourcePath + ".flv";
                        file.trastatus = 1;
                        //修改 该文件的转换状态
                        new BLL.ZK_FileList().Update(file);
                        string ConvertResult = ZK.Common.VideoHelper.ConvertVideo(ResourcePath, TargetPath, Server.MapPath(ZK.Common.ModelSettings.FFmpegPath));
                        if (ConvertResult == "success")
                        {
                            file.trastatus = 2;
                        }
                        else//如果转换失败 则改该格式为不支持 转换状态为失败
                        {
                            file.isTraf = 0;
                            file.trastatus = 3;
                        }
                    }
                    else if (file.trastatus == 2 || file.trastatus == 3)
                    {
                        file.isTraf = 0;
                    }
                    //修改 该文件的转换状态
                    new BLL.ZK_FileList().Update(file);
                    List<Model.ZK_FileListTra> tramodel = bll_filelisttra.GetModelList(" fileid=" + file.fileID.ToString());
                    bll_filelisttra.Delete(tramodel[0].id);

                }

            }
            //
            return View();
        }

        //测试
        public ActionResult test()
        {
            return View();
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

    }
}
