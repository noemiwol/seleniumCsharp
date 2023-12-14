using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumCSharp.FunctionalTests.Pages
{
    internal class LoginPage
    {
        private IWebDriver _driver;

        public LoginPage(IWebDriver driver)
        {
            this._driver = driver;
        }

        public void EnterEmail(string email)
        {
            _driver.FindElement(By.Name("email")).SendKeys(email);
        }

        public void EnterPassword(string password)
        {
            _driver.FindElement(By.Name("password")).SendKeys(password);
        }
        public void ClickSignInButton()
        {
            _driver.FindElement(By.CssSelector("button.common__auth_btn--full-width")).Click();
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("user-menu-button")));
        }

    }
}
