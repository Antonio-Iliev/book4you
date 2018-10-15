using LibrarySystem.Data.Context;
using LibrarySystem.Data.Models;
using LibrarySystem.Services;
using LibrarySystem.Services.Abstract.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;

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

            var book = new Book
            {
                Title = title
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
                int cont = actContext.Books.Count();
                var testbook = actContext.Books.Find();
                var fundedBook = bookServices.GetBook(title);


                // Assert
                Assert.AreEqual(title, fundedBook.Title);
            }

        }
    }
}
