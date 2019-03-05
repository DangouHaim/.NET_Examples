using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagement.ASP.AutoTests.WebPages
{
    class PurchaseHistoryPage : Page
    {
        public PurchaseHistoryPage(IWebDriver driver) : base(driver){ }

        public IWebElement Purchases => FindByCss(@"#Purchases");

        public void Open()
        {
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            Driver.Url = "http://localhost:58949/Purchase";
        }
    }
}
