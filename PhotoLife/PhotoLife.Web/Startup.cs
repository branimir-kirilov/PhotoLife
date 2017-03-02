using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PhotoLife.Startup))]
namespace PhotoLife
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
