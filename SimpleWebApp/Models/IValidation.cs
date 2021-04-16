using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebApp.Models
{
  public interface IValidation
  {
    bool IsValid();
    List<ValidationFailure> GetValidationErrors();
  }

}
