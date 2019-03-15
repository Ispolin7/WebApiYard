using System;

namespace WebApiYard.Repositories.Models
{
    public class Address : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public string StreetLine1 { get; set; }

        public string StreetLine2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public bool IsDelete { get; set; }

        public DateTime CreatedAT { get; set; }

        public DateTime? UpdatedAt { get ; set; }
    }
}
