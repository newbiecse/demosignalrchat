using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DemoSignalRChat.Startup))]
namespace DemoSignalRChat
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
