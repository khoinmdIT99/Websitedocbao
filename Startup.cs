using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ltweb.Startup))]
namespace ltweb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
