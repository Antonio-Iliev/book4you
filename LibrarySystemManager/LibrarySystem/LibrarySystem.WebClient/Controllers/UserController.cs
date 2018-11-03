using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibrarySystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.WebClient.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class UserController : Controller
    {
        private readonly IBooksServices _booksServices;

        public UserController(IBooksServices booksServices)
        {
            this._booksServices = booksServices;
        }

        public IActionResult Index()
        {
            var allBooks = _booksServices.ListBooks();

            return View();
        }
    }
}