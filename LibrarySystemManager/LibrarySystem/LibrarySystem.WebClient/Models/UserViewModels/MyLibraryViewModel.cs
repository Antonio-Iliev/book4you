using LibrarySystem.Data.Models;
using System.Collections.Generic;

namespace LibrarySystem.WebClient.Models.UserViewModels
{
    public class MyLibraryViewModel
    {
        public MyLibraryViewModel(UserViewModel user)
        {
            this.UserView = user;
            this.BorrowBooks = user.BorrowBooks;
            this.ReadBooks = user.ReadBooks;
        }

        public UserViewModel UserView { get; set; }

        public IEnumerable<UsersBooks> BorrowBooks { get; set; }

        public IEnumerable<UsersReadBooks> ReadBooks { get; set; }
    }
}
 