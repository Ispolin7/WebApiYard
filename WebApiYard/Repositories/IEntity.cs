using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiYard.Repositories
{
    public interface IEntity<T>
    {
        T Id { get; set; }
        bool IsDelete { get; set; }
        DateTime CreatedAT { get; set; }
        DateTime UpdatedAt { get; set; }
    }
}
