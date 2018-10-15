using LibrarySystem.Data.Context;
using LibrarySystem.Services;
using LibrarySystem.Services.Exceptions.UserServices;
using LibrarySystem.Services.Services;
using LibrarySystem.Services.Validations;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;

namespace LibrarySystem.Tests.Services.UserServiceTests
{
    [TestClass]
    public class RemoveUser_Should
    {
        [TestMethod]
        public void Remove_User_FromDatabase_IfUser_Exists()
        {
            //Arrange
            var contextOptions = new DbContextOptionsBuilder<LibrarySystemContext>()
                .UseInMemoryDatabase(databaseName: "Remove_User_FromDatabase")
                .Options;

            string firstName = "Ivan",
                middleName = "Ivanov",
                lastName = "Ivanov",
                phoneNumber = "1234567899";
            DateTime addOnDate = DateTime.Now;
            bool isDeleted = false;
            var validationMock = new Mock<CommonValidations>();

            using (var actContext = new LibrarySystemContext(contextOptions))
            {
                var unit = new UnitOfWork(actContext);

                var townService = new TownService(unit, validationMock.Object);
                var addressService = new AddressService(unit, validationMock.Object);
                var userService = new UsersServices(unit, validationMock.Object);

                var town = townService.AddTown("test");
                var address = addressService.AddAddress("test address", town);

                userService.AddUser(firstName, middleName, lastName, phoneNumber, addOnDate, isDeleted, address);
            }
            using (var assertContext = new LibrarySystemContext(contextOptions))
            {
                var unit = new UnitOfWork(assertContext);
                var userService = new UsersServices(unit, validationMock.Object);
                
                //Act
                userService.RemoveUser(firstName, middleName, lastName);
                
                // Assert
                Assert.AreEqual(true, assertContext.Users.First().IsDeleted);
            }
        }
        [TestMethod]
        [ExpectedException(typeof(UserNullableException))]
        public void Throw_Exeption_IfUser_DoesNot_Exist()
        {
            //Arrange
            var contextOptions = new DbContextOptionsBuilder<LibrarySystemContext>()
                .UseInMemoryDatabase(databaseName: "Remove_User_FromDatabase").Options;

            using (var actContext = new LibrarySystemContext(contextOptions))
            {
                var unit = new UnitOfWork(actContext);
                var validationMock = new Mock<CommonValidations>();
                var userService = new UsersServices(unit, validationMock.Object);

                //Act & Assert
                userService.RemoveUser("hjasmnzx", "iqdkwjsan", "kjasn");
            }
        }
    }
}
