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
        [TestCategory("Staff")]
        public void testAddNewStaff()
        {
            //Given the user is loggedin and then clicks on the staff from the Dropdown (before we create a branch to make sure it is there)
            GurukulaLoginPage loginPage = new GurukulaLoginPage(this.driver);
            loginPage.Navigate();
            loginPage.Login(Properties.TestData.User1, Properties.TestData.Pwd1);
            GurukulaLoggedInWelcomePage loggedIn = new GurukulaLoggedInWelcomePage(this.driver);
            Helper.WaintUntilPageIsLoaded(driver);
            loggedIn.GoToBranch();
            GurukulaBranchesPage branches = new GurukulaBranchesPage(this.driver);
            var branchName = Properties.TestData.BranchName + Helper.GetRandomString(4);
            branches.AddBranch(branchName, Properties.TestData.BranchCode + Helper.GetShortTimestamp());
            loggedIn.Navigate();
            loggedIn.GoToStaff();

            //When the user adds a new staff
            GurukulaStaffPage staff = new GurukulaStaffPage(this.driver);
            var staffName = Properties.TestData.StaffName+Helper.GetRandomString(4);
            staff.AddStaff(staffName, branchName);
            //Then if we use the search it should have only one value
            staff.SearchStaff(staffName);
            Assert.IsTrue(staff.GetResultsCount() == 1, "Result different than 1");
        }

        [TestMethod()]
        [TestCategory("Staff")]
        public void testAddModifyStaff()
        {
            //Given we have the staff that we want to modify
            GurukulaLoginPage loginPage = new GurukulaLoginPage(this.driver);
            loginPage.Navigate();
            loginPage.Login(Properties.TestData.User1, Properties.TestData.Pwd1);
            GurukulaLoggedInWelcomePage loggedIn = new GurukulaLoggedInWelcomePage(this.driver);
            Helper.WaintUntilPageIsLoaded(driver);
            loggedIn.GoToStaff();
            GurukulaStaffPage staff = new GurukulaStaffPage(this.driver);
            var staffName = Properties.TestData.StaffName + Helper.GetRandomString(4);
            staff.AddStaffWithoutBranch(staffName);

            //When the user modifies the staff
            var newStaffName = Properties.TestData.StaffName + Helper.GetRandomString(4);
            staff.ModifyStaff(staffName, newStaffName);

            //Then if we use the search the new one it should return only one value AND if we search for the old name we should not find it    
            staff.SearchStaff(newStaffName);
            Assert.IsTrue(staff.GetResultsCount() == 1, "Result different than 1");

            staff.SearchStaff(staffName);
            Assert.IsTrue(staff.GetResultsCount() == 0, "Result different than 0");
        }

        [TestMethod()]
        [TestCategory("Staff")]
        public void testAddStaffAndDeleteStaff()
        {
            //Given we have the staff that we want to modify
            GurukulaLoginPage loginPage = new GurukulaLoginPage(this.driver);
            loginPage.Navigate();
            loginPage.Login(Properties.TestData.User1, Properties.TestData.Pwd1);
            GurukulaLoggedInWelcomePage loggedIn = new GurukulaLoggedInWelcomePage(this.driver);
            Helper.WaintUntilPageIsLoaded(driver);
            loggedIn.GoToStaff();
            GurukulaStaffPage staff = new GurukulaStaffPage(this.driver);
            var staffName = Properties.TestData.StaffName + Helper.GetRandomString(4);
            staff.AddStaffWithoutBranch(staffName);

            //When the user deletes the Staff
            staff.DeleteStaff(staffName);


            //Then if we use the search for the Branch we should not find it    
            staff.SearchStaff(staffName);
            Assert.IsTrue(staff.GetResultsCount() == 0, "Result different than 0");
        }

    }
}
