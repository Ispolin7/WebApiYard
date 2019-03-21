using System;
using System.Collections.Generic;
using System.Linq;
using WebApiYard.Mappings;
using WebApiYard.Repositories;
using WebApiYard.Services.Interfaces;
using WebApiYard.Services.Models;

namespace WebApiYard.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Repositories.Models.Product> productRepository;
        private readonly RepositoryToServiceMapper upMapper;

        /// <summary>
        /// Initialization of all repositories
        /// </summary>
        public ProductService()
        {
            this.productRepository = new Repository<Repositories.Models.Product>();
            this.upMapper = new RepositoryToServiceMapper();
        }

        /// <summary>
        /// Get all products from DB
        /// </summary>
        /// <returns>products collection</returns>
        public IEnumerable<Product> All()
        {
            var products = this.productRepository.All();
            return upMapper.MapProducts(products);
        }

        /// <summary>
        /// Get Product information from DB
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Product or throw an exception</returns>
        public Product Get(Guid id)
        {
            var product = productRepository.GetById(id).FirstOrDefault();
            return upMapper.MapProduct(product);
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
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
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
            oldProduct.Name = product.Name;
            oldProduct.Description = product.Description;
            oldProduct.Price = product.Price;
            return this.productRepository.Update(oldProduct);
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
            product.UpdatedAt = DateTime.Now;
            return this.productRepository.Update(product);
        }

        /// <summary>
        /// Get Product from repository 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Product model or throw an exception</returns>
        public Repositories.Models.Product GetProductFromDB(Guid id)
        {
            var product = productRepository.GetById(id).First();
            if (product == null || product.IsDelete == true)
            {
                throw new ArgumentException("Product not found");
            }
            return product;
        }
    }
}
