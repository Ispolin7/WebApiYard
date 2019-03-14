using System;
using System.ComponentModel.DataAnnotations;

namespace WebApiYard.Controllers.ValidationModels
{
    public class OrderUpdate : OrderCreate
    {
        [Required]
        public Guid Id { get; set; }
    }
}
