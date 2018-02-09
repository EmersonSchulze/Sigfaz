using System.Data.Entity;
using Sigfaz.Portal.AutoMapper;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Sigfaz.Infra.Data.Contexto;

namespace Sigfaz.Portal
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfig.RegisterMappings();

            Database.SetInitializer<SigfazContext>(new CreateDatabaseIfNotExists<SigfazContext>());

           
        }
    }
}
