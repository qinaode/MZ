using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZK.Manage.MoralManagement
{
    public partial class MoralResourceSendEdit : System.Web.UI.Page
    {
        ZK.BLL.ZK_FileJP bllFileJP = new BLL.ZK_FileJP();
        ZK.Model.ZK_FileJP mdlFileJP = new Model.ZK_FileJP();

        ZK.BLL.ZK_FileList bllFileList = new BLL.ZK_FileList();
        ZK.Model.ZK_FileList mdlFileList = new ZK.Model.ZK_FileList();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PageLoad();
            }  
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            mdlFileJP.typeID = Convert.ToInt32(cmbSpecialTopic.Value);
            mdlFileJP.fileID = Convert.ToInt32(Request.QueryString["tyid"]);

            mdlFileList = bllFileList.GetModel(Convert.ToInt32(Request.QueryString["tyid"]));
            if (txtSpecialName.Text.Trim() == string.Empty)
            {
                mdlFileJP.fileName = mdlFileList.fileName;
            }
            else
            {
                mdlFileJP.fileName = txtSpecialName.Text;
            }
            mdlFileJP.fileType = mdlFileList.fileTypeID;
            mdlFileJP.imageURL = mdlFileList.imageURL;

            DataSet ds = bllFileJP.GetAllList();
            List<ZK.Model.ZK_FileJP> list = new List<Model.ZK_FileJP>();
            list = bllFileJP.DataTableToList(ds.Tables[0]);
            int level = 0;
            if (list.Count != 0)
            {
                level = Convert.ToInt32(list[0].sortNum);
                for (int i = 1; i < list.Count; i++)
                {
                    if (level < list[i].sortNum)
                    {
                        level = Convert.ToInt32(list[i].sortNum);
                    }
                }
            }

            mdlFileJP.sortNum = level+1;

            bllFileJP.Add(mdlFileJP);

            Response.Write("<script>window.open('MoralManagementCategory.aspx?curp=moral&ty=NoDel', '_parent', '');var api = frameElement.api, W = api.opener; api.reload();api.close();</script>");
        }

        private void PageLoad()
        {
            int id = Convert.ToInt32(Request.QueryString["tyid"]);

            if (id != 0)
            {
                mdlFileList = bllFileList.GetModel(id);

                lblName.Text = mdlFileList.fileName;
            }

            BindTopic();
        }

        private void BindTopic()
        {
            DataTable dt = new DataTable();
            ZK.BLL.ZK_FileJPType bllFileType = new BLL.ZK_FileJPType();

            System.Data.DataSet ds = bllFileType.GetAllList();

            dt = ds.Tables[0];

            cmbSpecialTopic.DataSource = dt;
            cmbSpecialTopic.DataValueField = "id";
            cmbSpecialTopic.DataTextField = "TypeName";
            cmbSpecialTopic.DataBind();

            cmbSpecialTopic.SelectedIndex = -1;
        }
    }
}