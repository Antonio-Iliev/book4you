using LibrarySystem.ConsoleClient.Commands;
using LibrarySystem.Services;
using LibrarySystem.Services.Exceptions.UserServices;
using LibrarySystem.Services.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Tests.Commands.BorrowBookCommandTests
{
    [TestClass]
    public class Execute_Should
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidUserServiceParametersExeption))]
        public void Throw_If_ParamethersCount_IsInvalid()
        {
            var parameters = new List<string>() { "Fname", "Mname", "Lname" };
            var userServiceMock = new Mock<IUsersServices>();
            var command = new BorrowBookCommand(userServiceMock.Object);

            command.Execute(parameters);
        }

        [TestMethod]
        public void Call_UserService()
        {
            var parameters = new List<string>() { "Fname", "Mname", "Lname", "BookTitle" };
            string fullName = parameters[0] + ' ' + parameters[1] + ' ' + parameters[2];
            var userServiceMock = new Mock<IUsersServices>();
            var command = new BorrowBookCommand(userServiceMock.Object);

            userServiceMock.Setup(u => u.BorrowBook(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new UserViewModel()
                {
                    FullName = fullName
                });

            command.Execute(parameters);

            userServiceMock.Verify(u => u.BorrowBook(parameters[0], parameters[1], parameters[2], parameters[3]), Times.Once);
        }

        [TestMethod]
        public void Return_SuccessMessage()
        {
            var parameters = new List<string>() { "Fname", "Mname", "Lname", "BookTitle" };
            string fullName = parameters[0] + ' ' + parameters[1] + ' ' + parameters[2];
            var userServiceMock = new Mock<IUsersServices>();
            var command = new BorrowBookCommand(userServiceMock.Object);

            userServiceMock.Setup(u => u.BorrowBook(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new UserViewModel()
                {
                    FullName = fullName
                });

            string message = command.Execute(parameters);


            Assert.AreEqual($"User {fullName} borrow the book {parameters[3]}", message);

        }
    }
}
