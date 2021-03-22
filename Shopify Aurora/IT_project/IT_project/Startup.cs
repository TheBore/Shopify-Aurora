using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IT_project.Startup))]
namespace IT_project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
