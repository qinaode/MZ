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
    public partial class license : System.Web.UI.Page
    {
        static int userid;
        #region 页面加载
		 protected void Page_Load(object sender, EventArgs e)
        {
            userid =Convert.ToInt32(Request.QueryString["userid"]);
            if (!IsPostBack)//第一次加载
            {   
                DataBindlicense();
                PageLoad();
            }           
        }
	#endregion

         protected void btnSearch_Click(object sender, EventArgs e)
         {
             string strtxt = txt_username.Text;
             string strWhere = " roleName like '%" + strtxt +"%'";
             DataSet ds = new ZK.BLL.ZK_RoleList().GetList(strWhere);
             cblRole.DataSource = ds;
             cblRole.DataTextField = ds.Tables[0].Columns[1].ToString();//iD
             cblRole.DataValueField = ds.Tables[0].Columns[0].ToString(); //name;
             cblRole.DataBind();

             ZK.BLL.ZK_RoleToUser bllRole = new BLL.ZK_RoleToUser();
             List<ZK.Model.ZK_RoleToUser> roleList = new List<Model.ZK_RoleToUser>();
             roleList = bllRole.DataTableToList(ds.Tables[0]);

             for (int i = 0; i < roleList.Count; i++)
             {
                 for (int j = 0; j < cblRole.Items.Count; j++)
                 {
                     if (cblRole.Items[j].Value == roleList[i].roleID.ToString())
                     {
                         cblRole.Items[j].Selected = true;
                     }
                 }
             }
         }

        #region 绑定数据
         private void DataBindlicense()
         {
             DataSet ds = new ZK.BLL.ZK_RoleList().GetAllList();
             cblRole.DataSource = ds;
             cblRole.DataTextField = ds.Tables[0].Columns[1].ToString();//iD
             cblRole.DataValueField = ds.Tables[0].Columns[0].ToString(); //name;
             cblRole.DataBind();

         }
	#endregion


         #region 保存
         protected void btnSave_Click(object sender, EventArgs e)
         {
              userid = Convert.ToInt32(Request.QueryString["userid"]);
             ZK.BLL.ZK_RoleToUser bllRole = new BLL.ZK_RoleToUser();
             if (userid != 0)
             {
                 List<ZK.Model.ZK_RoleToUser> roleList = bllRole.GetModelList("userID=" + userid);

                 string strroles = "";
                 foreach (var item in roleList)
                 {                     
                     strroles += item.ID.ToString() + ",";
                 }
                 if (strroles.Length > 0)
                 {
                     strroles = strroles.Substring(0, strroles.Length - 1);
                     bllRole.DeleteList(strroles);
                 }
                 
             }

             List<int> list = new List<int>();
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
                 Model.ZK_RoleToUser roleuser = new Model.ZK_RoleToUser();
                 roleuser.roleID = Convert.ToInt32(list[i]);
                 roleuser.userID = Convert.ToInt32(userid);
                 new ZK.BLL.ZK_RoleToUser().Add(roleuser);
             }

             Response.Write("<script>window.open('license.aspx?curp=system&ty=NoDel', '_parent', '');var api = frameElement.api, W = api.opener; api.reload();api.close();</script>");
         }
         #endregion

         private void PageLoad()
         {
             userid = Convert.ToInt32(Request.QueryString["userid"]);
             ZK.BLL.ZK_RoleToUser bllRole = new BLL.ZK_RoleToUser();
             Model.ZK_RoleToUser mdlRole = new Model.ZK_RoleToUser();
             if (userid != 0)
             {
                 DataSet ds = bllRole.GetList("userID=" + userid);

                 List<ZK.Model.ZK_RoleToUser> roleList = new List<Model.ZK_RoleToUser>();
                 roleList = bllRole.DataTableToList(ds.Tables[0]);

                 for (int i = 0; i < roleList.Count; i++)
                 {
                     for (int j = 0; j < cblRole.Items.Count; j++)
                     {
                         if (cblRole.Items[j].Value == roleList[i].roleID.ToString())
                         {
                             cblRole.Items[j].Selected = true;
                         }
                     }
                 }
                 
             }
         }

     }
 }
   

