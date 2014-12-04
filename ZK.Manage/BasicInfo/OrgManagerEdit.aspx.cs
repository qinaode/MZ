using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using ZK.Common;
using System.Xml;

namespace ZK.Manage.BasicInfo
{
    public partial class OrgManagerEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDepartment();
                LoadPage();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string departmentName = txtDepartment.Text;
            string parentDepartment = cmbMoralCategory.Value.ToString();

            if (departmentName == string.Empty)
            {
                MessageBox.Show(this, "部门名称不能为空！");
                return;
            }
             
            string strResponse = "";

            if (Server.HtmlEncode(Request.QueryString["ty"]) == "add" || Server.HtmlEncode(Request.QueryString["ty"]) == "addchild")
            {
                if (ExistDepart(departmentName, parentDepartment)==false)
                {
                    string strRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><request>" +
                          "<ip>" + Page.Request.UserHostAddress + "</ip>" +
                          "<departname>" + departmentName + "</departname>" +
                          "<parentdepartid>" + parentDepartment + "</parentdepartid>" +
                          "</request> ";

                    bool boolIS = new OpenCom.Command().Execute("Admin.AddDepartment", strRequest, ref strResponse, 5000);
                    //xml to dataset
                    StringReader stream = null;
                    XmlTextReader reader = null;
                    DataSet dsResponse = new DataSet();

                    stream = new StringReader(strResponse);
                    //从stream装载到XmlTextReader
                    reader = new XmlTextReader(stream);
                    dsResponse.ReadXml(reader);
                    Response.Write("<script> window.open('OrgManager.aspx', '_parent', '');var api = frameElement.api, W = api.opener; api.reload();api.close();</script>");
                }
                else
                {
                    MessageBox.Show(this, "已存在该部门！");
                 
                    return;
                }
            }
            if (Server.HtmlEncode(Request.QueryString["ty"]) == "edit")
            {
                string id = Server.HtmlEncode(Request.QueryString["id"]);
                Model.DEPARTMENTS depart = new ZK.BLL.DEPARTMENTS().GetModel(Convert.ToInt32(id));
                    //if (!ExistDepart(departmentName, parentDepartment))//所属部门不存在该部门名称
                    {
                        string strRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><request>" +
                     "<ip>" + Page.Request.UserHostAddress + "</ip>" +
                     "<departid>" + id + "</departid>" +
                     "<departname>" + departmentName + "</departname>" +
                     "</request> ";

                        bool boolIS = new OpenCom.Command().Execute("Admin.ModifyDepartment", strRequest, ref strResponse, 5000);
                        //xml to dataset
                        StringReader stream = null;
                        XmlTextReader reader = null;
                        DataSet dsResponse = new DataSet();

                        stream = new StringReader(strResponse);
                        //从stream装载到XmlTextReader
                        reader = new XmlTextReader(stream);
                        dsResponse.ReadXml(reader);
                        Response.Write("<script> window.open('OrgManager.aspx', '_parent', '');var api = frameElement.api, W = api.opener; api.reload();api.close();</script>");
                    }
                    //else 
                    //{
                    //   MessageBox.Show(this, "已存在该部门！");                     
                    //   return;
                    //}
                           
            }

         
            //Response.Write("<script>window.open('OrgManager.aspx', '_parent', '');var api = frameElement.api, W = api.opener; api.reload();api.close();</script>");
        }

        private void LoadPage()
        {
            string id = Server.HtmlEncode(Request.QueryString["id"]);            

            if (Server.HtmlEncode(Request.QueryString["ty"]) == "addchild")
            {
                cmbMoralCategory.Value = id;
            }

            if (Server.HtmlEncode(Request.QueryString["ty"]) == "edit")
            {
                ZK.BLL.DEPARTMENTS bll = new BLL.DEPARTMENTS();
                ZK.Model.DEPARTMENTS mdl = new Model.DEPARTMENTS();
                mdl = bll.GetModel(Convert.ToInt32(id));

                DataSet ds = bll.GetList("DEPARTID="+Convert.ToInt32(id)+" and PARENTDEPARTID=0");
                if (ds.Tables[0].Rows.Count>0)
                {
                    txtDepartment.Text = mdl.DEPARTNAME;
                    divDisplay.Visible = false;
                }
                else
                {
                    txtDepartment.Text = mdl.DEPARTNAME;
                    cmbMoralCategory.Value = mdl.PARENTDEPARTID.ToString();
                }
            }
        }

        private void BindDepartment()
        {
            DataTable dt = new DataTable();
            ZK.BLL.DEPARTMENTS bllDepartment = new BLL.DEPARTMENTS();

            System.Data.DataSet ds = bllDepartment.GetAllList();

            dt = ds.Tables[0];

            cmbMoralCategory.DataSource = dt;
            cmbMoralCategory.DataValueField = "DEPARTID";
            cmbMoralCategory.DataTextField = "DEPARTNAME";
            cmbMoralCategory.DataBind();
        }
        #region 得到具有相同父ID的部门名，其中key=部门名，value=部门ID
        private Dictionary<string, string> seachDepartment(string departmentName, string parentDepartment)
        {
            string strRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><request>" +
                    "<ip>" + Page.Request.UserHostAddress + "</ip>" +
                    "<parentdepartid>-1</parentdepartid>" +
                    "<showdepartusers>0</showdepartusers>" +
                    "</request> ";
            string strResponse = "";
            bool boolIS = new OpenCom.Command().Execute("Admin.GetDepartments", strRequest, ref strResponse, 5000);
            //xml to dataset
            StringReader stream = null;
            XmlTextReader reader = null;
            DataSet dsResponse = new DataSet();

            stream = new StringReader(strResponse);
            //从stream装载到XmlTextReader
            reader = new XmlTextReader(stream);
            dsResponse.ReadXml(reader);

            string depID = "";

            Dictionary<string, string> dic = new Dictionary<string, string>();
            for (int i = 0; i < dsResponse.Tables[0].Rows.Count; i++)
            {
                DataRow row = dsResponse.Tables[0].Rows[i];

                if (parentDepartment.Equals(row["DEPARTNAME"]))
                {
                    depID = row["DEPARTID"].ToString();
                    break;
                }
            }
            for (int i = 0; i < dsResponse.Tables[0].Rows.Count; i++)
            {
                DataRow row = dsResponse.Tables[0].Rows[i];
                if (depID.Equals(row["[PARENTDEPARTID]"].ToString()))
                {
                    string depName = row["DEPARTNAME"].ToString();
                    dic.Add(depName, depID);

                }
            }
            return dic;
        }
        
        #endregion

        #region 是否存在相同的额部门
        private bool ExistDepart(string departname, string parentDepartment)
        {
            bool flag = false;
            string where = "PARENTDEPARTID=" + parentDepartment;
            DataSet ds = new ZK.BLL.DEPARTMENTS().GetList(where);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow row = ds.Tables[0].Rows[i];
                if (departname.Equals(row["DEPARTNAME"]))
                {
                    flag = true;
                    break;

                }
            }
            return flag;

        }
        #endregion
       
    }
}