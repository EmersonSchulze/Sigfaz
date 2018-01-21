using Microsoft.Owin;
using Owin;
using Sigfaz.Portal;

[assembly: OwinStartupAttribute(typeof(Startup))]
namespace Sigfaz.Portal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
            ConfigureAuth(app);
        }
    }
}
