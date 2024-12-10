using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumCSharp.FunctionalTests.Pages
{
    internal class HomePage
    {
        private IWebDriver _driver;

        public HomePage(IWebDriver driver)
        {
            this._driver = driver;
        }

        public string GetTitle()
        {
            return _driver.Title;
        }

        public string GetCurrentUrl()
        {
            return _driver.Url;
        }

        public string GetHeaderText()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            IWebElement header = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("//h1[contains(text(), 'Poznaj specjalistyczną platformę szkoleniową')]")));
            return header.Text;
        }
    }
}