using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleWebApp.Areas.Catalogs.Models;
using SimpleWebApp.Data;
using SimpleWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebApp.Areas.Catalogs.Controllers
{
  [Area("Catalogs")]
  [Authorize]
  public class BeerController : Controller
  {
    private readonly ApplicationDbContext dbContext;
    private readonly UserManager<IdentityUser> userManager;

    public BeerController(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
    {
      this.dbContext = dbContext;
      this.userManager = userManager;
    }

    // GET: Catalogs/Beer/
    public ActionResult Index()
    {
      return View();
    }

    // POST: Catalogs/Beer/GetListBeers
    [HttpPost]
    public JsonResult GetListBeers(DataTableParamModel<BeerClientModel> dataTableParam)
    {
      
      var listBeers = FindAllBeers(dataTableParam);

      return Json(new Response<IEnumerable<BeerClientModel>>(ResponseStatus.SUCCESS,
        ResponseMessages.SUCCESS_REQUESTS, listBeers));
    }

    // GET: Catalogs/Beer/Form
    public ActionResult Form(int? id)
    {
      var beer = FindBeerById(id ?? 0);

      if (beer == null)
        beer = new BeerClientModel();

      return PartialView(beer);
    }

    // POST: Catalogs/Beer/SaveAsync
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<JsonResult> Save(IFormCollection collection)
    {
      try
      {
        bool isUpdate = false;
        int Id = 0;

        if (!string.IsNullOrEmpty(collection["Id"].ToString()))
          Id = Convert.ToInt32(collection["Id"].ToString());

        var user = await userManager.GetUserAsync(User);

        var beer = dbContext.Beers.FirstOrDefault(x => x.Id == Id);

        if (beer == null)
        {
          beer = new BeerModel()
          {
            Name = collection["Name"].ToString(),
            Brand = collection["Brand"].ToString(),
            Alcohol = Convert.ToDecimal(collection["Alcohol"].ToString()),
            IsDeleted = false,
            CreatedAt = DateTime.Now,
            CreatedBy = user.Id,
            UpdatedAt = DateTime.Now,
            UpdatedBy = user.Id
          };

          dbContext.Beers.Add(beer);
        }
        else
        {
          beer.Name = collection["Name"].ToString();
          beer.Brand = collection["Brand"].ToString();
          beer.Alcohol = Convert.ToDecimal(collection["Alcohol"].ToString());
          beer.UpdatedAt = DateTime.Now;
          beer.UpdatedBy = user.Id;

          isUpdate = true;
        }

        await dbContext.SaveChangesAsync();

        if (isUpdate)
          return Json(new Response<string>(ResponseStatus.SUCCESS,
            ResponseMessages.SUCCESS_UPDATED));
        else
          return Json(new Response<string>(ResponseStatus.SUCCESS,
            ResponseMessages.SUCCESS_CREATED));

      }
      catch (Exception ex)
      {
        return Json(new Response<string>(ResponseStatus.FATAL_ERROR,
          ResponseMessages.FATAL_ERROR, ex));
      }
    }

    // POST: Catalogs/Beer/Delete/5
    [HttpPost]
    public async Task<JsonResult> Delete([FromRoute] int id)
    {
      try
      {
        var user = await userManager.GetUserAsync(User);

        var beer = dbContext.Beers.FirstOrDefault(x => x.Id == id);

        if (beer == null)
        {
          return Json(new Response<string>(ResponseStatus.ERROR,
            ResponseMessages.ERROR_NOT_FOUND));
        }
        else
        {
          beer.IsDeleted = true;
          beer.UpdatedAt = DateTime.Now;
          beer.UpdatedBy = user.Id;
        }

        await dbContext.SaveChangesAsync();

        return Json(new Response<string>(ResponseStatus.SUCCESS,
            ResponseMessages.SUCCESS_DELETED));

      }
      catch (Exception ex)
      {
        return Json(new Response<string>(ResponseStatus.FATAL_ERROR,
          ResponseMessages.FATAL_ERROR, ex));
      }
    }

    private IEnumerable<BeerClientModel> FindAllBeers(DataTableParamModel<BeerClientModel> dataTableParam)
    {
      var filters = dataTableParam.filters;
      var order = dataTableParam.GetOrder();

      var beerList = dbContext.Beers
        .Where(x => x.IsDeleted == false)
        .Where(x => string.IsNullOrEmpty(filters.Name) || x.Name.ToUpper().Contains(filters.Name.ToUpper()))
        .Where(x => string.IsNullOrEmpty(filters.Brand) || x.Brand.ToUpper().Contains(filters.Brand.ToUpper()))
        .Select(x => new BeerClientModel()
        {
          Id = x.Id,
          Name = x.Name,
          Alcohol = x.Alcohol,
          Brand = x.Brand
        })
        .ToList();

      if (order.name.Equals("id") && order.dir == "asc")
        beerList = beerList.OrderBy(x => x.Id).ToList();

      if (order.name.Equals("id") && order.dir == "desc")
        beerList = beerList.OrderByDescending(x => x.Id).ToList();

      if (order.name.Equals("name") && order.dir == "asc")
        beerList = beerList.OrderBy(x => x.Name).ToList();

      if (order.name.Equals("name") && order.dir == "desc")
        beerList = beerList.OrderByDescending(x => x.Name).ToList();

      if (order.name.Equals("brand") && order.dir == "asc")
        beerList = beerList.OrderBy(x => x.Brand).ToList();

      if (order.name.Equals("brand") && order.dir == "desc")
        beerList = beerList.OrderByDescending(x => x.Brand).ToList();

      if (order.name.Equals("alcohol") && order.dir == "asc")
        beerList = beerList.OrderBy(x => x.Alcohol).ToList();

      if (order.name.Equals("alcohol") && order.dir == "desc")
        beerList = beerList.OrderByDescending(x => x.Alcohol).ToList();

      return beerList;
    }

    private BeerClientModel FindBeerById(int id)
    {
      var beer = dbContext.Beers.Where(x => x.IsDeleted == false && x.Id == id)
        .Select(x => new BeerClientModel()
        {
          Id = x.Id,
          Name = x.Name,
          Brand = x.Brand,
          Alcohol = x.Alcohol
        }).FirstOrDefault();

      return beer;
    }
  }
}
