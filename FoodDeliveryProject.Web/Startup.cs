using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FoodDeliveryProject.Web.Startup))]
namespace FoodDeliveryProject.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
