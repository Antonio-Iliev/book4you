using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LibrarySystem.WebClient.Models;
using LibrarySystem.Services;

namespace LibrarySystem.WebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBooksServices bookService;

        public HomeController(IBooksServices bookService)
        {
            this.bookService = bookService;
        }

        public IActionResult Index()
        {
            var books = bookService.ListBooks().Select(b => new BookViewModel(b));
            return View(books);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
