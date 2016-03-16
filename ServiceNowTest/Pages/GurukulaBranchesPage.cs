using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support;
using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace ServiceNowTest
{
    class GurukulaBranchesPage
    {
        private readonly IWebDriver driver;
        public readonly string url = string.Format(@"http://{0}:{1}/#/branch", Properties.TestData.ServerUrl, Properties.TestData.Port);
        // @"http://127.0.0.1:8080/#/branch";

        public GurukulaBranchesPage(IWebDriver browser)
        {
            this.driver = browser;
            PageFactory.InitElements(browser, this);
        }

        [FindsBy(How = How.CssSelector, Using = @"[data-target=""#saveBranchModal""]")]
        public IWebElement NewBranchButton { get; set; }

        [FindsBy(How = How.Id, Using = @"searchQuery")]
        public IWebElement SearchInput { get; set; }

        [FindsBy(How = How.CssSelector, Using = @"[ng-click=""search()""]")]
        public IWebElement SearchButton { get; set; }

        //These are from the modal
        [FindsBy(How = How.CssSelector, Using = @"[name=""name""]")]
        public IWebElement BranchName { get; set; }

        [FindsBy(How = How.CssSelector, Using = @"[name=""code""]")]
        public IWebElement BranchCode { get; set; }

        [FindsBy(How = How.CssSelector, Using = @"#saveBranchModal > div > div > form > div.modal-footer > button.btn.btn-primary")]
        public IWebElement SaveBranchButton { get; set; }

        //After the Search
        [FindsBy(How = How.CssSelector, Using = @"[ui-sref=""branchDetail({id:branch.id})""]")]
        public IWebElement BranchViewButton { get; set; }
        
        [FindsBy(How = How.CssSelector, Using = @"[ng-click=""showUpdate(branch.id)""]")]
        public IWebElement BranchEditButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = @"[ng-click=""delete(branch.id)""]")]
        public IWebElement BranchDeleteButton { get; set; }

        //Modal for Delete
        [FindsBy(How = How.CssSelector, Using = @"#deleteBranchConfirmation > div > div > form > div.modal-footer > button.btn.btn-danger")]
        public IWebElement BranchDeleteModalDeleteButton { get; set; }

        ///
        //body > div.container > div.well.ng-scope > div > div > div > form > button

        public void Navigate()
        {
            this.driver.Navigate().GoToUrl(this.url);
        }

        public Boolean isSaveBranchButton()
        {
            return this.SaveBranchButton.Enabled;
        }

        public void AddBranch(String branchName, String branchCode)
        {
            this.NewBranchButton.Click();
            Helper.WaintUntilPageIsLoaded(driver);
            this.BranchName.SendKeys(branchName);
            Helper.WaintUntilPageIsLoaded(driver);
            this.BranchCode.SendKeys(branchCode);
            this.SaveBranchButton.Click();
            Helper.WaintUntilPageIsLoaded(driver);
        }

        public void SearchBranch(String branchToSearch)
        {
            this.SearchInput.Clear();
            this.SearchInput.SendKeys(branchToSearch);
            this.SearchButton.Click();
            Helper.WaintUntilPageIsLoaded(driver);
        }


        public void ModifyBranch(String branchToSearch, String newName)
        {
            this.SearchBranch(branchToSearch);
            this.BranchEditButton.Click();
            this.BranchName.Clear();
            this.BranchName.SendKeys(newName);
            this.SaveBranchButton.Click();
            Helper.WaintUntilPageIsLoaded(driver);
        }

        public void DeleteBranch(String branchToSearch)
        {
            this.SearchBranch(branchToSearch);
            this.BranchDeleteButton.Click();
            this.BranchDeleteModalDeleteButton.Click();
            Helper.WaintUntilPageIsLoaded(driver);
        }
        public ReadOnlyCollection<IWebElement> GetResults()
        {
            return this.driver.FindElements(By.CssSelector(@"[ng-repeat=""branch in branches""]"));        
        }

        public int GetResultsCount()
        {
            return this.driver.FindElements(By.CssSelector(@"[ng-repeat=""branch in branches""]")).Count;
        }
    }
}


