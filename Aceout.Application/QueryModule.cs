using Aceout.Infrastructure.Configuration;
using Aceout.Infrastructure.Modules;
using Autofac;
using Microsoft.AspNetCore.Hosting;

namespace Aceout.Application
{
    public class QueryModule : DependencyModule
    {
        public QueryModule(IHostingEnvironment enviroment, AppConfiguration config) : base(enviroment, config)
        {
        }

        protected override void Load(ContainerBuilder builder)
        {

        }
    }
}
