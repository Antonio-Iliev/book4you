using LibrarySystem.Data.Models;
using System;
using System.Collections.Generic;

namespace LibrarySystem.Services.ViewModels
{
    public class UserViewModel
    {
        public string FullName { get; set; }

        public string Phonenumber { get; set; }

        public string Address { get; set; }

        public string Town { get; set; }

        public DateTime AddedOn { get; set; }

        public ICollection<UsersBooks> UserBooks { get; set; }
    }
}
