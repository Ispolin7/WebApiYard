using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiYard.Services.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        public int Age { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<string> Orders { get; set; }
        //public bool IsDelete { get; set; }
        //public DateTime CreatedAT { get; set; }
        //public DateTime UpdatedAt { get; set; }
    }
}
