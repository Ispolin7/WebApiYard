using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiYard.Repositories.Models
{
    public class Order : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public Guid AddressId { get; set; }
        public Address ShippingAddress { get; set; }

        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }

        //public DateTime OrderDate { get; set; }

        public IEnumerable<OrderItem> Items { get; set; }

        public bool IsDelete { get; set; }

        public DateTime CreatedAT { get; set; }

        public DateTime? UpdatedAt { get; set; }      
    }
}
