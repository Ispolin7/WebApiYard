using System;
using WebApiYard.Repositories;
using WebApiYard.Repositories.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace WebApiYard.Controllers.ValidationModels.CustomDataAnnotations
{
    public class OrderExistsDBAttribute : ValidationAttribute
    {
        //public IRepository<Order> Repository { get; set; }

        //public OrderExistsDBAttribute(IRepository<Order> orderRepository)
        //{
        //    Repository = orderRepository;
        //}

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var orderRepository = (IRepository<Order>)validationContext.GetService(typeof(IRepository<Order>));
            if (orderRepository.GetById((Guid)value).FirstOrDefault() == null)
            {
                return new ValidationResult("Order not found");
            }
            return ValidationResult.Success;
        }
    }
}