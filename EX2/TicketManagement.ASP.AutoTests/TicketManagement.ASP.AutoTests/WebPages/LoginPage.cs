using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagement.ASP.AutoTests.Steps;

namespace TicketManagement.ASP.AutoTests.WebPages
{
    class LoginPage : Page
    {
        public LoginPage(IWebDriver driver) : base(driver){ }

        private static HomePage HomePage => PageFactory.Instance.Get<HomePage>();

        public IWebElement Email => FindByCss("#Email");
        public IWebElement Password => FindByCss("#Password");
        public IWebElement Submit => FindByCss("#login");
        public IWebElement LogoutForm => FindByCss(@"#logoutForm>ul>li>a[title=""Manage""]");

        public IWebElement FormError => FindByCss(".validation-summary-errors");

        public void Open()
        {
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            Driver.Url = "http://localhost:58949/Account/Login";
        }

        public HomePage Login(string email, string password)
        {
            Email.Clear();
            Email.SendKeys(email);

            Password.Clear();
            Password.SendKeys(password);

            Submit.Click();

            return HomePage;
        }


    }
}
