using System;
using System.Collections.Generic;
using System.Linq;
using WebApiYard.Repositories;
using WebApiYard.Services.Interfaces;
using WebApiYard.Controllers.ViewModels;
using WebApiYard.Common;

namespace WebApiYard.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IRepository<Repositories.Models.OrderItem> orderItemRepository;
        private readonly IRepository<Repositories.Models.Product> productRepository;

        /// <summary>
        /// Initialization of all repositories
        /// </summary>
        public OrderItemService()
        {
            this.orderItemRepository = new Repository<Repositories.Models.OrderItem>();
            this.productRepository = new Repository<Repositories.Models.Product>();
        }

        /// <summary>
        /// Get all OrderItems from DB
        /// </summary>
        /// <returns>OrderItems collection</returns>
        public IEnumerable<OrderItem> All()
        {
            var OrderItems = this.orderItemRepository.All();

            return OrderItems.Select(o => new OrderItem
            {
                Id = o.Id,
                Quantity = o.Quantity,
                PurchasePrice = o.PurchasePrice,
                Color =  Enum.GetName(typeof(Colors), o.Color),
                OrderId = o.OrderId,
                ProductId = o.ProductId
                // TODO add Order and Product models
            });
        }

        /// <summary>
        /// Get OrderItem information from DB
        /// </summary>
        /// <param name="id"></param>
        /// <returns>OrderItem or throw an exception</returns>
        public OrderItem Get(Guid id)
        {
            var orderItem = this.GetOrderItemFromDB(id);
            return new OrderItem
            {
                Id = orderItem.Id,
                Quantity = orderItem.Quantity,
                PurchasePrice = orderItem.PurchasePrice,
                Color = Enum.GetName(typeof(Colors), orderItem.Color),
                OrderId = orderItem.OrderId,
                ProductId = orderItem.ProductId
                // TODO add Order and Product models
            };
        }

        /// <summary>
        /// Add new OrderItem to DB
        /// </summary>
        /// <param name="orderItem"></param>
        /// <returns>new OrderItems's id</returns>
        public Guid Save(OrderItem orderItem)
        {
            var product = productRepository.GetById(orderItem.ProductId);

            var repositoryOrderItem = new Repositories.Models.OrderItem
            {
                Id = orderItem.Id,
                Quantity = orderItem.Quantity,
                PurchasePrice = orderItem.Quantity * product.Price,
                Color = (int)(Colors)Enum.Parse(typeof(Colors), orderItem.Color),
                OrderId = orderItem.OrderId,
                ProductId = orderItem.ProductId,
                CreatedAT = DateTime.UtcNow
            };
            return this.orderItemRepository.Insert(repositoryOrderItem);
        }

        /// <summary>
        /// Update OrderItem's information in DB
        /// </summary>
        /// <param name="orderItem"></param>
        /// <returns>result status</returns>
        public bool Update(OrderItem orderItem)
        {
            var product = productRepository.GetById(orderItem.ProductId);
            var oldOrderItem = this.GetOrderItemFromDB(orderItem.Id);

            var newOrderItem = new Repositories.Models.OrderItem
            {
                Id = oldOrderItem.Id,
                Quantity = orderItem.Quantity,
                PurchasePrice = orderItem.Quantity * product.Price,
                Color = (int)(Colors)Enum.Parse(typeof(Colors), orderItem.Color),
                OrderId = orderItem.OrderId,
                ProductId = orderItem.ProductId,
                CreatedAT = oldOrderItem.CreatedAT,
                UpdatedAt = DateTime.UtcNow,
            };
            return this.orderItemRepository.Update(newOrderItem);
        }

        /// <summary>
        /// SoftDelete
        /// </summary>
        /// <param name="id"></param>
        /// <returns>result status</returns>
        public bool Remove(Guid id)
        {
            var OrderItem = this.GetOrderItemFromDB(id);
            OrderItem.IsDelete = true;
            return this.orderItemRepository.Update(OrderItem);
        }

        /// <summary>
        /// Get OrderItem from repository 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>OrderItem model or throw an exception</returns>
        public Repositories.Models.OrderItem GetOrderItemFromDB(Guid id)
        {
            var OrderItem = orderItemRepository.GetById(id);
            if (OrderItem == null || OrderItem.IsDelete == true)
            {
                throw new ArgumentException("Entity not found");
            }
            return OrderItem;
        }
    }
}
