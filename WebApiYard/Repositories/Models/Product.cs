using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiYard.Repositories.Models
{
    public class Product : IEntity<Guid>
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        [DefaultValue(false)]
        public bool IsDelete { get; set; }

        public DateTime CreatedAT { get; set; }

        [DefaultValue(false)]
        public DateTime UpdatedAt { get; set; }
    }
}
