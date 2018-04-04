using System.Web.Mvc;
using Timesheet.Models;

namespace Timesheet.Controllers
{
    public class AccountController : Controller
    {
        // GET: Home
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(UserModel user)
        {
            int? userId = null;

            if (!ModelState.IsValid)
            {
                return View(user);
            }
            
            //persist user
            //userId = _userRepository.Save(user);

            return RedirectToAction("Index", "Timesheet", new { id = userId });
        }


    }
}