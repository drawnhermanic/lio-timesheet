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

        public class WhenAnInvalidUserModelIsPosted : SpecsFor<AccountController>
        {
            protected ActionResult Result { get; set; }
            protected UserModel UserModel { get; set; }


            protected override void Given()
            {
                UserModel = new UserModel();

                SUT.ModelState.AddModelError("test", "test");
            }

            protected override void When()
            {
                Result = SUT.LogIn(UserModel);
            }

            [Test]
            public void ThenItRedirectsToLogInPage()
            {
                Result.ShouldBeType<ViewResult>();
            }

            [Test]
            public void AndTheUserIsNotSaved()
            {
                GetMockFor<IUserRepository>().Verify(r => r.Save(UserModel.UserName, It.IsAny<string>()), 
                    Times.Never);
            }
        }

        public class WhenAUserModelIsPosted : SpecsFor<AccountController>
        {
            protected ActionResult Result { get; set; }
            protected UserModel UserModel { get; set; }

            protected int UserId { get; set; }

            protected override void Given()
            {
                UserModel = new UserModel
                {
                    UserName = "Test",
                    Password = "Password"
                };

                UserId = 1;

                GetMockFor<IUserRepository>().Setup(r => r.Save(UserModel.UserName, It.IsAny<string>()))
                    .Returns(UserId);
            }

            protected override void When()
            {
                Result = SUT.LogIn(UserModel);
            }

            [Test]
            public void ThenItRedirectsToTimesheetPage()
            {
                Result.ShouldBeType<RedirectToRouteResult>();

                var redirectToRouteResult = Result as RedirectToRouteResult;
                var controllerName = redirectToRouteResult.RouteValues["controller"];
                var actionName = redirectToRouteResult.RouteValues["action"];
                var userId = redirectToRouteResult.RouteValues["id"];

                actionName.ShouldEqual("Index");
                controllerName.ShouldEqual("Timesheet");
                userId.ShouldEqual(UserId);
            }

            [Test]
            public void AndTheUserIsSaved()
            {
                GetMockFor<IUserRepository>().Verify(r => r.Save(UserModel.UserName, It.IsAny<string>()));
            }
        }
    }
}
