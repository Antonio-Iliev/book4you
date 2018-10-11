using LibrarySystem.Data.Context;
using LibrarySystem.Data.Contracts;
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
        public void AddAddress_ToDataBase_IfNotAlready()
        {
            var options = new DbContextOptionsBuilder<LibrarySystemContext>()
                   .UseInMemoryDatabase(databaseName: "AddAddress_ToDataBase")
                   .Options;

            string street = "test street";
            var town = new Town() { TownName = "Dupnitsa" };
            var validationMock = new Mock<CommonValidations>();

            using (var context = new LibrarySystemContext(options))
            {
                var service = new AddressService(context, validationMock.Object);                
                service.AddAddress(street, town);
            }

            using (var context = new LibrarySystemContext(options))
            {
                Assert.AreEqual(1, context.Addresses.Count());
                Assert.AreEqual(street, context.Addresses.Single().StreetAddress);
            }
        }

        [TestMethod]
        public void Return_AddressFromDataBase_IfExists()
        {
            var options = new DbContextOptionsBuilder<LibrarySystemContext>()
                   .UseInMemoryDatabase(databaseName: "AddAddress_ToDataBase")
                   .Options;

            string street = "test street";
            var town = new Town() { TownName = "Dupnitsa" };
            var validationMock = new Mock<CommonValidations>();
            Address address;

            using (var context = new LibrarySystemContext(options))
            {
                var service = new AddressService(context, validationMock.Object);
                service.AddAddress(street, town);
                address = service.AddAddress(street, town);
            }

            using (var context = new LibrarySystemContext(options))
            {
                Assert.AreEqual(1, context.Addresses.Count());
                Assert.AreEqual(street, address.StreetAddress);
            }
        }
    }
}
