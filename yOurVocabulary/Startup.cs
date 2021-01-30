using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(yOurVocabulary.Startup))]
namespace yOurVocabulary
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
