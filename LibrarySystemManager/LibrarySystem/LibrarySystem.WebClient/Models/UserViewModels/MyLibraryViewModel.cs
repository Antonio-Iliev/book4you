using LibrarySystem.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace LibrarySystem.WebClient.Models.UserViewModels
{
    public class MyLibraryViewModel
    {
        public MyLibraryViewModel(UserViewModel user)
        {
            this.userView = user;
        }

        public UserViewModel userView { get; set; }

        public IEnumerable<UsersBooks> borrowBooks { get; set; }

        public IEnumerable<UsersBooks> returnedBooks { get; set; }
    }
}
 