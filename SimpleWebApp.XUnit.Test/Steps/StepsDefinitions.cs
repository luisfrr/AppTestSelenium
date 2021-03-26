using log4net;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using SimpleWebApp.XUnit.Test.Common;
using SimpleWebApp.XUnit.Test.Functions;
using SimpleWebApp.XUnit.Test.Hooks;
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

    public StepsDefinitions()
    {
      _driver = Scenarios.driver;
      _configuration = AppSettings.GetConfiguration();
      seleniumFunctions = new SeleniumFunctions(_driver);
    }

    [Given(@"I am in App main site")]
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

    [When(@"I load the DOM Information '(.*)'")]
    [Then(@"I load the DOM Information '(.*)'")]
    public void ThenILoadTheDOMInformation(string fileName)
    {
      seleniumFunctions.filePageName = fileName;
      seleniumFunctions.LoadListElements();
    }

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


  }
}
