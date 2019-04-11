using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiYard.Repositories.Models;
using WebApiYard.Services.Models;

namespace WebApiYard.Services.Interfaces
{
    public interface ICustomerService : IEntityService<CustomerServiceModel>
    {
        Task<Customer> GetCustomerFromDBAsync(Guid id);           
    }
}
