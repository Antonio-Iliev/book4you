using LibrarySystem.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibrarySystem.WebClient.Models
{
    public class BookViewModel
    {
        public BookViewModel(Book book)
        {
            Title = book.Title;
            BooksInStore = book.BooksInStore;
            Genre = book.Genre.GenreName;
            Author = book.Author.Name;
            // TDOD add ImageUrl in db.Book model
            ImageUrl = "Default_image.png";
        }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Title { get; set; }

        [Range(0, 20)]
        public int BooksInStore { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Genre { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Author { get; set; }

        public string ImageUrl { get; set; }
    }
}
