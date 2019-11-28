using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aceout.Infrastructure.Configuration;
using Autofac;
using Microsoft.AspNetCore.Hosting;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using System.Reflection;
using NHibernate;
using Microsoft.AspNetCore.Identity;
using Aceout.Infrastructure.Identity;
using Aceout.Infrastructure.Repositories;
using Aceout.Domain;
using NHibernate.SqlCommand;
using System.IO;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Automapping;
using Aceout.Domain.Model.Identity;
using FluentNHibernate.Conventions;
using FluentNHibernate;

namespace Aceout.Infrastructure.Modules
{
    public class InfrastructureModule : DependencyModule
    {
        public InfrastructureModule(IHostingEnvironment enviroment, AppConfiguration config) : base(enviroment, config)
        {
        }


        protected override void Load(ContainerBuilder builder)
        {

            var dbConfig = MySQLConfiguration.Standard.Dialect<MySQL57Dialect>()
                .ConnectionString(Configuration.Database.ConnectionString)
                .UseOuterJoin()
                .Driver<MySqlDataDriver>();

            var nhibernateConfig = new NHibernate.Cfg.Configuration();

            switch (Configuration.Database.Type)
            {
                case "MySql":
                    nhibernateConfig.DataBaseIntegration(x =>
                    {
                        x.Dialect<MySQL57Dialect>();
                        x.ConnectionString = Configuration.Database.ConnectionString;
                        x.SchemaAction = SchemaAutoAction.Validate;
                        x.Driver<MySqlDataDriver>();
                    });
                    break;
                default:
                    break;
            }

            var fluentConfig = Fluently.Configure()
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<InfrastructureModule>().Conventions.Add(typeof(CustomForeignKeyConvention)))
                .Database(dbConfig);



            //var mapper = new ModelMapper();
            //mapper.AddMappings(Assembly.GetExecutingAssembly().GetExportedTypes());
            //var mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            //nhibernateConfig.AddMapping(mapping);

            var sessionFactory =

            builder.Register<ISessionFactory>((c, s) =>
                //nhibernateConfig.BuildSessionFactory()
                fluentConfig.BuildSessionFactory()
                )
                .SingleInstance();

            builder.Register<ISession>((c, s) =>
            {
                var session = c.Resolve<ISessionFactory>().OpenSession();
                session.FlushMode = FlushMode.Manual;

                return session;
            })
            .InstancePerLifetimeScope();

            builder.Register<IStatelessSession>((c, s) => {
                var session = c.Resolve<ISessionFactory>().OpenStatelessSession();

                return session;
                })
                .InstancePerLifetimeScope();


            builder.RegisterType<PasswordHasher>().AsImplementedInterfaces();
            builder.RegisterType<PasswordValidator>().AsImplementedInterfaces();

            builder.RegisterType<UserStore>().AsImplementedInterfaces();
            builder.RegisterType<RoleStore>().AsImplementedInterfaces();

            builder.RegisterType<RolePermissionStore>().AsImplementedInterfaces();
            builder.RegisterType<UserManager>().AsSelf();
            builder.RegisterType<RoleManager>().AsSelf();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
        }
    }
    public class CustomForeignKeyConvention: ForeignKeyConvention
    {

        protected override string GetKeyName(Member property, Type type)
        {
            if (property == null)
                return type.Name + "Id";  // many-to-many, one-to-many, join

            return property.Name + "Id"; // many-to-one
        }
    }
}
