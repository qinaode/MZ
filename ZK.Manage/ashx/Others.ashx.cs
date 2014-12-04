using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Script.Serialization;

namespace ZK.Manage.ashx
{
    /// <summary>
    /// Others 的摘要说明
    /// </summary>
    public class Others : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string Flag = context.Request.Form["Flag"];
            string result = "";
            switch (Flag)
            {
                case "GetDepartList":// 获取部门列表
                    result = GetDepartList(context);
                    break;
                default:
                    break;
            }


            context.Response.Write(result);
        }
        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <param name="context"></param>
        /// <returns>返回部门列表的字符串</returns>
        private string GetDepartList(HttpContext context)
        {
            ZK.BLL.DEPARTMENTS blldepartments = new BLL.DEPARTMENTS();
            List<string> list = new List<string>();
            DataSet ds = blldepartments.GetAllList();
            List<Model.DEPARTMENTS> modellist = blldepartments.DataTableToList(ds.Tables[0]);
            JavaScriptSerializer jss = new JavaScriptSerializer();

            return "{\"DataList\":" + jss.Serialize(modellist) + "}";
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