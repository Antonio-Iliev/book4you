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
    public class UpdateUserPhone_Should
    {
        [TestMethod]
        public void Change_User_Phone_IfUser_Exists()
        {
            //Arrange
            var contextOptions = new DbContextOptionsBuilder<LibrarySystemContext>()
                .UseInMemoryDatabase(databaseName: "Change_User_Phone")
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
                userService.UpdateUserPhone(firstName, middleName, lastName, "22222222");

                //Assert
                Assert.AreEqual("22222222", assertContext.Users.First().PhoneNumber);
            }
        }
        [TestMethod]
        [ExpectedException(typeof(UserNullableException))]
        public void Throw_Exeption_IfUser_IsDeleted()
        {
            //Arrange
            var contextOptions = new DbContextOptionsBuilder<LibrarySystemContext>()
                .UseInMemoryDatabase(databaseName: "Throw_Exeption_IfUser_IsDeleted")
                .Options;
            string firstName = "Ivan1",
                middleName = "Ivanov1",
                lastName = "Ivanov1",
                phoneNumber = "1234567899";
            DateTime addOnDate = DateTime.Now;
            bool isDeleted = false;
            var validationMock = new Mock<CommonValidations>();

            using (var actContext = new LibrarySystemContext(contextOptions))
            {
                var unit = new UnitOfWork(actContext);
                var townService = new TownService(unit,validationMock.Object);
                var addressService = new AddressService(unit, validationMock.Object);
                var userService = new UsersServices(unit, validationMock.Object);
                var town = townService.AddTown("test");
                var address = addressService.AddAddress("test address", town);
                userService.AddUser(firstName, middleName, lastName, phoneNumber, addOnDate, isDeleted, address);
                userService.RemoveUser(firstName, middleName, lastName);

                //Act & Assert
                userService.UpdateUserPhone(firstName, middleName, lastName, "1111111111");
            }
        }
        [TestMethod]
        [ExpectedException(typeof(UserNullableException))]
        public void Throw_Exeption_IfUser_DoesNot_Exist()
        {
            //Arrange
            var contextOptions = new DbContextOptionsBuilder<LibrarySystemContext>()
                .UseInMemoryDatabase(databaseName: "Throw_Exeption_IfUser_DoesNot_Exist").Options;

            using (var actContext = new LibrarySystemContext(contextOptions))
            {
                var unit = new UnitOfWork(actContext);
                var validationMock = new Mock<CommonValidations>();
                var userService = new UsersServices(unit, validationMock.Object);

                //Act & Assert
                userService.UpdateUserPhone("hjasmnzx", "iqdkwjsan", "kjasn", "111112222");
            }
        }
    }
}
