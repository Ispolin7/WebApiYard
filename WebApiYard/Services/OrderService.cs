using System;
using System.Collections.Generic;
using System.Linq;
using WebApiYard.Repositories;
using WebApiYard.Services.Interfaces;
using WebApiYard.Services.Models;

namespace WebApiYard.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Repositories.Models.Order> orderRepository;

        /// <summary>
        /// Initialization of all repositories
        /// </summary>
        public OrderService()
        {
            this.orderRepository = new Repository<Repositories.Models.Order>();
        }


        /// <summary>
        /// Get all orders from DB
        /// </summary>
        /// <returns>Orders collection</returns>
        public IEnumerable<Order> All()
        {
            var orders = this.orderRepository.All();

            return orders.Select(o => new Order
            {
                Id = o.Id,
                AddressId = o.AddressId,
                OrderDate = o.OrderDate,
                CustomerId = o.CustomerId
            });
        }

        /// <summary>
        /// Get Order information from DB
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Order or throw an exception</returns>
        public Order Get(Guid id)
        {
            var order = this.orderRepository.AllIncluding(o => o.ShippingAddress).First();
            return new Order
            {
                Id = order.Id,
                AddressId = order.AddressId,
                CustomerId = order.CustomerId,
                ShippingAddress = new Address
                {
                    Id = order.ShippingAddress.Id,
                    StreetLine1 = order.ShippingAddress.StreetLine1,
                    StreetLine2 = order.ShippingAddress.StreetLine2
                }
            };
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
                CustomerId = order.CustomerId,
                OrderDate = DateTime.UtcNow,
                CreatedAT = DateTime.UtcNow               
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
            var newOrder = new Repositories.Models.Order
            {
                Id = oldOrder.Id,
                AddressId = oldOrder.AddressId,
                CustomerId = oldOrder.CustomerId,
                OrderDate = oldOrder.OrderDate,
                CreatedAT = oldOrder.CreatedAT,
                UpdatedAt = DateTime.UtcNow,
            };
            return this.orderRepository.Update(newOrder);
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
            return this.orderRepository.Update(order);
        }

        /// <summary>
        /// Get Order from repository 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Order model or throw an exception</returns>
        public Repositories.Models.Order GetOrderFromDB(Guid id)
        {
            var order = orderRepository.GetById(id);
            if (order == null || order.IsDelete == true)
            {
                throw new ArgumentException("Entity not found");
            }
            return order;
        }
    }
}
