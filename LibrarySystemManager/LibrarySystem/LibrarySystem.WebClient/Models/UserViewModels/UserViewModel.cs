using LibrarySystem.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace LibrarySystem.WebClient.Models.UserViewModels
{
    public class UserViewModel
    {
        public UserViewModel(User user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            MiddleName = user.MiddleName;
            LastName = user.LastName;
            Email = user.Email;
            AddOnDate = user.AddOnDate;
            IsDeleted = user.IsDeleted;
            Phone = user.PhoneNumber;
            Address = user.Address.StreetAddress;
            Town = user.Address.Town.TownName;
            BorrowBooks = user.UsersBooks;
            ReadBooks = user.UsersReadBooks;
        }

        public string Id { get; set; }

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

        public string Phone { get; set; }
        public string Address { get; set; }
        public string Town { get; set; }

        public IEnumerable<UsersBooks> BorrowBooks { get; set; }

        public IEnumerable<UsersReadBooks> ReadBooks { get; set; }
    }
}
