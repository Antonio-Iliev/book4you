using LibrarySystem.Data.Contracts;
using LibrarySystem.Data.Models;
using LibrarySystem.Services.Abstract;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using LibrarySystem.Services.Constants;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Services
{
    public class BooksServices : BaseServicesClass, IBooksServices
    {
        public BooksServices(ILibrarySystemContext context) : base(context)
        {
        }

        public Book AddBook(string title, Genre genre, Author author, string bookInStore)
        {

            if (title.Length > ServicesConstants.MaxBookTitleLength
                || title.Length < ServicesConstants.MinBookTitleLength)
            {
                throw new ArgumentException($"The book title '{title}' is more then " +
                    $"{ServicesConstants.MaxBookTitleLength} symbols.");
            }

            int numberOfBook;
            if (!int.TryParse(bookInStore, out numberOfBook))
            {
                throw new ArgumentException("Invalid number");
            }

            var newBook = this.context.Books.FirstOrDefault(b => b.Title == title);

            if (newBook != null)
            {
                throw new ArgumentException("The book already exist.");
            }

            newBook = new Book
            {
                Title = title,
                GenreId = genre.Id,
                AuthorId = author.Id,
                BooksInStore = numberOfBook
            };

            this.context.Books.Add(newBook);
            this.context.SaveChanges();
            return newBook;
        }

        public void GetBook(string bookTitle)
        {

            if (bookTitle.Length > ServicesConstants.MaxBookTitleLength)
            {
                throw new ArgumentException();
            }

            var findBook = context.Books.Select(b => new
            {
                Title= b.Title,
                Author = b.Author.Name,
                Genre = b.Genre.GenreName
            }).Where(b => b.Title == bookTitle);

            if (findBook.Any())
            {
                throw new ArgumentException("There is no such book in this the Library.");
            }

            //return findBook;
        }
    }
}
