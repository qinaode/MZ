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
    public partial class SpecialTopicResourcesMag : System.Web.UI.Page
    {
        ZK.BLL.ZK_FileJP bllFileJP = new BLL.ZK_FileJP();
        BLL.CommondBase bll = new BLL.CommondBase();

        ZK.Model.ZK_FileJP mdlFileJP = new ZK.Model.ZK_FileJP();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PageLoad();
                BindResourcesList();
            }
        }

        #region 事件
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string strSelect = "*";
            string strTable = " ZK_FileJP a join ZK_FileList b on a.fileID=b.fileID join USERS c on b.ownerID=c.USERID";
            string strPrimaryKey = "b.ID";
            string strOrderby = "typeID";
            int PageSize = 10;
            if (bllFileJP.GetAllList().Tables[0].Rows.Count > 0)
            {
                PageSize = bllFileJP.GetAllList().Tables[0].Rows.Count;
            }
            int PageIndex = 1;
            string strWhere = " typeID=" + Convert.ToInt32(Request.QueryString["specialId"]);
            int intBlPage = 1;
            string txtWhere = txtFileName.Text;

            if (strWhere.Trim() != string.Empty)
            {
                strWhere = " typeID=" + Convert.ToInt32(Request.QueryString["specialId"]) + " and a.fileName like '%" + txtWhere + "%'";
            }

            DataSet ds = new DataSet();
            ds = bll.GetList(strSelect, strTable, strPrimaryKey, strOrderby, PageSize, PageIndex, strWhere, intBlPage);

            //获取总数
            //int totalNumber = bllFileJP.GetRecordCount(strWhere);
            int totalNumber = ds.Tables[0].Rows.Count;

            rptSpecialtopicList.DataSource = ds;
            rptSpecialtopicList.DataBind();
        }

        /// <summary>
        /// 操作系列按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AppListItem_Commond(object sender, RepeaterCommandEventArgs e)
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

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {

        }
        #endregion

        #region 方法
        private void BindResourcesList()
        {
            string strSelect = "*";
            string strTable = " ZK_FileJP a left join ZK_FileList b on a.fileID=b.fileID left join USERS c on b.ownerID=c.USERID";
            string strPrimaryKey = "ZK_FileList.ID";
            string strOrderby = "sortNum";
            int PageSize = 10;
            if (bllFileJP.GetAllList().Tables[0].Rows.Count > 0)
            {
                PageSize = bllFileJP.GetAllList().Tables[0].Rows.Count;
            }
            int PageIndex = 1;
            string strWhere = " typeID=" + Convert.ToInt32(Request.QueryString["specialId"]);
            int intBlPage = 1;

            //int startIndex=1;
            //int endindex = startindex + PageSize;  

            DataSet ds = new DataSet();
            ds = bll.GetList(strSelect, strTable, strPrimaryKey, strOrderby, PageSize, PageIndex, strWhere, intBlPage);

            //获取总数
            //int totalNumber = bllFileJP.GetRecordCount(strWhere);

            rptSpecialtopicList.DataSource = ds;
            rptSpecialtopicList.DataBind();
        }

        private void Delect(string id)
        {
            bool msgError = bllFileJP.Delete(Convert.ToInt32(id));
            if (msgError == true)
                MessageBox.Show(this, "删除成功");
            else
                MessageBox.Show(this, "删除失败");

            BindResourcesList();
        }

        private void Move(string levelid, string flag)
        {
            int id = Convert.ToInt32(levelid);
            mdlFileJP = bllFileJP.GetModel(id);

            int depOrder = Convert.ToInt32(mdlFileJP.sortNum);

            string strSQL = "";
            if (flag == "Up")
            {
                strSQL = " sortNum<" + depOrder + " Order by sortNum";
            }

            if (flag == "Down")
            {
                strSQL = "sortNum>" + depOrder + " Order by sortNum desc";
            }
            System.Data.DataSet ds = bllFileJP.GetList(strSQL);

            List<ZK.Model.ZK_FileJP> depList = new List<Model.ZK_FileJP>();
            depList = bllFileJP.DataTableToList(ds.Tables[0]);
            if (depList.Count > 0)
            {
                int upid = Convert.ToInt32(depList[depList.Count - 1].sortNum);

                int upOrgid = depList[depList.Count - 1].ID;
                ZK.Model.ZK_FileJP depmdlB = new Model.ZK_FileJP();
                depmdlB = bllFileJP.GetModel(upOrgid);

                ZK.Model.ZK_FileJP depmdl1 = new Model.ZK_FileJP();
                ZK.Model.ZK_FileJP depmdl2 = new Model.ZK_FileJP();

                depmdl1.ID = mdlFileJP.ID;
                depmdl1.typeID = mdlFileJP.typeID;
                depmdl1.sortNum = upid;
                depmdl1.fileID = mdlFileJP.fileID;
                depmdl1.fileName = mdlFileJP.fileName;
                depmdl1.fileType = mdlFileJP.fileType;
                depmdl1.imageURL = mdlFileJP.imageURL;

                depmdl2.ID = depmdlB.ID;
                depmdl2.typeID = depmdlB.typeID;
                depmdl2.sortNum = depOrder;
                depmdl2.fileID = depmdlB.fileID;
                depmdl2.fileName = depmdlB.fileName;
                depmdl2.fileType = depmdlB.fileType;
                depmdl2.imageURL = depmdlB.imageURL;

                bllFileJP.Update(depmdl1);
                bllFileJP.Update(depmdl2);

                BindResourcesList();
            }

        }

        public string BindFileType(Object obj)
        {
            string str = "";
            if (obj.ToString() != string.Empty)
            {
                int markIndex = Convert.ToInt32(obj);
                if (markIndex == 2)
                { str = "文档"; }
                else if (markIndex == 1)
                { str = "视频"; }
                else if (markIndex == 3)
                { str = "图片"; }
                else if (markIndex == 4)
                { str = "音频"; }
                else if (markIndex == 6)
                { str = "excel"; }
                else if (markIndex == 7)
                { str = "ppt"; }
                else if (markIndex == 8)
                { str = "pdf"; }
                else if (markIndex == 9)
                { str = "rar"; }
                else if (markIndex == 10)
                { str = "其他"; }
                else
                { str = "未知"; }
            }
            else
            { str = "未知"; }

            return str;
        }

        private void PageLoad()
        {
            if (Request.QueryString["ty"] == "Resources")
            {
                ZK.BLL.ZK_FileJPType bllFileJP = new BLL.ZK_FileJPType();
                ZK.Model.ZK_FileJPType mdlFileJP = new ZK.Model.ZK_FileJPType();

                int id = Convert.ToInt32(Request.QueryString["specialId"]);

                mdlFileJP = bllFileJP.GetModel(id);

                lblSpecialTopic.Text = mdlFileJP.TypeName;
            }
        }
        #endregion

    }
}