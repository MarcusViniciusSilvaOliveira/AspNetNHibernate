
namespace System
{
    public static class DialectAndDriverConfiguration
    {
        public static NHibernate.Cfg.Configuration Configure(TypeConnection Type)
        {
            NHibernate.Cfg.Configuration cfg = new NHibernate.Cfg.Configuration();
            switch (Type)
            {
                case TypeConnection.SqlServer:
                    cfg.Properties.Add(NHibernate.Cfg.Environment.ConnectionDriver, "NHibernate.Driver.SqlClientDriver");
                    cfg.Properties.Add(NHibernate.Cfg.Environment.Dialect, "NHibernate.Dialect.MsSql2000Dialect");
                    cfg.Properties.Add(NHibernate.Cfg.Environment.BatchSize, "500");
                    cfg.Properties.Add(NHibernate.Cfg.Environment.CommandTimeout, "10000");
                    cfg.Properties.Add(NHibernate.Cfg.Environment.ShowSql, "false");
                    cfg.Properties.Add(NHibernate.Cfg.Environment.FormatSql, "false");
                    break;

                case TypeConnection.Firebird:
                    cfg.Properties.Add(NHibernate.Cfg.Environment.ConnectionDriver, "NHibernate.Driver.FirebirdClientDriver");
                    cfg.Properties.Add(NHibernate.Cfg.Environment.ConnectionProvider, "NHibernate.Connection.DriverConnectionProvider");
                    cfg.Properties.Add(NHibernate.Cfg.Environment.Dialect, "NHibernate.Dialect.FirebirdDialect");
                    cfg.Properties.Add(NHibernate.Cfg.Environment.BatchSize, "500");
                    cfg.Properties.Add(NHibernate.Cfg.Environment.CommandTimeout, "10000");
                    cfg.Properties.Add(NHibernate.Cfg.Environment.ShowSql, "false");
                    cfg.Properties.Add(NHibernate.Cfg.Environment.FormatSql, "false");
                    break;

                case TypeConnection.PostgreSQL:
                    cfg.Properties.Add(NHibernate.Cfg.Environment.ConnectionDriver, "NHibernate.Driver.NpgsqlDriver");
                    cfg.Properties.Add(NHibernate.Cfg.Environment.Dialect, "NHibernate.Dialect.PostgreSQLDialect");
                    cfg.Properties.Add(NHibernate.Cfg.Environment.BatchSize, "500");
                    cfg.Properties.Add(NHibernate.Cfg.Environment.CommandTimeout, "10000");
                    cfg.Properties.Add(NHibernate.Cfg.Environment.ShowSql, "false");
                    cfg.Properties.Add(NHibernate.Cfg.Environment.FormatSql, "false");
                    break;
            }
            return cfg;
        }
    }


    public enum TypeConnection
    {
        SqlServer,
        Firebird,
        PostgreSQL
    }
}
