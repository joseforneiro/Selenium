using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Bovespa
{
    public class Utils
    {
        public static IWebDriver InitializeSelenium()
        {
            ChromeOptions pOptions = new ChromeOptions();

            string driverPath = "/home/junior/Documents/Chromedriver";

            string driverExecutableFileName = "chromedriver";

            ChromeOptions options = new ChromeOptions();

            if (Environment.GetEnvironmentVariable("ENVIRONMENT") == "PRD")
            {
                options.AddArgument("--headless");
            }
 
            ChromeDriverService service = ChromeDriverService.CreateDefaultService(driverPath, driverExecutableFileName);
            IWebDriver driver = null;

            try{
                driver = new ChromeDriver(service, options, TimeSpan.FromSeconds(500));
            }
            catch(Exception ex){
                
            }
            return driver;
        }
    }
}