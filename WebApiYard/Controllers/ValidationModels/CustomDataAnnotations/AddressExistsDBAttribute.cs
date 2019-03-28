using System;
using WebApiYard.Repositories;
using WebApiYard.Repositories.Models;
using System.ComponentModel.DataAnnotations;

namespace WebApiYard.Controllers.ValidationModels.CustomDataAnnotations
{
    public class AddressExistsDBAttribute : ValidationAttribute
    {
        public Repository<Address> Address { get; set; }
        public AddressExistsDBAttribute(Repository<Address> addresses)
        {
            Address = addresses;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //var repository = new Repository<Address>();
            if (Address.GetById((Guid)value) == null)
            {
                return new ValidationResult("Address not found");
            }
            return ValidationResult.Success;
        }
    }
}