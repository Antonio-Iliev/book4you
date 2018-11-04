using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibrarySystem.Data.Models;
using LibrarySystem.Services;
using LibrarySystem.WebClient.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.WebClient.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBooksServices _booksServices;
        private readonly UserManager<User> _userManager;
        private readonly IUsersServices _usersServices;

        public BooksController(
            IBooksServices booksServices,
            UserManager<User> userManager,
            IUsersServices usersServices)
        {
            _booksServices = booksServices;
            _userManager = userManager;
            _usersServices = usersServices;
        }

        [Authorize(Roles = "Admin, User")]
        public IActionResult AddBook(string title)
        {
            // TODO take user Id
            var user = this._userManager.GetUserAsync(HttpContext.User).Result;

            // TODO BorrowBook(userID, bookID)
            var addbook = this._usersServices.BorrowBook(user.FirstName, user.MiddleName, user.LastName, title);
            return RedirectToAction("Index", "User");
        }

        public IActionResult Details(string title)
        {
            var book = _booksServices.GetBook(title);

            var model = new BookViewModel(book);

            return View(model);
        }
    }
}