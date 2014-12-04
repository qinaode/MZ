using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Configuration;

namespace ZK.Common
{
    public class VideoHelper
    {

        /// <summary>
        /// 视频转换
        /// </summary>
        /// <param name="ResourceName">源文件完整路径</param>
        /// <param name="ConvertName">转换文件完整路径</param>
        /// <param name="FFmpegPath">ffmpeg的路径</param>
        /// <returns></returns>
        public static string ConvertVideo(string ResourceName, string ConvertName, string FFmpegPath)
        {
            //FormatConvertMedia(ResourceName, ConvertName, "320*480", "1000");

            string BitRate = "200";
            string VideoSize = "640*480";
            //VideoSize = "900*600";
            //VideoSize = "1280*720";
            BitRate = ConfigurationManager.AppSettings["BitRate"];
            VideoSize = ConfigurationManager.AppSettings["VideoSize"];
            //FFmpegPath = "d:\\ffmpeg64.exe";
            try
            {
                Process pConvert = new Process();
                pConvert.StartInfo.FileName = FFmpegPath;
                //参数 
                //支持的参数
                //-r 29.97  帧频 为了音视频同步 只可定义为 15 或者29.97 
                // -s 1600*900 分辨率
                //-ab 128（音频数据流量，一般选择32、64、96、128） 
                //不支持的参数
                //-qscale 0.01-255 数值越小质量越好 -title 
                string Arguments = "";
                //这一条参数 可以转换 wmv avi mp4
                //Arguments = " -y -i " + ResourceName + " -s 1600*900 -r 29.97  " + ConvertName;
                //这一条参数 可以转换 wmv avi mp4 flv swf
                // -ar 22050 20000
                //不影响大小的参数 -ab -ar  
                //Arguments = "-y -i " + filepath + " -f mp4 -vcodec mpeg4 -acodec libfaac -ac 2 -ab 64K -ar 48000 " + flvpath; 
                //Arguments = "-y -i " + filepath + " -ab 32 -ar 22050 -b 200 k -r 15 -s 320*480 " + flvpath;
                // Arguments="-y -i "+ResourceName+" -f psp -vcodec h264 -vlevel 13 -b 200 -qmin 1 -qmax 51 -s 320x240  -acodec aac -ab 64 -ar 44100 -ac 2 "+ConvertName;
                //Arguments = "-y -i d:\\Video1.wmv -bitexact -vcodec h263 -b 128 -r 15 -s 176x144 -acodec aac -ac 2 -ar 22500 -ab 24 -f 3gp d:\\test.3gp ";
                //Arguments = "-y -i d:\\Video1.wmv -ab 32 -ar 22050 -b 200 k -r 15 -s 640*480 d:\\test.flv";
                Arguments = "-y -i " + ResourceName + " -ab 32 -ar 22050 -b " + BitRate + "k -r 15 -s " + VideoSize + " " + ConvertName;
                pConvert.StartInfo.Arguments = Arguments;
                pConvert.StartInfo.UseShellExecute = false;
                pConvert.StartInfo.RedirectStandardError = true;
                pConvert.StartInfo.RedirectStandardOutput = true;
                pConvert.StartInfo.CreateNoWindow = true;
                DateTime t1 = DateTime.Now;
                pConvert.Start();
                pConvert.BeginOutputReadLine();
                pConvert.BeginErrorReadLine();
                pConvert.WaitForExit();
                pConvert.Close();
                pConvert.Dispose();
                DateTime t2 = DateTime.Now;
                TimeSpan ts = t2 - t1;
                //if (ts.TotalSeconds <= 1)
                //{
                //    return "fail";
                //}
                return "success";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }

        /// <summary>
        /// 视频转换
        /// </summary>
        /// <param name="ResourceName">源文件完整路径</param>
        /// <param name="ConvertName">转换文件完整路径</param>
        /// <param name="FFmpegPath">ffmpeg的路径</param>
        /// <returns></returns>
        public static string ConvertVideo_Behind(string ResourceName, string ConvertName, string FFmpegPath)
        {
            //FormatConvertMedia(ResourceName, ConvertName, "320*480", "1000");

            string BitRate = "200";
            string VideoSize = "640*480";
            //VideoSize = "900*600";
            //VideoSize = "1280*720";
            BitRate = ConfigurationManager.AppSettings["BitRate"];
            VideoSize = ConfigurationManager.AppSettings["VideoSize"];
            //FFmpegPath = "d:\\ffmpeg64.exe";
            try
            {
                Process pConvert = new Process();
                pConvert.StartInfo.FileName = FFmpegPath;
                //参数 
                //支持的参数
                //-r 29.97  帧频 为了音视频同步 只可定义为 15 或者29.97 
                // -s 1600*900 分辨率
                //-ab 128（音频数据流量，一般选择32、64、96、128） 
                //不支持的参数
                //-qscale 0.01-255 数值越小质量越好 -title 
                string Arguments = "";
                //这一条参数 可以转换 wmv avi mp4
                //Arguments = " -y -i " + ResourceName + " -s 1600*900 -r 29.97  " + ConvertName;
                //这一条参数 可以转换 wmv avi mp4 flv swf
                // -ar 22050 20000
                //不影响大小的参数 -ab -ar  
                //Arguments = "-y -i " + filepath + " -f mp4 -vcodec mpeg4 -acodec libfaac -ac 2 -ab 64K -ar 48000 " + flvpath; 
                //Arguments = "-y -i " + filepath + " -ab 32 -ar 22050 -b 200 k -r 15 -s 320*480 " + flvpath;
                // Arguments="-y -i "+ResourceName+" -f psp -vcodec h264 -vlevel 13 -b 200 -qmin 1 -qmax 51 -s 320x240  -acodec aac -ab 64 -ar 44100 -ac 2 "+ConvertName;
                //Arguments = "-y -i d:\\Video1.wmv -bitexact -vcodec h263 -b 128 -r 15 -s 176x144 -acodec aac -ac 2 -ar 22500 -ab 24 -f 3gp d:\\test.3gp ";
                //Arguments = "-y -i d:\\Video1.wmv -ab 32 -ar 22050 -b 200 k -r 15 -s 640*480 d:\\test.flv";
                Arguments = "-y -i " + ResourceName + " -ab 32 -ar 22050 -b " + BitRate + "k -r 15 -s " + VideoSize + " " + ConvertName;
                pConvert.StartInfo.Arguments = Arguments;
                pConvert.StartInfo.UseShellExecute = false;
                pConvert.StartInfo.RedirectStandardError = true;
                pConvert.StartInfo.RedirectStandardOutput = true;
                pConvert.StartInfo.CreateNoWindow = true;
                DateTime t1 = DateTime.Now;
                pConvert.Start();
                pConvert.BeginOutputReadLine();
                pConvert.BeginErrorReadLine();
               // pConvert.WaitForExit();
                pConvert.Close();
                pConvert.Dispose();
                DateTime t2 = DateTime.Now;
                TimeSpan ts = t2 - t1;
                //if (ts.TotalSeconds <= 1)
                //{
                //    return "fail";
                //}
                return "success";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }
        /// <summary>
        /// 截图
        /// </summary>
        /// <param name="ResourceName">源文件完整路径</param>
        /// <param name="ConvertName">转换文件完整路径</param>
        /// <param name="FFmpegPath">ffmpeg的路径</param>
        /// <returns></returns>
        public static string CutImage(string ResourceName, string ConvertName, string FFmpegPath)
        {

            try
            {
                Process pConvert = new Process();
                pConvert.StartInfo.FileName = FFmpegPath;

                string Arguments = "";
                Arguments = " -i " + ResourceName + " -y -f  image2  -ss 5 -vframes 1  " + ConvertName;
                pConvert.StartInfo.Arguments = Arguments;
                pConvert.StartInfo.UseShellExecute = false;
                pConvert.StartInfo.RedirectStandardError = true;
                pConvert.StartInfo.RedirectStandardOutput = true;
                pConvert.StartInfo.CreateNoWindow = true;
                pConvert.Start();
                pConvert.BeginOutputReadLine();
                pConvert.BeginErrorReadLine();
                pConvert.WaitForExit();
                pConvert.Close();
                pConvert.Dispose();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
