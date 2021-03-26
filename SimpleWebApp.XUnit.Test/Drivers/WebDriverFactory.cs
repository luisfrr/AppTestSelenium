using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using SimpleWebApp.XUnit.Test.Common;
using System;
using System.IO;

namespace SimpleWebApp.XUnit.Test.Drivers
{
  public class WebDriverFactory
  {
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

        FirefoxProfile profile = new FirefoxProfile()
        {
          AcceptUntrustedCertificates = true,
          AssumeUntrustedCertificateIssuer = true,
        };

        FirefoxOptions options = new FirefoxOptions()
        {
          AcceptInsecureCertificates = true
        };

        driver = new FirefoxDriver(service, options);
      } 
      else if (Browsers.CHROME.Equals(browser))
      {
        driver = new ChromeDriver();
      }
      else
      {
        return null;
      }

      driver.Manage().Window.Maximize();
      return driver;
    }

  }
}
