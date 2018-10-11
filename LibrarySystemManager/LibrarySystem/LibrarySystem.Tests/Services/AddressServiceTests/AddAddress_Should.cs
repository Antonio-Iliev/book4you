using LibrarySystem.Data.Context;
using LibrarySystem.Data.Contracts;
using LibrarySystem.Data.Models;
using LibrarySystem.Services.Exceptions.AddressServices;
using LibrarySystem.Services.Services;
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
        [ExpectedException(typeof(AddAddressNullableExeption))]
        public void Throw_When_StreetAddress_IsNull()
        {

            var contextMoq = new Mock<ILibrarySystemContext>();
            var service = new AddressService(contextMoq.Object);
            var town = new Town();

            service.AddAddress(null, town);
        }

        [TestMethod]
        [ExpectedException(typeof(AddAddressNullableExeption))]
        public void Throw_When_Town_IsNull()
        {
            var contextMoq = new Mock<ILibrarySystemContext>();
            var service = new AddressService(contextMoq.Object);

            service.AddAddress("test", null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidAddressServiceParametersExeption))]
        public void Throw_When_StreetAddresss_IsEmpty()
        {
            var contextMoq = new Mock<ILibrarySystemContext>();
            var service = new AddressService(contextMoq.Object);

            var town = new Town();

            service.AddAddress("", town);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidAddressServiceParametersExeption))]
        public void Throw_When_StreetAddress_IsMoreThan50()
        {
            var contextMoq = new Mock<ILibrarySystemContext>();
            var service = new AddressService(contextMoq.Object);

            var town = new Town();

            service.AddAddress(new string('a', 51), town);
        }

        [TestMethod]
        public void AddAddress_ToDataBase_IfNotAlready()
        {
            var options = new DbContextOptionsBuilder<LibrarySystemContext>()
                   .UseInMemoryDatabase(databaseName: "AddAddress_ToDataBase")
                   .Options;

            string street = "test street";
            var town = new Town() { TownName = "Dupnitsa" };

            using (var context = new LibrarySystemContext(options))
            {
                var service = new AddressService(context);                
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
            Address address;

            using (var context = new LibrarySystemContext(options))
            {
                var service = new AddressService(context);
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
