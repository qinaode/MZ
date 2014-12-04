using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZK.Manage.ashx
{
    /// <summary>
    /// OrgManageDown 的摘要说明
    /// </summary>
    public class OrgManageDown : IHttpHandler
    {
        ZK.BLL.DEPARTMENTS depbll = new BLL.DEPARTMENTS();
        ZK.Model.DEPARTMENTS depmdl = new Model.DEPARTMENTS();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string Rezult = string.Empty;
            string strSQL = string.Empty;

            int id = Convert.ToInt32(context.Request.Form["ID"]);
            depmdl = depbll.GetModel(id);

            int depOrder = depmdl.ORDERVALUE;
            int depParentid = depmdl.PARENTDEPARTID;

             strSQL = "PARENTDEPARTID=" + depParentid + " And " + "ORDERVALUE>" + depOrder + "Order by ORDERVALUE desc";
            
            System.Data.DataSet ds = depbll.GetList(strSQL);

            List<ZK.Model.DEPARTMENTS> depList = new List<Model.DEPARTMENTS>();
            depList = depbll.DataTableToList(ds.Tables[0]);

            int upid = depList[depList.Count - 1].ORDERVALUE;

            int upOrgid = depList[depList.Count - 1].DEPARTID;
            ZK.Model.DEPARTMENTS depmdlB = new Model.DEPARTMENTS();
            depmdlB = depbll.GetModel(upOrgid);

            ZK.Model.DEPARTMENTS depmdl1 = new Model.DEPARTMENTS();
            ZK.Model.DEPARTMENTS depmdl2 = new Model.DEPARTMENTS();

            depmdl1.DEPARTID = depmdl.DEPARTID;
            depmdl1.DEPARTNAME = depmdl.DEPARTNAME;
            depmdl1.ORDERVALUE = upid;
            depmdl1.PARENTDEPARTID = depmdl.PARENTDEPARTID;
            depmdl1.CREATETIME = depmdl.CREATETIME;

            depmdl2.DEPARTID = depmdlB.DEPARTID;
            depmdl2.DEPARTNAME = depmdlB.DEPARTNAME;
            depmdl2.ORDERVALUE = depOrder;
            depmdl2.PARENTDEPARTID = depmdlB.PARENTDEPARTID;
            depmdl2.CREATETIME = depmdlB.CREATETIME;

            depbll.Update(depmdl1);
            depbll.Update(depmdl2);

            context.Response.Write("Rezult");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}