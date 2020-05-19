using Dapper;
using Server.Abstractions;
using System;
using System.Data;
using System.Data.SQLite;

namespace Server.Database
{
    public class SqliteDatabase : IDatabase
    {
        private string _localDatabasePath = "";
        private string _connectionString;
        private IFileSystemManager _fileSystemManager;
        

        public SqliteDatabase(IFileSystemManager fileSystemManager)
        {
            _fileSystemManager = fileSystemManager;

            CreateCacheIfNotExists();
        }

        public IDbConnection GetDbConnection()
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                _connectionString = new SQLiteConnectionStringBuilder
                {
                    DataSource = _localDatabasePath,
                    Version = 3,
                    DateTimeKind = DateTimeKind.Utc
                }.ConnectionString;
            }

            var conn = new SQLiteConnection(_connectionString);

            return conn;
        }

        public void CreateCacheIfNotExists()
        {
            if (_fileSystemManager.FileExists(_localDatabasePath))
                CreateDatabaseFile();

            CreateTablesIfNotExists();
        }

        private void CreateDatabaseFile()
        {
            var localDatabaseDirectory = _fileSystemManager.GetDirectoryName(_localDatabasePath);

            if (!_fileSystemManager.DirectoryExists(localDatabaseDirectory))
                _fileSystemManager.CreateDirectory(localDatabaseDirectory);

            using (var fileStream = _fileSystemManager.CreateFile(_localDatabasePath))
                fileStream.Flush();
        }

        private void CreateTablesIfNotExists()
        {
            using (var conn = GetDbConnection())
            {
                var sql = @"PRAGMA foreign_keys = off;
                            BEGIN TRANSACTION;
                            CREATE TABLE if not exists Customers (id VARCHAR (36) PRIMARY KEY UNIQUE NOT NULL, type INTEGER NOT NULL, companyName VARCHAR(80), accountId VARCHAR(20) NOT NULL, mediaTypeId VARCHAR(36), birthDate DATE, identifier VARCHAR(40), name VARCHAR(80) NOT NULL, active BOOLEAN NOT NULL, notes VARCHAR(200), externalCode VARCHAR(30), isDraft BIT NOT NULL); Create Index if not exists Customers_idx_001 on Customers(id); Create Index if not exists Customers_idx_002 on Customers(name);
                            CREATE TABLE if not exists Addresses (id VARCHAR (36) PRIMARY KEY UNIQUE NOT NULL, customerId VARCHAR(36) REFERENCES Customers (id) NOT NULL, addressType INTEGER NOT NULL, zipCode VARCHAR(50), completeAddress VARCHAR(50), number VARCHAR(10), complement VARCHAR(20), reference VARCHAR(30), district VARCHAR(40), city VARCHAR(50), country VARCHAR(40), state VARCHAR(40)); Create Index if not exists Addresses_idx_001 on Addresses(customerId);
                            CREATE TABLE if not exists Documents (id VARCHAR (36) PRIMARY KEY UNIQUE NOT NULL, customerId VARCHAR (36) REFERENCES Customers (id) NOT NULL, value VARCHAR (40) NOT NULL, documentTypeId VARCHAR(36) REFERENCES DocumentTypes (id)); Create Index if not exists Documents_idx_001 on Documents(customerId);
                            CREATE TABLE if not exists Emails (id VARCHAR(36) PRIMARY KEY UNIQUE NOT NULL, customerId VARCHAR(36) REFERENCES Customers (id) NOT NULL, emailType INTEGER NOT NULL, value VARCHAR(60) NOT NULL); Create Index if not exists Emails_idx_001 on Emails(customerId);
                            CREATE TABLE if not exists Phones (id VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE, customerId VARCHAR(36) REFERENCES Customers (id) NOT NULL, phoneType INTEGER NOT NULL, countryCode INTEGER NOT NULL, areaCode INTEGER NOT NULL, phoneNumber VARCHAR(36) NOT NULL); Create Index if not exists Phones_idx_001 on Phones(customerId);
                            CREATE TABLE if not exists Projects (id VARCHAR (36) PRIMARY KEY UNIQUE NOT NULL, accountId VARCHAR (15) NOT NULL, responsibleId VARCHAR (15) NOT NULL, responsibleName VARCHAR (150) NOT NULL, name VARCHAR(150) NOT NULL, createdOn DATE NOT NULL, modifiedOn DATE NOT NULL, type INTEGER, situation INTEGER, stage INTEGER, deliveryDate DATE, closingDate DATE, observation VARCHAR (100), customerId VARCHAR (36), customerName VARCHAR (150) NOT NULL, systemId VARCHAR (36), systemName VARCHAR (150) NOT NULL, isDraft BIT NOT NULL); Create Index if not exists Projects_idx_001 on Projects(id); Create Index if not exists Projects_idx_002 on Projects(customerId);
                            COMMIT TRANSACTION;
                            PRAGMA foreign_keys = on;";

                conn.Execute(sql);
            }
        }
    }
}