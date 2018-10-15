using LibrarySystem.Data.Models;
using LibrarySystem.Services.ViewModels;
using System.Collections.Generic;

namespace LibrarySystem.Services
{
    public interface IBooksServices
    {
        BookViewModel AddBook(string title, int genreId, int authorId, string bookInStore);

        BookViewModel GetBook(string bookTitel);

        IEnumerable<BookViewModel> ListOfBooksByGenre(string byGenre);

        IEnumerable<BookViewModel> ListOfBooksByAuthor(string byAuthor);

        IEnumerable<BookViewModel> ListBooks();
    }
}