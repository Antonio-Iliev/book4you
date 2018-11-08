using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                return this.View();
            }
            var newGenre = this._genreServices.AddGenre(model.Genre);
            var newAuthor = this._authorServices.AddAuthor(model.Author);
            var bookQnty = model.BooksInStore;
            var newBook = this._booksService.AddBook(model.Title, newGenre, newAuthor, model.BooksInStore.ToString());

            //TO DO
            //return RedirectToAction("Details", "Books", new { id = newBook.Id });
            return RedirectToAction("Index", "Users");
        }
        public IActionResult RemoveFromLibrary(Guid bookId)
        {
            var book = this._booksService.RemoveBook(bookId);

            //To Do return RedirectToAction("Index", "Books");
            return RedirectToAction("Index", "Users");
        }
    }
}