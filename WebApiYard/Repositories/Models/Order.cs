using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiYard.Repositories.Models
{
    public class Order : IEntity<Guid>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid AddressId { get; set; }
        public Address ShippingAddress { get; set; }

        public Guid CustomerId { get; set; }
        //public Customer Cusromer { get; set; }

        public DateTime OrderDate { get; set; }

        public IEnumerable<OrderItem> Items { get; set; }

        [DefaultValue(false)]
        public bool IsDelete { get; set; }

        public DateTime CreatedAT { get; set; }

        [DefaultValue(false)]
        public DateTime UpdatedAt { get; set; }      
    }
}
