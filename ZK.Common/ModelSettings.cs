using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.SessionState;
namespace ZK.Common
{
    public class ModelSettings : IRequiresSessionState
    {


        //是否启用模拟数据 如果不启用（false)则采用实际数据
        public static bool IsModelData = false;
        #region 模拟数据

        //视频file_path
        public static string LVpath = "/Files/Video1.wmv";

        //图片的file_path
        public static string LIpath = "/Files/TestImg.jpg";

        //文档的file_path
        public static string LDPath = "/Files/miniyun.docx";

        //视频的 file_path 转码时使用
        public static string VideoPath = "/Files/Video1.wmv";

        //视频的 转换后的路径
        public static string ConvertPath = "/Files/Videos/Video1.flv";
        //PPT的 file_path
        public static string LPpath = "/Files/test.pptx";

        //转换文档同一根目录
        public static string CRootPath = "/Files/nfiles";

        #endregion

        #region 默认数据 前台

        //前台网站名称 仅有提取 不作为设置使用
        public static string Pre_Name = "ZK.MVCWeb";

        //默认imageurl初始化值
        public static string imageURL = "default";

        //默认 ffmpeg存放路径
        public static string FFmpegPath = "/Files/Tools/ffmpeg.exe";

        //默认资源排行榜的显示量
        public static int ResourceCountList = 5;

        //默认贡献排行榜的显示量
        public static int UserResCountRank = 5;

        //默认最近浏览文件的数量
        public static int VisitedFilesCount = 10;

        //默认网站配置文件 systemSetting.xml路径
        //前台
        public static string Pre_SysSettingXMLPath = "/Files/XML/SystemSetting.xml";


        //默认网站logo存放地址
        //前台
        public static string Pre_LogoPath = "/Files/Logo/WebLogo.jpg";

        public static string Video_LogoPath = "/Scripts/CuPlayerMiniV4/images/Logo.png";

        //激活码xml位置
        public static string ActiveCodeXML = "/Files/XML/hoolianActiveCode.xml";
        #region 文件柜

        //文件柜 默认文件名显示字符数
        public static int FileNameStrCount = 5;

        //文件柜 每页显示的文件列表的条数
        public static int DiskFileListCount = 12;

        
        // 此路径为文件实际存放的根目录
         public static string CreateFileDefaultPath = "/Files/Nfiles";
         //文件柜  默认文件夹和文件的创建地址
        //public static string CreateFileDefaultPath = "/"+System.Web.HttpContext.Current.Session["uid"].ToString();
        //文件柜 默认文件打包级别
        public static int CompressionLevel = 5;

        //文件柜 默认文件打包沉睡时间(毫秒)
        public static int SleepTimer = 100;

        public static string TempFilesDefaultPath = "/Files/Nfiles/TempFiles";
        #endregion


        #endregion

        #region 默认数据 后台

        //后台网站名称 仅有提取 不作为设置使用
        public static string BH_Name = "ZK.Manager";

        //默认网站配置文件 systemSetting.xml路径
        //后台
        public static string BH_SysSettingXMLPath = "/Files/XML/SystemSetting.xml";

        //默认网站logo存放地址
        //后台
        public static string BH_LogoPath = "/Files/Logo/WebLogo.jpg";


        //后台德育页面幻灯片的图片存储的 XML文件存放的目录
        public static string XMLFilePath = "/files/bfiles/Moral/Images.xml";
        //后台德育页面幻灯片使用的XML文档的节点。
        public static string XMLNodePath = "Images/Moral/img";


        #endregion


        //log
        /*
         * 需复制到前台的数据
         *           复制专题文件夹和图片 SpecialImage 
         *           logo
         *           moral文件夹
         */



    }
}
