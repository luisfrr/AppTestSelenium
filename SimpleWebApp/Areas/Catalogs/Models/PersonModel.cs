using SimpleWebApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebApp.Areas.Catalogs.Models
{
  public class PersonModel : IAudit
  {
    public Guid Id { get; set; }

    [DisplayName("Name")]
    [Required]
    public string Name { get; set; }

    [DisplayName("LastName")]
    [Required]
    public string LastName { get; set; }

    [DisplayName("Email")]
    [Required]
    [EmailAddress(ErrorMessage = "Invalid Email")]
    public string Email { get; set; }

    [DisplayName("Phone")]
    [Phone(ErrorMessage = "Invalid Phone Number")]
    public string Phone { get; set; }

    [DisplayName("BirthDate")]
    public DateTime BirthDate { get; set; }

    [DisplayName("Gender")]
    public Gender Gender { get; set; }


    public bool IsDeleted { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
  }

  public enum Gender
  {
    MALE,
    FEMALE
  }
}
