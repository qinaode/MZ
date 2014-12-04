using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Xml;
using System.Data;
using ZK.Common;

namespace ZK.Manage.SettingManage
{
    public partial class AdminAddOrEdit : System.Web.UI.Page
    {
        string adminname = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            string flag = Context.Request.QueryString["flag"];
            adminname = Context.Request.QueryString["adminname"];
            if (flag == "0")
            {
                this.div_Add.Visible = true;
                this.div_InitPWD.Visible = false;
                btn_AddOrEdit.Text = "添加";
            }
            else
            {
                this.div_Add.Visible = false;
                this.div_InitPWD.Visible = true;
                btn_AddOrEdit.Text = "保存";
            }
        }

        private bool ExistsAdmin(string AdminName)
        {
            bool flag = false;
           
            string where = "";
            DataSet ds = new ZK.BLL.ADMINS().GetList(where);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow row = ds.Tables[0].Rows[i];
                if (AdminName.Equals(row["ADMINNAME"].ToString()))
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }
        /// <summary>
        /// 更新用户实体
        /// </summary>
        /// <param name="usermodel"></param>
        /// <returns></returns>
        private bool AddNewAdminInfo()
        {
            string AdminName = txt_AdminName.Value;
            if (ExistsAdmin(AdminName) == false)
            {
                string pwd = ZK.Common.StringPlus.StringToMD5(this.txt_Pwd.Value.TrimEnd());
                string strRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><request>" +
                                    "<ip>" + Page.Request.UserHostAddress + "</ip>" +
                                    "<adminname>" + txt_AdminName.Value + "</adminname>" +
                                    "<adminpwd>" + pwd + "</adminpwd>" +
                                    "<description>" + txt_description.Value.TrimEnd() + "</description>" +
                                    "</request> ";
                string strResponse = "";
                bool boolIS = new OpenCom.Command().Execute("Admin.AddAdmin", strRequest, ref strResponse, 5000);
                return boolIS;
            }
            else
            {
                MessageBox.Show(this, "已存在相同ADMIN用户！");
                return false;
            }

        }
        /// <summary>
        /// 添加或保存点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_AddOrEdit_Click(object sender, EventArgs e)
        {
            //string AdminName = txt_AdminName.Value;

            if (btn_AddOrEdit.Text == "添加")
            {
                if (txt_AdminName.Value.TrimEnd() == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "java", "alert('请输入管理员账号');", true);
                    return;
                }
                if (txt_Pwd.Value.TrimEnd() != txt_Pwd2.Value.TrimEnd())
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "java", "alert('两次密码输入不一致');", true);
                    return;
                }
                if (AddNewAdminInfo())
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "java", "alert('添加成功');var api = frameElement.api, W = api.opener; api.reload();api.close();", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "java", "alert('添加失败');var api = frameElement.api, W = api.opener; api.reload();api.close();", true);
                }
            }
            else
            {
                if (txt_NewPwd.Value.TrimEnd() != txt_NewPwd2.Value.TrimEnd())
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "java", "alert('两次新密码输入不一致');", true);
                    return;
                }
               bool b= InitPWD(adminname, txt_OldPwd.Value.TrimEnd(), txt_NewPwd.Value.TrimEnd());
                if (b)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "java", "alert('修改成功');var api = frameElement.api, W = api.opener; api.reload();api.close();", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "java", "alert('修改失败');var api = frameElement.api, W = api.opener; api.reload();api.close();", true);
                }
            }
        }
        /// <summary>
        /// 初始化密码
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        private bool InitPWD(string adminname, string oldpwd, string newpwd)
        {
            oldpwd = ZK.Common.StringPlus.StringToMD5(oldpwd);
            newpwd = ZK.Common.StringPlus.StringToMD5(newpwd);
            //ResetUserPWD
            string strRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><request>" +
                                "<ip>" + Page.Request.UserHostAddress + "</ip>" +
                                "<adminname>" + adminname + "</adminname>" +
                                "<oldpwd>" + oldpwd + "</oldpwd>" +
                                "<newpwd>" + newpwd + "</newpwd>" +
                                "</request>";
            string strResponse = "";
            bool boolIS = new OpenCom.Command().Execute("Admin.ChangeAdminPWD", strRequest, ref strResponse, 5000);
            StringReader stream = null;
            XmlTextReader reader = null;
            DataSet dsResponse = new DataSet();

            stream = new StringReader(strResponse);
            //从stream装载到XmlTextReader
            reader = new XmlTextReader(stream);
            dsResponse.ReadXml(reader);
            return boolIS;
        }

    }
}