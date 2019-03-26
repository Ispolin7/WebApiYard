using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApiYard.Controllers.ViewModels
{
    public class OrderView
    {
        public Guid Id { get; set; }

        public Guid AddressId { get; set; }
        public AddressView ShippingAddress { get; set; }

        public Guid CustomerId { get; set; }
        public CustomerView Cusromer { get; set; }

        public DateTime OrderDate { get; set; }

        public IEnumerable<OrderItemView> Items { get; set; }
    }
}
