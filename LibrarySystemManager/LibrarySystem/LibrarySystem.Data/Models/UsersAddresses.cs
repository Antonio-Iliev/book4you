using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Data.Models
{
    public class UsersAddresses
    {
        public Guid UserId { get; set; }

        public int AddressId { get; set; }

        public User Student { get; set; }

        public Address Address { get; set; }
    }
}
