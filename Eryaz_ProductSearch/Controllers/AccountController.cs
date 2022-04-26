using Eryaz_ProductSearch.DataLayer;
using Eryaz_ProductSearch.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eryaz_ProductSearch.Controllers
{    
    //[Authorize]
    public class AccountController : Controller
    {
        UserDAL _dbUsers = new UserDAL();

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous, HttpPost]
        public IActionResult Register(Users user)
        {
            _dbUsers.Register(user);
            return RedirectToAction("Login");
        }
        [AllowAnonymous]
        public IActionResult LogIn()
        {
            return View();
        }

        [AllowAnonymous, HttpPost]
        public IActionResult LogIn(Users user)
        {
            bool isValid = _dbUsers.LogIn(user);
            if (isValid)
                return RedirectToAction(nameof(ProductController.List), "Product");
            else
                return View();
        }
    }
}
