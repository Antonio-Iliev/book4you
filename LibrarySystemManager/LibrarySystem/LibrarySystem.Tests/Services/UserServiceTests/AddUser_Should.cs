using LibrarySystem.Data.Context;
using LibrarySystem.Data.Models;
using LibrarySystem.Services;
using LibrarySystem.Services.Exceptions.UserServices;
using LibrarySystem.Services.Services;
using LibrarySystem.Services.Validations;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
                .UseInMemoryDatabase(databaseName: "Add_User_ToDatabase")
                .Options;

            string firstName = "Ivan",
                middleName = "Ivanov",
                lastName = "Ivanov",
                phoneNumber = "1234567899";
            DateTime addOnDate = DateTime.Now;
            bool isDeleted = false;

            using (var actContext = new LibrarySystemContext(contextOptions))
            {
                var unit = new UnitOfWork(actContext);
                var validationMock = new Mock<CommonValidations>();

                var townService = new TownService(unit, new CommonValidations());
                var addressService = new AddressService(unit, new CommonValidations());
                var userService = new UsersServices(unit, new CommonValidations());

                var town = townService.AddTown("test");
                var address = addressService.AddAddress("test address", town);

                userService.AddUser(firstName, middleName, lastName, phoneNumber, addOnDate, isDeleted, address);
            }

            // Assert
            using (var assertContext = new LibrarySystemContext(contextOptions))
            {
                int count = assertContext.Users.Count();
                Assert.AreEqual(1, count);
                Assert.AreEqual(firstName, assertContext.Users.First().FirstName);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(UserNullableException))]
        public void Not_Add_IfUser_Exists()
        {
            var contextOptions = new DbContextOptionsBuilder<LibrarySystemContext>()
                .UseInMemoryDatabase(databaseName: "Not_Add_IfUser_Exists").Options;

            string firstName = "Ivan",
                middleName = "Ivanov",
                lastName = "Ivanov",
                phoneNumber = "1234567899";
            DateTime addOnDate = DateTime.Now;
            bool isDeleted = false;

            using (var actContext = new LibrarySystemContext(contextOptions))
            {
                var unit = new UnitOfWork(actContext);
                var validationMock = new Mock<CommonValidations>();

                var townService = new TownService(unit, new CommonValidations());
                var addressService = new AddressService(unit, new CommonValidations());
                var userService = new UsersServices(unit, new CommonValidations());

                var town = townService.AddTown("test");
                var address = addressService.AddAddress("test address", town);

                userService.AddUser(firstName, middleName, lastName, phoneNumber, addOnDate, isDeleted, address);
                userService.AddUser(firstName, middleName, lastName, phoneNumber, addOnDate, isDeleted, address);
            }           
        }
    }
}
