using log4net;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using SimpleWebApp.XUnit.Test.Common;
using SimpleWebApp.XUnit.Test.Drivers;
using SimpleWebApp.XUnit.Test.Functions;
using SimpleWebApp.XUnit.Test.Hooks;
using System;
using TechTalk.SpecFlow;

namespace SimpleWebApp.XUnit.Test.Steps
{
  [Binding]
  public class StepsDefinitions
  {
    public IWebDriver _driver;
    public ILog _log;
    private IConfiguration _configuration;
    public SeleniumFunctions seleniumFunctions;
    private readonly ScenarioContext _scenarioContext;

    public StepsDefinitions(ScenarioContext scenarioContext)
    {
      _scenarioContext = scenarioContext;
      _driver = WebDriverFactory.GetDriver();

      _configuration = AppSettings.GetConfiguration();
      seleniumFunctions = new SeleniumFunctions(_driver);
    }

    [Given(@"I am on the main site")]
    public void GivenIAmInAppMainSite()
    {
      string page = "Main";

      seleniumFunctions.GoToPage(page);
    }

    [Given(@"I navigate to '(.*)' page")]
    public void GivenINavigateToPage(string page)
    {
      seleniumFunctions.GoToPage(page);
    }

    [Given(@"I log in to the web app")]
    public void GivenILogInToTheWebApp()
    {
      string username = _configuration.GetSection("Credentials:SimpleWebApp:Username").Value.ToString();
      string password = _configuration.GetSection("Credentials:SimpleWebApp:Password").Value.ToString();
      string page = "Login";

      seleniumFunctions.GoToPage(page);

      seleniumFunctions.SetElementWithText("Email", username);
      seleniumFunctions.SetElementWithText("Password", password);
      seleniumFunctions.ClickJSElement("Login Button");

      seleniumFunctions.LoadPageDomInformation("Main");
    }

    [Given(@"I load the DOM Information '(.*)'")]
    [When(@"I load the DOM Information '(.*)'")]
    [Then(@"I load the DOM Information '(.*)'")]
    public void ThenILoadTheDOMInformation(string fileName)
    {
      seleniumFunctions.filePageName = fileName;
      seleniumFunctions.LoadListElements();
    }

    [Given(@"I load the Page DOM Information '(.*)'")]
    [When(@"I load the Page DOM Information '(.*)'")]
    [Then(@"I load the Page DOM Information '(.*)'")]
    public void ThenILoadThePageDOMInformation(string page)
    {
      seleniumFunctions.LoadPageDomInformation(page);
    }

    [Given(@"I click in element '(.*)'")]
    [When(@"I click in element '(.*)'")]
    [Then(@"I click in element '(.*)'")]
    public void ThenIClickInElement(string element)
    {
      seleniumFunctions.ClickJSElement(element);
    }

    [When(@"I set element '(.*)' with text '(.*)'")]
    [Then(@"I set element '(.*)' with text '(.*)'")]
    public void ThenISetElementWithText(string element, string text)
    {
      seleniumFunctions.SetElementWithText(element, text);
    }

    [Then(@"Assert if element '(.*?)' contains text '(.*?)'$")]
    public void ThenAssertIfElementContainsText(string element, string text)
    {
      seleniumFunctions.CheckPartialTextElementPresent(element, text);
    }

    [Then(@"Assert if title page is equals to '(.*)'")]
    public void ThenAssertIfTitlePageContainsText(string title)
    {
      seleniumFunctions.AssertIfExpectedIsEqualsToPageName(title);
    }

    [Then(@"Assert if element '(.*)' text is equals to '(.*)'")]
    public void ThenAssertIfElementTextIsEqualsTo(string element, string text)
    {
      seleniumFunctions.AssertIfElementTextIsEqualsTo(element, text);
    }

    [BeforeScenario]
    public void BeforeScenario()
    {
      Console.WriteLine(string.Format("Scenario: {0}", _scenarioContext.ScenarioInfo.Title));
    }

    [AfterScenario]
    public void AfterScenario()
    {
      if (_scenarioContext.TestError != null)
      {
        //TakeScreenshot(driver);
      }
      _driver.Quit();
    }

    //public void Dispose()
    //{
    //  if (_driver != null)
    //  {
    //    _driver.Dispose();
    //    _driver = null;
    //  }
    //}
  }
}
