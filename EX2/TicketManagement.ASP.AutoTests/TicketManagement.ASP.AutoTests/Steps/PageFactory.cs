using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using TicketManagement.ASP.AutoTests.Utils;
using TicketManagement.ASP.AutoTests.WebPages;

namespace TicketManagement.ASP.AutoTests.Steps
{
    [Binding]
    public class PageFactory
    {
        private static IWebDriver _driver;

        private PageFactory() { }

        private static readonly Lazy<PageFactory> Lazy = new Lazy<PageFactory>(() => new PageFactory());

        public static PageFactory Instance => Lazy.Value;

        public T Get<T>() where T : Page
        {
            object[] args = { _driver };
            return (T)Activator.CreateInstance(typeof(T), args);
        }

        [BeforeFeature]
        public static void OpenBrowser()
        {
            _driver = DriverFactory.GetDriver();
        }

        [AfterFeature]
        public static void CloseBrowser()
        {
            _driver.Close();
        }
    }
}
