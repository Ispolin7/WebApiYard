using System;
using System.ComponentModel.DataAnnotations;
using WebApiYard.Controllers.ValidationModels.CustomDataAnnotations;
using WebApiYard.Repositories;
using WebApiYard.Repositories.Models;

namespace WebApiYard.Controllers.ValidationModels
{
    public class OrderCreate
    {
        [Required]
        public Guid AddressId { get; set; }

        [Required]
        [CustomerExists]
        public Guid CustomerId { get; set; }
    }
}
