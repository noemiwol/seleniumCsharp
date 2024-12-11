using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumCSharp.FunctionalTests.PageComponents
{
    public class Filters
    {
        private readonly IWebDriver _driver;

        public Filters(IWebDriver driver)
        {
            _driver = driver;
        }

        public void SelectFilter(string filterName)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            IWebElement filter = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector($"input[name='{filterName}']")));
            filter.Click();
        }

        public void ApplyFilters()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            IWebElement filterButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("button[type='submit']")));
            filterButton.Click();
        }

        public void ClearFilters()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            IWebElement clearButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("button[type='button'] span:contains('Wyczyść filtry')")));
            clearButton.Click();
        }
    }
}
