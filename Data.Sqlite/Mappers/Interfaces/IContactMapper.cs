using Common.Models;
using System.Collections.Generic;

namespace Data.Sqlite.Mappers.Interfaces
{
    public interface IContactMapper
    {        
        Contact Map(Entities.Contact contact);

        IEnumerable<Contact> Map(IEnumerable<Entities.Contact> contacts);

        Entities.Contact Map(Contact contact);

        IEnumerable<Entities.Contact> Map(IEnumerable<Contact> contacts);
    }
}