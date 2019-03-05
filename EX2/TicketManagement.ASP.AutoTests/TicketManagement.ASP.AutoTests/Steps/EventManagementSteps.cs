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
    class EventManagementSteps
    {
        private static EditEventPage EditEventPage => PageFactory.Instance.Get<EditEventPage>();
        private static LoginPage LoginPage => PageFactory.Instance.Get<LoginPage>();
        private static SetLocalePage SetLocalePage => PageFactory.Instance.Get<SetLocalePage>();
        private static EventManagementPage EventManagementPage => PageFactory.Instance.Get<EventManagementPage>();

        [Given(@"Manager is on event management page")]
        public void GivenManagerIsOnEventManagementPage()
        {
            LoginPage.Open();
            LoginPage.Login("manager", "managerboss");
            SetLocalePage.Open();
            SetLocalePage.SetLocale("en-us");
            EventManagementPage.Open();
        }

        [When(@"Manager selects layout")]
        public void ManagerSelectsLayout()
        {
            EventManagementPage.SelectLayout.Click();
        }

        [When(@"Manager enter the event name ""(.*)""")]
        public void ManagerEnterTheEventName(string inputString)
        {
            EventManagementPage.Name.SendKeys(inputString);
        }

        [When(@"Manager enter the event description ""(.*)""")]
        public void ManagerEnterTheEventDescription(string inputString)
        {
            EventManagementPage.Description.SendKeys(inputString);
        }

        [When(@"Manager enter the event date and time ""(.*)""")]
        public void ManagerEnterTheEventDateAndTime(string inputString)
        {
            EventManagementPage.EventDate.SendKeys(inputString);
        }

        [When(@"Manager enter the event thumbnail ""(.*)""")]
        public void ManagerEnterTheEventThumbnail(string inputString)
        {
            EventManagementPage.File.SendKeys(inputString);
        }

        [When(@"Manager create event")]
        public void ManagerCreateEvent()
        {
            EventManagementPage.Add.Click();
        }

        [Then(@"Manager can see event named ""(.*)""")]
        public void ManagerCanSeeEventNamed(string inputString)
        {
            Assert.That(EventManagementPage.FindEventByName(inputString) != null);
        }

        [Then(@"Manager can not see event named ""(.*)""")]
        public void ManagerCanNotSeeEventNamed(string inputString)
        {
            Assert.That(EventManagementPage.FindEventByName(inputString) == null);
        }

        [When(@"Manager delete selected event with name ""(.*)""")]
        public void ManagerDeleteSelectedEvent(string inputString)
        {
            var v = EventManagementPage.SelectEventByName(inputString);
            if (v != null)
            {
                v.Click();
                EventManagementPage.Delete.Click();
            }
        }

        [When(@"Manager edits ""(.*)""")]
        public void ManagerEdits(string inputString)
        {
            EventManagementPage.EditEventByName(inputString).Click();
        }

        [When(@"Manager change name to ""(.*)""")]
        public void ManagerChangeNameTo(string inputString)
        {
            EditEventPage.Name.Clear();
            EditEventPage.Name.SendKeys(inputString);
        }

        [When(@"Manager change date to ""(.*)""")]
        public void ManagerChangeDateTo(string inputString)
        {
            EditEventPage.EventDate.Clear();
            EditEventPage.EventDate.SendKeys(inputString);
        }

        [When(@"Manager save changes")]
        public void ManagerSaveChanges()
        {
            EditEventPage.Submit.Click();
            EventManagementPage.Open();
        }
        
        [Then(@"Manager can not see event edit button ""(.*)""")]
        public void ManagerCanNotSeeEventEditButton(string inputString)
        {
            Assert.That(EventManagementPage.EditEventByName(inputString) == null);
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            GivenManagerIsOnEventManagementPage();
            ManagerSelectsLayout();
            ManagerDeleteSelectedEvent("TestEvent");
            ManagerSelectsLayout();
            ManagerDeleteSelectedEvent("R");
            ManagerSelectsLayout();
            ManagerDeleteSelectedEvent("R2");
        }

        [AfterScenario]
        public void AfterScenario()
        {
            GivenManagerIsOnEventManagementPage();
            ManagerSelectsLayout();
            ManagerDeleteSelectedEvent("TestEvent");
            ManagerSelectsLayout();
            ManagerDeleteSelectedEvent("R");
            ManagerSelectsLayout();
            ManagerDeleteSelectedEvent("R2");
        }
    }
}
