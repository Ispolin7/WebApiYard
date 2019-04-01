using System;
using System.ComponentModel.DataAnnotations;
using WebApiYard.Controllers.ValidationModels.CustomDataAnnotations;

namespace WebApiYard.Controllers.ValidationModels
{
    public class ProductUpdate : ProductCreate
    {
        [Required]
        [ProductExistsDB]
        public Guid Id { get; set; }
    }
}
