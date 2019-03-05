using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagement.ASP.AutoTests.WebPages
{
    class SetLocalePage : Page
    {
        public SetLocalePage(IWebDriver driver) : base(driver){ }

        public IWebElement Locale => FindByCss(@"select[name=""locale""]");
        public IWebElement Submit => FindByCss(@"input[type=""submit""]");

        public void Open()
        {
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            Driver.Url = "http://localhost:58949/UserPage/SetLocale";
        }

        public void SetLocale(string language)
        {
            Locale.Click();
            FindByCss("option[value='" + language + "']").Click();
            Submit.Click();
        }

    }
}
