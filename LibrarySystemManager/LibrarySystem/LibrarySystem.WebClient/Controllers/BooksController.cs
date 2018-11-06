using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibrarySystem.Data.Models;
using LibrarySystem.Services;
using LibrarySystem.WebClient.Models;
using LibrarySystem.WebClient.Models.BooksViewModels;
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
        public IActionResult Index()
        {
            var user = GetUser();

            // TODO Take 5 random books.
            var booksOfTheDay = this._booksServices.ListBooks().Take(5);

            var bookModel = booksOfTheDay.Select(b => new BookViewModel(b, user));

            var model = new BookIndexViewModel(bookModel);

            return View(model);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPost]
        public IActionResult AddBook(Guid bookId)
        {
            var userId = this._userManager.GetUserId(HttpContext.User);

            this._usersServices.BorrowBook(userId, bookId);

            return RedirectToAction("Index", "Books");
        }

        public IActionResult Details(Guid bookId)
        {
            var book = _booksServices.GetBookById(bookId);

            var model = new BookViewModel(book);

            return View(model);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet]
        public IActionResult ListBooks(string searchBy, string parameters)
        {
            var books = this._booksServices.ListBooks(searchBy, parameters);

            var user = GetUser();

            var bookModel = books.Select(b => new BookViewModel(b, user));

            var model = new BookIndexViewModel(bookModel);

            model.SearchBy = searchBy;
            model.Parameters = parameters;

            return View(model);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPost]
        public IActionResult ListBooks(BookIndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction
                ("ListBooks", new { searchBy = model.SearchBy, parameters = model.Parameters });
        }

        private User GetUser()
        {
            var userId = this._userManager.GetUserId(HttpContext.User);

            var user = this._usersServices.GetUserById(userId);

            return user;
        }
    }
}