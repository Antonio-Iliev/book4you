using LibrarySystem.Data.Context;
using LibrarySystem.Data.Contracts;
using LibrarySystem.Services.Exceptions.TownServiceExeptions;
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
        public void Throw_When_NullParameters_ArePassed()
        {
            var contextMoq = new Mock<ILibrarySystemContext>();

            var service = new TownService(contextMoq.Object);

            service.AddTown(null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidTownServiceParametersExeption))]
        public void Throw_When_EmptyTownName_IsPassed()
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

    }
}
