using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiYard.Services.Interfaces
{
    public interface IEntityService<T>
    {
        IEnumerable<T> All();
        T Get(Guid id);
        Guid Save(T entity);
        bool Update(T entity);
        bool Remove(Guid id);
    }
}
