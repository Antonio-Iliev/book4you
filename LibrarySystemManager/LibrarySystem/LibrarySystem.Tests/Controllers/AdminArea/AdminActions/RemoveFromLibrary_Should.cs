using LibrarySystem.Data.Models;
using LibrarySystem.Services;
using LibrarySystem.Services.Services;
using LibrarySystem.WebClient.Areas.Administration.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Tests.Controllers.AdminArea.AdminActions
{
    [TestClass]
    public class RemoveFromLibrary_Should
    {
        private Mock<IGenreServices> genreService = new Mock<IGenreServices>();
        private Mock<IBooksServices> bookService = new Mock<IBooksServices>();
        private Mock<IAuthorServices> authorService = new Mock<IAuthorServices>();
        private Guid guid = Guid.Parse("00000000-0000-0000-0000-000000000000");

        [TestMethod]
        public void Redirect_ToHomeIndex()
        {
            var controller = new AdminBooksController(bookService.Object, genreService.Object, authorService.Object);
            bookService.Setup(s => s.RemoveBook(It.IsAny<Guid>())).Returns(It.IsAny<Book>());            

            var result = (RedirectToActionResult)controller.RemoveFromLibrary(guid);

            Assert.AreEqual("Index", result.ActionName);
            Assert.AreEqual("Home", result.ControllerName);
        }

        [TestMethod]
        public void Invoce_BooksServices_RemoveBook()
        {
            var controller = new AdminBooksController(bookService.Object, genreService.Object, authorService.Object);
            bookService.Setup(s => s.RemoveBook(It.IsAny<Guid>())).Returns(It.IsAny<Book>());

            controller.RemoveFromLibrary(guid);

            bookService.Verify(s => s.RemoveBook(guid), Times.Once);
        }
    }
}
