using LibrarySystem.Data.Models;
using LibrarySystem.Services;
using LibrarySystem.Tests.Mocks;
using LibrarySystem.WebClient.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace LibrarySystem.Tests.Controllers.BookActions
{
    [TestClass]
    public class AddBook_Should
    {
        [TestMethod]
        public void MyTestMethod()
        {
            //var guid = Guid.Parse("00000000-0000-0000-0000-000000000000");
            //var book = new Book()
            //{
            //    Id = guid,
            //    Author = new Author() { Name = "Pesho" },
            //    Title = "test",
            //    Genre = new Genre() { GenreName = "drama" },
            //    BooksInStore = 10
            //};
            //var userServiceMock = new Mock<IUsersServices>();
            //var userManagerMock = MockHelpers.MockUserManager<User>();
            //var bookServiceMock = new Mock<IBooksServices>();
            //var contextMock = new Mock<HttpContext>();

            //userManagerMock.Setup(m => m.GetUserId(null)).Returns("1");
            //userServiceMock.Setup(s => s.BorrowBook(It.IsAny<string>(), It.IsAny<Guid>()));

            //var controller = new BooksController(bookServiceMock.Object, userManagerMock.Object, userServiceMock.Object);

            //var result = (RedirectToActionResult)controller.AddBook(guid);

            //Assert.AreEqual("Index", result.ActionName);
            //Assert.AreEqual("Books", result.ControllerName);
        }

    }
}
