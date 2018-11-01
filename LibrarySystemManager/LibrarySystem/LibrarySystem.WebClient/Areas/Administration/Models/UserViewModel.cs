using LibrarySystem.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibrarySystem.WebClient.Areas.Administration.Models
{
    public class UserViewModel
    {
        public UserViewModel(User user)
        {
            FirstName = user.FirstName;
            MiddleName = user.MiddleName;
            LastName = user.LastName;
            Email = user.Email;
            AddOnDate = user.AddOnDate;
            IsDeleted = user.IsDeleted;
            Address = user.Address.StreetAddress;
            Town = user.Address.Town.TownName;
            UsersBooks = user.UsersBooks;
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

        [Required]
        [MinLength(4)]
        [MaxLength(200)]
        [EmailAddress]
        public string Email { get; set; }

        public DateTime AddOnDate { get; set; }

        public bool IsDeleted { get; set; }
      
        public string Address { get; set; }
        public string Town { get; set; }

        public ICollection<UsersBooks> UsersBooks { get; set; }
    }
}
