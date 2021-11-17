using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CarMaint.Startup))]
namespace CarMaint
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
