using Common.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Data.Sqlite.Repositories.Interfaces
{
    public interface IContactRepository
    {
        Task<Contact> GetContactAsync(Guid contactId, IDbConnection connection);

        Task<IEnumerable<Contact>> GetAllContactsAsync(IDbConnection connection);

        Task DeleteContactAsync(Guid contactId, IDbConnection connection);

        Task InsertContactAsync(Contact contact, IDbConnection connection);
        
        Task UpdateContactAsync(Contact contact, IDbConnection connection);
    }
}