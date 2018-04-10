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
    public class HashingHelpersTests
    {
        public class WhenHashing : SpecsFor<object>
        {
            protected string Input { get; set; }
            protected string ExpectedResult { get; set; }
            protected string Result { get; set; }

            protected override void Given()
            {
                Input = "Manager";
                ExpectedResult = "8B2085F74DFA9C78A23B7D573C23D27D6D0B0E50C82A9B13138B193325BE3814";
            }

            protected override void When()
            {
                Result = HashingHelper.GetStringSha256Hash(Input);
            }

            [Test]
            public void ThenItHashesTheString()
            {
                Result.ShouldEqual(ExpectedResult);
            }

        }
    }
}
