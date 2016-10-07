using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BritanicaQuiz.Startup))]
namespace BritanicaQuiz
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
