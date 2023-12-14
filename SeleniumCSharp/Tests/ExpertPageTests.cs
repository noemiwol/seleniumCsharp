using SeleniumCSharp.FunctionalTests.Pages;
using SeleniumCSharp.FunctionalTests.PageComponents;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumCSharp.FunctionalTests.Models;

namespace SeleniumCSharp.FunctionalTests.Tests
{
    internal class ExpertPageTests
    {
        private IWebDriver _driver;
        private NavMenu navMenu;
        private ExpertsPage expertsPage;
        private ExpertDetailsModal expertDetailsModal;

        [SetUp]
        public void Setup()
        {
            // Tworzenie instancji WebDriver
            _driver = new ChromeDriver();

            // Inicjalizacja obiektów stron
            navMenu = new NavMenu(_driver);
            expertsPage = new ExpertsPage(_driver);
            expertDetailsModal = new ExpertDetailsModal(_driver);

            // Konfiguracja przeglądarki
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl("https://akademia.librus.pl/");
        }

        [Test]
        public void TestGoToTrainingPage_When_ClickExpertssOnNavMenu()
        {
            // Kliknięcie przycisku Eksperci w navManu
            navMenu.SelectExperts();


            // Oczekiwanie na przekierowanie
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Url.Contains("/nasi-eksperci"));

            // Pobranie aktualnego URL
            string currentUrl = _driver.Url;

            // Sprawdzenie, czy URL zawiera określony tekst
            Assert.IsTrue(currentUrl.Contains("/nasi-eksperci"));
        }
        [Test]
        public void TestExpertsPageHeaderIsCorrect()
        {
            // Kliknięcie przycisku Eksperci w navManu
            navMenu.SelectExperts();


            string expectedHeader = "EKSPERCI AKADEMII LIBRUS";
            string actualHeader = expertsPage.GetHeader();

            Assert.That(actualHeader, Is.EqualTo(expectedHeader));
        }
        [Test]
        public void TestCheckingShortDescription()
        {
            // Kliknięcie przycisku Eksperci w navManu
            navMenu.SelectExperts();

            //

            // JSON experts
            var expertData = TestExpertProfileProcessor.LoadFromJson("../../../Resources/experts.json");

            // Uży danych experta
            var expert = expertData.expert[0];
            string nameSurname = expert.NameSurname;
            expertsPage.GetShortDescription(nameSurname);

            string expectedShortDescription = expert.ShortDescription;
            string actualShortDescription = expertsPage.GetShortDescription(nameSurname);

            // Sprawdzenie, czy wyświetla się określony tekst
            Assert.That(actualShortDescription, Is.EqualTo(expectedShortDescription));
        }

        [Test]
        public void TestCheckingDescription()
        {
            // Click on the 'Experts' button in navMenu
            navMenu.SelectExperts();

            // Load expert data from JSON
            var expertData = TestExpertProfileProcessor.LoadFromJson("../../../Resources/experts.json");
            var expert = expertData.expert[0]; // Use the first expert for testing

            expertsPage.ClickMoreInfoButton();

            // Pobieranie opisu z JSON
            string expectedDescription = expert.Description;
            // Pobieranie opisu z strony
            string actualDescription = expertDetailsModal.GetDescription();

            // Sprawdzenie, czy wyświetla się określony tekst
            Assert.That(actualDescription, Is.EqualTo(expectedDescription));
        }


        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}
