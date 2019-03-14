using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiYard.Repositories.Models
{
    public class Address : IEntity<Guid>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string StreetLine1 { get; set; }

        public string StreetLine2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        [DefaultValue(false)]
        public bool IsDelete { get; set; }

        public DateTime CreatedAT { get; set; }

        [DefaultValue(false)]
        public DateTime UpdatedAt { get ; set; }
    }
}
