using System;
using OpenQA.Selenium;
using TicketManagement.ASP.AutoTests.Steps;
using NUnit.Framework;
using System.Threading;

namespace TicketManagement.ASP.AutoTests.WebPages
{
    public class HomePage : Page
    {
        public HomePage(IWebDriver driver) : base(driver){}

        private static LoginPage LoginPage => PageFactory.Instance.Get<LoginPage>();

        public void Open()
        {
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            Driver.Url = "http://localhost:58949/";
        }

        public void SelectLanguage(string language)
        {
            LoginPage.Open();
            LoginPage.Login("admin", "adminboss").Open();
            Assert.That(true);
        }
    }
}
