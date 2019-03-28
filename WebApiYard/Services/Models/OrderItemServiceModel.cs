using System;
using WebApiYard.Common;
using WebApiYard.Repositories.Models;
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

        public OrderItem UpdateProperties(OrderItem oldOrderItem)
        {
            oldOrderItem.Quantity = this.Quantity;
            //oldOrderItem.PurchasePrice = this.Quantity * product.Price;
            oldOrderItem.Color = (int)(Colors)Enum.Parse(typeof(Colors), this.Color);
            oldOrderItem.OrderId = this.OrderId;
            oldOrderItem.ProductId = this.ProductId;
            oldOrderItem.UpdatedAt = DateTime.UtcNow;
            return oldOrderItem;
        }
    }

}
