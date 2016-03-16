using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceNowTest
{
	class GurukulaWelcomePage
	{
		private readonly IWebDriver driver;
		public readonly string url = string.Format(@"http://{0}:{1}", Properties.TestData.ServerUrl, Properties.TestData.Port);
		// @"http://127.0.0.1:8080";
		public GurukulaWelcomePage(IWebDriver browser)
		{
			this.driver = browser;
			PageFactory.InitElements(browser, this);
		}

		[FindsBy(How = How.XPath, Using = @"//*[@id=""navbar-collapse""]/ul/li[2]")]
		public IWebElement AccountDropdown { get; set; }

		[FindsBy(How = How.XPath, Using = "/html/body/div[3]/div[1]/div/div/div[2]/div/div[1]/a")]
		public IWebElement LoginLink { get; set; }

		[FindsBy(How = How.XPath, Using = "/html/body/div[3]/div[1]/div/div/div[2]/div/div[2]/a")]
		public IWebElement RegisterLink { get; set; }

		[FindsBy(How = How.XPath, Using = @"//*[@id=""navbar-collapse""]/ul/li[2]/ul/li[1]/a")]
		public IWebElement LoginLinkFromDropDown { get; set; }
		// 

		public void Navigate()
		{
			this.driver.Navigate().GoToUrl(this.url);            
		}

		public void GoToLoginFromDropdown()
		{
			this.AccountDropdown.Click();
			this.LoginLinkFromDropDown.Click();
		}

		public void GoToLogin()
		{
			this.LoginLink.Click();
		}

		public void GoToLoginRegistration()
		{
			this.RegisterLink.Click();
		}

		public Boolean isLoginLinkDisplayed()
		{
			return this.LoginLink.Displayed;
		}
	}
}
