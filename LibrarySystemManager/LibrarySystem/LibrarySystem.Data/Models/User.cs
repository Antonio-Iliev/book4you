using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibrarySystem.Data.Models
{
    //TODO: IdentityUser<Guid>
    public class User : IdentityUser
    {

        public User()
        {
            this.UsersBooks = new HashSet<UsersBooks>();
        }
        
        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [MinLength(2)]
        [MaxLength(20)]
        public string MiddleName { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        public string LastName { get; set; }
        
        public DateTime AddOnDate { get; set; }

        public bool IsDeleted { get; set; }

        public int AddressId { get; set; }
        public Address Address { get; set; }

        public ICollection<UsersBooks> UsersBooks { get; set; }
    }
}
