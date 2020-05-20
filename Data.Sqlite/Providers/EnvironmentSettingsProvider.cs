using Data.Sqlite.Providers.Interfaces;
using System;
using System.IO;

namespace Data.Sqlite.Providers
{
    public class EnvironmentSettingsProvider : IEnvironmentSettingsProvider
    {
        public string LocalDatabasePath { get; }
        public EnvironmentSettingsProvider()
        {
            string myDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            LocalDatabasePath = Path.Combine(myDocuments, "db", "contact.db");
        }
    }
}