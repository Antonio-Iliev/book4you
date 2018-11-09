using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibrarySystem.Data.Models;
using LibrarySystem.Services;
using LibrarySystem.Services.Services;
using LibrarySystem.WebClient.Areas.Administration.Models;
using LibrarySystem.WebClient.Models.BooksViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.WebClient.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Admin")]
    public class AdminBooksController : Controller
    {
        private readonly IBooksServices _booksService;
        private readonly IGenreServices _genreServices;
        private readonly IAuthorServices _authorServices;

        public AdminBooksController(IBooksServices booksService, IGenreServices genreServices, IAuthorServices authorServices)
        {
            _booksService = booksService;
            _genreServices = genreServices;
            _authorServices = authorServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateBook()
        {
            return this.View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateBook(BookViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View("CreateBook");
            }
            var newGenre = this._genreServices.AddGenre(model.Genre);
            var newAuthor = this._authorServices.AddAuthor(model.Author);
            var bookQnty = model.BooksInStore;

            var bookDbModel = new Book(model.Title, bookQnty, newGenre, newAuthor, model.ImageName);

            var newBook = this._booksService.AddBook(bookDbModel);

            return RedirectToAction("Index", "Home", new { area=""});
        }
        public IActionResult RemoveFromLibrary(Guid bookId)
        {
            var book = this._booksService.RemoveBook(bookId);

            return RedirectToAction("Index", "Home", new {area=""});
        }
    }
}