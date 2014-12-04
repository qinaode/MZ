using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Globalization;

namespace ZK.Common
{
    public class UploadHelper
    {
        /// <summary>
        /// 上传,返回上传后的服务器端路径
        /// </summary>
        /// <param name="postFile"></param>
        /// <returns>上传后服务器地址</returns>
        public static string Upload(HttpPostedFile postFile)
        {

            //文件保存目录路径
            String savePath = "/Uploads/";
            //文件保存目录URL
            String saveUrl = "/Uploads/";
            //保存目录绝对路径
            String dirPath = HttpContext.Current.Server.MapPath(savePath);

            string ymd = DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
            dirPath += ymd + "/";
            saveUrl += ymd + "/";
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            string fileName = postFile.FileName;
            string fileExt = Path.GetExtension(fileName).ToLower();
            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + fileExt;
            string filePath = dirPath + newFileName;

            postFile.SaveAs(filePath);

            string fileUrl = saveUrl + newFileName;

            return fileUrl;
        }

        /// <summary>
        /// 验证文件合法性 只允许图片
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool ValidateFileType(string fileName)
        {
            string fileTypes = "gif,jpg,jpeg,png,bmp";



            string fileExt = Path.GetExtension(fileName).ToLower();



            if (string.IsNullOrEmpty(fileExt) || Array.IndexOf(fileTypes.Split(','), fileExt.Substring(1).ToLower()) == -1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
