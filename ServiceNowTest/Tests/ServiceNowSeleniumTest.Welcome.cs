using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;
using System.Threading;
using System.Drawing.Imaging;


namespace ServiceNowTest
{
    public partial class ServiceNowSeleniumTest
    {
        

        [TestMethod()]
        [TestCategory("Welcome Page")]
        public void testLinkToLogin()
        {
            //Given the user goes to the welcome page
            GurukulaWelcomePage welcomePage = new GurukulaWelcomePage(this.driver);
            welcomePage.Navigate();

            //When the user clicks on the login link
            welcomePage.GoToLogin();

            //Then we should be redirected to the login page
            GurukulaLoginPage loginPage = new GurukulaLoginPage (this.driver);
            Assert.IsTrue(driver.Url.ToLowerInvariant().Equals(loginPage.url.ToLowerInvariant()), "Not redirected to the login page");
            
        }

        [TestMethod()]
        [TestCategory("Welcome Page")]
        public void testLinkToLoginFromDropdown()
        {
            //Given the user goes to the welcome page
            GurukulaWelcomePage welcomePage = new GurukulaWelcomePage(this.driver);
            welcomePage.Navigate();

            //When the user clicks on the login link from the Dropdown
            welcomePage.GoToLoginFromDropdown();

            //Then we should be redirected to the login page
            GurukulaLoginPage loginPage = new GurukulaLoginPage(this.driver);
            Helper.WaintUntilPageIsLoaded(driver);
            Assert.IsTrue(driver.Url.ToLowerInvariant().Equals(loginPage.url.ToLowerInvariant()), "Not redirected to the login page");
        }

        [TestMethod()]
        [TestCategory("Welcome Page")]
        public void testLinkToRegistration()
        {
            //Given the user goes to the welcome page
            GurukulaWelcomePage welcomePage = new GurukulaWelcomePage(this.driver);
            welcomePage.Navigate();

            //When the user clicks on the login link
            welcomePage.GoToLoginRegistration();

            //Then we should be redirected to the login page
            GurukulaRegistrationPage registrationPage = new GurukulaRegistrationPage(this.driver);
            Assert.IsTrue(driver.Url.ToLowerInvariant().Equals(registrationPage.url.ToLowerInvariant()), "Not redirected to the login page");

        }



    }
}
