using LibrarySystem.Data.Models;
using LibrarySystem.Services;
using LibrarySystem.Services.Services;
using LibrarySystem.WebClient.Areas.Administration.Controllers;
using LibrarySystem.WebClient.Models.BooksViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Tests.Controllers.AdminArea.AdminActions
{
    [TestClass]
    public class CreateBook_Should
    {
        private Mock<IGenreServices> genreService = new Mock<IGenreServices>();
        private Mock<IBooksServices> bookService = new Mock<IBooksServices>();
        private Mock<IAuthorServices> authorService = new Mock<IAuthorServices>();
        private Guid guid = Guid.Parse("00000000-0000-0000-0000-000000000000");
        private Book book = new Book()
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000000"),
            Author = new Author() { Name = "Pesho" },
            Title = "test",
            Genre = new Genre() { GenreName = "drama" },
            BooksInStore = 10
        };

        [TestMethod]
        public void Redirect_To_HomeIndex_IfModelIsVAlid()
        {
            var controller = new AdminBooksController(bookService.Object, genreService.Object, authorService.Object);
            controller.ModelState.Clear();

            var result = (RedirectToActionResult)controller.CreateBook(new BookViewModel());

            Assert.AreEqual("Index", result.ActionName);
            Assert.AreEqual("Home", result.ControllerName);
        }

        [TestMethod]
        public void Return_CreateBookView_IfModelIsInvalid()
        {
            var controller = new AdminBooksController(bookService.Object, genreService.Object, authorService.Object);
            controller.ModelState.AddModelError("test", "test");

            var result = controller.CreateBook(new BookViewModel()) as ViewResult;

            Assert.AreEqual("CreateBook", result.ViewName);
        }
    }
}
