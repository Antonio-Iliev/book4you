using LibrarySystem.Data.Context;
using LibrarySystem.Data.Contracts;
using LibrarySystem.Data.Models;
using LibrarySystem.Services.Exceptions.TownServices;
using LibrarySystem.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibrarySystem.Tests.Services.TownServiceTests
{
    [TestClass]
    public class AddTown_Should
    {
        [TestMethod]
        [ExpectedException(typeof(AddTownNullableExeption))]
        public void Throw_When_TownName_IsNull()
        {
            var contextMoq = new Mock<ILibrarySystemContext>();

            var service = new TownService(contextMoq.Object);

            service.AddTown(null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidTownServiceParametersExeption))]
        public void Throw_When_TownName_IsMoreThan50()
        {
            var contextMoq = new Mock<ILibrarySystemContext>();
            var service = new TownService(contextMoq.Object);

            var town = new Town();

            service.AddTown(new string('a', 51));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidTownServiceParametersExeption))]
        public void Throw_When_TownName_IsEmpty()
        {
            var contextMoq = new Mock<ILibrarySystemContext>();

            var service = new TownService(contextMoq.Object);

            service.AddTown("");
        }

        [TestMethod]
        public void AddTown_InDataBase_IfNotAlready()
        {
            var options = new DbContextOptionsBuilder<LibrarySystemContext>()
                   .UseInMemoryDatabase(databaseName: "AddTown_InDataBase")
                   .Options;

            // Run the test against one instance of the context
            using (var context = new LibrarySystemContext(options))
            {
                var service = new TownService(context);
                service.AddTown("Dupnitsa");
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new LibrarySystemContext(options))
            {
                Assert.AreEqual(1, context.Towns.Count());
                Assert.AreEqual("Dupnitsa", context.Towns.Single().TownName);
            }
        }

        [TestMethod]
        public void Return_TownFromDataBase_IfExists()
        {
            var options = new DbContextOptionsBuilder<LibrarySystemContext>()
                   .UseInMemoryDatabase(databaseName: "Return_TownFromDataBase")
                   .Options;
            Town result;

            // Run the test against one instance of the context
            using (var context = new LibrarySystemContext(options))
            {
                var service = new TownService(context);
                service.AddTown("Dupnitsa");
                result = service.AddTown("Dupnitsa");
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new LibrarySystemContext(options))
            {
                Assert.AreEqual(1, context.Towns.Count());
                Assert.AreEqual("Dupnitsa", result.TownName);
            }
        }

    }
}
