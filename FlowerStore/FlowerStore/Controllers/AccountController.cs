
using FlowerStore.Models;
using FlowerStore.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowerStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;

        public AccountController(UserManager<User> userManager)
        {
            _userManager = userManager; 
        }
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var dbUser = await _userManager.FindByNameAsync(model.Username);
            if (dbUser != null)
            {
                ModelState.AddModelError(nameof(RegisterViewModel.Username), "The user with this username already exists ");
                return View();
            }

            User user = new User
            {
                 Name = model.FullName,
                 UserName = model.Username,
                 Email = model.Email
            };

            var IdentityResult = await _userManager.CreateAsync(user, model.Password);
            if (!IdentityResult.Succeeded)
            {
                foreach (var item in IdentityResult.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
