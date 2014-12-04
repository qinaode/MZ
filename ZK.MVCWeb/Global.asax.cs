using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ZK.MVCWeb
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "cpl1", // 路由名称
                "cpl1/{id}", // 带有参数的 URL
                new { controller = "ContentPage", action = "List1", id = UrlParameter.Optional } // 参数默认值
             );

            routes.MapRoute(
                "cpl2", // 路由名称
                "cpl2/{id}", // 带有参数的 URL
                new { controller = "ContentPage", action = "List2", id = UrlParameter.Optional } // 参数默认值
             );

            routes.MapRoute(
                "cp", // 路由名称
                "cp/{id}", // 带有参数的 URL
                new { controller = "ContentPage", action = "Index", id = UrlParameter.Optional } // 参数默认值
             );

            routes.MapRoute(
                "Default", // 路由名称
                "{controller}/{action}/{id}", // 带有参数的 URL
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // 参数默认值
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);
        }


        void Application_Error(object sender, EventArgs e)
        {
            Exception x = Server.GetLastError().GetBaseException();
            ZK.Common.WebHint.ShowError(x.ToString(), "", false);
            //NetCMS.Web.UI.WebHint.ShowError(x.Message, "", false); 
        }
    }
}