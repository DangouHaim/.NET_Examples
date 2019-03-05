using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagement.ASP.AutoTests.WebPages
{
    class EventManagementPage : Page
    {
        public EventManagementPage(IWebDriver driver) : base(driver){ }

        public IWebElement SelectLayout => FindByCss(@"#SelectLayout");
        public IWebElement Name => FindByCss(@"#Name");
        public IWebElement Description => FindByCss(@"#Description");
        public IWebElement EventDate => FindByCss(@"#EventDate");
        public IWebElement File => FindByCss(@"#file");
        public IWebElement Add => FindByCss(@"input[value=""Add""]");
        public IWebElement Delete => FindByCss(@"input[value=""Delete""]");

        public IWebElement FindEventByName(string Name)
        {
            try
            {
                return FindByPath("//a[text()='" + Name + "']");
            }
            catch
            {
                return null;
            }
        }

        public IWebElement SelectEventByName(string Name)
        {
            try
            {
                return FindByPath("//div[span[a[text()='" + Name + "']]]//input[@type='checkbox']");
            }
            catch
            {
                return null;
            }
        }
        public IWebElement EditEventByName(string Name)
        {
            try
            {
                return FindByPath("//div[span[a[text()='" + Name + "']]]//span[@class='EditEvent']//a");
            }
            catch
            {
                return null;
            }
        }

        public void Open()
        {
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            Driver.Url = "http://localhost:58949/Event/ManageEvents";
        }
    }
}
