using Aceout.Infrastructure.Configuration;
using Aceout.Infrastructure.Modules;
using Autofac;
using Microsoft.AspNetCore.Hosting;

namespace Aceout.Web
{
    public class WebModule : DependencyModule
    {
        public WebModule(IHostingEnvironment enviroment, AppConfiguration config) : base(enviroment, config)
        {
        }

        protected override void Load(ContainerBuilder builder)
        {

        }
    }
}
