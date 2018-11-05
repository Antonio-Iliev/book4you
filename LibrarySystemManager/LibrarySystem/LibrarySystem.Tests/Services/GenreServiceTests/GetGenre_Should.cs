using LibrarySystem.Data.Context;
using LibrarySystem.Data.Models;
using LibrarySystem.Services.Abstract.Contracts;
using LibrarySystem.Services.Exceptions.GenreServices;
using LibrarySystem.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace LibrarySystem.Tests.Services.GenreServiceTests
{
    [TestClass]
    public class GetGenre_Should
    {
        [TestMethod]
        public void GetGenre_WhenMethodIsCall_ShouldReturnGenre()
        {
            // Arrange
            var contexInMemory = new DbContextOptionsBuilder<LibrarySystemContext>()
                .UseInMemoryDatabase(databaseName: "GetGenre").Options;

            var validationMock = new Mock<IValidations>();

            string genreName = "Genre";
            string book1 = "book1";
            string book2 = "book2";

            var authorBooks = new List<Book>();
            authorBooks.Add(new Book() { Title = book1 });
            authorBooks.Add(new Book() { Title = book2 });

            var genre = new Genre()
            {
                Id = 1,
                GenreName = genreName,
                Books = authorBooks
            };

            using (var arrangeContext = new LibrarySystemContext(contexInMemory))
            {
                arrangeContext.Genres.Add(genre);
                arrangeContext.SaveChanges();
            }


            // Act
            using (var actContext = new LibrarySystemContext(contexInMemory))
            {
                var service = new GenreServices(actContext, validationMock.Object);

                service.GetGenre(genreName);
            }

            // Assert
            using (var assertContext = new LibrarySystemContext(contexInMemory))
            {
                Assert.AreEqual(genreName, assertContext.Genres.FirstOrDefault().GenreName);
                Assert.AreEqual(2, assertContext.Books.Where(b => b.Genre.GenreName == genreName).Count());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(AddGenreNullableExeption))]
        public void GetGenre_WhenGenreDontExist_ThrowException()
        {
            // Arrange
            var contexInMemory = new DbContextOptionsBuilder<LibrarySystemContext>()
                .UseInMemoryDatabase(databaseName: "GetGenreDontExist").Options;

            var validationMock = new Mock<IValidations>();

            string genreName = "Genre";

            // Act
            using (var actContext = new LibrarySystemContext(contexInMemory))
            {
                var service = new GenreServices(actContext, validationMock.Object);

                service.GetGenre(genreName);
            }
        }
    }
}
