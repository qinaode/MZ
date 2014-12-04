using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZK.Manage.BasicInfo
{
    public partial class AddHaveUser : System.Web.UI.Page
    {
        static string userid = "";
        static string roleid = "";
        static string depId = "";
        static string depName = "";
        static string roleflag = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            userid = Request.QueryString["userid"];
            depId = Context.Request.QueryString["depId"];
            depName = Context.Request.QueryString["depName"];
            roleflag = Context.Request.QueryString["roleuser"];
            roleid = Context.Request.QueryString["roleid"];

            if (!IsPostBack)//第一次加载
            {
                if (roleflag == "roleuser")
                {
                    BindRoleUser();
                    PageLoad();
                }
                else
                    DataBindlicense();
            }
        }

        #region 事件
        #region 绑定数据
        private void DataBindlicense()
        {
            DataSet ds = new ZK.BLL.USERS().GetList("DEPARTID <> " + depId);
            cblRole.DataSource = ds;
            cblRole.DataTextField = ds.Tables[0].Columns[6].ToString();//iD
            cblRole.DataValueField = ds.Tables[0].Columns[0].ToString(); //name;
            cblRole.DataBind();

        }
        #endregion


        #region 事件
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string strtxt = txt_username.Text;
            string strWhere = " ACTUALNAME like '%" + strtxt + "%'";
            DataSet ds = new ZK.BLL.USERS().GetList(strWhere);
            cblRole.DataSource = ds;
            cblRole.DataTextField = ds.Tables[0].Columns[6].ToString();//iD
            cblRole.DataValueField = ds.Tables[0].Columns[0].ToString(); //name;
            cblRole.DataBind();

            if (roleflag == "roleuser")
            {
                ZK.BLL.USERS bllRole = new BLL.USERS();
                List<ZK.Model.USERS> roleList = new List<Model.USERS>();
                roleList = bllRole.DataTableToList(ds.Tables[0]);

                for (int i = 0; i < roleList.Count; i++)
                {
                    for (int j = 0; j < cblRole.Items.Count; j++)
                    {
                        if (cblRole.Items[j].Value == roleList[i].USERID.ToString())
                        {
                            cblRole.Items[j].Selected = true;
                        }
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<int> list = new List<int>();
            
            if (roleflag == "roleuser")
            {
                int m;
                for (int n = 0; n < cblRole.Items.Count; n++)
                {
                    if (cblRole.Items[n].Selected == false)
                    {
                        m = Convert.ToInt32(cblRole.Items[n].Value);
                        list.Add(m);

                    }
                }
                for (int i = 0; i < list.Count; i++)
                {
                    int roleUserId = Convert.ToInt32(list[i]);
                    int Id;
                    ZK.BLL.ZK_RoleToUser bllRoleUser = new BLL.ZK_RoleToUser();
                    DataSet ds = bllRoleUser.GetList(" roleID =" + roleid + " and userid=" + roleUserId);

                    List<ZK.Model.ZK_RoleToUser> roleUserList = new List<Model.ZK_RoleToUser>();
                    roleUserList = bllRoleUser.DataTableToList(ds.Tables[0]);

                    if (roleUserList.Count > 0)
                    {
                        Id = roleUserList[0].ID;
                        bllRoleUser.Delete(Id);
                    }     
                }

                Response.Write("<script>window.open('/SettingManage/RoleManage.aspx?curp=system', '_parent', '');var api = frameElement.api, W = api.opener; api.reload();api.close();</script>");

            }
            else
            {
                int j;
                for (int i = 0; i < cblRole.Items.Count; i++)
                {
                    if (cblRole.Items[i].Selected == true)
                    {
                        j = Convert.ToInt32(cblRole.Items[i].Value);
                        list.Add(j);

                    }
                }
                for (int i = 0; i < list.Count; i++)
                {
                    Model.DEPARTUSERS roleuser = new Model.DEPARTUSERS();
                    roleuser.USERID = Convert.ToInt32(list[i]);
                    roleuser.DEPARTID = Convert.ToInt32(depId);
                    roleuser.DEPARTNAME = depName;

                    new ZK.BLL.DEPARTUSERS().Add(roleuser);
                }

                Response.Write("<script>window.open('OrgAndUserSetting.aspx?curp=system', '_parent', '');var api = frameElement.api, W = api.opener; api.reload();api.close();</script>");
            }
        }
        #endregion
        private void BindRoleUser()
        {
            DataSet ds = new ZK.BLL.USERS().GetList("userid in (select userid from zk_roletouser where roleID =" + roleid + ")");
            cblRole.DataSource = ds;
            cblRole.DataTextField = ds.Tables[0].Columns[6].ToString();//iD
            cblRole.DataValueField = ds.Tables[0].Columns[0].ToString(); //name;
            cblRole.DataBind();
        }

        private void PageLoad()
        {
            ZK.BLL.USERS bllRole = new BLL.USERS();
            if (roleid != null && roleid != "")
            {
                DataSet ds = bllRole.GetList("userid in (select userid from zk_roletouser where roleID =" + roleid + ")");

                List<ZK.Model.USERS> roleList = new List<Model.USERS>();
                roleList = bllRole.DataTableToList(ds.Tables[0]);

                for (int i = 0; i < roleList.Count; i++)
                {
                    for (int j = 0; j < cblRole.Items.Count; j++)
                    {
                        if (cblRole.Items[j].Value == roleList[i].USERID.ToString())
                        {
                            cblRole.Items[j].Selected = true;
                        }
                    }
                }

            }
        }
        #endregion

    }
}