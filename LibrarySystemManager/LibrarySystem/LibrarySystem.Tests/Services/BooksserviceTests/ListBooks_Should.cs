using LibrarySystem.Data.Context;
using LibrarySystem.Data.Models;
using LibrarySystem.Services;
using LibrarySystem.Services.Abstract.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibrarySystem.Tests.Services.BooksserviceTests
{
    [TestClass]
    public class ListBooks_Should
    {
        [TestMethod]
        public void ListBooks_WhenMethodIsCall_ShouldReturnAllBooks()
        {
            // Arrange
            var contexInMemory = new DbContextOptionsBuilder<LibrarySystemContext>()
                .UseInMemoryDatabase(databaseName: "ListOfBooksByGenre").Options;

            var validationMock = new Mock<IValidations>();

            string title1 = "newBook1", title2 = "newBook2", title3 = "newBook3",
            author1 = "Author1", author2 = "Author2", author3 = "Author3",
            genre1 = "Genre1", genre2 = "Genre2", genre3 = "Genre3";

            Author authorMock1 = new Author() { Name = author1 },
            authorMock2 = new Author() { Name = author2 },
            authorMock3 = new Author() { Name = author3 };

            Genre genreMock1 = new Genre() { GenreName = genre1 },
            genreMock2 = new Genre() { GenreName = genre2 },
            genreMock3 = new Genre() { GenreName = genre3 };

            var book1 = new Book
            {
                Id = new Guid(),
                Title = title1,
                Author = authorMock1,
                Genre = genreMock1
            };

            var book2 = new Book
            {
                Id = new Guid(),
                Title = title2,
                Author = authorMock2,
                Genre = genreMock2
            };

            var book3 = new Book
            {
                Id = new Guid(),
                Title = title3,
                Author = authorMock3,
                Genre = genreMock3
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

                var listOfBooks = bookServices.ListBooks().ToList();

                // Assert
                int count = listOfBooks.Count;
                Assert.AreEqual(3, count);
                Assert.AreEqual(title1, listOfBooks[0].Title);
                Assert.AreEqual(title2, listOfBooks[1].Title);
                Assert.AreEqual(title3, listOfBooks[2].Title);
            }
        }
    }
}
