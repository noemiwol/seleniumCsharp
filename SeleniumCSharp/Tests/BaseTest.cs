using NUnit.Framework;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumCSharp.FunctionalTests.PageComponents;
using SeleniumCSharp.FunctionalTests.Pages;
using SeleniumCSharp.PageComponents;

namespace SeleniumCSharp.FunctionalTests
{
    public abstract class BaseTest
    {
        protected IWebDriver _driver;
        protected IConfiguration _configuration;

        protected HomePage homePage;
        protected NavMenu navMenu;
        protected SearchField searchField;
        protected TrainingsPage trainingsPage;
        protected CourseContentPage courseContentPage;
        protected UnloggedUserModal modal;
        protected VideoPlayer videoPlayer;

        public BaseTest()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            _configuration = builder.Build();
        }

        [SetUp]
        public virtual void SetUp()
        {
            string baseUrl = _configuration["BaseUrl"];
            _driver = new ChromeDriver();

            // Inicjalizacja komponentów strony
            homePage = new HomePage(_driver);
            navMenu = new NavMenu(_driver);
            searchField = new SearchField(_driver);
            trainingsPage = new TrainingsPage(_driver);
            courseContentPage = new CourseContentPage(_driver);
            modal = new UnloggedUserModal(_driver);
            videoPlayer = new VideoPlayer(_driver);

            _driver.Manage().Window.Maximize();
            _driver.Url = baseUrl;
        }

        [TearDown]
        public virtual void TearDown()
        {
            if (_driver != null)
            {
                Thread.Sleep(1000); 
                _driver.Quit();
                _driver.Dispose();
            }
        }
    }
}
