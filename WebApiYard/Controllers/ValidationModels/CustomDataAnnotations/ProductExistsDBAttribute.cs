using System;
using WebApiYard.Repositories;
using WebApiYard.Repositories.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace WebApiYard.Controllers.ValidationModels.CustomDataAnnotations
{
    public class ProductExistsDBAttribute : ValidationAttribute
    {
        //public IRepository<Product> Repository { get; set; }

        //public ProductExistsDBAttribute(IRepository<Product> productRepository)
        //{
        //    Repository = productRepository;
        //}

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var productRepository = (IRepository<Product>)validationContext.GetService(typeof(IRepository<Product>));
            if (productRepository.GetById((Guid)value).FirstOrDefault() == null)
            {
                return new ValidationResult("Product not found");
            }
            return ValidationResult.Success;
        }
    }
}