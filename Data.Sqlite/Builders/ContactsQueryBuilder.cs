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

        public string CreateUpdateContactQuery(Guid id)
            => $"select * from Contacts";

        public string CreateDeleteContactQuery(Guid id)
            => $"select * from Contacts";
    }
}