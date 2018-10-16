using LibrarySystem.ConsoleClient.Commands;
using LibrarySystem.Services;
using LibrarySystem.Services.Exceptions.BookServiceExeptions;
using LibrarySystem.Services.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace LibrarySystem.Tests.Commands.GetBookCommandTests
{
    [TestClass]
    public class Execute_Should
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidBookServiceParametersExeption))]
        public void Throw_If_ParamethersCount_IsInvalid()
        {
            var parameters = new List<string>();
            var bookServiceMock = new Mock<IBooksServices>();
            var command = new GetBookCommand(bookServiceMock.Object);

            command.Execute(parameters);
        }

        [TestMethod]
        public void Call_BookService_GetBook()
        {
            string title = "some Title",
                author = "dr. Radeva",
                genre = "Commedy";
            var parameters = new List<string>() { title };

            var bookServiceMock = new Mock<IBooksServices>();
            var command = new GetBookCommand(bookServiceMock.Object);

            bookServiceMock.Setup(s => s.GetBook(It.IsAny<string>()))
                .Returns(new BookViewModel()
                {
                    Title = title,
                    Author = author,
                    Genre = genre
                });

            command.Execute(parameters);

            bookServiceMock.Verify(s => s.GetBook(title), Times.Once);
        }

        [TestMethod]
        public void Return_SuccessMessage()
        {
            string title = "some Title",
                author = "dr. Radeva",
                genre = "Commedy";
            var parameters = new List<string>() { title };

            var bookServiceMock = new Mock<IBooksServices>();
            var command = new GetBookCommand(bookServiceMock.Object);

            bookServiceMock.Setup(s => s.GetBook(It.IsAny<string>()))
                .Returns(new BookViewModel()
                {
                    Title = title,
                    Author = author,
                    Genre = genre
                });

            string message = command.Execute(parameters);

            StringAssert.Contains(message, title);
            StringAssert.Contains(message, author);
            StringAssert.Contains(message, genre);
        }
    }
}
