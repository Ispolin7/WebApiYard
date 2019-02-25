using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiYard.Repositories.Models
{
    public class Customer : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public int Age { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedAT { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
