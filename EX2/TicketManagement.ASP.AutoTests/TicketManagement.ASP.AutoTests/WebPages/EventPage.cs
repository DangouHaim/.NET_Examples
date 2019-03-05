using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagement.ASP.AutoTests.WebPages
{
    class EventPage : Page
    {
        public EventPage(IWebDriver driver) : base(driver){ }

        public IWebElement FirstSeat => FindByCss(@"input[type=""checkbox""]");
        public IWebElement BuyRadio => FindByCss(@"#From[value=""1""]");
        public IWebElement ToCartRadio => FindByCss(@"#From[value=""2""]");
        public IWebElement Submit => FindByCss(@"input[type=""Submit""]");

        public void Open()
        {
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            Driver.Url = "http://localhost:58949/Event";
        }
    }
}
