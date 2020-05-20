using Dapper.Contrib.Extensions;

namespace Data.Sqlite.Entities
{
    [Table("Contacts")]
    public class Contact
    {
        [ExplicitKey]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}