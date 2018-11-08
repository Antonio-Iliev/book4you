using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibrarySystem.Data.Models
{
    public class Book
    {
        public Book() { }

        public Book
            (string title, int booksInStore, Genre genre, Author author, string imageName)
        {
            this.Title = title;
            this.BooksInStore = booksInStore;
            this.ImageName = imageName;
            this.Genre = genre;
            this.Author = author;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Range(0, 20)]
        public int BooksInStore { get; set; }

        public int GenreId { get; set; }
        public Genre Genre { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public string ImageName { get; set; }

        public ICollection<UsersBooks> UsersBooks { get; set; }

    }
}
