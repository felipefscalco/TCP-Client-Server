using Common.Models;
using Data.Sqlite.Executors.Interfaces;
using Data.Sqlite.Interfaces;
using Data.Sqlite.Mappers.Interfaces;
using Data.Sqlite.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Data.Sqlite.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly IContactMapper _contactMapper;
        private readonly IDbExecutor _dbExecutor;
        private readonly IDatabase _database;

        public ContactRepository(IContactMapper contactMapper, IDbExecutor dbExecutor, IDatabase database)
        {
            _contactMapper = contactMapper;
            _dbExecutor = dbExecutor;
            _database = database;
        }

        public Task<Contact> GetContactAsync(Guid contactId, IDbConnection connection)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Contact>> GetAllContactsAsync(IDbConnection connection)
        {
            throw new NotImplementedException();
        }

        public Task InsertContactAsync(Contact contact, IDbConnection connection)
        {
            throw new NotImplementedException();
        }
        public Task UpdateContactAsync(Contact contact, IDbConnection connection)
        {
            throw new NotImplementedException();
        }

        public Task DeleteContactAsync(Guid contactId, IDbConnection connection)
        {
            throw new NotImplementedException();
        }
    }
}
