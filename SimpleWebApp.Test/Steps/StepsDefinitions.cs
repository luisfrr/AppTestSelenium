using log4net;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using SimpleWebApp.Test.Drivers;
using SimpleWebApp.Test.Hooks;
using TechTalk.SpecFlow;

namespace SimpleWebApp.Test.Steps
{
  [Binding]
  public class StepsDefinitions
  {
    public IWebDriver _driver;
    public ILog _log;
    private IConfiguration _configuration;
    public SeleniumFunctions seleniumFunctions;

    public StepsDefinitions()
    {
      _driver = Scenarios.driver;
      _log = LogManager.GetLogger(typeof(StepsDefinitions));
      //_configuration = Settings.GetConfiguration();
      seleniumFunctions = new SeleniumFunctions(_driver);
    }

    [Given(@"I am in App main site")]
    public void GivenIAmInAppMainSite()
    {
      //string url = _configuration.GetSection("SimpleWebAppUrl").Value.ToString();
      string url = @"https://localhost:44375/Identity/Account/Register";
      _log.Info("Navigate to: " + url);
      _driver.Url = url;
      seleniumFunctions.PageHasLoaded();
    }

    [Then(@"I load the DOM Information '(.*)'")]
    public void ThenILoadTheDOMInformation(string fileName)
    {
      seleniumFunctions.filePageName = fileName;

    }

    [Then(@"I click in element '(.*)'")]
    public void ThenIClickInElement(string element)
    {
      seleniumFunctions.ClickJSElement(element);
    }

    [Then(@"I set element '(.*)' with text '(.*)'")]
    public void ThenISetElementWithText(string element, string text)
    {
      seleniumFunctions.SetElementWithText(element, text);
    }

    [Then(@"Assert if element '(.*?)' contains text (.*?)$")]
    public void ThenAssertIfElementContainsText(string element, string text)
    {
      seleniumFunctions.CheckPartialTextElementPresent(element, text);
    }

  }
}
