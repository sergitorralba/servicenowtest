using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support;
using OpenQA.Selenium;

namespace ServiceNowTest
{
	class GurukulaLoginPage
	{
		private readonly IWebDriver driver;
		public readonly string url = string.Format(@"http://{0}:{1}/#/login", Properties.TestData.ServerUrl, Properties.TestData.Port);
			// @"http://127.0.0.1:8080/#/login";

		public GurukulaLoginPage(IWebDriver browser)
		{
			this.driver = browser;
			PageFactory.InitElements(browser, this);
		}

		[FindsBy(How = How.CssSelector, Using = @"[ng-show=""authenticationError""]")]
		public IWebElement AuthenticationError { get; set; }

		[FindsBy(How = How.Id, Using = @"username")]
		public IWebElement UsernameInput { get; set; }

		[FindsBy(How = How.Id, Using = @"password")]
		public IWebElement PasswordInput { get; set; }

		[FindsBy(How = How.XPath, Using = @"/html/body/div[3]/div[1]/div/div/div/form/button")]
		public IWebElement LoginButton { get; set; }

		[FindsBy(How = How.CssSelector, Using = @"body > div.container > div.well.ng-scope > div > div > div > div:nth-child(5) > a")]
		public IWebElement ForgetPasswordLink { get; set; }

		public void Navigate()
		{
			this.driver.Navigate().GoToUrl(this.url);
		}

		public void Login(String user, String pswd)
		{
			this.UsernameInput.SendKeys(user);
			this.PasswordInput.SendKeys(pswd);
			this.LoginButton.Click();
			Helper.WaintUntilPageIsLoaded(driver);
		}

		public Boolean isAuthenticationErrorDisplayed()
		{
			return this.AuthenticationError.Displayed;
		}

	}
}
