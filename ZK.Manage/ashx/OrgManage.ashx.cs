using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using System.Web.Script.Serialization;

namespace ZK.Manage.ashx
{
    /// <summary>
    /// OrgManage 的摘要说明
    /// </summary>
    public class OrgManage : IHttpHandler
    {
        ZK.BLL.DEPARTMENTS depbll = new BLL.DEPARTMENTS();
        ZK.Model.DEPARTMENTS depmdl = new Model.DEPARTMENTS();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string Flag = context.Request.Form["Flag"];
            string Result = string.Empty;

            if (Flag == "GetListPaging")
            {
                //Result = GetListPaging(context);
            }
           
            context.Response.Write(Result);            
        }
        ZK.BLL.SYSMSGS bll = new BLL.SYSMSGS();
        ZK.Model.SYSMSGS mdl = new Model.SYSMSGS();
        //private string GetListPaging(HttpContext context)
        //{
        //    string PageIndex = context.Request.Form["PageIndex"] == "" ? "1" : context.Request.Form["PageIndex"];
        //    string PageSize = context.Request.Form["PageSize"] == "" ? "10" : context.Request.Form["PageSize"];
          
        //    string strWhere = context.Request.Form["strWhere"];
        //    List<ZK.Model.SYSMSGS> List = new List<Model.SYSMSGS>();
        //    StringBuilder builder = new StringBuilder();
        //    builder.Append("1=1 and fileid in (" + sourceids + ") ");
        //    if (strWhere != "")
        //    {
        //        builder.Append(" and fileName like '%" + strWhere + "%'");
        //    }
        //    DataSet ds = bll.GetListByPage(builder.ToString(), "", int.Parse(PageIndex), int.Parse(PageSize));

        //    List = bll.DataTableToList(ds.Tables[0]);
        //    JavaScriptSerializer jss = new JavaScriptSerializer();
        //    int total = bll.GetRecordCount(builder.ToString());
        //    return SerializeJsonString(jss.Serialize(List), total);
        //}

        private static string SerializeJsonString(string DataList, int TotalNumber)
        {

            return "{\"DataList\":" + DataList + ",\"TotalNumber\":" + TotalNumber + "}";
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