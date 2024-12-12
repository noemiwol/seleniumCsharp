using OpenQA.Selenium.Support.UI;
using SeleniumCSharp.FunctionalTests.Models;
using SeleniumCSharp.FunctionalTests.PageComponents;
using SeleniumCSharp.FunctionalTests.Pages;

namespace SeleniumCSharp.FunctionalTests.Tests
{
    public class ExpertPageTests : BaseTest
    {
        private ExpertsPage expertsPage;
        private ExpertDetailsModal expertDetailsModal;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            expertsPage = new ExpertsPage(_driver);
            expertDetailsModal = new ExpertDetailsModal(_driver);
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
    }
}