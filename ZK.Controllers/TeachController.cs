using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Text;
using System.Configuration;
using System.Web.UI;
using System.IO;
using ZK.Common;

namespace ZK.Controllers
{
    [ZKAuthAttributeFilter(purl = "")]
    public class TeachController : Controller
    {
        //
        // GET: /Teach/
        BLL.ZK_Course bllcourse = new BLL.ZK_Course();
        BLL.ZK_Grade bllgrade = new BLL.ZK_Grade();
        BLL.ZK_Edition blledition = new BLL.ZK_Edition();
        BLL.ZK_JXCategory blljxcategory = new BLL.ZK_JXCategory();
        BLL.ZK_LessonClass bllclass = new BLL.ZK_LessonClass();
        BLL.ZK_Lesson blllesson = new BLL.ZK_Lesson();
        BLL.ZK_LessonAndFileList blllessonfile = new BLL.ZK_LessonAndFileList();
        BLL.ZK_FileList bllfile = new BLL.ZK_FileList();
        BLL.View_TeachFileList bllv_filelist = new BLL.View_TeachFileList();

        static string XMLFilePath = "SystemSetting.xml";//xml路径E:\智慧教育系统\trunk\ZK.Manage\js\SystemSetting.xml
        int pageSize = 8;
        public static string LessonCourse = "";
        public static string LessonGrade = "";
        public static string LessonEdition = "";
        public static  string strCurrent = "teach"; 
        public ActionResult Index()
        {
            #region 科目

            DataSet courseds = bllcourse.GetAllList();
            if (courseds.Tables.Count > 0)
            {
                List<Model.ZK_Course> CourseList = bllcourse.DataTableToList(courseds.Tables[0]);
                ViewData["Teach_CourseList"] = CourseList;
            }
            else
            {
                ViewData["Teach_CourseList"] = new List<Model.ZK_Course>();
            }

            #endregion

            #region 年级

            DataSet gradds = bllgrade.GetAllList();
            if (gradds.Tables.Count > 0)
            {
                List<Model.ZK_Grade> gradeList = bllgrade.DataTableToList(gradds.Tables[0]);
                ViewData["Teach_GradeList"] = gradeList;
            }
            else
            {
                ViewData["Teach_GradeList"] = new List<Model.ZK_Grade>();
            }
            #endregion

            #region 版本

            DataSet editionds = blledition.GetAllList();
            if (editionds.Tables.Count > 0)
            {
                List<Model.ZK_Edition> editionList = blledition.DataTableToList(editionds.Tables[0]);
                ViewData["Teach_EditionList"] = editionList;
            }
            else
            {
                ViewData["Teach_EditionList"] = new List<Model.ZK_Edition>();
            }
            #endregion
            string LessonData = Request["id"];
            #region 课程单元菜单 
            
            if (LessonData != null)
            {
                string[] StrSp = LessonData.Split('_');
                LessonCourse = StrSp[0].Substring(1);
                LessonGrade = StrSp[1].Substring(1);
                LessonEdition = StrSp[2].Substring(1);//courseid == "undefined"
            }
            if (LessonCourse == "" || LessonCourse == "undefined")
            {
                LessonCourse = bllcourse.GetModelList("")[0].courseID.ToString();
            }
            if (LessonGrade == "" || LessonGrade == "undefined")
            {
                LessonGrade = bllgrade.GetModelList("")[0].gradeID.ToString();
            }
            if (LessonEdition == "" || LessonEdition == "undefined")
            {
                LessonEdition = blledition.GetModelList("")[0].editionID.ToString();
            }

            //先通过教材 年级 版本 查询class
            Model.ZK_LessonClass classmodel = new Model.ZK_LessonClass();
            StringBuilder strWhere_class = new StringBuilder();
            strWhere_class.Append(" 1=1 ");
            strWhere_class.Append(" and CourseID=" + LessonCourse);
            strWhere_class.Append(" and gradeID=" + LessonGrade);
            strWhere_class.Append(" and editionID=" + LessonEdition);

            List<Model.ZK_LessonClass> listclass = bllclass.GetModelList(strWhere_class.ToString());
            if (listclass == null || listclass.Count < 1)
            {
                ViewData["Teach_UnitList"] = new List<Model.ZK_Lesson>();
            }
            else
            {
                string strWhere = " classID=" + listclass[0].classID.ToString();

                List<ZK.Model.ZK_Lesson> lessonList = new List<Model.ZK_Lesson>();
                //年级，科目，版本下的所有资源
                lessonList = blllesson.GetModelList(strWhere);
                ViewData["AllLessonList"] = lessonList;
                //获取总数
                //int totalNumber = blllesson.GetRecordCount(strWhere);

                //排序整合列表
                List<ZK.Model.ZK_Lesson> lessonList2 = new List<Model.ZK_Lesson>();
                lessonList2 = blllesson.GetModelList(" lessonParent=0 order by lessonLevel ");
                ViewData["Teach_UnitList"] = lessonList2;

                //获取相应单元下的课程列表
                List<ZK.Model.ZK_Lesson> lessonList3 = blllesson.GetModelList("1=1 order by lessonlevel");
            }
            #endregion

            #region 教学资源分类
            DataSet teachcategory = blljxcategory.GetAllList();
            if (teachcategory.Tables.Count > 0)
            {
                List<Model.ZK_JXCategory> categroyList = blljxcategory.DataTableToList(teachcategory.Tables[0]);
                ViewData["Teach_CategoryList"] = categroyList;
            }
            else
            {
                ViewData["Teach_CategoryList"] = new List<Model.ZK_JXCategory>();
            }
            #endregion
            ViewData["current"] = strCurrent;
            return View("IndexN");
        }
        /// <summary>
        /// 根据lessonid获取相关信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetInfoByLessonID()
        {
            string lessonID = Request.Form["lessonID"];
            BLL.ZK_Lesson blllesson = new BLL.ZK_Lesson();
            Model.ZK_Lesson lessonmodel = blllesson.GetModel(Convert.ToInt32(lessonID));
            return Json(lessonmodel);
        }
        /// <summary>
        /// 更新课程相关信息
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult UpdateLessonInfo()
        {
            string Flag = Request.Form["Flag"];
            string content = Request.Form["Content"];
            string LessonID = Request.Form["LessonID"];
            bool b = false;
            ZK.Model.ZK_Lesson lessonmodel = blllesson.GetModel(Convert.ToInt32(LessonID));
            if (lessonmodel != null)
            {
                switch (Flag)
                {
                    case "TeachMB":
                        lessonmodel.teachMB = content;
                        break;
                    case "TeachND":
                        lessonmodel.teachND = content;
                        break;
                    case "TeachZD":
                        lessonmodel.teachZD = content;
                        break;
                    default:
                        break;
                }
                b = blllesson.Update(lessonmodel);
            }
            return Content(b.ToString(), "text");
        }
        /// <summary>
        /// 文件列表页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Teach_FileList(int? page)
        {
            string LessonID = Request["lesid"];
            string filetype = Request["type"];
            string orderby = Request["orderType"];

            string courseid = Request["courseid"].Trim();
            string gradeid = Request["gradeid"].Trim();
            string editionid = Request["editionid"].Trim();

            if (courseid == "" || courseid == "undefined")
            {
                LessonCourse = bllcourse.GetModelList("")[0].courseID.ToString();
            }
            else
            {
                LessonCourse = courseid;
            }
            if (gradeid == "" || gradeid == "undefined")
            {
                LessonGrade = bllgrade.GetModelList("")[0].gradeID.ToString();
            }
            else
            {
                LessonGrade = gradeid;
            }
            if (editionid == "" || editionid == "undefined")
            {
                LessonEdition = blledition.GetModelList("")[0].editionID.ToString();
            }
            else
            {
                LessonEdition = editionid;
            }

            StringBuilder builder = new StringBuilder();
            builder.Append("1=1 ");
            if (!String.IsNullOrEmpty(filetype) && filetype != "0")
            {
                builder.Append(" and CategoryId=" + filetype);
            }
            if (LessonID == "0"||LessonID=="")//没有点击单元下课程
            {
                string strWhere_class = " 1=1 and courseid= " + LessonCourse + " and gradeid= " + LessonGrade + " and editionid= " + LessonEdition;
                List<Model.ZK_LessonClass> listclass = bllclass.GetModelList(strWhere_class.ToString());
                string classID = listclass[0].classID.ToString();
                DataSet dsFileList = new ZK.BLL.ZK_Lesson().GetList(" classID=" + classID);
                List<int> lessonids = new List<int>();
                for (int i = 0; i < dsFileList.Tables[0].Rows.Count; i++)
                {
                    int sid = Convert.ToInt32(dsFileList.Tables[0].Rows[i]["lessonID"].ToString());
                    lessonids.Add(sid);
                }
                string strids = String.Join(",", lessonids);
                if (lessonids.Count > 0)
                {
                    builder.Append(" and cateID in (" + strids + ")");
                }
            }
            else
            {
                builder.Append(" and cateID=" + LessonID);
                builder.Append(" and isDir=0 ");
            }
            string XMLFilePath = Server.MapPath(ZK.Common.ModelSettings.Pre_SysSettingXMLPath);
            pageSize = Convert.ToInt32(XMLHelper.GetXmlAttribute(XMLFilePath, "Settings/PageCount/Teach", "value").Value.ToString());
            int PageIndex = page.HasValue ? page.Value : 1;
            int startindex = (Convert.ToInt32(PageIndex) - 1) * (Convert.ToInt32(pageSize)) + 1;
            int endindex = Convert.ToInt32(pageSize) * PageIndex;
            DataSet FileList = new DataSet();
            if (orderby == "1")//orderby == "1"
            {
                    //string strSQL = "1=1  and cateID=" + LessonID + " and CategoryId=" + filetype;
                    //string strSQL = "1=1  and cateID=" + LessonID + " and CategoryId=" + filetype;
                    //int startindex = (Convert.ToInt32(PageIndex) - 1) * (Convert.ToInt32(pageSize)) + 1;
                    //dsFileList = bllv_filelist.GetList(strSQL);
                    //int endindex = dsFileList.Tables[0].Rows.Count / pageSize + 1;
                    //int endindex = Convert.ToInt32(pageSize) * PageIndex;

                   // FileList = bllv_filelist.GetListByPage(strSQL, "createTime", startindex, endindex);
                 FileList = bllv_filelist.GetListByPage(builder.ToString(), "createTime desc", startindex, endindex);
             }
             else
             {
                // FileList = bllv_filelist.GetListpageSize, page.HasValue ? page.Value : 1, builder.ToString());
                 FileList = bllv_filelist.GetListByPage(builder.ToString(), "clickNum desc", startindex, endindex);
             }

              if (FileList.Tables.Count > 0)
                {
                    DataTable v_teacherfilelisttable = FileList.Tables[0];
                    List<Model.View_TeachFileList> v_teacherfilelist = bllv_filelist.DataTableToList(v_teacherfilelisttable);
                    ViewData["TeachFileList"] = v_teacherfilelist;

                    ViewData["totlecount"] = bllv_filelist.GetRecordCount(builder.ToString());
                }
            ViewData["page"] = "page";
            ViewData["pagesize"] = pageSize;
            return View();
        }
        /// <summary>
        /// 通过单元名获取课程列表
        /// </summary>
        /// <param name="itemstr"></param>
        /// <returns></returns>
        public List<ZK.Model.ZK_Lesson> GetLessonByUnit(string itemstr)
        {
            List<ZK.Model.ZK_Lesson> lessonList3 = blllesson.GetModelList("lessonParent=" + itemstr + "order by lessonlevel ");
            return lessonList3;
        }
        public ActionResult AllLessonList(int? page) 
        {
            string courseid = Request["courseid"].Trim();
            string gradeid = Request["gradeid"].Trim();
            string editionid = Request["editionid"].Trim();

            if (courseid == "" || courseid == "undefined")
            {
                courseid = bllcourse.GetModelList("")[0].courseID.ToString();
            }
            if (gradeid == "" || gradeid == "undefined")
            {
                gradeid = bllgrade.GetModelList("")[0].gradeID.ToString();
            }
            if (editionid == "" || editionid == "undefined")
            {
                editionid = blledition.GetModelList("")[0].editionID.ToString();
            }

            string XMLFilePath = Server.MapPath(ZK.Common.ModelSettings.Pre_SysSettingXMLPath);
            pageSize = Convert.ToInt32(XMLHelper.GetXmlAttribute(XMLFilePath, "Settings/PageCount/Teach", "value").Value.ToString());
            int PageIndex = page.HasValue ? page.Value : 1;

            DataSet dsFileList = new DataSet();
           //得到classID
            string strWhere_class=" 1=1 and courseid= "+courseid+" and gradeid= "+gradeid+" and editionid= "+editionid;
            List<Model.ZK_LessonClass> listclass = bllclass.GetModelList(strWhere_class.ToString());
            int startindex = (Convert.ToInt32(PageIndex) - 1) * (Convert.ToInt32(pageSize)) + 1;
            int endindex = Convert.ToInt32(pageSize) * PageIndex;
            string classID=listclass[0].classID.ToString();
 
            dsFileList = new ZK.BLL.ZK_Lesson().GetList(" classID="+classID);
            List<int> lessonids = new List<int>();
            for (int i = 0; i < dsFileList.Tables[0].Rows.Count; i++)
            {
                int sid =Convert.ToInt32(dsFileList.Tables[0].Rows[i]["lessonID"].ToString());
                lessonids.Add(sid);
            }
            string strids = String.Join(",", lessonids);
            if (dsFileList.Tables.Count > 0)
            {
                string strWhere = " cateID in (" + strids + ")";
                dsFileList = bllv_filelist.GetListByPage(strWhere, "createTime", startindex, endindex);
                DataTable v_teacherfilelisttable = dsFileList.Tables[0];
                List<Model.View_TeachFileList> v_teacherfilelist = bllv_filelist.DataTableToList(v_teacherfilelisttable);
                ViewData["TeachFileList"] = v_teacherfilelist;

                ViewData["totlecount"] = bllv_filelist.GetRecordCount(strWhere);
            }
            ViewData["page"] = "page";
            ViewData["pagesize"] = pageSize;
            return View();

        }
        

    }
}
