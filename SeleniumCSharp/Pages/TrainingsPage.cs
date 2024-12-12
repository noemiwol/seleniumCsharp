using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumCSharp.FunctionalTests.Pages
{
    public class TrainingsPage
    {
        private IWebDriver _driver;

        public TrainingsPage(IWebDriver driver)
        {
            _driver = driver;
        }
        public string GetCurrentUrl()
        {
            return _driver.Url;
        }


        public string GetResultSearch()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            IWebElement trainingElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("div.MuiTypography-root.MuiTypography-h5")));
            return trainingElement.Text;
        }

        public void ClickResultSearchTraining()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            IWebElement trainingElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("div.MuiTypography-root.MuiTypography-h5")));
            trainingElement.Click();
        }
        public List<string> GetAppliedFilters()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

                var appliedFilterElements = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                    .VisibilityOfAllElementsLocatedBy(By.CssSelector("div.applied-filters span.filter-name")));

                return appliedFilterElements.Select(element => element.Text).ToList();
            }
            catch (NoSuchElementException)
            {
                return new List<string>();
            }
            catch (WebDriverTimeoutException)
            {
                return new List<string>();
            }
        }
        public List<string> GetTrainingTitles()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
                var trainingElements = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(
                    By.CssSelector("div.MuiTypography-root.MuiTypography-h5.MuiTypography-gutterBottom.css-1ouvs95")));

                return trainingElements.Select(element => element.Text).ToList();
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("No training titles were found within the timeout.");
                return new List<string>();
            }
        }
        public bool AreTrainingsDisplayed()
        {
            try
            {
                return _driver.FindElements(By.CssSelector("div[data-testid^='list-item']")).Count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AreTrainingsDisplayed: {ex.Message}");
                return false;
            }
        }
        public string GetNoResultsDetails()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
                IWebElement noResultsDetails = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(
                    By.CssSelector("p.MuiTypography-root.MuiTypography-paragraph2--regular.MuiTypography-alignCenter.css-xpmu18")));
                return noResultsDetails?.Text ?? string.Empty;
            }
            catch (WebDriverTimeoutException ex)
            {
                Console.WriteLine($"Timeout while waiting for 'No results' details: {ex.Message}");
                return string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                return string.Empty;
            }
        }

        public bool IsNoResultsMessageDisplayed()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
                IWebElement noResultsMessage = wait.Until(driver =>
                {
                    var elements = driver.FindElements(By.CssSelector("h4.MuiTypography-root.MuiTypography-h4.css-1cs3xk9"));
                    foreach (var element in elements)
                    {
                        if (!string.IsNullOrEmpty(element.Text) && element.Text.Contains("Brak wyników wyszukiwania"))
                        {
                            return element;
                        }
                    }
                    return null;
                });

                return noResultsMessage != null;
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("Timeout: Element 'Brak wyników wyszukiwania' nie został znaleziony.");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                return false;
            }
        }

    }
}