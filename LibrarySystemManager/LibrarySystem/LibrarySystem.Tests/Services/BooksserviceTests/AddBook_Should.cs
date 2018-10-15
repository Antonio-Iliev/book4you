using LibrarySystem.Data.Context;
using LibrarySystem.Services;
using LibrarySystem.Services.Abstract.Contracts;
using LibrarySystem.Services.Exceptions.BookServiceExeptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;

namespace LibrarySystem.Tests.Services.BooksserviceTests
{
    [TestClass]
    public class AddBook_Should
    {
        [TestMethod]
        public void AddBook_WhenAddBookToDatabase_ShouldAddBookToDatabase()
        {
            // Arrange
            var contexInMemory = new DbContextOptionsBuilder<LibrarySystemContext>()
                .UseInMemoryDatabase(databaseName: "AddBookToDatabase").Options;

            var validationMock = new Mock<IValidations>();

            string title = "newBook";
            int genreId = 1;
            int authorId = 2;
            int booksInStor = 3;

            // Act
            using (var actContext = new LibrarySystemContext(contexInMemory))
            {
                var unitOfWork = new UnitOfWork(actContext);
                var bookServices = new BooksServices(unitOfWork, validationMock.Object);

                bookServices
                    .AddBook(title, genreId, authorId, booksInStor.ToString());
            }

            // Assert
            using (var assertContex = new LibrarySystemContext(contexInMemory))
            {
                int cont = assertContex.Books.Count();
                Assert.AreEqual(1, cont);
                Assert.AreEqual(title, assertContex.Books.First().Title);
                Assert.AreEqual(genreId, assertContex.Books.First().GenreId);
                Assert.AreEqual(authorId, assertContex.Books.First().AuthorId);
                Assert.AreEqual(booksInStor, assertContex.Books.First().BooksInStore);
            }
        }

        [TestMethod]
        public void AddBook_WhenBookExist_ShouldAddItToBooksInStor()
        {
            // Arrange
            var contexInMemory = new DbContextOptionsBuilder<LibrarySystemContext>()
                .UseInMemoryDatabase(databaseName: "WhenBookExist").Options;

            var validationMock = new Mock<IValidations>();

            string title = "newBook";
            int genreId = 1;
            int authorId = 2;
            int booksInStor = 3;

            // Act
            using (var actContext = new LibrarySystemContext(contexInMemory))
            {
                var unitOfWork = new UnitOfWork(actContext);
                var bookServices = new BooksServices(unitOfWork, validationMock.Object);

                bookServices
                    .AddBook(title, genreId, authorId, booksInStor.ToString());
                bookServices
                    .AddBook(title, genreId, authorId, booksInStor.ToString());
            }

            // Assert
            using (var assertContex = new LibrarySystemContext(contexInMemory))
            {
                Assert.AreEqual(title, assertContex.Books.First().Title);
                Assert.AreEqual(booksInStor + booksInStor, assertContex.Books.First().BooksInStore);
            }
        }

        [TestMethod]
        [DataRow("Test")]
        [DataRow("1.5")]
        [DataRow("1,8")]
        [ExpectedException(typeof(InvalidBookServiceParametersExeption))]
        public void AddBook_WhenPassInvalidNumber_ShouldThrowException(string booksInStor)
        {
            // Arrange
            var contexInMemory = new DbContextOptionsBuilder<LibrarySystemContext>()
                .UseInMemoryDatabase(databaseName: "ThrowException").Options;

            var validationMock = new Mock<IValidations>();

            string title = "newBook";
            int genreId = 1;
            int authorId = 2;

            // Act
            using (var actContext = new LibrarySystemContext(contexInMemory))
            {
                var unitOfWork = new UnitOfWork(actContext);
                var bookServices = new BooksServices(unitOfWork, validationMock.Object);

                bookServices
                    .AddBook(title, genreId, authorId, booksInStor);
            }
        }

        [TestMethod]
        [DataRow("0")]
        [DataRow("-1")]
        [ExpectedException(typeof(InvalidBookServiceParametersExeption))]
        public void AddBook_WhenPassNegativNumber_ShouldThrowException(string booksInStor)
        {
            // Arrange
            var contexInMemory = new DbContextOptionsBuilder<LibrarySystemContext>()
                .UseInMemoryDatabase(databaseName: "ThrowException").Options;

            var validationMock = new Mock<IValidations>();

            string title = "newBook";
            int genreId = 1;
            int authorId = 2;

            // Act
            using (var actContext = new LibrarySystemContext(contexInMemory))
            {
                var unitOfWork = new UnitOfWork(actContext);
                var bookServices = new BooksServices(unitOfWork, validationMock.Object);

                bookServices
                    .AddBook(title, genreId, authorId, booksInStor.ToString());
            }

        }
    }
}
