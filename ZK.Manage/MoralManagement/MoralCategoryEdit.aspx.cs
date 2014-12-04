using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZK.Common;
using System.Data;

namespace ZK.Manage.MoralManagement
{

    public partial class MoralEdit : System.Web.UI.Page
    {

        private static int level;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindMoralCategory();
                LoadPage();
            }         
        }
        #region 事件
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtCategoryName.Text == string.Empty)
            {
                MessageBox.Show(this, "德育分类名称不能为空！");
                return;
            }
            ZK.BLL.ZK_ChannelGroup chanelGroupbll = new BLL.ZK_ChannelGroup();
            int groupParentID = Convert.ToInt32(cmbMoralCategory.Value);
            string CategoryName = txtCategoryName.Text;
            string CategoryDes = txtCategoryDesc.Text;

           

            ZK.Model.ZK_ChannelGroup chanelGroupmdl = new ZK.Model.ZK_ChannelGroup();

            chanelGroupmdl.channelGroupName = txtCategoryName.Text;
            chanelGroupmdl.channelGroupDesc = txtCategoryDesc.Text;
            chanelGroupmdl.channelGroupParent = Convert.ToInt32(cmbMoralCategory.Value);
            chanelGroupmdl.channelID = 2;
            

            if (Request.QueryString["ty"] == "add")
            {
                string strSQL = "channelId=" + 2 + " order by channelGroupLevel desc";
                  chanelGroupmdl.channelGroupLevel = chanelGroupbll.DataTableToList(chanelGroupbll.GetList(strSQL).Tables[0])[0].channelGroupLevel + 1;
                  if (ExistsCategory(groupParentID, CategoryName) == false)
                  {

                      chanelGroupbll.Add(chanelGroupmdl);
                  }
                  else 
                  {
                      MessageBox.Show(this,"已存在该分类！");
                      return;
                  }
            }
            //备注：在添加子分类时候需要使得分类列表为只读
            if (Request.QueryString["ty"] == "addchild")
            {
                //cmbMoralCategory.SelectedIndex=groupParentID
               // cmbMoralCategory.Disabled = true;
                string strSQL = "channelId=" + 2 + " order by channelGroupLevel desc";

                chanelGroupmdl.channelGroupLevel = chanelGroupbll.DataTableToList(chanelGroupbll.GetList(strSQL).Tables[0])[0].channelGroupLevel + 1;
                if (ExistsCategory(groupParentID, CategoryName) == false)
                {

                    chanelGroupbll.Add(chanelGroupmdl);
                }
                else
                {
                    MessageBox.Show(this, "已存在该分类！");
                    return;
                }
            }

            if (Request.QueryString["ty"] == "edit")
            {
                int groupId=Convert.ToInt32(Request.QueryString["id"]);

                chanelGroupmdl.channelGroupID = Convert.ToInt32(Request.QueryString["id"]);
                chanelGroupmdl.channelGroupLevel = level;
                ZK.Model.ZK_ChannelGroup channelgroup=new ZK.BLL.ZK_ChannelGroup().GetModel(groupId);
               
                if(groupParentID.Equals(channelgroup.channelGroupParent)&&CategoryName.Equals(channelgroup.channelGroupName))//只是修改了描述,没有修改所属分类和名称
                {
                    chanelGroupbll.Update(chanelGroupmdl);
                }
                else if(groupParentID.Equals(channelgroup.channelGroupParent))//只是修改了名称和描述,没有修改所属分类
                {
                    if(ExistsCategory(groupParentID,  CategoryName)==false)
                    {
                        chanelGroupbll.Update(chanelGroupmdl);
                    }
                }
                else if (groupParentID != (channelgroup.channelGroupParent))
                {
                    string strSQL = "channelId=" + 2 + " order by channelGroupLevel desc";
                    chanelGroupmdl.channelGroupLevel = chanelGroupbll.DataTableToList(chanelGroupbll.GetList(strSQL).Tables[0])[0].channelGroupLevel + 1;
                    if (ExistsCategory(groupParentID, CategoryName) == false)
                    {

                        //chanelGroupmdl.channelGroupLevel = level;
                        chanelGroupmdl.channelGroupID = Convert.ToInt32(Request.QueryString["id"]);
                        chanelGroupbll.Update(chanelGroupmdl);
                    }
                   
                }
                else
                {
                    MessageBox.Show(this, "已存在该分类！");
                    return;
                }
              /*
               chanelGroupmdl.channelGroupLevel = level;
                chanelGroupmdl.channelGroupID = Convert.ToInt32(Request.QueryString["id"]);


                chanelGroupbll.Update(chanelGroupmdl);
               * */
            }

            Response.Write("<script>window.open('MoralManagementCategory.aspx?curp=moral&ty=NoDel', '_parent', '');var api = frameElement.api, W = api.opener; api.reload();api.close();</script>");               
        }

        #endregion

        #region  方法
        private void LoadPage()
        {
            
            ZK.BLL.ZK_ChannelGroup chanelGroupbll = new BLL.ZK_ChannelGroup();
            ZK.Model.ZK_ChannelGroup chanelGroupmdl = new ZK.Model.ZK_ChannelGroup();

            
            int id = Convert.ToInt32(Request.QueryString["id"]);
            chanelGroupmdl=chanelGroupbll.GetModel(id);

            if (Request.QueryString["ty"] == "addchild")
            {
                cmbMoralCategory.Value = chanelGroupmdl.channelGroupID.ToString();
                cmbMoralCategory.Disabled = true;
               
            }

            if (Request.QueryString["ty"] == "edit")
            {
                level = Convert.ToInt32(chanelGroupmdl.channelGroupLevel);
                txtCategoryName.Text = chanelGroupmdl.channelGroupName;
                txtCategoryDesc.Text = chanelGroupmdl.channelGroupDesc;
                cmbMoralCategory.Value = chanelGroupmdl.channelGroupParent.ToString();                
            }
        }

        private void BindMoralCategory()
        {
            DataTable dt = new DataTable();
            ZK.BLL.ZK_ChannelGroup bllChannelGroup = new BLL.ZK_ChannelGroup();

            string strWhere = " channelID=2";
            System.Data.DataSet dsChannelGroup = bllChannelGroup.GetList(strWhere);

            dt = dsChannelGroup.Tables[0];

            cmbMoralCategory.DataSource = dt;
            cmbMoralCategory.DataValueField = "channelGroupID";
            cmbMoralCategory.DataTextField = "channelGroupName";
            cmbMoralCategory.DataBind();
        }

        #region 是否存在相同的分类
        private bool ExistsCategory(int groupParentID, string CategoryName)
        {
            bool flag = false;
            string where = "channelID=2 and channelGroupParent=" + groupParentID.ToString();
            DataSet ds = new ZK.BLL.ZK_ChannelGroup().GetList(where);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow row = ds.Tables[0].Rows[i];
                if (CategoryName.Equals(row["channelGroupName"].ToString()))
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }
        #endregion
      
        #endregion

    }
}