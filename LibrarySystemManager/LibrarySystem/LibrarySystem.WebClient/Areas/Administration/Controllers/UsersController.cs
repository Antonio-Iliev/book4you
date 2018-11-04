using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibrarySystem.Data.Models;
using LibrarySystem.Services;
using LibrarySystem.WebClient.Areas.Administration.Models;
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

        public UsersController(UserManager<User> userManager, IUsersServices usersServices)
        {
            _userManager = userManager;
            _usersServices = usersServices;
        }

        public IActionResult Index()
        {
            var users = this._userManager
                .Users
                .Include(u => u.Address)
                    .ThenInclude(a => a.Town)
                .Include(u => u.UsersBooks)
                    .ThenInclude(ub => ub.Book)
                .Where(u => u.IsDeleted == false)
                .Select(u => new UserViewModel(u))
                .ToList();

            return View(users);
        }
        public IActionResult ActiveUsers()
        {
            var users = this._usersServices
                .ListUsers(false)
                .Select(u => new UserViewModel(u))
                .ToList();
            return View(users);
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
            return this.RedirectToAction("ActiveUsers", "Users");
        }
    }
}