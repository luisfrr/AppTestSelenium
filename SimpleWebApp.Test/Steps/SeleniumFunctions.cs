using log4net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SimpleWebApp.Test.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace SimpleWebApp.Test.Steps
{
  public class SeleniumFunctions
  {

    private readonly ILog _log;
    private readonly IWebDriver _driver;
    private readonly IConfiguration _configuration;

    public string environment = string.Empty;
    public string elementText = string.Empty;

    public const int EXPLICIT_TIMEOUT = 15;
    public bool isDisplayed = false;

    // Page Path
    public string filePageName = string.Empty;
    public string folderPageFilePath = string.Empty;

    public string getFieldBy = string.Empty;
    public string valueToFind = string.Empty;

    #region Constructor

    public SeleniumFunctions(IWebDriver driver)
    {
      _log = LogManager.GetLogger(typeof(SeleniumFunctions));
      //_configuration = Settings.GetConfiguration();
      _driver = driver;
      //folderPageFilePath = _configuration.GetSection("FolderPageFilePath").Value.ToString();
      folderPageFilePath = "./Resources/Pages";
    }

    #endregion

    public string GetValueVariable(string variable)
    {
      string value = _configuration.GetSection(variable).Value.ToString();
      return value;
    }


    public IList<Element> GetListElements()
    {
      string filePath = Path.Combine(folderPageFilePath, filePageName);
      try
      {
        string strFileJson = File.ReadAllText(filePath);

        return JsonConvert.DeserializeObject<IList<Element>>(strFileJson);
      }
      catch (Exception ex)
      {
        _log.Error("[ ReadJsonFile ] - Json File not found: " + filePath);
        throw ex;
      }
    }

    public Element GetDataElement(string element)
    {
      var pageElements = GetListElements();

      var dataElement = pageElements.Where(x => x.Name.Equals(element)).FirstOrDefault();

      return dataElement;
    }

    public By GetCompleteElement(string element)
    {
      By result = null;

      var dataElement = GetDataElement(element);

      getFieldBy = dataElement.GetFieldBy;
      valueToFind = dataElement.ValueToFind;

      if (TypeSelector.CLASS_NAME.Equals(getFieldBy))
        result = By.ClassName(valueToFind);
      else if (TypeSelector.CSS_SELECTOR.Equals(getFieldBy))
        result = By.CssSelector(valueToFind);
      else if (TypeSelector.ID.Equals(getFieldBy))
        result = By.Id(valueToFind);
      else if (TypeSelector.LINK_TEXT.Equals(getFieldBy))
        result = By.LinkText(valueToFind);
      else if (TypeSelector.NAME.Equals(getFieldBy))
        result = By.Name(valueToFind);
      else if (TypeSelector.LINK.Equals(getFieldBy))
        result = By.PartialLinkText(valueToFind);
      else if (TypeSelector.TAG_NAME.Equals(getFieldBy))
        result = By.TagName(valueToFind);
      else if (TypeSelector.XPATH.Equals(getFieldBy))
        result = By.XPath(valueToFind);

      return result;
    }

    public void ClickJSElement(string element)
    {
      By selectorElement = GetCompleteElement(element);
      IJavaScriptExecutor jse = (IJavaScriptExecutor)_driver;
      // Da clic en un elemento
      jse.ExecuteScript("arguments[0].click()", _driver.FindElement(selectorElement));
    }

    public void SetElementWithText(string element, string text)
    {
      By selectorElement = GetCompleteElement(element);
      IJavaScriptExecutor jse = (IJavaScriptExecutor)_driver;
      // Se agrega texto a un elemento
      _driver.FindElement(selectorElement).SendKeys(text);
    }

    public void CheckPartialTextElementPresent(string element, string text)
    {
      var currentText = GetTextElement(element);

      bool found = currentText.Contains(text);

      Assert.IsTrue(found, string.Format("Text is not present in element '{0}', current text is: {1}", element, currentText));
    }

    public string GetTextElement(string element)
    {
      By seleniumElement = GetCompleteElement(element);

      WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(EXPLICIT_TIMEOUT));
      _log.Info(string.Format("Esperando el elemento: {0}.", element));

      wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(seleniumElement));


      var elementText = _driver.FindElement(seleniumElement).Text;

      return elementText;
    }

    public void PageHasLoaded()
    {
      string currentUrl = _driver.Url;
      _log.Info("Go to URL: " + currentUrl);

      _driver.Navigate().GoToUrl(currentUrl);

      WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(EXPLICIT_TIMEOUT));

      wait.Until(webDriver => ((IJavaScriptExecutor)webDriver)
        .ExecuteScript("return document.readyState").Equals("complete"));
    }
  }
}
