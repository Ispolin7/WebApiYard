using System;
using System.Collections.Generic;
using WebApiYard.Services.Interfaces;

namespace WebApiYard.Services.Models
{
    public class CustomerServiceModel : IModelService
    {
        public Guid Id { get; set; }
        public int Age { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<OrderServiceModel> Orders { get; set; }
    }
}
