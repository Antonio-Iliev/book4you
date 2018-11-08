using LibrarySystem.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibrarySystem.WebClient.Models.BooksViewModels
{
    public class BookViewModel
    {
        public BookViewModel()
        {
        }
  

        public BookViewModel(Book book)
        {
            this.Id = book.Id.ToString();
            this.Title = book.Title;
            this.BooksInStore = book.BooksInStore;
            this.Genre = book.Genre.GenreName;
            this.Author = book.Author.Name;
            this.ImageName = book.ImageName;
        }

        public BookViewModel(Book book, User user) :this(book)
        {
            this.IsBorrowed = user.UsersBooks
                .SingleOrDefault(b => b.BookId.ToString() == this.Id) == null ? false : true;
        }

        public string Id { get; set; }

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

        public string ImageName { get; set; }

        public bool IsBorrowed { get; set; }
    }
}
