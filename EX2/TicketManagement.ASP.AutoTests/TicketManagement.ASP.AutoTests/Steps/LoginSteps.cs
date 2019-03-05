using NUnit.Framework;
using System.Threading;
using TechTalk.SpecFlow;
using TicketManagement.ASP.AutoTests.WebPages;

namespace TicketManagement.ASP.AutoTests.Steps
{
    [Binding]
    public class LoginSteps
    {
        private static LoginPage LoginPage => PageFactory.Instance.Get<LoginPage>();
        private static SetLocalePage SetLocalePage => PageFactory.Instance.Get<SetLocalePage>();


        [Given(@"User is on login page without language preparing")]
        public void GivenUserIsOnHomePageWithoutLanguagePreparing()
        {
            LoginPage.Open();
        }

        [Given(@"User is on login page")]
        public void GivenUserIsOnLoginPage()
        {
            LoginPage.Open();
            LoginPage.Login("user", "userboss");
            SetLocalePage.Open();
            SetLocalePage.SetLocale("en-us");
            LoginPage.Open();
        }

        [Given(@"User without balance is on login page")]
        public void GivenUserWithoutBalanceIsOnLoginPage()
        {
            LoginPage.Open();
            LoginPage.Login("user2", "userboss2");
            SetLocalePage.Open();
            SetLocalePage.SetLocale("en-us");
            LoginPage.Open();
        }

        [When(@"Enters ""(.*)"" to user name input")]
        public void WhenEntersToUserNameInput(string inputString)
        {
            LoginPage.Email.Click();
            LoginPage.Email.Clear();
            LoginPage.Email.SendKeys(inputString);
        }

        [When(@"Enters ""(.*)"" to password field")]
        public void WhenEntersToPasswordField(string inputString)
        {
            LoginPage.Password.Click();
            LoginPage.Password.Clear();
            LoginPage.Password.SendKeys(inputString);
        }

        [When(@"Clicks Sign In button on login form")]
        public void WhenClicksSignInButtonOnLoginForm()
        {
            LoginPage.Submit.Click();
        }

        [Then(@"Login form has error ""(.*)""")]
        public void ThenLoginFormHasError(string expectedErrorMsg)
        {
            if(LoginPage.LogoutForm.Text == expectedErrorMsg)
            {
                Assert.That(true);
            }
            else
            {
                Assert.That(LoginPage.FormError.Text == expectedErrorMsg);
            }
        }
    }
}
