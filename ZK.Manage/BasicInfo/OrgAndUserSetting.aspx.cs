using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Xml;
using ZK.Common;

namespace ZK.Manage.BasicInfo
{
    public partial class OrgAndUserSetting : System.Web.UI.Page
    {
        #region 定义
        ZK.BLL.DEPARTMENTS bllDepartment = new BLL.DEPARTMENTS();
        ZK.BLL.USERS bllUser = new BLL.USERS();
        ZK.BLL.DEPARTUSERS bllDepUser = new BLL.DEPARTUSERS();

        static bool IsSearch = false;
        static bool IsDep = false;

        //static string UserID = "";
        //static string UserName = "";
        static string UserDepart = "";
        static string UserState = "";
        static string depId = "";
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitBindData();

                InitGridData();

                this.TreeProduct.ExpandAll();

                btnAdd.Enabled = false;
                btnHaveUser.Enabled = false;

                AddReback();
                HavedUserReBack();

                //ClientScript.RegisterStartupScript(this.GetType(), "java", "GetDataForPaging(1,PagerDivID,'');", true);
            }
            if (this.TreeProduct.SelectedNode != null)
            {
                if (this.TreeProduct.SelectedNode.ChildNodes.Count > 0)
                {
                    btnAdd.Enabled = false;
                    btnHaveUser.Enabled = false;
                }
                else
                {
                    btnAdd.Enabled = true;
                    btnHaveUser.Enabled = true;
                }

                if (this.TreeProduct.SelectedValue != "Root")
                {
                    IsHaveChild();
                    //InitGridData();
                }
                else
                {
                    TreeNode parent = TreeProduct.SelectedNode;
                    if ((bool)parent.Expanded)
                    {
                        parent.CollapseAll();
                    }
                    else
                    {
                        parent.ExpandAll();
                    }
                }
            }

            #region 注释
            //string id = Request.QueryString["id"];
            //string IsLock = Request.QueryString["IsLock"];
            //string Pwd = Request.QueryString["Pwd"];
            //string Del = Request.QueryString["Del"];

            //if (Del == "Del")
            //{
            //    DeleteUserInfo(id);
            //}

            //if (IsLock == "lock")
            //{
            //    LockUserInfo(id);
            //}
            //if (IsLock == "unlock")
            //{
            //    UnLockUserInfo(id);
            //}

            //if (Pwd == "pwd")
            //{
            //    InitPWD(id);
            //}
            #endregion

        }

        #region 事件
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            IsSearch = true;
            string UserName = txt_username.Text.Trim();
            string UserID = txt_userid.Text.Trim();

            string strWhere = "1=1";

            if (UserName != null && UserName != "")
            {
                strWhere += " and ACTUALNAME like '%" + UserName + "%'";
            }
            if (UserID != null && UserID != "")
            {
                strWhere += " and userid like '%" + UserID + "%'";
            }
            if (depId != null && depId != "")
            {
                strWhere += " and USERID in (select USERID from DEPARTUSERS where DEPARTID= " + Convert.ToInt32(depId) + ") ";
            }

            DataSet ds = bllUser.GetList(strWhere);

            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
        }

        protected void btnHaveUser_Click(object sender, EventArgs e)
        {
            if (this.TreeProduct.SelectedNode != null && this.TreeProduct.SelectedValue != "Root")
            {
                string depId = TreeProduct.SelectedValue.ToString();
                string depName = TreeProduct.SelectedNode.Text;

                ClientScript.RegisterStartupScript(this.GetType(), "java", "AddHaveUser('" + depName + "','添加用户','" + depId + "');", true);
            }

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.TreeProduct.SelectedNode != null && this.TreeProduct.SelectedValue != "Root")
            {
                string depId = TreeProduct.SelectedValue.ToString();
                //Session["DepartmentId"] = depId;
                //Session["reBack"] = "reBack";
                ClientScript.RegisterStartupScript(this.GetType(), "java", "AddOrEditUser('" + depId + "','0','添加用户');", true);
            }

        }

        /// <summary>
        /// 操作系列按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void UserListItem_Commond(object sender, RepeaterCommandEventArgs e)
        //{

        //    if (e.CommandName == "CN_btn_Delete")
        //    {
        //        string UserID = e.CommandArgument.ToString();
        //        DeleteUserInfo(UserID);
        //    }
        //    if (e.CommandName == "CN_btn_Lock")
        //    {
        //        string UserID = e.CommandArgument.ToString();
        //        LockUserInfo(UserID);
        //    }
        //    if (e.CommandName == "CN_btn_UnLock")
        //    {
        //        string UserID = e.CommandArgument.ToString();
        //        UnLockUserInfo(UserID);
        //    }
        //    if (e.CommandName == "CN_btn_InitPWD")
        //    {
        //        string UserID = e.CommandArgument.ToString();
        //        InitPWD(UserID);
        //    }
        //    if (e.CommandName == "CN_btn_Update")
        //    {
        //        if (this.TreeProduct.SelectedNode != null && this.TreeProduct.SelectedValue != "Root")
        //        {
        //            string depId = TreeProduct.SelectedValue.ToString();
        //            string UserID = e.CommandArgument.ToString();
        //            ClientScript.RegisterStartupScript(this.GetType(), "java", "AddOrEditUser('" + e.CommandArgument + "','1','修改用户信息','" + depId + "');", true);
        //        }
        //        else
        //        {
        //            string UserID = e.CommandArgument.ToString();
        //            ClientScript.RegisterStartupScript(this.GetType(), "java", "AddOrEditUser('" + e.CommandArgument + "','1','修改用户信息','');", true);
        //        }
        //    }
        //    if (IsSearch)
        //    {
        //        UserDataBind(UserID, UserName, UserDepart, UserState);
        //    }
        //    else
        //    {
        //        UserDataBind("-1", "", "", "");
        //    }
        //}

        protected void TreeProduct_SelectedNodeChanged(object sender, EventArgs e)
        {
            depId = TreeProduct.SelectedNode.Value;

            if (this.TreeProduct.SelectedNode.ChildNodes.Count == 0)
            {
                btnAdd.Enabled = true;
                btnHaveUser.Enabled = true;
                string UserName = txt_username.Text.Trim();
                string UserID = txt_userid.Text.Trim();
                string strWhere = "1=1";
                if (UserName != null && UserName != "")
                {
                    strWhere += " and ACTUALNAME like '%" + UserName + "%'";
                }
                if (UserID != null && UserID != "")
                {
                    strWhere += " and userid like '%" + UserID + "%'";
                }

                if (depId != null && depId != "")
                {
                    strWhere += " and USERID in (select USERID from DEPARTUSERS where DEPARTID= " + Convert.ToInt32(depId) + ") ";
                }

                DataSet ds = bllUser.GetList(strWhere);

                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
            }
            if (this.TreeProduct.SelectedValue == "Root")
            {
                this.TreeProduct.CollapseAll();
            }

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CN_btn_Delete")
            {
                string UserID = e.CommandArgument.ToString();
                DeleteUserInfo(UserID);
            }
            if (e.CommandName == "CN_btn_Lock")
            {
                string UserID = e.CommandArgument.ToString();
                LockUserInfo(UserID);
            }
            if (e.CommandName == "CN_btn_UnLock")
            {
                string UserID = e.CommandArgument.ToString();
                UnLockUserInfo(UserID);
            }
            if (e.CommandName == "CN_btn_InitPWD")
            {
                string UserID = e.CommandArgument.ToString();
                InitPWD(UserID);
            }
            if (e.CommandName == "CN_btn_Update")
            {
                if (this.TreeProduct.SelectedNode != null && this.TreeProduct.SelectedValue != "Root")
                {
                    string depId = TreeProduct.SelectedValue.ToString();
                    string UserID = e.CommandArgument.ToString();
                    ClientScript.RegisterStartupScript(this.GetType(), "java", "EditUser('" + e.CommandArgument + "','1','修改用户信息','" + depId + "');", true);
                }
                else
                {
                    string UserID = e.CommandArgument.ToString();
                    ClientScript.RegisterStartupScript(this.GetType(), "java", "EditUser('" + e.CommandArgument + "','1','修改用户信息','');", true);
                }
            }

            if (e.CommandName == "CN_btn_Role")
            {
                string UserID = e.CommandArgument.ToString();
                ClientScript.RegisterStartupScript(this.GetType(), "java", "AddOrEditRole('" + UserID + "','角色授权');", true);
            }
        }

        //protected void TreeProduct_SelectedNodeChanged(object sender, EventArgs e)
        //{
        //    depId = TreeProduct.SelectedNode.Value;

        //    //if (this.TreeProduct.SelectedNode.ChildNodes.Count == 0)
        //    //{
        //    //    btnAdd.Enabled = true;
        //    //    btnHaveUser.Enabled = true;
        //    //}
        //    if (this.TreeProduct.SelectedNode != null && this.TreeProduct.SelectedValue != "Root" && this.TreeProduct.SelectedNode.ChildNodes.Count == 0)
        //    {
        //        IsDep = true;

        //        UserDataBind(UserID, UserName, UserDepart, UserState);

        //        //ClientScript.RegisterStartupScript(this.GetType(), "java", "GetDataForPaging('1','PagerDivID','" + depId + "');", true);
        //    }

        //    ClientScript.RegisterStartupScript(this.GetType(), "java", "GetDataForPaging(1,PagerDivID,'" + depId + "');", true);

        //}
        #endregion

        #region 方法

        private void IsHaveChild()
        {
            if (this.TreeProduct.SelectedNode != null)
            {
                if (this.TreeProduct.SelectedNode.ChildNodes.Count > 0)
                {
                    depId = this.TreeProduct.SelectedNode.Value;
                    TreeNode parent = TreeProduct.SelectedNode;
                    if (parent.ChildNodes.Count > 0)
                    {
                        btnAdd.Enabled = false;
                        btnHaveUser.Enabled = false;
                        if ((bool)parent.Expanded)
                        {
                            parent.CollapseAll();
                        }
                        else
                        {
                            parent.ExpandAll();
                        }

                        string strWhere = "1=1";
                        string UserName = txt_username.Text.Trim();
                        string UserID = txt_userid.Text.Trim();
                        //string strWhere = " DEPARTID=" + depId + " or DEPARTID in (select DEPARTID from DEPARTMENTS where PARENTDEPARTID=" + depId + ")";
                        strWhere += " and USERID in (select USERID from DEPARTUSERS where DEPARTID=" + depId + ") or USERID in (select USERID from DEPARTUSERS where DEPARTID in (select DEPARTID from DEPARTMENTS where PARENTDEPARTID=" + depId + "))";
                        if (UserName != null && UserName != "")
                        {
                            strWhere += " and ACTUALNAME like '%" + UserName + "%'";
                        }
                        if (UserID != null && UserID != "")
                        {
                            strWhere += " and userid like '%" + UserID + "%'";
                        }
                        DataSet ds = bllUser.GetList(strWhere);

                        GridView1.DataSource = ds.Tables[0];
                        GridView1.DataBind();
                    }
                    else
                    {
                        btnAdd.Enabled = true;
                        btnHaveUser.Enabled = true;
                    }
                }
            }
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

        private void InitBindData()
        {
            string strWhere = " 1=1 ";
            //DataSet ds = bllDepartment.GetAllList();
            DataSet ds = bllDepartment.GetList(strWhere);
            DataTable dt = ds.Tables[0];

            if (dt != null)
            {
                TreeNode vRootItem = new TreeNode();
                vRootItem.Text = "组织部门";
                vRootItem.Value = "Root";
                List<TreeNode> vNodeList = new List<TreeNode>();
                vNodeList.Add(vRootItem);
                this.TreeProduct.Nodes.Add(vRootItem);

                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["PARENTDEPARTID"].ToString() == "0")
                    {
                        TreeNode vItem = new TreeNode();
                        vItem.Text = dr["DEPARTNAME"].ToString();
                        vItem.Value = dr["DEPARTID"].ToString();
                        vItem.SelectAction = TreeNodeSelectAction.Expand;
                        vRootItem.ChildNodes.Add(vItem);
                        ChildLoad(vItem);
                    }
                }

            }
        }

        private void InitGridData()
        {
            DataSet ds = bllUser.GetAllList();

            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();

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
            InitGridData();
        }

        private void ChildLoad(TreeNode vItem)
        {
            string strWhere = "PARENTDEPARTID=" + vItem.Value + " order by ORDERVALUE";
            DataTable dt1 = bllDepartment.GetList(strWhere).Tables[0];
            if (dt1.Rows.Count > 0)
            {
                TreeNode vParentItem = vItem;

                foreach (DataRow dr1 in dt1.Rows)
                {
                    TreeNode vItem1 = new TreeNode();
                    vItem1.Text = dr1["DEPARTNAME"].ToString();
                    vItem1.Value = dr1["DEPARTID"].ToString();

                    vParentItem.ChildNodes.Add(vItem1);

                    ChildLoad(vItem1);
                }
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="UserID">用户id</param>
        /// <returns>是否成功 true false</returns>
        private void DeleteUserInfo(string UserID)
        {
            if (this.TreeProduct.SelectedNode != null)
            {
                string departmentid = this.TreeProduct.SelectedNode.Value;
                bool msgError1 = bllDepUser.Delete(Convert.ToInt32(UserID), Convert.ToInt32(departmentid));
                if (msgError1 == true)
                    MessageBox.Show(this, "删除成功");
                else
                    MessageBox.Show(this, "删除失败");

                btnAdd.Enabled = true;
                btnHaveUser.Enabled = true;

                string strWhere = "1=1";

                if (depId != null && depId != "")
                {
                    strWhere += " and USERID in (select USERID from DEPARTUSERS where DEPARTID= " + Convert.ToInt32(departmentid) + ") ";
                }

                DataSet ds = bllUser.GetList(strWhere);

                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
            }
            else
            {
                MessageBox.Show(this, "请先选择部门后，进行删除！");
                return;
            }
        }
        /// <summary>
        /// 锁定用户
        /// </summary>
        /// <param name="UserID">id</param>
        /// <returns></returns>
        private bool LockUserInfo(string UserID)
        {
            string strRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><request>" +
                                          "<ip>" + Page.Request.UserHostAddress + "</ip>" +
                                          "<userid>" + UserID + "</userid>" +
                                          "</request>";
            string strResponse = "";
            bool boolIS = new OpenCom.Command().Execute("Admin.LockUser", strRequest, ref strResponse, 5000);
            if (boolIS == true)
            {
                MessageBox.Show(this.Page, "当前用户状态已更改为解锁！");
            }
            return boolIS;

            //ZK.BLL.USERS bllUser = new BLL.USERS();
            //ZK.Model.USERS mdlUser = new Model.USERS();

            //mdlUser = bllUser.GetModel(Convert.ToInt32(UserID));
            //mdlUser.USERLOCK = 1;

            //bool bools = bllUser.Update(mdlUser);
            //if (bools == true)
            //{
            //    MessageBox.Show(this.Page, "当前用户状态已更改为解锁！");
            //}
            //return bools;
        }
        /// <summary>
        /// 解除锁定用户
        /// </summary>
        /// <param name="UserID">id</param>
        /// <returns></returns>
        private bool UnLockUserInfo(string UserID)
        {
            string strRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><request>" +
                                          "<ip>" + Page.Request.UserHostAddress + "</ip>" +
                                          "<userid>" + UserID + "</userid>" +
                                          "</request>";
            string strResponse = "";
            bool boolIS = new OpenCom.Command().Execute("Admin.UnLockUser", strRequest, ref strResponse, 5000);
            if (boolIS == true)
            {
                MessageBox.Show(this.Page, "当前用户状态已更改为锁定！");
            }
            return boolIS;
        }
        /// <summary>
        /// 更新用户实体
        /// </summary>
        /// <param name="usermodel"></param>
        /// <returns></returns>
        /// <summary>
        /// 更新用户实体
        /// </summary>
        /// <param name="usermodel"></param>
        /// <returns></returns>
        private bool UpdateUserInfo(Model.USERS usermodel)
        {
            string strRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><request>" +
                                "<ip>" + Page.Request.UserHostAddress + "</ip>" +
                                "<userid>" + usermodel.USERID.ToString() + "</userid>" +
                                "<canfindbypublicusers>" + usermodel.CANFINDBYPUBLICUSERS.ToString() + "</canfindbypublicusers>" +
                                "<nickname>" + usermodel.NICKNAME + "</nickname>" +
                                "<signature>" + usermodel.SIGNATURE + "</signature>" +
                                "<actualname>" + usermodel.ACTUALNAME + "</actualname>" +
                                "<sex>" + usermodel.SEX.ToString() + "</sex>" +
                                "<age>" + usermodel.AGE.ToString() + "</age>" +
                                "<birth_year>" + usermodel.BIRTH_YEAR.ToString() + "</birth_year>" +
                                "<birth_month>" + usermodel.BIRTH_MONTH.ToString() + "</birth_month>" +
                                "<birth_day>" + usermodel.BIRTH_DAY.ToString() + "</birth_day>" +
                                "<country>" + usermodel.COUNTRY + "</country>" +
                                "<province>" + usermodel.PROVINCE.ToString() + "</province>" +
                                "<city>" + usermodel.CITY.ToString() + "</city>" +
                                "<area>" + usermodel.AREA.ToString() + "</area>" +
                                "<address>" + usermodel.ADDRESS + "</address>" +
                                "<telephone>" + usermodel.TELEPHONE + "</telephone>" +
                                "<mobile>" + usermodel.MOBILE + "</mobile>" +
                                "<fax>" + usermodel.FAX + "</fax>" +
                                "<qq>" + usermodel.QQ + "</qq>" +
                                "<msn>" + usermodel.MSN + "</msn>" +
                                "<email>" + usermodel.EMAIL + "</email>" +
                                "<homepage>" + usermodel.HOMEPAGE + "</homepage>" +
                                "<departid>" + usermodel.DEPARTID.ToString() + "</departid>" +
                                "<departname>" + usermodel.DEPARTNAME + "</departname>" +
                                "<jobtitle>" + usermodel.JOBTITLE + "</jobtitle>" +
                                "<jobnumber>" + usermodel.JOBNUMBER + "</jobnumber>" +
                                "<introduction>" + usermodel.INTRODUCTION + "</introduction>" +
                                "</request> ";
            string strResponse = "";
            bool boolIS = new OpenCom.Command().Execute("Admin.ModifyUser", strRequest, ref strResponse, 5000);
            return boolIS;
        }
        /// <summary>
        /// 初始化密码
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        private bool InitPWD(string UserID)
        {

            string pwd = ZK.Common.StringPlus.StringToMD5("123456");
            string strRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><request>" +
                                "<ip>" + Page.Request.UserHostAddress + "</ip>" +
                                "<userid>" + UserID + "</userid>" +
                                "<newpwd>" + pwd + "</newpwd>" +
                                "</request>";
            string strResponse = "";
            bool boolIS = new OpenCom.Command().Execute("Admin.ResetUserPWD", strRequest, ref strResponse, 5000);
            if (boolIS)
            {
                MessageBox.Show(this.Page, "初始化密码成功！");
            }
            return boolIS;
        }
        /// <summary>
        /// 处理islock字段 用于按钮的显示
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected string ISLockFunc(string id)
        {
            if (id == "1")
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }

        private void SelectNodes(string DepartmentId)
        {
            foreach (TreeNode n in TreeProduct.Nodes)
            {
                ErgodicTreeView(n, DepartmentId);
            }
        }

        //下面是遍历的函数
        private void ErgodicTreeView(TreeNode tn, string DepartmentId)
        {
            if (tn == null) return;
            if (tn.Value == DepartmentId)
            {
                tn.Selected = true;
                return;
            }
            foreach (TreeNode n in tn.ChildNodes)
            {
                ErgodicTreeView(n, DepartmentId);
            }
        }

        private void AddReback()
        {
            if (Session["ReBack"] != null)
            {
                btnAdd.Enabled = true;
                btnHaveUser.Enabled = true;

                string DepartmentId = Session["DepartmentId"].ToString();
                string strWhere = "1=1";

                if (DepartmentId != null && DepartmentId != "")
                {
                    strWhere += " and USERID in (select USERID from DEPARTUSERS where DEPARTID= " + Convert.ToInt32(DepartmentId) + ") ";
                }

                DataSet ds = bllUser.GetList(strWhere);

                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();

                SelectNodes(DepartmentId);

                Session.Remove("ReBack");
                Session.Remove("DepartmentId");
            }

        }

        private void HavedUserReBack()
        {
            if (Session["ReBackHaved"] != null)
            {
                btnAdd.Enabled = true;
                btnHaveUser.Enabled = true;

                string DepartmentId = Session["haveDepartmentId"].ToString();
                string strWhere = "1=1";

                if (DepartmentId != null && DepartmentId != "")
                {
                    strWhere += " and USERID in (select USERID from DEPARTUSERS where DEPARTID= " + Convert.ToInt32(DepartmentId) + ") ";
                }

                DataSet ds = bllUser.GetList(strWhere);

                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();

                SelectNodes(DepartmentId);

                Session.Remove("ReBackHaved");
                Session.Remove("haveDepartmentId");
            }
        }
        #endregion

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            e.Row.Attributes.Add("onmouseover", "if(this!=prevselitem){this.style.backgroundColor='#Efefef'}");
            //    //当鼠标停留时更改背景色            
            e.Row.Attributes.Add("onmouseout", "if(this!=prevselitem){this.style.backgroundColor='#F8F7EF'}");
            //    //当鼠标移开时还原背景色           
            e.Row.Attributes.Add("onclick", e.Row.ClientID.ToString() + ".checked=true;selectx(this)");
            //}
        }
        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {   //鼠标移开和停留时的背景色
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //当鼠标停留时更改背景颜色
                e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#FFF9E8'");
                //当鼠标移开时还原背景颜色
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            }
        }

       
    }
}