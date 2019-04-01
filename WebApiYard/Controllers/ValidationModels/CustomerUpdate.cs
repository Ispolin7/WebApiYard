using System;
using System.ComponentModel.DataAnnotations;
using WebApiYard.Controllers.ValidationModels.CustomDataAnnotations;

namespace WebApiYard.Controllers.ValidationModels
{
    public class CustomerUpdate : CustomerCreate
    {
        [Required]
        [CustomerExistsDB]
        public Guid Id { get; set; }
    }
}
