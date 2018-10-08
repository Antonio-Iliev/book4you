using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LibrarySystem.Data.Models
{
    public class User
    {
        private ICollection<UsersBooks> usersBooks;

        public User()
        {
            this.usersBooks = new HashSet<UsersBooks>();
        }

        public Guid Id { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [MinLength(1)]
        [MaxLength(20)]
        public string MiddleName { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(20)]
        public string LastName { get; set; }

        public int? PhoneNumber { get; set; }

        public DateTime AddOnDate { get; set; }

        public bool IsDeleted { get; set; }

        public int AddressId { get; set; }
        public Address Address { get; set; }

        public virtual ICollection<UsersBooks> UsersBooks
        {
            get { return this.usersBooks; }
            set { this.usersBooks = value; }
        }
    }
}
