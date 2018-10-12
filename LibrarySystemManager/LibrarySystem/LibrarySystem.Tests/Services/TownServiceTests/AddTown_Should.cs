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
            var validationMock = new Mock<CommonValidations>();

            string town1 = "test 1";
            string town2 = "test 2";

            // Act
            using (var actContext = new LibrarySystemContext(contextOptions))
            {
                var unit = new UnitOfWork(actContext);
                var repo = unit.GetRepo<Town>();

                var service = new TownService(unit, validationMock.Object);

                service.AddTown(town1);
                service.AddTown(town2);
            }

            // Assert
            using (var assertContext = new LibrarySystemContext(contextOptions))
            {
                var unit = new UnitOfWork(assertContext);
                var repo = unit.GetRepo<Town>();

                var service = new TownService(unit, validationMock.Object);

                int count = assertContext.Towns.Count();
                Assert.AreEqual(2, count);
                Assert.AreEqual(town1, assertContext.Towns.First().TownName);
            }
        }

        [TestMethod]
        public void Not_Add_IfTown_Exists()
        {
            var contextOptions = new DbContextOptionsBuilder<LibrarySystemContext>()
                .UseInMemoryDatabase(databaseName: "Not_Add_IfTown_Exists")
                .Options;
            var validationMock = new Mock<CommonValidations>();

            string town1 = "test 1";
            string town2 = "test 1";

            // Act
            using (var actContext = new LibrarySystemContext(contextOptions))
            {
                var unit = new UnitOfWork(actContext);
                var repo = unit.GetRepo<Town>();

                var service = new TownService(unit, validationMock.Object);

                service.AddTown(town1);
                service.AddTown(town2);
            }

            // Assert
            using (var assertContext = new LibrarySystemContext(contextOptions))
            {
                var unit = new UnitOfWork(assertContext);
                var repo = unit.GetRepo<Town>();

                var service = new TownService(unit, validationMock.Object);

                int count = assertContext.Towns.Count();
                Assert.AreEqual(1, count);
                Assert.AreEqual(town1, assertContext.Towns.First().TownName);
            }
        }
    }
}
