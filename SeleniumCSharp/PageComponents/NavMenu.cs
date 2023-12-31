﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumCSharp.FunctionalTests.PageComponents
{
    internal class NavMenu
    {
        private IWebDriver _driver;


        public NavMenu(IWebDriver driver)
        {
            this._driver = driver;
        }

        public void SelectIndividualOffer()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            IWebElement individualOfferMenuOption = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("//a[contains(text(), 'Dla Ciebie')]")));
            individualOfferMenuOption.Click();

        }
        public void SelectSchoolOffer()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            IWebElement schoollOfferMenuOption = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("//a[contains(text(), 'Dla szkoły')]")));
            schoollOfferMenuOption.Click();

        }
        public void SelectTrainings()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            IWebElement trainingsMenuOption = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("//a[contains(text(), 'Szkolenia')]")));
            trainingsMenuOption.Click();
        }
        public void SelectExperts()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            IWebElement expertsMenuOption = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("//a[contains(text(), 'Eksperci')]")));
            expertsMenuOption.Click();
        }
    }
}
