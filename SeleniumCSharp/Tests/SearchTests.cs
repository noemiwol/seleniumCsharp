using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumCSharp.FunctionalTests;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumCSharp.FunctionalTests.PageComponents;
using SeleniumCSharp.FunctionalTests.Pages;
using SeleniumCSharp.FunctionalTests.Models;

namespace SeleniumCSharp.Tests
{
    public class SearchTests : BaseTest
    {
        private SearchField searchField;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            searchField = new SearchField(_driver);
        }
        private void NavigateToTrainingsPage()
        {
            navMenu.SelectTrainings();
        }

        [Test]
        public void TestSearchExistingTrainingShowsResults()
        {
           // Arrange
            NavigateToTrainingsPage();
            var trainingData = TestTrainingData.LoadFromJson("../../../Resources/trainings.json");
            var training = trainingData.training[0];
            string expectedTrainingName = "Zajęcia specjalistyczne w module Dzienniki zajęć dodatkowych";

            // Act
            searchField.EnterNameTraining(training.Name);
            string actualTrainingName = trainingsPage.GetResultSearch();

            // Assert
            Assert.That(actualTrainingName, Is.EqualTo(expectedTrainingName));
        }
        [Test]
        public void TestSearchNotExistingTrainingShowsNoResultsMessage()
        {
            // Arrange
            NavigateToTrainingsPage();
            string nonExistingTraining = "NonExistingTrainingName";

            // Act
            searchField.EnterNameTraining(nonExistingTraining);

            // Assert
            Assert.That(isNoResultsDisplayed, Is.True, "Expected 'Brak wyników wyszukiwania'.");
            Assert.That(noResultsDetails, Does.Contain("Nie znaleźliśmy szkoleń spełniających Twoje zapytanie. Upewnij się, czy nie ma w nim błędu lub zmień kryteria wyszukiwania."));
        }

    }
}

