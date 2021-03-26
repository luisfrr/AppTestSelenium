using log4net;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace SimpleWebApp.Test.Drivers
{
  public class CreateDriver
  {
    private static ILog _log = LogManager.GetLogger(typeof(CreateDriver));
    //private static IConfiguration _configuration = Settings.GetConfiguration();
    public CreateDriver() { }

    public static IWebDriver GetDriver()
    {
      IWebDriver driver;
      string browser = string.Empty;

      try
      {
        _log.Info("[ Environment Variable ] - Read Browser");
        //browser = ConfigurationManager.AppSettings["Browser"].ToString();
        browser = "firefox";
        /* _configuration.GetSection("Browser").Value.ToString();*/

      }
      catch (Exception ex)
      {
        _log.Error("[ Environment Variable ] - Error", ex);
      }

      driver = WebDriverFactory.CreateWebDriver(browser);

      return driver;
    }

  }
}
