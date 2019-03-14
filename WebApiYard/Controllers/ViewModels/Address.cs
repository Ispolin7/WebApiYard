using System;
using System.ComponentModel.DataAnnotations;

namespace WebApiYard.Controllers.ViewModels
{
    public class Address
    {
        public Guid Id { get; set; }

        [Required]
        public string StreetLine1 { get; set; }

        [Required]
        public string StreetLine2 { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [Required]
        public string Country { get; set; }
    }
}
