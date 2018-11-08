using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ICareAlz.Startup))]
namespace ICareAlz
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
