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
        [TestCategory("Login")]//Using categories we can pick with testrunner which group of test we want to use at class level (so before the tests)
        //[ExpectedException(typeof(System.DivideByZeroException))]
        public void testLoginWithValidCredentials()
        {
            //Given the user is at the login page
            GurukulaLoginPage loginPage = new GurukulaLoginPage(this.driver);
            loginPage.Navigate();

            //When the user logins with valid credentials
            loginPage.Login(Properties.TestData.User1,Properties.TestData.Pwd1);
            GurukulaLoggedInWelcomePage loggedIn = new GurukulaLoggedInWelcomePage(this.driver);

            //Then the user should end int the logged version of the welcome page
            Helper.WaintUntilPageIsLoaded(driver);
            Assert.IsTrue(driver.Url.ToLowerInvariant().Equals(loggedIn.url.ToLowerInvariant()),"Not redirected to the welcome page");
            Assert.IsTrue(loggedIn.isLoginSuccessAlertDisplayed(), "Alert not being displayed");

        }

        [TestMethod()]
        [TestCategory("Login")]//Using categories we can pick with testrunner which group of test we want to use at class level (so before the tests)
        //[ExpectedException(typeof(System.DivideByZeroException))]
        public void testLogOut()
        {
            //Given the user is logged in
            GurukulaLoginPage loginPage = new GurukulaLoginPage(this.driver);
            loginPage.Navigate();
            loginPage.Login(Properties.TestData.User1, Properties.TestData.Pwd1);
            Helper.WaintUntilPageIsLoaded(driver);

            //When the user decides to logout
            GurukulaLoggedInWelcomePage loggedIn = new GurukulaLoggedInWelcomePage(this.driver);
            loggedIn.Logout();
            Helper.WaintUntilPageIsLoaded(driver);
            GurukulaWelcomePage welcomePage = new GurukulaWelcomePage (this.driver);

            //Then the user should end int the welcome page
            Assert.IsTrue(welcomePage.isLoginLinkDisplayed(), "Login link is not displayed");
        }

        [TestMethod()]
        [TestCategory("Login")]//Using categories we can pick with testrunner which group of test we want to use at class level (so before the tests)
        public void testLoginWithNonValidCredentials()
        {
            //Given the user is at the login page
            GurukulaLoginPage loginPage = new GurukulaLoginPage(this.driver);
            loginPage.Navigate();

            //When the user tries to login with invalid credentials
            loginPage.Login(Properties.TestData.NonRegisteredUser, Properties.TestData.NonRegisteredPwd);

            //Then the user should end int the logged version of the welcome page
            Helper.WaintUntilPageIsLoaded(driver);
            Assert.IsTrue(driver.Url.ToLowerInvariant().Equals(loginPage.url.ToLowerInvariant()), "Not stayed on the login page");
            Assert.IsTrue(loginPage.isAuthenticationErrorDisplayed(), "Alert not being displayed");

        }
        
    }
}
