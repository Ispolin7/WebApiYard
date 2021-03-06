﻿using System;

namespace WebApiYard.Repositories.Models
{
    public class OrderItem : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public int Quantity { get; set; }

        public double PurchasePrice { get; set; }

        public int Color { get; set; }

        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; } 

        public bool IsDelete { get; set; }

        public DateTime CreatedAT { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }

}
