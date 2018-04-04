using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Timesheet.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Display(int id)
        {
            return View();
        }
    }
}