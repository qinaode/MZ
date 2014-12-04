using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;

namespace ZK.Common
{
    public class DownLoad
    {
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="Response">Response 文本</param>
        /// <param name="filePath">文件路径</param>
        /// <param name="fileext">文件扩展名</param>
        /// <param name="filename">要显示的文件名</param>
        public static void DownLoadFile(HttpResponseBase Response, string filePath, string fileext, string filename)
        {

            //客户端保存的文件名

            //以字符流的形式下载文件
            FileStream fs = new FileStream(filePath, FileMode.Open);
            byte[] bytes = new byte[(int)fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();

            Response.ContentType = "application/octet-stream";
            //通知浏览器下载文件而不是打开
            //多文件下载需要清空
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8));
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Response"></param>
        /// <param name="filePath">文件绝对路径</param>
        /// <param name="filename">显示文件名</param>
        public static void DownLoadFile(HttpResponseBase Response, string filePath, string filename)
        {

            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
            Response.TransmitFile(filePath);
        }
    }

}
