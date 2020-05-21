using Common.Models;
using Data.Sqlite.Mappers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Sqlite.Mappers
{
    public class ContactMapper : IContactMapper
    {
        public Contact Map(Entities.Contact dbContact)
        {
            if (dbContact == null)
                return null;

            return new Contact(
                Guid.Parse(dbContact.Id),
                dbContact.Name,
                dbContact.Telephone,
                dbContact.Email,
                dbContact.Address);
        }

        public Entities.Contact Map(Contact contact)
        {
            if (contact == null)
                return null;

            return new Entities.Contact
            {
                Id = contact.Id.ToString(),
                Address = contact.Address,
                Email = contact.Email,
                Name = contact.Name,
                Telephone = contact.Telephone
            };
        }

        public IEnumerable<Entities.Contact> Map(IEnumerable<Contact> contacts)
        {
            if (contacts == null)
                return null;

            var result = new List<Entities.Contact>(contacts.Count());

            foreach (var contact in contacts)
                result.Add(Map(contact));

            return result;
        }

        public IEnumerable<Contact> Map(IEnumerable<Entities.Contact> dbContacts)
        {
            if (dbContacts == null)
                return null;

            var result = new List<Contact>(dbContacts.Count());

            foreach (var contact in dbContacts)
                result.Add(Map(contact));

            return result;
        }
    }
}