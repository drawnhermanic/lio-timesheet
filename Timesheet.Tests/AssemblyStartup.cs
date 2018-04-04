using System.Web.Routing;
using NUnit.Framework;
using SpecsFor.Mvc;

namespace Timesheet.Acceptance.Tests
{
    [SetUpFixture]
    public class AssemblyStartup
    {
        private SpecsForIntegrationHost _host;

        [SetUp]
        public void SetupTestRun()
        {
            var config = new SpecsForMvcConfig();
            config.UseIISExpress()
                .UsePort(44300)               
                .With(Project.Named("Timesheet"))
                .CleanupPublishedFiles();

            config.BuildRoutesUsing(r => RouteConfig.RegisterRoutes(RouteTable.Routes));

            //config.UseBrowser(BrowserDriver.InternetExplorer);
            config.UseBrowser(BrowserDriver.Chrome);
            //config.UseBrowser(BrowserDriver.Firefox);
            
            config.SetBrowserWindowSize(width: 1024, height: 768);

            _host = new SpecsForIntegrationHost(config);
            _host.Start();
        }

        [TearDown]
        public void TearDownTestRun()
        {
            _host.Shutdown();
        }
    }
}