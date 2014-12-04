using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;

namespace ZK.Common
{
    public class LogHelper
    {
        /// <summary>
        /// 保存日志文件
        /// </summary>
        /// <param name="_num">日志号</param>
        /// <param name="UserNum">用户ID</param>
        /// <param name="Title">日志标题</param>
        /// <param name="Content">日志内容</param>
        public static void saveLogFiles(int _num, string UserNum, string Title, string Content)
        {
            StreamWriter sw = null;
            DateTime date = DateTime.Now;
            string FileName = date.Year + "-" + date.Month;
            try
            {
                FileName = HttpContext.Current.Server.MapPath("~/Logs/User-" + _num + "-") + FileName + "-" + ZK.Common.StringPlus.StringToMD5(FileName) + "-s.log";

                #region 检测日志目录是否存在
                if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Logs")))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Logs"));
                }

                if (!File.Exists(FileName))
                    sw = File.CreateText(FileName);
                else
                {
                    sw = File.AppendText(FileName);
                }
                #endregion

                sw.WriteLine("IP                 :" + HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString() + "\r");
                sw.WriteLine("title              :" + Title + "\r");
                sw.WriteLine("content            :" + Content);
                sw.WriteLine("usernum&UserName   :" + UserNum + "\r");
                sw.WriteLine("Time               :" + System.DateTime.Now);
                sw.WriteLine("≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡\r");
                sw.Flush();
            }
            finally
            {
                if (sw != null)
                    sw.Close();
            }
        }
    }
}
