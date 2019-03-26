using System;
using WebApiYard.Services.Interfaces;

namespace WebApiYard.Services.Models
{
    public class AddressServiceModel : IModelService
    {
        public Guid Id { get; set; }

        public string StreetLine1 { get; set; }

        public string StreetLine2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }
    }
}
