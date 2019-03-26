using System;
using WebApiYard.Common;
using WebApiYard.Services.Interfaces;

namespace WebApiYard.Services.Models
{
    public class OrderItemServiceModel : IModelService
    {
        public Guid Id { get; set; }

        public int Quantity { get; set; }

        public double PurchasePrice { get; set; }

        public string Color { get; set; }

        public Guid OrderId { get; set; }
        public OrderServiceModel Order { get; set; }

        public Guid ProductId { get; set; }
        public ProductServiceModel Product { get; set; }
    }

}
