using LibrarySystem.Data.Models;
using LibrarySystem.Services;
using LibrarySystem.Tests.Mocks;
using LibrarySystem.WebClient.Controllers;
using LibrarySystem.WebClient.Models.BooksViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Tests.Controllers.BookActions
{
    [TestClass]
    public class Details_Should
    {
        [TestMethod]
        public void Return_BookViewModel()
        {
            var guid = Guid.Parse("00000000-0000-0000-0000-000000000000");
            var book = new Book()
            {
                Id = guid,
                Author = new Author() { Name = "Pesho" },
                Title = "test",
                Genre = new Genre() { GenreName = "drama" },
                BooksInStore = 10
            };
            var bookServiceMock = new Mock<IBooksServices>();
            bookServiceMock.Setup(s => s.GetBookById(It.IsAny<Guid>())).Returns(book);

            var userServiceMock = new Mock<IUsersServices>();
            var userManagerMock = MockHelpers.MockUserManager<User>();
            var memoryCacheMock = new Mock<IMemoryCache>();

            var controller = new BooksController
                (bookServiceMock.Object, userManagerMock.Object, userServiceMock.Object, memoryCacheMock.Object);

            var result = controller.Details(guid) as ViewResult;
            var bookViewModel = (BookViewModel)result.ViewData.Model;
            Assert.AreEqual("test", bookViewModel.Title);
        }

        [TestMethod]
        public void Return_DetailsView()
        {
            var guid = Guid.Parse("00000000-0000-0000-0000-000000000000");
            var book = new Book()
            {
                Id = guid,
                Author = new Author() { Name = "Pesho" },
                Title = "test",
                Genre = new Genre() { GenreName = "drama" },
                BooksInStore = 10
            };
            var bookServiceMock = new Mock<IBooksServices>();
            bookServiceMock.Setup(s => s.GetBookById(It.IsAny<Guid>())).Returns(book);

            var userServiceMock = new Mock<IUsersServices>();
            var userManagerMock = MockHelpers.MockUserManager<User>();
            var memoryCacheMock = new Mock<IMemoryCache>();

            var controller = new BooksController
                (bookServiceMock.Object, userManagerMock.Object, userServiceMock.Object, memoryCacheMock.Object);

            var result = controller.Details(guid) as ViewResult;

            Assert.AreEqual("Details", result.ViewName);
        }

    }
}
