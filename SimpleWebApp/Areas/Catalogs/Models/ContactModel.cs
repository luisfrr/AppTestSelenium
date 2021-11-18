using SimpleWebApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebApp.Areas.Catalogs.Models
{
  public class ContactModel : IAudit
  {
    public Guid Id { get; set; }

    [DisplayName("Name")]
    public string Name { get; set; }

    [DisplayName("NickName")]
    public string NickName { get; set; }

    [DisplayName("Email")]
    public string Email { get; set; }

    [DisplayName("Phone")]
    public string Phone { get; set; }

    public Guid Entity { get; set; }

    public TypeContact Type { get; set; }

    public bool IsDeleted { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
  }

  public enum TypeContact
  {
    PERSON = 1,
    PROVIDERS = 2
  }
}
