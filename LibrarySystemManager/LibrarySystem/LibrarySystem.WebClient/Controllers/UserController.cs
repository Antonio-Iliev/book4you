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
            var booksOfTheDay = this._booksServices.ListBooks().Take(5);

            var model = booksOfTheDay.Select(b => new BookViewModel(b));

            return View(model);
        }

        public IActionResult Details()
        {
            var userId = this._userManager.GetUserId(HttpContext.User);

            var user = this._usersServices.GetUserById(userId);

            var model = new UserViewModel(user);

            return View(model);
        }
    }
}