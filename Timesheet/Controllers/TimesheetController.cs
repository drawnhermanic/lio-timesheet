using System.Web.Mvc;
using Timesheet.Data;
using Timesheet.Models;

namespace Timesheet.Controllers
{
    public class TimesheetController : Controller
    {
        private readonly ITimesheetRepository _timesheetRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        public TimesheetController(ITimesheetRepository timesheetRepository, 
            IUserRepository userRepository,
            IEmailService emailService)
        {
            _timesheetRepository = timesheetRepository;
            _userRepository = userRepository;
            _emailService = emailService;
        }

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

            _timesheetRepository.Save(timesheet.UserId, timesheet.Hours, timesheet.Date);
            var manager = _userRepository.GetManager(timesheet.UserId);
            if (manager != null)
            {
                _emailService.SendEmail(manager.EmailAddress);
            }

            return RedirectToAction("Display", "Report", new {id = timesheet.UserId});
        }
    }
}