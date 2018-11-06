using System.Collections.Generic;

namespace LibrarySystem.WebClient.Models.BooksViewModels
{
    public class BookIndexViewModel
    {
        public BookIndexViewModel() { }

        public BookIndexViewModel(IEnumerable<BookViewModel> books)
        {
            this.Books = books;
        }

        public IEnumerable<BookViewModel> Books { get; set; }

        public string SearchBy { get; set; }

        public string Parameters { get; set; }
    }
}
