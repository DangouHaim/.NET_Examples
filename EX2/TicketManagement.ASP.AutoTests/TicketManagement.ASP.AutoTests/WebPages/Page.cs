using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TicketManagement.ASP.AutoTests.WebPages
{
    public abstract class Page
    {
        protected IWebDriver Driver;

        protected Page(IWebDriver driver)
        {
            Driver = driver;
        }

        protected IWebElement FindByCss(string css)
        {
            new WebDriverWait(Driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementIsVisible(By.CssSelector(css)));
            return Driver.FindElement(By.CssSelector(css));
        }

        protected IWebElement FindByPath(string xpath)
        {
            new WebDriverWait(Driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementIsVisible(By.XPath(xpath)));
            return Driver.FindElement(By.XPath(xpath));
        }

    }
}
