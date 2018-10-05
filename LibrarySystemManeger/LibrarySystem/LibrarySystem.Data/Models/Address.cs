using System.Collections.Generic;

namespace LibrarySystem.Data.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string StreetAddress { get; set; }

        public int TownId { get; set; }
        public Town Town { get; set; }

        public virtual ICollection<UsersAddresses> UsersAddresses { get; set; }
    }
}