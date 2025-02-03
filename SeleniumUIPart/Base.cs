using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using WebDriverManager.DriverConfigs.Impl;
using System.Configuration;

namespace SeleniumUIPart
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class Base
    {
        protected const string INVENTORY_END_POINT = "/inventory";
        protected const string CART_END_POINT = "/cart";
        protected ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();
        string browser;

        [SetUp]
        public void StartBrowser()
        {        
            browser = TestContext.Parameters["browser"];
            browser = browser == null ? ConfigurationManager.AppSettings["browser"] : browser;
            
            Assert.That(browser, Is.Not.Null);       
            TestContext.Out.WriteLine($"Current browser from settings is: {browser}");
            
            InitBrowser(browser);

            driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Value.Manage().Window.Maximize();
            driver.Value.Url = $"{ConfigurationManager.ConnectionStrings["startpage"]}";
        }
        public void InitBrowser(string browserName)
        {
            switch (browserName)
            {           
                case "GoogleChrome":
                    ChromeOptions optionsChrome = new ChromeOptions();
                    //optionsChrome.AddArgument("--headless");
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    driver.Value = new ChromeDriver(optionsChrome);
                    break;
                case "Firefox":
                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                    driver.Value = new FirefoxDriver();
                    break;
                case "Edge":
                    new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                    driver.Value = new EdgeDriver();
                    break;
                default:
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    driver.Value = new ChromeDriver();
                    break;
            }
        }

        [TearDown]
        public void AfterTest()
        {          
            driver.Value.Quit();
        }
        [OneTimeTearDown]
        public void AfterTestOneTime()
        {
            driver?.Dispose();
        }
        protected IWebDriver GetDriver()
        {
            return driver.Value;
        }
    }
}
