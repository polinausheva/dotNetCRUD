using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DotNetCrud.Web.Startup))]
namespace DotNetCrud.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
