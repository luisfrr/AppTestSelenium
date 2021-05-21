using log4net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SimpleWebApp.XUnit.Test.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace SimpleWebApp.XUnit.Test.Functions
{
  public class SeleniumFunctions
  {

    private readonly ILog _log;
    private readonly IWebDriver _driver;
    private readonly IConfiguration _configuration;

    public string environment = string.Empty;
    public string elementText = string.Empty;

    public const int EXPLICIT_TIMEOUT = 15;
    public const int IMPLICIT_TIMEOUT = 15;
    public bool isDisplayed = false;

    // Page Path
    public string filePageName = string.Empty;
    public string folderPageFilePath = string.Empty;

    public string getFieldBy = string.Empty;
    public string valueToFind = string.Empty;

    // List Page
    IList<Page> pages;

    // List Page Elements
    IList<Element> elements;

    #region Constructor

    public SeleniumFunctions(IWebDriver driver)
    {
      _log = LogManager.GetLogger(typeof(SeleniumFunctions));
      _configuration = AppSettings.GetConfiguration();
      _driver = driver;
      folderPageFilePath = _configuration.GetSection("FolderPageFilePath").Value.ToString();

      LoadPages();
    }

    #endregion

    public string GetValueVariable(string variable)
    {
      string value = _configuration.GetSection(variable).Value.ToString();
      return value;
    }

    public Page GetDataPage(string name)
    {
      var dataPage = pages.Where(x => x.Name.Equals(name)).FirstOrDefault();

      return dataPage;
    }

    private void LoadPages()
    {
      try
      {
        string filePath = _configuration.GetSection("SimpleWebAppPages").Value.ToString();

        string strFileJson = File.ReadAllText(filePath);

        pages = JsonConvert.DeserializeObject<IList<Page>>(strFileJson);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void LoadListElements()
    {
      string filePath = Path.Combine(folderPageFilePath, filePageName);
      try
      {
        string strFileJson = File.ReadAllText(filePath);

        elements = JsonConvert.DeserializeObject<IList<Element>>(strFileJson);
      }
      catch (Exception ex)
      {
        _log.Error("[ ReadJsonFile ] - Json File not found: " + filePath);
        throw ex;
      }
    }

    public Element GetDataElement(string element)
    {
      var dataElement = elements.Where(x => x.Name.Equals(element)).FirstOrDefault();

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
      //IJavaScriptExecutor jse = (IJavaScriptExecutor)_driver;

      // Da clic en un elemento
      //jse.ExecuteScript("arguments[0].click()", _driver.FindElement(selectorElement));
      _driver.FindElement(selectorElement).Click();
    }

    public void SetElementWithText(string element, string text)
    {
      By selectorElement = GetCompleteElement(element);
      //IJavaScriptExecutor jse = (IJavaScriptExecutor)_driver;
      // Se agrega texto a un elemento
      _driver.FindElement(selectorElement).SendKeys(text);
    }

    public void SubmitFormButton (string buttonElement)
    {
      
    }

    public void CheckPartialTextElementPresent(string element, string text)
    {
      var currentText = GetTextElement(element);

      bool found = currentText.Contains(text);

      Assert.True(found, string.Format("Text is not present in element '{0}', current text is: {1}", element, currentText));
    }

    public void AssertIfExpectedIsEqualsToPageName(string title)
    {
      string currentPageName = _driver.Title;

      bool isEquals = currentPageName.Equals(title);

      Assert.True(isEquals, string.Format("Expected Title: {0} is not equals to: {1}", title, currentPageName));
    }

    internal void AssertIfElementTextIsEqualsTo(string element, string text)
    {
      var currentText = GetTextElement(element);

      bool isEquals = currentText.Equals(text);

      Assert.True(isEquals, string.Format("The text is not equal to the value of the element '{0}', current text is: {1}", element, currentText));
    }

    public string GetTextElement(string element)
    {
      By seleniumElement = GetCompleteElement(element);

      WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(EXPLICIT_TIMEOUT));

      wait.Until(ExpectedConditions.ElementExists(seleniumElement));

      var elementText = _driver.FindElement(seleniumElement).Text;

      return elementText;
    }

    public void GoToPage(string pageName)
    {
      var page = GetDataPage(pageName);

      filePageName = page.PageDomInformation;
      LoadListElements();

      _driver.Navigate().GoToUrl(page.Url);
    }

    public void LoadPageDomInformation(string pageName)
    {
      var page = GetDataPage(pageName);

      filePageName = page.PageDomInformation;
      LoadListElements();
    }
  }
}
