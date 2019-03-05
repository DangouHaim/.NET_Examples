using System.Threading;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TicketManagement.ASP.AutoTests.WebPages;
using System;

namespace TicketManagement.ASP.AutoTests.Steps
{
    [Binding]
    public class LocalisationSteps
    {
        private static LoginPage LoginPage => PageFactory.Instance.Get<LoginPage>();
        private static SetLocalePage SetLocalePage => PageFactory.Instance.Get<SetLocalePage>();

        [When(@"User sets site language to ""(.*)""")]
        public void WhenUserSetsSiteLanguageTo(string language)
        {
            LoginPage.Open();
            LoginPage.Login("admin", "adminboss");
            SetLocalePage.Open();
            SetLocalePage.SetLocale(language);
        }

        [Then(@"Banner text is ""(.*)""")]
        public void ThenBannerTextIs(string bannerText)
        {
            Assert.That(LoginPage.LogoutForm.Text == bannerText);
            Console.WriteLine("Current banner text: " + bannerText);
        }
    }
}
