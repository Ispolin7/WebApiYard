using System;
using System.Collections.Generic;
using WebApiYard.Controllers.ViewModels;
using WebApiYard.Services.Interfaces;

namespace WebApiYard.Services.Models
{
    public class OrderServiceModel : IModelService
    {
        public Guid Id { get; set; }

        public Guid AddressId { get; set; }
        public AddressServiceModel ShippingAddress { get; set; }

        public Guid CustomerId { get; set; }
        public CustomerServiceModel Cusromer { get; set; }

        public DateTime OrderDate { get; set; }

        public IEnumerable<OrderItemServiceModel> Items { get; set; }  
    }
}
