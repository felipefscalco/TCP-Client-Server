using System;

namespace Common.Models
{
    public class Contact
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public Contact(Guid id, string name, string telephone, string email, string address)
        {
            Id = id;
            Name = name;
            Telephone = telephone;
            Email = email;
            Address = address;
        }
    }
}