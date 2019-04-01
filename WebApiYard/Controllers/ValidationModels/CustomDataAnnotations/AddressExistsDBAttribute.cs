using System;
using WebApiYard.Repositories;
using WebApiYard.Repositories.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace WebApiYard.Controllers.ValidationModels.CustomDataAnnotations
{
    public class AddressExistsDBAttribute : ValidationAttribute
    {
        //public IRepository<Address> Address { get; set; }
        //public AddressExistsDBAttribute(IRepository<Address> addresses)
        //{
        //    Address = addresses;
        //}
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var addressRepository = (IRepository<Address>)validationContext.GetService(typeof(IRepository<Address>));
            if (addressRepository.GetById((Guid)value).FirstOrDefault() == null)
            {
                return new ValidationResult("Address not found");
            }
            return ValidationResult.Success;
        }
    }
}