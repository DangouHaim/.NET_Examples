using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagement.ASP.AutoTests.Steps;

namespace TicketManagement.ASP.AutoTests.WebPages
{
    class EventsHomePage : Page
    {
        public EventsHomePage(IWebDriver driver) : base(driver){ }

        private static EventPage EventPage => PageFactory.Instance.Get<EventPage>();

        public IWebElement Event => FindByCss("#events>a");

        public void Open()
        {
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            Driver.Url = "http://localhost:58949/Event";
        }

        public EventPage ToFirstEvent()
        {
            Event.Click();
            return EventPage;
        }
    }
}
