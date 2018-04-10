using System;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Should;
using SpecsFor;
using Timesheet.Controllers;
using Timesheet.Data;
using Timesheet.Models;

namespace Timesheet.Unit.Tests
{
    public class TimesheetControllerTests
    {
        public class WhenCreatingTimesheet : SpecsFor<TimesheetController>
        {
            protected ActionResult Result { get; set; }
            protected int UserId { get; set; }

            protected override void Given()
            {
                UserId = 1;
            }

            protected override void When()
            {
                Result = SUT.Index(UserId);
            }

            [Test]
            public void ThenItDisplaysTheTimesheetPage()
            {
                Result.ShouldBeType<ViewResult>();
            }

        }

        public class WhenATimesheetIsSubmittedAndTheUserHasAManager : SpecsFor<TimesheetController>
        {
            protected ActionResult Result { get; set; }
            protected TimesheetModel TimesheetModel { get; set; }

            protected int UserId { get; set; }
            protected DateTime TimesheetDate { get; set; }
            protected int HoursWorked { get; set; }

            protected string EmailAddress { get; set; }
            protected Manager Manager { get; set; }

            protected override void Given()
            {
                UserId = 1;
                TimesheetDate = new DateTime(2018,1,1);
                HoursWorked = 1;
                EmailAddress = "test@test.com";

                TimesheetModel = new TimesheetModel
                {
                    UserId = UserId,
                    Date = TimesheetDate,
                    Hours = HoursWorked
                };

                Manager = new Manager
                {
                    EmailAddress = EmailAddress
                };

                GetMockFor<IUserRepository>().Setup(r => r.GetManager(UserId)).Returns(Manager);
            }

            protected override void When()
            {
                Result = SUT.Index(TimesheetModel);
            }

            [Test]
            public void AndTheTimesheetIsSaved()
            {
                GetMockFor<IUserRepository>().Verify(r => r.GetManager(UserId));
            }

            [Test]
            public void AndAnEmailIsSentToTheManager()
            {
                GetMockFor<IEmailService>().Verify(e => e.SendEmail(Manager.EmailAddress));
            }

            [Test]
            public void TheItRedirectsToReportPage()
            {
                Result.ShouldBeType<RedirectToRouteResult>();
                var redirectToRouteResult = Result as RedirectToRouteResult;
                var controllerName = redirectToRouteResult.RouteValues["controller"];
                var actionName = redirectToRouteResult.RouteValues["action"];
                var userId = redirectToRouteResult.RouteValues["id"];

                actionName.ShouldEqual("Display");
                controllerName.ShouldEqual("Report");
                userId.ShouldEqual(UserId);
            }

        }

        public class WhenATimesheetIsSubmittedAndTheUserDoesNotHaveAManager : SpecsFor<TimesheetController>
        {
            protected ActionResult Result { get; set; }
            protected TimesheetModel TimesheetModel { get; set; }

            protected int UserId { get; set; }
            protected DateTime TimesheetDate { get; set; }
            protected int HoursWorked { get; set; }

            protected override void Given()
            {
                UserId = 1;
                TimesheetDate = new DateTime(2018, 1, 1);
                HoursWorked = 1;

                TimesheetModel = new TimesheetModel
                {
                    UserId = UserId,
                    Date = TimesheetDate,
                    Hours = HoursWorked
                };
                
            }

            protected override void When()
            {
                Result = SUT.Index(TimesheetModel);
            }

            [Test]
            public void AndTheTimesheetIsSaved()
            {
                GetMockFor<IUserRepository>().Verify(r => r.GetManager(UserId));
            }

            [Test]
            public void AndAnEmailIsNotSentToTheManager()
            {
                GetMockFor<IEmailService>().Verify(e => e.SendEmail(It.IsAny<string>()), Times.Never);
            }

            [Test]
            public void TheItRedirectsToReportPage()
            {
                Result.ShouldBeType<RedirectToRouteResult>();
                var redirectToRouteResult = Result as RedirectToRouteResult;
                var controllerName = redirectToRouteResult.RouteValues["controller"];
                var actionName = redirectToRouteResult.RouteValues["action"];
                var userId = redirectToRouteResult.RouteValues["id"];

                actionName.ShouldEqual("Display");
                controllerName.ShouldEqual("Report");
                userId.ShouldEqual(UserId);
            }

        }
    }
}
