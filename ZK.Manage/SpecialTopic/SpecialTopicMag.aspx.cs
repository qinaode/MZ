using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZK.Common;

namespace ZK.Manage.SpecialTopic
{
    public partial class SpecialTopicMag : System.Web.UI.Page
    {
        ZK.BLL.ZK_FileJPType bllFileJP = new BLL.ZK_FileJPType();
        ZK.Model.ZK_FileJPType mdlFileJP = new Model.ZK_FileJPType();

        ZK.BLL.ZK_FileJP bllRfile = new BLL.ZK_FileJP();
        ZK.Model.ZK_FileJP mdlRfile = new Model.ZK_FileJP();
        ZK.BLL.ZK_FileJPPic bllFileJPImg = new BLL.ZK_FileJPPic();
        ZK.Model.ZK_FileJPPic mdlFileJPImg = new Model.ZK_FileJPPic();

        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"];
            string IsLock = Request.QueryString["IsLock"];
            string Res = Request.QueryString["Res"];
            string Img = Request.QueryString["Img"];
            string strDelList = Request.QueryString["checkedlist"];

            if (strDelList != null && strDelList!="")
            {
                DeleteList();
            }
            if (IsLock == "lock")
            {
                Locking(id);
            }
            if (IsLock == "unlock")
            {
                UnLocking(id);
            }
                                    
            if (Img == "Img")
            {
                Response.Redirect("/SpecialTopic/SpecialTopicImgMag.aspx?curp=topic&ty=imgMag&specialId=" + id);
            }
            if (Res == "Res")
            {
                Response.Redirect("/SpecialTopic/SpecialTopicResourcesMag.aspx?curp=topic&ty=Resources&specialId=" + id);
            }
        }

        #region 事件
        /// <summary>
        /// 点击换页事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //int PageIndex = 1;
            //int PageSize = 10;
            //string strWhere = txtSpecialTypeName.Text;

            //if (strWhere.Trim() != string.Empty)
            //{
            //    strWhere = " TypeName like '%" + strWhere + "%'";
            //}
            //else
            //    strWhere = "";

            //DataSet ds = new DataSet();
            //ds = bllFileJP.GetListByPage(strWhere, "", PageIndex, PageSize);

            ////获取总数
            //int totalNumber = bllFileJP.GetRecordCount(strWhere);

            ////rptSpecialtopicList.DataSource = ds;
            ////rptSpecialtopicList.DataBind();
        }

        /// <summary>
        /// 操作系列按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UserListItem_Commond(object sender, RepeaterCommandEventArgs e)
        {
            string specialTypeId = e.CommandArgument.ToString();
            if (e.CommandName == "CN_btn_Delete")
            {
                DeleteSpecialType(specialTypeId);
            }
            if (e.CommandName == "CN_btn_Lock")
            {
                Locking(specialTypeId);
            }
            if (e.CommandName == "CN_btn_UnLock")
            {
                UnLocking(specialTypeId);
            }
            if (e.CommandName == "CN_btn_Img")
            {
                Response.Redirect("/SpecialTopic/SpecialTopicImgMag.aspx?curp=topic&ty=imgMag&specialId=" + specialTypeId);
            }
            if (e.CommandName == "CN_btn_Resources")
            {
                Response.Redirect("/SpecialTopic/SpecialTopicResourcesMag.aspx?curp=topic&ty=Resources&specialId=" + specialTypeId);
            }
        }

        #endregion

        #region 方法
        private void BindSpecialTopicList()
        {

            int PageIndex = 1;
            int PageSize = 10;
            string strWhere = "";

            //int startIndex=1;
            //int endindex = startindex + PageSize;  

            DataSet ds = new DataSet();
            ds = bllFileJP.GetListByPage(strWhere, "", PageIndex, PageSize);

            //获取总数
            int totalNumber = bllFileJP.GetRecordCount(strWhere);

            //rptSpecialtopicList.DataSource = ds;
            //rptSpecialtopicList.DataBind();
        }

        private void DeleteSpecialType(string id)
        {
            mdlFileJP = bllFileJP.GetModel(Convert.ToInt32(id));
            if (mdlFileJP.isOpen != true)
            {
                string strRfile = " typeID in(select id from ZK_FileJPType where id=" + Convert.ToInt32(id) + ")";
                string strPic = "fileJPTypeID in(select id from ZK_FileJPType where id=" + Convert.ToInt32(id) + ")";
                DataSet dsR = bllRfile.GetList(strRfile);
                DataSet dsPic = bllFileJPImg.GetList(strPic);
                if (dsR.Tables[0].Rows.Count > 0 || dsPic.Tables[0].Rows.Count > 0)
                {
                    MessageBox.Show(this, "请先删除该专题下的资源和图片！");
                }
                else
                {
                    bool msgError = bllFileJP.Delete(Convert.ToInt32(id));
                    if (msgError == true)
                        MessageBox.Show(this, "删除成功");
                    else
                        MessageBox.Show(this, "删除失败");
                }
            }
            else
            {
                MessageBox.Show(this, "请先改为锁定状态，再删除！");
            }

            BindSpecialTopicList();
        }

        private void UnLocking(string id)
        {
            //mdlFileJP = bllFileJP.GetModel(Convert.ToInt32(id));
            //mdlFileJP.isOpen = false;

            //MessageBox.Show(this.Page, "状态锁定成功！");

            //bllFileJP.Update(mdlFileJP);
            MessageBox.Show(this.Page, "专题至少需有一个处于启用状态，若要锁定当前状态，请先启用其他专题！");
            BindSpecialTopicList();
        }

        private void Locking(string id)
        {
            DataSet ds = new DataSet();
            ds = bllFileJP.GetAllList();
            List<ZK.Model.ZK_FileJPType> list = new List<Model.ZK_FileJPType>();
            list = bllFileJP.DataTableToList(ds.Tables[0]);
            for (int i = 0; i <= list.Count - 1; i++)
            {
                if (list[i].isOpen == true)
                {
                    int updateid = list[i].id;
                    ZK.Model.ZK_FileJPType mdlFileJP1 = new Model.ZK_FileJPType();
                    mdlFileJP1 = bllFileJP.GetModel(updateid);
                    mdlFileJP1.isOpen = false;
                    bllFileJP.Update(mdlFileJP1);
                }
            }

            mdlFileJP = bllFileJP.GetModel(Convert.ToInt32(id));
            mdlFileJP.isOpen = true;

            MessageBox.Show(this.Page, "状态启用成功！");

            bllFileJP.Update(mdlFileJP);

            BindSpecialTopicList();
        }

        public string BindBrowser(Object obj)
        {
            string str = obj.ToString();

            return str;
        }

        private void DeleteList()
        {
            string strDelList = Request.QueryString["checkedlist"];

            if (strDelList != null)
            {
                string[] ids = strDelList.Split(',');   
                if (ids.Length > 0)
                {
                    for (int i = 0; i < ids.Length; i++)
                    {
                        string strRfile = " typeID in(select id from ZK_FileJPType where id=" + Convert.ToInt32(ids[i]) + ")";
                        string strPic = "fileJPTypeID in(select id from ZK_FileJPType where id=" + Convert.ToInt32(ids[i]) + ")";
                        DataSet dsR = bllRfile.GetList(strRfile);
                        DataSet dsPic = bllFileJPImg.GetList(strPic);
                        if (dsR.Tables[0].Rows.Count > 0 || dsPic.Tables[0].Rows.Count > 0)
                        {
                            MessageBox.Show(this, "请先删除该专题下的资源和图片！");
                            break;
                        }
                        else
                        {
                            bool msgError = bllFileJP.DeleteList(strDelList);
                            if (msgError == true)
                                MessageBox.Show(this, "删除成功");
                            else
                                MessageBox.Show(this, "删除失败");
                        }
                    }
                }

            }
        }

        #endregion



    }
}