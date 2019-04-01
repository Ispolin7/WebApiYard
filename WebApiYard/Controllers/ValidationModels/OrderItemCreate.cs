using System;
using System.ComponentModel.DataAnnotations;
using WebApiYard.Controllers.ValidationModels.CustomDataAnnotations;

namespace WebApiYard.Controllers.ValidationModels
{
    public class OrderItemCreate
    {
        [Required]
        public int Quantity { get; set; }

        [ColorCheck]
        public string Color { get; set; }

        [Required]
        [OrderExistsDB]
        public Guid OrderId { get; set; }

        [Required]
        [ProductExistsDB]
        public Guid ProductId { get; set; }
    }
}
