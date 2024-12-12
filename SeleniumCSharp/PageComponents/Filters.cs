using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumCSharp.FunctionalTests.PageComponents
{
    public class Filters
    {
        private IWebDriver _driver;

        public Filters(IWebDriver driver)
        {
            _driver = driver;
        }

        public void SelectFilterByName(string filterName)
        {
            try
            {
                var filters = _driver.FindElements(By.CssSelector("label.MuiFormControlLabel-root"));

                foreach (var filter in filters)
                {
                    var labelText = filter.Text.Trim();
                    if (labelText.Equals(filterName, StringComparison.OrdinalIgnoreCase))
                    {
                        ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", filter);

                        filter.Click();
                        return;
                    }
                }

                throw new NoSuchElementException($"Filter with name '{filterName}' not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SelectFilterByName: {ex.Message}");
                throw;
            }
        }

        public void ClickApplyFilters()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            var filterButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("button[type='submit']")));
            filterButton.Click();
        }

        public void ClickClearFilters()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            var clearButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("button:contains('Wyczyść filtry')")));
            clearButton.Click();
        }

        public bool IsFilterSelected(string filterName)
        {
            try
            {
                var filters = _driver.FindElements(By.CssSelector("label.MuiFormControlLabel-root"));

                foreach (var filter in filters)
                {
                    var labelText = filter.Text.Trim();
                    if (labelText.Equals(filterName, StringComparison.OrdinalIgnoreCase))
                    {
                        var checkbox = filter.FindElement(By.CssSelector("input[type='checkbox']"));
                        return checkbox.Selected;
                    }
                }

                throw new NoSuchElementException($"Filter with name '{filterName}' not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in IsFilterSelected: {ex.Message}");
                throw;
            }
        }
    }
}