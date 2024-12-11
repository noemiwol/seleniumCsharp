using SeleniumCSharp.FunctionalTests.Pages;
using SeleniumCSharp.FunctionalTests.PageComponents;
using SeleniumCSharp.FunctionalTests.Models;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using SeleniumCSharp.PageComponents;

namespace SeleniumCSharp.FunctionalTests.Tests
{
    public class PlayVideoAsLicensedUserTests : BaseTest
    {
        private VideoPlayer videoPlayer;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            videoPlayer = new VideoPlayer(_driver);
        }

        [Test]
        public void TestGoToTrainingPage_When_ClickTrainingsOnNavMenu()
        {
            // Arrange
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            // Act
            navMenu.SelectTrainings();
            wait.Until(d => d.Url.Contains("/szkolenia"));
            string currentUrl = _driver.Url;

            // Assert
            Assert.That(currentUrl.Contains("/szkolenia"));
        }

        [Test]
        public void TestVerifySearchResultsForValidInput()
        {
            // Arrange
            var trainingData = TestTrainingData.LoadFromJson("../../../Resources/trainings.json");
            var training = trainingData.training[0];
            string expectedTrainingName = "Zajęcia specjalistyczne w module Dzienniki zajęć dodatkowych";

            // Act
            navMenu.SelectTrainings();
            searchField.EnterNameTraining(training.Name);
            string actualTrainingName = trainingsPage.GetResultSearch();

            // Assert
            Assert.That(actualTrainingName, Is.EqualTo(expectedTrainingName));
        }

        [Test]
        public void TestRedirectionToCorrectTrainingPage_When_ClickingSearchResult()
        {
            // Arrange
            var trainingData = TestTrainingData.LoadFromJson("../../../Resources/trainings.json");
            var trainingFirst = trainingData.training[1];
            string expectedTrainingName = "Dziennik świetlicy – odkryj jego potencjał";

            // Act
            navMenu.SelectTrainings();
            searchField.EnterNameTraining(trainingFirst.Name);
            trainingsPage.ClickResultSearchTraining();
            string actualTrainingName = courseContentPage.GetNameTraining();

            // Assert
            Assert.That(actualTrainingName, Is.EqualTo(expectedTrainingName));
        }

        [Test]
        public void TestVideoPlaybackTimeGreaterThanZero_When_ClikWatchTheTrailer()
        {
            // Arrange
            var trainingData = TestTrainingData.LoadFromJson("../../../Resources/trainings.json");
            var trainingFirst = trainingData.training[2];

            // Act
            navMenu.SelectTrainings();
            searchField.EnterNameTraining(trainingFirst.Name);
            trainingsPage.ClickResultSearchTraining();
            courseContentPage.ClickWatchTrailerButton();
            TimeSpan playbackTime = videoPlayer.GetVideoPlaybackTime();

            // Assert
            Assert.That(playbackTime > TimeSpan.Zero, $"Playback time {playbackTime} is not greater than 00:00:00");
        }
    }
}