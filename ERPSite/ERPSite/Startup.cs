using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ERPSite.Startup))]
namespace ERPSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
