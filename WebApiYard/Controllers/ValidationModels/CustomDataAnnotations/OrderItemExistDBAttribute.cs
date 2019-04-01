using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebApiYard.Repositories;
using WebApiYard.Repositories.Models;

namespace WebApiYard.Controllers.ValidationModels.CustomDataAnnotations
{
    public class OrderItemExistDBAttribute : ValidationAttribute
    {
        //public IRepository<OrderItem> Repository { get; set; }

        //public OrderItemExistDBAttribute(IRepository<OrderItem> itemRepository)
        //{
        //    Repository = itemRepository;
        //}

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var itemRepository = (IRepository<OrderItem>)validationContext.GetService(typeof(IRepository<OrderItem>));
            if (itemRepository.GetById((Guid)value).FirstOrDefault() == null)
            {
                return new ValidationResult("Order Item not found");
            }
            return ValidationResult.Success;
        }
    }
}
