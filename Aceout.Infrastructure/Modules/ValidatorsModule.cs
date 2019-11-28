using Aceout.Domain.Services.Materials.Categories;
using Aceout.Infrastructure.Configuration;
using Autofac;
using Autofac.Core;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Infrastructure.Modules
{
    public class ValidatorsModule : DependencyModule
    {
        public ValidatorsModule(IHostingEnvironment enviroment, AppConfiguration config) : base(enviroment, config)
        {
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MaterialCategoryValidator>().As<IMaterialCategoryValidator>();
        }
    }
}
