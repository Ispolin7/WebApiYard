using System;
using System.ComponentModel.DataAnnotations;
using WebApiYard.Repositories;
using WebApiYard.Repositories.Models;

namespace WebApiYard.Controllers.ValidationModels.CustomDataAnnotations
{
    public class OrderItemExistDBAttribute : ValidationAttribute
    {
        public Repository<OrderItem> Repository { get; set; }

        public OrderItemExistDBAttribute(Repository<OrderItem> itemRepository)
        {
            Repository = itemRepository;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //var repository = new Repository<OrderItem>();
            if (Repository.GetById((Guid)value) == null)
            {
                return new ValidationResult("Order Item not found");
            }
            return ValidationResult.Success;
        }
    }
}
