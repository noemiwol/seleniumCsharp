using SeleniumCSharp.FunctionalTests.PageComponents;

namespace SeleniumCSharp.FunctionalTests.Tests
{
    public class SearchTests : BaseTest
    {
        private SearchField searchField;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            searchField = new SearchField(_driver);
            navMenu.SelectTrainings();
        }

        [Test]
        public void TestSearchExistingTrainingShowsResults()
        {
            // Arrange
            string trainingName = "Monitorowanie realizacji podstawy programowej";
            string expectedTrainingName = "Monitorowanie realizacji podstawy programowej";

            // Act
            searchField.EnterNameTraining(trainingName);
            string actualTrainingName = trainingsPage.GetResultSearch();

            // Assert
            Assert.That(actualTrainingName, Is.EqualTo(expectedTrainingName));
        }

        [Test]
        public void TestSearchLongQueryShowsNoResults()
        {
            // Arrange
            string longQuery = new string('A', 61);

            // Act
            searchField.EnterNameTraining(longQuery);
            bool isNoResultsDisplayed = trainingsPage.IsNoResultsMessageDisplayed();
            string noResultsDetails = trainingsPage.GetNoResultsDetails();

            // Assert
            Assert.That(isNoResultsDisplayed, Is.True, "Expected 'Brak wyników wyszukiwania'.");
            Assert.That(noResultsDetails, Does.Contain("Nie znaleźliśmy szkoleń spełniających Twoje zapytanie. Upewnij się, czy nie ma w nim błędu lub zmień kryteria wyszukiwania."));
        }

        [Test]
        public void TestSearchSpecialCharactersShowsNoResults()
        {
            // Arrange
            string specialCharsQuery = "!@#$%^&*()";

            // Act
            searchField.EnterNameTraining(specialCharsQuery);
            bool isNoResultsDisplayed = trainingsPage.IsNoResultsMessageDisplayed();
            string noResultsDetails = trainingsPage.GetNoResultsDetails();

            // Assert
            Assert.That(isNoResultsDisplayed, Is.True, "Expected 'Brak wyników wyszukiwania'.");
            Assert.That(noResultsDetails, Does.Contain("Nie znaleźliśmy szkoleń spełniających Twoje zapytanie. Upewnij się, czy nie ma w nim błędu lub zmień kryteria wyszukiwania."));
        }

        [Test]
        public void TestSearchNotExistingTrainingShowsNoResultsMessage()
        {
            // Arrange
            string nonExistingTraining = "NonExistingTrainingName";

            // Act
            searchField.EnterNameTraining(nonExistingTraining);
            bool isNoResultsDisplayed = trainingsPage.IsNoResultsMessageDisplayed();
            string noResultsDetails = trainingsPage.GetNoResultsDetails();

            // Assert
            Assert.That(isNoResultsDisplayed, Is.True, "Expected 'Brak wyników wyszukiwania'.");
            Assert.That(noResultsDetails, Does.Contain("Nie znaleźliśmy szkoleń spełniających Twoje zapytanie. Upewnij się, czy nie ma w nim błędu lub zmień kryteria wyszukiwania."));
        }
    }
}