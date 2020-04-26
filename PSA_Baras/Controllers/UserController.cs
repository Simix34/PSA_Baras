using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PSA_Baras.Data;
using PSA_Baras.Models;

namespace PSA_Baras.Controllers
{
    public class UserController : Controller
    {
        private readonly BarasDBContext _context;

        public UserController(BarasDBContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userdetails = await _context.User
                .SingleOrDefaultAsync(m => m.login == model.login && m.password == model.password);
                if (userdetails == null)
                {
                    ModelState.AddModelError("Password", "Invalid login attempt.");
                    return View("Index");
                }
                HttpContext.Session.SetString("userId", userdetails.login);

            }
            else
            {
                return View("Login");
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<ActionResult> Register(RegistrationViewModel model)
        {

            if (ModelState.IsValid)
            {
                User user = new User
                {
                    login = model.login,
                    first_name = model.first_name,
                    last_name = model.last_name,
                    email = model.email,
                    password = model.password,
                    role = 0

                };
                _context.Add(user);
                await _context.SaveChangesAsync();

            }
            else
            {
                return View("Registration");
            }
            return RedirectToAction("Index", "Home");
        }
        // registration Page load
        public IActionResult Registration()
        {
            ViewData["Message"] = "Registration Page";

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        public void ValidationMessage(string key, string alert, string value)
        {
            try
            {
                TempData.Remove(key);
                TempData.Add(key, value);
                TempData.Add("alertType", alert);
            }
            catch
            {
                Debug.WriteLine("TempDataMessage Error");
            }

        }
    }
}