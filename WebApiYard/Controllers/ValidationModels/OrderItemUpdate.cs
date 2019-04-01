using System;
using System.ComponentModel.DataAnnotations;
using WebApiYard.Controllers.ValidationModels.CustomDataAnnotations;

namespace WebApiYard.Controllers.ValidationModels
{
    public class OrderItemUpdate : OrderItemCreate
    {
        [Required]
        [OrderItemExistDB]
        public Guid Id { get; set; }
    }
}
