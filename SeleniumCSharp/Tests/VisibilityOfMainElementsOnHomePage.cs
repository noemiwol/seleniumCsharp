using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumCSharp.FunctionalTests.PageComponents;
using SeleniumExtras.WaitHelpers;

namespace SeleniumCSharp.FunctionalTests.Pages
{
    public class VisibilityOfMainElementsOnHomePage
    {
        private IWebDriver _driver;
        private HomePage homePage;
        private NavMenu navMenu;
        private IConfiguration _configuration;

        [SetUp]
        public void Setup()
        {
            // Pobieranie baseUrl z nowej konfiguracji
            string baseUrl = _configuration["BaseUrl"];

            // Tworzenie instancji WebDriver
            _driver = new ChromeDriver();

            // Inicjalizacja obiektów stron
            homePage = new HomePage(_driver);
            navMenu = new NavMenu(_driver);
           

            // Konfiguracja przeglądarki
            _driver.Manage().Window.Maximize();
            _driver.Url = baseUrl;
        }
        [Test]
        public void TestCheckVisibilityOfLogoOnHomePage()
        {
        }
        [TearDown]
        public void TearDown()
        {
            if (_driver != null)
            {
                _driver.Quit();
                _driver.Dispose();
            }
        }
    }
}
