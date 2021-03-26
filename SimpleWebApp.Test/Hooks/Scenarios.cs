using log4net;
using OpenQA.Selenium;
using SimpleWebApp.Test.Drivers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace SimpleWebApp.Test.Hooks
{
  [Binding]
  public sealed class Scenarios
  {
    private readonly ScenarioContext _scenarioContext;
    public static IWebDriver driver;
    private ILog _log;

    public Scenarios(ScenarioContext scenarioContext)
    {
      _scenarioContext = scenarioContext;
      _log = LogManager.GetLogger(typeof(Scenarios));
    }

    [BeforeScenario]
    public void BeforeScenario()
    {
      _log.Info("[ Configuration ] - Initializing driver configuration");
      driver = CreateDriver.GetDriver();
      _log.Info("[ Scenario ] - " + _scenarioContext.ScenarioInfo.Title);
    }

    [AfterScenario]
    public void AfterScenario()
    {
      if (_scenarioContext.TestError != null)
      {
        //TakeScreenshot(_driver, scenarioContext);
      }

      _log.Info("[ Configuration ] -  Clean and close the intance of the driver");
      driver.Quit();
    }

    private void TakeScreenshot(IWebDriver driver)
    {
      try
      {
        string fileNameBase = string.Format("Error_{0}_{1}",
                                            _scenarioContext.ScenarioInfo.Title,
                                            DateTime.Now.ToString("yyyyMMdd_HHmmss"));

        var artifactDirectory = Path.Combine(Directory.GetCurrentDirectory(), "screenshots");

        if (!Directory.Exists(artifactDirectory))
          Directory.CreateDirectory(artifactDirectory);

        string pageSource = driver.PageSource;
        string sourceFilePath = Path.Combine(artifactDirectory, fileNameBase + "_source.html");

        File.WriteAllText(sourceFilePath, pageSource, Encoding.UTF8);
        _log.Error(string.Format("The scenario failed. Current Page URL is: {0}", new Uri(sourceFilePath)));

        if (driver is ITakesScreenshot takesScreenshot)
        {
          var screenshot = takesScreenshot.GetScreenshot();

          string screenshotFilePath = Path.Combine(artifactDirectory, fileNameBase + "_screenshot.png");

          screenshot.SaveAsFile(screenshotFilePath);

          _log.Error(string.Format("Screenshot: {0}", new Uri(screenshotFilePath)));
        }
      }
      catch (Exception ex)
      {
        _log.Error("Error while taking screenshot: {0}", ex);
      }
    }
  }
}
