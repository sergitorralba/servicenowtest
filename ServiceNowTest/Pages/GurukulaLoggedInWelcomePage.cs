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
	class GurukulaLoggedInWelcomePage
	{
		private readonly IWebDriver driver;
		public readonly string url = string.Format(@"http://{0}:{1}/#/", Properties.TestData.ServerUrl, Properties.TestData.Port);
			// @"http://127.0.0.1:8080/#/";

		public GurukulaLoggedInWelcomePage(IWebDriver browser)
		{
			this.driver = browser;
			PageFactory.InitElements(browser, this);
		}
		//body > div.container > div.well.ng-scope > div > div > div.col-md-8 > div > div
		[FindsBy(How = How.ClassName, Using = @"alert-success")]
		public IWebElement LoginSuccessAlert { get; set; }

		[FindsBy(How = How.CssSelector, Using = @"#navbar-collapse > ul > li:nth-child(4)")]
		public IWebElement AccountDropdown { get; set; }

		[FindsBy(How = How.CssSelector, Using = @"[ng-click=""logout()""]")] 
		public IWebElement AccountDropdownLogout { get; set; }
		
		[FindsBy(How = How.CssSelector, Using = @"#navbar-collapse > ul > li.dropdown.pointer.ng-scope")]
		public IWebElement EntitiesDropdown { get; set; }

		[FindsBy(How = How.CssSelector, Using = @"[ui-sref=""branch""]")]
		public IWebElement EntitiesDropdownBranch { get; set; }

		[FindsBy(How = How.CssSelector, Using = @"[ui-sref=""staff""]")]
		public IWebElement EntitiesDropdownStaff { get; set; }


		public Boolean isLoginSuccessAlertDisplayed()
		{
			return this.LoginSuccessAlert.Displayed;
		}

        public void Navigate()
        {
            this.driver.Navigate().GoToUrl(this.url);
        }
		public void GoToStaff()
		{
			this.EntitiesDropdown.Click();
            Helper.WaintUntilPageIsLoaded(this.driver);
			this.EntitiesDropdownStaff.Click();
		}
		public void GoToBranch()
		{
			this.EntitiesDropdown.Click();
            Helper.WaintUntilPageIsLoaded(this.driver);
			this.EntitiesDropdownBranch.Click();
		}

		public void Logout()
		{
			this.AccountDropdown.Click();
            Helper.WaintUntilPageIsLoaded(this.driver);
			this.AccountDropdownLogout.Click();
		}
	}
}
