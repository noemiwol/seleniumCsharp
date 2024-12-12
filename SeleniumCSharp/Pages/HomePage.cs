using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumCSharp.FunctionalTests.Pages
{
    public class HomePage
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

        public IWebElement GetLogo()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            return wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("a.MuiTypography-root.MuiTypography-inherit.MuiLink-root")));
        }

        public IWebElement GetMainMenu()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            return wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div.MuiStack-root")));
        }

        public IList<IWebElement> GetMainMenuLinks()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            var menu = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div.MuiStack-root")));
            return menu.FindElements(By.CssSelector("a"));
        }

        public IWebElement GetFooter()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            return wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div.MuiGrid-container.css-1xorlg6")));
        }

        public IReadOnlyCollection<IWebElement> GetFooterLinks()
        {
            return _driver.FindElements(By.CssSelector("footer a"));
        }

        public IList<IWebElement> GetFooterSocialIcons()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            var socialIconsContainer = wait.Until(ExpectedConditions.ElementExists(By.CssSelector(".MuiBox-root.css-1a88wtd")));

            return socialIconsContainer.FindElements(By.CssSelector("a"));
        }
    }
}