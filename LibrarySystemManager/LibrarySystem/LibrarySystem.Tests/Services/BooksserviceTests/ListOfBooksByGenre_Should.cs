using LibrarySystem.Data.Context;
using LibrarySystem.Data.Models;
using LibrarySystem.Services;
using LibrarySystem.Services.Abstract.Contracts;
using LibrarySystem.Services.Exceptions.GenreServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;

namespace LibrarySystem.Tests.Services.BooksserviceTests
{
    [TestClass]
    public class ListOfBooksByGenre_Should
    {
        [TestMethod]
        public void ListOfBooksByGenre_WhenGenreIsPassed_ShouldReturnBooksByGenre()
        {
            // Arrange
            var contexInMemory = new DbContextOptionsBuilder<LibrarySystemContext>()
                .UseInMemoryDatabase(databaseName: "ListOfBooksByGenre").Options;

            var validationMock = new Mock<IValidations>();

            string title1 = "newBook1";
            string title2 = "newBook2";
            string title3 = "newBook3";
            string author = "Author";
            string genre = "Genre";
            var authorMock = new Author() { Name = author };
            var genreMock = new Genre() { GenreName = genre };

            var book1 = new Book
            {
                Id = new Guid(),
                Title = title1,
                Author = authorMock,
                Genre = genreMock
            };

            var book2 = new Book
            {
                Id = new Guid(),
                Title = title2,
                Author = authorMock,
                Genre = genreMock
            };

            var book3 = new Book
            {
                Id = new Guid(),
                Title = title3,
                Author = authorMock,
                Genre = genreMock
            };

            using (var arrangeContext = new LibrarySystemContext(contexInMemory))
            {
                arrangeContext.Books.Add(book1);
                arrangeContext.Books.Add(book2);
                arrangeContext.Books.Add(book3);
                arrangeContext.SaveChanges();
            }


            using (var actContext = new LibrarySystemContext(contexInMemory))
            {
                var unitOfWork = new UnitOfWork(actContext);
                var bookServices = new BooksServices(unitOfWork, validationMock.Object);

                var listOfBooks = bookServices.ListOfBooksByGenre(genre).ToList();

                // Assert
                int count = listOfBooks.Count;
                Assert.AreEqual(3, count);
                Assert.AreEqual(title1, listOfBooks[0].Title);
                Assert.AreEqual(title2, listOfBooks[1].Title);
                Assert.AreEqual(title3, listOfBooks[2].Title);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(AddGenreNullableExeption))]
        public void ListOfBooksByGenre_WhenThereIsnotSuchGenre_ThrowException()
        {
            // Arrange
            var contexInMemory = new DbContextOptionsBuilder<LibrarySystemContext>()
                .UseInMemoryDatabase(databaseName: "ListOfBooksByGenre Exception").Options;

            var validationMock = new Mock<IValidations>();

            string genre = "Genre";


            using (var actContext = new LibrarySystemContext(contexInMemory))
            {
                var unitOfWork = new UnitOfWork(actContext);
                var bookServices = new BooksServices(unitOfWork, validationMock.Object);

                var listOfBooks = bookServices.ListOfBooksByGenre(genre);
            }
        }
    }
}
