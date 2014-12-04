using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Core;
using System.IO;
using System.Security.Cryptography;

namespace ZK.Common
{
    public class CommonFunction
    {
        /// <summary>
        /// 获取不带扩展名的路径
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static string GetPathWithoutExtension(string filepath)
        {
            string ext = System.IO.Path.GetExtension(filepath);
            return filepath.Substring(0, filepath.Length - ext.Length);
        }


        /// <summary>
        /// 截取ppt的第一张图片
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        private static string GetPPTImage(string filepath)
        {

            string imagepath = System.IO.Path.GetFileNameWithoutExtension(filepath) + ".jpg";

            try
            {
                Microsoft.Office.Interop.PowerPoint.Application pptapplication = null;
                pptapplication = new Microsoft.Office.Interop.PowerPoint.Application();
                Microsoft.Office.Interop.PowerPoint.Presentation ppt1 = pptapplication.Presentations.Open(filepath, MsoTriState.msoFalse, MsoTriState.msoFalse, MsoTriState.msoFalse);
                ppt1.Slides[1].Export(imagepath, "jpg", 480, 320);
                //关闭
                ppt1.Close();
                pptapplication.Quit();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }


        #region 复制文件 或 文件夹
        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="Rpaths">原文件地址集合</param>
        /// <param name="Tpaths">复制文件集合</param>
        /// <returns>1 true   2 false   3 ex.ToString()</returns>
        public static string CopyFiles(List<string> Rpaths, List<string> Tpaths)
        {
            try
            {
                if (Rpaths.Count != Tpaths.Count)
                {
                    return "false";
                }
                for (int i = 0; i < Rpaths.Count; i++)
                {
                    System.IO.File.Copy(Rpaths[i], Tpaths[i], true);
                }
                return "true";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        /// <summary>
        /// 复制文件夹
        /// </summary>
        /// <param name="DRpath">源文件夹名</param>
        /// <param name="DTpath">目标文件夹名</param>
        /// <returns></returns>
        public static string CopyDirectory(string DRpath, string DTpath)
        {
            List<string> listfilename = new List<string>();
            List<string> DTfileNames = new List<string>();
            foreach (var item in System.IO.Directory.GetFiles(DRpath))
            {
                listfilename.Add(item);
                DTfileNames.Add(DTpath + "/" + System.IO.Path.GetFileName(item));
            }
            System.IO.Directory.CreateDirectory(DTpath);
            return CopyFiles(listfilename, DTfileNames);
        }

        #endregion

        /// <summary>
        /// 通过扩展名 获得该文件的mime_type
        /// </summary>
        /// <param name="extname">扩展名</param>
        /// <returns>mime_type</returns>
        public static string ExtTomimetype(string extname)
        {
            Dictionary<string, string> dictype = new Dictionary<string, string>();

            #region 类型字典

            dictype.Add("txt", "text/plain");
            dictype.Add("htm", "text/html");
            dictype.Add("html", "text/html");
            dictype.Add("php", "text/html");
            dictype.Add("css", "text/css");
            dictype.Add("js", "application/javascript");
            dictype.Add("json", "application/json");
            dictype.Add("xml", "application/xml");
            dictype.Add("swf", "application/x-shockwave-flash");
            dictype.Add("flv", "video/x-flv");

            //image
            dictype.Add("bmp", "image/bmp");
            dictype.Add("cod", "image/cis-cod");
            dictype.Add("gif", "image/gif");
            dictype.Add("ief", "image/ief");
            dictype.Add("jpe", "image/jpeg");
            dictype.Add("jpg", "image/jpeg");
            dictype.Add("jpeg", "image/jpeg");
            dictype.Add("jfif", "image/pipeg");
            dictype.Add("svg", "image/svg+xml");
            dictype.Add("tif", "image/tiff");
            dictype.Add("tiff", "image/tiff");
            dictype.Add("ras", "image/x-cmu-raster");
            dictype.Add("cmx", "image/x-cmx");
            dictype.Add("ico", "image/x-icon");
            dictype.Add("png", "image/png");
            dictype.Add("pnm", "image/x-portable-anymap");
            dictype.Add("pbm", "image/x-portable-bitmap");
            dictype.Add("pgm", "image/x-portable-graymap");
            dictype.Add("ppm", "image/x-portable-pixmap");
            dictype.Add("rgb", "image/x-rgb");
            dictype.Add("xbm", "image/x-xbitmap");
            dictype.Add("xpm", "image/x-xpixmap");
            dictype.Add("xwd", "image/x-xwindowdump");
            dictype.Add("svgz", "image/svg+xml");

            //archives
            dictype.Add("zip", "application/zip");
            dictype.Add("rar", "application/x-rar-compressed");
            dictype.Add("exe", "application/x-msdownload");
            dictype.Add("msi", "application/x-msdownload");
            dictype.Add("cab", "application/vnd.ms-cab-compressed");

            //audio
            dictype.Add("au", "audio/basic");
            dictype.Add("snd", "audio/basic");
            dictype.Add("mid", "audio/mid");
            dictype.Add("rmi", "audio/mid");
            dictype.Add("mp3", "audio/mpeg");
            dictype.Add("aif", "audio/x-aiff");
            dictype.Add("aifc", "audio/x-aiff");
            dictype.Add("aiff", "audio/x-aiff");
            dictype.Add("m3u", "audio/x-mpegurl");
            dictype.Add("ra", "audio/x-pn-realaudio");
            dictype.Add("ram", "audio/x-pn-realaudio");
            dictype.Add("wav", "audio/x-wav");
            dictype.Add("ape", "audio/x-monkeys-audio");
            dictype.Add("wma", "audio/x-ms-wma");
            dictype.Add("wvx", "audio/x-ms-wvx");

            //video
            dictype.Add("mp4", "video/mp4");
            dictype.Add("qt", "video/quicktime");
            dictype.Add("3gp", "video/3gpp");
            dictype.Add("wmv", "video/x-ms-wmv");
            dictype.Add("avi", "video/x-msvideo");
            dictype.Add("mp2", "video/mpeg");
            dictype.Add("mpa", "video/mpeg");
            dictype.Add("mpe", "video/mpeg");
            dictype.Add("mpeg", "video/mpeg");
            dictype.Add("mpg", "video/mpeg");
            dictype.Add("mpv2", "video/mpeg");
            dictype.Add("mov", "video/quicktime");
            dictype.Add("lsf", "video/x-la-asf");
            dictype.Add("lsx", "video/x-la-asf");
            dictype.Add("asf", "video/x-ms-asf");
            dictype.Add("asr", "video/x-ms-asf");
            dictype.Add("asx", "video/x-ms-asf");
            dictype.Add("movie", "video/x-sgi-movie");
            dictype.Add("rmvb", "video/vnd.rn-realvideo");
            dictype.Add("rm", "video/vnd.rn-realvideo");
            dictype.Add("viv", "video/vnd.vivo");
            dictype.Add("vivo", "video/vnd.vivo");

            // adobe
            dictype.Add("pdf", "application/pdf");
            dictype.Add("psd", "image/vnd.adobe.photoshop");
            dictype.Add("ai", "application/postscript");
            dictype.Add("eps", "application/postscript");
            dictype.Add("ps", "application/postscript");

            // ms office
            dictype.Add("doc", "application/msword");
            dictype.Add("docx", "application/msword");
            dictype.Add("rtf", "application/rtf");
            dictype.Add("xls", "application/msexcel");
            dictype.Add("xlsx", "application/msexcel");
            dictype.Add("ppt", "application/mspowerpoint");
            dictype.Add("pptx", "application/mspowerpoint");

            //open office
            dictype.Add("odt", "application/vnd.oasis.opendocument.text");
            dictype.Add("ods", "application/vnd.oasis.opendocument.spreadsheet");

            //  apk
            dictype.Add("apk", "application/vnd.android.package-archive");

            #endregion
            if (dictype.Keys.Contains(extname))
            {
                return dictype[extname];
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 把页容量和index转化为起始和结束页
        /// </summary>
        /// <param name="pageindex">index</param>
        /// <param name="pagesize">页容量</param>
        /// <param name="startpager">起始数据</param>
        /// <param name="endpager">结束数据</param>
        public static void GetStartAndEndPager(int pageindex, int pagesize, out int startpager, out int endpager)
        {
            startpager = (pageindex - 1) * pagesize + 1;
            endpager = pageindex * pagesize;
        }

        /// <summary>
        /// 转换文件typeid 
        /// </summary>
        /// <param name="mime_type"></param>
        /// <param name="filelistmodel"></param>
        /// <returns>filetypeid</returns>
        public static string GetTypeByMimeType(string mime_type)
        {

            if (!string.IsNullOrEmpty(mime_type))
            {

                //文档分类
                if (mime_type.Split('/')[0] == "application")
                {
                    string doctype = mime_type.Split('/')[1];
                    switch (doctype)
                    {
                        case "mspowerpoint":
                            mime_type = "7";
                            break;
                        case "msword":
                            mime_type = "2";
                            break;
                        case "msexcel":
                            mime_type = "6";
                            break;
                        case "pdf":
                            mime_type = "8";
                            break;
                        default:
                            mime_type = "10";
                            break;
                    }
                    //rar
                    if (new string[] { "zip", "x-rar-compressed", "x-msdownload", "x-msdownload", "vnd.ms-cab-compressed" }.Contains(doctype))
                    {
                        mime_type = "9";
                    }
                }//视频
                else if (mime_type.Split('/')[0] == "video")
                {
                    mime_type = "1";
                }
                else if (mime_type.Split('/')[0] == "audio")
                {
                    mime_type = "4";
                }
                // 图片
                else if (mime_type.Split('/')[0] == "image")
                {
                    mime_type = "3";
                }
                //text
                else if (mime_type.Split('/')[0] == "txt")
                {
                    mime_type = "10";
                }
                else
                {
                    mime_type = "10";
                }
            }
            else
            {
                mime_type = "10";
            }
            return mime_type;
        }

        /// <summary>
        /// 获取所需位数的随机数字和字母组合
        /// </summary>
        /// <param name="num">所需长度</param>
        /// <returns></returns>
        public static string GetRandomHashs(int num)
        {
            string[] strs = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
            StringBuilder builder = new StringBuilder();
            Random r = new Random();
            for (int i = 0; i < num; i++)
            {

                int rindex = r.Next(0, strs.Length);
                builder.Append(strs[rindex]);
            }
            return builder.ToString();
        }

        public static string StreamToMd5(Stream stream)
        {
            StringBuilder sb = new StringBuilder();
            MD5 md5 = new MD5CryptoServiceProvider();
            var bs=md5.ComputeHash(stream);
            foreach (var b in bs)
            {
                sb.Append(b.ToString("x0"));
            }
            return sb.ToString();
        }
    }
}
