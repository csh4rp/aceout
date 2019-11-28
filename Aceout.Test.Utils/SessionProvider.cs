using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;

namespace Aceout.Tests.Utils
{
    public class SessionProvider
    {
        private static ISessionFactory _factory;

        static SessionProvider()
        {
            var nhibernateConfig = new NHibernate.Cfg.Configuration();

            nhibernateConfig.DataBaseIntegration(x =>
            {
                x.Dialect<SQLiteDialect>();
                x.ConnectionString = "Data Source=:memory:";
                x.SchemaAction = SchemaAutoAction.Create;
                x.Driver<SQLite20Driver>();
            });


            var mapper = new ModelMapper();
            mapper.AddMappings(Assembly.GetExecutingAssembly().GetExportedTypes());
            var mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            nhibernateConfig.AddMapping(mapping);
            _factory = nhibernateConfig.BuildSessionFactory();

        }

        public static ISession GetSession() => _factory.OpenSession();

        public static IStatelessSession GetStatelessSession() => _factory.OpenStatelessSession();

    }
}
