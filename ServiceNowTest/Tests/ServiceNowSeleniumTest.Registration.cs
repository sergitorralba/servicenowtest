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
        [TestCategory("Registration")]//Using categories we can pick with testrunner which group of test we want to use at class level (so before the tests)
        //[ExpectedException(typeof(System.DivideByZeroException))]
        public void testForgetPasswordLink()
        {
            //Given the user is at the login page
            GurukulaLoginPage loginPage = new GurukulaLoginPage(this.driver);
            loginPage.Navigate();

            //When the user clicks on the forgot Password link
            loginPage.ForgetPasswordLink.Click();

            //Then the user should end in the reset password page
            Helper.WaintUntilPageIsLoaded(driver);
            GurukulaResetPasswordPage resetPwdPage = new GurukulaResetPasswordPage(this.driver);
            Assert.IsTrue(driver.Url.ToLowerInvariant().Equals(resetPwdPage.url.ToLowerInvariant()), "Not stayed on the login page");
            
        }

        [TestMethod()]
        [TestCategory("Registration")]
        public void testRegisterUser()
        {
            //Given the user goes to the registration page
            GurukulaRegistrationPage registration = new GurukulaRegistrationPage(this.driver);
            registration.Navigate();

            //When fills the form and clicks on the Register botton
            var timeStamp = Helper.GetTimestamp();
            registration.Register(Properties.TestData.NewUser+timeStamp, Properties.TestData.NewPassword+timeStamp,timeStamp+Properties.TestData.NewEmail);

            //Then we should be redirected to the login page
            GurukulaLoginPage loginPage = new GurukulaLoginPage(this.driver);
            Helper.WaintUntilPageIsLoaded(driver);
            Assert.IsTrue(driver.Url.ToLowerInvariant().Equals(loginPage.url.ToLowerInvariant()), "Not redirected to the login page");
        }

        [TestMethod()]
        [TestCategory("Registration")]
        public void testRegisterButtonNotEnabled()
        {
            //Given the user goes to the registration page
            GurukulaRegistrationPage registration = new GurukulaRegistrationPage(this.driver);
            registration.Navigate();

            //When fills the form and clicks on the Register botton
            var timeStamp = Helper.GetTimestamp();
            registration.NoFilledRegistration(Properties.TestData.NewUser + timeStamp);

            //Then the button should not be enabled
            GurukulaLoginPage loginPage = new GurukulaLoginPage(this.driver);
            Helper.WaintUntilPageIsLoaded(driver);
            Assert.IsFalse(registration.isRegisterButtonEnabled(), "The button was enabled before it was needed");
        }

    }
}
