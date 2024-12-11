using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumCSharp.FunctionalTests.PageComponents;
using SeleniumCSharp.FunctionalTests.Pages;


namespace SeleniumCSharp.FunctionalTests.Tests
{
    public class VisibilityOfMainElementsOnHomePage
    {
        private IWebDriver _driver;
        private HomePage _homePage;
        private IConfiguration _configuration;
        private NavMenu _navMenu;

        public VisibilityOfMainElementsOnHomePage()
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

            // Inicjalizacja obiektów strony
            _homePage = new HomePage(_driver);
            _navMenu = new NavMenu(_driver);

            // Konfiguracja przeglądarki
            _driver.Manage().Window.Maximize();
            _driver.Url = baseUrl;
        }
        [Test]
        public void TestCheckVisibilityOnHomePageLogo()
        {
            // Arrange
            var logo = _homePage.GetLogo();

            // Act
            // Act: Weryfikacja widoczności elementów
            bool isLogoVisible = logo.Displayed;

            // Assert
            Assert.That(isLogoVisible, Is.True, "Logo nie jest widoczne.");

        }
        [Test]
        public void TestMainMenuLinksVisibilityAndUrls()
        {
            // Arrange
            IList<IWebElement> menuLinks = _homePage.GetMainMenuLinks();

            // Oczekiwane linki
            var expectedLinks = new Dictionary<string, string>
    {
        { "Dla Ciebie", "/oferta-indywidualna" },
        { "Dla szkoły", "/oferta-dla-szkol" },
        { "Mam voucher", "/voucher" },
        { "Szkolenia", "/szkolenia" },
        { "Eksperci", "/nasi-eksperci" }
    };

            // Act & Assert
            foreach (var link in menuLinks)
            {
                string linkText = link.Text;
                string href = link.GetAttribute("href");

                Assert.That(link.Displayed, Is.True, $"Link '{linkText}' nie jest widoczny.");
                Assert.That(href.Contains(expectedLinks[linkText]),
                    $"Link '{linkText}' nie prowadzi do oczekiwanej sekcji. Oczekiwano: {expectedLinks[linkText]}, znaleziono: {href}");
            }
        }
        [Test]
        public void TestFooterVisibilityAndLinks()
        {
            // Arrange
            var footerLinks = _homePage.GetFooterLinks();
            var expectedFooterLinks = new Dictionary<string, string>
    {
        { "LIBRUS Synergia", "https://portal.librus.pl/rodzina" },
        { "e-Sekretariat", "https://sekretariat.librus.pl/" },
        { "e-Świadectwa", "https://swiadectwa.librus.pl/" },
        { "indywidualni.pl", "https://indywidualni.pl/" },
        { "e-Biblioteka", "https://biblioteka.librus.pl/" },
        { "librus.pl", "https://www.librus.pl/" },
        { "Librus Szkoła", "https://portal.librus.pl/szkola" },
        { "Librus Rodzina", "https://portal.librus.pl/rodzina" },
        { "Aplikacja Librus Nauczyciel", "https://konto.librus.pl/nauczyciel" },
        { "Konto LIBRUS", "https://konto.librus.pl/" },
        { "Biuro prasowe", "https://www.librus.pl/biuro-prasowe/kontakt-dla-mediow/" },
        { "Pomoc", "https://konto.librus.pl/pomoc/akademia-librus" },
        { "Kariera", "https://www.librus.pl/kariera/" },
        { "Kontakt", "https://www.librus.pl/kontakt/" },
        { "Informacja prawna", "https://www.librus.pl/informacja-prawna/" },
        { "Polityka prywatności", "https://synergia.librus.pl/polityka-prywatnosci/" },
        { "Regulamin Panelu Dyrektora", "https://portal.librus.pl/akademia-librus/regulamin-panelu-dyrektorskiego" }
    };

            // Act & Assert
            foreach (var link in footerLinks)
            {
                string linkText = !string.IsNullOrEmpty(link.Text.Trim())
                    ? link.Text.Trim()
                    : link.GetAttribute("aria-label") ?? link.GetAttribute("title") ?? "Nieznany link";
                string href = link.GetAttribute("href");

                Console.WriteLine($"Sprawdzanie linku: Text='{linkText}', Href='{href}'");

                // Sprawdź widoczność
                Assert.That(link.Displayed, Is.True, $"Link '{linkText}' nie jest widoczny.");

                // Sprawdź, czy link ma tekst lub atrybut zastępczy
                Assert.That(!string.IsNullOrEmpty(linkText), Is.True, $"Link ma pusty tekst. Href: {href}");

                // Sprawdź, czy link jest w oczekiwanych danych
                Assert.That(expectedFooterLinks.ContainsKey(linkText), Is.True, $"Link '{linkText}' nie jest oczekiwanym linkiem.");

                // Sprawdź poprawność adresu URL
                Assert.That(href, Is.EqualTo(expectedFooterLinks[linkText]), $"Link '{linkText}' prowadzi do niepoprawnej lokalizacji. Znaleziono: {href}");
            }
        }
        [Test]
        public void TestFooterSocialIconsVisibility()
        {
            // Arrange
            IList<IWebElement> socialIcons = _homePage.GetFooterSocialIcons();

            // Act
            var iconHrefs = socialIcons.Select(icon => icon.GetAttribute("href")).ToList();

            // Assert
            Assert.That(socialIcons, Is.Not.Null.And.Not.Empty, "Nie znaleziono ikon społecznościowych w stopce.");
            Assert.That(iconHrefs, Contains.Item("https://www.facebook.com/Librus-Szko%C5%82a-113258060081485/"), "Nie znaleziono linku do Facebooka.");
            Assert.That(iconHrefs, Contains.Item("https://twitter.com/libruspl"), "Nie znaleziono linku do Twittera.");
        }
        private void VerifyNavMenuLink(Action clickAction, string expectedUrl)
        {
            // Wykonaj akcję (kliknięcie w link w menu)
            clickAction();

            // Pobierz aktualny URL
            string actualUrl = _driver.Url;

            // Sprawdź, czy URL jest zgodny z oczekiwanym
            Assert.That(actualUrl, Is.EqualTo(expectedUrl), $"Niepoprawny URL. Oczekiwano: {expectedUrl}, znaleziono: {actualUrl}");
        }

        [Test]
        public void TestNavMenuLinks()
        {
            // Arrange
            var navMenu = _navMenu;

            // Act & Assert
            VerifyNavMenuLink(() => navMenu.SelectIndividualOffer(), "https://akademia.librus.pl/oferta-indywidualna");
            VerifyNavMenuLink(() => navMenu.SelectSchoolOffer(), "https://akademia.librus.pl/oferta-dla-szkol");
            VerifyNavMenuLink(() => navMenu.SelectTrainings(), "https://akademia.librus.pl/szkolenia");
            VerifyNavMenuLink(() => navMenu.SelectExperts(), "https://akademia.librus.pl/nasi-eksperci");
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
