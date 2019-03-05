using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;

namespace TicketManagement.ASP.AutoTests.Utils
{
    public static class DriverFactory
    {
        public static IWebDriver GetDriver()
        {
            var driver = new FirefoxDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromMilliseconds(25000);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(1500);
            driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromMilliseconds(2000);
            return driver;
        }
    }
}
