using LibrarySystem.ConsoleClient.Commands;
using LibrarySystem.Services;
using LibrarySystem.Services.Exceptions.BookServiceExeptions;
using LibrarySystem.Services.Services;
using LibrarySystem.Services.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Tests.Commands.AddBookCommandTests
{
    [TestClass]
    public class Execute_Should
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidBookServiceParametersExeption))]
        public void Throw_If_ParamethersCount_IsInvalid()
        {
            var parameters = new List<string>() { "title", "author", "genre" };
            var genreServiceMock = new Mock<IGenreServices>();
            var authorServiceMock = new Mock<IAuthorServices>();
            var bookServiceMock = new Mock<IBooksServices>();
            var command = new AddBookCommand(bookServiceMock.Object, genreServiceMock.Object, authorServiceMock.Object);

            command.Execute(parameters);
        }

        [TestMethod]
        public void Call_GengeService_AddGenre_Once()
        {
            var parameters = new List<string>() { "title", "genre", "author", "1" };
            var genreServiceMock = new Mock<IGenreServices>();
            var authorServiceMock = new Mock<IAuthorServices>();
            var bookServiceMock = new Mock<IBooksServices>();
            var command = new AddBookCommand(bookServiceMock.Object, genreServiceMock.Object, authorServiceMock.Object);

            genreServiceMock.Setup(x => x.AddGenre(It.IsAny<string>())).Returns(1);
            authorServiceMock.Setup(x => x.AddAuthor(It.IsAny<string>())).Returns(1);
            bookServiceMock.Setup(x => x.AddBook(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Returns(new BookViewModel() { Title = parameters[0] });

            command.Execute(parameters);

            genreServiceMock.Verify(s => s.AddGenre(parameters[1]), Times.Once());
        }

        [TestMethod]
        public void Call_AuthorService_AddAuthor_Once()
        {
            var parameters = new List<string>() { "title", "genre", "author", "1" };
            var genreServiceMock = new Mock<IGenreServices>();
            var authorServiceMock = new Mock<IAuthorServices>();
            var bookServiceMock = new Mock<IBooksServices>();
            var command = new AddBookCommand(bookServiceMock.Object, genreServiceMock.Object, authorServiceMock.Object);

            genreServiceMock.Setup(x => x.AddGenre(It.IsAny<string>())).Returns(1);
            authorServiceMock.Setup(x => x.AddAuthor(It.IsAny<string>())).Returns(1);
            bookServiceMock.Setup(x => x.AddBook(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Returns(new BookViewModel() { Title = parameters[0] });

            command.Execute(parameters);

            authorServiceMock.Verify(a => a.AddAuthor(parameters[2]), Times.Once());
        }

        [TestMethod]
        public void Call_BookService_AddBook_Once()
        {
            var parameters = new List<string>() { "title", "genre", "author", "1" };
            var genreServiceMock = new Mock<IGenreServices>();
            var authorServiceMock = new Mock<IAuthorServices>();
            var bookServiceMock = new Mock<IBooksServices>();
            var command = new AddBookCommand(bookServiceMock.Object, genreServiceMock.Object, authorServiceMock.Object);

            genreServiceMock.Setup(x => x.AddGenre(It.IsAny<string>())).Returns(1);
            authorServiceMock.Setup(x => x.AddAuthor(It.IsAny<string>())).Returns(1);
            bookServiceMock.Setup(x => x.AddBook(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Returns(new BookViewModel() { Title = parameters[0] });

            command.Execute(parameters);

            bookServiceMock.Verify( b => b.AddBook(parameters[0], 1, 1, parameters[3]), Times.Once());
        }

        [TestMethod]
        public void Return_SuccessMessage()
        {
            var parameters = new List<string>() { "title", "genre", "author", "1" };
            var genreServiceMock = new Mock<IGenreServices>();
            var authorServiceMock = new Mock<IAuthorServices>();
            var bookServiceMock = new Mock<IBooksServices>();
            var command = new AddBookCommand(bookServiceMock.Object, genreServiceMock.Object, authorServiceMock.Object);

            genreServiceMock.Setup(x => x.AddGenre(It.IsAny<string>())).Returns(1);
            authorServiceMock.Setup(x => x.AddAuthor(It.IsAny<string>())).Returns(1);
            bookServiceMock.Setup(x => x.AddBook(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Returns(new BookViewModel() { Title = parameters[0] });

            var message = command.Execute(parameters);

            Assert.AreEqual($"New book {parameters[0]} was added.", message);
        }
    }
}
