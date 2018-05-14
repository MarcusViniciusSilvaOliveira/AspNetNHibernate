using NHibernate;
using System.Linq;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Reflection;
using System.IO;
using System.Configuration;

namespace System
{
    /// <summary>
    /// Fábrica de conexões com o banco de dados.
    /// </summary>
    public class ConnectionBase
    {
        static NHibernate.Cfg.Configuration config;
        static ISessionFactory Factory;
        static ConnectionBase()
        {
            ReloadConfiguration();
        }

        public static void ReloadConfiguration()
        {
            config = DialectAndDriverConfiguration.Configure(TypeConnection.SqlServer);
            config.Properties.Add(NHibernate.Cfg.Environment.ConnectionString, System.Configuration.ConfigurationManager.ConnectionStrings["DataBaseConnection"].ConnectionString);
            config.AddAssembly("Entity.Mapping");

            SchemaUpdate updater = new SchemaUpdate(config);
            updater.Execute(true, true);
        }

        public static void SchemaExport()
        {
            ReloadConfiguration();
            new SchemaExport(config).SetOutputFile("wmsgestor.sql").Create(false, false);
        }

        public static ISessionFactory GetFactory()
        {

                if (Factory == null)
                    Factory = config.BuildSessionFactory();
                return Factory;
        }

        public ISessionFactory ReturnFactory()
        {

            if (Factory == null)
                Factory = config.BuildSessionFactory();
            return Factory;
        }

        public static ISession OpenSession()
        {
            if (Factory == null)
                Factory = config.BuildSessionFactory();
            return Factory.OpenSession();
        }
    }
}
