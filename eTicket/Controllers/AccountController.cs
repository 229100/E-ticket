﻿using eTicket.Data;
using eTicket.Data.Static;
using eTicket.Data.ViewModels;
using eTicket.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTicket.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            AppDbContext context, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Users()
        {
            var users = await _context.Users.ToListAsync();
            _logger.LogInformation("List of users");
            return View(users);
        }

        public IActionResult Login()
        {
            _logger.LogInformation("I am in Account Controller: Login");

            return View(new LoginVM());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);

            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        _logger.LogError("Logged in Success.");
                        return RedirectToAction("Index", "Movies");
                    }
                }

                _logger.LogError("Неверные данные. Введите снова!");
                TempData["LoinError"] = "Неверные данные. Введите снова!";
                return View(loginVM);

            }

            _logger.LogError("Неверные данные. Введите снова!");
            TempData["LoinError"] = "Неверные данные. Введите снова!";
            return View(loginVM);

        }


        public IActionResult Register()
        {
            _logger.LogInformation("I am in Account Controller: Register");

            return View(new RegisterVM());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);
            var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress);
            if (user != null)
            {
                TempData["RegiError"] = "Эта электронная почта уже используется";
                _logger.LogError($"{registerVM.EmailAddress}: электронная почта уже используется");
                return View(registerVM);
            }
            else
            {
                var newUser = new ApplicationUser()
                {
                    FullName = registerVM.FullName,
                    Email = registerVM.EmailAddress,
                    UserName = registerVM.EmailAddress
                };

                var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);
                if (newUserResponse.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser, UserRoles.User);
                    _logger.LogInformation($"{newUser.Email}: Пользователь создан");
                    return View("RegisterCompleted");

                }
                else
                {
                    _logger.LogError($"{newUser.Email}: Пользователь не создан");
                    TempData["RegiError"] = "В пароле должны быть символы. Введите пароль надежнее";
                    return View(registerVM);
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Movies");
        }

        public IActionResult AccessDenied(string ReturnUrl)
        {
            return View();
        }
    }
}
