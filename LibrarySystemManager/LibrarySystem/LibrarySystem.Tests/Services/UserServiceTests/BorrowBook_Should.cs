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
    public class BorrowBook_Should
    {

        [TestMethod]
        public void BorrowBook_WhenBorrowBook_ShouldReturnViewModel()
        {
            // Arrange
            var contexInMemory = new DbContextOptionsBuilder<LibrarySystemContext>()
                .UseInMemoryDatabase(databaseName: "BorrowBook_ShouldReturnViewModel").Options;

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

            UserViewModel borrow;

            // Act
            using (var actContext = new LibrarySystemContext(contexInMemory))
            {
                var unitOfWork = new UnitOfWork(actContext);
                var services = new UsersServices(unitOfWork, validationMock.Object);

                borrow = services.BorrowBook
                    (userFirstName, userMiddName, userLastName, title);
            }

            // Assert
            StringAssert.Contains(borrow.FullName, userFirstName);
            StringAssert.Contains(borrow.FullName, userMiddName);
            StringAssert.Contains(borrow.FullName, userLastName);
            StringAssert.Contains(borrow.Phonenumber, userPhone);
            StringAssert.Contains(borrow.Town, user.Address.Town.TownName);
            StringAssert.Contains(borrow.Address, user.Address.StreetAddress);
            Assert.AreEqual(thisTime, borrow.AddedOn);
        }

        [TestMethod]
        public void BorrowBook_WhenBorrowBook_ShouldAddBookToUser()
        {
            // Arrange
            var contexInMemory = new DbContextOptionsBuilder<LibrarySystemContext>()
                .UseInMemoryDatabase(databaseName: "BorrowBook_ShouldAddBookToUser").Options;

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

            UserViewModel borrow;

            // Act
            using (var actContext = new LibrarySystemContext(contexInMemory))
            {
                var unitOfWork = new UnitOfWork(actContext);
                var services = new UsersServices(unitOfWork, validationMock.Object);

                borrow = services.BorrowBook
                    (userFirstName, userMiddName, userLastName, title);
            }

            // Assert
            using (var assertContext = new LibrarySystemContext(contexInMemory))
            {
                var findUser = assertContext.Users.Select(u => u)
                      .Include(u => u.UsersBooks)
                    .ThenInclude(ub => ub.Book)
                    .SingleOrDefault
                    (u => u.FirstName == userFirstName && u.LastName == userLastName);

                var userBorrowBook = findUser.UsersBooks.SingleOrDefault(b => b.Book.Title == title);

                Assert.IsNotNull(userBorrowBook);
                Assert.AreEqual(title, userBorrowBook.Book.Title);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(UserNullableException))]
        public void BorrowBook_WhenUserNotFound_ThrowException()
        {
            // Arrange
            var contexInMemory = new DbContextOptionsBuilder<LibrarySystemContext>()
                .UseInMemoryDatabase(databaseName: "WhenUserNotFound_ThrowException").Options;

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

                services.BorrowBook(userFirstName, userMiddName, userLastName, title);
            }
        }


        [TestMethod]
        [ExpectedException(typeof(AddBookNullableExeption))]
        public void BorrowBook_WhenBookNotFound_ThrowException()
        {
            // Arrange
            var contexInMemory = new DbContextOptionsBuilder<LibrarySystemContext>()
                .UseInMemoryDatabase(databaseName: "WhenBookNotFound_ThrowException").Options;

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

                services.BorrowBook(userFirstName, userMiddName, userLastName, title);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(AddBookNullableExeption))]
        public void BorrowBook_WhenNotBooksInStore_ShouldThrowException()
        {
            // Arrange
            var contexInMemory = new DbContextOptionsBuilder<LibrarySystemContext>()
                .UseInMemoryDatabase(databaseName: "WhenNotBooksInStore_ShouldThrowException").Options;

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
                BooksInStore = 0
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

                services.BorrowBook(userFirstName, userMiddName, userLastName, title);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(AddBookNullableExeption))]
        public void BorrowBook_WhenUserAlreadyBorrowBook_ShouldThrowException()
        {
            // Arrange
            var contexInMemory = new DbContextOptionsBuilder<LibrarySystemContext>()
                .UseInMemoryDatabase(databaseName: "_WhenUserAlreadyBorrowBook_ShouldThrowException").Options;

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

            // Act
            using (var actContext = new LibrarySystemContext(contexInMemory))
            {
                var unitOfWork = new UnitOfWork(actContext);
                var services = new UsersServices(unitOfWork, validationMock.Object);

                services.BorrowBook(userFirstName, userMiddName, userLastName, title);

                // Second add
                services.BorrowBook(userFirstName, userMiddName, userLastName, title);
            }
        }

        [TestMethod]
        public void BorrowBook_WhenBookIsBorrowed_ShouldExtractOneBookFromStore()
        {
            // Arrange
            var contexInMemory = new DbContextOptionsBuilder<LibrarySystemContext>()
                .UseInMemoryDatabase(databaseName: "ShouldExtractOneBookFromStore").Options;

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

                services.BorrowBook(userFirstName, userMiddName, userLastName, title);
            }

            // Assert
            using (var assertContext = new LibrarySystemContext(contexInMemory))
            {
                var result = assertContext.Books.SingleOrDefault(b => b.Title == title);
                Assert.AreEqual(booksInStore - 1, result.BooksInStore);
            }
        }
    }
}
