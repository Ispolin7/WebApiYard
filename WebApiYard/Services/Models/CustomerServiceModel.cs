using System;
using System.Collections.Generic;
using WebApiYard.Repositories.Models;
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

        /// <summary>
        /// Update all properties
        /// </summary>
        /// <param name="oldCustomer"></param>
        /// <returns>updated model</returns>
        public Customer UpdateProperties(Customer oldCustomer)
        {
            oldCustomer.FirstName = this.FirstName;
            oldCustomer.LastName = this.LastName;
            oldCustomer.Age = this.Age;
            oldCustomer.UpdatedAt = DateTime.Now;
            return oldCustomer;
        }
    }
}
