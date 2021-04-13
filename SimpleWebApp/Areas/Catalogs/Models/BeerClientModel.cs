using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebApp.Areas.Catalogs.Models
{
  public class BeerClientModel
  {
    public int? Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Brand { get; set; }

    [Range(0.1, 50)]
    public decimal? Alcohol { get; set; }
  }
}
