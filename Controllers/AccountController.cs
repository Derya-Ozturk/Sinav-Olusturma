using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SinavOlusturmaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SinavOlusturmaWeb.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Admin loginModel)
        {
            if (LoginUser(loginModel.Kullanici, loginModel.Sifre))
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loginModel.Kullanici)
            };

                var userIdentity = new ClaimsIdentity(claims, "login");

                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);

                return RedirectToAction("BaslikGoruntuleme", "Home");
            }
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index");
        }

        private bool LoginUser(string username, string password)
        {
            if (username == "derya" && password == "1234")
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
