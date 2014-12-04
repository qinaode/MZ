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
    public partial class NoticeRange : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)//第一次加载
            //{
            //    ShowAllDep();
 
            //}
           // ShowAllDep();
        }

    
        //private string AllDep()
        //{
        //    //DataSet depds=new ZK.BLL.DEPARTMENTS().GetAllList();
        //   string JsonString = RESTfulServices.AddInterface.DepAndUsers.AllDepInfo();
        //   return JsonString;

        //}

        /*
        #region 绑定TreeView
        private void Bind_Tv(DataTable dt, TreeNodeCollection tnc, string pid_val, string DEPARTID, string PARENTDEPARTID, string DEPARTNAME)
        {
            DataView dv = new DataView(dt);//将DataTable存到DataView中，以便于筛选数据
            TreeNode tn;//建立TreeView的节点（TreeNode），以便将取出的数据添加到节点中
            //以下为三元运算符，如果父id为空，则为构建“父id字段 is null”的查询条件，否则构建“父id字段=父id字段值”的查询条件
            //string filter = string.IsNullOrEmpty(pid_val) ? pid + " is null" : string.Format(pid + "='{0}'", pid_val);
            dv.RowFilter =" PARENTDEPARTID=0" ;//利用DataView将数据进行筛选，选出相同 父id值 的数据
            foreach (DataRowView drv in dv)
            {
                tn = new TreeNode();//建立一个新节点（学名叫：一个实例）
                tn.Value = drv[DEPARTID].ToString();//节点的Value值，一般为数据库的id值
                tn.Text = drv[DEPARTNAME].ToString();//节点的Text，节点的文本显示
                tnc.Add(tn);//将该节点加入到TreeNodeCollection（节点集合）中
                Bind_Tv(dt, tn.ChildNodes, tn.Value, DEPARTID, PARENTDEPARTID, DEPARTNAME);//递归（反复调用这个方法，直到把数据取完为止）
            }
        }
        #endregion
         */
    }
}