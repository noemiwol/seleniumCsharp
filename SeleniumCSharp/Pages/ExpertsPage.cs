using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumCSharp.FunctionalTests.Pages
{
    internal class ExpertsPage
    {
        private IWebDriver _driver;

        public ExpertsPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public string GetCurrentUrl()
        {
            return _driver.Url;
        }

        public string GetHeader()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            IWebElement expertsElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("span.MuiTypography-root")));
            return expertsElement.Text;
        }

        public string GetShortDescription(string nameSurname)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            IWebElement expertInfo = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath($"//h4[contains(text(), '{nameSurname}')]/following-sibling::p")));

            return expertInfo.Text;
        }

        public void ClickMoreInfoButton()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            var moreInfoButtons = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector(".css-1d1dutt > div:nth-child(3) > button:nth-child(1)")));
            moreInfoButtons.First().Click();
        }
    }
}