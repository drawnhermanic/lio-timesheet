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
            
            var userId = _userRepository.Save(user.UserName, GetStringSha256Hash(user.Password));

            return RedirectToAction("Index", "Timesheet", new { id = userId });
        }

        internal static string GetStringSha256Hash(string text)
        {
            if (String.IsNullOrEmpty(text))
                return String.Empty;

            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                byte[] textData = System.Text.Encoding.UTF8.GetBytes(text);
                byte[] hash = sha.ComputeHash(textData);
                return BitConverter.ToString(hash).Replace("-", String.Empty);
            }
        }

    }
}