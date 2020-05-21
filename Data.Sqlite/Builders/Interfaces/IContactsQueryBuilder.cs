using Common.Models;
using System;

namespace Data.Sqlite.Builders.Interfaces
{
    public interface IContactsQueryBuilder
    {
        string CreateGetContactQuery(Guid id);
        
        string CreateGetAllContactsQuery();

        string CreateUpdateContactQuery(Guid id);

        string CreateDeleteContactQuery(Guid id);
    }
}