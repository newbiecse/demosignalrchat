using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.WebPages;

namespace DemoSignalRChat
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            BundleMobileConfig.RegisterBundles(BundleTable.Bundles);

            DisplayModeProvider.Instance.Modes.Insert(0, new DefaultDisplayMode("iphone")
            {
                ContextCondition = Context =>
                                Context.Request.Browser["HardwareModel"] == "iPhone"
            });

            DisplayModeProvider.Instance.Modes.Insert(1, new DefaultDisplayMode("android")
            {
                ContextCondition = Context =>
                                Context.Request.Browser["PlatformName"] == "Android"
            });

            DisplayModeProvider.Instance.Modes.Insert(2, new DefaultDisplayMode("mobile")
            {
                ContextCondition = Context =>
                                Context.Request.Browser["IsMobile"] == "True"
            });

        }
    }
}
