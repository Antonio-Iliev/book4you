using LibrarySystem.Data.Models;
using LibrarySystem.Services;
using LibrarySystem.WebClient.Models.BooksViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using LibrarySystem.WebClient.Controllers;
using System.Text;
using Microsoft.Extensions.Caching.Memory;

namespace LibrarySystem.Tests.Controllers.HomeActions
{
    [TestClass]
    public class Index_Should
    {
        [TestMethod]
        public void Return_IndexView()
        {
            var bookServiceMock = new Mock<IBooksServices>();
            var memoryCacheMock = new Mock<IMemoryCache>();
            var controller = new HomeController(bookServiceMock.Object, memoryCacheMock.Object);
            bookServiceMock.Setup(s => s.ListBooks()).Returns(new List<Book>());

            var result = controller.Index() as ViewResult;

            //To test the name of the view you have to specify it in the action "return View("Index")"
            Assert.AreEqual("Index", result.ViewName); 
        }

        [TestMethod]
        public void Return_IndexView_WithListOfBookViewModel()
        {
            var book = new Book()
            {
                Title = "test",
                Author = new Author() { Name = "Author" },
                Genre = new Genre() { GenreName = "UnitTest" },
                UsersBooks = new List<UsersBooks>(),
                Id = Guid.Parse("00000000-0000-0000-0000-000000000000")
            };
            
            var bookServiceMock = new Mock<IBooksServices>();
            bookServiceMock.Setup(s => s.ListBooks()).Returns(new List<Book>() { book });

            var memoryCacheMock = new Mock<IMemoryCache>();
            var controller = new HomeController(bookServiceMock.Object, memoryCacheMock.Object);


            var result = controller.Index() as ViewResult;
            var books = (IEnumerable<BookViewModel>) result.ViewData.Model;

            //To test the name of the view you have to specify it in the action "return View("Index")"
            Assert.AreEqual(1, books.Count());
            Assert.AreEqual("test", books.First().Title);
        }
    }
}
