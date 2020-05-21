using Common.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Data.Sqlite.Repositories.Interfaces
{
    public interface IContactRepository
    {
        Task<Contact> GetContactAsync(Guid contactId);

        Task<IEnumerable<Contact>> GetAllContactsAsync();

        Task DeleteContactAsync(Guid contactId);

        Task InsertContactAsync(Contact contact);
        
        Task UpdateContactAsync(Contact contact);
    }
}