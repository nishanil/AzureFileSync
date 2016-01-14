using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(GoMonkeysService.Startup))]

namespace GoMonkeysService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}