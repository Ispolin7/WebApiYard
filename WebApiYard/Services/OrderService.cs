using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiYard.Mappings;
using WebApiYard.Repositories;
using WebApiYard.Repositories.Models;
using WebApiYard.Services.Interfaces;
using WebApiYard.Services.Models;

namespace WebApiYard.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> orderRepository;
        private readonly RepositoryToServiceMapper upMapper;

        /// <summary>
        /// Initialization of all repositories
        /// </summary>
        public OrderService(IRepository<Order> orders)
        {
            this.orderRepository = orders ?? throw new ArgumentNullException(nameof(orders));
            this.upMapper = new RepositoryToServiceMapper();
        }


        /// <summary>
        /// Get all orders from DB
        /// </summary>
        /// <returns>Orders collection</returns>
        public async Task<IEnumerable<OrderServiceModel>> AllAsync()
        {
            var orders = await this.orderRepository.All()
                .Where(o => o.IsDelete != true)
                //.Including(o => o.ShippingAddress)
                .Include(o => o.ShippingAddress)
                .Include(o => o.Customer)
                .Include(o => o.Items)
                    .ThenInclude(i => i.Product)                        
                .ToListAsync();

            return upMapper.MapOrders(orders);
           
        }

        /// <summary>
        /// Get Order information from DB
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Order or throw an exception</returns>
        public async Task<OrderServiceModel> GetAsync(Guid id)
        {
            var order = await this.orderRepository
                .GetById(id)
                .Include(o => o.ShippingAddress)
                .Include(o => o.Customer)
                .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync();

            return upMapper.MapOrder(order);            
        }

        /// <summary>
        /// Add new Order to DB
        /// </summary>
        /// <param name="order"></param>
        /// <returns>new Orders's id</returns>
        public async Task<Guid> SaveAsync(OrderServiceModel order)
        {
            var repositoryOrder = new Order
            {
                Id = order.Id,
                AddressId = order.AddressId,
                CustomerId = order.CustomerId              
            };
            return await this.orderRepository.InsertAsync(repositoryOrder);
        }

        /// <summary>
        /// Update Orders's information in DB
        /// </summary>
        /// <param name="order"></param>
        /// <returns>result status</returns>
        public async Task<bool> UpdateAsync(OrderServiceModel order)
        {
            var oldOrder = await this.GetOrderFromDBAsync(order.Id);
            var updatedOrder = order.UpdateProperties(oldOrder);
            return await this.orderRepository.UpdateAsync(updatedOrder);
        }

        /// <summary>
        /// SoftDelete
        /// </summary>
        /// <param name="id"></param>
        /// <returns>result status</returns>
        public async Task<bool> RemoveAsync(Guid id)
        {
            var order = await this.GetOrderFromDBAsync(id);
            order.IsDelete = true;
            order.UpdatedAt = DateTime.Now;
            return await this.orderRepository.UpdateAsync(order);
        }

        /// <summary>
        /// Get Order from repository 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Order model or throw an exception</returns>
        public async Task<Order> GetOrderFromDBAsync(Guid id)
        {
            var order = await orderRepository.GetById(id).FirstOrDefaultAsync();
            if (order == null || order.IsDelete == true)
            {
                throw new ArgumentException("Order not found");
            }
            return order;
        }
    }
}
