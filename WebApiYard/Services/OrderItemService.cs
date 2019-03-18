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
                // TODO add Order and Product models
                OrderId = orderItem.OrderId,
                ProductId = orderItem.ProductId
                
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
                ProductId = orderItem.ProductId
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

            oldOrderItem.Quantity = orderItem.Quantity;
            oldOrderItem.PurchasePrice = orderItem.Quantity * product.Price;
            oldOrderItem.Color = (int)(Colors)Enum.Parse(typeof(Colors), orderItem.Color);
            oldOrderItem.OrderId = orderItem.OrderId;
            oldOrderItem.ProductId = orderItem.ProductId;
            oldOrderItem.UpdatedAt = DateTime.UtcNow;

            return this.orderItemRepository.Update(oldOrderItem);
        }

        /// <summary>
        /// SoftDelete
        /// </summary>
        /// <param name="id"></param>
        /// <returns>result status</returns>
        public bool Remove(Guid id)
        {
            var orderItem = this.GetOrderItemFromDB(id);
            orderItem.IsDelete = true;
            orderItem.UpdatedAt = DateTime.Now;
            return this.orderItemRepository.Update(orderItem);
        }

        /// <summary>
        /// Get OrderItem from repository 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>OrderItem model or throw an exception</returns>
        public Repositories.Models.OrderItem GetOrderItemFromDB(Guid id)
        {
            var orderItem = orderItemRepository.GetById(id);
            if (orderItem == null || orderItem.IsDelete == true)
            {
                throw new ArgumentException("Order Item not found");
            }
            return orderItem;
        }
    }
}
