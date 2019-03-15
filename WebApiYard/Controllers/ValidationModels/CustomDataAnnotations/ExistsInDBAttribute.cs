using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApiYard.Repositories;
using WebApiYard.Repositories.Models;

namespace WebApiYard.Controllers.ValidationModels.CustomDataAnnotations
{
    public class CustomerExistsAttribute : ValidationAttribute
    { 
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var repository = new Repository<Customer>();
            if (repository.GetById((Guid)value) == null)
            {
                return new ValidationResult("Customer not found");
            }
            return ValidationResult.Success;
        }
    }
}
