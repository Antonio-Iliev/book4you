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

        [Authorize(Roles = "Admin, User")]
        public IActionResult MyLibrary()
        {
            var userId = this._userManager.GetUserId(HttpContext.User);

            var user = this._usersServices.GetUserById(userId);
            // pass only UserViewModel
            var model = new UserViewModel(user);

            return View(model);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPost]
        public IActionResult ReturnBook(Guid bookId)
        {
            var userId = this._userManager.GetUserId(HttpContext.User);

            var book = this._usersServices.ReturnBook(userId, bookId);

            return RedirectToAction("MyLibrary");
        }

    }
}