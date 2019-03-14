using System;
using System.ComponentModel.DataAnnotations;

namespace WebApiYard.Controllers.ValidationModels
{
    public class OrderItemUpdate : OrderItemCreate
    {
        // TODO "The input was not valid."
        [Required]
        public int Id { get; set; }
    }
}
