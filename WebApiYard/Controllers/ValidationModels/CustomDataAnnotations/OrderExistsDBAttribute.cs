using System;
using WebApiYard.Repositories;
using WebApiYard.Repositories.Models;
using System.ComponentModel.DataAnnotations;

namespace WebApiYard.Controllers.ValidationModels.CustomDataAnnotations
{
    public class OrderExistsDBAttribute : ValidationAttribute
    {
        public Repository<Order> Repository { get; set; }

        public OrderExistsDBAttribute(Repository<Order> orderRepository)
        {
            Repository = orderRepository;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //var repository = new Repository<Order>();
            if (Repository.GetById((Guid)value) == null)
            {
                return new ValidationResult("Order not found");
            }
            return ValidationResult.Success;
        }
    }
}