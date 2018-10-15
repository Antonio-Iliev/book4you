using LibrarySystem.Data.Models;
using System.Collections.Generic;

namespace LibrarySystem.Services.ViewModels
{
    public class GenreViewModel
    {
        public string GenreName { get; set; }

        public ICollection<Book> BooksByGenre { get; set; }
    }
}
