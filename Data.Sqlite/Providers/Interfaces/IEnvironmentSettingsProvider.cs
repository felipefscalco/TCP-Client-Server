namespace Data.Sqlite.Providers.Interfaces
{
    public interface IEnvironmentSettingsProvider
    {
        string LocalDatabasePath { get; }
    }
}