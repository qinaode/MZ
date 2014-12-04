using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZK.Common;

namespace ZK.Manage.TeachChannelManage
{
    public partial class lessonManage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Server.HtmlEncode(Request.QueryString["ty"]) == "Up")
            {
                string id = Server.HtmlEncode(Request.QueryString["id"]);
                Move(id, "Up");
            }
            if (Session["reBack"] == "reBack")
            {
                string classId = Session["classId"].ToString();
                string courseId = Session["courseId"].ToString();
                string gradeId = Session["gradeId"].ToString();

                ReloadPage();
            }
        }
        ZK.BLL.ZK_Lesson chanelGroupbll = new BLL.ZK_Lesson();
        ZK.Model.ZK_Lesson chanelGroupmdl = new ZK.Model.ZK_Lesson();
        private void Move(string downId, string flag)
        {
            int id = Convert.ToInt32(downId);
            chanelGroupmdl = chanelGroupbll.GetModel(id);

            int depOrder = Convert.ToInt32(chanelGroupmdl.lessonLevel);
            int depParentid = Convert.ToInt32(chanelGroupmdl.lessonParent);

            string strSQL = "";
            if (flag == "Up")
            {
                strSQL = "lessonParent=" + depParentid + " And " + "lessonLevel<" + depOrder + " Order by lessonLevel";
            }

            if (flag == "Down")
            {
                strSQL = "channelGroupParent=" + depParentid + " And " + "channelGroupLevel>" + depOrder + " Order by channelGroupLevel desc";
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
                depmdl1.classID = chanelGroupmdl.classID;
                depmdl1.lessonLevel = upid;
                depmdl1.lessonDesc = chanelGroupmdl.lessonDesc;
                depmdl1.lessonName = chanelGroupmdl.lessonName;
                depmdl1.lessonParent = chanelGroupmdl.lessonParent;
                depmdl1.teachMB = chanelGroupmdl.teachMB;
                depmdl1.teachND = chanelGroupmdl.teachND;
                depmdl1.teachZD = chanelGroupmdl.teachZD;

                depmdl2.lessonID = depmdlB.lessonID;
                depmdl2.lessonDesc = depmdlB.lessonDesc;
                depmdl2.lessonLevel = depOrder;
                depmdl2.classID = depmdlB.classID;
                depmdl2.lessonName = depmdlB.lessonName;
                depmdl2.lessonParent = depmdlB.lessonParent;
                depmdl2.teachMB = depmdlB.teachMB;
                depmdl2.teachND = depmdlB.teachND;
                depmdl2.teachZD = depmdlB.teachZD;

                chanelGroupbll.Update(depmdl1);
                chanelGroupbll.Update(depmdl2);

                //ChannelGroupDataBind();
            }

            else
            {
                MessageBox.Show(this, "该节点不能移出它的父节点！");
            }

        }

        private void ReloadPage()
        {
 
        }
    }
}