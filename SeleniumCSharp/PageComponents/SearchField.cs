using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumCSharp.FunctionalTests.PageComponents
{
    public class SearchField
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
        public void ToggleFilterByName(string filterName)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            IWebElement filterCheckbox = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath($"//span[text()='{filterName}']/preceding-sibling::span/input")));
            filterCheckbox.Click();
        }

        public void ApplyFilters()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            IWebElement filterButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("button[type='submit']")));
            filterButton.Click();
        }

        public void ClearFilters()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            IWebElement clearFiltersButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//button[span[text()='Wyczyść filtry']]")));
            clearFiltersButton.Click();
        }
    }
}
