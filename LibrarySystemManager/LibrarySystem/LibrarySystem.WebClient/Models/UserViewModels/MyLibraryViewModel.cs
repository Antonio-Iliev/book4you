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
            this.borrowBooks = user.UsersBooks.Where(ub => ub.IsReturn == false);
            this.returnedBooks = user.UsersBooks.Where(ub => ub.IsReturn == true);
        }

        public UserViewModel userView { get; set; }

        public IEnumerable<UsersBooks> borrowBooks { get; set; }

        public IEnumerable<UsersBooks> returnedBooks { get; set; }
    }
}
 