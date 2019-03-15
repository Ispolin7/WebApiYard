using System;

namespace WebApiYard.Repositories
{
    public interface IEntity<T>
    {
        T Id { get; set; }
        bool IsDelete { get; set; }
        DateTime CreatedAT { get; set; }
        DateTime? UpdatedAt { get; set; }
    }
}
