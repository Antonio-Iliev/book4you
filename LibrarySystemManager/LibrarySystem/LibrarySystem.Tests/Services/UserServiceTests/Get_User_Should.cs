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
    public class Get_User_Should
    {       
        [TestMethod]
        public void Return_User_With_Valid_Parameters_IfExists()
        {
            var contextOptions = new DbContextOptionsBuilder<LibrarySystemContext>()
                .UseInMemoryDatabase(databaseName: "Return_User_With_Valid_Parameters_IfExists").Options;

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

        }
    }
}
