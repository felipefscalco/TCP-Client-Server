using Common.Models;
using Data.Sqlite.Builders.Interfaces;
using System;

namespace Data.Sqlite.Builders
{
    public class ContactsQueryBuilder : IContactsQueryBuilder
    {
        public string CreateGetAllContactsQuery()
            => $"select * from Contacts";
        
        public string CreateSearchContactsByNameOrTelephoneQuery(string searchText)
            => $"select * from Contacts where telephone like '%{searchText}%' or name like '%{searchText}%'";

        public string CreateUpdateContactQuery(Contact contact)
            => $"update Contacts set name='{contact.Name}', telephone='{contact.Telephone}', email='{contact.Email}', address='{contact.Address}' where Id='{contact.Id}'";

        public string CreateDeleteContactQuery(Guid id)
            => $"delete from Contacts where id='{id}'";        
    }
}