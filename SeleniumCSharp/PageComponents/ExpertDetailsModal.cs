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
    internal class ExpertDetailsModal
    {
        private IWebDriver _driver;

        public ExpertDetailsModal(IWebDriver driver)
        {
            _driver = driver;
        }

        public string GetDescription()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            IWebElement descriptionElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//p[contains(@class, 'MuiTypography-paragraph3--regular') and contains(@class, 'css-sna552')]")));
            return descriptionElement.Text;
        }

    }
}
