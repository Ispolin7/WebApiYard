using System;
using WebApiYard.Services.Interfaces;

namespace WebApiYard.Services.Models
{
    public class ProductServiceModel : IModelService
    {      
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }
    }
}
