using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace WebApiYard.Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> All();
        T GetById(Guid id);
        Guid Insert(T entity);
        bool Delete(Guid id);
        bool Update(T entity);
        IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> AllIncluding(Func<T, bool> predicate, params Expression<Func<T, object>>[] includeProperties);
    }
}
