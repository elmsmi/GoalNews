using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GoalNews.Startup))]
namespace GoalNews
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
