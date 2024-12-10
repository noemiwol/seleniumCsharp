using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumCSharp.FunctionalTests.Pages
{
    internal class CourseContentPage
    {
        private IWebDriver _driver;

        public CourseContentPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public string GetNameTraining()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            IWebElement trainingElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//h1[contains(text(), 'Dziennik świetlicy – odkryj jego potencjał')]")));
            return trainingElement.Text;
        }

        public void ClickWatchTrainingButton()
        {
            Thread.Sleep(2000); // Czekaj 2 sekund
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            IWebElement watchButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".MuiButton-root.MuiLoadingButton-root.MuiButton-containedPrimary")));
            watchButton.Click();
        }

        public void ClickWatchTrailerButton()
        {
            Thread.Sleep(2000); // Czekaj 2 sekund
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            IWebElement watchButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(), 'Zobacz zwiastun')]")));
            watchButton.Click();
        }

        public void ClickWatchTrainingButtonAfterLogged()
        {
            Thread.Sleep(2000); // Czekaj 2 sekund
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            IWebElement watchButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".MuiButton-root.MuiLoadingButton-root.MuiButton-containedPrimary")));
            watchButton.Click();

            // Krótkie opóźnienie, aby dać wideo czas na rozpoczęcie
            Thread.Sleep(2000); // Czekaj 2 sekund
        }
    }
}