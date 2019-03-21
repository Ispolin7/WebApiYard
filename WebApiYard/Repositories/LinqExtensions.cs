using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace WebApiYard.Repositories
{
    public static class LinqExtensions
    {
        /// <summary>
        /// Extension for adding relationships
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="includes"></param>
        /// <returns>including item</returns>
        public static IQueryable<T> Including<T>(
            this IQueryable<T> query, 
            params Expression<Func<T, 
            object>>[] includes)
        where T : class, IEntity<Guid>
        {
            if (includes != null)
            {
                query = includes.Aggregate(query,
                          (current, include) => current.Include(include));
            }

            return query;
        }
    }
}
