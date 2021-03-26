using log4net;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using SimpleWebApp.XUnit.Test.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace SimpleWebApp.XUnit.Test.Drivers
{
  public class CreateDriver
  {
    private static IConfiguration _configuration = AppSettings.GetConfiguration();
    public CreateDriver() { }

    public static IWebDriver GetDriver()
    {
      IWebDriver driver;
      string browser = string.Empty;

      try
      {
        browser = _configuration.GetSection("Browser").Value.ToString();
      }
      catch (Exception)
      {

      }

      driver = WebDriverFactory.CreateWebDriver(browser);

      return driver;
    }

  }
}
