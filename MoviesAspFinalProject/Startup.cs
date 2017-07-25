using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MoviesAspFinalProject.Startup))]
namespace MoviesAspFinalProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
