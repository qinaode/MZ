using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using ZK.Common;

namespace ZK.Manage.BasicInfo
{
    public partial class HavedOtherUser : System.Web.UI.Page
    {
        static string depId = "";
        static string depName = "";
        ZK.BLL.DEPARTUSERS bllDepUser = new BLL.DEPARTUSERS();
        ZK.Model.DEPARTUSERS mdlDepUser = new Model.DEPARTUSERS();

        protected void Page_Load(object sender, EventArgs e)
        {
            depId = Context.Request.QueryString["depId"];
            depName = Context.Request.QueryString["depName"];

            if (!IsPostBack)//第一次加载
            {
                DataBindUser();
            }
        }

        protected void gvwDesignationName_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // 得到该控件
            GridView theGrid = sender as GridView;
            int newPageIndex = 0;
            if (e.NewPageIndex == -3)
            {
                //点击了Go按钮
                TextBox txtNewPageIndex = null;

                //GridView较DataGrid提供了更多的API，获取分页块可以使用BottomPagerRow 或者TopPagerRow，当然还增加了HeaderRow和FooterRow
                GridViewRow pagerRow = theGrid.BottomPagerRow;

                if (pagerRow != null)
                {
                    //得到text控件
                    txtNewPageIndex = pagerRow.FindControl("txtNewPageIndex") as TextBox;
                }
                if (txtNewPageIndex != null)
                {
                    //得到索引
                    newPageIndex = int.Parse(txtNewPageIndex.Text) - 1;
                }
            }
            else
            {
                //点击了其他的按钮
                newPageIndex = e.NewPageIndex;
            }
            //防止新索引溢出
            newPageIndex = newPageIndex < 0 ? 0 : newPageIndex;
            newPageIndex = newPageIndex >= theGrid.PageCount ? theGrid.PageCount - 1 : newPageIndex;

            //得到新的值
            theGrid.PageIndex = newPageIndex;

            //重新绑定
            DataBindUser();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string strtxt = txt_username.Text;

            string strWhere = " USERID not in (select USERID from DEPARTUSERS where DEPARTID = " + depId + ")";
            if (strtxt.Trim() != "")
            {
                strWhere += " and ACTUALNAME like '%" + strtxt + "%'";
            }
            DataSet ds = new ZK.BLL.USERS().GetList(strWhere);

            //if (strtxt.Trim() != "")
            //{
            //    GridView1.DataSource = ds.Tables[0].Select("ACTUALNAME like '%" + strtxt+"%'");
            //}
            GridView1.DataSource = ds.Tables[0];    
            GridView1.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            CollectSelected();

            foreach (object tmp in this.SelectedItems)
            {
                mdlDepUser.USERID = Convert.ToInt32(tmp);
                mdlDepUser.DEPARTID = Convert.ToInt32(depId);
                mdlDepUser.DEPARTNAME = depName;

                bool bools = bllDepUser.Add(mdlDepUser);
                if (bools == true)
                {
                    Session["haveDepartmentId"] = depId;
                    Session["ReBackHaved"] = "ReBackHaved";
                    MessageBox.Show(this.Page, "添加成功！");
                }
            }

            Response.Write("<script>window.open('/SettingManage/RoleManage.aspx?curp=system', '_parent', '');var api = frameElement.api, W = api.opener; api.reload();api.close();</script>");
        }

        #region 绑定数据
        private void DataBindUser()
        {
            DataSet ds = new ZK.BLL.USERS().GetList(" USERID not in (select USERID from DEPARTUSERS where DEPARTID = " + depId + ")");

            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
        }

        public string GetSex(Object obj)
        {
            string sexStr = "";
            int i = Convert.ToInt32(obj);
            if (i == 1)
            {
                sexStr = "男";
            }
            if (i == 0)
            {
                sexStr = "女";
            }
            return sexStr;
        }

        /// <summary>
        /// 从当前页收集选中项的情况
        /// </summary>
        protected void CollectSelected()
        {
            ArrayList selectedItems = null;
            if (this.SelectedItems == null)
                selectedItems = new ArrayList();
            else
                selectedItems = this.SelectedItems;

            for (int i = 0; i < this.GridView1.Rows.Count; i++)
            {
                string id = this.GridView1.Rows[i].Cells[1].Text;
                CheckBox cb = this.GridView1.Rows[i].FindControl("CheckBox1") as CheckBox;
                if (selectedItems.Contains(id) && !cb.Checked)
                    selectedItems.Remove(id);
                if (!selectedItems.Contains(id) && cb.Checked)
                    selectedItems.Add(id);
            }
            this.SelectedItems = selectedItems;
        }

        /// <summary>
        /// 获取或设置选中项的集合
        /// </summary>
        protected ArrayList SelectedItems
        {
            get
            {
                return (ViewState["mySelectedItems"] != null) ? (ArrayList)ViewState["mySelectedItems"] : null;
            }
            set
            {
                ViewState["mySelectedItems"] = value;
            }
        }
        #endregion
    }
}