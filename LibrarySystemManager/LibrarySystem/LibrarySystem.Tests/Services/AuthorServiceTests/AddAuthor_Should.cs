using LibrarySystem.Data.Context;
using LibrarySystem.Data.Models;
using LibrarySystem.Services.Abstract.Contracts;
using LibrarySystem.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;

namespace LibrarySystem.Tests.Services.AuthorServiceTests
{
    [TestClass]
    public class AddAuthor_Should
    {
        [TestMethod]
        public void AddAuthor_WhenAuthorDontExist_ShouldAddToDatabase()
        {
            // Arrange
            var contexInMemory = new DbContextOptionsBuilder<LibrarySystemContext>()
                .UseInMemoryDatabase(databaseName: "AddAuthor").Options;

            var validationMock = new Mock<IValidations>();

            string author = "newAuthor";

            // Act
            using (var actContext = new LibrarySystemContext(contexInMemory))
            {
                var unit = new UnitOfWork(actContext);

                var service = new AuthorServices(unit, validationMock.Object);

                var result = service.AddAuthor(author);
            }

            // Assert
            using (var assertContex = new LibrarySystemContext(contexInMemory))
            {
                int cont = assertContex.Authors.Count();
                Assert.AreEqual(1, cont);
                Assert.AreEqual(author, assertContex.Authors.First().Name);
            }
        }

        [TestMethod]
        public void AddAuthor_WhenAuthorExist_ShouldReturnAddToDatabase()
        {
            // Arrange
            var contexInMemory = new DbContextOptionsBuilder<LibrarySystemContext>()
                .UseInMemoryDatabase(databaseName: "AddAuthorExist").Options;

            var validationMock = new Mock<IValidations>();

            string author = "newAuthor";

            Author existingAuthor = new Author()
            {
                Id = 1,
                Name = author
            };
            int result;

            // Act
            using (var actContext = new LibrarySystemContext(contexInMemory))
            {
                var unit = new UnitOfWork(actContext);
                var test = actContext.Authors.Add(existingAuthor).Entity;
                actContext.SaveChanges();

                var service = new AuthorServices(unit, validationMock.Object);
                result = service.AddAuthor(author);
            }

            // Assert
            using (var assertContext = new LibrarySystemContext(contexInMemory))
            {
                var toAssert = assertContext.Authors
                    .SingleOrDefault(a => a.Name == author);

                int count = assertContext.Authors.Count();

                Assert.AreEqual(1, count);
                Assert.AreEqual(toAssert.Id, result);
            }
        }
    }
}