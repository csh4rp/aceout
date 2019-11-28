using Aceout.Infrastructure.Configuration;
using Autofac;
using Autofac.Core;
using Microsoft.AspNetCore.Hosting;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;


namespace Aceout.Infrastructure.Modules
{
    public abstract class DependencyModule : Autofac.Module
    {
        protected IHostingEnvironment Enviroment { get; }
        protected AppConfiguration Configuration { get; }

        public DependencyModule(IHostingEnvironment enviroment, AppConfiguration config)
        {
            Enviroment = enviroment;
            Configuration = config;
        }

    }
}
