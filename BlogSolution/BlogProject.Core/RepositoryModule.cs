using BlogProject.Core.Objects;
using NHibernate;
using NHibernate.Cache;
using NHibernate.Tool.hbm2ddl;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

namespace BlogProject.Core
{
    public class RepositoryModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISessionFactory>()
              .ToMethod(e => Fluently.Configure()
              .Database(MsSqlConfiguration.MsSql2008.ConnectionString(c => c.FromConnectionStringWithKey("BlogDatabaseConnectionString")))
              .Cache(c => c.UseQueryCache().ProviderClass<HashtableCacheProvider>())
              .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Post>())
                  //.ExposeConfiguration(cfg => new SchemaExport(cfg).Execute(true, true, false))
              .BuildConfiguration()
              .BuildSessionFactory())
              .InSingletonScope();

            Bind<ISession>()
              .ToMethod((ctx) => ctx.Kernel.Get<ISessionFactory>().OpenSession())
              .InRequestScope();
        }
    }
}
