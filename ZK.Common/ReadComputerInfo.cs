using System.Management;
using System.Text;
using System.Security.Cryptography;
using System;
namespace ZK.Common
{
    public class ReadComputerInfo
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static string GetAllMechinData()
        {
            string strID = ZK.Common.StringPlus.StringToMD5(System.Web.HttpContext.Current.Request.Url.Host + ":" + System.Web.HttpContext.Current.Request.Url.Port + "hoolian");
            return strID;
        }
    }
}
