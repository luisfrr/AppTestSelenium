using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SimpleWebApp.Areas.Widgets.Models;
using SimpleWebApp.Data;
using SimpleWebApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebApp.Areas.Widgets.Controllers
{
  [Area("Widgets")]
  [Authorize]
  public class SummernoteController : Controller
  {
    public const string AD_CONFIG_JSON_PATH = @"Configurations\AdConfig.json";

    private readonly ApplicationDbContext dbContext;

    public SummernoteController(ApplicationDbContext dbContext)
    {
      this.dbContext = dbContext;
    }

    #region Views

    public IActionResult Index()
    {
      return PartialView();
    }

    public IActionResult AdPreview()
    {
      return PartialView();
    }

    #endregion

    #region Actions

    [HttpGet]
    public JsonResult GetAddConfig()
    {
      Response<AdConfigModel> response;
      try
      {
        AdConfigModel adConfigModel = dbContext.AdConfigs.FirstOrDefault();

        if (adConfigModel == null)
          adConfigModel = new AdConfigModel();

        response = new Response<AdConfigModel>(ResponseStatus.SUCCESS,
          ResponseMessages.SUCCESS_REQUESTS, adConfigModel);

        return Json(response);
      }
      catch (Exception ex)
      {
        response = new Response<AdConfigModel>(ResponseStatus.ERROR, ResponseMessages.ERROR, ex);
        return Json(response);
      }
    }

    [HttpPost]
    public JsonResult SaveConfig(AdConfigModel newAdConfig)
    {
      Response<AdConfigModel> response;
      bool isUpdated;
      try
      {
        AdConfigModel actualAdConfig = dbContext.AdConfigs.FirstOrDefault();

        if (actualAdConfig == null)
          actualAdConfig = new AdConfigModel();

        string username = User.Identity.Name;

        // If the editor is null or empty then it is a new configuration, otherwise it is an update
        if (string.IsNullOrWhiteSpace(actualAdConfig.Editor))
        {
          isUpdated = false;

          actualAdConfig.Editor = newAdConfig.Editor;
          actualAdConfig.CreatedBy = username;
          actualAdConfig.CreatedAt = DateTime.Now;
          actualAdConfig.UpdatedBy = string.Empty;
          actualAdConfig.UpdatedAt = new DateTime(1990, 1, 1);

          dbContext.AdConfigs.Add(actualAdConfig);
        }
        else
        {
          isUpdated = true;

          actualAdConfig.Editor = newAdConfig.Editor;
          actualAdConfig.UpdatedBy = username;
          actualAdConfig.UpdatedAt = DateTime.Now;
        }

        dbContext.SaveChangesAsync();
      }
      catch (Exception ex)
      {
        response = new Response<AdConfigModel>(ResponseStatus.ERROR, ResponseMessages.ERROR, ex);
        return Json(response);
      }

      if (isUpdated)
        response = new Response<AdConfigModel>(ResponseStatus.SUCCESS, ResponseMessages.SUCCESS_UPDATED);
      else
        response = new Response<AdConfigModel>(ResponseStatus.SUCCESS, ResponseMessages.SUCCESS_CREATED);

      return Json(response);
    }

    #endregion

    #region Private Methods Json Config

    private AdConfigModel GetAdConfigFromJsonFile()
    {
      AdConfigModel adConfig = null;
      try
      {
        string path = Path.Combine(AppContext.BaseDirectory, AD_CONFIG_JSON_PATH);

        using StreamReader r = new StreamReader(path);
        string json = r.ReadToEnd();
        adConfig = JsonConvert.DeserializeObject<AdConfigModel>(json);
      }
      catch (Exception ex)
      {
        throw new Exception("Configuration file not found", ex);
      }

      return adConfig;
    }

    private void SaveAdConfigJson(AdConfigModel adConfigModel)
    {
      try
      {
        string path = Path.Combine(AppContext.BaseDirectory, AD_CONFIG_JSON_PATH);

        string json = JsonConvert.SerializeObject(adConfigModel, Formatting.Indented);
        System.IO.File.WriteAllText(path, json);
      }
      catch(Exception ex)
      {
        throw new Exception("Configuration file could not be saved", ex);
      }
      
    }

    #endregion

  }
}
