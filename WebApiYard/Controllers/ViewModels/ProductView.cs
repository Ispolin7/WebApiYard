using System;
using System.ComponentModel.DataAnnotations;

namespace WebApiYard.Controllers.ViewModels
{
    public class ProductView
    {      
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(double.Epsilon, double.MaxValue)]
        public double Price { get; set; }
    }
}
