using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Xml;
using System.Data;

namespace ZK.Manage.BasicInfo
{
    public partial class UserInfoList : System.Web.UI.Page
    {
        static string UserDepartID = "";
        static string UserDepart = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UserDepartID = Context.Request.QueryString["_dep"];
                UserDepart = BindOrgSlect(UserDepartID);
                UserDataBind(UserDepart);

            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="UserID">用户id</param>
        /// <param name="UserName">用户名</param>
        /// <param name="UserDepart">用户机构</param>
        private void UserDataBind(string UserDepart)
        {
            //UserState 没有用上
            string strRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><request>" +
                               "<ip>" + Page.Request.UserHostAddress + "</ip>" +
                               "<beginuserid>-1</beginuserid>" +
                               "<enduserid>-1</enduserid>" +
                               "<username> </username>" +
                               "<nickname> </nickname>" +
                               "<actualname> </actualname>" +
                               "<departname>" + UserDepart + "</departname>" +
                               "<jobnumber> </jobnumber>" +
                               "<sex>-1</sex>" +
                               "<age>-1</age>" +
                               "<online>-1</online>" +
                               "<hascamera>-1</hascamera>" +
                               "<hasmic>-1</hasmic>" +
                               "<pagesize>" + this.AspNetPager1.PageSize + "</pagesize>" +
                               "<pageindex>" + this.AspNetPager1.CurrentPageIndex + "</pageindex>" +
                               "</request> ";

            string strResponse = "";
            string strAllResponse = "";
            bool boolIS = new OpenCom.Command().Execute("Admin.SearchEnterpriseUsers", strRequest, ref strResponse, 5000);
            //xml to dataset
            StringReader stream = null;
            XmlTextReader reader = null;
            DataSet dsResponse = new DataSet();

            stream = new StringReader(strResponse);
            //从stream装载到XmlTextReader
            reader = new XmlTextReader(stream);
            dsResponse.ReadXml(reader);

            DataTable dt = dsResponse.Tables["item"];
            BLL.USERS blluser = new BLL.USERS();
            // List<Model.USERS> list = blluser.DataTableToList(dt);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["SEX"].ToString() == "1")
                    {
                        dt.Rows[i]["SEX"] = "男";
                    }
                    if (dt.Rows[i]["SEX"].ToString() == "2")
                    {
                        dt.Rows[i]["SEX"] = "女";
                    }

                }
            }
            rptUserList.DataSource = dt;
            rptUserList.DataBind();
            if (dsResponse.Tables["item"] != null)
            {
                //this.AspNetPager1.RecordCount = int.Parse(dsResponse.Tables["response"].Rows[0]["allusercount"].ToString());
            }

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
            UserDataBind(UserDepart);
        }

        /// <summary>
        /// 绑定组织结构
        /// </summary>
        private string BindOrgSlect(string DepartID)
        {
            if (DepartID == "0")
            {
                return "";
            }
            ZK.BLL.DEPARTMENTS blldepartments = new BLL.DEPARTMENTS();
            Model.DEPARTMENTS model = blldepartments.GetModel(Convert.ToInt32(DepartID));
            return model.DEPARTNAME;
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
            UserDataBind(UserDepart);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="UserID">用户id</param>
        /// <returns>是否成功 true false</returns>
        private bool DeleteUserInfo(string UserID)
        {
            DataSet dsUser = new BLL.miniyun_users().GetList("user_name='" + new BLL.USERS().GetModel(int.Parse(UserID)).USERNAME + "'");
            string strRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><request>" +
                                "<ip>" + Page.Request.UserHostAddress + "</ip>" +
                                "<userid>" + UserID + "</userid>" +
                                "</request>";
            string strResponse = "";
            bool boolIS = new OpenCom.Command().Execute("Admin.RemoveUser", strRequest, ref strResponse, 5000);
            if (dsUser.Tables[0].Rows.Count > 0)
            {
                boolIS = new BLL.miniyun_user_metas().Delete(int.Parse(dsUser.Tables[0].Rows[0]["id"].ToString()));
                boolIS = new BLL.miniyun_users().Delete(int.Parse(dsUser.Tables[0].Rows[0]["id"].ToString()));
            }
            return boolIS;
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
            return boolIS;
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
            string pwd = ZK.Common.StringPlus.StringToMD5("123456");
            string strRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><request>" +
                                "<ip>" + Page.Request.UserHostAddress + "</ip>" +
                                "<userid>" + UserID + "</userid>" +
                                "<newpwd>" + pwd + "</newpwd>" +
                                "</request>";
            string strResponse = "";
            bool boolIS = new OpenCom.Command().Execute("Admin.ResetUserPWD", strRequest, ref strResponse, 5000);
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