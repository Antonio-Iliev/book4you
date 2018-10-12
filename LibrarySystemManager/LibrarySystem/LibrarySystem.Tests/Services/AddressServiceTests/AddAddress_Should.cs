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
                .UseInMemoryDatabase(databaseName: "Not_Add_IfTown_Exists")
                .Options;

            string streetAddress = "str test";
            var address = new Address()
            {
                StreetAddress = streetAddress,
                TownId = 1
            };

            // Act
            using (var actContext = new LibrarySystemContext(contextOptions))
            {
                actContext.Addresses.Add(address);
                actContext.SaveChanges();
            }

            // Assert
            using (var assertContext = new LibrarySystemContext(contextOptions))
            {
                int count = assertContext.Towns.Count();
                Assert.AreEqual(1, count);
                Assert.AreEqual(streetAddress, assertContext.Addresses.First().StreetAddress);
            }

        }

        [TestMethod]
        public void Not_Add_IfAddress_Exists()
        {
            var contextOptions = new DbContextOptionsBuilder<LibrarySystemContext>()
                .UseInMemoryDatabase(databaseName: "Not_Add_IfTown_Exists")
                .Options;

            string streetAddress = "str test";
            var address = new Address()
            {
                StreetAddress = streetAddress,
                TownId = 1
            };

            // Act
            using (var actContext = new LibrarySystemContext(contextOptions))
            {
                actContext.Addresses.Add(address);
                actContext.Addresses.Add(address);
                actContext.SaveChanges();
            }

            // Assert
            using (var assertContext = new LibrarySystemContext(contextOptions))
            {
                int count = assertContext.Towns.Count();
                Assert.AreEqual(1, count);
                Assert.AreEqual(streetAddress, assertContext.Addresses.First().StreetAddress);
            }

        }
    }
}
