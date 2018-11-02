using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibrarySystem.Data.Models;
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

        public UsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var users = this._userManager
                .Users
                .Include(u=>u.Address)
                .ThenInclude(a=>a.Town)
                .Select(u=>new UserViewModel(u))
                .ToList();

            return View(users);
        }
        public IActionResult Details()
        {           
            var userId = _userManager.GetUserId(HttpContext.User);
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {             
                var user = this._userManager
                    .Users
                    .Include(u => u.Address)
                    .ThenInclude(a => a.Town)
                    .SingleOrDefault(u => u.Id == userId);
                    
                var viewModel=new UserViewModel(user);
                return View(viewModel);
            }
        }
    }
}