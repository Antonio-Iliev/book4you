using LibrarySystem.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace LibrarySystem.WebClient.Models.UserViewModels
{
    public class UserViewModel
    {
        public UserViewModel(User user, IEnumerable<Book> books)
        {
            this.UserId = user.Id;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Books = books.Select(b => new BookViewModel(b));
        }

        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public IEnumerable<BookViewModel> Books { get; set; }
    }
}
