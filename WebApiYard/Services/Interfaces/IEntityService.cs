using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiYard.Services.Interfaces
{
    public interface IEntityService<T>
    {
        Task<IEnumerable<T>> AllAsync();
        Task<T> GetAsync(Guid id);
        Task<Guid> SaveAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> RemoveAsync(Guid id);
    }
}
