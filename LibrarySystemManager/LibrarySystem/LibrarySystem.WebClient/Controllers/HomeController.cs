using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LibrarySystem.WebClient.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using LibrarySystem.Services;
using LibrarySystem.WebClient.Models.BooksViewModels;

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
            return View("Index");
        }

        public IActionResult ReadGridBooks([DataSourceRequest] DataSourceRequest request)
        {
            var result = this.bookService.ListBooks().Select(b => new BookViewModel(b)).ToDataSourceResult(request);

            return this.Json(result);
        }

        [ResponseCache(Duration = 3600)]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View("About");
        }

        [ResponseCache(Duration = 3600)]
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
