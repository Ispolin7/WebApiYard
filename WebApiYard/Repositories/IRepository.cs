using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace WebApiYard.Repositories
{
    public interface IRepository<T>
    {
        IQueryable<T> All();
        IQueryable<T> GetById(Guid id);
        Guid Insert(T entity);
        bool Delete(Guid id);
        bool Update(T entity);        
    }
}
