using FluentValidation;
using FluentValidation.Results;
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

  public class BeerParamModel : IValidation
  {
    public int? Id { get; set; }

    public string Name { get; set; }

    public string Brand { get; set; }

    public decimal Alcohol { get; set; }

    public bool IsValid()
    {
      BeerValidator beerValidator = new BeerValidator();
      ValidationResult validationResult = beerValidator.Validate(this);

      return validationResult.IsValid;
    }

    public List<ValidationFailure> GetValidationErrors()
    {
      BeerValidator beerValidator = new BeerValidator();
      ValidationResult validationResult = beerValidator.Validate(this);

      return validationResult.Errors;
    }
  }

  public class BeerValidator : AbstractValidator<BeerParamModel>
  {
    public BeerValidator()
    {
      RuleFor(x => x.Name).NotEmpty().MaximumLength(128);
      RuleFor(x => x.Alcohol).NotNull().GreaterThan(0);
      RuleFor(x => x.Brand).NotEmpty().MaximumLength(128);
    }
  }
}
