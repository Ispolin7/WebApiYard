using System;
using System.Collections.Generic;

namespace WebApiYard.Services.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        public int Age { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<WebApiYard.Repositories.Models.Order> Orders { get; set; }
    }
}
