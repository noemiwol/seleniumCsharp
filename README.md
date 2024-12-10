# Projekt Selenium C#

To repozytorium zawiera przykładową konfigurację do tworzenia i uruchamiania zautomatyzowanych testów end-to-end (E2E) z wykorzystaniem Selenium WebDriver w języku C#.

## Struktura projektu

```
.
├── SeleniumCSharp/        # Główny folder zawierający implementację testów
├── .vs/                   # Pliki konfiguracyjne Visual Studio
├── sampleSeleniumCSharp Tests.sln # Plik rozwiązania dla Visual Studio
└── README.md              # Dokumentacja (ten plik)
```

## Wymagania wstępne

Przed uruchomieniem testów upewnij się, że masz zainstalowane:

- **Visual Studio** (zalecana najnowsza wersja)
- **.NET Framework** (kompatybilny z projektem, np. .NET 6.0 lub nowszy)
- **Pakiety NuGet**:
  - Selenium.WebDriver
  - Selenium.Support
  - NUnit (lub inny framework testowy, jeśli używany)

Możesz zainstalować te zależności za pomocą Menedżera Pakietów NuGet w Visual Studio.

## Pierwsze kroki

### Klonowanie repozytorium

```bash
git clone https://github.com/noemiwol/seleniumCsharp.git
cd seleniumCsharp
```

### Otwarcie projektu

1. Otwórz Visual Studio.
2. Wczytaj plik rozwiązania `sampleSeleniumCSharp Tests.sln`.
3. Przywróć pakiety NuGet, jeśli zostaniesz o to poproszony.

### Uruchamianie testów

1. Skompiluj rozwiązanie, naciskając `Ctrl+Shift+B`.
2. Uruchom testy za pomocą Test Explorer w Visual Studio:
   - Przejdź do `Test > Test Explorer`.
   - Wybierz i uruchom testy.

### Tworzenie własnych testów

Aby stworzyć nowy test:

1. Dodaj nowy plik C# w folderze `SeleniumCSharp`.
2. Użyj poniższego szablonu:

```csharp
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumCSharp.Tests
{
    public class ExampleTest
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void SampleTest()
        {
            driver.Navigate().GoToUrl("https://example.com");
            Assert.AreEqual("Example Domain", driver.Title);
        }

        [TearDown]
        public void Teardown()
        {
            driver.Quit();
        }
    }
}
```

3. Zapisz i uruchom test w Test Explorer.

## Rozwiązywanie problemów

- **Nie znaleziono zależności:** Upewnij się, że wszystkie pakiety NuGet są zainstalowane i aktualne.
- **Błąd: Nie znaleziono sterownika:** Upewnij się, że plik wykonywalny WebDriver (np. ChromeDriver) znajduje się w zmiennej PATH systemu lub w katalogu projektu.
- **Problemy z kompatybilnością przeglądarki:** Użyj wersji WebDriver kompatybilnej z wersją Twojej przeglądarki.

## Wkład w projekt

Śmiało fork'uj to repozytorium i przesyłaj pull requesty, aby ulepszyć projekt. W przypadku większych zmian otwórz zgłoszenie, aby omówić, co chciałbyś zmienić.

## Licencja

Ten projekt jest licencjonowany na warunkach licencji MIT - szczegóły w pliku LICENSE.
