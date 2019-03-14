using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiYard.Repositories.Models
{
    public class OrderItem : IEntity<Guid>
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public int Quantity { get; set; }

        public double PurchasePrice { get; set; }

        public int Color { get; set; }

        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        [DefaultValue(false)]
        public bool IsDelete { get; set; }

        public DateTime CreatedAT { get; set; }

        [DefaultValue(false)]
        public DateTime UpdatedAt { get; set; }
    }

}
