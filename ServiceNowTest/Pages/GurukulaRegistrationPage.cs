using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support;
using OpenQA.Selenium;

namespace ServiceNowTest
{
    class GurukulaRegistrationPage
    {
        private readonly IWebDriver driver;
        public readonly string url = string.Format(@"http://{0}:{1}/#/register", Properties.TestData.ServerUrl, Properties.TestData.Port);
        // @"http://127.0.0.1:8080/#/register";

        public GurukulaRegistrationPage(IWebDriver browser)
        {
            this.driver = browser;
            PageFactory.InitElements(browser, this);
        }

        [FindsBy(How = How.CssSelector, Using = @"[name=""login""]")]
        public IWebElement LoginInput { get; set; }

        [FindsBy(How = How.CssSelector, Using = @"[name=""email""]")]
        public IWebElement EmailInput { get; set; }

        [FindsBy(How = How.CssSelector, Using = @"[name=""password""]")]
        public IWebElement PasswordInput { get; set; }

        [FindsBy(How = How.CssSelector, Using = @"[name=""confirmPassword""]")]
        public IWebElement ConfirmPasswordInput { get; set; }

        [FindsBy(How = How.XPath, Using = @"html/body/div[3]/div[1]/div/div/div/form/button")]
        public IWebElement RegisterButton { get; set; }

        ///
        //body > div.container > div.well.ng-scope > div > div > div > form > button

        public void Navigate()
        {
            this.driver.Navigate().GoToUrl(this.url);
        }

        public Boolean isRegisterButtonEnabled()
        {
            return this.RegisterButton.Enabled;
        }

        public void Register(String name, String password, String email)
        {
            this.LoginInput.SendKeys(name);
            this.EmailInput.SendKeys(email);
            this.PasswordInput.SendKeys(password);
            this.ConfirmPasswordInput.SendKeys(password);
            this.RegisterButton.Click();
        }

        public void NoFilledRegistration(String name)
        {
            this.LoginInput.SendKeys(name);
            this.RegisterButton.Click();
        }
    }
}
