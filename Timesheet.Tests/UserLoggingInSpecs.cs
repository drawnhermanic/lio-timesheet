using NUnit.Framework;
using SpecsFor;
using SpecsFor.Mvc;
using SpecsFor.Mvc.Helpers;
using Timesheet.Controllers;
using Timesheet.Models;

namespace Timesheet.Acceptance.Tests
{
    public class UserLoggingInSpecs
    {
        public class WhenAUserRegisters : SpecsFor<MvcWebApp>
        {
            protected override void Given()
            {
                SUT.NavigateTo<AccountController>(c => c.LogIn());
            }

            protected override void When()
            {
                SUT.FindFormFor<UserModel>()                    
                    .Field(m => m.UserName).SetValueTo("Test User")
                    .Field(m => m.Password).SetValueTo("P@ssword!")                    
                    .Submit();
            }

            [Test]
            public void TheItRedirectsToTimesheetPage()
            {
                SUT.Route.ShouldMapTo<TimesheetController>(c => c.Index());
            }
            
        }
    }
}
