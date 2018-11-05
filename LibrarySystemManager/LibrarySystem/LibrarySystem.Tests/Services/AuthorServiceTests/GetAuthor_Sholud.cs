using LibrarySystem.Data.Context;
using LibrarySystem.Data.Models;
using LibrarySystem.Services.Abstract.Contracts;
using LibrarySystem.Services.Exceptions.AuthorServices;
using LibrarySystem.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace LibrarySystem.Tests.Services.AuthorServiceTests
{
    [TestClass]
    public class GetAuthor_Sholud
    {
        [TestMethod]
        public void GetAuthor_WhenMethodIsCall_ShouldReturnAuthor()
        {
            // Arrange
            var contexInMemory = new DbContextOptionsBuilder<LibrarySystemContext>()
                .UseInMemoryDatabase(databaseName: "GetAuthor").Options;

            var validationMock = new Mock<IValidations>();

            string authorName = "Author";
            string book1 = "book1";
            string book2 = "book2";

            var authorBooks = new List<Book>();
            authorBooks.Add(new Book() { Title = book1 });
            authorBooks.Add(new Book() { Title = book2 });

            var author = new Author()
            {
                Name = authorName,
                Books = authorBooks
            };

            using (var arrangeContext = new LibrarySystemContext(contexInMemory))
            {
                arrangeContext.Authors.Add(author);
                arrangeContext.SaveChanges();
            }

            Author result;

            // Act
            using (var actContext = new LibrarySystemContext(contexInMemory))
            {
                var service = new AuthorServices(actContext, validationMock.Object);

                result = service.GetAuthor(authorName);
            }

            // Assert
            Assert.AreEqual(authorName, result.Name);
            Assert.AreEqual(2, result.Books.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(AddAuthorNullableExeption))]
        public void GetAuthor_WhenAuthorDontExist_ThrowException()
        {
            // Arrange
            var contexInMemory = new DbContextOptionsBuilder<LibrarySystemContext>()
                .UseInMemoryDatabase(databaseName: "GetAuthorDontExist").Options;

            var validationMock = new Mock<IValidations>();

            string authorName = "Author";

            // Act
            using (var actContext = new LibrarySystemContext(contexInMemory))
            {
                var service = new AuthorServices(actContext, validationMock.Object);

                service.GetAuthor(authorName);
            }
        }
    }
}
