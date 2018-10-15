using LibrarySystem.Data.Context;
using LibrarySystem.Services;
using LibrarySystem.Services.Exceptions.UserServices;
using LibrarySystem.Services.Services;
using LibrarySystem.Services.Validations;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace LibrarySystem.Tests.Services.UserServiceTests
{
    [TestClass]
    public class GetUser_Should
    {
        [TestMethod]
        public void Get_User_FromDatabase_IfUser_Exists()
        {
            //Arrange
            var contextOptions = new DbContextOptionsBuilder<LibrarySystemContext>()
                .UseInMemoryDatabase(databaseName: "Get_User_FromDatabase")
                .Options;

            string firstName = "Ivan",
                middleName = "Ivanov",
                lastName = "Ivanov",
                phoneNumber = "1234567899";
            DateTime addOnDate = DateTime.Now;
            bool isDeleted = false;

            string fullName = firstName + " " + middleName + " " + lastName;

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
                var getUser = userService.GetUser(firstName, middleName, lastName);
                //Assert
                Assert.AreEqual(fullName, getUser.FullName);
            }
        }
        [TestMethod]
        [ExpectedException(typeof(UserNullableException))]
        public void Throw_Exeption_IfUser_IsDeleted()
        {
            //Arrange
            var contextOptions = new DbContextOptionsBuilder<LibrarySystemContext>()
                .UseInMemoryDatabase(databaseName: "Get_User_FromDatabase")
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
                var userService = new UsersServices(unit,validationMock.Object);
                var town = townService.AddTown("test");
                var address = addressService.AddAddress("test address", town);

                //Act
                userService.AddUser(firstName, middleName, lastName, phoneNumber, addOnDate, isDeleted, address);
                userService.RemoveUser(firstName, middleName, lastName);
                //Assert
                userService.GetUser(firstName, middleName, lastName);
            }
        }
        [TestMethod]
        [ExpectedException(typeof(UserNullableException))]
        public void Throw_Exeption_IfUser_DoesNot_Exist()
        {
            //Arrange
            var contextOptions = new DbContextOptionsBuilder<LibrarySystemContext>()
                .UseInMemoryDatabase(databaseName: "Get_User_FromDatabase").Options;

            using (var actContext = new LibrarySystemContext(contextOptions))
            {
                var unit = new UnitOfWork(actContext);
                var validationMock = new Mock<CommonValidations>();
                var userService = new UsersServices(unit, validationMock.Object);
                
                //Act & Assert
                userService.GetUser("hjasmnzx", "iqdkwjsan", "kjasn");
            }
        }
    }
}
