using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZK.Model;
using ZK.Common;

namespace ZK.Manage.SettingManage
{
    public partial class RoleManage : System.Web.UI.Page
    {
        BLL.ZK_RoleList bllRoleList = new BLL.ZK_RoleList();

        Model.ZK_RoleList mRoleList = new ZK_RoleList();
        ZK.BLL.CommondBase bll = new BLL.CommondBase();
       
        static string roleid = "";
        static string userid = "";
        static int total = 0;
        static int pagesize = 3;
        static int pageindex = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //第一次加载数据绑定数据源
                //repeater1.DataSource = LoadData();
                //repeater1.DataBind();

                if (!string.IsNullOrEmpty(Request.QueryString["checkedlist"]))
                {
                    DeleteList();
                }
                BindDataList();

            }
            BindDataList();

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string strtxt = txtSpecialTypeName.Text;
            string strWhere = " roleName like '%" + strtxt + "%'";
            DataSet ds = new ZK.BLL.ZK_RoleList().GetList(strWhere);
            repeater1.DataSource = ds;
            repeater1.DataBind();
        }

        //protected DataTable LoadData()
        //{
        //    DataTable dt = new DataTable();
        //    //Asc 升序，降序指定 Desc
        //    //string where="";
        //    DataSet ds = new ZK.BLL.ZK_RoleList().GetAllList();
        //    dt = ds.Tables[0];
        //    total=ds.Tables[0].Rows.Count;
        //    return dt;
        //}
        protected void roleListItem_Commond(object sender, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "CN_btn_Delete")
            {
                string roleid = e.CommandArgument.ToString();
                Delect(roleid);
            }
            if (e.CommandName == "CN_btn_MoveUp")
            {
                string roleid = e.CommandArgument.ToString();
                Move(roleid, "Up");
              

            }
            if (e.CommandName == "CN_btn_MoveDown")
            {
                string roleid = e.CommandArgument.ToString();
                Move(roleid, "Down");
              
            }
            
        }
        #region 删除
        private void Delect(string roleid)
        {
            bool flag = bllRoleList.Delete(Convert.ToInt32(roleid));
            if (flag == true)
            {
                MessageBox.Show(this, "删除成功");
            }
            else
            {
                MessageBox.Show(this, "删除失败");
            }
            BindDataList();
        }

        #endregion

        private void  DeleteList()
        {
            string strDelList = Request.QueryString["checkedlist"];
             string[] strid = strDelList.Split(',');

             for (int i = 0; i < strid.Length; i++)
             {
                 bllRoleList.Delete(Convert.ToInt32(strid[i]));
             }
             BindDataList();
        }

        #region 操作行为
        private void Move(string roleid, string flag)
        {
            int id = Convert.ToInt32(roleid);
            mRoleList = bllRoleList.GetModel(id);

            int roleAsc = Convert.ToInt32(mRoleList.roleASC);

            string strSQL = "";
            if (flag.ToLower() == "up")
            {
                //strSQL = " roleAsc<" + roleAsc+ " Order by roleAsc";
                strSQL = " roleAsc>" + roleAsc + " Order by roleAsc asc";
            }
            if (flag.ToLower() == "down")
            {
                //strSQL = " roleAsc>" + roleAsc + " Order by roleAsc desc";
                strSQL = " roleAsc<" + roleAsc + " Order by roleAsc desc";
            }

            DataSet ds = bllRoleList.GetList(strSQL);
            List<ZK.Model.ZK_RoleList> depList = new List<Model.ZK_RoleList>();
            depList = bllRoleList.DataTableToList(ds.Tables[0]);
            if (depList.Count > 0)
            {
                //int upid = Convert.ToInt32(depList[depList.Count - 1].roleASC);
               // int upRoleid = depList[depList.Count - 1].roleID;

                int up_RoleASC = Convert.ToInt32(depList[0].roleASC);
                int up_Roleid = depList[0].roleID;

                ZK.Model.ZK_RoleList role = new ZK.Model.ZK_RoleList();
                role = bllRoleList.GetModel(up_Roleid);

                ZK.Model.ZK_RoleList roleA = new Model.ZK_RoleList();
                ZK.Model.ZK_RoleList roleB = new Model.ZK_RoleList();

                roleA.roleID = mRoleList.roleID;
                roleA.roleName = mRoleList.roleName;
                roleA.roleDesc = mRoleList.roleDesc;
                // roleA.roleASC=upid
                roleA.roleASC = up_RoleASC;
                roleA.roleType = mRoleList.roleType;

                roleB.roleID = role.roleID;
                roleB.roleName = role.roleName;
                roleB.roleDesc = role.roleDesc;
                roleB.roleType = role.roleType;
                roleB.roleASC = roleAsc;

                bllRoleList.Update(roleA);
                bllRoleList.Update(roleB);

                BindDataList();

            }

        }
        #endregion

        #region 加载分页数据
        
        #endregion
        private void BindDataList()
        {
            string strSelect = " * ";
            string strTable = " ZK_RoleList ";
            string strPrimaryKey = "";
            string strOrderby = " 1=1 Order by roleAsc desc ";
            string strWhere = "";
            int BlPage=1;
           // int pagesize=3;
            //if (bllRoleList.GetAllList().Tables[0].Rows.Count > 0)
            //{
            //    total = bllRoleList.GetAllList().Tables[0].Rows.Count;
            //}
            DataSet ds=new DataSet();
            //ds = bll.GetList(strSelect,strTable,strPrimaryKey, strOrderby,pagesize,pageindex,strWhere,BlPage);
            //ds = bllRoleList.GetAllList();
            ds = bllRoleList.GetList(strOrderby);
            repeater1.DataSource = ds;
            repeater1.DataBind();


        }
    }
}