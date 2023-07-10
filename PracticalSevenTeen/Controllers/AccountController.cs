using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using PracSevenTeen.Db.Interfaces;
using PracSevenTeen.Models.Enum;
using PracSevenTeen.Models.Models;
using PracticalSevenTeen.ViewModels;
using System.Security.Claims;

namespace PracticalSevenTeen.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel user)
        {
            if (ModelState.IsValid)
            {
                var temp = await _accountRepository.GetUserByEmailAsync(user.Email);
                if (temp == null)
                {

                    await _accountRepository.RegisterUserAsync(user);
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    ModelState.AddModelError("", "User Already Registered!");
                    return View(user);
                }
            }
            return View(user);
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginViewModel model)
        {
            if (model == null)
            {
                return NotFound();
            }
            UserLoginStatus status = await _accountRepository.LoginUserAsync(model);

            switch (status)
            {
                case UserLoginStatus.UserNull:
                    return NotFound();
                    break;
                case UserLoginStatus.UserNotFound:
                    ModelState.AddModelError("", "No such Account found!");
                    return View(model);
                    break;

                case UserLoginStatus.InvalidPassword:
                    ModelState.AddModelError("", "Invalid User Id and Password");
                    return View(model);
                    break;

                case UserLoginStatus.LoginSuccess:
                    await LoginUserAsync(model);

                    return RedirectToAction("Index", "Student");
                    break;
            }


            return RedirectToAction("Index", "Home");
        }

        private async Task LoginUserAsync(LoginViewModel model)
        {
            User user = await _accountRepository.GetUserByEmailAsync(model.Email);
            List<string> roles = _accountRepository.GetUserRoles(user.Id);

            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Name, model.Email));
            foreach (var item in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));
            }

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrinciple = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrinciple);
            HttpContext.Session.SetString("Email", user.Email);

        }


        public async Task<IActionResult> Logout()
        {
            var cookieKeys = Request.Cookies.Keys;
            foreach (var cookieKey in cookieKeys)
            {
                Response.Cookies.Delete(cookieKey);
            }
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
