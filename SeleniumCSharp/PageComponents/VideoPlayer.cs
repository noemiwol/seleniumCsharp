using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumCSharp.PageComponents
{
    public class VideoPlayer
    {
        private IWebDriver _driver;

        public VideoPlayer(IWebDriver driver)
        {
            _driver = driver;
        }

        public TimeSpan GetVideoPlaybackTime()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));

            // Poczekaj na widoczność elementu wideo
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("video.css-1o37g1z")));

            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;

            // Kliknięcie wideo, aby zainicjować odtwarzanie
            IWebElement videoElement = _driver.FindElement(By.CssSelector("video.css-1o37g1z"));
            videoElement.Click();

            // Wymuszenie odtwarzania wideo za pomocą JavaScript
            js.ExecuteScript("document.querySelector('video.css-1o37g1z').play();");

            // Poczekaj chwilę, aby wideo rozpoczęło odtwarzanie
            System.Threading.Thread.Sleep(1000);

            // Pobierz czas odtwarzania
            var currentTimeObject = js.ExecuteScript("return document.querySelector('video.css-1o37g1z').currentTime;");
            double currentTime = Convert.ToDouble(currentTimeObject);

            if (currentTime <= 0)
            {
                throw new Exception("Wideo nie rozpoczęło odtwarzania.");
            }

            // Zwróć czas odtwarzania jako TimeSpan
            return TimeSpan.FromSeconds(currentTime);
        }
    }
}