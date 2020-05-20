using Data.Sqlite;
using Data.Sqlite.Executors;
using Data.Sqlite.Executors.Interfaces;
using Data.Sqlite.Interfaces;
using Data.Sqlite.IO;
using Data.Sqlite.IO.Interfaces;
using Data.Sqlite.Mappers;
using Data.Sqlite.Mappers.Interfaces;
using Data.Sqlite.Providers;
using Data.Sqlite.Providers.Interfaces;
using Data.Sqlite.Repositories;
using Data.Sqlite.Repositories.Interfaces;
using Prism.Ioc;

namespace Server.Extensions
{
    public static class ContainerRegistryExtensions
    {
        public static void RegisterRepositories(this IContainerRegistry containerRegistry)
        {            
            containerRegistry.Register<IContactRepository, ContactRepository>();
            containerRegistry.Register<IEnvironmentSettingsProvider, EnvironmentSettingsProvider>();
            containerRegistry.Register<IContactMapper, ContactMapper>();
            containerRegistry.Register<IFileSystemManager, FileSystemManager>();
            containerRegistry.Register<IDbExecutor, DapperDbExecutor>();
            containerRegistry.Register<IDatabase, SqliteDatabase>();
        }
    }
}