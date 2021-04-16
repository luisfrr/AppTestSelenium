using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebApp.Models
{
  public interface IAudit
  {
    bool IsDeleted { get; set; }
    string CreatedBy { get; set; }
    DateTime CreatedAt { get; set; }
    string UpdatedBy { get; set; }
    DateTime UpdatedAt { get; set; }
  }
}
