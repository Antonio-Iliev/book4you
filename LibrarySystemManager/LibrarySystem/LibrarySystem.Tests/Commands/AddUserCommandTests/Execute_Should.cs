using LibrarySystem.ConsoleClient.Commands;
using LibrarySystem.Services;
using LibrarySystem.Services.Exceptions.UserServices;
using LibrarySystem.Services.Services;
using LibrarySystem.Services.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Tests.Commands.AddUserCommandTests
{
    [TestClass]
    public class Execute_Should
    {

        [TestMethod]
        [ExpectedException(typeof(InvalidUserServiceParametersExeption))]
        public void Throw_If_ParamethersCount_IsInvalid()
        {
            var parameters = new List<string>() { "Fname", "Mname", "Lname", "Address", "Town" };
            var townServiceMock = new Mock<ITownService>();
            var addressServiceMock = new Mock<IAddressService>();
            var userServiceMock = new Mock<IUsersServices>();
            var command = new AddUserCommand(userServiceMock.Object, addressServiceMock.Object, townServiceMock.Object);

            command.Execute(parameters);
        }

        [TestMethod]
        public void Call_TownService_AddTown_Once()
        {
            var parameters = new List<string>() { "Fname", "Mname", "Lname", "0123123123", "Address", "Town" };
            var townServiceMock = new Mock<ITownService>();
            var addressServiceMock = new Mock<IAddressService>();
            var userServiceMock = new Mock<IUsersServices>();
            var command = new AddUserCommand(userServiceMock.Object, addressServiceMock.Object, townServiceMock.Object);

            townServiceMock.Setup(x => x.AddTown(It.IsAny<string>())).Returns(1);
            addressServiceMock.Setup(x => x.AddAddress(It.IsAny<string>(), It.IsAny<int>())).Returns(1);
            userServiceMock.Setup(x => x.AddUser(It.IsAny<string>()
                , It.IsAny<string>()
                , It.IsAny<string>()
                , It.IsAny<string>()
                , It.IsAny<DateTime>()
                , It.IsAny<bool>()
                , It.IsAny<int>()
                )).Returns(new UserViewModel());

            command.Execute(parameters);

            townServiceMock.Verify(s => s.AddTown(parameters[5]), Times.Once());
        }

        [TestMethod]
        public void Call_AddressService_AddAddress_Once()
        {
            var parameters = new List<string>() { "Fname", "Mname", "Lname", "0123123123", "Address", "Town" };
            var townServiceMock = new Mock<ITownService>();
            var addressServiceMock = new Mock<IAddressService>();
            var userServiceMock = new Mock<IUsersServices>();
            var command = new AddUserCommand(userServiceMock.Object, addressServiceMock.Object, townServiceMock.Object);

            townServiceMock.Setup(x => x.AddTown(It.IsAny<string>())).Returns(1);
            addressServiceMock.Setup(x => x.AddAddress(It.IsAny<string>(), It.IsAny<int>())).Returns(1);
            userServiceMock.Setup(x => x.AddUser(It.IsAny<string>()
                , It.IsAny<string>()
                , It.IsAny<string>()
                , It.IsAny<string>()
                , It.IsAny<DateTime>()
                , It.IsAny<bool>()
                , It.IsAny<int>()
                )).Returns(new UserViewModel());

            command.Execute(parameters);

            addressServiceMock.Verify(a => a.AddAddress(parameters[4], 1), Times.Once());
        }

        [TestMethod]
        public void Call_UserService_AddUser_Once()
        {
            var parameters = new List<string>() { "Fname", "Mname", "Lname", "0123123123", "Address", "Town" };
            var townServiceMock = new Mock<ITownService>();
            var addressServiceMock = new Mock<IAddressService>();
            var userServiceMock = new Mock<IUsersServices>();
            var command = new AddUserCommand(userServiceMock.Object, addressServiceMock.Object, townServiceMock.Object);

            townServiceMock.Setup(x => x.AddTown(It.IsAny<string>())).Returns(1);
            addressServiceMock.Setup(x => x.AddAddress(It.IsAny<string>(), It.IsAny<int>())).Returns(1);
            userServiceMock.Setup(x => x.AddUser(It.IsAny<string>()
                , It.IsAny<string>()
                , It.IsAny<string>()
                , It.IsAny<string>()
                , It.IsAny<DateTime>()
                , It.IsAny<bool>()
                , It.IsAny<int>()
                )).Returns(new UserViewModel());

            command.Execute(parameters);

            userServiceMock.Verify(s => s.AddUser(parameters[0], parameters[1], parameters[2], parameters[3], It.IsAny<DateTime>(), false, 1), Times.Once());
        }

        [TestMethod]
        public void Return_SuccessMessage()
        {
            var parameters = new List<string>() { "Fname", "Mname", "Lname", "0123123123", "Address", "Town" };
            var fullName = parameters[0] + ' ' + parameters[1] + ' ' + parameters[2];
            var date = new DateTime(2018, 12, 12);
            var townServiceMock = new Mock<ITownService>();
            var addressServiceMock = new Mock<IAddressService>();
            var userServiceMock = new Mock<IUsersServices>();
            var command = new AddUserCommand(userServiceMock.Object, addressServiceMock.Object, townServiceMock.Object);

            townServiceMock.Setup(x => x.AddTown(It.IsAny<string>())).Returns(1);
            addressServiceMock.Setup(x => x.AddAddress(It.IsAny<string>(), It.IsAny<int>())).Returns(1);
            userServiceMock.Setup(x => x.AddUser(It.IsAny<string>()
                , It.IsAny<string>()
                , It.IsAny<string>()
                , It.IsAny<string>()
                , It.IsAny<DateTime>()
                , It.IsAny<bool>()
                , It.IsAny<int>()
                )).Returns(new UserViewModel()
                {
                    FullName = fullName,
                    AddedOn = date
                });

            var message = command.Execute(parameters);

            userServiceMock.Verify(s => s.AddUser(parameters[0], parameters[1], parameters[2], parameters[3], It.IsAny<DateTime>(), false, 1), Times.Once());

            Assert.AreEqual($"New user {fullName} was added successfully on {date}.", message);
        }
    }
}
