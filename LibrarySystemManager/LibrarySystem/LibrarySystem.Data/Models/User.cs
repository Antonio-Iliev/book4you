using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LibrarySystem.Data.Models
{
    public class User
    {
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

        public virtual ICollection<UsersAddresses> UserAddresses { get; set; }

        public virtual ICollection<UsersBooks> UsersBooks { get; set; }
    }
}
