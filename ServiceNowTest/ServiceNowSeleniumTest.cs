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
    [TestClass]
    public class preTest
    {
        [AssemblyInitialize()]
        //This one will be run only once at the start or running the tests
        //Only after this one has finished, the rest of the tests will begin
        //That makes it great to wake up app, fetch data, etc.
        public static void AssemblyInit(TestContext context)
        {
         
        }
        [AssemblyCleanup()]
        public static void AssemblyCleanup()
        {
            
        }
    }

    //the enumeration will contain the different browsers that can be configured
    public enum Browser { Firefox, Chrome, Explorer, Safari };

    [TestClass]
    
    //This class extends the class with the tests adding the selection of the browser
    public class FirefoxTests : ServiceNowSeleniumTest
    {
        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            browserToTest = Browser.Firefox;
         }

        [ClassCleanup()]
        public static void ClassCleanup()
        {

        }
    }

    //[TestClass]//uncomment this to run the tests also with Chrome
    public class ChromeTests : ServiceNowSeleniumTest
    {
        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            browserToTest = Browser.Chrome;
        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {

        }
    }

    public partial class ServiceNowSeleniumTest
    {
        public static Browser browserToTest;
        private IWebDriver driver;
        public TestContext TestContext { get; set; }
        [TestInitialize()]
        public void Initialize()
        {
            try
            {
            switch (browserToTest)
                {
                    case Browser.Chrome:
                        driver = new ChromeDriver();
                        break;
                    case Browser.Explorer:
                        driver = new InternetExplorerDriver();
                        break;
                    case Browser.Safari:
                        driver = new SafariDriver();
                        break;
                    case Browser.Firefox:
                    default:
                        driver = new FirefoxDriver();
                        break;

                }
            //every find in the page will be max 3s
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(3));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Assert.Inconclusive();             
            }
        }


        [TestCleanup()]
        public void Cleanup()
        {
            if (this.TestContext.CurrentTestOutcome == UnitTestOutcome.Failed)
            {
                var filename = System.IO.Path.GetTempPath() + DateTime.Now.ToString("yy-MM-dd-HH-mm-ss-FFF") + "-" + this.GetType().Name + "-" + this.TestContext.TestName + ".jpg";
                ((ITakesScreenshot)this.driver).GetScreenshot().SaveAsFile(filename, ImageFormat.Jpeg);
                this.TestContext.AddResultFile(filename);
            }
            driver.Quit();
        }


    }
}
