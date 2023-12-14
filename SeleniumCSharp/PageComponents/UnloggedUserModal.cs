using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumCSharp.FunctionalTests.PageComponents
{
    internal class UnloggedUserModal
    {
        private IWebDriver _driver;

        public UnloggedUserModal(IWebDriver driver)
        {
            _driver = driver;
        }

        public string GetModalHeader()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            IWebElement modalElement = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("h2.MuiDialogTitle-root")));
            return modalElement.Text;
        }
        public void ClickSignInButton()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            IWebElement button = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("a.MuiButton-fullWidth:nth-child(1)")));
            button.Click();

        }


    }
}
