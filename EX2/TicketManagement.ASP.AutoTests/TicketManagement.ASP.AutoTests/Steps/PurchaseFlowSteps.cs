using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TicketManagement.ASP.AutoTests.WebPages;

namespace TicketManagement.ASP.AutoTests.Steps
{
    [Binding]
    class PurchaseFlowSteps
    {
        //http://localhost:58949/

        private static EventsHomePage EventsHomePage => PageFactory.Instance.Get<EventsHomePage>();
        private static PurchaseHistoryPage PurchaseHistoryPage => PageFactory.Instance.Get<PurchaseHistoryPage>();

        [When(@"User buy the first free seat")]
        public void WhenUserBuyTheSeat()
        {
            EventsHomePage.Open();
            var ep = EventsHomePage.ToFirstEvent();
            ep.FirstSeat.Click();
            ep.BuyRadio.Click();
            ep.Submit.Click();
            PurchaseHistoryPage.Open();
        }

        [Then(@"Purchase history contains any seats")]
        public void ThenPurchaseHistoryContainsAnySeats()
        {
            Assert.That(PurchaseHistoryPage.Purchases.FindElements(OpenQA.Selenium.By.CssSelector("div")).Count > 0);
        }

        [Then(@"Purchase history not contains any seats")]
        public void ThenPurchaseHistoryNotContainsAnySeats()
        {
            Assert.That(PurchaseHistoryPage.Purchases.FindElements(OpenQA.Selenium.By.CssSelector("div")).Count == 0);
        }
    }
}
