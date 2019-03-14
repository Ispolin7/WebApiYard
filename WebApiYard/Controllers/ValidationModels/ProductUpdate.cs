using System;
using System.ComponentModel.DataAnnotations;

namespace WebApiYard.Controllers.ValidationModels
{
    public class ProductUpdate : ProductCreate
    {
        [Required]
        public Guid Id { get; set; }
    }
}
