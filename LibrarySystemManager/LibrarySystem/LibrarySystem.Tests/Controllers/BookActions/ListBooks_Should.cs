using LibrarySystem.Data.Models;
using LibrarySystem.Services;
using LibrarySystem.Tests.Mocks;
using LibrarySystem.WebClient.Controllers;
using LibrarySystem.WebClient.Models.BooksViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace LibrarySystem.Tests.Controllers.BookActions
{
    [TestClass]
    public class ListBooks_Should
    {
        [TestMethod]
        public void Redirect_ToListBooks_If_NoBooksAreFound()
        {
            var books = new List<Book>() { };
            var userServiceMock = new Mock<IUsersServices>();
            var userManagerMock = MockHelpers.MockUserManager<User>();
            var bookServiceMock = new Mock<IBooksServices>();
            bookServiceMock.Setup(s => 
                s.ListBooks(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())
            ).Returns(books);

            var memoryCacheMock = new Mock<IMemoryCache>();

            var controller = new BooksController
                (bookServiceMock.Object, userManagerMock.Object, userServiceMock.Object, memoryCacheMock.Object);

            var result = (RedirectToActionResult)controller.ListBooks("test", "test", 1);

            Assert.AreEqual("ListBooks", result.ActionName);
        }
    }
}
