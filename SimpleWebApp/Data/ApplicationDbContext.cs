using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleWebApp.Areas.Catalogs.Models;
using SimpleWebApp.Areas.Widgets.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleWebApp.Data
{
  public class ApplicationDbContext : IdentityDbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<AdConfigModel> AdConfigs { get; set; }
    public DbSet<BeerModel> Beers { get; set; }

  }
}
