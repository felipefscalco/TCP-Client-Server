using Common.Models;
using Data.Sqlite.Builders.Interfaces;
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
        private readonly IContactsQueryBuilder _contactsQueryBuilder;
        private readonly IDbExecutor _dbExecutor;
        private readonly IDatabase _database;

        public ContactRepository(IContactMapper contactMapper, IContactsQueryBuilder contactsQueryBuilder, IDbExecutor dbExecutor, IDatabase database)
        {
            _contactMapper = contactMapper;
            _contactsQueryBuilder = contactsQueryBuilder;
            _dbExecutor = dbExecutor;
            _database = database;
        }
        
        public Task<Contact> GetContactAsync(Guid contactId)
        {
            return null;
        }       

        public Task<IEnumerable<Contact>> GetAllContactsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task InsertContactAsync(Contact contact)
        {
            using (var connection = _database.GetDbConnection())
            {
                connection.EnsureConnectionOpen();

                var dbCustomers = _contactMapper.Map(contact);

                await _dbExecutor.InsertAsync(connection, dbCustomers).ConfigureAwait(false);
            }
        }

        public Task UpdateContactAsync(Contact contact)
        {
            throw new NotImplementedException();
        }
        
        public Task DeleteContactAsync(Guid contactId)
        {
            throw new NotImplementedException();
        }
    }
}
