using System;
using System.Web.Mvc;
using Timesheet.Data;
using Timesheet.Models;

namespace Timesheet.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        // GET: Home
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(UserModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            
            var userId = _userRepository.Save(user.UserName, HashingHelper.GetStringSha256Hash(user.Password));

            return RedirectToAction("Index", "Timesheet", new { id = userId });
        }
    }
}