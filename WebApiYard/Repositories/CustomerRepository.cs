using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiYard.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApiYard.Repositories
{
    public class CustomerRepository : Repository<Customer>
    {
        public CustomerRepository() : base() { }

        //public Customer Include(Guid id)
        //{
        //    return dbSet
        //            .Where(c => c.Id == id)
        //            .Include(c => c.Orders.Where(o => o.IsDelete != true))
        //                .ThenInclude(o => o.Items.Where(i => i.IsDelete != true))
        //                    .ThenInclude(i => i.Product)
        //            .Include(c => c.Orders)
        //                .ThenInclude(o => o.ShippingAddress)
        //            .FirstOrDefault();
                    
        //}
    }
}
