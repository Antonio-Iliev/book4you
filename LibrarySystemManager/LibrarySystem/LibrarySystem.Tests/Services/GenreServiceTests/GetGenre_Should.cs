using LibrarySystem.Data.Context;
using LibrarySystem.Data.Models;
using LibrarySystem.Services.Abstract.Contracts;
using LibrarySystem.Services.Exceptions.GenreServices;
using LibrarySystem.Services.Services;
using LibrarySystem.Services.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace LibrarySystem.Tests.Services.AddressServiceTests
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

            GenreViewModel result;

            // Act
            using (var actContext = new LibrarySystemContext(contexInMemory))
            {
                var unitOfWork = new UnitOfWork(actContext);
                var service = new GenreServices(unitOfWork, validationMock.Object);

                result = service.GetGenre(genreName);
            }

            // Assert
            Assert.AreEqual(genreName, result.GenreName);
            Assert.AreEqual(2, result.BooksByGenre.Count);
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
                var unitOfWork = new UnitOfWork(actContext);
                var service = new GenreServices(unitOfWork, validationMock.Object);

                service.GetGenre(genreName);
            }
        }
    }
}
