using Common.Models;
using Data.Sqlite.Builders.Interfaces;
using System;

namespace Data.Sqlite.Builders
{
    public class ContactsQueryBuilder : IContactsQueryBuilder
    {
        public string CreateGetAllContactsQuery()
            => $"select * from Contacts";

        public string CreateGetContactQuery(Guid id)
            => $"select * from Contacts where id = '{id}'";

        public string CreateUpdateContactQuery(Contact contact)
            => $"update Contacts set name={contact.Name}, telephone={contact.Telephone}, email={contact.Email}, address={contact.Address} where Id='{contact.Id}'";

        public string CreateDeleteContactQuery(Guid id)
            => $"select * from Contacts";
    }
}