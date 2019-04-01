using System;
using System.ComponentModel.DataAnnotations;
using WebApiYard.Controllers.ValidationModels.CustomDataAnnotations;

namespace WebApiYard.Controllers.ValidationModels
{
    public class OrderCreate
    {
        [Required]
        [AddressExistsDB]
        public Guid AddressId { get; set; }

        [Required]
        [CustomerExistsDB]
        public Guid CustomerId { get; set; }
    }
}
