using System;
using System.ComponentModel.DataAnnotations;

namespace WebApiYard.Controllers.ValidationModels
{
    public class OrderCreate
    {
        [Required]
        public Guid AddressId { get; set; }

        [Required]
        public Guid CustomerId { get; set; }
    }
}
