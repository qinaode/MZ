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
    public partial class GroupManager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGroupData();
                lblShowTxt.Visible = false;
                txtType.Visible = false;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string txtstr = txtType.Text;

            if (ddlGroups.SelectedIndex == 0)
            {
                BindGroupData();
            }

            if (lblShowTxt.Text == "群组ID:")
            {
                BindGroupData(txtstr, "Id");
            }
            else
            {
                BindGroupData(txtstr, "Name");
            }   
            
        }

        protected void GroupListItem_Commond(object sender, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "CN_btn_Delete")
            {
                string id = e.CommandArgument.ToString();
                Delect(id);
            }
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            string txtstr = txtType.Text;

            if (ddlGroups.SelectedIndex == 0)
            {
                BindGroupData();
            }

            if (txtstr != "" && txtstr != null)
            {
                if (lblShowTxt.Text == "群组ID:")
                {
                    BindGroupData(txtstr, "Id");
                }
                else
                {
                    BindGroupData(txtstr, "Name");
                }
            }
        }

        private void Delect(string id)
        {
            string strRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><request>" +
               "<ip>" + Page.Request.UserHostAddress + "</ip>" +
               "<groupid>" + id + "</groupid>" +
               "</request> ";
            string strResponse = "";
            bool boolIS = new OpenCom.Command().Execute("Admin.RemoveGroup", strRequest, ref strResponse, 5000);
            //xml to dataset
            StringReader stream = null;
            XmlTextReader reader = null;
            DataSet dsResponse = new DataSet();

            stream = new StringReader(strResponse);
            //从stream装载到XmlTextReader
            reader = new XmlTextReader(stream);
            dsResponse.ReadXml(reader);

            BindGroupData();
        }

        private void BindGroupData()
        {                   
            int ownerType = -1;
            int searchType = 0;
            int groupId=0;
            string groupName="";
           
            string strRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><request>" +
                                         "<ip>" + Page.Request.UserHostAddress + "</ip>" +
                                            "<ownerusertype>" + ownerType + "</ownerusertype>" +
                                             "<searchtype>" + searchType + "</searchtype>" +
                                              "<param_groupid>" + groupId + "</param_groupid>" +
                                               "<param_groupname>" + groupName + "</param_groupname>" +
                                                 "<pagesize>" + this.AspNetPager1.PageSize + "</pagesize>" +
                               "<pageindex>" + this.AspNetPager1.CurrentPageIndex + "</pageindex>" +
                                            "</request> ";
            string strResponse = "";
            bool boolIS = new OpenCom.Command().Execute("Admin.SearchGroups", strRequest, ref strResponse, 5000);
            //xml to dataset
            StringReader stream = null;
            XmlTextReader reader = null;
            DataSet dsResponse = new DataSet();

            stream = new StringReader(strResponse);
            //从stream装载到XmlTextReader
            reader = new XmlTextReader(stream);
            dsResponse.ReadXml(reader);   

            rptGroupList.DataSource = dsResponse.Tables["group"]; 
            rptGroupList.DataBind();
            if (dsResponse.Tables["item"] != null)
            {
                this.AspNetPager1.RecordCount = int.Parse(dsResponse.Tables["response"].Rows[0]["allusercount"].ToString());
            }
        }

        private void BindGroupData(string txt,string flag)
        {
            int ownerType = -1;
            int searchType = 0;
             int groupId=0;
            if (flag == "Id")
            {
                searchType = 1;
                groupId = Convert.ToInt32(txt);
            }                          
            string groupName = "";
            if (flag == "Name")
            {
                searchType = 2;
                groupName = txt;
            }

            string strRequest = "<?xml version=\"1.0\" encoding=\"utf-8\"?><request>" +
                                         "<ip>" + Page.Request.UserHostAddress + "</ip>" +
                                            "<ownerusertype>" + ownerType + "</ownerusertype>" +
                                             "<searchtype>" + searchType + "</searchtype>" +
                                              "<param_groupid>" + groupId + "</param_groupid>" +
                                               "<param_groupname>" + groupName + "</param_groupname>" +
                                                "<pagesize>" + this.AspNetPager1.PageSize + "</pagesize>" +
                               "<pageindex>" + this.AspNetPager1.CurrentPageIndex + "</pageindex>" +
                                            "</request> ";
            string strResponse = "";
            bool boolIS = new OpenCom.Command().Execute("Admin.SearchGroups", strRequest, ref strResponse, 5000);
            //xml to dataset
            StringReader stream = null;
            XmlTextReader reader = null;
            DataSet dsResponse = new DataSet();

            stream = new StringReader(strResponse);
            //从stream装载到XmlTextReader
            reader = new XmlTextReader(stream);
            dsResponse.ReadXml(reader);

            rptGroupList.DataSource = dsResponse.Tables["group"];
            rptGroupList.DataBind();

            if (dsResponse.Tables["item"] != null)
            {
                this.AspNetPager1.RecordCount = int.Parse(dsResponse.Tables["response"].Rows[0]["allusercount"].ToString());
            }
        }

        public string BindSeting(Object obj)
        {
            string str = "";
            int setIndex = Convert.ToInt32(obj);
            if (setIndex == 1)  
            { str = "需要验证"; }

            else if (setIndex == 2)
            { str = "不允许加入"; }

            else  
            { str = "允许任何人"; }

            return str;
        }

        protected void ddlGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGroups.SelectedIndex == 0)
            {                      
                lblShowTxt.Visible = false;
                txtType.Visible = false;
                BindGroupData();
            }
            if (ddlGroups.SelectedIndex == 1)
            {
                lblShowTxt.Visible = true;
                txtType.Visible = true;
                lblShowTxt.Text = "群组ID:";
                txtType.Text = "";
            }

            if (ddlGroups.SelectedIndex == 2)
            {
                lblShowTxt.Visible = true;
                txtType.Visible = true;
                lblShowTxt.Text = "群组名称:";
                txtType.Text = "";
            }
        }

        #region 注释

        //DataSet ds = new DataSet();

        //ZK.BLL.GROUPS bllGroup = new BLL.GROUPS();
        //ZK.BLL.GROUPMEMBERS bllg = new BLL.GROUPMEMBERS();

        //ds = bllGroup.GetAllList();
        //List<Model.GROUPS> lists = bllGroup.DataTableToList(dt);
        //foreach (var item in lists)
        //{
        //    List<ZK.Model.GROUPMEMBERS> model = new List<Model.GROUPMEMBERS>();
        //    model.AddRange(bllg.GetModelList("groupid=" + item.GROUPID + ""));
        //    item.NOTICE = model.Count;
        //}
        #endregion
    }
}