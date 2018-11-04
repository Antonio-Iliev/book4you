using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibrarySystem.Data.Models;
using LibrarySystem.Services;
using LibrarySystem.WebClient.Models;
using LibrarySystem.WebClient.Models.UserViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.WebClient.Controllers
{

    [Authorize(Roles = "Admin, User")]
    public class UserController : Controller
    {
        private readonly IBooksServices _booksServices;
        private readonly IUsersServices _usersServices;
        private readonly UserManager<User> _userManager;

        public UserController(
            IBooksServices booksServices,
            IUsersServices usersServices,
            UserManager<User> userManager)
        {
            this._booksServices = booksServices;
            this._usersServices = usersServices;
            this._userManager = userManager;
        }

        public IActionResult Index()
        {
            // TODO Take 5 random books.
            var allBooks = _booksServices.ListBooks().Take(5);

            // TODO ask Edo for permission to use .Result
            var user = this._userManager.GetUserAsync(HttpContext.User).Result;

            var model = new UserViewModel(user, allBooks);

            return View(model);
        }

        public IActionResult Details()
        {
            // TODO ask Edo for permission to use .Result
            User currentUser = this._userManager.GetUserAsync(HttpContext.User).Result;

            var user = this._usersServices.GetUser(currentUser.FirstName, "", currentUser.LastName);

            var books = user.UsersBooks.Select(b => b.Book);

            var model = new UserViewModel(user, books);

            return View(model);
        }
    }
}