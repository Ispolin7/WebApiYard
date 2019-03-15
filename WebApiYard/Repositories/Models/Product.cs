using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiYard.Repositories.Models
{
    public class Product : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public bool IsDelete { get; set; }

        public DateTime CreatedAT { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
