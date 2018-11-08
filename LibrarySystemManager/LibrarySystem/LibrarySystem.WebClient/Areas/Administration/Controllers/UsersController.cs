using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibrarySystem.Data.Models;
using LibrarySystem.Services;
using LibrarySystem.Services.Constants.Enumeration;
using LibrarySystem.Services.Services;
using LibrarySystem.WebClient.Areas.Administration.Models;
using LibrarySystem.WebClient.WebClientGlobalConstants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.WebClient.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IUsersServices _usersServices;
        private readonly IAddressService _addressService;
        private readonly ITownService _townService;

        public UsersController(UserManager<User> userManager, IUsersServices usersServices, IAddressService addressService, ITownService townService)
        {
            _userManager = userManager;
            _usersServices = usersServices;
            _addressService = addressService;
            _townService = townService;
        }

        public IActionResult Index(int page = 1)
        {
            var users = this._usersServices
                   .ListUsers(ListUsersCategory.active.ToString(), WebConstants.numOfElementInPage, page)
                   .Select(u => new UserViewModel(u));

            if (users.Count() == 0)
            {
                return RedirectToAction("Index", new
                {
                    page = page - 1
                });
            }

            var model = new ListUsersModel(users, page);

            return View(model);
        }


        public IActionResult AllUsers(int page = 1)
        {
            var users = this._usersServices
                .ListUsers(ListUsersCategory.all.ToString(), WebConstants.numOfElementInPage, page)
                .Select(u => new UserViewModel(u));

            if (users.Count() == 0)
            {
                return RedirectToAction("AllUsers", new
                {
                    page = page - 1
                });
            }

            var model = new ListUsersModel(users, page);

            return View(model);
        }

        public IActionResult Details(string id)
        {
            var user = this._usersServices.GetUserById(id);
            var model = new UserViewModel(user);
            return View(model);
        }
        public IActionResult Delete(string id)
        {
            this._usersServices.RemoveUserById(id);
            return this.RedirectToAction("Index", "Users");
        }
        public IActionResult Restore(string id)
        {
            this._usersServices.RestoreUserById(id);
            return this.RedirectToAction("Index", "Users");
        }

        [HttpGet]
        public IActionResult AddBook()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddBook(string id, string title)
        {
            if (this.ModelState.IsValid)
            {
                this._usersServices.BorrowBook(id, title);
            }
            return this.RedirectToAction("Details", "Users", new { id });
        }

        [HttpGet]
        public IActionResult RemoveBook()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveBook(string id, string title)
        {
            if (this.ModelState.IsValid)
            {
                this._usersServices.ReturnBook(id, title);
            }
            return this.RedirectToAction("Details", "Users", new { id });
        }
        [HttpGet]
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var user = this._usersServices.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(new UserViewModel(user));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind(include: "Id, FirstName, MiddleName, LastName, Email, Phone, Address, Town")]UserViewModel model)
        {
            var town = this._townService.AddTown(model.Town);
            var address = this._addressService.AddAddress(model.Address, town);

            if (this.ModelState.IsValid)
            {
                this._usersServices.UpdateUser(model.Id, model.FirstName, model.MiddleName, model.LastName, model.Phone, address);
            }
            return this.RedirectToAction("Details", "Users", new { model.Id });
        }

    }
}