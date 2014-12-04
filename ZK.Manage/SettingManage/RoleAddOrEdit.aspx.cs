using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZK.Common;

namespace ZK.Manage.SettingManage
{
    public partial class RoleAddOrEdit : System.Web.UI.Page
    {
        string roleid = "";
        string roleName = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            string flag = Context.Request.QueryString["flag"];
            roleid = Context.Request.QueryString["roleid"];
           
            if (!IsPostBack)
            {
                
                if (flag == "0")
                {
                    this.div_Add.Visible = true;
                    btn_AddOrEdit.Text = "添加";
                }
                else
                {
                    //int id = Convert.ToInt32(roleid);
                    //role = new BLL.ZK_RoleList().GetModel(id);
                    Model.ZK_RoleList role = new Model.ZK_RoleList();
                    int id = Convert.ToInt32(roleid);
                    role = new BLL.ZK_RoleList().GetModel(id);
                    txtRoleName.Text = role.roleName;
                    txtRoleDesc.Text = role.roleDesc;
                    this.div_Add.Visible = true;
                    btn_AddOrEdit.Text = "保存";
                }
            }
        }
        #region 是否存在同名的角色
        //是否存在同名的角色
        private bool ExistsRole(string roleName)
        {
            bool flag = false;
            string where = "";
            DataSet ds = new ZK.BLL.ZK_RoleList().GetList(where);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow row = ds.Tables[0].Rows[i];
                if (roleName.Equals(row["roleName"].ToString()))
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }
        #endregion
        #region 添加角色
        private void AddNewRole()
        {

           // int roleid = new ZK.BLL.ZK_RoleList().GetMaxId();
            string where=" 1=1 order by roleASC desc ";
            DataSet ds = new BLL.ZK_RoleList().GetList(where);
            string asc = ds.Tables[0].Rows[0]["roleASC"].ToString();
            int roleASC = Convert.ToInt32(asc) + 1;
            int roleType = 1;
            string roleName = txtRoleName.Text;
            string roleDesc = txtRoleDesc.Text;
            if (ExistsRole(roleName) == false)
            {
                Model.ZK_RoleList role = new Model.ZK_RoleList();
                role.roleName = roleName;
                role.roleDesc = roleDesc;
                role.roleASC = roleASC;
                role.roleType = roleType;
                int i = new BLL.ZK_RoleList().Add(role);
                if (i>0)
                {
                    MessageBox.Show(this, "添加成功！");
                    
                }
                else
                {
                    MessageBox.Show(this, "添加失败！");
                    return;
                }
            }

            else
            {
                MessageBox.Show(this, "已存在该角色名称，添加失败！");
                return;
            }
        }
        #endregion
        #region 保存按钮
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (btn_AddOrEdit.Text == "添加")
            {
                if (txtRoleName.Text.TrimEnd() == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "java", "alert('请输入角色名称');", true);
                    return;
                }
                else
                {
                    AddNewRole();
                }
            }
            else
            {
                if (txtRoleName.Text.TrimEnd() == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "java", "alert('请输入角色名称');", true);
                    return;
                }
                else
                {
                    UpdataRole(roleid);
                }
            }

            Response.Write("<script>window.open('/SettingManage/RoleManage.aspx?curp=system', '_parent', '');var api = frameElement.api, W = api.opener; api.reload();api.close();</script>");
        }
        #endregion
      
        #region 修改角色

        private void UpdataRole(string roleid)
        {
            string roleName = txtRoleName.Text;
            int id = Convert.ToInt32(roleid);
            Model.ZK_RoleList role = new BLL.ZK_RoleList().GetModel(id);
            //int roleASC = Convert.ToInt32(role.roleASC);
            //int roleTyle = 1;
           
            string reRoleName = role.roleName;
            if (roleName.Equals(reRoleName) == true)
            {
                role.roleName = txtRoleName.Text;
                role.roleDesc = txtRoleDesc.Text;
                role.roleID = id;
                //role.roleASC = roleASC;
                //role.roleType = roleTyle;
                bool flag = new BLL.ZK_RoleList().Update(role);
                if (flag == true)
                {
                    MessageBox.Show(this, "修改成功！");
                   

                }
                else
                {
                    MessageBox.Show(this, "修改失败！");
                    return;
                }
            }
            else
            {
                if (ExistsRole(roleName) == false)
                {
                    role.roleName = txtRoleName.Text;
                    role.roleDesc = txtRoleDesc.Text;
                    role.roleID = id;
                    bool flag = new BLL.ZK_RoleList().Update(role);

                    if (flag == true)
                    {
                       
                        MessageBox.Show(this, "修改成功！");
                      
                    }
                    else
                    {
                        MessageBox.Show(this, "修改失败！");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show(this, "已存在该角色名称，添加失败！");
                    return;
                }
            }

        }
        #endregion
       
        
    }
}