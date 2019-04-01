using System;
using System.Collections.Generic;
using WebApiYard.Repositories;
using WebApiYard.Services.Interfaces;
using WebApiYard.Common;
using WebApiYard.Mappings;
using WebApiYard.Services.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiYard.Repositories.Models;

namespace WebApiYard.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IRepository<OrderItem> orderItemRepository;
        private readonly IRepository<Product> productRepository;
        private readonly RepositoryToServiceMapper upMapper;

        /// <summary>
        /// Initialization of all repositories
        /// </summary>
        public OrderItemService(IRepository<OrderItem> repositoryOrderItem, IRepository<Product> repositoryProduct)
        {
            this.orderItemRepository = repositoryOrderItem ?? throw new ArgumentNullException(nameof(repositoryOrderItem));
            this.productRepository = repositoryProduct ?? throw new ArgumentNullException(nameof(repositoryProduct));
            this.upMapper = new RepositoryToServiceMapper();
        }

        /// <summary>
        /// Get all OrderItems from DB
        /// </summary>
        /// <returns>OrderItems collection</returns>
        public async Task<IEnumerable<OrderItemServiceModel>> AllAsync()
        {
            var orderItems = await this.orderItemRepository
                .All()
                .Include(i => i.Product)
                .Include(i => i.Order)
                .ToListAsync();
            return upMapper.MapOrderItems(orderItems);
        }

        /// <summary>
        /// Get OrderItem information from DB
        /// </summary>
        /// <param name="id"></param>
        /// <returns>OrderItem or throw an exception</returns>
        public async Task<OrderItemServiceModel> GetAsync(Guid id)
        {
            var orderItem = await orderItemRepository
                .GetById(id)
                .Include(i => i.Product)
                .Include(i => i.Order)
                .FirstAsync();
            return upMapper.MapOrderItem(orderItem);
        }

        /// <summary>
        /// Add new OrderItem to DB
        /// </summary>
        /// <param name="orderItem"></param>
        /// <returns>new OrderItems's id</returns>
        public async Task<Guid> SaveAsync(OrderItemServiceModel orderItem)
        {
            var product = await productRepository
                .GetById(orderItem.ProductId)
                .FirstAsync();

            var repositoryOrderItem = new OrderItem
            {
                Id = orderItem.Id,
                Quantity = orderItem.Quantity,
                PurchasePrice = orderItem.Quantity * product.Price,
                Color = (int)(Colors)Enum.Parse(typeof(Colors), orderItem.Color),
                OrderId = orderItem.OrderId,
                ProductId = orderItem.ProductId
            };
            return await this.orderItemRepository.InsertAsync(repositoryOrderItem);
        }

        /// <summary>
        /// Update OrderItem's information in DB
        /// </summary>
        /// <param name="orderItem"></param>
        /// <returns>result status</returns>
        public async Task<bool> UpdateAsync(OrderItemServiceModel orderItem)
        {
            var product = await productRepository
                .GetById(orderItem.ProductId)
                .FirstAsync();

            var oldOrderItem = await this.GetOrderItemFromDBAsync(orderItem.Id);
            var updatedOrderItem = orderItem.UpdateProperties(oldOrderItem);
            updatedOrderItem.PurchasePrice = updatedOrderItem.Quantity * product.Price;

            return await this.orderItemRepository.UpdateAsync(updatedOrderItem);
        }

        /// <summary>
        /// SoftDelete
        /// </summary>
        /// <param name="id"></param>
        /// <returns>result status</returns>
        public async Task<bool> RemoveAsync(Guid id)
        {
            var orderItem = await this.GetOrderItemFromDBAsync(id);
            orderItem.IsDelete = true;
            orderItem.UpdatedAt = DateTime.Now;
            return await this.orderItemRepository.UpdateAsync(orderItem);
        }

        /// <summary>
        /// Get OrderItem from repository 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>OrderItem model or throw an exception</returns>
        public async Task<OrderItem> GetOrderItemFromDBAsync(Guid id)
        {
            var orderItem = await orderItemRepository.GetById(id).FirstAsync();
            if (orderItem == null || orderItem.IsDelete == true)
            {
                throw new ArgumentException("Order Item not found");
            }
            return orderItem;
        }
    }
}
