using LibrarySystem.Services;
using LibrarySystem.WebClient.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Tests.Controllers.HomeActions
{
    [TestClass]
    public class Contacts_Should
    {
        [TestMethod]
        public void Return_ContactsView()
        {
            var bookServiceMock = new Mock<IBooksServices>();
            var controller = new HomeController(bookServiceMock.Object);

            var result = controller.Contact() as ViewResult;

            //To test the name of the view you have to specify it in the action "return View("Index")"
            Assert.AreEqual("Contact", result.ViewName);
        }
    }
}
