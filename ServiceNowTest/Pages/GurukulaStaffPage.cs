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
    class GurukulaStaffPage
    {
        private readonly IWebDriver driver;
        public readonly string url = string.Format(@"http://{0}:{1}/#/staff", Properties.TestData.ServerUrl, Properties.TestData.Port);
        // @"http://127.0.0.1:8080/#/staff";

        public GurukulaStaffPage(IWebDriver browser)
        {
            this.driver = browser;
            PageFactory.InitElements(browser, this);
        }

        [FindsBy(How = How.CssSelector, Using = @"[data-target=""#saveStaffModal""]")]
        public IWebElement NewStaffButton { get; set; }

        [FindsBy(How = How.Id, Using = @"searchQuery")]
        public IWebElement SearchInput { get; set; }

        [FindsBy(How = How.CssSelector, Using = @"[ng-click=""search()""]")]
        public IWebElement SearchButton { get; set; }

        //These are from the modal
        [FindsBy(How = How.CssSelector, Using = @"[name=""name""]")]
        public IWebElement StaffName { get; set; }

        [FindsBy(How = How.CssSelector, Using = @"[name=""related_branch""]")]
        public IWebElement BranchDropdoown { get; set; }
                                       
        [FindsBy(How = How.CssSelector, Using = @"#saveStaffModal > div > div > form > div.modal-footer > button.btn.btn-primary")]
        public IWebElement SaveStaffButton { get; set; }

        //After the Search
        [FindsBy(How = How.CssSelector, Using = @"[ui-sref=""staffDetail({id:staff.id})""]")]
        public IWebElement StaffViewButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = @"[ng-click=""showUpdate(staff.id)""]")]
        public IWebElement StaffEditButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = @"[ng-click=""delete(staff.id)""]")]
        public IWebElement StaffDeleteButton { get; set; }

        //Modal for Delete
        [FindsBy(How = How.CssSelector, Using = @"#deleteStaffConfirmation > div > div > form > div.modal-footer > button.btn.btn-danger")]
        public IWebElement StaffModalDeleteButton { get; set; }

        ///
        //body > div.container > div.well.ng-scope > div > div > div > form > button

        public void Navigate()
        {
            this.driver.Navigate().GoToUrl(this.url);
        }

        public Boolean isSaveBranchButton()
        {
            return this.SaveStaffButton.Enabled;
        }

        public void AddStaff(String name, String branchName)
        {
            this.NewStaffButton.Click();
            this.StaffName.SendKeys(name);
            Helper.WaintUntilPageIsLoaded(driver);
            IWebElement selectElem = this.BranchDropdoown;
            foreach (IWebElement option in selectElem.FindElements(By.TagName("option")))
            {
                if (option.Text.Equals(branchName))
                {
                    option.Click();
                }
            }
            this.SaveStaffButton.Click();
            Helper.WaintUntilPageIsLoaded(driver);
        }

        public void AddStaffWithoutBranch(String name)
        {
            this.NewStaffButton.Click();
            this.StaffName.SendKeys(name);
            Helper.WaintUntilPageIsLoaded(driver);
            this.SaveStaffButton.Click();
            Helper.WaintUntilPageIsLoaded(driver);
        }

        public void SearchStaff(String branchToSearch)
        {
            this.SearchInput.Clear();
            this.SearchInput.SendKeys(branchToSearch);
            this.SearchButton.Click();
            Helper.WaintUntilPageIsLoaded(driver);
        }


        public void ModifyStaff(String nameToSearch, String newName)
        {
            this.SearchStaff(nameToSearch);
            this.StaffEditButton.Click();
            this.StaffName.Clear();
            this.StaffName.SendKeys(newName);
            this.SaveStaffButton.Click();
            Helper.WaintUntilPageIsLoaded(driver);
        }

        public void DeleteStaff(String branchToSearch)
        {
            this.SearchStaff(branchToSearch);
            this.StaffDeleteButton.Click();
            this.StaffModalDeleteButton.Click();
            Helper.WaintUntilPageIsLoaded(driver);
        }
        public ReadOnlyCollection<IWebElement> GetResults()
        {
            return this.driver.FindElements(By.CssSelector(@"[ng-repeat=""staff in staffs""]"));        
        }

        public int GetResultsCount()
        {
            return this.driver.FindElements(By.CssSelector(@"[ng-repeat=""staff in staffs""]")).Count;
        }
    }
}


