using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApiYard.Controllers.ViewModels
{
    public class Order
    {
        public Guid Id { get; set; }

        public Guid AddressId { get; set; }
        public Address ShippingAddress { get; set; }

        public Guid CustomerId { get; set; }
        public Customer Cusromer { get; set; }

        public DateTime OrderDate { get; set; }

        public IEnumerable<OrderItem> Items { get; set; }
    }
}
