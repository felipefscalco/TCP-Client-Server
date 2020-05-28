using Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Sqlite.Repositories.Interfaces
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contact>> SearchContactsByNameAsync(string searchText);

        Task<IEnumerable<Contact>> SearchContactsByTelephoneAsync(string searchText);

        Task<IEnumerable<Contact>> GetAllContactsAsync();

        Task DeleteContactAsync(Guid contactId);

        Task InsertContactAsync(Contact contact);
        
        Task UpdateContactAsync(Contact contact);
    }
}