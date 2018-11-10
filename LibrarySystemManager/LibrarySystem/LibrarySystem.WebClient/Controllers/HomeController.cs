using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LibrarySystem.WebClient.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using LibrarySystem.Services;
using LibrarySystem.Data.Context;
using LibrarySystem.WebClient.Models.BooksViewModels;
using Microsoft.Extensions.Caching.Memory;

namespace LibrarySystem.WebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBooksServices bookService;
        private readonly IMemoryCache memoryCache;

        public HomeController(IBooksServices bookService, IMemoryCache memoryCache)
        {
            this.bookService = bookService;
            this.memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            var listAllBooks = this.memoryCache.GetOrCreate("AllBooks", e => 
            {
                e.AbsoluteExpiration = DateTime.UtcNow.AddDays(7);
                return this.bookService.ListBooks();
            });

            var books = listAllBooks.Select(b => new BookViewModel(b));
            return View("Index", books);
        }

        public IActionResult ReadGridBooks([DataSourceRequest] DataSourceRequest request)
        {
            var result = this.bookService.ListBooks().Select(b => new BookViewModel(b)).ToDataSourceResult(request);

            return this.Json(result);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View("About");
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View("Contact");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
