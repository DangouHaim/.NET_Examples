using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagement.ASP.AutoTests.WebPages
{
    class EditEventPage : Page
    {
        public EditEventPage(IWebDriver driver) : base(driver){ }

        public IWebElement Name => FindByCss(@"#Name");
        public IWebElement Description => FindByCss(@"#Description");
        public IWebElement EventDate => FindByCss(@"#EventDate");
        public IWebElement File => FindByCss(@"#file");
        public IWebElement Submit => FindByCss(@"input[type=""Submit""]");

        public void Open()
        {
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            Driver.Url = "http://localhost:58949/Event/EditEvent/";
        }
    }
}
