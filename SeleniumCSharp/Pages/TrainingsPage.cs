using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumCSharp.FunctionalTests.Pages
{
    internal class TrainingsPage
    {
        private IWebDriver _driver;

        public TrainingsPage(IWebDriver driver)
        {
            _driver = driver;
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
    }
}