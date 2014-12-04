using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using ZK.Common;

namespace ZK.Manage.BasicInfo
{
    public partial class UserManager : System.Web.UI.Page
    {
        static bool IsSearch = false;
        static string UserID = "";
        static string UserName = "";
        static string UserDepart = "";
        static string UserState = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    UserDataBind("-1", "", "", "");
            //    BindOrgSlect();
            //}
            //else
            //{
            //    //UserDataBindByWhere(null);
            //    UserDataBind(UserID, UserName, UserDepart, UserState);
            //}
            string id = Request.QueryString["id"];
            string IsLock = Request.QueryString["IsLock"];
            string Pwd = Request.QueryString["Pwd"];
            string Del = Request.QueryString["Del"];

            if (Del == "Del")
            {
                DeleteUserInfo(id);

                
            }

            if (IsLock == "lock")
            {
                LockUserInfo(id);
            }
            if (IsLock == "unlock")
            {
                UnLockUserInfo(id);
            }

            if (Pwd == "pwd")
            {
                InitPWD(id);
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="UserID">用户id</param>
        /// <param name="UserName">用户名</param>
        /// <param name="UserDepart">用户机构</param>
        private void UserDataBind(string UserID, string UserName, string UserDepart, string UserState)
        {
            ////UserState 没有用上
            //string strRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><request>" +
            //                   "<ip>" + Page.Request.UserHostAddress + "</ip>" +
            //                   "<beginuserid>" + UserID + "</beginuserid>" +
            //                   "<enduserid>" + UserID + "</enduserid>" +
            //                   "<username>" + UserName + "</username>" +
            //                   "<nickname> </nickname>" +
            //                   "<actualname> </actualname>" +
            //                   "<departname>" + UserDepart + "</departname>" +
            //                   "<jobnumber> </jobnumber>" +
            //                   "<sex>-1</sex>" +
            //                   "<age>-1</age>" +
            //                   "<online>-1</online>" +
            //                   "<hascamera>-1</hascamera>" +
            //                   "<hasmic>-1</hasmic>" +
            //                   "<pagesize>" + this.AspNetPager1.PageSize + "</pagesize>" +
            //                   "<pageindex>" + this.AspNetPager1.CurrentPageIndex + "</pageindex>" +
            //                   "</request> ";

            //string strResponse = "";
            ////  string strAllResponse = "";
            //bool boolIS = new OpenCom.Command().Execute("Admin.SearchEnterpriseUsers", strRequest, ref strResponse, 5000);
            ////xml to dataset
            //StringReader stream = null;
            //XmlTextReader reader = null;
            //DataSet dsResponse = new DataSet();

            //stream = new StringReader(strResponse);
            ////从stream装载到XmlTextReader
            //reader = new XmlTextReader(stream);
            //dsResponse.ReadXml(reader);

            //DataTable dt = dsResponse.Tables["item"];
            //BLL.USERS blluser = new BLL.USERS();
            //// List<Model.USERS> list = blluser.DataTableToList(dt);
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    if (dt.Rows[i]["SEX"].ToString() == "1")
            //    {
            //        dt.Rows[i]["SEX"] = "男";
            //    }
            //    else
            //    {
            //        dt.Rows[i]["SEX"] = "女";
            //    }

            //}
            //rptUserList.DataSource = dt;
            //rptUserList.DataBind();
            //if (dsResponse.Tables["item"] != null)
            //{
            //    this.AspNetPager1.RecordCount = int.Parse(dsResponse.Tables["response"].Rows[0]["allusercount"].ToString());
            //}

        }
        /// <summary>
        /// 绑定用户[条件]
        /// </summary>
        private void UserDataBindByWhere(string order)
        {


            //string key = Request.Form["orderKey"].ToString();
            //string name = this.txt_username.Text;
            //ZK.BLL.USERS bllUser = new BLL.USERS();
            //System.Data.DataSet dsUser = bllUser.GetList(this.AspNetPager1.PageSize, this.AspNetPager1.CurrentPageIndex, "1=1");
            //rptUserList.DataSource = dsUser;
            //rptUserList.DataBind();


        }
        /// <summary>
        /// 搜索按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ////IsSearch = true;
            ////UserName = txt_username.Text.TrimEnd();
            ////UserID = txt_userid.Text.TrimEnd() == "" ? "-1" : txt_userid.Text.TrimEnd();
            ////UserDepart = select_Depart.Value == "全部" ? "" : select_Depart.Value;
            ////UserState = select_userstate.Value == "全部" ? "" : select_userstate.Value;
            ////UserDataBind(UserID, UserName, UserDepart, UserState);
            //ZK.BLL.USERS bllUser = new BLL.USERS();
            //int PageIndex = this.AspNetPager1.CurrentPageIndex;
            //int PageSize = this.AspNetPager1.PageSize;
            //string strUserName = txt_username.Text;
            //string strUserId = txt_userid.Text;

            //string strWhere = "";

            //if (strWhere != "" || strWhere != null)
            //{
            //    if (strUserName.Trim() != string.Empty)
            //    {
            //        strWhere = " username like '%" + strUserName + "%'";

            //        if (strUserId.Trim() != string.Empty)
            //        {
            //            strWhere =strWhere+ " and userid like '%" + strUserId + "%'";
            //        }
            //    }
            //    else
            //    {
            //        if (strUserId.Trim() != string.Empty)
            //        {
            //            strWhere = "  userid like '%" + strUserId + "%'";
            //        }
            //    }

            //    DataSet ds = new DataSet();

            //    int startindex = (Convert.ToInt32(PageIndex) - 1) * (Convert.ToInt32(PageSize)) + 1;
            //    int endindex = Convert.ToInt32(PageSize) * Convert.ToInt32(PageIndex);

            //    ds = bllUser.GetListByPage(strWhere, "", PageIndex, PageSize);

            //    //获取总数
            //    this.AspNetPager1.RecordCount = bllUser.GetRecordCount(strWhere);

            //    rptUserList.DataSource = ds;
            //    rptUserList.DataBind();
            //}

            //else
            //{
            //    UserDataBind("-1", "", "", "");
            //}
        }
        /// <summary>
        /// 性别判断
        /// </summary>
        /// <param name="sex"></param>
        /// <returns></returns>
        protected string getSexString(string sex)
        {
            if (sex.Equals("1"))
            {
                return "男";
            }
            else if (sex.Equals("2"))
            {
                return "女";
            }
            else
            {
                return "未知";
            }
        }
        /// <summary>
        /// 删除按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 初始化密码点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnInitPWD_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 点击换页事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            if (IsSearch)
            {
                UserDataBind(UserID, UserName, UserDepart, UserState);
            }
            else
            {
                UserDataBind("-1", "", "", "");
            }
        }
        /// <summary>
        /// 绑定组织结构
        /// </summary>
        private void BindOrgSlect()
        {
            ZK.BLL.DEPARTMENTS blldepartments = new BLL.DEPARTMENTS();
            List<string> list = new List<string>();
            DataSet ds = blldepartments.GetAllList();
            List<Model.DEPARTMENTS> modellist = blldepartments.DataTableToList(ds.Tables[0]);
            list.Add("全部");
            foreach (var item in modellist)
            {
                list.Add(item.DEPARTNAME);
            }
            select_Depart.DataSource = list;
            select_Depart.DataBind();
        }

        /// <summary>
        /// 操作系列按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UserListItem_Commond(object sender, RepeaterCommandEventArgs e)
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
                string UserID = e.CommandArgument.ToString();
                ClientScript.RegisterStartupScript(this.GetType(), "java", "AddOrEditUser('" + e.CommandArgument + "','1','修改用户信息');", true);
            }
            if (IsSearch)
            {
                UserDataBind(UserID, UserName, UserDepart, UserState);
            }
            else
            {
                UserDataBind("-1", "", "", "");
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="UserID">用户id</param>
        /// <returns>是否成功 true false</returns>
        private bool DeleteUserInfo(string UserID)
        {
            //string strRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><request>" +
            //                    "<ip>" + Page.Request.UserHostAddress + "</ip>" +
            //                    "<userid>" + UserID + "</userid>" +
            //                    "</request>";
            //string strResponse = "";
            //bool boolIS = new OpenCom.Command().Execute("Admin.RemoveUser", strRequest, ref strResponse, 5000);
            //return boolIS;

            ZK.BLL.USERS bllUser = new BLL.USERS();
            ZK.BLL.ZK_RoleToUser bllRoleUser = new BLL.ZK_RoleToUser();
            ZK.BLL.DEPARTUSERS bllDepUser = new BLL.DEPARTUSERS();

            //int userid = Convert.ToInt32(UserID);
            //ZK.BLL.ZK_FileList bllfilelist=new BLL.ZK_FileList();
            //DataSet dsother=bllfilelist.GetList("ownerID="+userid);
            //if(dsother.Tables[0].Rows.Count>0)
            //{
            //    List<Model.ZK_FileList> listfile = bllfilelist.DataTableToList(dsother.Tables[0]);
            //    for (int i = 0; i < listfile.Count; i++)
            //    {
            //        bllfilelist.Delete(listfile[i].fileID);
            //    }
            //}
            DataSet dsDepUser = bllDepUser.GetList(" userid=" + UserID);
            List<ZK.Model.DEPARTUSERS> listDepUser = new List<Model.DEPARTUSERS>();
            listDepUser = bllDepUser.DataTableToList(dsDepUser.Tables[0]);
            if (listDepUser.Count > 0)
            {
                for (int i = 0; i < listDepUser.Count; i++)
                {
                    bllDepUser.Delete(Convert.ToInt32(UserID), listDepUser[i].DEPARTID);
                }
            }
            DataSet dsRoleUser = bllRoleUser.GetList(" userid=" + UserID);
            List<ZK.Model.ZK_RoleToUser> listRoleUser = new List<Model.ZK_RoleToUser>();
            listRoleUser = bllRoleUser.DataTableToList(dsRoleUser.Tables[0]);
            if (listRoleUser.Count > 0)
            {
                for (int i = 0; i < listRoleUser.Count; i++)
                {
                    bllRoleUser.Delete(listRoleUser[i].ID);
                }
            }
            //mysql中人员已存在处理
            Model.USERS mdlUsers = bllUser.GetModel(Convert.ToInt32(UserID));
            BLL.miniyun_users bllNetUser = new BLL.miniyun_users();
            DataSet dsNetUser = bllNetUser.GetAllList();
            if (dsNetUser.Tables[0].Rows.Count > 0)
            {
                List<Model.miniyun_users> listNetUser = bllNetUser.DataTableToList(dsNetUser.Tables[0]);
                for (int i = 0; i < listNetUser.Count; i++)
                {
                    if (mdlUsers.USERNAME == listNetUser[i].user_name)
                    {
                        bllNetUser.Delete(listNetUser[i].id);
                    }
                }
            }
            bool msgError = bllUser.Delete(Convert.ToInt32(UserID));
            if (msgError == true)
            // MessageBox.Show(this, "删除成功");
            {
                //ClientScript.RegisterStartupScript(this.GetType(), "java", "删除成功 ", true);
                //Response.Redirect("UserManager.aspx", false);
                ZK.Common.MessageBox.ShowAndRedirect(this, "删除成功", "UserManager.aspx");

            }

            else
                ZK.Common.MessageBox.ShowAndRedirect(this, "删除失败", "UserManager.aspx");


            return msgError;

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
                MessageBox.Show(this.Page, "用户锁定成功！");
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
                MessageBox.Show(this.Page, "用户解锁成功！");
            }
            return boolIS;
        }
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
            //ResetUserPWD
            //string pwd = ZK.Common.DEncrypt.MD5Encrypt.MD5("123456");
            //string strRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><request>" +
            //                    "<ip>" + Page.Request.UserHostAddress + "</ip>" +
            //                    "<userid>" + UserID + "</userid>" +
            //                    "<newpwd>123456</newpwd>" +
            //                    "</request>";
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
    }
}