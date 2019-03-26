using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebApiYard.Repositories
{
    public interface IRepository<T>
    {
        IQueryable<T> All();
        IQueryable<T> GetById(Guid id);
        Task<Guid> InsertAsync(T entity);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> UpdateAsync(T entity);        
    }
}
