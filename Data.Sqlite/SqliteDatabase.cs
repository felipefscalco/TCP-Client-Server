using Dapper;
using Data.Sqlite.Interfaces;
using Data.Sqlite.IO.Interfaces;
using Data.Sqlite.Providers.Interfaces;
using System;
using System.Data;
using System.Data.SQLite;

namespace Data.Sqlite
{
    public class SqliteDatabase : IDatabase
    {
        private readonly IEnvironmentSettingsProvider _environmentSettingsProvider;
        private readonly IFileSystemManager _fileSystemManager;
        private string _connectionString;

        public SqliteDatabase(IEnvironmentSettingsProvider environmentSettingsProvider, IFileSystemManager fileSystemManager)
        {
            _environmentSettingsProvider = environmentSettingsProvider;
            _fileSystemManager = fileSystemManager;

            CreateCacheIfNotExists();
        }

        public IDbConnection GetDbConnection()
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                _connectionString = new SQLiteConnectionStringBuilder
                {
                    DataSource = _environmentSettingsProvider.LocalDatabasePath,
                    Version = 3,
                    DateTimeKind = DateTimeKind.Utc
                }.ConnectionString;
            }

            var conn = new SQLiteConnection(_connectionString);

            //conn.Open();

            //SetPassword(conn);

            return conn;
        }

        public void CreateCacheIfNotExists()
        {
            if (!_fileSystemManager.FileExists(_environmentSettingsProvider.LocalDatabasePath))
                CreateDatabaseFile();

            CreateTablesIfNotExists();
        }

        private void CreateDatabaseFile()
        {
            var localDatabaseDirectory = _fileSystemManager.GetDirectoryName(_environmentSettingsProvider.LocalDatabasePath);

            if (!_fileSystemManager.DirectoryExists(localDatabaseDirectory))
                _fileSystemManager.CreateDirectory(localDatabaseDirectory);

            using (var fileStream = _fileSystemManager.CreateFile(_environmentSettingsProvider.LocalDatabasePath))
                fileStream.Flush();
        }

        private void CreateTablesIfNotExists()
        {
            using (var conn = GetDbConnection())
            {
                var sql = @"PRAGMA foreign_keys = off;
                            BEGIN TRANSACTION;
                            CREATE TABLE if not exists Contacts (id VARCHAR (36) PRIMARY KEY UNIQUE NOT NULL, type INTEGER NOT NULL, name VARCHAR(100) NOT NULL, telephone VARCHAR(20), email VARCHAR(60), address VARCHAR(100));
                            COMMIT TRANSACTION;
                            PRAGMA foreign_keys = on;";

                conn.Execute(sql);
            }
        }
    }
}