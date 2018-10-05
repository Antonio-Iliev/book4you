using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Data.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int PhoneNumber { get; set; }

        public DateTime AddOnDate { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<UsersAddresses> UserAddresses { get; set; }

        public virtual ICollection<UsersBooks> UsersBooks { get; set; }
    }
}
