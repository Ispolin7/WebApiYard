using System;
using System.ComponentModel.DataAnnotations;
using WebApiYard.Controllers.ValidationModels.CustomDataAnnotations;

namespace WebApiYard.Controllers.ValidationModels
{
    public class CustomerUpdate : OrderCreate
    {
        [Required]
        [CustomerExistsDB]
        public Guid Id { get; set; }
    }
}
