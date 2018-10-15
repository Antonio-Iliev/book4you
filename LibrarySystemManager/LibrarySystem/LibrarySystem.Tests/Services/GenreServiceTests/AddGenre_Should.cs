using LibrarySystem.Data.Context;
using LibrarySystem.Data.Models;
using LibrarySystem.Services.Abstract.Contracts;
using LibrarySystem.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibrarySystem.Tests.Services.GenreServiceTests
{
    [TestClass]
    public class AddGenre_Should
    {
        [TestMethod]
        public void AddGenre_WhenMethodIsCall_ShouldReturnGenre()
        {

            // Arrange
            var contexInMemory = new DbContextOptionsBuilder<LibrarySystemContext>()
                .UseInMemoryDatabase(databaseName: "AddGenre").Options;

            var validationMock = new Mock<IValidations>();

            string genre = "newGenre";

            // Act
            using (var actContext = new LibrarySystemContext(contexInMemory))
            {
                var unit = new UnitOfWork(actContext);

                var service = new GenreServices(unit, validationMock.Object);
                var result = service.AddGenre(genre);
            }

            // Assert
            using (var assertContex = new LibrarySystemContext(contexInMemory))
            {
                int cont = assertContex.Genres.Count();
                Assert.AreEqual(1, cont);
                Assert.AreEqual(genre, assertContex.Genres.First().GenreName);
            }
        }

        [TestMethod]
        public void AddGenre_WhenAuthorExist_ShouldReturnAddToDatabase()
        {
            // Arrange
            var contexInMemory = new DbContextOptionsBuilder<LibrarySystemContext>()
                .UseInMemoryDatabase(databaseName: "AddGenreExist").Options;

            var validationMock = new Mock<IValidations>();

            string genre = "newAuthor";

            var existingGenre= new Genre()
            {
                Id = 1,
                GenreName = genre
            };
            int result;

            // Act
            using (var actContext = new LibrarySystemContext(contexInMemory))
            {
                var unit = new UnitOfWork(actContext);
                var test = actContext.Genres.Add(existingGenre).Entity;
                actContext.SaveChanges();

                var service = new GenreServices(unit, validationMock.Object);
                result = service.AddGenre(genre);
            }

            // Assert
            using (var assertContext = new LibrarySystemContext(contexInMemory))
            {
                var toAssert = assertContext.Genres
                    .SingleOrDefault(g => g.GenreName == genre);

                int count = assertContext.Genres.Count();

                Assert.AreEqual(1, count);
                Assert.AreEqual(toAssert.Id, result);
            }
        }
    }
}
