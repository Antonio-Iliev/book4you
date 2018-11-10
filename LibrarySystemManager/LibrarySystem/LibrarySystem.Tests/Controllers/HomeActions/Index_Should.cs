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
            var controller = new HomeController(bookServiceMock.Object);
            bookServiceMock.Setup(s => s.ListBooks()).Returns(new List<Book>());

            var result = controller.Index() as ViewResult;

            //To test the name of the view you have to specify it in the action "return View("Index")"
            Assert.AreEqual("Index", result.ViewName); 
        }
    }
}
