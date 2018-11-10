using LibrarySystem.Services;
using LibrarySystem.WebClient.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Tests.Controllers.HomeActions
{
    [TestClass]
    public class About_Should
    {
        [TestMethod]
        public void Return_AboutView()
        {
            var bookServiceMock = new Mock<IBooksServices>();
            var memoryCacheMock = new Mock<IMemoryCache>();
            var controller = new HomeController(bookServiceMock.Object);

            var result = controller.About() as ViewResult;

            //To test the name of the view you have to specify it in the action "return View("Index")"
            Assert.AreEqual("About", result.ViewName);
        }
    }
}
