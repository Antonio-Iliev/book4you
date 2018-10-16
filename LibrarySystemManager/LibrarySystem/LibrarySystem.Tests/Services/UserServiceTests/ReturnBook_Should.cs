using LibrarySystem.Data.Context;
using LibrarySystem.Data.Models;
using LibrarySystem.Services;
using LibrarySystem.Services.Abstract.Contracts;
using LibrarySystem.Services.Exceptions.BookServices;
using LibrarySystem.Services.Exceptions.UserServices;
using LibrarySystem.Services.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;

namespace LibrarySystem.Tests.Services.UserServiceTests
{
    [TestClass]
    public class ReturnBook_Should
    {
        [TestMethod]
        public void ReturnBook_WhenBorrowBook_ShouldReturnViewModel()
        {
            // Arrange
            var contexInMemory = new DbContextOptionsBuilder<LibrarySystemContext>()
                .UseInMemoryDatabase(databaseName: "ReturnBook_WhenBorrowBook_ShouldReturnViewModel").Options;

            var validationMock = new Mock<IValidations>();

            string title = "newBook",
            author = "Author",
            genre = "Genre",
            userFirstName = "newUser",
            userMiddName = "midd",
            userLastName = "Userov",
            userPhone = "888888888";

            DateTime thisTime = DateTime.Now;

            var book = new Book
            {
                Id = new Guid(),
                Title = title,
                Author = new Author() { Name = author },
                Genre = new Genre() { GenreName = genre },
                BooksInStore = 1
            };

            var user = new User
            {
                Id = new Guid(),
                FirstName = userFirstName,
                MiddleName = userMiddName,
                LastName = userLastName,
                AddOnDate = thisTime,
                AddressId = 1,
                Address = new Address
                { StreetAddress = "addres", TownId = 1, Town = new Town { TownName = "Town" } },
                PhoneNumber = userPhone,
                IsDeleted = false,
            };

            using (var arrangeContext = new LibrarySystemContext(contexInMemory))
            {
                arrangeContext.Books.Add(book);
                arrangeContext.Users.Add(user);
                arrangeContext.SaveChanges();
            }

            UserViewModel returnBook;

            // Act
            using (var actContext = new LibrarySystemContext(contexInMemory))
            {
                var unitOfWork = new UnitOfWork(actContext);
                var services = new UsersServices(unitOfWork, validationMock.Object);

                returnBook = services.ReturnBook
                    (userFirstName, userMiddName, userLastName, title);
            }

            // Assert
            StringAssert.Contains(returnBook.FullName, userFirstName);
            StringAssert.Contains(returnBook.FullName, userMiddName);
            StringAssert.Contains(returnBook.FullName, userLastName);
            StringAssert.Contains(returnBook.Phonenumber, userPhone);
            StringAssert.Contains(returnBook.Town, user.Address.Town.TownName);
            StringAssert.Contains(returnBook.Address, user.Address.StreetAddress);
            Assert.AreEqual(thisTime, returnBook.AddedOn);
        }

        [TestMethod]
        [ExpectedException(typeof(UserNullableException))]
        public void ReturnBook_WhenUserNotFound_ThrowException()
        {
            // Arrange
            var contexInMemory = new DbContextOptionsBuilder<LibrarySystemContext>()
                .UseInMemoryDatabase(databaseName: "ReturnBook_WhenUserNotFound_ThrowException").Options;

            var validationMock = new Mock<IValidations>();

            string title = "newBook",
            author = "Author",
            genre = "Genre",
            userFirstName = "newUser",
            userMiddName = "midd",
            userLastName = "Userov";

            var book = new Book
            {
                Id = new Guid(),
                Title = title,
                Author = new Author() { Name = author },
                Genre = new Genre() { GenreName = genre },
                BooksInStore = 1
            };

            using (var arrangeContext = new LibrarySystemContext(contexInMemory))
            {
                arrangeContext.Books.Add(book);
                arrangeContext.SaveChanges();
            }

            // Act
            using (var actContext = new LibrarySystemContext(contexInMemory))
            {
                var unitOfWork = new UnitOfWork(actContext);
                var services = new UsersServices(unitOfWork, validationMock.Object);

                services.ReturnBook(userFirstName, userMiddName, userLastName, title);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(AddBookNullableExeption))]
        public void ReturnBook_WhenBookNotFound_ThrowException()
        {
            // Arrange
            var contexInMemory = new DbContextOptionsBuilder<LibrarySystemContext>()
                .UseInMemoryDatabase(databaseName: "ReturnBook_WhenBookNotFound_ThrowException").Options;

            var validationMock = new Mock<IValidations>();

            string title = "newBook",
            author = "Author",
            genre = "Genre",
            userFirstName = "newUser",
            userMiddName = "midd",
            userLastName = "Userov",
            userPhone = "888888888";

            DateTime thisTime = DateTime.Now;

            var user = new User
            {
                Id = new Guid(),
                FirstName = userFirstName,
                MiddleName = userMiddName,
                LastName = userLastName,
                AddOnDate = thisTime,
                AddressId = 1,
                Address = new Address
                { StreetAddress = "addres", TownId = 1, Town = new Town { TownName = "Town" } },
                PhoneNumber = userPhone,
                IsDeleted = false,
            };

            using (var arrangeContext = new LibrarySystemContext(contexInMemory))
            {
                arrangeContext.Users.Add(user);
                arrangeContext.SaveChanges();
            }

            // Act
            using (var actContext = new LibrarySystemContext(contexInMemory))
            {
                var unitOfWork = new UnitOfWork(actContext);
                var services = new UsersServices(unitOfWork, validationMock.Object);

                services.ReturnBook(userFirstName, userMiddName, userLastName, title);
            }
        }

        [TestMethod]
        public void ReturnBook_WhenBookIsReturned_ShouldAddOneBookToStore()
        {
            // Arrange
            var contexInMemory = new DbContextOptionsBuilder<LibrarySystemContext>()
                .UseInMemoryDatabase(databaseName: "ReturnBook_WhenBookIsReturned_ShouldAddOneBookToStore").Options;

            var validationMock = new Mock<IValidations>();

            string title = "newBook",
            author = "Author",
            genre = "Genre",
            userFirstName = "newUser",
            userMiddName = "midd",
            userLastName = "Userov",
            userPhone = "888888888";

            DateTime thisTime = DateTime.Now;

            int booksInStore = 10;

            var book = new Book
            {
                Id = new Guid(),
                Title = title,
                Author = new Author() { Name = author },
                Genre = new Genre() { GenreName = genre },
                BooksInStore = booksInStore
            };

            var user = new User
            {
                Id = new Guid(),
                FirstName = userFirstName,
                MiddleName = userMiddName,
                LastName = userLastName,
                AddOnDate = thisTime,
                AddressId = 1,
                Address = new Address
                { StreetAddress = "addres", TownId = 1, Town = new Town { TownName = "Town" } },
                PhoneNumber = userPhone,
                IsDeleted = false,
            };

            using (var arrangeContext = new LibrarySystemContext(contexInMemory))
            {
                arrangeContext.Books.Add(book);
                arrangeContext.Users.Add(user);
                arrangeContext.SaveChanges();
            }

            // Act
            using (var actContext = new LibrarySystemContext(contexInMemory))
            {
                var unitOfWork = new UnitOfWork(actContext);
                var services = new UsersServices(unitOfWork, validationMock.Object);

                services.ReturnBook(userFirstName, userMiddName, userLastName, title);
            }

            // Assert
            using (var assertContext = new LibrarySystemContext(contexInMemory))
            {
                var result = assertContext.Books.SingleOrDefault(b => b.Title == title);
                Assert.AreEqual(booksInStore + 1, result.BooksInStore);
            }
        }
    }
}
