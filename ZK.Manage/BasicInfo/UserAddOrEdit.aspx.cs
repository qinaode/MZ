using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Configuration;
using System.Text;
using System.Net;
using ZK.Common;
using System.Security.Cryptography;

namespace ZK.Manage.BasicInfo
{
    public partial class UserAddOrEdit : System.Web.UI.Page
    {
        static string userid = "";
        static string IsDepOrUser = "";
        static string depId = "";
        string flag = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                flag = Context.Request.QueryString["flag"];
                userid = Context.Request.QueryString["userid"];
                IsDepOrUser = Context.Request.QueryString["IsDepOrUser"];
                depId = Context.Request.QueryString["depId"];
                BindOrgSlect();
                BindAgeSelect();
                
                if (IsDepOrUser == "user")
                {
                    UserEdit();

                }
                if (IsDepOrUser == "dep")
                {
                    OrgAndUserEdit();
                }
                Session.Remove("ReBack");
                Session.Remove("DepartmentId");
            }
        }

        /// <summary>
        /// 更新用户实体
        /// </summary>
        /// <param name="usermodel"></param>
        /// <returns></returns>
        private bool UpdateUserInfo(Model.USERS usermodel)
        {
            #region 更新信息至知识库&IM
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
            #endregion

            #region 更新SQl库
            if (IsDepOrUser == "dep")
            {
                ZK.BLL.DEPARTUSERS bllDepUser = new BLL.DEPARTUSERS();
                ZK.Model.DEPARTUSERS mdlDepUser = new Model.DEPARTUSERS();

                mdlDepUser.DEPARTID = Convert.ToInt32(select_departments.Value);
                mdlDepUser.USERID = usermodel.USERID;
                mdlDepUser.DEPARTNAME = select_departments.Items[select_departments.SelectedIndex].Text;

                bllDepUser.Update(mdlDepUser);
            }

            #endregion

            #region 更新信息至pan

            string strUserID = new BLL.miniyun_users().GetList("user_name='" + usermodel.USERNAME + "'").Tables[0].Rows[0]["id"].ToString();

            //更新邮件至meta表
            List<ZK.Model.miniyun_user_metas> modelMetasList = new BLL.miniyun_user_metas().GetModelList("user_id=" + strUserID + " and meta_key='email'");
            ZK.Model.miniyun_user_metas modelMetas = new Model.miniyun_user_metas();
            if (modelMetasList.Count > 0)
            {
                modelMetas = modelMetasList[0];
                modelMetas.meta_value = usermodel.EMAIL;
                modelMetas.updated_at = DateTime.Now;
                boolIS = new BLL.miniyun_user_metas().Update(modelMetas);
            }
            //更新昵称至meta表
            modelMetasList = new BLL.miniyun_user_metas().GetModelList("user_id=" + strUserID + " and meta_key='nick'");
            if (modelMetasList.Count > 0)
            {
                modelMetas = modelMetasList[0];
                modelMetas.meta_value = usermodel.NICKNAME;
                modelMetas.updated_at = DateTime.Now;
                boolIS = new BLL.miniyun_user_metas().Update(modelMetas);
            }

            //更新空间至meta表
            modelMetasList = new BLL.miniyun_user_metas().GetModelList("user_id=" + strUserID + " and meta_key='space'");
            if (modelMetasList.Count > 0)
            {
                modelMetas = modelMetasList[0];
                modelMetas.meta_value = txt_space.Value.TrimEnd();
                modelMetas.updated_at = DateTime.Now;
                boolIS = new BLL.miniyun_user_metas().Update(modelMetas);
            }
            #endregion

            return boolIS;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        private bool AddNewUserInfo()
        {
            string UserID = txtUserID.Text.Trim();
            string where = "USERID=" + UserID;
            
            string strUserName = "userName='" + txtUserName.Text.Trim() + "'";
            string str = "user_name='" + txtUserName.Text.Trim() + "'";
            DataSet ds1 = new ZK.BLL.USERS().GetList(strUserName);
            DataSet ds2 = new ZK.BLL.miniyun_users().GetList(str);
            int j = ds1.Tables[0].Rows.Count;
            int k = ds2.Tables[0].Rows.Count;
            DataSet ds = new ZK.BLL.USERS().GetList(where);
            int i = ds.Tables[0].Rows.Count;
            if (i == 0)
            {
                if (j == 0&&k==0)
                {
                    #region 初始化用户信息
                    Model.USERS usermodel = new Model.USERS();
                    usermodel.USERID = Convert.ToInt32(txtUserID.Text.TrimEnd());
                    usermodel.USERNAME = txtUserName.Text.TrimEnd();
                    usermodel.USERTYPE = 1;
                    if (check_Comm.Checked)
                    {
                        usermodel.CANFINDBYPUBLICUSERS = 1;
                    }
                    else
                    {
                        usermodel.CANFINDBYPUBLICUSERS = 0;
                    }
                    usermodel.NICKNAME = txtnickname.Text;
                    usermodel.ACTUALNAME = txtactualname.Text;
                    usermodel.SEX = Convert.ToInt32(select_Sex.Value);
                    usermodel.AGE = Convert.ToInt32(select_Age.Value);
                    //网盘配额
                    //txt_space.Value = ReadDefSpace();
                    //虚拟数据
                    usermodel.BIRTH_YEAR = 1990;
                    usermodel.BIRTH_MONTH = 1;
                    usermodel.BIRTH_DAY = 1;
                    usermodel.COUNTRY = 1;
                    usermodel.PROVINCE = 1;
                    usermodel.CITY = 1;

                    usermodel.ADDRESS = txt_address.Value.TrimEnd();
                    usermodel.TELEPHONE = txt_telephone.Value.TrimEnd();
                    usermodel.MOBILE = txt_mobile.Value.TrimEnd();
                    usermodel.FAX = txt_Fax.Value.TrimEnd();
                    usermodel.QQ = txt_qq.Value.TrimEnd();
                    usermodel.MSN = txt_msn.Value.TrimEnd();
                    usermodel.EMAIL = txt_email.Value.TrimEnd();
                    usermodel.HOMEPAGE = txt_homepage.Value.TrimEnd();
                    usermodel.DEPARTID = Convert.ToInt32(select_departments.Value);
                    usermodel.DEPARTNAME = select_departments.DataTextField;
                    usermodel.JOBTITLE = txt_jobtitle.Value.TrimEnd();
                    usermodel.JOBNUMBER = txt_jobnumber.Value.TrimEnd();
                    usermodel.INTRODUCTION = "";
                    if (txtPwd.Text == string.Empty)
                    {
                        usermodel.PWD = ZK.Common.StringPlus.StringToMD5("123456");
                    }
                    else
                    {
                        usermodel.PWD = ZK.Common.StringPlus.StringToMD5(txtPwd.Text);
                    }

                    //  usermodel.SALT = "";

                    #endregion

                    #region 添加信息至知识库&IM
                    string strRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><request>" +
                                  "<ip>" + Page.Request.UserHostAddress + "</ip>" +
                                  "<userid>" + usermodel.USERID.ToString() + "</userid>" +
                                 "<username>" + usermodel.USERNAME + "</username>" +
                                  "<usertype>" + usermodel.USERTYPE.ToString() + "</usertype>" +
                                  "<canfindbypublicusers>" + usermodel.CANFINDBYPUBLICUSERS.ToString() + "</canfindbypublicusers>" +
                                  "<nickname>" + usermodel.NICKNAME + "</nickname>" +
                                  "<actualname>" + usermodel.ACTUALNAME + "</actualname>" +
                                   "<sex>" + usermodel.SEX.ToString() + "</sex>" +
                                  "<age>" + usermodel.AGE.ToString() + "</age>" +
                                  "<birth_year>" + usermodel.BIRTH_YEAR.ToString() + "</birth_year>" +
                                  "<birth_month>" + usermodel.BIRTH_MONTH.ToString() + "</birth_month>" +
                                  "<birth_day>" + usermodel.BIRTH_DAY.ToString() + "</birth_day>" +
                                  "<country>" + usermodel.COUNTRY + "</country>" +
                                  "<province>" + usermodel.PROVINCE.ToString() + "</province>" +
                                  "<city>" + usermodel.CITY.ToString() + "</city>" +
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
                                  "<password>" + usermodel.PWD + "</password>" +
                                  "<salt>" + usermodel.SALT + "</salt>" +
                                  "</request> ";
                    string strResponse = "";
                    bool boolIS = new OpenCom.Command().Execute("Admin.AddUser", strRequest, ref strResponse, 5000);
                    #endregion

                    #region 添加至SQL库中
                    if (IsDepOrUser == "dep")
                    {
                        ZK.BLL.DEPARTUSERS bllDepUser = new BLL.DEPARTUSERS();
                        ZK.Model.DEPARTUSERS mdlDepUser = new Model.DEPARTUSERS();

                        mdlDepUser.DEPARTID = Convert.ToInt32(depId);
                        mdlDepUser.USERID = Convert.ToInt32(UserID);
                        mdlDepUser.DEPARTNAME = select_departments.DataTextField;

                        bllDepUser.Add(mdlDepUser);
                    }
                    #endregion
                    #region 添加信息至pan

                    //添加至user表
                    ZK.Model.miniyun_users modelUser = new Model.miniyun_users();
                    modelUser.user_uuid = ZK.Common.StringPlus.StringToMD5(DateTime.Now.ToString());
                    modelUser.id = usermodel.USERID;
                    modelUser.user_name = usermodel.USERNAME;
                    modelUser.salt = ZK.Common.Assistant.GetRandomCode(6);
                    modelUser.user_status = 1;
                    modelUser.user_pass = usermodel.PWD;
                    modelUser.created_at = DateTime.Now;
                    new BLL.miniyun_users().Add(modelUser);

                    System.Data.DataSet dsUser = new BLL.miniyun_users().GetList("user_uuid='" + modelUser.user_uuid + "'");

                    //添加邮件至meta表
                    ZK.Model.miniyun_user_metas modelMetas = new Model.miniyun_user_metas();
                    modelMetas.user_id = int.Parse(dsUser.Tables[0].Rows[0]["id"].ToString());
                    modelMetas.meta_key = "email";
                    modelMetas.meta_value = usermodel.EMAIL;
                    modelMetas.created_at = DateTime.Now;
                    boolIS = new BLL.miniyun_user_metas().Add(modelMetas);

                    //添加昵称至meta表
                    modelMetas.user_id = int.Parse(dsUser.Tables[0].Rows[0]["id"].ToString());
                    modelMetas.meta_key = "nick";
                    modelMetas.meta_value = usermodel.NICKNAME;
                    modelMetas.created_at = DateTime.Now;
                    boolIS = new BLL.miniyun_user_metas().Add(modelMetas);

                    //添加权限至meta表
                    modelMetas.user_id = int.Parse(dsUser.Tables[0].Rows[0]["id"].ToString());
                    modelMetas.meta_key = "is_admin";
                    modelMetas.meta_value = "0";
                    modelMetas.created_at = DateTime.Now;
                    boolIS = new BLL.miniyun_user_metas().Add(modelMetas);

                    //添加空间至meta表
                    modelMetas.user_id = int.Parse(dsUser.Tables[0].Rows[0]["id"].ToString());
                    modelMetas.meta_key = "space";
                    modelMetas.meta_value = txt_space.Value.TrimEnd();
                    modelMetas.created_at = DateTime.Now;
                    boolIS = new BLL.miniyun_user_metas().Add(modelMetas);


                    #endregion
                    return boolIS;
                }
                else
                {
                    MessageBox.Show(this, "该用户名已存在！");
                    return false;
                }

            }
            else
            {
                MessageBox.Show(this, "该用户ID已存在！");
                return false;
            }

        }

        /// <summary>
        /// 绑定组织结构
        /// </summary>
        private void BindOrgSlect()
        {
            //ZK.BLL.DEPARTMENTS blldepartments = new BLL.DEPARTMENTS();
            //DataSet ds = blldepartments.GetAllList();
            //List<Model.DEPARTMENTS> modellist = blldepartments.DataTableToList(ds.Tables[0]);
            ////select_departments.Items.Clear();
            //foreach (var item in modellist)
            //{
            //    select_departments.Items.Add(new ListItem(item.DEPARTNAME, item.DEPARTID.ToString()));
            //}

            DataTable dt = new DataTable();
            ZK.BLL.DEPARTMENTS blldepartments = new BLL.DEPARTMENTS();

            System.Data.DataSet ds = blldepartments.GetAllList();

            dt = ds.Tables[0];

            select_departments.DataSource = dt;
            select_departments.DataValueField = "DEPARTID";
            select_departments.DataTextField = "DEPARTNAME";
            select_departments.DataBind();

            select_departments.SelectedIndex = -1;
        }

        #region 添加或编辑按钮事件
        protected void btn_AddOrEdit_Click(object sender, EventArgs e)
        {
            int tempid = 0;
            //string userID = txtUserID.Text.;

            #region 检查必填项
            if (txtUserID.Text == string.Empty)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "java", "alert('用户ID不能为空！');", true);
                return;
            }
            if (txtUserID.Text.Length > 6)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "java", "alert('用户ID不能超过六位数！');", true);
                return;
            }
            if (!int.TryParse(txtUserID.Text, out tempid))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "java", "alert('ID必须为数字！');", true);
                return;
            }
            if (txtUserName.Text == string.Empty)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "java", "alert('用户名不能为空！');", true);
                return;
            }
            if (txtPwd.Text != txtPwdOK.Text)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "java", "alert('输入的密码不一致，请重新输入！');", true);
                return;
            }
          
            if (txtnickname.Text == string.Empty)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "java", "alert('昵称不能为空！');", true);
                return;
            }
            if (txtactualname.Text == string.Empty)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "java", "alert('真实姓名不能为空！');", true);
                return;
            }
            if (select_Sex.Value == "-1")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "java", "alert('请选择性别！');", true);
                return;
            }
            //if (select_Age.Value == "-1")
            //{
            //    ClientScript.RegisterStartupScript(this.GetType(), "java", "alert('请选择年龄！');", true);
            //    return;
            //}
            //if (Context.Request.QueryString["flag"] != "0")
            //{
            //    if (select_departments.Value == "-1")
            //    {
            //        ClientScript.RegisterStartupScript(this.GetType(), "java", "alert('请选择部门');", true);
            //        return;
            //    }
            //}
            #endregion

            if (btn_AddOrEdit.Text == "添加")
            {
                if (AddNewUserInfo())
                {
                    if (IsDepOrUser == "dep")
                    {
                        Session["DepartmentId"] = depId;
                        Session["ReBack"] = "ReBack";
                    }

                    ClientScript.RegisterStartupScript(this.GetType(), "java", "alert('添加成功');var api = frameElement.api, W = api.opener; api.reload();api.close();", true);
                }
                else
                {
                    return;
                    ////ClientScript.RegisterStartupScript(this.GetType(), "java", "alert('添加失败');var api = frameElement.api, W = api.opener; api.reload();api.close();", true);
                }
            }
            else
            {
                if (userid == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "java", "alert('数据错误');var api = frameElement.api, W = api.opener; api.reload();api.close();", true);
                    return;
                }

                //更新数据
                Model.USERS user = new Model.USERS();
                BLL.USERS blluser = new BLL.USERS();
                user = blluser.GetModel(Convert.ToInt32(userid));
                user.ACTUALNAME = txtactualname.Text;
                user.ADDRESS = txt_address.Value;

                user.AGE = Convert.ToInt32(select_Age.Value);
                //user.AREA = 0;
                //user.BIRTH_DAY = 0;
                //user.BIRTH_MONTH = 0;
                //user.BIRTH_YEAR = 0;
                //user.CANFINDBYPUBLICUSERS = 0;
                //user.CITY = 0;
                //user.CLIENTIPADDR = "暂无数据";
                //user.CLIENTLOCATION = "暂无数据";
                //user.COUNTRY = 0;

                user.DEPARTID = Convert.ToInt32(select_departments.Value);
                user.DEPARTNAME = select_departments.Items[select_departments.SelectedIndex].Text;

                user.EMAIL = txt_email.Value;
                user.FAX = txt_Fax.Value;
                //user.FACEFILE = "";
                user.HOMEPAGE = txt_homepage.Value;
                //user.INTEGRAL=
                //user.INTRODUCTION
                user.JOBNUMBER = txt_jobnumber.Value;
                user.JOBTITLE = txt_jobtitle.Value;
                // user.JOINANSWER=
                user.MOBILE = txt_mobile.Value;
                user.MSN = txt_msn.Value;
                user.NICKNAME = txtnickname.Text;
                user.PWD = txtPwd.Text;
                user.QQ = txt_qq.Value;
                //user.SALT
                user.SEX = Convert.ToInt32(select_Sex.Value);
                user.TELEPHONE = txt_telephone.Value;
                user.USERID = Convert.ToInt32(txtUserID.Text);// Convert.ToInt32(userid);
                user.USERNAME = txtUserName.Text;
                if (IsDepOrUser == "dep")
                {
                    Session["DepartmentId"] = depId;
                    Session["ReBack"] = "ReBack";
                }
                if (UpdateUserInfo(user))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "java", "alert('更新成功');var api = frameElement.api, W = api.opener; api.reload();api.close();", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "java", "alert('更新失败');", true);
                    return;
                }
            }
        }

        #endregion

        #region 得到用户的space
        private string getSpace(string username)
        {
            string where = "user_name='" + username + "' ";
            //Model.miniyun_users user;
            DataSet ds = new ZK.BLL.miniyun_users().GetList(where);
            string space = "0";
            if (ds.Tables[0].Rows.Count > 0)
            {
                string id = ds.Tables[0].Rows[0]["id"].ToString();
                int userid = Convert.ToInt32(id);
                string matewhere = "user_id=" + userid.ToString() + " and meta_key = '" + "space'";
                DataSet dataset = new ZK.BLL.miniyun_user_metas().GetList(matewhere);
                if (dataset.Tables[0].Rows.Count > 0)
                {
                    space = dataset.Tables[0].Rows[0]["meta_value"].ToString();
                }
            }
            return space;
        }
        #endregion

        //用户编辑
        private void UserEdit()
        {
            select_departments.Visible = false;
            trdisplayid.Visible = false;

            if (flag == "0")
            {
                this.tr_Pwd.Visible = true;
                this.tr_pwdtext.Visible = true;
                this.tr_block1.Visible = false;
                this.tr_block2.Visible = false;
                btn_AddOrEdit.Text = "添加";
                txt_space.Value = ReadDefSpace();

                BLL.USERS blluser = new BLL.USERS();
                DataSet ds = blluser.GetList("1=1 order by userid desc");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<Model.USERS> listuser = new List<Model.USERS>();
                    listuser = blluser.DataTableToList(ds.Tables[0]);

                    int userID = listuser[0].USERID + 1;
                    txtUserID.Text = userID.ToString();
                }
                else
                {
                    txtUserID.Text = "10000";
                }
            }
            else
            {
                tr_Pwd.Visible = false;
                tr1_Ok.Visible = false;
                tr_pwdtext.Visible = false;
                txtUserID.ReadOnly = true;
                txtUserName.ReadOnly = true;
                btn_AddOrEdit.Text = "修改";
                this.tr_pwdtext.Visible = false;
                //this.tr_Pwd.Visible = false;
                this.tr_block1.Visible = true;
                this.tr_block2.Visible = true;
                InitEditForm(userid);
            }
        }

        //组织用户编辑
        private void OrgAndUserEdit()
        {
            if (flag == "0")
            {
                trdisplayid.Visible = false;
                this.tr_Pwd.Visible = true;
                this.tr_pwdtext.Visible = true;
                this.tr_block1.Visible = false;
                this.tr_block2.Visible = false;
                btn_AddOrEdit.Text = "添加";
                txt_space.Value = ReadDefSpace();
                select_departments.Value = depId;

                BLL.USERS blluser = new BLL.USERS();
                DataSet ds = blluser.GetList("1=1 order by userid desc");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<Model.USERS> listuser = new List<Model.USERS>();
                    listuser = blluser.DataTableToList(ds.Tables[0]);

                    int userID = listuser[0].USERID + 1;

                    txtUserID.Text = userID.ToString();
                }
                else
                {
                    txtUserID.Text = "10000";
                }
            }
            else
            {
                tr_Pwd.Visible = false;
                tr1_Ok.Visible = false;
                tr_pwdtext.Visible = false;
                trdisplayid.Visible = false;
                select_departments.Value = depId;
                select_departments.Disabled = false;
                txtUserID.ReadOnly = true;
                txtUserName.ReadOnly = true;
                btn_AddOrEdit.Text = "修改";
                this.tr_pwdtext.Visible = false;
                //this.tr_Pwd.Visible = false;
                this.tr_block1.Visible = true;
                this.tr_block2.Visible = true;
                InitEditForm(userid);
            }
        }

        #region 修改页面的加载
        //修改页面的加载
        private void InitEditForm(string UserID)
        {
            BindOrgSlect();
            BLL.USERS blluser = new BLL.USERS();
            Model.USERS usermodel = new Model.USERS();
            usermodel = blluser.GetModel(Convert.ToInt32(UserID));
            txtUserID.Text = usermodel.USERID.ToString();
            txtUserName.Text = usermodel.USERNAME;
            //string pwd = usermodel.PWD;
            //string v1 = HttpUtility.UrlEncode(pwd, Encoding.Default);
            //string v2 = HttpUtility.UrlDecode(v1, Encoding.Default);
            //MD5 md5 = MD5.Create();
            //byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(pwd));
            //for (int i = 0; i < s.Length; i++)
            //{
            //    pwd = pwd + s[i].ToString("x");
            //}
            //txtPwd.Text = pwd;
            //txt_PwdOK.Value = pwd;
            //从M有SQL中得到space
            string meta_UserName = usermodel.USERNAME;
            //txt_space.Value = "200";
            txt_space.Value = getSpace(meta_UserName);
            //usermodel.USERTYPE = 1;

            if (usermodel.CANFINDBYPUBLICUSERS == 0)
            {
                check_Comm.Checked = false;
            }
            else
            {
                check_Comm.Checked = true;
            }
            txtnickname.Text = usermodel.NICKNAME;
            txtactualname.Text = usermodel.ACTUALNAME;
            select_Sex.Value = usermodel.SEX.ToString();
            select_Age.Value = usermodel.AGE.ToString();
            select_departments.Value = usermodel.DEPARTID.ToString();
            //虚拟数据
            //usermodel.BIRTH_YEAR = 1990;
            //usermodel.BIRTH_MONTH = 1;
            //usermodel.BIRTH_DAY = 1;
            //usermodel.COUNTRY = 1;
            //usermodel.PROVINCE = 1;
            //usermodel.CITY = 1;
            txt_address.Value = usermodel.ADDRESS;
            txt_telephone.Value = usermodel.TELEPHONE;
            txt_mobile.Value = usermodel.MOBILE;
            txt_Fax.Value = usermodel.FAX;
            txt_qq.Value = usermodel.QQ;
            txt_msn.Value = usermodel.MSN;
            txt_email.Value = usermodel.EMAIL;
            txt_homepage.Value = usermodel.HOMEPAGE;
            // select_departments.SelectedIndex = usermodel.DEPARTID;
            select_departments.Value = usermodel.DEPARTNAME;
            txt_jobtitle.Value = usermodel.JOBTITLE;
            txt_jobnumber.Value = usermodel.JOBNUMBER;
            //usermodel.INTRODUCTION = "模拟数据";
            // txt_Pwd.Value = usermodel.PWD;
            //usermodel.SALT = "模拟数据";
        }
        #endregion

        private void BindAgeSelect()
        {
            for (int i = 1; i < 120; i++)
            {
                select_Age.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

        }

        #region 从数据库中读取默认用户空间
        private string ReadDefSpace()
        {
            int option_id = 21;
            string where = "option_id=" + option_id.ToString();
            string userSpace = "";
            ZK.Model.miniyun_options options = new Model.miniyun_options();
            DataSet ds = new ZK.BLL.miniyun_options().GetList(where);
            DataRow row = ds.Tables[0].Rows[0];
            userSpace = row["option_value"].ToString();
            return userSpace;
        }

        #endregion


        //private bool AddNewUserToMiniYun(Model.USERS usermodel)
        //{


        //        string data = "UserCreateForm[email]="+usermodel.EMAIL;
        //        data += "&UserCreateForm[isAdmin]=0";
        //        data += "&UserCreateForm[name]="+usermodel.USERNAME;
        //        data += "&UserCreateForm[nick]="+usermodel.NICKNAME;
        //        data += "&UserCreateForm[passwordConfirm]="+usermodel.JOINQUESTION;
        //        data += "&UserCreateForm[password]="+usermodel.PWD;
        //        data += "&UserCreateForm[space]=" + txt_space.Value.TrimEnd();
        //        string strUrl = string.Format("http://{0}/netdisk/index.php/zk/createuser", ConfigurationManager.AppSettings["url"]);
        //        string strAddminiStatus = PostWebRequest(strUrl, data, Encoding.Default);
        //    return true;
        //}



        //private string PostWebRequest(string postUrl, string paramData, Encoding dataEncode)
        //{
        //    string ret = string.Empty;
        //    try
        //    {
        //        byte[] byteArray = dataEncode.GetBytes(paramData); //转化
        //        HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
        //        webReq.Method = "POST";
        //        webReq.ContentType = "application/x-www-form-urlencoded";

        //        webReq.ContentLength = byteArray.Length;
        //        Stream newStream = webReq.GetRequestStream();
        //        newStream.Write(byteArray, 0, byteArray.Length);//写入参数
        //        newStream.Close();
        //        HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
        //        StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.Default);
        //        ret = sr.ReadToEnd();
        //        sr.Close();
        //        response.Close();
        //        newStream.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //    return ret;
        //}
    }
}