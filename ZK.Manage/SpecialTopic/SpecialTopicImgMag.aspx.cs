using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZK.Common;
using System.Data;

namespace ZK.Manage.SpecialTopic
{
    public partial class SpecialTopicImgMag : System.Web.UI.Page
    {
        ZK.BLL.ZK_FileJPType bllFileJP = new BLL.ZK_FileJPType();
        ZK.Model.ZK_FileJPType mdlFileJP = new ZK.Model.ZK_FileJPType();

        ZK.BLL.ZK_FileJPPic bllFileJPPic = new BLL.ZK_FileJPPic();
        ZK.Model.ZK_FileJPPic mdlFileJPPic = new ZK.Model.ZK_FileJPPic();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PageLoad();
                BindResourcesImgList();
                DeleteList();
            }
        }

        #region 事件
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 操作系列按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UserListItem_Commond(object sender, RepeaterCommandEventArgs e)
        {

            if (e.CommandName == "CN_btn_Delete")
            {
                string id = e.CommandArgument.ToString();
                Delect(id);
            }
            if (e.CommandName == "CN_btn_MoveUp")
            {
                string id = e.CommandArgument.ToString();
                Move(id, "Up");
            }
            if (e.CommandName == "CN_btn_MoveDown")
            {
                string id = e.CommandArgument.ToString();
                Move(id, "Down");
            }
        }

        protected void btnUploadImg_Click(object sender, EventArgs e)
        {
            string imgLink = txtImgURL.Text;
            string imag = upFile.Value;
            if (imag == string.Empty)
            {
                MessageBox.Show(this, "请先添加图片！");
                return;
            }

            #region
            //if (imgLink==string.Empty)
            //{
            //    MessageBox.Show(this, "链接地址不能为空！");
            //    return;
            //}   
            //if (imgLink != string.Empty)
            //{
            //    if (imgLink.Length >= 7)
            //    {
            //        if (imgLink.Trim().Substring(0, 7) != "http://" && imgLink.Trim().Substring(0, 8) != "https://")
            //        {
            //            MessageBox.Show(this, "链接地址必须以'http://'或'https://'开头");
            //            return;
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show(this, "链接地址必须以http://或https://开头");
            //        return;
            //    }

            //    mdlFileJPPic.imageURL = imgLink; 
            //}
            #endregion

            string fullFileName = this.upFile.PostedFile.FileName;//要上传文件的全路径；  
            string fileName = fullFileName.Substring(fullFileName.LastIndexOf("\\") + 1);  //截取当前全路径的最后文字，文件名  
            string type = fullFileName.Substring(fullFileName.LastIndexOf(".") + 1); //查取.后面的字符，即文件名的扩展名。判断上传格式是否为图片  

            this.upFile.PostedFile.SaveAs(Server.MapPath("../SpecialImage/") + fileName);  //上传    MapPath返回相对路径 

            mdlFileJPPic.imageName = "/SpecialImage/" + fileName;

            //复制专题文件夹和图片  
            string B_ImgPath = "/SpecialImage";
            string F_ImgPath = Server.MapPath(B_ImgPath);

            string msg = ZK.Common.CommonFunction.CopyDirectory(Server.MapPath(B_ImgPath), F_ImgPath);

            mdlFileJPPic.imageURL = imgLink;
            mdlFileJPPic.fileJPTypeID = Convert.ToInt32(Request.QueryString["specialId"]);

            DataSet ds = bllFileJPPic.GetAllList();
            int level = 0;
            List<ZK.Model.ZK_FileJPPic> list = new List<Model.ZK_FileJPPic>();
            list = bllFileJPPic.DataTableToList(ds.Tables[0]);
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
                level = level + 1;
            }
            else
                level = 1;

            mdlFileJPPic.sortNum = level;

            bllFileJPPic.Add(mdlFileJPPic);

            txtImgURL.Text = "";

            //BindResourcesImgList();
        }
        #endregion

        #region 方法
        private void PageLoad()
        {
            if (Request.QueryString["ty"] == "imgMag")
            {
                int id = Convert.ToInt32(Request.QueryString["specialId"]);

                mdlFileJP = bllFileJP.GetModel(id);

                lblSpecialTopic.Text = mdlFileJP.TypeName;
                img_Special.ImageUrl = mdlFileJP.imageURL;
            }
        }

        private void BindResourcesImgList()
        {
            int PageIndex = 1;
            int PageSize = 10;

            int fileIPTypeid = Convert.ToInt32(Request.QueryString["specialId"]);
            string strWhere = " fileJPTypeID=" + fileIPTypeid;

            DataSet ds = new DataSet();
            ds = bllFileJPPic.GetListByPage(strWhere, "sortNum", PageIndex, PageSize);

            List<ZK.Model.ZK_FileJPPic> list = new List<Model.ZK_FileJPPic>();
            list = bllFileJPPic.DataTableToList(ds.Tables[0]);
            if (list.Count > 0)
            {
                img_Special.ImageUrl = list[0].imageName;
            }
            //获取总数
            int totalNumber = bllFileJPPic.GetRecordCount(strWhere);

            //rptSpecialtopicList.DataSource = ds;
            //rptSpecialtopicList.DataBind();
        }

        private void Delect(string id)
        {
            bool msgError = bllFileJPPic.Delete(Convert.ToInt32(id));
            if (msgError == true)
                MessageBox.Show(this, "删除成功");
            else
                MessageBox.Show(this, "删除失败");

        }

        private void Move(string levelid, string flag)
        {
            int id = Convert.ToInt32(levelid);
            mdlFileJPPic = bllFileJPPic.GetModel(id);

            int depOrder = Convert.ToInt32(mdlFileJPPic.sortNum);

            string strSQL = "";
            if (flag == "Up")
            {
                //strSQL = " sortNum<" + depOrder + " Order by sortNum";
                strSQL = " sortNum>" + depOrder + " Order by sortNum";
            }

            if (flag == "Down")
            {
                //strSQL = "sortNum>" + depOrder + " Order by sortNum desc";
                strSQL = "sortNum<" + depOrder + " Order by sortNum desc";
            }
            System.Data.DataSet ds = bllFileJPPic.GetList(strSQL);

            List<ZK.Model.ZK_FileJPPic> depList = new List<Model.ZK_FileJPPic>();
            depList = bllFileJPPic.DataTableToList(ds.Tables[0]);
            if (depList.Count > 0)
            {
                //int upid = Convert.ToInt32(depList[depList.Count - 1].sortNum);
                int upid = Convert.ToInt32(depList[0].sortNum);
                //int upOrgid = depList[depList.Count - 1].id;
                int upOrgid = depList[0].id;
                ZK.Model.ZK_FileJPPic depmdlB = new Model.ZK_FileJPPic();
                depmdlB = bllFileJPPic.GetModel(upOrgid);

                ZK.Model.ZK_FileJPPic depmdl1 = new Model.ZK_FileJPPic();
                ZK.Model.ZK_FileJPPic depmdl2 = new Model.ZK_FileJPPic();

                depmdl1.id = mdlFileJPPic.id;
                depmdl1.fileJPTypeID = mdlFileJPPic.fileJPTypeID;
                depmdl1.sortNum = upid;
                depmdl1.imageName = mdlFileJPPic.imageName;
                depmdl1.imageURL = mdlFileJPPic.imageURL;

                depmdl2.id = depmdlB.id;
                depmdl2.fileJPTypeID = depmdlB.fileJPTypeID;
                depmdl2.sortNum = depOrder;
                depmdl2.imageName = depmdlB.imageName;
                depmdl2.imageURL = depmdlB.imageURL;

                bllFileJPPic.Update(depmdl1);
                bllFileJPPic.Update(depmdl2);

                BindResourcesImgList();
            }

        }

        private void DeleteList()
        {
            int id = Convert.ToInt32(Request.QueryString["specialId"]);
            string strDelList = Request.QueryString["checkedlist"];
            if (strDelList != null)
            {
                bool msgError = bllFileJPPic.DeleteList(strDelList);
                if (msgError == true)
                    MessageBox.Show(this, "删除成功");
                else
                    MessageBox.Show(this, "删除失败");

                BindResourcesImgList();

                Response.Write("/SpecialTopic/SpecialTopicImgMag.aspx?curp=topic&ty=imgMag&specialId=" + id);
            }
        }

        #endregion

        #region

        #endregion

    }
}