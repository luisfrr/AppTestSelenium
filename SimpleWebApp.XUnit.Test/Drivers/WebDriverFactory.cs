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
        driver = new FirefoxDriver();
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
