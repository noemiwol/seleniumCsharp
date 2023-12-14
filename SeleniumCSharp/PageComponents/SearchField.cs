using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumCSharp.FunctionalTests.PageComponents
{
    internal class SearchField
    {
        private IWebDriver _driver;

        public SearchField(IWebDriver driver)
        {
            _driver = driver;
        }

        public void EnterNameTraining(string name)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            IWebElement searchField = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("input.MuiInputBase-input")));
            searchField.Clear();
            searchField.SendKeys(name);
            // Czekaj 2 sekund
            Thread.Sleep(2000); 



        }

    }
}
