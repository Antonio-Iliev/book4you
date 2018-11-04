using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibrarySystem.Services;
using LibrarySystem.WebClient.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.WebClient.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBooksServices booksService;

        public BooksController(IBooksServices booksService)
        {
            this.booksService = booksService;
        }

        public IActionResult Details(string id)
        {
            var book = this.booksService.GetBookById(id);
            return View(new BookViewModel(book));
        }
    }
}