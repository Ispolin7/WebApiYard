using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiYard.Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> All();
        T GetById(Guid id);
        Guid Insert(T entity);
        bool Delete(Guid id);
        bool Update(T entity);
    }
}
