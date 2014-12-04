using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZK.Common;

namespace ZK.Manage.MoralManagement
{
    public partial class MoralManagementCategory : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            ChannelGroupDataBind();

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
            ZK.BLL.ZK_ChannelGroup bllChannelGroup = new BLL.ZK_ChannelGroup();

            //string str =  txtCategoryName.Text;+ "channelGroupName='德育分类'" + " or "
            string strSQL = "channelId=" + 2 + " And " + "channelGroupName=" + "'txtCategoryName.Text'";
            System.Data.DataSet ds = bllChannelGroup.GetList(strSQL);

            pCategoryList.DataSource = ds;
            pCategoryList.DataBind();
        }
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            ChannelGroupDataBind();
        }   
        #endregion

        #region 方法
        private void ChannelGroupDataBind()
        {

            ZK.BLL.ZK_ChannelGroup bllChannelGroup = new BLL.ZK_ChannelGroup();

            string strSQL = "channelId=" + 2 + " order by channelGroupParent,channelGroupLevel";
            System.Data.DataSet ds = bllChannelGroup.GetList(strSQL);

            pCategoryList.DataSource = ds;
            pCategoryList.DataBind();

            litCount.Text = ds.Tables[0].Rows.Count.ToString();
        }

        private void Delect(int id)
        {
            ZK.BLL.ZK_ChannelGroup bllChannelGroup = new BLL.ZK_ChannelGroup();
            ZK.Model.ZK_ChannelGroup mdlChannelGroup = new Model.ZK_ChannelGroup();

            string strSQL = "channelId=" + 2 + " and channelGroupParent= "+id ;
            System.Data.DataSet ds = bllChannelGroup.GetList(strSQL);

            if (ds.Tables[0].Rows.Count > 0)
            {
                MessageBox.Show(this, "请先删除分类中的子分类！");
            }
            else
            {
                ZK.BLL.ZK_ChannelGroupAndFileList bllGroupfileList = new BLL.ZK_ChannelGroupAndFileList();
                ZK.Model.ZK_ChannelGroupAndFileList mdlGroupfileList = new Model.ZK_ChannelGroupAndFileList();

                string strfile = " channelGroupID=" + id;
                System.Data.DataSet dsfile = bllGroupfileList.GetList(strfile);
                List<ZK.Model.ZK_ChannelGroupAndFileList> fileList = bllGroupfileList.DataTableToList(dsfile.Tables[0]);
                if (fileList.Count > 0)
                {
                    for (int i = 0; i <= fileList.Count; i++)
                    {
                        int linkId = fileList[0].ID;
                        bool msgErr = bllGroupfileList.Delete(linkId);
                    }
                }
                bool msgError = bllChannelGroup.Delete(id);
                if (msgError == true)
                    MessageBox.Show(this, "删除成功");
                else
                    MessageBox.Show(this, "删除失败");
            }
            ChannelGroupDataBind();
        }

        ZK.BLL.ZK_ChannelGroup chanelGroupbll = new BLL.ZK_ChannelGroup();
        ZK.Model.ZK_ChannelGroup chanelGroupmdl = new ZK.Model.ZK_ChannelGroup();

        private void Move(string downId, string flag)
        {
            int id = Convert.ToInt32(downId);
            chanelGroupmdl = chanelGroupbll.GetModel(id);

            int depOrder = Convert.ToInt32(chanelGroupmdl.channelGroupLevel);
            int depParentid = Convert.ToInt32(chanelGroupmdl.channelGroupParent);

            string strSQL = "";
            if (flag == "Up")
            {
                strSQL = "channelGroupParent=" + depParentid + " And " + "channelGroupLevel<" + depOrder + " Order by channelGroupLevel";
            }

            if (flag == "Down")
            {
                strSQL = "channelGroupParent=" + depParentid + " And " + "channelGroupLevel>" + depOrder + " Order by channelGroupLevel desc";
            }
            System.Data.DataSet ds = chanelGroupbll.GetList(strSQL);

            List<ZK.Model.ZK_ChannelGroup> depList = new List<Model.ZK_ChannelGroup>();
            depList = chanelGroupbll.DataTableToList(ds.Tables[0]);

            if (depList.Count > 0)
            {
                int upid = Convert.ToInt32(depList[depList.Count - 1].channelGroupLevel);

                int upOrgid = depList[depList.Count - 1].channelGroupID;
                ZK.Model.ZK_ChannelGroup depmdlB = new Model.ZK_ChannelGroup();
                depmdlB = chanelGroupbll.GetModel(upOrgid);

                ZK.Model.ZK_ChannelGroup depmdl1 = new Model.ZK_ChannelGroup();
                ZK.Model.ZK_ChannelGroup depmdl2 = new Model.ZK_ChannelGroup();

                depmdl1.channelGroupID = chanelGroupmdl.channelGroupID;
                depmdl1.channelGroupDesc = chanelGroupmdl.channelGroupDesc;
                depmdl1.channelGroupLevel = upid;
                depmdl1.channelGroupName = chanelGroupmdl.channelGroupName;
                depmdl1.channelGroupParent = chanelGroupmdl.channelGroupParent;
                depmdl1.channelID = chanelGroupmdl.channelID;

                depmdl2.channelGroupID = depmdlB.channelGroupID;
                depmdl2.channelGroupDesc = depmdlB.channelGroupDesc;
                depmdl2.channelGroupLevel = depOrder;
                depmdl2.channelGroupName = depmdlB.channelGroupName;
                depmdl2.channelGroupParent = depmdlB.channelGroupParent;
                depmdl2.channelID = depmdlB.channelID;

                chanelGroupbll.Update(depmdl1);
                chanelGroupbll.Update(depmdl2);

                ChannelGroupDataBind();
            }

            else
            {
                MessageBox.Show(this, "该节点不能移出它的父节点！");
            }
            
        }   
        
        #endregion
    }
}