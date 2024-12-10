﻿using Microsoft.Extensions.Configuration;
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
            // Pobieranie baseUrl z nowej konfiguracji
            string baseUrl = _configuration["BaseUrl"];

            // Tworzenie instancji WebDriver
            _driver = new ChromeDriver();

            // Inicjalizacja obiektów stron
            homePage = new HomePage(_driver);
            navMenu = new NavMenu(_driver);
            searchField = new SearchField(_driver);
            trainingsPage = new TrainingsPage(_driver);
            courseContentPage = new CourseContentPage(_driver);
            modal = new UnloggedUserModal(_driver);
            videoPlayer = new VideoPlayer(_driver);

            // Konfiguracja przeglądarki
            _driver.Manage().Window.Maximize();
            _driver.Url = baseUrl;
        }

        [Test]
        public void TestGoToTrainingPage_When_ClickTrainingsOnNavMenu()
        {
            // Kliknięcie przycisku Szkolenia w navManu
            navMenu.SelectTrainings();

            // Oczekiwanie na przekierowanie
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Url.Contains("/szkolenia"));

            // Pobranie aktualnego URL
            string currentUrl = _driver.Url;

            // Sprawdzenie, czy URL zawiera określony tekst
            Assert.That(currentUrl.Contains("/szkolenia"));
        }

        [Test]
        public void TestVerifySearchResultsForValidInput()
        {
            // Kliknięcie przycisku Szkolenia w navManu
            navMenu.SelectTrainings();

            // Wpisz nazwę szkolenia
            var trainingData = TestTrainingData.LoadFromJson("../../../Resources/trainings.json");

            // Użyj danych  szkolenia
            var training = trainingData.training[0];

            searchField.EnterNameTraining(training.Name);

            string expectedTrainingName = "Zajęcia specjalistyczne w module Dzienniki zajęć dodatkowych";
            string actualTrainingName = trainingsPage.GetResultSearch();

            Assert.That(actualTrainingName, Is.EqualTo(expectedTrainingName));
        }

        [Test]
        public void TestRedirectionToCorrectTrainingPage_When_ClickingSearchResult()
        {
            // Kliknięcie przycisku Szkolenia w navManu
            navMenu.SelectTrainings();

            // Wpisz nazwę szkolenia
            var trainingData = TestTrainingData.LoadFromJson("../../../Resources/trainings.json");

            // Uży danych pierwszego szkolenia
            var trainingFirst = trainingData.training[1];

            //Wprowadzanie do wyszukiwarki danych
            searchField.EnterNameTraining(trainingFirst.Name);
            trainingsPage.ClickResultSearchTraining();

            string expectedTrainingName = "Dziennik świetlicy – odkryj jego potencjał";
            string actualTrainingName = courseContentPage.GetNameTraining();

            Assert.That(actualTrainingName, Is.EqualTo(expectedTrainingName));
        }
        [Test]
        public void TestVideoPlaybackTimeGreaterThanZero_When_ClikWatchTheTrailer()
        {
            // Kliknięcie przycisku Szkolenia w navMenu
            navMenu.SelectTrainings();

            // Wpisz nazwę szkolenia
            var trainingData = TestTrainingData.LoadFromJson("../../../Resources/trainings.json");

            // Uży danych pierwszego szkolenia
            var trainingFirst = trainingData.training[2];

            searchField.EnterNameTraining(trainingFirst.Name);
            trainingsPage.ClickResultSearchTraining();

            // Kliknięcie przycisku "Obejrzyj trailer"
            courseContentPage.ClickWatchTrailerButton();

            // Pobierz czas odtwarzania
            TimeSpan playbackTime = videoPlayer.GetVideoPlaybackTime();

            // Sprawdzenie, czy czas odtwarzania jest większy niż zero
            Assert.That(playbackTime > TimeSpan.Zero, $"Playback time {playbackTime} is not greater than 00:00:00");
        }




        [TearDown]
        public void TearDown()
        {
            if (_driver != null)
            {
                System.Threading.Thread.Sleep(2000);

                _driver.Quit();
                _driver.Dispose();
            }
        }
    }
}