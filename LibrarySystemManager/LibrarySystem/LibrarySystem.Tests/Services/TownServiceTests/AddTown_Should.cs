using LibrarySystem.Data.Context;
using LibrarySystem.Data.Models;
using LibrarySystem.Data.Repository;
using LibrarySystem.Data.Repository.Contracts;
using LibrarySystem.Services.Exceptions.TownServices;
using LibrarySystem.Services.Services;
using LibrarySystem.Services.Validations;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;

namespace LibrarySystem.Tests.Services.TownServiceTests
{
    [TestClass]
    public class AddTown_Should
    {

        [TestMethod]
        public void Add_Town_ToDataBase()
        {
            var contextOptions = new DbContextOptionsBuilder<LibrarySystemContext>()
                .UseInMemoryDatabase(databaseName: "Add_Town_ToDataBase")
                .Options;

            string townName = "test";
            var town = new Town() { TownName = townName };

            // Act
            using (var actContext = new LibrarySystemContext(contextOptions))
            {
                actContext.Towns.Add(town);
                actContext.SaveChanges();
            }

            // Assert
            using (var assertContext = new LibrarySystemContext(contextOptions))
            {
                int count = assertContext.Towns.Count();
                Assert.AreEqual(1, count);
                Assert.AreEqual(townName, assertContext.Towns.First().TownName);
            }
        }

        [TestMethod]
        public void Not_Add_IfTown_Exists()
        {
            var contextOptions = new DbContextOptionsBuilder<LibrarySystemContext>()
                .UseInMemoryDatabase(databaseName: "Not_Add_IfTown_Exists")
                .Options;

            string townName = "test";
            var town = new Town() { TownName = townName };

            // Act
            using (var actContext = new LibrarySystemContext(contextOptions))
            {
                actContext.Towns.Add(town);
                actContext.Towns.Add(town);
                actContext.SaveChanges();
            }

            // Assert
            using (var assertContext = new LibrarySystemContext(contextOptions))
            {
                int count = assertContext.Towns.Count();
                Assert.AreEqual(1, count);
                Assert.AreEqual(townName, assertContext.Towns.First().TownName);
            }
        }
    }
}
