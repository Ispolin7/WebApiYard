using System;
using System.Collections.Generic;
using System.Linq;
using WebApiYard.Repositories;
using WebApiYard.Services.Interfaces;
using WebApiYard.Services.Models;

namespace WebApiYard.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Repositories.Models.Product> productRepository;

        /// <summary>
        /// Initialization of all repositories
        /// </summary>
        public ProductService()
        {
            this.productRepository = new Repository<Repositories.Models.Product>();
        }

        /// <summary>
        /// Get all products from DB
        /// </summary>
        /// <returns>products collection</returns>
        public IEnumerable<Product> All()
        {
            var products = this.productRepository.All();

            return products.Select(o => new Product
            {
                Id = o.Id,
                Name = o.Name,
                Description = o.Description,
                Price = o.Price
            });
        }

        /// <summary>
        /// Get Product information from DB
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Product or throw an exception</returns>
        public Product Get(Guid id)
        {
            var product = this.GetProductFromDB(id);
            return new Product
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };
        }

        /// <summary>
        /// Add new Product to DB
        /// </summary>
        /// <param name="product"></param>
        /// <returns>new products's id</returns>
        public Guid Save(Product product)
        {
            var repositoryProduct = new Repositories.Models.Product
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CreatedAT = DateTime.UtcNow
            };
            return this.productRepository.Insert(repositoryProduct);
        }

        /// <summary>
        /// Update product's information in DB
        /// </summary>
        /// <param name="product"></param>
        /// <returns>result status</returns>
        public bool Update(Product product)
        {
            var oldProduct = this.GetProductFromDB(product.Id);
            var newProduct = new Repositories.Models.Product
            {
                Id = oldProduct.Id,
                Name = oldProduct.Name,
                Description = product.Description,
                Price = product.Price,
                CreatedAT = oldProduct.CreatedAT,
                UpdatedAt = DateTime.UtcNow,
            };
            return this.productRepository.Update(newProduct);
        }

        /// <summary>
        /// SoftDelete
        /// </summary>
        /// <param name="id"></param>
        /// <returns>result status</returns>
        public bool Remove(Guid id)
        {
            var product = this.GetProductFromDB(id);
            product.IsDelete = true;
            return this.productRepository.Update(product);
        }

        /// <summary>
        /// Get Product from repository 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Product model or throw an exception</returns>
        public Repositories.Models.Product GetProductFromDB(Guid id)
        {
            var product = productRepository.GetById(id);
            if (product == null || product.IsDelete == true)
            {
                throw new ArgumentException("Entity not found");
            }
            return product;
        }
    }
}
