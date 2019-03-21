using System;
using System.Collections.Generic;
using System.Linq;
using WebApiYard.Mappings;
using WebApiYard.Repositories;
using WebApiYard.Services.Interfaces;
using WebApiYard.Services.Models;

namespace WebApiYard.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Repositories.Models.Order> orderRepository;
        private readonly RepositoryToServiceMapper upMapper;

        /// <summary>
        /// Initialization of all repositories
        /// </summary>
        public OrderService()
        {
            this.orderRepository = new Repository<Repositories.Models.Order>();
            this.upMapper = new RepositoryToServiceMapper();
        }


        /// <summary>
        /// Get all orders from DB
        /// </summary>
        /// <returns>Orders collection</returns>
        public IEnumerable<Order> All()
        {
            var orders = this.orderRepository.All()
                .Where(o => o.IsDelete != true)
                .Including(o => o.ShippingAddress)
                //.Including(o => o.Items)
                .ToList();
            return upMapper.MapOrders(orders);
           
        }

        /// <summary>
        /// Get Order information from DB
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Order or throw an exception</returns>
        public Order Get(Guid id)
        {
            var order = this.orderRepository
                .GetById(id)
                .Where(o => (o.IsDelete != true) && (o.Id == id))
                .Including(o => o.ShippingAddress)
                //.Including(o => o.Items)
                .FirstOrDefault();

            return upMapper.MapOrder(order);
            
        }

        /// <summary>
        /// Add new Order to DB
        /// </summary>
        /// <param name="order"></param>
        /// <returns>new Orders's id</returns>
        public Guid Save(Order order)
        {
            var repositoryOrder = new Repositories.Models.Order
            {
                Id = order.Id,
                AddressId = order.AddressId,
                CustomerId = order.CustomerId              
            };
            return this.orderRepository.Insert(repositoryOrder);
        }

        /// <summary>
        /// Update Orders's information in DB
        /// </summary>
        /// <param name="order"></param>
        /// <returns>result status</returns>
        public bool Update(Order order)
        {
            var oldOrder = this.GetOrderFromDB(order.Id);           
            oldOrder.AddressId = order.AddressId;
            oldOrder.CustomerId = order.CustomerId;
            oldOrder.UpdatedAt = DateTime.UtcNow;
            return this.orderRepository.Update(oldOrder);
        }

        /// <summary>
        /// SoftDelete
        /// </summary>
        /// <param name="id"></param>
        /// <returns>result status</returns>
        public bool Remove(Guid id)
        {
            var order = this.GetOrderFromDB(id);
            order.IsDelete = true;
            order.UpdatedAt = DateTime.Now;
            return this.orderRepository.Update(order);
        }

        /// <summary>
        /// Get Order from repository 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Order model or throw an exception</returns>
        public Repositories.Models.Order GetOrderFromDB(Guid id)
        {
            var order = orderRepository.GetById(id).FirstOrDefault();
            if (order == null || order.IsDelete == true)
            {
                throw new ArgumentException("Order not found");
            }
            return order;
        }
    }
}
