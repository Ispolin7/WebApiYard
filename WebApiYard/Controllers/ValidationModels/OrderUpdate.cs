using System;
using System.ComponentModel.DataAnnotations;
using WebApiYard.Controllers.ValidationModels.CustomDataAnnotations;

namespace WebApiYard.Controllers.ValidationModels
{
    public class OrderUpdate : OrderCreate
    {
        [Required]
        //[OrderExistsDB]
        public Guid Id { get; set; }
    }
}
