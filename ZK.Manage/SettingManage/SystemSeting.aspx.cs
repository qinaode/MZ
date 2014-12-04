using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZK.Common;
using System.Web.Script;
using System.IO;
using System.Data;
namespace ZK.Manage.SettingManage
{
    public partial class SystemSeting : System.Web.UI.Page
    {
        static string XMLFilePath = "";//xml路径E:\智慧教育系统\trunk\ZK.Manage\js\SystemSetting.xml
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                XMLFilePath = Request.PhysicalApplicationPath + ZK.Common.ModelSettings.BH_SysSettingXMLPath;
                InitPage();
            }

        }

        /// <summary>
        /// 初始化页面
        /// </summary>
        private void InitPage()
        {
            this.txt_webtitle.Value = XMLHelper.GetXmlAttribute(XMLFilePath, "Settings/WebTitle", "value").Value.ToString();
            //版权多行文本框
             //this.txt_copyright.Value = XMLHelper.GetXmlAttribute(XMLFilePath, "Settings/CopyRight", "value").Value.ToString();
            this.text_copyright.Value = XMLHelper.GetXmlAttribute(XMLFilePath, "Settings/CopyRight", "value").Value.ToString();
            
            //从数据库中得到用户空间配额，默认的
            this.txt_defaultspace.Value = ReadDefSpace();

            this.txt_recordnum.Value = XMLHelper.GetXmlAttribute(XMLFilePath, "Settings/RecordNum", "value").Value.ToString();
            //this.txt_weblogo.Value = XMLHelper.GetXmlAttribute(XMLFilePath, "Settings/Logo", "value").Value.ToString();
            this.img_Logo.ImageUrl = XMLHelper.GetXmlAttribute(XMLFilePath, "Settings/Logo", "value").Value.ToString();
            if (XMLHelper.GetXmlAttribute(XMLFilePath, "Settings/IsConvert", "value").Value.ToString() == "Yes")
            {
                this.rbtn_Open.Checked = true;
            }
            else
            {
                this.rbtn_Close.Checked = true;
            }
            this.txt_teachnum.Value = XMLHelper.GetXmlAttribute(XMLFilePath, "Settings/PageCount/Teach", "value").Value.ToString();
            this.txt_Moralnum.Value = XMLHelper.GetXmlAttribute(XMLFilePath, "Settings/PageCount/Moral", "value").Value.ToString();
            this.txt_administrationnum.Value = XMLHelper.GetXmlAttribute(XMLFilePath, "Settings/PageCount/Admin", "value").Value.ToString();
            this.txt_searchnum.Value = XMLHelper.GetXmlAttribute(XMLFilePath, "Settings/PageCount/Search", "value").Value.ToString();

            //this.txt_defaultspace.Value = XMLHelper.GetXmlAttribute(XMLFilePath, "Settings/UserSpace", "value").Value.ToString();
            this.txt_filestorepath.Value = XMLHelper.GetXmlAttribute(XMLFilePath, "Settings/FilePath", "value").Value.ToString();
            this.txt_time_format.Value = XMLHelper.GetXmlAttribute(XMLFilePath, "Settings/TimeFormate", "value").Value.ToString();
            this.txt_Doc.Value = XMLHelper.GetXmlAttribute(XMLFilePath, "Settings/DocType", "value").Value.ToString();
            this.txt_Video.Value = XMLHelper.GetXmlAttribute(XMLFilePath, "Settings/VideoType", "value").Value.ToString();
            this.txt_Photo.Value = XMLHelper.GetXmlAttribute(XMLFilePath, "Settings/PhotoType", "value").Value.ToString();
        }
        /// <summary>
        /// 提交 修改的xml内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            string space = this.txt_defaultspace.Value;
            XMLHelper.CreateOrUpdateXmlAttributeByXPath(XMLFilePath, "Settings/WebTitle", "value", this.txt_webtitle.Value);
            //XMLHelper.CreateOrUpdateXmlAttributeByXPath(XMLFilePath, "Settings/CopyRight", "value", this.txt_copyright.Value);
            //XMLHelper.CreateOrUpdateXmlAttributeByXPath(XMLFilePath, "Settings/RecordNum", "value", this.txt_copyright.Value);


            XMLHelper.CreateOrUpdateXmlAttributeByXPath(XMLFilePath, "Settings/CopyRight", "value", this.text_copyright.Value);
            XMLHelper.CreateOrUpdateXmlAttributeByXPath(XMLFilePath, "Settings/RecordNum", "value", this.txt_recordnum.Value);

            //logo
            if (this.txt_weblogo.Value != "")
            {
                this.txt_weblogo.PostedFile.SaveAs(Server.MapPath(ZK.Common.ModelSettings.BH_LogoPath));
            }
            XMLHelper.CreateOrUpdateXmlAttributeByXPath(XMLFilePath, "Settings/Logo", "value", ZK.Common.ModelSettings.BH_LogoPath);

            if (this.rbtn_Open.Checked)
            {
                XMLHelper.CreateOrUpdateXmlAttributeByXPath(XMLFilePath, "Settings/IsConvert", "value", "Yes");
            }
            else
            {
                XMLHelper.CreateOrUpdateXmlAttributeByXPath(XMLFilePath, "Settings/IsConvert", "value", "No");
            }
            XMLHelper.CreateOrUpdateXmlAttributeByXPath(XMLFilePath, "Settings/PageCount/Teach", "value", this.txt_teachnum.Value);
            XMLHelper.CreateOrUpdateXmlAttributeByXPath(XMLFilePath, "Settings/PageCount/Moral", "value", this.txt_Moralnum.Value);
            XMLHelper.CreateOrUpdateXmlAttributeByXPath(XMLFilePath, "Settings/PageCount/Admin", "value", this.txt_administrationnum.Value);
            XMLHelper.CreateOrUpdateXmlAttributeByXPath(XMLFilePath, "Settings/PageCount/Search", "value", this.txt_searchnum.Value);
            //提交时候修改数据库中用户默认配额
            //XMLHelper.CreateOrUpdateXmlAttributeByXPath(XMLFilePath, "Settings/UserSpace", "value", this.txt_defaultspace.Value);
             UpUserDefSpace(space);

            XMLHelper.CreateOrUpdateXmlAttributeByXPath(XMLFilePath, "Settings/FilePath", "value", this.txt_filestorepath.Value);
            XMLHelper.CreateOrUpdateXmlAttributeByXPath(XMLFilePath, "Settings/TimeFormate", "value", this.txt_time_format.Value);
            XMLHelper.CreateOrUpdateXmlAttributeByXPath(XMLFilePath, "Settings/DocType", "value", this.txt_Doc.Value);
            XMLHelper.CreateOrUpdateXmlAttributeByXPath(XMLFilePath, "Settings/VideoType", "value", this.txt_Video.Value);
            XMLHelper.CreateOrUpdateXmlAttributeByXPath(XMLFilePath, "Settings/PhotoType", "value", this.txt_Photo.Value);
            //复制xml文件和logo  操作取消

            ClientScript.RegisterStartupScript(this.GetType(), "java", "alert('修改成功');", true);
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

        private void UpUserDefSpace(string space)
        {
            int option_id = 21;
            string where = "option_id=" + option_id.ToString();
            ZK.Model.miniyun_options options = new Model.miniyun_options();
            options = new ZK.BLL.miniyun_options().GetModel(option_id);
            options.option_value = space;
            new ZK.BLL.miniyun_options().Update(options);
        }


    }
}