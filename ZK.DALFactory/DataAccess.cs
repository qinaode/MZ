using System;
using System.Reflection;
using System.Configuration;
using ZK.IDAL;
namespace ZK.DALFactory
{
    /// <summary>
    /// 抽象工厂模式创建DAL。
    /// web.config 需要加入配置：(利用工厂模式+反射机制+缓存机制,实现动态创建不同的数据层对象接口) 
    /// DataCache类在导出代码的文件夹里
    /// <appSettings> 
    /// <add key="DAL" value="ZK.SQLServerDAL" /> (这里的命名空间根据实际情况更改为自己项目的命名空间)
    /// </appSettings> 
    /// </summary>
    public sealed class DataAccess//<t>
    {
        private static readonly string AssemblyPath = ConfigurationManager.AppSettings["DAL"];
        /// <summary>
        /// 创建对象或从缓存获取
        /// </summary>
        public static object CreateObject(string AssemblyPath, string ClassNamespace)
        {
            object objType = DataCache.GetCache(ClassNamespace);//从缓存读取
            if (objType == null)
            {
                try
                {
                    objType = Assembly.Load(AssemblyPath).CreateInstance(ClassNamespace);//反射创建
                    DataCache.SetCache(ClassNamespace, objType);// 写入缓存
                }
                catch
                { }
            }
            return objType;
        }
        /// <summary>
        /// 创建数据层接口
        /// </summary>
        //public static t Create(string ClassName)
        //{
        //string ClassNamespace = AssemblyPath +"."+ ClassName;
        //object objType = CreateObject(AssemblyPath, ClassNamespace);
        //return (t)objType;
        //}
        /// <summary>
        /// 创建ACCOUNTS数据层接口。
        /// </summary>
        public static ZK.IDAL.IACCOUNTS CreateACCOUNTS()
        {

            string ClassNamespace = AssemblyPath + ".ACCOUNTS";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IACCOUNTS)objType;
        }


        /// <summary>
        /// 创建ACCOUNTSECTIONS数据层接口。
        /// </summary>
        public static ZK.IDAL.IACCOUNTSECTIONS CreateACCOUNTSECTIONS()
        {

            string ClassNamespace = AssemblyPath + ".ACCOUNTSECTIONS";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IACCOUNTSECTIONS)objType;
        }


        /// <summary>
        /// 创建ADMINLOGS数据层接口。
        /// </summary>
        public static ZK.IDAL.IADMINLOGS CreateADMINLOGS()
        {

            string ClassNamespace = AssemblyPath + ".ADMINLOGS";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IADMINLOGS)objType;
        }


        /// <summary>
        /// 创建ADMINS数据层接口。
        /// </summary>
        public static ZK.IDAL.IADMINS CreateADMINS()
        {

            string ClassNamespace = AssemblyPath + ".ADMINS";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IADMINS)objType;
        }


        /// <summary>
        /// 创建CONTACTGROUPS数据层接口。
        /// </summary>
        public static ZK.IDAL.ICONTACTGROUPS CreateCONTACTGROUPS()
        {

            string ClassNamespace = AssemblyPath + ".CONTACTGROUPS";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.ICONTACTGROUPS)objType;
        }


        /// <summary>
        /// 创建CONTACTS数据层接口。
        /// </summary>
        public static ZK.IDAL.ICONTACTS CreateCONTACTS()
        {

            string ClassNamespace = AssemblyPath + ".CONTACTS";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.ICONTACTS)objType;
        }


        /// <summary>
        /// 创建DEPARTMENTS数据层接口。
        /// </summary>
        public static ZK.IDAL.IDEPARTMENTS CreateDEPARTMENTS()
        {

            string ClassNamespace = AssemblyPath + ".DEPARTMENTS";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IDEPARTMENTS)objType;
        }


        /// <summary>
        /// 创建GROUPMEMBERS数据层接口。
        /// </summary>
        public static ZK.IDAL.IGROUPMEMBERS CreateGROUPMEMBERS()
        {

            string ClassNamespace = AssemblyPath + ".GROUPMEMBERS";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IGROUPMEMBERS)objType;
        }


        /// <summary>
        /// 创建GROUPS数据层接口。
        /// </summary>
        public static ZK.IDAL.IGROUPS CreateGROUPS()
        {

            string ClassNamespace = AssemblyPath + ".GROUPS";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IGROUPS)objType;
        }


        /// <summary>
        /// 创建GROUPSHAREFILES数据层接口。
        /// </summary>
        public static ZK.IDAL.IGROUPSHAREFILES CreateGROUPSHAREFILES()
        {

            string ClassNamespace = AssemblyPath + ".GROUPSHAREFILES";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IGROUPSHAREFILES)objType;
        }


        /// <summary>
        /// 创建JOINGROUPREQUESTS数据层接口。
        /// </summary>
        public static ZK.IDAL.IJOINGROUPREQUESTS CreateJOINGROUPREQUESTS()
        {

            string ClassNamespace = AssemblyPath + ".JOINGROUPREQUESTS";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IJOINGROUPREQUESTS)objType;
        }


        /// <summary>
        /// 创建JOINGROUPRESPONSES数据层接口。
        /// </summary>
        public static ZK.IDAL.IJOINGROUPRESPONSES CreateJOINGROUPRESPONSES()
        {

            string ClassNamespace = AssemblyPath + ".JOINGROUPRESPONSES";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IJOINGROUPRESPONSES)objType;
        }


        /// <summary>
        /// 创建JOINREQUESTS数据层接口。
        /// </summary>
        public static ZK.IDAL.IJOINREQUESTS CreateJOINREQUESTS()
        {

            string ClassNamespace = AssemblyPath + ".JOINREQUESTS";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IJOINREQUESTS)objType;
        }


        /// <summary>
        /// 创建JOINRESPONSES数据层接口。
        /// </summary>
        public static ZK.IDAL.IJOINRESPONSES CreateJOINRESPONSES()
        {

            string ClassNamespace = AssemblyPath + ".JOINRESPONSES";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IJOINRESPONSES)objType;
        }


        /// <summary>
        /// 创建OFFGROUPMSGS数据层接口。
        /// </summary>
        public static ZK.IDAL.IOFFGROUPMSGS CreateOFFGROUPMSGS()
        {

            string ClassNamespace = AssemblyPath + ".OFFGROUPMSGS";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IOFFGROUPMSGS)objType;
        }


        /// <summary>
        /// 创建OFFGROUPSUPEROBJS数据层接口。
        /// </summary>
        public static ZK.IDAL.IOFFGROUPSUPEROBJS CreateOFFGROUPSUPEROBJS()
        {

            string ClassNamespace = AssemblyPath + ".OFFGROUPSUPEROBJS";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IOFFGROUPSUPEROBJS)objType;
        }


        /// <summary>
        /// 创建OFFLINEFILES数据层接口。
        /// </summary>
        public static ZK.IDAL.IOFFLINEFILES CreateOFFLINEFILES()
        {

            string ClassNamespace = AssemblyPath + ".OFFLINEFILES";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IOFFLINEFILES)objType;
        }


        /// <summary>
        /// 创建OFFLINEMESSAGES数据层接口。
        /// </summary>
        public static ZK.IDAL.IOFFLINEMESSAGES CreateOFFLINEMESSAGES()
        {

            string ClassNamespace = AssemblyPath + ".OFFLINEMESSAGES";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IOFFLINEMESSAGES)objType;
        }


        /// <summary>
        /// 创建OFFLINESUPEROBJECTS数据层接口。
        /// </summary>
        public static ZK.IDAL.IOFFLINESUPEROBJECTS CreateOFFLINESUPEROBJECTS()
        {

            string ClassNamespace = AssemblyPath + ".OFFLINESUPEROBJECTS";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IOFFLINESUPEROBJECTS)objType;
        }


        /// <summary>
        /// 创建PWDPROTECTION数据层接口。
        /// </summary>
        public static ZK.IDAL.IPWDPROTECTION CreatePWDPROTECTION()
        {

            string ClassNamespace = AssemblyPath + ".PWDPROTECTION";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IPWDPROTECTION)objType;
        }


        /// <summary>
        /// 创建RECENTITEMS数据层接口。
        /// </summary>
        public static ZK.IDAL.IRECENTITEMS CreateRECENTITEMS()
        {

            string ClassNamespace = AssemblyPath + ".RECENTITEMS";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IRECENTITEMS)objType;
        }


        /// <summary>
        /// 创建sysdiagrams数据层接口。1
        /// </summary>
        public static ZK.IDAL.Isysdiagrams Createsysdiagrams()
        {

            string ClassNamespace = AssemblyPath + ".sysdiagrams";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.Isysdiagrams)objType;
        }


        /// <summary>
        /// 创建SYSMSGS数据层接口。1
        /// </summary>
        public static ZK.IDAL.ISYSMSGS CreateSYSMSGS()
        {

            string ClassNamespace = AssemblyPath + ".SYSMSGS";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.ISYSMSGS)objType;
        }


        /// <summary>
        /// 创建USERS数据层接口。1
        /// </summary>
        public static ZK.IDAL.IUSERS CreateUSERS()
        {

            string ClassNamespace = AssemblyPath + ".USERS";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IUSERS)objType;
        }


        /// <summary>
        /// 创建WEBAPPS数据层接口。1
        /// </summary>
        public static ZK.IDAL.IWEBAPPS CreateWEBAPPS()
        {

            string ClassNamespace = AssemblyPath + ".WEBAPPS";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IWEBAPPS)objType;
        }


        /// <summary>
        /// 创建WEBVISITORMESSAGES数据层接口。1
        /// </summary>
        public static ZK.IDAL.IWEBVISITORMESSAGES CreateWEBVISITORMESSAGES()
        {

            string ClassNamespace = AssemblyPath + ".WEBVISITORMESSAGES";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IWEBVISITORMESSAGES)objType;
        }


        /// <summary>
        /// 创建WEBVISITORS数据层接口。1
        /// </summary>
        public static ZK.IDAL.IWEBVISITORS CreateWEBVISITORS()
        {

            string ClassNamespace = AssemblyPath + ".WEBVISITORS";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IWEBVISITORS)objType;
        }


        /// <summary>
        /// 创建WEBVISITORTOTAL数据层接口。1
        /// </summary>
        public static ZK.IDAL.IWEBVISITORTOTAL CreateWEBVISITORTOTAL()
        {

            string ClassNamespace = AssemblyPath + ".WEBVISITORTOTAL";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IWEBVISITORTOTAL)objType;
        }


        /// <summary>
        /// 创建ZK_Channel数据层接口。1
        /// </summary>
        public static ZK.IDAL.IZK_Channel CreateZK_Channel()
        {

            string ClassNamespace = AssemblyPath + ".ZK_Channel";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IZK_Channel)objType;
        }


        /// <summary>
        /// 创建ZK_ChannelGroup数据层接口。1
        /// </summary>
        public static ZK.IDAL.IZK_ChannelGroup CreateZK_ChannelGroup()
        {

            string ClassNamespace = AssemblyPath + ".ZK_ChannelGroup";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IZK_ChannelGroup)objType;
        }


        /// <summary>
        /// 创建ZK_ChannelGroupAndFileList数据层接口。1
        /// </summary>
        public static ZK.IDAL.IZK_ChannelGroupAndFileList CreateZK_ChannelGroupAndFileList()
        {

            string ClassNamespace = AssemblyPath + ".ZK_ChannelGroupAndFileList";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IZK_ChannelGroupAndFileList)objType;
        }


        /// <summary>
        /// 创建ZK_Course数据层接口。1
        /// </summary>
        public static ZK.IDAL.IZK_Course CreateZK_Course()
        {

            string ClassNamespace = AssemblyPath + ".ZK_Course";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IZK_Course)objType;
        }


        /// <summary>
        /// 创建ZK_Edition数据层接口。1
        /// </summary>
        public static ZK.IDAL.IZK_Edition CreateZK_Edition()
        {

            string ClassNamespace = AssemblyPath + ".ZK_Edition";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IZK_Edition)objType;
        }


        /// <summary>
        /// 创建ZK_FileAndTags数据层接口。1
        /// </summary>
        public static ZK.IDAL.IZK_FileAndTags CreateZK_FileAndTags()
        {

            string ClassNamespace = AssemblyPath + ".ZK_FileAndTags";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IZK_FileAndTags)objType;
        }


        /// <summary>
        /// 创建ZK_FileExtend数据层接口。1
        /// </summary>
        public static ZK.IDAL.IZK_FileExtend CreateZK_FileExtend()
        {

            string ClassNamespace = AssemblyPath + ".ZK_FileExtend";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IZK_FileExtend)objType;
        }


        /// <summary>
        /// 创建ZK_FileJP数据层接口。1
        /// </summary>
        public static ZK.IDAL.IZK_FileJP CreateZK_FileJP()
        {

            string ClassNamespace = AssemblyPath + ".ZK_FileJP";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IZK_FileJP)objType;
        }


        /// <summary>
        /// 创建ZK_FileList数据层接口。1
        /// </summary>
        public static ZK.IDAL.IZK_FileList CreateZK_FileList()
        {

            string ClassNamespace = AssemblyPath + ".ZK_FileList";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IZK_FileList)objType;
        }


        /// <summary>
        /// 创建ZK_FileType数据层接口。1
        /// </summary>
        public static ZK.IDAL.IZK_FileType CreateZK_FileType()
        {

            string ClassNamespace = AssemblyPath + ".ZK_FileType";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IZK_FileType)objType;
        }


        /// <summary>
        /// 创建ZK_Grade数据层接口。1
        /// </summary>
        public static ZK.IDAL.IZK_Grade CreateZK_Grade()
        {

            string ClassNamespace = AssemblyPath + ".ZK_Grade";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IZK_Grade)objType;
        }


        /// <summary>
        /// 创建ZK_Lesson数据层接口。1
        /// </summary>
        public static ZK.IDAL.IZK_Lesson CreateZK_Lesson()
        {

            string ClassNamespace = AssemblyPath + ".ZK_Lesson";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IZK_Lesson)objType;
        }


        /// <summary>
        /// 创建ZK_LessonAndFileList数据层接口。1
        /// </summary>
        public static ZK.IDAL.IZK_LessonAndFileList CreateZK_LessonAndFileList()
        {

            string ClassNamespace = AssemblyPath + ".ZK_LessonAndFileList";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IZK_LessonAndFileList)objType;
        }


        /// <summary>
        /// 创建ZK_LessonClass数据层接口。1
        /// </summary>
        public static ZK.IDAL.IZK_LessonClass CreateZK_LessonClass()
        {

            string ClassNamespace = AssemblyPath + ".ZK_LessonClass";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IZK_LessonClass)objType;
        }


        /// <summary>
        /// 创建ZK_Tags数据层接口。1
        /// </summary>
        public static ZK.IDAL.IZK_Tags CreateZK_Tags()
        {

            string ClassNamespace = AssemblyPath + ".ZK_Tags";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IZK_Tags)objType;
        }



        /// <summary>
        /// 创建ZK_FileJPType数据层接口。
        /// </summary>
        public static ZK.IDAL.IZK_FileJPType CreateZK_FileJPType()
        {

            string ClassNamespace = AssemblyPath + ".ZK_FileJPType";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IZK_FileJPType)objType;
        }

        /// <summary>
        /// 创建View_OtherFileList数据层接口。
        /// </summary>
        public static ZK.IDAL.IView_OtherFileList CreateView_OtherFileList()
        {

            string ClassNamespace = AssemblyPath + ".View_OtherFileList";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IView_OtherFileList)objType;
        }


        /// <summary>
        /// 创建View_TeachFileList数据层接口。
        /// </summary>
        public static ZK.IDAL.IView_TeachFileList CreateView_TeachFileList()
        {

            string ClassNamespace = AssemblyPath + ".View_TeachFileList";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IView_TeachFileList)objType;
        }

        /// <summary>
        /// 创建View_AllFileList数据层接口。
        /// </summary>
        public static ZK.IDAL.IView_AllFileList CreateView_AllFileList()
        {

            string ClassNamespace = AssemblyPath + ".View_AllFileList";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IView_AllFileList)objType;
        }

        /// <summary>
        /// 创建ZK_FileVisitors数据层接口。
        /// </summary>
        public static ZK.IDAL.IZK_FileVisitors CreateZK_FileVisitors()
        {

            string ClassNamespace = AssemblyPath + ".ZK_FileVisitors";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IZK_FileVisitors)objType;
        }

        /// <summary>
        /// 创建file_base数据层接口。
        /// </summary>
        public static ZK.IDAL.Ifile_base Createfile_base()
        {

            string ClassNamespace = AssemblyPath + ".file_base";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.Ifile_base)objType;
        }

        /// <summary>
        /// 创建BMS_UD_UserRole数据层接口。制卡单基本信息
        /// </summary>
        public static ZK.IDAL.ICommondBase CreateCommondBase()
        {

            string ClassNamespace = AssemblyPath + ".CommondBase";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.ICommondBase)objType;
        }


        /// <summary>
        /// 创建miniyun_user_metas数据层接口。
        /// </summary>
        public static ZK.IDAL.Iminiyun_user_metas Createminiyun_user_metas()
        {

            string ClassNamespace = AssemblyPath + ".miniyun_user_metas";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.Iminiyun_user_metas)objType;
        }


        /// <summary>
        /// 创建miniyun_users数据层接口。
        /// </summary>
        public static ZK.IDAL.Iminiyun_users Createminiyun_users()
        {

            string ClassNamespace = AssemblyPath + ".miniyun_users";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.Iminiyun_users)objType;
        }


        /// <summary>
        /// 创建miniyun_files数据层接口。
        /// </summary>
        public static ZK.IDAL.Iminiyun_files Createminiyun_files()
        {

            string ClassNamespace = AssemblyPath + ".miniyun_files";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.Iminiyun_files)objType;
        }


        /// <summary>
        /// 创建ZK_FileJPPic数据层接口。
        /// </summary>
        public static ZK.IDAL.IZK_FileJPPic CreateZK_FileJPPic()
        {

            string ClassNamespace = AssemblyPath + ".ZK_FileJPPic";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IZK_FileJPPic)objType;
        }


        /// <summary>
        /// 创建ZK_FileListTra数据层接口。
        /// </summary>
        public static ZK.IDAL.IZK_FileListTra CreateZK_FileListTra()
        {

            string ClassNamespace = AssemblyPath + ".ZK_FileListTra";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IZK_FileListTra)objType;
        }


        /// <summary>
        /// 创建miniyun_events数据层接口。
        /// </summary>
        public static ZK.IDAL.Iminiyun_events Createminiyun_events()
        {

            string ClassNamespace = AssemblyPath + ".miniyun_events";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.Iminiyun_events)objType;
        }


        /// <summary>
        /// 创建miniyun_file_exifs数据层接口。
        /// </summary>
        public static ZK.IDAL.Iminiyun_file_exifs Createminiyun_file_exifs()
        {

            string ClassNamespace = AssemblyPath + ".miniyun_file_exifs";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.Iminiyun_file_exifs)objType;
        }


        /// <summary>
        /// 创建miniyun_file_metas数据层接口。
        /// </summary>
        public static ZK.IDAL.Iminiyun_file_metas Createminiyun_file_metas()
        {

            string ClassNamespace = AssemblyPath + ".miniyun_file_metas";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.Iminiyun_file_metas)objType;
        }


        /// <summary>
        /// 创建miniyun_file_star数据层接口。
        /// </summary>
        public static ZK.IDAL.Iminiyun_file_star Createminiyun_file_star()
        {

            string ClassNamespace = AssemblyPath + ".miniyun_file_star";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.Iminiyun_file_star)objType;
        }


        /// <summary>
        /// 创建miniyun_file_tags数据层接口。
        /// </summary>
        public static ZK.IDAL.Iminiyun_file_tags Createminiyun_file_tags()
        {

            string ClassNamespace = AssemblyPath + ".miniyun_file_tags";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.Iminiyun_file_tags)objType;
        }


        /// <summary>
        /// 创建miniyun_file_version_metas数据层接口。
        /// </summary>
        public static ZK.IDAL.Iminiyun_file_version_metas Createminiyun_file_version_metas()
        {

            string ClassNamespace = AssemblyPath + ".miniyun_file_version_metas";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.Iminiyun_file_version_metas)objType;
        }


        /// <summary>
        /// 创建miniyun_file_versions数据层接口。
        /// </summary>
        public static ZK.IDAL.Iminiyun_file_versions Createminiyun_file_versions()
        {

            string ClassNamespace = AssemblyPath + ".miniyun_file_versions";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.Iminiyun_file_versions)objType;
        }


        /// <summary>
        /// 创建miniyun_share_files数据层接口。
        /// </summary>
        public static ZK.IDAL.Iminiyun_share_files Createminiyun_share_files()
        {

            string ClassNamespace = AssemblyPath + ".miniyun_share_files";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.Iminiyun_share_files)objType;
        }


        /// <summary>
        /// 创建miniyun_tags数据层接口。
        /// </summary>
        public static ZK.IDAL.Iminiyun_tags Createminiyun_tags()
        {

            string ClassNamespace = AssemblyPath + ".miniyun_tags";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.Iminiyun_tags)objType;
        }

        /// <summary>
        /// 创建miniyun_options数据层接口。
        /// </summary>
        public static ZK.IDAL.Iminiyun_options Createminiyun_options()
        {

            string ClassNamespace = AssemblyPath + ".miniyun_options";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.Iminiyun_options)objType;
        }

        /// <summary>
        /// 创建ZK_RoleList数据层接口。
        /// </summary>
        public static ZK.IDAL.IZK_RoleList CreateZK_RoleList()
        {

            string ClassNamespace = AssemblyPath + ".ZK_RoleList";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IZK_RoleList)objType;
        }


        /// <summary>
        /// 创建ZK_RoleToUser数据层接口。
        /// </summary>
        public static ZK.IDAL.IZK_RoleToUser CreateZK_RoleToUser()
        {

            string ClassNamespace = AssemblyPath + ".ZK_RoleToUser";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IZK_RoleToUser)objType;
        }

        /// <summary>
        /// 创建DEPARTUSERS数据层接口。
        /// </summary>
        public static ZK.IDAL.IDEPARTUSERS CreateDEPARTUSERS()
        {

            string ClassNamespace = AssemblyPath + ".DEPARTUSERS";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IDEPARTUSERS)objType;
        }
        /// <summary>
        /// 创建ZK_JXCategory数据层接口。
        /// </summary>
        public static ZK.IDAL.IZK_JXCategory CreateZK_JXCategory()
        {

            string ClassNamespace = AssemblyPath + ".ZK_JXCategory";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IZK_JXCategory)objType;
        }

        /// <summary>
        /// 创建miniyun_user_privilege数据层接口。
        /// </summary>
        public static ZK.IDAL.Iminiyun_user_privilege Createminiyun_user_privilege()
        {

            string ClassNamespace = AssemblyPath + ".miniyun_user_privilege";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.Iminiyun_user_privilege)objType;
        }

        /// <summary>
        /// 创建View_UserMessage数据层接口。
        /// </summary>
        public static ZK.IDAL.IView_UserMessage CreateView_UserMessage()
        {

            string ClassNamespace = AssemblyPath + ".View_UserMessage";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IView_UserMessage)objType;
        }


        /// <summary>
        /// 创建ZK_UserAndSysMsg数据层接口。
        /// </summary>
        public static ZK.IDAL.IZK_UserAndSysMsg CreateZK_UserAndSysMsg()
        {

            string ClassNamespace = AssemblyPath + ".ZK_UserAndSysMsg";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IZK_UserAndSysMsg)objType;
        }

        /// <summary>
        /// 创建ZK_NT_MsgExtent数据层接口。
        /// </summary>
        public static ZK.IDAL.IZK_NT_MsgExtent CreateZK_NT_MsgExtent()
        {

            string ClassNamespace = AssemblyPath + ".ZK_NT_MsgExtent";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.IZK_NT_MsgExtent)objType;
        }
        /// <summary>
        /// 创建file_collection数据层接口。
        /// </summary>
        public static ZK.IDAL.Ifile_collection Createfile_collection()
        {

            string ClassNamespace = AssemblyPath + ".file_collection";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.Ifile_collection)objType;
        }

        /// <summary>
        /// 创建file_collection数据层接口。
        /// </summary>
        public static ZK.IDAL.Ipublic_files Createpublic_files()
        {

            string ClassNamespace = AssemblyPath + ".public_files";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (ZK.IDAL.Ipublic_files)objType;
        }
    }
}