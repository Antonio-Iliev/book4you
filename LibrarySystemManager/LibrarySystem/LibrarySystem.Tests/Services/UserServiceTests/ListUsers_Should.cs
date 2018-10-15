using LibrarySystem.Data.Context;
using LibrarySystem.Services;
using LibrarySystem.Services.Exceptions.UserServices;
using LibrarySystem.Services.Validations;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Tests.Services.UserServiceTests
{
    [TestClass]
    public class ListUsers_Should
    {
        [TestMethod]
        [ExpectedException(typeof(UserNullableException))]
        public void Throw_IfNoUsers()
        {
            var contextOptions = new DbContextOptionsBuilder<LibrarySystemContext>()
                .UseInMemoryDatabase(databaseName: "Throw_IfNoUsers").Options;
            
            // Act
            using (var actContext = new LibrarySystemContext(contextOptions))
            {
                var unit = new UnitOfWork(actContext);
                var validationMock = new Mock<CommonValidations>();

                var service = new UsersServices(unit, validationMock.Object);

                service.ListUsers(true);
            }

        }
    }
}
