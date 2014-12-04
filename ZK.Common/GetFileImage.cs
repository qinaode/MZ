using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZK.Common
{
    public class GetFileImage
    {

        #region 获取列表页图片
        /// <summary>
        /// 获取列表页图片
        /// </summary>
        /// <param name="fileType">文件类型</param>
        /// <param name="imageUrl">数据库中保存地址</param>
        /// <returns></returns>
        public static string getListImage(int fileType, string imageUrl)
        {
            string nImageUrl = "/images/Icon/other.png";
            if (fileType == 1)
            {
                // nImageUrl = "/themes/defaultpic/video.png";
                nImageUrl = imageUrl;
            }
            else if (fileType == 2)
            {
                nImageUrl = "/themes/defaultpic/word/48.png";
            }
            else if (fileType == 3)
            {
                nImageUrl = imageUrl;
            }
            else if (fileType == 4)
            {
                nImageUrl = "/images/Icon/audio.png";
            }
            else if (fileType == 6)
            {
                nImageUrl = "/themes/defaultpic/excel/48.png";
            }
            else if (fileType == 7)
            {
                nImageUrl = "/themes/defaultpic/ppt/48.png";
            }
            else if (fileType == 8)
            {
                nImageUrl = "/images/Icon/pdf.jpg";
            }
            else if (fileType == 9)
            {
                nImageUrl = "/images/Icon/rar.png";
            }
            else if (fileType == 10)
            {
                nImageUrl = "/images/Icon/other.png";
            }

            return nImageUrl;
        }

        #endregion

        #region 获取展示页图片
        /// <summary>
        /// 获取展示页列表
        /// </summary>
        /// <param name="fileType">文件类型</param>
        /// <param name="fileURL">文件存储地址</param>
        /// <returns></returns>
        public static string getContentImage(int fileType, string fileURL)
        {


            string nImageUrl = "";

            if (fileType == 2)
            {
                nImageUrl = "/themes/defaultpic/word/word549X369.png";
            }
            else if (fileType == 3)
            {
                nImageUrl = fileURL;
            }

            else if (fileType == 6)
            {
                nImageUrl = "/themes/defaultpic/excel/excel549X369.png";
            }
            else if (fileType == 7)
            {
                nImageUrl = "/themes/defaultpic/ppt/ppt549X369.png";
            }
            else if (fileType == 8)
            {
                nImageUrl = "/images/Icon/pdf.jpg";
            }
            else if (fileType == 9)
            {
                nImageUrl = "/images/Icon/rar.png";
            }
            else if (fileType == 10)
            {
                nImageUrl = "/images/Icon/other.jpg";
            }

            return nImageUrl;
        }

        #endregion

        /// <summary>
        /// 文件柜 获取列表页图片 
        /// </summary>
        /// <param name="fileType"></param>
        /// <param name="imageUrl"></param>
        /// <returns></returns>
        public static string getListImageForDisk(int fileType)
        {
            string nImageUrl = "/themes/defaultpic/other.png";
            if (fileType == 1)
            {
                nImageUrl = "/themes/defaultpic/video.png";
            }
            else if (fileType == 2)
            {
                nImageUrl = "/themes/defaultpic/word/48.png";
            }
            else if (fileType == 3)
            {
                nImageUrl = "/themes/defaultpic/img.png";
            }
            else if (fileType == 4)
            {
                nImageUrl = "/themes/defaultpic/audio.png";
            }
            else if (fileType == 6)
            {
                nImageUrl = "/themes/defaultpic/excel/48.png";
            }
            else if (fileType == 7)
            {
                nImageUrl = "/themes/defaultpic/ppt/48.png";
            }
            else if (fileType == 8)
            {
                nImageUrl = "/themes/defaultpic/pdf.jpg";
            }
            else if (fileType == 9)
            {
                nImageUrl = "/themes/defaultpic/rar.png";
            }
            else if (fileType == 10)
            {
                nImageUrl = "/themes/defaultpic/other.png";
            }
            else if (fileType == 11)
            {
                nImageUrl = "/themes/defaultpic/other.jpg";
            }
            else if (fileType == 12)
            {
                nImageUrl = "/themes/defaultpic/share.jpg";
            }
            else if (fileType == 13)
            {
                nImageUrl = "/themes/defaultpic/oshare.png";//oshare.png
            }
            return nImageUrl;
        }

        /// <summary>
        /// 将mime_type 转化为相应的数字
        /// </summary>
        /// <param name="mime_type"></param>
        /// <returns></returns>
        public static string Con_mime_path_filetype(string mime_type)
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
                else if (mime_type.Split('/')[0] == "text")
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
                mime_type = "11";
            }
            return mime_type;
            // 1 视频  2 word  3 图片  4 音频  
            // 6 excel  7 ppt  8 pdf  9	rar  10 text  11（10	其他）
        }

        /// <summary>
        /// 将mime_type 转化为相应的数字
        /// </summary>
        /// <param name="mime_type"></param>
        /// <returns></returns>
        public static string Con_mime_path_filetype(string mime_type,string file_type)  
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
                else if (mime_type.Split('/')[0] == "text")
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
                mime_type = "11";
            }
            return mime_type;
            // 1 视频  2 word  3 图片  4 音频  
            // 6 excel  7 ppt  8 pdf  9	rar  10 text  11（10	其他）
        }

    }
}
