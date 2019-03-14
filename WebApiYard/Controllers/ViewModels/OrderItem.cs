using System;
using System.ComponentModel.DataAnnotations;
using WebApiYard.Common;

namespace WebApiYard.Controllers.ViewModels
{
    public class OrderItem
    {
        public Guid Id { get; set; }

        public int Quantity { get; set; }

        public double PurchasePrice { get; set; }

        public string Color { get; set; }

        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        public Guid ProductId { get; set; }
        public Product Products { get; set; }
    }

}
