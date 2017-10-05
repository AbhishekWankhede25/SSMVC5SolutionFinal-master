using Microsoft.Practices.Unity;
using SSMVC5WebApp.Infrastructure.Concrete;
using SSMVC5WebApp.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SSMVC5WebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            PhotoService photoService = new PhotoService(new Logger());
            DbConfiguration.SetConfiguration(new SportsStoreEfConfiguration());

            IUnityContainer unityContainer = new UnityContainer();
            ControllerBuilder.Current.SetControllerFactory(new SSMVC5WebApp.Infrastructure.SportsStoreUnityControllerFactory());

        }
    }
}
