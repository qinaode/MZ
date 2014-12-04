using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Script.Serialization;
using System.Data;
using System.Text;

namespace ZK.Manage.ashx
{
    /// <summary>
    /// TeachChannelManage 的摘要说明
    /// </summary>
    public class TeachChannelManage : IHttpHandler
    {
        private ZK.BLL.ZK_Grade bllgrade = new BLL.ZK_Grade();
        private ZK.BLL.ZK_Course bllcourse = new BLL.ZK_Course();
        private ZK.BLL.ZK_Edition blledition = new BLL.ZK_Edition();
        private ZK.BLL.ZK_Lesson blllesson = new BLL.ZK_Lesson();
        private ZK.BLL.ZK_LessonClass bllclass = new BLL.ZK_LessonClass();
        private ZK.BLL.ZK_FileList bllfile = new BLL.ZK_FileList();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string Flag = context.Request.Form["Flag"];
            string Result = string.Empty;
            //            this.GetType().GetMethod(Result).Invoke(this, new HttpContext[] { context });
            switch (Flag)
            {
                case "GetGradeListPaging":
                    Result = GetGradeListPaging(context);
                    break;
                case "GetGradeList":
                    Result = GetGradeList(context);
                    break;
                case "GetCourseListPaging":
                    Result = GetCourseListPaging(context);
                    break;
                case "GetCourseList":
                    Result = GetCourseList(context);
                    break;
                case "GetEditionListPaging":
                    Result = GetEditionListPaging(context);
                    break;
                case "GetEditionList":
                    Result = GetEditionList(context);
                    break;
                case "GetLessonListPaging":
                    Result = GetLessonListPaging(context);
                    break;
                case "GetLessonList":
                    Result = GetLessonList(context);
                    break;
                case "GetUnitList":
                    Result = GetUnitList(context);
                    break;
                case "GetTeachResourceListPaging":
                    Result = GetTeachResourceListPaging(context);
                    break;
                case "GetLessonModel":
                    Result = GetLessonModel(context);
                    break;
                case "DeleteCheckedCourse":
                    Result = DeleteCheckedCourse(context);
                    break;
                case "DeleteCheckedGrades":
                    Result = DeleteCheckedGrades(context);
                    break;
                case "DeleteCheckedEditions":
                    Result = DeleteCheckedEditions(context);
                    break;
                case "DeleteCheckedLesson":
                    Result = DeleteCheckedLesson(context);
                    break;
                case "UpCheckedLesson":
                    Result = MoveCheckedLesson(context);
                    break;
                case "DownCheckedLesson":
                    Result = MoveCheckedLesson(context);
                    break;
                case "DeleteCheckedTeachResource":
                    Result = DeleteCheckedTeachResource(context);
                    break;
                case "AddNewCourse":
                    Result = AddNewCourse(context);
                    break;
                case "UpdateCourse":
                    Result = UpdateCourse(context);
                    break;
                case "AddNewGrade":
                    Result = AddNewGrade(context);
                    break;
                case "UpdateGrade":
                    Result = UpdateGrade(context);
                    break;
                case "AddNewEdition":
                    Result = AddNewEdition(context);
                    break;
                case "UpdateEdition":
                    Result = UpdateEdition(context);
                    break;
                case "AddNewLesson":
                    Result = AddNewLesson(context);
                    break;
                case "UpdateLesson":
                    Result = UpdateLesson(context);
                    break;
                case "DeleteCourse":
                    Result = DeleteCourse(context);
                    break;
                case "DeleteGrade":
                    Result = DeleteGrade(context);
                    break;
                case "DeleteEdition":
                    Result = DeleteEdition(context);
                    break;
                case "Convert_TeachResource":
                    Result = Convert_TeachResource(context);
                    break;
                default:
                    Result = "";
                    break;
            }
            context.Response.Write(Result);
        }

        /// <summary>
        ///        //获取年级分页的列表
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string GetGradeListPaging(HttpContext context)
        {
            string PageIndex = context.Request.Form["PageIndex"] == "" ? "1" : context.Request.Form["PageIndex"];
            string PageSize = context.Request.Form["PageSize"] == "" ? "10" : context.Request.Form["PageSize"];
            string strWhere = context.Request.Form["strWhere"];
            if (strWhere != "")
            {
                strWhere = "gradeName like '%" + strWhere + "%'";
            }
            //int startindex = (Convert.ToInt32(PageIndex) - 1) * (Convert.ToInt32(PageSize)) + 1;
            //int endindex = startindex + Convert.ToInt32(PageSize);

            int startindex = (Convert.ToInt32(PageIndex) - 1) * (Convert.ToInt32(PageSize)) + 1;
            int endindex = Convert.ToInt32(PageSize) * Convert.ToInt32(PageIndex);

            List<ZK.Model.ZK_Grade> gradeList = new List<Model.ZK_Grade>();

            DataSet ds = bllgrade.GetListByPage(strWhere, "", startindex, endindex);
            gradeList = bllgrade.DataTableToList(ds.Tables[0]);
            //获取总数
            int totalNumber = bllgrade.GetRecordCount(strWhere);

            JavaScriptSerializer jss = new JavaScriptSerializer();
            return SerializeJsonString(jss.Serialize(gradeList), totalNumber);
        }
        /// <summary>
        /// 获取年级列表
        /// </summary>
        /// <param name="context"></param>
        /// <returns>年级列表</returns>
        private string GetGradeList(HttpContext context)
        {
            List<ZK.Model.ZK_Grade> List = new List<Model.ZK_Grade>();
            List = bllgrade.GetModelList("");
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return SerializeJsonString(jss.Serialize(List), 0);
        }
        /// <summary>
        /// 获取教材分页列表
        /// </summary>
        /// <param name="context"></param>
        /// <returns>教材分页数据 list</returns>
        private string GetCourseListPaging(HttpContext context)
        {
            string PageIndex = context.Request.Form["PageIndex"] == "" ? "1" : context.Request.Form["PageIndex"];
            string PageSize = context.Request.Form["PageSize"] == "" ? "10" : context.Request.Form["PageSize"];
            string strWhere = context.Request.Form["strWhere"];
            if (strWhere != "")
            {
                strWhere = "courseName like '%" + strWhere + "%'";
            }
            //int startindex = (Convert.ToInt32(PageIndex) - 1) * (Convert.ToInt32(PageSize)) + 1;
            //int endindex = startindex + Convert.ToInt32(PageSize);

            int startindex = (Convert.ToInt32(PageIndex) - 1) * (Convert.ToInt32(PageSize)) + 1;
            int endindex = Convert.ToInt32(PageSize) * Convert.ToInt32(PageIndex);

            List<ZK.Model.ZK_Course> courseList = new List<Model.ZK_Course>();

            //gradeList = bllgrade.GetModelList("1=1");
            DataSet ds = bllcourse.GetListByPage(strWhere, "", startindex, endindex);
            courseList = bllcourse.DataTableToList(ds.Tables[0]);
            //获取总数
            int totalNumber = bllcourse.GetRecordCount(strWhere);

            JavaScriptSerializer jss = new JavaScriptSerializer();
            return SerializeJsonString(jss.Serialize(courseList), totalNumber);
        }
        /// <summary>
        /// 获取教材列表
        /// </summary>
        /// <param name="context"></param>
        /// <returns>教材列表</returns>
        private string GetCourseList(HttpContext context)
        {
            List<ZK.Model.ZK_Course> courseList = new List<Model.ZK_Course>();
            courseList = bllcourse.GetModelList("");
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return SerializeJsonString(jss.Serialize(courseList), 0);
        }
        /// <summary>
        /// 获取版本分页列表
        /// </summary>
        /// <param name="context"></param>
        /// <returns>版本分页列表</returns>
        private string GetEditionListPaging(HttpContext context)
        {
            string PageIndex = context.Request.Form["PageIndex"] == "" ? "1" : context.Request.Form["PageIndex"];
            string PageSize = context.Request.Form["PageSize"] == "" ? "10" : context.Request.Form["PageSize"];
            string strWhere = context.Request.Form["strWhere"];
            if (strWhere != "")
            {
                strWhere = " editionName like '%" + strWhere + "%'";
            }
            //int startindex = (Convert.ToInt32(PageIndex) - 1) * (Convert.ToInt32(PageSize)) + 1;
            //int endindex = startindex + Convert.ToInt32(PageSize);

            int startindex = (Convert.ToInt32(PageIndex) - 1) * (Convert.ToInt32(PageSize)) + 1;
            int endindex = Convert.ToInt32(PageSize) * Convert.ToInt32(PageIndex);

            List<ZK.Model.ZK_Edition> editionList = new List<Model.ZK_Edition>();

            //gradeList = bllgrade.GetModelList("1=1");
            DataSet ds = blledition.GetListByPage(strWhere, "", startindex, endindex);
            editionList = blledition.DataTableToList(ds.Tables[0]);
            //获取总数
            int totalNumber = blledition.GetRecordCount(strWhere);

            JavaScriptSerializer jss = new JavaScriptSerializer();
            return SerializeJsonString(jss.Serialize(editionList), totalNumber);
        }
        /// <summary>
        /// 获取版本列表
        /// </summary>
        /// <param name="context"></param>
        /// <returns>版本列表</returns>
        private string GetEditionList(HttpContext context)
        {
            List<ZK.Model.ZK_Edition> List = new List<Model.ZK_Edition>();
            List = blledition.GetModelList("");
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return SerializeJsonString(jss.Serialize(List), 0);
        }
        /// <summary>
        /// 获取课程的分页列表
        /// </summary>
        /// <param name="context"></param>
        /// <returns>课程的分页列表</returns>
        private string GetLessonListPaging(HttpContext context)
        {
            string PageIndex = context.Request.Form["PageIndex"] == "" ? "1" : context.Request.Form["PageIndex"];
            string PageSize = context.Request.Form["PageSize"] == "" ? "10" : context.Request.Form["PageSize"];
            string LessonCourse = context.Request.Form["LessonCourse"];
            string LessonGrade = context.Request.Form["LessonGrade"];
            string LessonEdition = context.Request.Form["LessonEdition"];
            //先通过教材 年级 版本 查询class
            Model.ZK_LessonClass classmodel = new Model.ZK_LessonClass();
            StringBuilder strWhere_class = new StringBuilder();
            strWhere_class.Append(" 1=1 ");
            if (LessonCourse != "")
            {
                strWhere_class.Append(" and CourseID=" + LessonCourse);
            }
            if (LessonGrade != "")
            {
                strWhere_class.Append(" and gradeID=" + LessonGrade);
            }
            if (LessonEdition != "")
            {
                strWhere_class.Append(" and editionID=" + LessonEdition);
            }
            List<Model.ZK_LessonClass> listclass = bllclass.GetModelList(strWhere_class.ToString());
            if (listclass == null || listclass.Count < 1)
            {
                return "班级不存在";
            }
            string strWhere = " classID=" + listclass[0].classID.ToString();

            //int startindex = (Convert.ToInt32(PageIndex) - 1) * (Convert.ToInt32(PageSize)) + 1;
            //int endindex = startindex + Convert.ToInt32(PageSize);

            int startindex = (Convert.ToInt32(PageIndex) - 1) * (Convert.ToInt32(PageSize)) + 1;
            int endindex = Convert.ToInt32(PageSize) * Convert.ToInt32(PageIndex);

            List<ZK.Model.ZK_Lesson> lessonList = new List<Model.ZK_Lesson>();

            //gradeList = bllgrade.GetModelList("1=1");
            DataSet ds = blllesson.GetListByPage(strWhere, "", startindex, endindex);
            lessonList = blllesson.DataTableToList(ds.Tables[0]);
            //获取总数
            int totalNumber = blllesson.GetRecordCount(strWhere);

            //排序整合列表
            List<ZK.Model.ZK_Lesson> lessonList2 = new List<Model.ZK_Lesson>();
            List<ZK.Model.ZK_Lesson> lessonList3 = new List<Model.ZK_Lesson>();
            lessonList2 = blllesson.GetModelList(" lessonParent=-1 ");
            foreach (var item in lessonList2)
            {
                lessonList3.Add(item);
                lessonList3.AddRange(blllesson.GetModelList(" lessonParent=" + item.lessonID.ToString()));
            }
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return SerializeJsonString(jss.Serialize(lessonList3), totalNumber);
        }
        private string GetLessonList(HttpContext context)
        {
            string LessonCourse = context.Request.Form["LessonCourse"];
            string LessonGrade = context.Request.Form["LessonGrade"];
            string LessonEdition = context.Request.Form["LessonEdition"];
            //先通过教材 年级 版本 查询class
            Model.ZK_LessonClass classmodel = new Model.ZK_LessonClass();
            StringBuilder strWhere_class = new StringBuilder();
            strWhere_class.Append(" 1=1 ");
            if (LessonCourse != "")
            {
                strWhere_class.Append(" and CourseID=" + LessonCourse);
            }
            if (LessonGrade != "")
            {
                strWhere_class.Append(" and gradeID=" + LessonGrade);
            }
            if (LessonEdition != "")
            {
                strWhere_class.Append(" and editionID=" + LessonEdition);
            }
            List<Model.ZK_LessonClass> listclass = bllclass.GetModelList(strWhere_class.ToString());
            if (listclass == null || listclass.Count < 1)
            {
                return "班级不存在";
            }
            string strWhere = " classID=" + listclass[0].classID.ToString();

            List<ZK.Model.ZK_Lesson> lessonList = new List<Model.ZK_Lesson>();

            lessonList = blllesson.GetModelList(strWhere);
            //获取总数
            //int totalNumber = blllesson.GetRecordCount(strWhere);

            //排序整合列表
            List<ZK.Model.ZK_Lesson> lessonList2 = new List<Model.ZK_Lesson>();
            List<ZK.Model.ZK_Lesson> lessonList3 = new List<Model.ZK_Lesson>();
            lessonList2 = blllesson.GetModelList(" lessonParent=0 ");
            foreach (var item in lessonList2)
            {
                lessonList3.Add(item);
                lessonList3.AddRange(blllesson.GetModelList(" lessonParent=" + item.lessonID.ToString() + " order by lessonlevel"));
            }
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return SerializeJsonString(jss.Serialize(lessonList3), 0);
        }
        /// <summary>
        /// 获得单元列表
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string GetUnitList(HttpContext context)
        {
            string LessonCourse = context.Request.Form["LessonCourse"];
            string LessonGrade = context.Request.Form["LessonGrade"];
            string LessonEdition = context.Request.Form["LessonEdition"];
            string LessonParent = context.Request.Form["LessonParent"];
            //先通过教材 年级 版本 查询class
            Model.ZK_LessonClass classmodel = new Model.ZK_LessonClass();
            StringBuilder strWhere_class = new StringBuilder();
            strWhere_class.Append(" 1=1 ");
            if (LessonCourse != "")
            {
                strWhere_class.Append(" and CourseID=" + LessonCourse);
            }
            if (LessonGrade != "")
            {
                strWhere_class.Append(" and gradeID=" + LessonGrade);
            }
            if (LessonEdition != "")
            {
                strWhere_class.Append(" and editionID=" + LessonEdition);
            }
            List<Model.ZK_LessonClass> listclass = bllclass.GetModelList(strWhere_class.ToString());
            if (listclass == null || listclass.Count < 1)
            {
                return "班级不存在";
            }
            string strWhere = " classID=" + listclass[0].classID.ToString() + " and lessonParent=" + LessonParent;
            List<ZK.Model.ZK_Lesson> lessonList = new List<Model.ZK_Lesson>();
            lessonList = blllesson.GetModelList(strWhere);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return SerializeJsonString(jss.Serialize(lessonList), 0);
        }
        /// <summary>
        /// 获取教学资源列表 分页
        /// </summary>
        /// <param name="context"></param>
        /// <returns>教学资源列表分页数据</returns>
        private string GetTeachResourceListPaging(HttpContext context)
        {
            string PageIndex = context.Request.Form["PageIndex"] == "" ? "1" : context.Request.Form["PageIndex"];
            string PageSize = context.Request.Form["PageSize"] == "" ? "10" : context.Request.Form["PageSize"];
            string LessonCourse = context.Request.Form["LessonCourse"];
            string LessonGrade = context.Request.Form["LessonGrade"];
            string LessonEdition = context.Request.Form["LessonEdition"];
            string lessonids = GetLessonIDsByCondition(LessonCourse, LessonGrade, LessonEdition);
            string sourceids = GetSourceIDs(lessonids);
            string strWhere = context.Request.Form["strWhere"];
            List<ZK.Model.ZK_FileList> List = new List<Model.ZK_FileList>();
            StringBuilder builder = new StringBuilder();
            builder.Append("1=1 and fileid in (" + sourceids + ") ");
            if (strWhere != "")
            {
                builder.Append(" and fileName like '%" + strWhere + "%'");
            }

            int startindex = (Convert.ToInt32(PageIndex) - 1) * (Convert.ToInt32(PageSize)) + 1;
            int endindex = Convert.ToInt32(PageSize) * Convert.ToInt32(PageIndex);
            DataSet ds = bllfile.GetListByPage(builder.ToString(), "", startindex, endindex);
            //DataSet ds = bllfile.GetListByPage(builder.ToString(), "", int.Parse(PageIndex), int.Parse(PageSize));

            List = bllfile.DataTableToList(ds.Tables[0]);
            foreach (var item in List)
            {
                if (!item.parentID.HasValue)
                {
                    item.parentID = 0;
                }

            }
            JavaScriptSerializer jss = new JavaScriptSerializer();
            int total = bllfile.GetRecordCount(builder.ToString());
            return SerializeJsonString(jss.Serialize(List), total);
        }

        /// <summary>
        /// 获取一个lesson对象
        /// </summary>
        /// <param name="context"></param>
        /// <returns>lesson对象</returns>
        private string GetLessonModel(HttpContext context)
        {
            string LessonID = context.Request.Form["LessonID"];
            Model.ZK_Lesson model = blllesson.GetModel(Convert.ToInt32(LessonID));
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(model);
        }

        /// <summary>
        /// 删除选中的教材对象
        /// </summary>
        /// <param name="context"></param>
        /// <returns>删除结果</returns>
        private string DeleteCheckedCourse(HttpContext context)
        {
            string CourseIDs = context.Request.Form["IDS"];
            bool res = bllcourse.DeleteList(CourseIDs);
            return res.ToString();
        }
        /// <summary>
        /// 删除选中的年级对象
        /// </summary>
        /// <param name="context"></param>
        /// <returns>删除结果</returns>
        private string DeleteCheckedGrades(HttpContext context)
        {
            string IDS = context.Request.Form["IDS"];
            bool res = bllgrade.DeleteList(IDS);
            return res.ToString();
        }
        /// <summary>
        /// 删除选中的版本对象
        /// </summary>
        /// <param name="context"></param>
        /// <returns>删除结果</returns>
        private string DeleteCheckedEditions(HttpContext context)
        {
            string IDS = context.Request.Form["IDS"];
            bool res = blledition.DeleteList(IDS);
            return res.ToString();
        }
        /// <summary>
        /// 删除选择的课程
        /// </summary>
        /// <param name="context"></param>
        /// <returns>返回删除结果 true false</returns>
        private string DeleteCheckedLesson(HttpContext context)
        {
            string IDS = context.Request.Form["IDS"];
            bool res = blllesson.DeleteList(IDS);
            return res.ToString();
        }
        /// <summary>
        ///下移选择的课程
        /// </summary>
        /// <param name="context"></param>
        /// <returns>返回下移结果 true false</returns>
        private string MoveCheckedLesson(HttpContext context)
        {
            ZK.BLL.ZK_Lesson chanelGroupbll = new BLL.ZK_Lesson();
            ZK.Model.ZK_Lesson chanelGroupmdl = new ZK.Model.ZK_Lesson();
            bool res = false;
            string restr = "NoMove";

            string IDS = context.Request.Form["IDS"];
            string dir = context.Request.Form["Dir"];

            int id = Convert.ToInt32(IDS);
            chanelGroupmdl = chanelGroupbll.GetModel(id);

            int depOrder = Convert.ToInt32(chanelGroupmdl.lessonLevel);
            int depParentid = Convert.ToInt32(chanelGroupmdl.lessonParent);

            string strSQL = "";
            if (dir == "Up")
            {
                strSQL = "lessonParent=" + depParentid + " And " + "lessonLevel<" + depOrder + " Order by lessonLevel";
            }

            if (dir == "Down")
            {
                strSQL = "lessonParent=" + depParentid + " And " + "lessonLevel>" + depOrder + " Order by lessonLevel desc";
            }
            System.Data.DataSet ds = chanelGroupbll.GetList(strSQL);

            List<ZK.Model.ZK_Lesson> depList = new List<Model.ZK_Lesson>();
            depList = chanelGroupbll.DataTableToList(ds.Tables[0]);

            if (depList.Count > 0)
            {
                int upid = Convert.ToInt32(depList[depList.Count - 1].lessonLevel);

                int upOrgid = depList[depList.Count - 1].lessonID;
                ZK.Model.ZK_Lesson depmdlB = new Model.ZK_Lesson();
                depmdlB = chanelGroupbll.GetModel(upOrgid);

                ZK.Model.ZK_Lesson depmdl1 = new Model.ZK_Lesson();
                ZK.Model.ZK_Lesson depmdl2 = new Model.ZK_Lesson();

                depmdl1.lessonID = chanelGroupmdl.lessonID;
                depmdl1.lessonDesc = chanelGroupmdl.lessonDesc;
                depmdl1.lessonLevel = upid;
                depmdl1.lessonName = chanelGroupmdl.lessonName;
                depmdl1.lessonParent = chanelGroupmdl.lessonParent;
                depmdl1.classID = chanelGroupmdl.classID;
                depmdl1.teachMB = chanelGroupmdl.teachMB;
                depmdl1.teachND = chanelGroupmdl.teachND;
                depmdl1.teachZD = chanelGroupmdl.teachZD;

                depmdl2.lessonID = depmdlB.lessonID;
                depmdl2.lessonDesc = depmdlB.lessonDesc;
                depmdl2.lessonLevel = depOrder;
                depmdl2.lessonName = depmdlB.lessonName;
                depmdl2.lessonParent = depmdlB.lessonParent;
                depmdl2.classID = depmdlB.classID;
                depmdl2.teachMB = depmdlB.teachMB;
                depmdl2.teachND = depmdlB.teachND;
                depmdl2.teachZD = depmdlB.teachZD;

                chanelGroupbll.Update(depmdl1);
                res = chanelGroupbll.Update(depmdl2);

                return res.ToString();
            }
            else
                return restr;

        }
        /// <summary>
        /// 删除选中的教学资源
        /// </summary>
        /// <param name="context"></param>
        /// <returns>返回删除结果 true false</returns>
        private string DeleteCheckedTeachResource(HttpContext context)
        {
            string ID = context.Request.Form["ID"];
            int rint = 0;
            if (int.TryParse(ID, out rint))
            {

                //List<Model.ZK_FileList> lists = bllfile.GetModelList(" parentID=" + rint.ToString());
                //if (lists != null && lists.Count > 0)
                //{
                //    return "文件夹不为空";
                //}
                BLL.ZK_LessonAndFileList bllzklf = new BLL.ZK_LessonAndFileList();
                List<Model.ZK_LessonAndFileList> modellist = bllzklf.GetModelList("fileid=" + rint.ToString());
                if (modellist != null && modellist.Count > 0)
                {
                    bool res = bllzklf.Delete(modellist[0].ID);
                    return res.ToString();
                }
                else
                {
                    return "false";
                }
            }
            else
            {
                return "false";
            }
        }
        /// <summary>
        /// 添加新的教材
        /// </summary>
        /// <param name="context">请求上下文</param>
        /// <returns>返回处理结果 影响行数</returns>
        private string AddNewCourse(HttpContext context)
        {
            string CourseName = context.Request.Form["CourseName"];
            string CourseDesc = context.Request.Form["CourseDesc"];
            string where = " courseName= '" + CourseName + "'";
            DataSet ds = new ZK.BLL.ZK_Course().GetList(where);
            int i = ds.Tables[0].Rows.Count;
            if (i == 0)
            {
                Model.ZK_Course model = new Model.ZK_Course();
                model.courseName = CourseName;
                model.courseDesc = CourseDesc;
                return bllcourse.Add(model).ToString();
            }
            else
            {

                return "-1";
            }
        }


        #region 更新修改的教材
        /// <summary>
        /// 更新修改的教材
        /// </summary>
        /// <param name="context">请求上下文</param>
        /// <returns>返回处理结果</returns>
        private string UpdateCourse(HttpContext context)
        {
            string CourseID = context.Request.Form["CourseID"];
            if (CourseID == "")
            {
                return "false";
            }
            string CourseName = context.Request.Form["CourseName"];
            string CourseDesc = context.Request.Form["CourseDesc"];
            string where = " courseID='" + CourseID + "'";
            int i = new ZK.BLL.ZK_Course().GetList(where).Tables[0].Rows.Count;
            ZK.BLL.ZK_Course bll_zkcourse = new ZK.BLL.ZK_Course();
            Model.ZK_Course course = bll_zkcourse.GetModel(Convert.ToInt32(CourseID));
            string whereName = " courseName='" + CourseName + "'";
            int j = new ZK.BLL.ZK_Course().GetList(whereName).Tables[0].Rows.Count;
            if (i > 0 && CourseName.Equals(course.courseName))
            {
                Model.ZK_Course model = new Model.ZK_Course();
                model.courseID = Convert.ToInt32(CourseID);
                model.courseName = CourseName;
                model.courseDesc = CourseDesc;
                return bllcourse.Update(model).ToString();
            }
            if (i > 0 && CourseName != course.courseName)
            {
                if (j == 0)
                {
                    Model.ZK_Course model = new Model.ZK_Course();
                    model.courseID = Convert.ToInt32(CourseID);
                    model.courseName = CourseName;
                    model.courseDesc = CourseDesc;

                    return bllcourse.Update(model).ToString();
                }
                else
                {
                    return "-1";
                }
            }
            else
            { return "-1"; }

        }
        #endregion

        #region 添加新的年级
        /// <summary>
        /// 添加新的年级
        /// </summary>
        /// <param name="context">请求上下文</param>
        /// <returns>添加结果  影响行数</returns>
        private string AddNewGrade(HttpContext context)
        {
            string Name = context.Request.Form["GradeName"];
            string Desc = context.Request.Form["GradeDesc"];
            string where = " gradeName='" + Name + "'";
            int i = new ZK.BLL.ZK_Grade().GetList(where).Tables[0].Rows.Count;
            if (i == 0)
            {
                Model.ZK_Grade model = new Model.ZK_Grade();
                model.gradeName = Name;
                model.gradeDesc = Desc;
                return bllgrade.Add(model).ToString();
            }
            else
            {
                return "-1";
            }
        }

        #endregion

        #region 更新年级
        /// <summary>
        /// 更新年级
        /// </summary>
        /// <param name="context">请求上下文</param>
        /// <returns>更新结果 True False</returns>
        private string UpdateGrade(HttpContext context)
        {
            string ID = context.Request.Form["GradeID"];
            if (ID == "")
            {
                return "False";
            }
            string GradeName = context.Request.Form["GradeName"];

            string CourseDesc = context.Request.Form["GradeDesc"];
            string where = " gradeID= '" + ID + "'";
            int i = new ZK.BLL.ZK_Grade().GetList(where).Tables[0].Rows.Count;
            string whereName = " gradeName= '" + GradeName + "'";
            int j = new ZK.BLL.ZK_Grade().GetList(where).Tables[0].Rows.Count;
            Model.ZK_Grade grade = new ZK.BLL.ZK_Grade().GetModel(Convert.ToInt32(ID));
            if (i > 0 && grade.gradeName.Equals(GradeName))
            {
                Model.ZK_Grade model = new Model.ZK_Grade();
                model.gradeID = Convert.ToInt32(ID);
                model.gradeName = GradeName;
                model.gradeDesc = CourseDesc;
                return bllgrade.Update(model).ToString();
            }
            else if (i > 0 && j == 0)
            {
                Model.ZK_Grade model = new Model.ZK_Grade();
                model.gradeID = Convert.ToInt32(ID);
                model.gradeName = GradeName;
                model.gradeDesc = CourseDesc;
                return bllgrade.Update(model).ToString();


            }
            else
            {
                return "-1";
            }
        }
        #endregion

        #region 添加新的版本
        /// <summary>
        /// 添加新的版本
        /// </summary>
        /// <param name="context">请求上下文</param>
        /// <returns>添加结果 影响行数</returns>
        private string AddNewEdition(HttpContext context)
        {
            string Name = context.Request.Form["EditionName"];
            string Desc = context.Request.Form["EditionDesc"];
            string where = " editionName='" + Name + "'";
            int i = new ZK.BLL.ZK_Edition().GetList(where).Tables[0].Rows.Count;
            if (i == 0)
            {
                Model.ZK_Edition model = new Model.ZK_Edition();
                model.editionName = Name;
                model.editionDesc = Desc;
                return blledition.Add(model).ToString();
            }
            else
            {
                return "-1";
            }
        }
        #endregion
        #region 跟新版本信息
        /// <summary>
        /// 跟新版本信息
        /// </summary>
        /// <param name="context">请求上下文</param>
        /// <returns>返回处理结果</returns>
        private string UpdateEdition(HttpContext context)
        {
            string ID = context.Request.Form["EditionID"];
            if (ID == "")
            {
                return "False";
            }
            string Name = context.Request.Form["EditionName"];
            string CourseDesc = context.Request.Form["EditionDesc"];
            string where = " editionID='" + ID + "'";
            int i = new ZK.BLL.ZK_Edition().GetList(where).Tables[0].Rows.Count;
            string whereName = " editionName='" + Name + "'";
            int j = new ZK.BLL.ZK_Edition().GetList(whereName).Tables[0].Rows.Count;
            Model.ZK_Edition edition = new ZK.BLL.ZK_Edition().GetModel(Convert.ToInt32(ID));
            if (i > 0 && Name.Equals(edition.editionName))
            {
                Model.ZK_Edition model = new Model.ZK_Edition();
                model.editionID = Convert.ToInt32(ID);
                model.editionName = Name;
                model.editionDesc = CourseDesc;
                return blledition.Update(model).ToString();
            }
            else if (i > 0 && j == 0)
            {
                Model.ZK_Edition model = new Model.ZK_Edition();
                model.editionID = Convert.ToInt32(ID);
                model.editionName = Name;
                model.editionDesc = CourseDesc;
                return blledition.Update(model).ToString();
            }
            else
            {
                return "-1";
            }

        }
        #endregion

        #region 添加新课程
        /// <summary>
        /// 添加新课程
        /// </summary>
        /// <param name="context"></param>
        /// <returns>返回添加结果 影响行数</returns>
        private string AddNewLesson(HttpContext context)
        {
            string LessonName = context.Request.Form["LessonName"];
            string LessonDesc = context.Request.Form["LessonDesc"];
            string LessonParent = context.Request.Form["LessonParent"] == "" ? "0" : context.Request.Form["LessonParent"];
            string LessonLevel = context.Request.Form["LessonLevel"] == "" ? "-1" : context.Request.Form["LessonLevel"];
            string LessonCourse = context.Request.Form["LessonCourse"];
            string LessonGrade = context.Request.Form["LessonGrade"];
            string LessonEdition = context.Request.Form["LessonEdition"];
            string TeachMB = context.Request.Form["TeachMB"];
            string TeachND = context.Request.Form["TeachND"];
            string TeachZD = context.Request.Form["TeachZD"];

            BLL.ZK_Lesson bllclass0 = new BLL.ZK_Lesson();
            Model.ZK_Lesson mdlclass0 = new Model.ZK_Lesson();
            string strSQL = " 1=1 order by lessonLevel desc";
            mdlclass0.lessonLevel = bllclass0.DataTableToList(bllclass0.GetList(strSQL).Tables[0])[0].lessonLevel + 1;

            //先通过教材 年级 版本 查询class//单元
            Model.ZK_LessonClass classmodel = new Model.ZK_LessonClass();
            StringBuilder strWhere_class = new StringBuilder();
            strWhere_class.Append(" 1=1 ");
            if (LessonCourse != "")
            {
                strWhere_class.Append(" and CourseID=" + LessonCourse);
            }
            if (LessonGrade != "")
            {
                strWhere_class.Append(" and gradeID=" + LessonGrade);
            }
            if (LessonEdition != "")
            {
                strWhere_class.Append(" and editionID=" + LessonEdition);
            }
            List<Model.ZK_LessonClass> listclass = bllclass.GetModelList(strWhere_class.ToString());
            if (listclass == null || listclass.Count < 1)
            {
                return "班级不存在";
            }
            string where = " lessonparent='" + LessonParent + "' and lessonName='" + LessonName + "'";

            int i = new ZK.BLL.ZK_Lesson().GetList(where).Tables[0].Rows.Count;
            if (i == 0)
            {
                Model.ZK_Lesson model = new Model.ZK_Lesson();
                model.lessonName = LessonName;
                model.lessonDesc = LessonDesc;
                model.lessonParent = Convert.ToInt32(LessonParent);
                if (LessonLevel != "-1" && LessonParent != "0")
                {
                    DataSet ds = blllesson.GetList(" lessonParent=" + LessonParent + " order by lessonlevel ");
                    if (ds != null && ds.Tables.Count > 0 && blllesson.DataTableToList(ds.Tables[0]).Count > 0)
                    {
                        model.lessonLevel = blllesson.DataTableToList(ds.Tables[0])[0].lessonLevel + 1;
                    }
                    else
                    {
                        model.lessonLevel = 0;
                    }
                }
                model.lessonLevel = Convert.ToInt32(LessonLevel);
                model.classID = listclass[0].classID;
                model.teachMB = TeachMB;
                model.teachND = TeachND;
                model.teachZD = TeachZD;
                return blllesson.Add(model).ToString();
            }

            else
            {
                return "-1";
            }

        }
        #endregion
        #region 更新一条lesson数据
        /// <summary>
        /// 更新一条lesson数据
        /// </summary>
        /// <param name="context"></param>
        /// <returns>更新结果 true false</returns>
        private string UpdateLesson(HttpContext context)
        {
            string LessonID = context.Request.Form["LessonID"];
            string LessonName = context.Request.Form["LessonName"];
            string LessonDesc = context.Request.Form["LessonDesc"];
            string LessonParent = context.Request.Form["LessonParent"] == "" ? "-1" : context.Request.Form["LessonParent"];
            string LessonLevel = context.Request.Form["LessonLevel"] == "" ? "-1" : context.Request.Form["LessonLevel"];
            string TeachMB = context.Request.Form["TeachMB"];
            string TeachND = context.Request.Form["TeachND"];
            string TeachZD = context.Request.Form["TeachZD"];
            string where = " lessonID='" + LessonID + "' ";
            int i = new ZK.BLL.ZK_Lesson().GetList(where).Tables[0].Rows.Count;
            string whereName = where = " lessonName='" + LessonName + "' ";
            int j = new ZK.BLL.ZK_Lesson().GetList(whereName).Tables[0].Rows.Count;
            Model.ZK_Lesson lession = new ZK.BLL.ZK_Lesson().GetModel(Convert.ToInt32(LessonID));
            if (i > 0 && LessonName.Equals(lession.lessonName))
            {
                Model.ZK_Lesson model = new Model.ZK_Lesson();
                model.lessonID = Convert.ToInt32(LessonID);
                model.lessonName = LessonName;
                model.lessonDesc = LessonDesc;
                model.lessonParent = Convert.ToInt32(LessonParent);
                model.lessonLevel = Convert.ToInt32(LessonLevel);
                model.teachMB = TeachMB;
                model.teachND = TeachND;
                model.teachZD = TeachZD;
                return blllesson.Update(model).ToString();
            }
            else if (i > 0 && j == 0)
            {
                Model.ZK_Lesson model = new Model.ZK_Lesson();
                model = blllesson.GetModel(Convert.ToInt32(LessonID));
                model.lessonName = LessonName;
                model.lessonDesc = LessonDesc;
                model.lessonParent = Convert.ToInt32(LessonParent);
                model.lessonLevel = Convert.ToInt32(LessonLevel);
                model.teachMB = TeachMB;
                model.teachND = TeachND;
                model.teachZD = TeachZD;
                return blllesson.Update(model).ToString();
            }
            else
            {
                return "-1";
            }
        }

        #endregion
        /// <summary>
        /// 文件转换 视频 1 word excel ppt
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string Convert_TeachResource(HttpContext context)
        {
            string result = "";
            string fileid = context.Request.Form["fileid"];
            string filetype = context.Request.Form["filetype"];
            int file_id = 0;

            if (!int.TryParse(fileid, out file_id))
            {
                return "false";
            }
            if (filetype == "1")
            {
                result = ConvertVideo(file_id, context);
            }
            else
            {
                result = ConvertDoc(file_id, context);
            }

            return result;
        }
        /// <summary>
        /// 获取全部的课程资源编号
        /// </summary>
        /// <param name="lessonids">"" 为全部课程 </param>
        /// <returns>资源ids</returns>
        private string GetSourceIDs(string lessonids)
        {
            string res = "";
            string strWhere = "lessonid in (" + lessonids + ")";
            BLL.ZK_LessonAndFileList bllsource = new BLL.ZK_LessonAndFileList();
            List<Model.ZK_LessonAndFileList> lists = bllsource.GetModelList(strWhere);
            if (lists != null && lists.Count > 0)
            {
                for (int i = 0; i < lists.Count; i++)
                {
                    res += lists[i].fileID.ToString() + ",";
                }
                return res.TrimEnd(',');
            }
            else
            {
                return "0";
            }
        }
        /// <summary>
        /// 通过条件获取相应的课程id集合
        /// </summary>
        /// <param name="LessonCourse"></param>
        /// <param name="LessonGrade"></param>
        /// <param name="LessonEdition"></param>
        /// <returns></returns>
        private string GetLessonIDsByCondition(string LessonCourse, string LessonGrade, string LessonEdition)
        {
            //先通过教材 年级 版本 查询class
            Model.ZK_LessonClass classmodel = new Model.ZK_LessonClass();
            StringBuilder strWhere_class = new StringBuilder();
            strWhere_class.Append(" 1=1 ");
            if (LessonCourse != "")
            {
                strWhere_class.Append(" and CourseID=" + LessonCourse);
            }
            if (LessonGrade != "")
            {
                strWhere_class.Append(" and gradeID=" + LessonGrade);
            }
            if (LessonEdition != "")
            {
                strWhere_class.Append(" and editionID=" + LessonEdition);
            }
            List<Model.ZK_LessonClass> listclass = bllclass.GetModelList(strWhere_class.ToString());
            if (listclass == null || listclass.Count < 1)
            {
                return "0";
            }
            string strWhere = " classID=" + listclass[0].classID.ToString();

            List<ZK.Model.ZK_Lesson> lessonList = new List<Model.ZK_Lesson>();

            lessonList = blllesson.GetModelList(strWhere);
            //获取总数
            //int totalNumber = blllesson.GetRecordCount(strWhere);

            //排序整合列表
            List<ZK.Model.ZK_Lesson> lessonList2 = new List<Model.ZK_Lesson>();
            List<ZK.Model.ZK_Lesson> lessonList3 = new List<Model.ZK_Lesson>();
            lessonList2 = blllesson.GetModelList(" lessonParent=0 ");
            foreach (var item in lessonList2)
            {
                lessonList3.AddRange(blllesson.GetModelList(" lessonParent=" + item.lessonID.ToString()));
            }
            string res = "";
            if (lessonList3 != null && lessonList3.Count > 0)
            {
                for (int i = 0; i < lessonList3.Count; i++)
                {
                    res += lessonList3[i].lessonID.ToString() + ",";
                }
                return res.TrimEnd(',');
            }
            return "0";
        }
        private static string SerializeJsonString(string DataList, int TotalNumber)
        {

            return "{\"DataList\":" + DataList + ",\"TotalNumber\":" + TotalNumber + "}";
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条Course数据
        /// </summary>
        /// <param name="context"></param>
        /// <returns>更新结果 true false</returns>
        private string DeleteCourse(HttpContext context)
        {
            string ID = context.Request.Form["ID"];
            int rint = 0;
            if (int.TryParse(ID, out rint))
            {
                BLL.ZK_Course bll = new BLL.ZK_Course();
                List<Model.ZK_Course> modellist = bll.GetModelList("courseID=" + rint.ToString());
                if (modellist != null && modellist.Count > 0)
                {
                    bool res = bll.Delete(modellist[0].courseID);
                    return res.ToString();
                }
                else
                {
                    return "false";
                }
            }
            else
            {
                return "false";
            }
        }

        /// <summary>
        /// 删除一条Grade数据
        /// </summary>
        /// <param name="context"></param>
        /// <returns>更新结果 true false</returns>
        private string DeleteGrade(HttpContext context)
        {
            string ID = context.Request.Form["ID"];
            int rint = 0;
            if (int.TryParse(ID, out rint))
            {
                BLL.ZK_Grade bll = new BLL.ZK_Grade();
                List<Model.ZK_Grade> modellist = bll.GetModelList("gradeID=" + rint.ToString());
                if (modellist != null && modellist.Count > 0)
                {
                    bool res = bll.Delete(modellist[0].gradeID);
                    return res.ToString();
                }
                else
                {
                    return "false";
                }
            }
            else
            {
                return "false";
            }
        }

        /// <summary>
        /// 删除一条Course数据
        /// </summary>
        /// <param name="context"></param>
        /// <returns>更新结果 true false</returns>
        private string DeleteEdition(HttpContext context)
        {
            string ID = context.Request.Form["ID"];
            int rint = 0;
            if (int.TryParse(ID, out rint))
            {
                BLL.ZK_Edition bll = new BLL.ZK_Edition();
                List<Model.ZK_Edition> modellist = bll.GetModelList("editionID=" + rint.ToString());
                if (modellist != null && modellist.Count > 0)
                {
                    bool res = bll.Delete(modellist[0].editionID);
                    return res.ToString();
                }
                else
                {
                    return "false";
                }
            }
            else
            {
                return "false";
            }
        }

        /// <summary>
        /// 视频转换
        /// </summary>
        /// <param name="fileid"></param>
        /// <param name="context"></param>
        private string ConvertVideo(int fileid, HttpContext context)
        {

            //查找要转换的文件
            Model.ZK_FileList file = new BLL.ZK_FileList().GetModel(fileid);
            if (file != null)
            {

                string hashfilename = "";
                string versionid = new BLL.miniyun_files().GetModel((int)file.fileOldID).version_id.ToString();
                string ResourcePath = context.Server.MapPath(Common.ModelSettings.CRootPath + "/" + GetFilePathByVersionID(versionid, out hashfilename) + "/" + hashfilename);
                if (ZK.Common.ModelSettings.IsModelData)
                {
                    ResourcePath = context.Server.MapPath(ZK.Common.ModelSettings.VideoPath);
                }
                string TargetPath = ResourcePath + ".flv";
                file.trastatus = 1;
                //修改 该文件的转换状态
                new BLL.ZK_FileList().Update(file);
                string ConvertResult = ZK.Common.VideoHelper.ConvertVideo_Behind(ResourcePath, TargetPath, context.Server.MapPath(ZK.Common.ModelSettings.FFmpegPath));
                if(ConvertResult=="success")
                return "true";
            }
            return "false";
        }

        /// <summary>
        /// 通过versionid来获取该文件的地址和文件的hash文件名
        /// </summary>
        /// <param name="versionid"></param>
        /// <returns>返回 12/34/56/78</returns>
        private string GetFilePathByVersionID(string versionid, out string hashfilename)
        {
            string filepath = "";
            BLL.miniyun_file_versions bll_version = new BLL.miniyun_file_versions();
            Model.miniyun_file_versions model = bll_version.GetModel(Convert.ToInt32(versionid));
            string hashname = model.file_signature;
            string Firstdir = hashname.Substring(0, 2);
            string Seconddir = hashname.Substring(2, 2);
            string Thriddir = hashname.Substring(4, 2);
            string Forthdir = hashname.Substring(6, 2);
            filepath = Firstdir + "/" + Seconddir + "/" + Thriddir + "/" + Forthdir;

            hashfilename = hashname;
            return filepath;
        }

        private string ConvertDoc(int fileid, HttpContext context)
        {
            //获取文件的路径 从源路径中查找html页的路径
            Model.ZK_FileList filelistmodel = new BLL.ZK_FileList().GetModel(Convert.ToInt32(fileid));
            if (filelistmodel != null)
            {

                string versionid = new BLL.miniyun_files().GetModel((int)filelistmodel.fileOldID).version_id.ToString();
                string hashfilename = "";
                string LDPath = Common.ModelSettings.CRootPath + "/" + GetFilePathByVersionID(versionid, out hashfilename) + "/" + hashfilename;

                if (ParseDocumentToHtml(context.Server.MapPath(LDPath), System.IO.Path.GetExtension(filelistmodel.fileName).ToLower()) == "success")
                {
                    return "true";
                }

            }
            return "false";

        }

        /// <summary>
        /// PPT 图片 word excel 文档
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="targetPath"></param>
        /// <returns></returns>
        private string ParseDocumentToHtml(string filepath, string extname)
        {
            return new ZK.WebService.ToHtml().FileToHtml(filepath, extname);
        }


    }
}