using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TviterApp.Startup))]
namespace TviterApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
