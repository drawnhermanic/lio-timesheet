using System.Web.Mvc;
using Timesheet.Models;

namespace Timesheet.Controllers
{
    public class TimesheetController : Controller
    {
        // GET: Timesheet
        public ActionResult Index(int id)
        {
            var timesheetViewModel = new TimesheetModel { UserId = id };
            return View(timesheetViewModel);
        }

        [HttpPost]
        public ActionResult Index(TimesheetModel timesheet)
        {
            if (!ModelState.IsValid)
            {
                return View(timesheet);
            }

            //save timesheet
            //look up manager from user
            //send email to manager

            return RedirectToAction("Display", "Report", new {id = timesheet.UserId});
        }
    }
}