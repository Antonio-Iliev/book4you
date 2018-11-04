using LibrarySystem.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace LibrarySystem.WebClient.Models.UserViewModels
{
    public class UserIndexViewModel
    {
        public UserIndexViewModel(IEnumerable<Book> books)
        {
            this.Books = books.Select(b => new BookViewModel(b));
        }
        public IEnumerable<BookViewModel> Books { get; set; }
    }
}
