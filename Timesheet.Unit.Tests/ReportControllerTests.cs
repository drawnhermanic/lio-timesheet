using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Should;
using SpecsFor;
using Timesheet.Controllers;
using Timesheet.Data;

namespace Timesheet.Unit.Tests
{
    public class ReportControllerTests
    {
        public class WhenDisplayingTheReport : SpecsFor<ReportController>
        {
            protected ActionResult Result { get; set; }
            protected int UserId { get; set; }
            protected List<Data.Timesheet> Timesheets { get; set; }

            protected override void Given()
            {
                UserId = 1;

                Timesheets = new List<Data.Timesheet>
                {
                    new Data.Timesheet(),
                    new Data.Timesheet()
                };

                GetMockFor<ITimesheetRepository>().Setup(r => r.Get(UserId)).Returns(Timesheets);
            }

            protected override void When()
            {
                Result = SUT.Display(UserId);
            }

            [Test]
            public void ThenItDisplaysTheReportPage()
            {
                Result.ShouldBeType<ViewResult>();
            }

            [Test]
            public void ItShouldGetTimesheets()
            {
                GetMockFor<ITimesheetRepository>().Verify(r => r.Get(UserId));
            }
        }       
    }
}
