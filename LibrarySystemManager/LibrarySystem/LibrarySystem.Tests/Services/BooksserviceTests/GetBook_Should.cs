using LibrarySystem.Data.Context;
using LibrarySystem.Data.Models;
using LibrarySystem.Services;
using LibrarySystem.Services.Abstract.Contracts;
using LibrarySystem.Services.Exceptions.BookServices;
using LibrarySystem.Services.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace LibrarySystem.Tests.Services.BooksserviceTests
{
    [TestClass]
    public class GetBook_Should
    {
        [TestMethod]
        public void GetBook_WhenTitleIsPassed_ShouldGetBookByTitle()
        {
            // Arrange
            var contexInMemory = new DbContextOptionsBuilder<LibrarySystemContext>()
                .UseInMemoryDatabase(databaseName: "GetBook").Options;

            var validationMock = new Mock<IValidations>();

            string title = "newBook";
            string author = "Author";
            string genre = "Genre";

            var book = new Book
            {
                Id = new Guid(),
                Title = title,
                Author = new Author() { Name = author },
                Genre = new Genre() { GenreName = genre }
            };


            using (var arrangeContext = new LibrarySystemContext(contexInMemory))
            {
                arrangeContext.Books.Add(book);
            }

            // Act
            using (var actContext = new LibrarySystemContext(contexInMemory))
            {
                var unitOfWork = new UnitOfWork(actContext);
                var bookServices = new BooksServices(unitOfWork, validationMock.Object);

                unitOfWork.GetRepo<Book>().Add(book);
                unitOfWork.SaveChanges();

                var fundedBook = bookServices.GetBook(title);

                // Assert
                Assert.AreEqual(title, fundedBook.Title);
                Assert.AreEqual(author, fundedBook.Author);
                Assert.AreEqual(genre, fundedBook.Genre);
            }

        }

        [TestMethod]
        [ExpectedException(typeof(AddBookNullableExeption))]
        public void GetBook_WhenTheBookDontExist_ThrowException()
        {
            // Arrange
            var contexInMemory = new DbContextOptionsBuilder<LibrarySystemContext>()
                .UseInMemoryDatabase(databaseName: "GetBook").Options;

            var validationMock = new Mock<IValidations>();

            string title = "notExistingBook";

            // Act
            using (var actContext = new LibrarySystemContext(contexInMemory))
            {
                var unitOfWork = new UnitOfWork(actContext);
                var bookServices = new BooksServices(unitOfWork, validationMock.Object);

                var fundedBook = bookServices.GetBook(title);
            }
        }
    }
}
