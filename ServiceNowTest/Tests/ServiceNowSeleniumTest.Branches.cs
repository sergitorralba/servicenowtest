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
        [TestCategory("Branches")]
        public void testAddNewBranch()
        {
            //Given the user is loggedin and then clicks on the branch from the Dropdown
            GurukulaLoginPage loginPage = new GurukulaLoginPage(this.driver);
            loginPage.Navigate();
            loginPage.Login(Properties.TestData.User1, Properties.TestData.Pwd1);
            GurukulaLoggedInWelcomePage loggedIn = new GurukulaLoggedInWelcomePage(this.driver);
            Helper.WaintUntilPageIsLoaded(driver);
            loggedIn.GoToBranch();

            //When the user adds a new branch
            GurukulaBranchesPage branches = new GurukulaBranchesPage(this.driver);
            var branchName = Properties.TestData.BranchName + Helper.GetRandomString(4);
            branches.AddBranch(branchName, Properties.TestData.BranchCode + Helper.GetShortTimestamp());            
            //Then if we use the search it should have only one value
            branches.SearchBranch(branchName);
            Assert.IsTrue(branches.GetResultsCount()==1, "Result different than 1");
        }

        [TestMethod()]
        [TestCategory("Branches")]
        public void testAddModifyBranch()
        {
            //Given we have the branch that we want to modify
            GurukulaLoginPage loginPage = new GurukulaLoginPage(this.driver);
            loginPage.Navigate();
            loginPage.Login(Properties.TestData.User1, Properties.TestData.Pwd1);
            GurukulaLoggedInWelcomePage loggedIn = new GurukulaLoggedInWelcomePage(this.driver);
            Helper.WaintUntilPageIsLoaded(driver);
            loggedIn.GoToBranch();
            GurukulaBranchesPage branches = new GurukulaBranchesPage(this.driver);
            var branchName = Properties.TestData.BranchName + Helper.GetRandomString(4);
            branches.AddBranch(branchName, Properties.TestData.BranchCode + Helper.GetShortTimestamp());
            
            //When the user modifies the branch
            var newBranchName = Properties.TestData.BranchName + Helper.GetRandomString(4);
            branches.ModifyBranch(branchName, newBranchName);

            //Then if we use the search the new one it should return only one value AND if we search for the old name we should not find it    
            branches.SearchBranch(newBranchName);
            Assert.IsTrue(branches.GetResultsCount() == 1, "Result different than 1");

            branches.SearchBranch(branchName);
            Assert.IsTrue(branches.GetResultsCount() == 0, "Result different than 0");
        }

        [TestMethod()]
        [TestCategory("Branches")]
        public void testAddBranchAndDeleteBranch()
        {
            //Given we have the branch that we want to modify
            GurukulaLoginPage loginPage = new GurukulaLoginPage(this.driver);
            loginPage.Navigate();
            loginPage.Login(Properties.TestData.User1, Properties.TestData.Pwd1);
            GurukulaLoggedInWelcomePage loggedIn = new GurukulaLoggedInWelcomePage(this.driver);
            
            loggedIn.GoToBranch();
            GurukulaBranchesPage branches = new GurukulaBranchesPage(this.driver);
            var branchName = Properties.TestData.BranchName + Helper.GetRandomString(4);
            branches.AddBranch(branchName, Properties.TestData.BranchCode + Helper.GetShortTimestamp());

            //When the user deletes the branch
            branches.DeleteBranch(branchName);


            //Then if we use the search for the Branch we should not find it    
            branches.SearchBranch(branchName);
            Assert.IsTrue(branches.GetResultsCount() == 0, "Result different than 0");
        }

    }
}
