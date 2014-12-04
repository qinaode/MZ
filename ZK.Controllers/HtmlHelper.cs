using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Web;

namespace ZK.Controllers
{
    public static class PagerHelper
    {
        /// <summary>  
        /// 分页Pager显示  
        /// </summary>   
        /// <param name="html"></param>  
        /// <param name="currentPageStr">标识当前页码的QueryStringKey</param>   
        /// <param name="pageSize">每页显示</param>  
        /// <param name="totalCount">总数据量</param>  
        /// <returns></returns> 
        public static string Pager(this HtmlHelper html, string currentPageStr, int pageSize, int totalCount)
        {
            var queryString = html.ViewContext.HttpContext.Request.QueryString;
            int currentPage = 1; //当前页  
            var totalPages = Math.Max((totalCount + pageSize - 1) / pageSize, 1); //总页数  
            var dict = new System.Web.Routing.RouteValueDictionary(html.ViewContext.RouteData.Values);
            var output = new StringBuilder();
            if (!string.IsNullOrEmpty(queryString[currentPageStr]))
            {
                //与相应的QueryString绑定 
                foreach (string key in queryString.Keys)
                    if (queryString[key] != null && !string.IsNullOrEmpty(key))
                        dict[key] = queryString[key];
                int.TryParse(queryString[currentPageStr], out currentPage);
            }
            else
            {
                //获取 ～/Page/{page number} 的页号参数
                if (dict.ContainsKey(currentPageStr))
                    int.TryParse(dict[currentPageStr].ToString(), out currentPage);
            }

            //保留查询字符到下一页
            foreach (string key in queryString.Keys)
                dict[key] = queryString[key];

            //如果有需要，保留表单值到下一页 (我暂时不需要， 所以注释掉)
            //var formValue = html.ViewContext.HttpContext.Request.Form;
            //foreach (string key in formValue.Keys)
            //    if (formValue[key] != null && !string.IsNullOrEmpty(key))
            //        dict[key] = formValue[key]; 
            output.Append("<ul class='pageNo'>");
            if (currentPage <= 0) currentPage = 1;
            if (totalPages > 1)
            {
                if (currentPage != 1)
                {
                    //处理首页连接  
                    dict[currentPageStr] = 1;
                    output.AppendFormat("<li>{0}</li> ", html.RouteLink("首页", dict));
                }
                if (currentPage > 1)
                {
                    //处理上一页的连接  
                    dict[currentPageStr] = currentPage - 1;
             //       output.Append(html.RouteLink("上一页", dict));
                    output.AppendFormat("<li>{0}</li> ", html.RouteLink("上一页", dict));
                }
                else
                {
            //        output.Append("<a disabled='disabled'>首页</a> <a disabled='disabled'>上一页</a>");
                }
                output.Append(" ");
                int currint = 5;
                for (int i = 0; i <= 10; i++)
                {
                    //一共最多显示10个页码，前面5个，后面5个  
                    if ((currentPage + i - currint) >= 1 && (currentPage + i - currint) <= totalPages)
                        if (currint == i)
                        {
                            //当前页处理  
                            output.Append(string.Format("<li><span class='cpb'>{0}</span></li>", currentPage));
                        }
                        else
                        {
                            //一般页处理 
                            dict[currentPageStr] = currentPage + i - currint;
                       //     output.Append(html.RouteLink((currentPage + i - currint).ToString(), dict));
                            output.AppendFormat("<li>{0}</li> ", html.RouteLink((currentPage + i - currint).ToString(), dict));
                        }
                    output.Append(" ");
                }
                if (currentPage < totalPages)
                {
                    //处理下一页的链接 
                    dict[currentPageStr] = currentPage + 1;
                  //  output.Append(html.RouteLink("下一页", dict));
                    output.AppendFormat("<li>{0}</li> ", html.RouteLink("下一页", dict));
                }
                else
                {
            //        output.Append("<a disabled='disabled'>下一页</a> <a disabled='disabled'>末页</a>");
                }
                output.Append(" ");
                if (currentPage != totalPages)
                {
                    dict[currentPageStr] = totalPages;
              //     output.Append(html.RouteLink("末页", dict));
                    output.AppendFormat("<li>{0}</li> ", html.RouteLink("末页", dict));
                }
                output.Append(" ");
            }
            output.AppendFormat("<li><span class='cpb'>第{0}页 / 共{1}页</span></li></ul> ", currentPage, totalPages);//这个统计加不加都行 
            return output.ToString();
        }
    }
}
