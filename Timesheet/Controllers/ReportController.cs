using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Timesheet.Data;
using Timesheet.Models;

namespace Timesheet.Controllers
{
    public class ReportController : Controller
    {
        private readonly ITimesheetRepository _timesheetRepository;

        public ReportController(ITimesheetRepository timesheetRepository)
        {
            _timesheetRepository = timesheetRepository;
        }

        // GET: Report
        public ActionResult Display(int id)
        {
            var timesheets = _timesheetRepository.Get(id);

            var viewModel = new List<TimesheetModel>();

            foreach (var timesheet in timesheets)
            {
                var timesheetModel = new TimesheetModel
                {
                    Hours = timesheet.HoursWorked,
                    Date = timesheet.Date
                };
                viewModel.Add(timesheetModel);
            }
            return View(viewModel);
        }
    }
}