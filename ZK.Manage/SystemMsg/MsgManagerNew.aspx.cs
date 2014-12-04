using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZK.Common;

namespace ZK.Manage.SystemMsg
{
    public partial class MsgManagerNew : System.Web.UI.Page
    {
        #region 定义
        ZK.BLL.SYSMSGS bllSysmsgs = new BLL.SYSMSGS();
        ZK.BLL.ZK_UserAndSysMsg bllUserAndMsg = new BLL.ZK_UserAndSysMsg();
        ZK.BLL.USERS bllUser = new BLL.USERS();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
           
            InitGridData();
        }

        #region 事件
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string title = txtTitle.Text.Trim();
            string strSql = "1=1";
            if (title != "")
            {
                strSql += " and TITLE like '%" + title + "%'";
            }
            DataSet ds = bllSysmsgs.GetList(strSql);

            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CN_btn_Delete")
            {
                string sid = e.CommandArgument.ToString();
                DeleteInfo(sid);
            }
        }

        protected void gvwDesignationName_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // 得到该控件
            GridView theGrid = sender as GridView;
            int newPageIndex = 0;
            if (e.NewPageIndex == -3)
            {
                //点击了Go按钮
                TextBox txtNewPageIndex = null;

                //GridView较DataGrid提供了更多的API，获取分页块可以使用BottomPagerRow 或者TopPagerRow，当然还增加了HeaderRow和FooterRow
                GridViewRow pagerRow = theGrid.BottomPagerRow;

                if (pagerRow != null)
                {
                    //得到text控件
                    txtNewPageIndex = pagerRow.FindControl("txtNewPageIndex") as TextBox;
                }
                if (txtNewPageIndex != null)
                {
                    //得到索引
                    newPageIndex = int.Parse(txtNewPageIndex.Text) - 1;
                }
            }
            else
            {
                //点击了其他的按钮
                newPageIndex = e.NewPageIndex;
            }
            //防止新索引溢出
            newPageIndex = newPageIndex < 0 ? 0 : newPageIndex;
            newPageIndex = newPageIndex >= theGrid.PageCount ? theGrid.PageCount - 1 : newPageIndex;

            //得到新的值
            theGrid.PageIndex = newPageIndex;

            //重新绑定
            InitGridData();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //当鼠标停留时更改背景色
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='rgb(248, 247, 239)'");
            //当鼠标移开时还原背景色
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
        }

        #endregion

        #region 方法
        public string GetLink(Object obj)
        {
            string linkStr = "";
            string i = obj.ToString();
            if (i == "")
            {
                linkStr = "无";
            }
            else
            {
                linkStr = "有";
            }
            return linkStr;
        }

        private void InitGridData()
        {
            string wherestr = "1=1 and title<>'共享消息' order by sendtime desc";
            DataSet ds = bllSysmsgs.GetList(wherestr);

            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
        }

        private void DeleteInfo(string sid)
        {
            bool msgError = bllSysmsgs.Delete(Convert.ToInt32(sid));
            if (msgError == true)
            {
                MessageBox.Show(this, "删除成功");
            }
            else
            {
                MessageBox.Show(this, "删除失败");
            }

            InitGridData();
        }

        public string GetSendToUser(Object sendto, Object id)
        {
            int sendTo = Convert.ToInt32(sendto);
            int sid = Convert.ToInt32(id);
            string sendToUser = "";
            if (sendTo == 1)
            {
                DataSet ds = bllUserAndMsg.GetList("sysMsgID=" + sid);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<Model.ZK_UserAndSysMsg> list = bllUserAndMsg.DataTableToList(ds.Tables[0]);
                    for (int i = 0; i < list.Count; i++)
                    {
                        int userId = Convert.ToInt32(list[i].userID);
                        Model.USERS mdlUser = bllUser.GetModel(userId);

                        if (i == list.Count - 1)
                        {
                            sendToUser += mdlUser.ACTUALNAME;
                        }
                        else
                            sendToUser += mdlUser.ACTUALNAME + ",";
                    }
                }
            }
            else
            {
                sendToUser = "所有人";
            }
            return sendToUser;
        }
        #endregion
    }
}