using SimpleWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebApp.Areas.Catalogs.Models
{
  public class BeerModel : IAudit
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Brand { get; set; }
    public decimal Alcohol { get; set; }
    public bool IsDeleted { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
  }
}
