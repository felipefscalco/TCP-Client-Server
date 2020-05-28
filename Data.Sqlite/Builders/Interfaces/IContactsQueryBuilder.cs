using Common.Models;
using System;

namespace Data.Sqlite.Builders.Interfaces
{
    public interface IContactsQueryBuilder
    {
        string CreateSearchContactsByNameQuery(string searchText);

        string CreateSearchContactsByTelephoneQuery(string searchText);
        
        string CreateGetAllContactsQuery();

        string CreateUpdateContactQuery(Contact contact);

        string CreateDeleteContactQuery(Guid id);
    }
}