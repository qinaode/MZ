using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZK.Common;
using System.Text;

namespace ZK.Manage.TeachChannelManage
{
    public partial class LessonMagEdit : System.Web.UI.Page
    {
        #region 定义
        static int lessionId;
        static int lessionPId;
        static int gradeId;
        static int classId;
        static int courseId;
        static int editionId;
        static int lessionclassId;
        static int level;
        static string flag;

        ZK.BLL.ZK_Lesson bllLession = new BLL.ZK_Lesson();      

        ZK.BLL.ZK_Grade bllGrade = new BLL.ZK_Grade();
        ZK.Model.ZK_Grade mdlGrade = new Model.ZK_Grade();

        ZK.BLL.ZK_Course bllCourse = new BLL.ZK_Course();
        ZK.Model.ZK_Course mdlCourse = new Model.ZK_Course();

        ZK.BLL.ZK_Edition bllEdition = new BLL.ZK_Edition();
        ZK.Model.ZK_Edition mdlEdition = new Model.ZK_Edition();

        ZK.BLL.ZK_LessonClass bllLessonClass = new BLL.ZK_LessonClass();
        ZK.Model.ZK_LessonClass mdlLessonClass = new Model.ZK_LessonClass();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                flag=Request.QueryString["ty"];
                courseId = Convert.ToInt32(Request.QueryString["courseId"]);
                editionId = Convert.ToInt32(Request.QueryString["editionId"]);
                gradeId = Convert.ToInt32(Request.QueryString["gradeId"]);

                BindMoralCategory();
                PageLoad();

                if (flag == "addUnit")
                {
                    trMB.Visible = false;
                    trND.Visible = false;
                    trZD.Visible = false;
                    trUnit.Visible = false;

                    lblLessonName.Text = "单元名称：";
                    lblLessonDesc.Text = "单元描述：";
                }
               
            }
        }

        #region 事件
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (flag == "addUnit")
            {
                if (txt_LessonName.Text.Trim() == string.Empty)
                {
                    MessageBox.Show(this, "课程名称不能为空！");
                    return;
                }
                AddUnitSave();
            }

            else
            {               
                #region
                if (txt_LessonName.Text.Trim() == string.Empty)
                {
                    MessageBox.Show(this, "课程名称不能为空！");
                    return;
                }
                if (selectMode.Items[selectMode.SelectedIndex].Text =="")
                {
                    MessageBox.Show(this, "所属单元不能为空！");
                    return;
                }
               
                if (flag == "addLession")
                {
                    ZK.Model.ZK_Lesson mdlLession = new Model.ZK_Lesson();
                    mdlLession.lessonName = txt_LessonName.Text;
                    mdlLession.lessonDesc = txtLessonDesc.Text;
                    mdlLession.lessonParent = Convert.ToInt32(selectMode.Value);
                    mdlLession.classID = classId;
                    List<Model.ZK_Lesson> listLessionlevel = bllLession.GetModelList("lessonParent=" + Convert.ToInt32(selectMode.Value) + " order by lessonLevel desc");
                    if (listLessionlevel != null && listLessionlevel.Count > 0)
                    {
                        mdlLession.lessonLevel = listLessionlevel[0].lessonLevel+1;
                    }
                    else
                    {
                        mdlLession.lessonLevel = 1;
                    }
                    mdlLession.teachMB = txt_LessonMB.Text;
                    mdlLession.teachND = txt_LessonND.Text;
                    mdlLession.teachZD = txt_LessonZD.Text;
                    bllLession.Add(mdlLession);
                }
                if (flag == "edit")
                {
                    ZK.Model.ZK_Lesson mdlLession = new Model.ZK_Lesson();
                    lessionId = Convert.ToInt32(Request.QueryString["id"]);
                    mdlLession = bllLession.GetModel(lessionId);
                    DataSet ds = bllLession.GetList("lessonParent=" + lessionId);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        mdlLession.lessonName = txt_LessonName.Text;
                        mdlLession.lessonDesc = txtLessonDesc.Text;
                        
                        bool bools = bllLession.Update(mdlLession);
                        if (bools == true)
                        {
                            MessageBox.Show(this, "修改成功！");
                        }
                    }
                    else
                    {
                        mdlLession.lessonParent = Convert.ToInt32(selectMode.Value);
                        mdlLession.lessonName = txt_LessonName.Text;
                        mdlLession.lessonDesc = txtLessonDesc.Text;

                        mdlLession.teachMB = txt_LessonMB.Text;
                        mdlLession.teachND = txt_LessonND.Text;
                        mdlLession.teachZD = txt_LessonZD.Text;

                        bool bools = bllLession.Update(mdlLession);
                        if (bools == true)
                        {
                            MessageBox.Show(this, "修改成功！");
                        }
                    }
                }
                #endregion
            }
            Session["reBack"] = "reBack";
            Session["editionId"] = editionId;
            Session["gradeId"] = gradeId;
            Session["courseId"] = courseId;
          
            Response.Write("<script>window.open('LessionMagNew.aspx?curp=teach', '_parent', '');var api = frameElement.api, W = api.opener; api.reload();api.close();</script>");
        }
        #endregion

        #region 方法
        private void PageLoad()
        {
            ZK.Model.ZK_Lesson mdlLession = new Model.ZK_Lesson();
            if (Server.HtmlEncode(Request.QueryString["ty"]) == "edit")
            {
                lessionId = Convert.ToInt32(Request.QueryString["id"]);
                mdlLession = bllLession.GetModel(lessionId);
                //DataSet ds = bllLession.GetList("lessonParent=" + lessionId);
                if (Request.QueryString["flag"] == "Pid")
                {
                    trMB.Visible = false;
                    trND.Visible = false;
                    trZD.Visible = false;
                    trUnit.Visible = false;

                    int classid =Convert.ToInt32(mdlLession.classID);
                    mdlLessonClass = bllLessonClass.GetModel(classid);
                    int courseid = Convert.ToInt32(mdlLessonClass.courseID);
                    int gradeid = Convert.ToInt32(mdlLessonClass.gradeID);
                    int editionId = Convert.ToInt32(mdlLessonClass.editionID);

                    lblCourseName.Text = bllCourse.GetModel(courseid).courseName;
                    lblEditionName.Text = bllEdition.GetModel(editionId).editionName;
                    lblGradeName.Text = bllGrade.GetModel(gradeid).gradeName;

                    lblLessonName.Text = "单元名称：";
                    lblLessonDesc.Text = "单元描述：";
                    txt_LessonName.Text = mdlLession.lessonName;
                    txtLessonDesc.Text = mdlLession.lessonDesc;
                }
                else
                {                    
                    level = Convert.ToInt32(mdlLession.lessonLevel);
                    lessionclassId = Convert.ToInt32(mdlLession.classID);
                    mdlLessonClass = bllLessonClass.GetModel(lessionclassId);
                    gradeId = Convert.ToInt32(mdlLessonClass.gradeID);
                    courseId = Convert.ToInt32(mdlLessonClass.courseID);
                    editionId = Convert.ToInt32(mdlLessonClass.editionID);
                    mdlCourse = bllCourse.GetModel(courseId);
                    mdlEdition = bllEdition.GetModel(editionId);
                    mdlGrade = bllGrade.GetModel(gradeId);

                    lblCourseName.Text = mdlCourse.courseName;
                    lblEditionName.Text = mdlEdition.editionName;
                    lblGradeName.Text = mdlGrade.gradeName;

                    //selectMode.Value = mdlLession.lessonParent.ToString();
                    for (int i = 0; i < selectMode.Items.Count; i++)
                    {
                        if (selectMode.Items[i].Value == mdlLession.lessonParent.ToString())
                        {
                            selectMode.Items[i].Selected = true;
                        }
                    }
                    txt_LessonMB.Text = mdlLession.teachMB;
                    txt_LessonName.Text = mdlLession.lessonName;
                    txt_LessonND.Text = mdlLession.teachND;
                    txt_LessonZD.Text = mdlLession.teachZD;
                    txtLessonDesc.Text = mdlLession.lessonDesc;
                }
            }
            else
            {
                mdlCourse = bllCourse.GetModel(courseId);
                mdlEdition = bllEdition.GetModel(editionId);
                mdlGrade = bllGrade.GetModel(gradeId);

                lblCourseName.Text = mdlCourse.courseName;
                lblEditionName.Text = mdlEdition.editionName;
                lblGradeName.Text = mdlGrade.gradeName;

                string strWhere = "courseID=" + courseId + " and gradeID=" + gradeId + " and editionID=" + editionId;

                List<Model.ZK_LessonClass> listclass = bllLessonClass.GetModelList(strWhere);
                if (listclass != null && listclass.Count > 0)
                {
                    classId = listclass[0].classID;
                }
                else
                {
                    mdlLessonClass.courseID = courseId;
                    mdlLessonClass.editionID = editionId;
                    mdlLessonClass.gradeID = gradeId;

                    bllLessonClass.Add(mdlLessonClass);
                    string strSql = "courseID=" + courseId + " and gradeID=" + gradeId + " and editionID=" + editionId;
                    List<Model.ZK_LessonClass> listClass = bllLessonClass.GetModelList(strSql);
                    classId = listClass[0].classID;
                }
            }
           
        }

        private void BindMoralCategory()
        {
            DataTable dt = new DataTable();
            string strWhere = "courseID=" + courseId + " and gradeID=" + gradeId + " and editionID=" + editionId;

            List<Model.ZK_LessonClass> listclass = bllLessonClass.GetModelList(strWhere);
            if (listclass != null && listclass.Count > 0)
            {
                classId = listclass[0].classID;
            }
            System.Data.DataSet ds = bllLession.GetList("lessonParent=0 and classID=" + classId);

            dt = ds.Tables[0];

            selectMode.DataSource = dt;
            selectMode.DataValueField = "lessonID";
            selectMode.DataTextField = "lessonName";
            selectMode.DataBind();

        }

        private void AddUnitSave()
        {
            if (txt_LessonName.Text.Trim() == string.Empty)
            {
                MessageBox.Show(this, "单元名称不能为空！");
                return;
            }
            DataSet ds=bllLession.GetAllList();
            if (ds.Tables[0].Rows.Count > 0)
            {
                List<Model.ZK_Lesson> list = bllLession.DataTableToList(ds.Tables[0]);
                for (int i = 0; i < list.Count; i++)
                {
 
                }
            }
            ZK.Model.ZK_Lesson mdlLession = new Model.ZK_Lesson();

            mdlLession.classID = classId;
            mdlLession.lessonDesc = txtLessonDesc.Text;
            List<Model.ZK_Lesson> listLessionlevel = bllLession.GetModelList("lessonParent=0 order by lessonLevel desc");
            if (listLessionlevel != null && listLessionlevel.Count > 0)
            {
                mdlLession.lessonLevel = listLessionlevel[0].lessonLevel+1;
            }
            else
            {
                mdlLession.lessonLevel = 1;
            }
            mdlLession.lessonName = txt_LessonName.Text;
            mdlLession.lessonParent = 0;

            bllLession.Add(mdlLession);
        }

       
        #endregion

    }
}