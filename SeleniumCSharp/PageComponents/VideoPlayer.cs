using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumCSharp.PageComponents
{
    internal class VideoPlayer
    {
        private IWebDriver _driver;
        public VideoPlayer(IWebDriver driver)
        {
            _driver = driver;
        }

        public string GetPlaybackTimeText()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));
            IWebElement playbackTimeElement;
            string initialPlaybackTime;

            // Początkowe sprawdzenie czasu odtwarzania
            playbackTimeElement = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("p.MuiTypography-body1.css-4ari97")));
            initialPlaybackTime = playbackTimeElement.Text;

            // Krótkie opóźnienie
            Thread.Sleep(2000); // Czekaj 2 sekundy

            // Ponowne sprawdzenie czasu odtwarzania
            playbackTimeElement = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("p.MuiTypography-body1.css-4ari97")));
            return playbackTimeElement.Text;
        }


    }
}
