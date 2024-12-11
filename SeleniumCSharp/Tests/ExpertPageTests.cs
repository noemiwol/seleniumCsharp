using SeleniumCSharp.FunctionalTests.Pages;
using SeleniumCSharp.FunctionalTests.PageComponents;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumCSharp.FunctionalTests.Models;
using Microsoft.Extensions.Configuration;

namespace SeleniumCSharp.FunctionalTests.Tests
{
    public class ExpertPageTests
    {
        private IWebDriver _driver;
        private NavMenu navMenu;
        private ExpertsPage expertsPage;
        private ExpertDetailsModal expertDetailsModal;
        private IConfiguration _configuration;

        public ExpertPageTests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            _configuration = builder.Build();
        }

        [SetUp]
        public void Setup()
        {
            string baseUrl = _configuration["BaseUrl"];
            _driver = new ChromeDriver();

            navMenu = new NavMenu(_driver);
            expertsPage = new ExpertsPage(_driver);
            expertDetailsModal = new ExpertDetailsModal(_driver);

            _driver.Manage().Window.Maximize();
            _driver.Url = baseUrl;
        }

        [Test]
        public void TestGoToTrainingPage_When_ClickExpertsOnNavMenu()
        {
            // Arrange
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            // Act
            navMenu.SelectExperts();
            wait.Until(d => d.Url.Contains("/nasi-eksperci"));
            string currentUrl = _driver.Url;

            // Assert
            Assert.That(currentUrl.Contains("/nasi-eksperci"));
        }

        [Test]
        public void TestExpertsPageHeaderIsCorrect()
        {
            // Arrange
            string expectedHeader = "EKSPERCI AKADEMII LIBRUS";

            // Act
            navMenu.SelectExperts();
            string actualHeader = expertsPage.GetHeader();

            // Assert
            Assert.That(actualHeader, Is.EqualTo(expectedHeader));
        }

        [Test]
        public void TestCheckingShortDescription()
        {
            // Arrange
            var expertData = TestExpertProfileProcessor.LoadFromJson("../../../Resources/experts.json");
            var expert = expertData.expert[0];
            string nameSurname = expert.NameSurname;
            string expectedShortDescription = expert.ShortDescription;

            // Act
            navMenu.SelectExperts();
            string actualShortDescription = expertsPage.GetShortDescription(nameSurname);

            // Assert
            Assert.That(actualShortDescription, Is.EqualTo(expectedShortDescription));
        }

        [Test]
        public void TestCheckingDescription()
        {
            // Arrange
            var expertData = TestExpertProfileProcessor.LoadFromJson("../../../Resources/experts.json");
            var expert = expertData.expert[0];
            string expectedDescription = expert.Description;

            // Act
            navMenu.SelectExperts();
            expertsPage.ClickMoreInfoButton();
            string actualDescription = expertDetailsModal.GetDescription();

            // Assert
            Assert.That(actualDescription, Is.EqualTo(expectedDescription));
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
