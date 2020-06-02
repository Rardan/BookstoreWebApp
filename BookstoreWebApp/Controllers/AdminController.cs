using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookstoreWebApp.Models;
using BookstoreWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreWebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<StoreUser> _userManager;

        public AdminController(UserManager<StoreUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> RegisterEmployee()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterEmployee(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new StoreUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if(result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Employee");
                    ViewBag.CreateUserSucceeded = "Created Employee: " + user.Email;
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View();
        }
    }
}