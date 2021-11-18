using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleWebApp.Areas.Catalogs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebApp.Data
{
  public class SqlSeverDbContext : DbContext
  {
    public SqlSeverDbContext(DbContextOptions<SqlSeverDbContext> options)
       : base(options)
    {
    }
    public DbSet<PersonModel> Persons { get; set; }
    public DbSet<ContactModel> Contacts { get; set; }
  }
}
