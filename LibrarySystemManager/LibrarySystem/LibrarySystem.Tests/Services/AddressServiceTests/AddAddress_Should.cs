using LibrarySystem.Data.Context;
using LibrarySystem.Data.Models;
using LibrarySystem.Services.Exceptions.AddressServices;
using LibrarySystem.Services.Services;
using LibrarySystem.Services.Validations;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibrarySystem.Tests.Services.AddressServiceTests
{
    [TestClass]
    public class AddAddress_Should
    {

        [TestMethod]
        public void Add_Address_ToDataBase()
        {
            var contextOptions = new DbContextOptionsBuilder<LibrarySystemContext>()
                .UseInMemoryDatabase(databaseName: "Add_Address_ToDataBase")
                .Options;
            var validationMock = new Mock<CommonValidations>();
            
            string streetAddress1 = "str test 1";
            string streetAddress2 = "str test 2";

            // Act
            using (var actContext = new LibrarySystemContext(contextOptions))
            {
                var unit = new UnitOfWork(actContext);
                var repo = unit.GetRepo<Address>();

                var service = new AddressService(unit, validationMock.Object);

                service.AddAddress(streetAddress1, new Town());
                service.AddAddress(streetAddress2, new Town());
            }

            // Assert
            using (var assertContext = new LibrarySystemContext(contextOptions))
            {
                var unit = new UnitOfWork(assertContext);
                var repo = unit.GetRepo<Address>();

                var service = new AddressService(unit, validationMock.Object);

                int count = assertContext.Addresses.Count();
                Assert.AreEqual(2, count);
                Assert.AreEqual(streetAddress1, assertContext.Addresses.First().StreetAddress);
            }
        }

        [TestMethod]
        public void Not_Add_IfAddress_Exists()
        {
            var contextOptions = new DbContextOptionsBuilder<LibrarySystemContext>()
                .UseInMemoryDatabase(databaseName: "Not_Add_IfAddress_Exists")
                .Options;
            var validationMock = new Mock<CommonValidations>();

            string streetAddress1 = "str test 1";
            string streetAddress2 = "str test 1";

            // Act
            using (var actContext = new LibrarySystemContext(contextOptions))
            {
                var unit = new UnitOfWork(actContext);
                var repo = unit.GetRepo<Address>();

                var service = new AddressService(unit, validationMock.Object);

                service.AddAddress(streetAddress1, new Town());
                service.AddAddress(streetAddress2, new Town());
            }

            // Assert
            using (var assertContext = new LibrarySystemContext(contextOptions))
            {
                var unit = new UnitOfWork(assertContext);
                var repo = unit.GetRepo<Address>();

                var service = new AddressService(unit, validationMock.Object);

                int count = assertContext.Addresses.Count();
                Assert.AreEqual(1, count);
                Assert.AreEqual(streetAddress1, assertContext.Addresses.First().StreetAddress);
            }

        }
    }
}
