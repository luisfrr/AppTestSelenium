using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using SimpleWebApp.Test.Common;
using SimpleWebApp.XUnit.Test.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SimpleWebApp.Test.Drivers
{
  public class WebDriverFactory
  {
    private static ILog _log = LogManager.GetLogger(typeof(WebDriverFactory));

    private WebDriverFactory() { }

    public static IWebDriver CreateWebDriver(string browser)
    {
      IWebDriver driver;

      if (Browsers.FIREFOX.Equals(browser))
      {
        string path = Directory.GetCurrentDirectory();

        //Give the path of the geckodriver.exe    
        FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(@"C:\Program Files\Mozilla Firefox\", "geckodriver.exe");

        //Give the path of the Firefox Browser        
        service.FirefoxBinaryPath = @"C:\Program Files\Mozilla Firefox\firefox.exe";

        driver = new FirefoxDriver(service);
      } 
      else if (Browsers.CHROME.Equals(browser))
      {
        driver = new ChromeDriver();
      }
      else
      {
        _log.Error(string.Format("Driver is not selected, invalid browser: {0}.", browser));
        return null;
      }

      driver.Manage().Window.Maximize();
      return driver;
    }

  }
}
