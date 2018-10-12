using LibrarySystem.Data.Context;
using LibrarySystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibrarySystem.Tests.Services.UserServiceTests
{
    [TestClass]
    public class AddUser_Should
    {
        [TestMethod]
        public void Add_User_ToDatabase()
        {
            var contextOptions = new DbContextOptionsBuilder<LibrarySystemContext>()
                .UseInMemoryDatabase(databaseName: "Add_IfUser_Does_Not_Exist").Options;

            var user = new User()
            {
                FirstName = "Ivan",
                MiddleName = "Ivanov",
                LastName = "Ivanov",
                PhoneNumber = "1234567899",
                AddOnDate = DateTime.Now,
                IsDeleted = false,
                AddressId = 1
            };

            // Act
            using (var actContext = new LibrarySystemContext(contextOptions))
            {
                actContext.Users.Add(user);
                actContext.SaveChanges();
            }

            // Assert
            using (var assertContext = new LibrarySystemContext(contextOptions))
            {
                int count = assertContext.Users.Count();
                Assert.AreEqual(1, count);
                Assert.AreEqual(user.FirstName, assertContext.Users.First().FirstName);
            }
        }

        [TestMethod]
        public void Not_Add_IfUser_Exists()
        {
            var contextOptions = new DbContextOptionsBuilder<LibrarySystemContext>()
                .UseInMemoryDatabase(databaseName: "Not_Add_IfUser_Exist").Options;

            var user = new User()
            {
                FirstName = "Ivan",
                MiddleName = "Ivanov",
                LastName = "Ivanov",
                PhoneNumber = "1234567899",
                AddOnDate = DateTime.Now,
                IsDeleted = false,
                AddressId = 1
            };

            // Act
            using (var actContext = new LibrarySystemContext(contextOptions))
            {
                actContext.Users.Add(user);
                actContext.Users.Add(user);
                actContext.SaveChanges();
            }

            // Assert
            using (var assertContext = new LibrarySystemContext(contextOptions))
            {
                int count = assertContext.Users.Count();
                Assert.AreEqual(1, count);
                Assert.AreEqual(user.FirstName, assertContext.Users.First().FirstName);
            }

        }
    }
}
