using System;
using WebApiYard.Repositories;
using WebApiYard.Repositories.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace WebApiYard.Controllers.ValidationModels.CustomDataAnnotations
{
    public class CustomerExistsDBAttribute : ValidationAttribute
    {
              
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customerRepository = (IRepository<Customer>)validationContext.GetService(typeof(IRepository<Customer>));
            if (customerRepository.GetById((Guid)value).FirstOrDefault() == null)
            {
                return new ValidationResult("Customer not found");
            }
            return ValidationResult.Success;
        }
    }
}
