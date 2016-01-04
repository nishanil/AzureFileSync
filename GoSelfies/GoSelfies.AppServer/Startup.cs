using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(GoSelfiesService.Startup))]

namespace GoSelfiesService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}