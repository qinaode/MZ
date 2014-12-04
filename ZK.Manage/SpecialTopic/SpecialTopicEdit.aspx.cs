using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZK.Common;

namespace ZK.Manage.SpecialTopic
{
    public partial class SpecialTopicEdit : System.Web.UI.Page
    {
        ZK.BLL.ZK_FileJPType bllFileJP = new BLL.ZK_FileJPType();
        ZK.Model.ZK_FileJPType mdlFileJP = new ZK.Model.ZK_FileJPType();
        bool isOpen = false;
        string imgUrl = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PageLoad();                
            }         
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtSpecialName.Text.Trim() == string.Empty)
            {
                MessageBox.Show(this, "专题名称不能为空！");
                return;
            }

            mdlFileJP.TypeName = txtSpecialName.Text;
            mdlFileJP.TypeDesc = txtSpecialDesc.Text;           

            if (Request.QueryString["ty"] == "add")
            {
                mdlFileJP.isOpen = false;
                bllFileJP.Add(mdlFileJP);
            }

            if (Request.QueryString["ty"] == "edit")
            {
                mdlFileJP.id = Convert.ToInt32(Request.QueryString["id"]);
                mdlFileJP.isOpen = isOpen;
                mdlFileJP.imageURL = imgUrl;
                bllFileJP.Update(mdlFileJP);
            }

            Response.Write("<script>window.open('SpecialTopicMag.aspx?curp=topic&ty=NoDel', '_parent', '');var api = frameElement.api, W = api.opener; api.reload();api.close();</script>");
        }

        private void PageLoad()
        {
            if (Request.QueryString["ty"] == "edit")
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);

                if (id != 0)
                {
                    mdlFileJP = bllFileJP.GetModel(id);

                    txtSpecialName.Text = mdlFileJP.TypeName;
                    txtSpecialDesc.Text = mdlFileJP.TypeDesc;
                    isOpen = mdlFileJP.isOpen;
                    imgUrl = mdlFileJP.imageURL;
                }
            }
        }


    }
}