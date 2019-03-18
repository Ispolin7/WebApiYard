using System;
using System.ComponentModel.DataAnnotations;
using WebApiYard.Repositories;
using WebApiYard.Repositories.Models;

namespace WebApiYard.Controllers.ValidationModels.CustomDataAnnotations
{
    public class OrderItemExistDBAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var repository = new Repository<OrderItem>();
            if (repository.GetById((Guid)value) == null)
            {
                return new ValidationResult("Order Item not found");
            }
            return ValidationResult.Success;
        }
    }
}
