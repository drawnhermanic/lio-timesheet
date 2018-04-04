using System.Web.Mvc;
using NUnit.Framework;
using Should;
using SpecsFor;
using Timesheet.Controllers;
using Timesheet.Models;

namespace Timesheet.Unit.Tests
{
    public class AccountControllerTests
    {
        public class WhenCallingLogIn : SpecsFor<AccountController>
        {
            protected ActionResult Result { get; set; }

            protected override void When()
            {
                Result = SUT.LogIn();
            }

            [Test]
            public void ThenItDisplaysTheLogInPage()
            {
                Result.ShouldBeType<ViewResult>();
            }

        }

        public class WhenAUserModelIsPosted : SpecsFor<AccountController>
        {
            protected ActionResult Result { get; set; }
            protected UserModel UserModel { get; set; }


            protected override void Given()
            {
                UserModel = new UserModel
                {
                    UserName = "Test",
                    Password = "Password"
                };
            }

            protected override void When()
            {
                Result = SUT.LogIn(UserModel);
            }

            [Test]
            public void TheItRedirectsToTimesheetPage()
            {
                Result.ShouldBeType<RedirectToRouteResult>();

                var redirectToRouteResult = Result as RedirectToRouteResult;
                var controllerName = redirectToRouteResult.RouteValues["controller"];
                var actionName = redirectToRouteResult.RouteValues["action"];
                var userId = redirectToRouteResult.RouteValues["id"];

                actionName.ShouldEqual("Index");
                controllerName.ShouldEqual("Timesheet");
                userId.ShouldBeNull();
            }

        }
    }
}
