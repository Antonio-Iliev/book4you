using LibrarySystem.Data.Models;
using System.Collections.Generic;

namespace LibrarySystem.Services.ViewModels
{
    public class AuthorViewModel
    {
        public string AuthorName { get; set; }

        public ICollection<Book> AuthorBooks { get; set; }
    }
}
