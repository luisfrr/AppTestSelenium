using log4net;
using OpenQA.Selenium;
using SimpleWebApp.XUnit.Test.Drivers;
using System;
using System.IO;
using System.Text;
using TechTalk.SpecFlow;

namespace SimpleWebApp.XUnit.Test.Hooks
{
  [Binding]
  public sealed class Scenarios
  {
    private readonly ScenarioContext _scenarioContext;
    public static IWebDriver driver;

    public Scenarios(ScenarioContext scenarioContext)
    {
      _scenarioContext = scenarioContext;
    }

    [BeforeScenario]
    public void BeforeScenario()
    {
      driver = CreateDriver.GetDriver();
    }

    [AfterScenario]
    public void AfterScenario()
    {
      if (_scenarioContext.TestError != null)
      {
        //TakeScreenshot(_driver, scenarioContext);
      }
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

        if (driver is ITakesScreenshot takesScreenshot)
        {
          var screenshot = takesScreenshot.GetScreenshot();

          string screenshotFilePath = Path.Combine(artifactDirectory, fileNameBase + "_screenshot.png");

          screenshot.SaveAsFile(screenshotFilePath);

        }
      }
      catch (Exception)
      {
      }
    }
  }
}
