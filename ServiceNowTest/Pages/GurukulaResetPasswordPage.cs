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
	class GurukulaResetPasswordPage
	{
		private readonly IWebDriver driver;
		public readonly string url = string.Format(@"http://{0}:{1}/#/reset/request", Properties.TestData.ServerUrl, Properties.TestData.Port);
		// @"http://127.0.0.1:8080/#/reset/request";

		public GurukulaResetPasswordPage(IWebDriver browser)
		{
			this.driver = browser;
			PageFactory.InitElements(browser, this);
		}

		[FindsBy(How = How.CssSelector, Using = @"body > div.container > div.well.ng-scope > div > div > div > form > div > input")]
		public IWebElement EmailInput { get; set; }

		[FindsBy(How = How.CssSelector, Using = @"body > div.container > div.well.ng-scope > div > div > div > form > button")]
		public IWebElement ResetPasswordButton { get; set; }

		public Boolean isResetPasswordButtonEnabled()
		{
			return this.ResetPasswordButton.Enabled;
		}
  
	}
}
