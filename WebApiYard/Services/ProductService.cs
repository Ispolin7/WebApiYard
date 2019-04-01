using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiYard.Mappings;
using WebApiYard.Repositories;
using WebApiYard.Repositories.Models;
using WebApiYard.Services.Interfaces;
using WebApiYard.Services.Models;

namespace WebApiYard.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> productRepository;
        private readonly RepositoryToServiceMapper upMapper;

        /// <summary>
        /// Initialization of all repositories
        /// </summary>
        public ProductService(IRepository<Product> products)
        {
            this.productRepository = products ?? throw new ArgumentNullException(nameof(products));
            this.upMapper = new RepositoryToServiceMapper();
        }

        /// <summary>
        /// Get all products from DB
        /// </summary>
        /// <returns>products collection</returns>
        public async Task<IEnumerable<ProductServiceModel>> AllAsync()
        {
            var products = await this.productRepository
                .All()
                .ToListAsync();
            return upMapper.MapProducts(products);
        }

        /// <summary>
        /// Get Product information from DB
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Product or throw an exception</returns>
        public async Task<ProductServiceModel> GetAsync(Guid id)
        {
            var product = await productRepository
                .GetById(id)
                .FirstAsync();
            return upMapper.MapProduct(product);
        }

        /// <summary>
        /// Add new Product to DB
        /// </summary>
        /// <param name="product"></param>
        /// <returns>new products's id</returns>
        public async Task<Guid> SaveAsync(ProductServiceModel product)
        {
            var repositoryProduct = new Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };
            return await this.productRepository.InsertAsync(repositoryProduct);
        }

        /// <summary>
        /// Update product's information in DB
        /// </summary>
        /// <param name="product"></param>
        /// <returns>result status</returns>
        public async Task<bool> UpdateAsync(ProductServiceModel product)
        {
            var oldProduct = await this.GetProductFromDBAsync(product.Id);
            var updatedProduct = product.UpdateProperties(oldProduct);
            return await this.productRepository.UpdateAsync(updatedProduct);
        }

        /// <summary>
        /// SoftDelete
        /// </summary>
        /// <param name="id"></param>
        /// <returns>result status</returns>
        public async Task<bool> RemoveAsync(Guid id)
        {
            var product = await this.GetProductFromDBAsync(id);
            product.IsDelete = true;
            product.UpdatedAt = DateTime.Now;
            return await this.productRepository.UpdateAsync(product);
        }

        /// <summary>
        /// Get Product from repository 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Product model or throw an exception</returns>
        public async Task<Product> GetProductFromDBAsync(Guid id)
        {
            var product = await productRepository.GetById(id).FirstAsync();
            if (product == null || product.IsDelete == true)
            {
                throw new ArgumentException("Product not found");
            }
            return product;
        }
    }
}
