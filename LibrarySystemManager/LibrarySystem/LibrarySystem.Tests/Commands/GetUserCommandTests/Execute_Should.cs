using LibrarySystem.ConsoleClient.Commands;
using LibrarySystem.Data.Models;
using LibrarySystem.Services;
using LibrarySystem.Services.Exceptions.UserServices;
using LibrarySystem.Services.Services;
using LibrarySystem.Services.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Tests.Commands.GetUserCommandTests
{
    [TestClass]
    public class Execute_Should
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidUserServiceParametersExeption))]
        public void Throw_If_ParamethersCount_IsInvalid()
        {
            var parameters = new List<string>() { "Fname", "Mname"};
            var townServiceMock = new Mock<ITownService>();
            var addressServiceMock = new Mock<IAddressService>();
            var userServiceMock = new Mock<IUsersServices>();
            var command = new GetUserCommand(userServiceMock.Object, addressServiceMock.Object, townServiceMock.Object);

            command.Execute(parameters);
        }

        [TestMethod]
        public void Call_UserService_GetUser_Once()
        {
            var parameters = new List<string>() { "Fname", "Mname", "Lname" };
            var date = new DateTime(2018, 12, 12);
            string phone = "089888888";
            string address = "test address";
            string town = "test town";
            var townServiceMock = new Mock<ITownService>();
            var addressServiceMock = new Mock<IAddressService>();
            var userServiceMock = new Mock<IUsersServices>();
            var command = new GetUserCommand(userServiceMock.Object, addressServiceMock.Object, townServiceMock.Object);

            userServiceMock.Setup(x => x.GetUser(It.IsAny<string>()
                , It.IsAny<string>()
                , It.IsAny<string>()))
                .Returns(new UserViewModel()
                {
                    FullName = parameters[0] + ' ' + parameters[1] + ' ' + parameters[2],
                    Phonenumber = phone,
                    AddedOn = date,
                    Address = address,
                    Town = town,
                    UserBooks = new List<UsersBooks>()
                });

            command.Execute(parameters);

            userServiceMock.Verify(s => s.GetUser(parameters[0], parameters[1], parameters[2]), Times.Once());
        }

        [TestMethod]
        public void Return_SuccessMessage()
        {
            var parameters = new List<string>() { "Fname", "Mname", "Lname" };
            var fullName = parameters[0] + ' ' + parameters[1] + ' ' + parameters[2];
            var date = new DateTime(2018, 12, 12);
            string phone = "089888888";
            string address = "test address";
            string town = "test town";
            var townServiceMock = new Mock<ITownService>();
            var addressServiceMock = new Mock<IAddressService>();
            var userServiceMock = new Mock<IUsersServices>();
            var command = new GetUserCommand(userServiceMock.Object, addressServiceMock.Object, townServiceMock.Object);

            userServiceMock.Setup(x => x.GetUser(It.IsAny<string>()
                , It.IsAny<string>()
                , It.IsAny<string>()))
                .Returns(new UserViewModel()
                {
                    FullName = parameters[0] + ' ' + parameters[1] + ' ' + parameters[2],
                    Phonenumber = phone,
                    AddedOn = date,
                    Address = address,
                    Town = town,
                    UserBooks = new List<UsersBooks>()
                });

            var message = command.Execute(parameters);
            
            StringAssert.Contains(message, fullName);
            StringAssert.Contains(message, phone);
            StringAssert.Contains(message, date.ToString());
            StringAssert.Contains(message, address);
            StringAssert.Contains(message, town);
        }
    }
}
