using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApiYard.Common;
using WebApiYard.Controllers.ValidationModels.CustomDataAnnotations;

namespace WebApiYard.Controllers.ValidationModels
{
    public class OrderItemCreate
    {
        [Required]
        public int Quantity { get; set; }

        [ColorCheck]
        public string Color { get; set; }

        // TODO validation in DB
        [Required]
        public Guid OrderId { get; set; }

        // TODO validation in DB
        [Required]
        public Guid ProductId { get; set; }
    }
}
