using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZK.Common;
using System.Data;

namespace ZK.Manage
{
    public partial class Default : System.Web.UI.Page
    {
        static string XMLFilePath = "";//xml路径E:\智慧教育系统\trunk\ZK.Manage\js\SystemSetting.xml//现在所在路径为ZK.Manage\Files\XML\
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                XMLFilePath = Request.PhysicalApplicationPath + ZK.Common.ModelSettings.BH_SysSettingXMLPath;
                InitPage();
            }

        }
        private void InitPage()
        {
            this.txt_webtitle.InnerText = XMLHelper.GetXmlAttribute(XMLFilePath, "Settings/WebTitle", "value").Value.ToString();
            this.txt_copyright.InnerText = XMLHelper.GetXmlAttribute(XMLFilePath, "Settings/CopyRight", "value").Value.ToString();
            this.txt_recordnum.InnerText = XMLHelper.GetXmlAttribute(XMLFilePath, "Settings/RecordNum", "value").Value.ToString();
            this.img_Logo.ImageUrl = XMLHelper.GetXmlAttribute(XMLFilePath, "Settings/Logo", "value").Value.ToString();
            if (XMLHelper.GetXmlAttribute(XMLFilePath, "Settings/IsConvert", "value").Value.ToString() == "Yes")
            {
                this.rbtn_Open.Checked = true;
            }
            else
            {
                this.rbtn_Close.Checked = true;
            }
            this.txt_teachnum.InnerText = XMLHelper.GetXmlAttribute(XMLFilePath, "Settings/PageCount/Teach", "value").Value.ToString();
            this.txt_Moralnum.InnerText = XMLHelper.GetXmlAttribute(XMLFilePath, "Settings/PageCount/Moral", "value").Value.ToString();
            this.txt_administrationnum.InnerText = XMLHelper.GetXmlAttribute(XMLFilePath, "Settings/PageCount/Admin", "value").Value.ToString();
            this.txt_searchnum.InnerText = XMLHelper.GetXmlAttribute(XMLFilePath, "Settings/PageCount/Search", "value").Value.ToString();
            //网盘默认配额
            //this.txt_defaultspace.InnerText = XMLHelper.GetXmlAttribute(XMLFilePath, "Settings/UserSpace", "value").Value.ToString();
            txt_defaultspace.InnerText = ReadDefSpace();

            
            this.txt_filestorepath.InnerText = XMLHelper.GetXmlAttribute(XMLFilePath, "Settings/FilePath", "value").Value.ToString();
            this.txt_time_format.InnerText = XMLHelper.GetXmlAttribute(XMLFilePath, "Settings/TimeFormate", "value").Value.ToString();
            this.txt_Doc.InnerText = XMLHelper.GetXmlAttribute(XMLFilePath, "Settings/DocType", "value").Value.ToString();
            this.txt_Video.InnerText = XMLHelper.GetXmlAttribute(XMLFilePath, "Settings/VideoType", "value").Value.ToString();
            this.txt_Photo.InnerText = XMLHelper.GetXmlAttribute(XMLFilePath, "Settings/PhotoType", "value").Value.ToString();
        }
        #region 默认网盘配额
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
       
    }
}