using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZK.Common;
using System.Data;
using System.Text;

namespace ZK.Manage.TeachChannelManage
{
    public partial class LessionMagNew : System.Web.UI.Page
    {
        #region 定义
        ZK.BLL.ZK_Course bllCourse = new BLL.ZK_Course();
        ZK.BLL.ZK_Edition bllEdition = new BLL.ZK_Edition();
        ZK.BLL.ZK_Grade bllGrade = new BLL.ZK_Grade();

        ZK.BLL.ZK_Lesson bllLession = new BLL.ZK_Lesson();
        ZK.Model.ZK_Lesson mdlLession = new Model.ZK_Lesson();

        ZK.BLL.ZK_LessonClass bllclass = new BLL.ZK_LessonClass();

        string courseId;
        string editionId;
        string gradeId;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCourseList();
                BindEditionList();
                BindGradeList();

                if (Request.QueryString["flag"] == "search")
                {
                    courseId = Request.QueryString["courseId"];
                    editionId = Request.QueryString["editionId"];
                    gradeId = Request.QueryString["gradeId"];
                    for (int i = 0; i < cmbCourseList.Items.Count; i++)
                    {
                        if (cmbCourseList.Items[i].Value == courseId)
                        {
                            cmbCourseList.Items[i].Selected = true;
                        }
                    }
                    for (int i = 0; i < cmbEditionList.Items.Count; i++)
                    {
                        if (cmbEditionList.Items[i].Value == editionId)
                        {
                            cmbEditionList.Items[i].Selected = true;
                        }
                    }
                    for (int i = 0; i < cmbGradeList.Items.Count; i++)
                    {
                        if (cmbGradeList.Items[i].Value == gradeId)
                        {
                            cmbGradeList.Items[i].Selected = true;
                        }
                    }

                    Search(courseId, editionId, gradeId);
                }
                else
                {
                    AddReback();
                    LoadPageList();
                }
            }

            if (Request.QueryString["ty"] != "NoDel")
            {
                if ((Request.QueryString["ty"] == "del"))
                {
                    int id = Convert.ToInt32(Request.QueryString["id"]);

                    Delect(id);
                }
            }

            if (Server.HtmlEncode(Request.QueryString["ty"]) == "Down")
            {
                string id = Server.HtmlEncode(Request.QueryString["id"]);
                Move(id, "Down");
            }

            if (Server.HtmlEncode(Request.QueryString["ty"]) == "Up")
            {
                string id = Server.HtmlEncode(Request.QueryString["id"]);
                Move(id, "Up");
            }
        }

        #region 事件
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            courseId = cmbCourseList.Value;
            editionId = cmbEditionList.Value;
            gradeId = cmbGradeList.Value;
            ClientScript.RegisterStartupScript(this.GetType(), "java", "Search('" + courseId + "','" + editionId + "','" + gradeId + "');", true);
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            courseId = cmbCourseList.Value;
            editionId = cmbEditionList.Value;
            gradeId = cmbGradeList.Value;
            ClientScript.RegisterStartupScript(this.GetType(), "java", "AddUnit('" + courseId + "','" + editionId + "','" + gradeId + "');", true);
        }

        protected void btnLession_Click(object sender, EventArgs e)
        {
            courseId = cmbCourseList.Value;
            editionId = cmbEditionList.Value;
            gradeId = cmbGradeList.Value;
            ClientScript.RegisterStartupScript(this.GetType(), "java", "AddLession('" + courseId + "','" + editionId + "','" + gradeId + "');", true);
        }
        #endregion

        #region 方法
        private void LoadPageList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" 1=1 ");

            courseId = cmbCourseList.Value;
            strSql.Append(" and courseID=" + Convert.ToInt32(courseId));

            editionId = cmbEditionList.Value;
            strSql.Append(" and editionID=" + Convert.ToInt32(editionId));

            gradeId = cmbGradeList.Value;
            strSql.Append(" and gradeID=" + Convert.ToInt32(gradeId));

            List<Model.ZK_LessonClass> listclass = bllclass.GetModelList(strSql.ToString());
            if (listclass != null && listclass.Count > 0)
            {                
                int classId = listclass[0].classID;

                LessionDataBind(classId);
            }
            else
            {
                DataSet ds = null;
                pCategoryList.DataSource = ds;
                pCategoryList.DataBind();
                litCount.Text = "0";
                btnLession.Enabled = false;
                //MessageBox.Show(this, "该搜寻条件下没有课程信息！");
                return;
            }
        }

        private void LessionDataBind(int classId)
        {
            string strSQL = "classID=" + classId + " order by lessonLevel";
            System.Data.DataSet ds = bllLession.GetList(strSQL);

            if (ds.Tables[0].Rows.Count > 0)
            {
                btnLession.Enabled = true;
            }
            else
            {
                btnLession.Enabled =  false;
            }

            pCategoryList.DataSource = ds;
            pCategoryList.DataBind();

            litCount.Text = ds.Tables[0].Rows.Count.ToString();
        }

        private void Search(string courseId, string editionId, string gradeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" 1=1 ");

            strSql.Append(" and courseID=" + Convert.ToInt32(courseId));

            strSql.Append(" and editionID=" + Convert.ToInt32(editionId));

            strSql.Append(" and gradeID=" + Convert.ToInt32(gradeId));

            List<Model.ZK_LessonClass> listclass = bllclass.GetModelList(strSql.ToString());
            if (listclass != null && listclass.Count > 0)
            {
                btnLession.Enabled = true;
                int classId = listclass[0].classID;
                                
                LessionDataBind(classId);
            }
            else
            {
                DataSet ds = null;
                pCategoryList.DataSource = ds;
                pCategoryList.DataBind();
                litCount.Text = "0";
                btnLession.Enabled = false;
                return;
            }
        }

        private void Delect(int id)
        {
            string strSQL = "lessonParent= " + id;
            System.Data.DataSet ds = bllLession.GetList(strSQL);

            if (ds.Tables[0].Rows.Count > 0)
            {
                MessageBox.Show(this, "请先删除单元中的课程！");
            }
            else
            {
                bool msgError = bllLession.Delete(id);
                if (msgError == true)
                    MessageBox.Show(this, "删除成功");
                else
                    MessageBox.Show(this, "删除失败");
            }

            LoadPageList();
        }

        private void Move(string downId, string flag)
        {
            int id = Convert.ToInt32(downId);
            mdlLession = bllLession.GetModel(id);

            int depOrder = Convert.ToInt32(mdlLession.lessonLevel);
            int depParentid = Convert.ToInt32(mdlLession.lessonParent);

            string strSQL = "";
            if (flag == "Up")
            {
                strSQL = "lessonParent=" + depParentid + " And " + "lessonLevel<" + depOrder + " Order by lessonLevel";
            }

            if (flag == "Down")
            {
                strSQL = "lessonParent=" + depParentid + " And " + "lessonLevel>" + depOrder + " Order by lessonLevel desc";
            }
            System.Data.DataSet ds = bllLession.GetList(strSQL);

            List<ZK.Model.ZK_Lesson> depList = new List<Model.ZK_Lesson>();
            depList = bllLession.DataTableToList(ds.Tables[0]);
            if (depList.Count > 0)
            {
                int upid = Convert.ToInt32(depList[depList.Count - 1].lessonLevel);

                int upOrgid = depList[depList.Count - 1].lessonID;
                ZK.Model.ZK_Lesson depmdlB = new Model.ZK_Lesson();
                depmdlB = bllLession.GetModel(upOrgid);

                ZK.Model.ZK_Lesson depmdl1 = new Model.ZK_Lesson();
                ZK.Model.ZK_Lesson depmdl2 = new Model.ZK_Lesson();

                depmdl1.classID = mdlLession.classID;
                depmdl1.lessonDesc = mdlLession.lessonDesc;
                depmdl1.lessonLevel = upid;
                depmdl1.lessonID = mdlLession.lessonID;
                depmdl1.lessonName = mdlLession.lessonName;
                depmdl1.lessonParent = mdlLession.lessonParent;
                depmdl1.teachMB = mdlLession.teachMB;
                depmdl1.teachND = mdlLession.teachND;
                depmdl1.teachZD = mdlLession.teachZD;

                depmdl2.classID = depmdlB.classID;
                depmdl2.lessonDesc = depmdlB.lessonDesc;
                depmdl2.lessonLevel = depOrder;
                depmdl2.lessonID = depmdlB.lessonID;
                depmdl2.lessonName = depmdlB.lessonName;
                depmdl2.lessonParent = depmdlB.lessonParent;
                depmdl2.teachMB = depmdlB.teachMB;
                depmdl2.teachND = depmdlB.teachND;
                depmdl2.teachZD = depmdlB.teachZD;

                bllLession.Update(depmdl1);
                bllLession.Update(depmdl2);

                LoadPageList();
            }
            else
            {
                MessageBox.Show(this, "该节点不能移出它的父节点！");
            }
        }

        private void BindCourseList()
        {
            DataTable dt = new DataTable();

            System.Data.DataSet ds = bllCourse.GetAllList();

            dt = ds.Tables[0];

            cmbCourseList.DataSource = dt;
            cmbCourseList.DataValueField = "courseID";
            cmbCourseList.DataTextField = "courseName";
            cmbCourseList.DataBind();
        }

        private void BindEditionList()
        {
            DataTable dt = new DataTable();

            System.Data.DataSet ds = bllEdition.GetAllList();

            dt = ds.Tables[0];

            cmbEditionList.DataSource = dt;
            cmbEditionList.DataValueField = "editionID";
            cmbEditionList.DataTextField = "editionName";
            cmbEditionList.DataBind();
        }

        private void BindGradeList()
        {
            DataTable dt = new DataTable();

            System.Data.DataSet ds = bllGrade.GetAllList();

            dt = ds.Tables[0];

            cmbGradeList.DataSource = dt;
            cmbGradeList.DataValueField = "gradeID";
            cmbGradeList.DataTextField = "gradeName";
            cmbGradeList.DataBind();
        }

        private void AddReback()
        {
            if (Session["ReBack"] != null)
            {

                editionId = Session["editionId"].ToString();
                gradeId = Session["gradeId"].ToString();
                courseId = Session["courseId"].ToString();
              
                for (int i = 0; i < cmbCourseList.Items.Count; i++)
                {
                    if (cmbCourseList.Items[i].Value == courseId)
                    {
                        cmbCourseList.Items[i].Selected = true;
                    }
                }
                for (int i = 0; i < cmbEditionList.Items.Count; i++)
                {
                    if (cmbEditionList.Items[i].Value == editionId)
                    {
                        cmbEditionList.Items[i].Selected = true;
                    }
                }
                for (int i = 0; i < cmbGradeList.Items.Count; i++)
                {
                    if (cmbGradeList.Items[i].Value == gradeId)
                    {
                        cmbGradeList.Items[i].Selected = true;
                    }
                }
                               
                Session.Remove("ReBack");
                Session.Remove("editionId");
                Session.Remove("courseId");
                Session.Remove("gradeId");
            }
        }
        #endregion
    }
}