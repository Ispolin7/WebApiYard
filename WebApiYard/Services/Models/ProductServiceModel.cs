using System;
using WebApiYard.Repositories.Models;
using WebApiYard.Services.Interfaces;

namespace WebApiYard.Services.Models
{
    public class ProductServiceModel : IModelService
    {      
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public Product UpdateProperties(Product oldProduct)
        {
            oldProduct.Name = this.Name;
            oldProduct.Description = this.Description;
            oldProduct.Price = this.Price;
            oldProduct.UpdatedAt = DateTime.Now;
            return oldProduct;
        }
    }
}
