using log4net;
using Microsoft.Extensions.Configuration;
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
    private static IConfiguration _configuration = AppSettings.GetConfiguration();

    public WebDriverFactory() { }

    public static IWebDriver GetDriver()
    {
      IWebDriver driver = null;
      string browser = string.Empty;

      try
      {
        browser = _configuration.GetSection("Browser").Value.ToString();

        driver = WebDriverFactory.CreateWebDriver(browser);
      }
      catch (Exception ex)
      {
        throw new Exception("GetDriver", ex);
      }

      return driver;
    }

    private static IWebDriver CreateWebDriver(string browser)
    {
      IWebDriver driver;

      if (Browsers.FIREFOX.Equals(browser))
      {
        driver = new FirefoxDriver();
      } 
      else if (Browsers.CHROME.Equals(browser))
      {
        driver = new ChromeDriver();
      }
      else
      {
        throw new Exception("Driver Not Created - Browser Not Found");
      }

      driver.Manage().Window.Maximize();
      driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
      return driver;
    }

  }
}
