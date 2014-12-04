using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data;
using System.IO;
using ZK.Common;

namespace ZK.Manage.BasicInfo
{
    public partial class OrgManager : System.Web.UI.Page
    {
        ZK.BLL.DEPARTMENTS bll = new BLL.DEPARTMENTS();
        ZK.Model.DEPARTMENTS mdl = new Model.DEPARTMENTS();

        protected void Page_Load(object sender, EventArgs e)
        {
            DepartmentBind();
               
            if (Server.HtmlEncode(Request.QueryString["ty"]) == "del")
            {
                string id = Server.HtmlEncode(Request.QueryString["id"]);
                Delect(id);
            }

            if (Server.HtmlEncode(Request.QueryString["ty"]) == "Down")
            {
                string id = Server.HtmlEncode(Request.QueryString["id"]);
                Move(id, "Down");
            }

            if (Server.HtmlEncode(Request.QueryString["ty"]) == "Up")
            {
                string id = Server.HtmlEncode(Request.QueryString["id"]);
                Move(id,"Up");
            }

        }


        #region 前台事件
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            System.Data.DataSet ds = bll.GetAllList();

            pCategoryList.DataSource = ds;
            pCategoryList.DataBind();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            DepartmentBind();
        }      
        #endregion

        #region 绑定事件
        private void DepartmentBind()
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

            pCategoryList.DataSource = dsResponse.Tables["department"];
            pCategoryList.DataBind();
            litCount.Text = dsResponse.Tables["department"].Rows.Count.ToString();
        }

        private void Delect(string id)
        {
            DataSet ds = bll.GetList("PARENTDEPARTID="+id);
            if (ds.Tables[0].Rows.Count > 0)
            {
                MessageBox.Show(this, "请先删除该部门下的子部门！");
                return;
            }
            string strRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><request>" +
                    "<ip>" + Page.Request.UserHostAddress + "</ip>" +
                    "<departid>"+id+"</departid>" +                     
                    "</request> ";
            string strResponse = "";
            bool boolIS = new OpenCom.Command().Execute("Admin.RemoveDepartment", strRequest, ref strResponse, 5000);
            //xml to dataset
            StringReader stream = null;
            XmlTextReader reader = null;
            DataSet dsResponse = new DataSet();

            stream = new StringReader(strResponse);
            //从stream装载到XmlTextReader
            reader = new XmlTextReader(stream);
            dsResponse.ReadXml(reader);

            DepartmentBind();
        }

        private void Move(string id, string flag)
        {
            string strRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><request>" +
                    "<ip>" + Page.Request.UserHostAddress + "</ip>" +
                    "<departid>" + id + "</departid>" +
                    "</request> ";
            string strResponse = "";
            if (flag == "Down")
            {
                bool boolIS = new OpenCom.Command().Execute("Admin.MoveDownDepartment", strRequest, ref strResponse, 5000);
            }
            if (flag == "Up")
            {
                bool boolIS = new OpenCom.Command().Execute("Admin.MoveUpDepartment", strRequest, ref strResponse, 5000);
            }
            //xml to dataset
            StringReader stream = null;
            XmlTextReader reader = null;
            DataSet dsResponse = new DataSet();

            stream = new StringReader(strResponse);
            //从stream装载到XmlTextReader
            reader = new XmlTextReader(stream);
            dsResponse.ReadXml(reader);

            DepartmentBind();
        }

        #endregion
    }
}