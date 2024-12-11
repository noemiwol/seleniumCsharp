using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumCSharp.FunctionalTests.Models;
using SeleniumCSharp.FunctionalTests.PageComponents;
using SeleniumCSharp.FunctionalTests.Pages;
using SeleniumCSharp.PageComponents;

namespace SeleniumCSharp.FunctionalTests.Tests
{
    public class PlayVideoAsLicensedUserTests
    {
        private IWebDriver _driver;
        private HomePage homePage;
        private NavMenu navMenu;
        private SearchField searchField;
        private TrainingsPage trainingsPage;
        private CourseContentPage courseContentPage;
        private UnloggedUserModal modal;
        private VideoPlayer videoPlayer;
        private IConfiguration _configuration;

        public PlayVideoAsLicensedUserTests()
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

            // Act
            navMenu.SelectTrainings();
            searchField.EnterNameTraining(training.Name);
            string actualTrainingName = trainingsPage.GetResultSearch();

            // Assert
            string expectedTrainingName = "Zajęcia specjalistyczne w module Dzienniki zajęć dodatkowych";
            Assert.That(actualTrainingName, Is.EqualTo(expectedTrainingName));
        }

        [Test]
        public void TestRedirectionToCorrectTrainingPage_When_ClickingSearchResult()
        {
            // Arrange
            var trainingData = TestTrainingData.LoadFromJson("../../../Resources/trainings.json");
            var trainingFirst = trainingData.training[1];

            // Act
            navMenu.SelectTrainings();
            searchField.EnterNameTraining(trainingFirst.Name);
            trainingsPage.ClickResultSearchTraining();
            string actualTrainingName = courseContentPage.GetNameTraining();

            // Assert
            string expectedTrainingName = "Dziennik świetlicy – odkryj jego potencjał";
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

        [TearDown]
        public void TearDown()
        {
            if (_driver != null)
            {
                Thread.Sleep(2000);
                _driver.Quit();
                _driver.Dispose();
            }
        }
    }
}