using System;
using System.ComponentModel.DataAnnotations;
using WebApiYard.Common;

namespace WebApiYard.Controllers.ViewModels
{
    public class OrderItemView
    {
        public Guid Id { get; set; }

        public int Quantity { get; set; }

        public double PurchasePrice { get; set; }

        public string Color { get; set; }

        public Guid OrderId { get; set; }
        public OrderView Order { get; set; }

        public Guid ProductId { get; set; }
        public ProductView Products { get; set; }
    }

}
