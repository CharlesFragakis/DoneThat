using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoneThatMVC.Models;
using Microsoft.AspNetCore.Mvc;
using DoneThatMVC.DatabaseAccess;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace DoneThatMVC.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View(new LoginVM());
        }
        [HttpPost]
        public IActionResult Login(LoginVM login)
        {


            if (!ModelState.IsValid)
            {
                return View(login);
            }

            User user = UserManager.GetUserByUsername(login.Username);


            if (user != null && login.Password == user.Password && user.IsActive)
            {
                List<Claim> claims;
                if (user.IsAdmin)
                {
                    claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.Role, "SuperAdmin")
                    };
                }
                else
                {
                    claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.Role, "SimpleUser")
                    };
                }


                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1),
                    IsPersistent = true,
                    IssuedUtc = DateTimeOffset.UtcNow,
                    //RedirectUri = <string>
                    // The full path or absolute URI to be used as an http 
                    // redirect response value.
                };

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);


                return RedirectToAction("Index", "Home");
            }
            else if (!user.IsActive)
            {
                // TODO: Retrun message for Deleted account
                return null;
            }
            else
            {
                // TODO: Return message for wrong username or password
                return null;
            }
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register(RegisterVM register)
        {
            bool usernameUnique = UserManager.GetUserByUsername(register.Username) == null;
            

            if (ModelState.IsValid && usernameUnique)
            {
                User user = new User();
                user.IsActive = true;
                user.IsAdmin = false;
                user.Name = register.Name;
                user.Password = register.Password;
                user.Surname = register.Surname;
                user.Username = register.Username;
                user.Email = register.Email;
                UserManager.AddUser(user);

                List<Claim> claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.Role, "SuperAdmin")
                    };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1),
                    IsPersistent = true,
                    IssuedUtc = DateTimeOffset.UtcNow,
                    //RedirectUri = <string>
                    // The full path or absolute URI to be used as an http 
                    // redirect response value.
                };

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                return RedirectToAction("Index", "Home");
            }
            else if (!usernameUnique)
            {
                //To do error message for Username allready taken
                return View(register);
            }
            else
            {
                //To Do error message not valid
                return View(register);
            }
        }
    }
}