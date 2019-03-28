using System;
using System.Collections.Generic;
using WebApiYard.Repositories.Models;
using WebApiYard.Services.Interfaces;

namespace WebApiYard.Services.Models
{
    public class OrderServiceModel : IModelService
    {
        public Guid Id { get; set; }

        public Guid AddressId { get; set; }
        public AddressServiceModel ShippingAddress { get; set; }

        public Guid CustomerId { get; set; }
        public CustomerServiceModel Cusromer { get; set; }

        public DateTime OrderDate { get; set; }

        public IEnumerable<OrderItemServiceModel> Items { get; set; }  

        /// <summary>
        /// Update all properties
        /// </summary>
        /// <param name="oldOrder"></param>
        /// <returns>updated entity</returns>
        public Order UpdateProperties(Order oldOrder)
        {
            oldOrder.AddressId = this.AddressId;
            oldOrder.CustomerId = this.CustomerId;
            oldOrder.UpdatedAt = DateTime.UtcNow;
            return oldOrder;
        }
    }
}
