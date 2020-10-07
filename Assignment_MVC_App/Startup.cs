using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Assignment_MVC_App.Startup))]
namespace Assignment_MVC_App
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
