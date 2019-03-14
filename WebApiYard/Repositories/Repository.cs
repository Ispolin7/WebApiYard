using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebApiYard.DAL;

namespace WebApiYard.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IEntity<Guid>
    {
        public static Dictionary<Guid, T> _entities = new Dictionary<Guid, T>();

        protected readonly DbContext dbContext;
        protected readonly DbSet<T> dbSet;

        public Repository()
        {
            dbContext = new ApiContext();
            dbSet = dbContext.Set<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> All()
        {
            return dbSet
                .AsNoTracking()
                .Where(e => e.IsDelete != true)
                .ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById(Guid id)
        {
            return dbSet
                .AsNoTracking()
                .Where(e => e.Id == id)
                .Where(e => e.IsDelete != true)
                .FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Guid Insert(T entity)
        {
            dbSet.Add(entity);
            dbContext.SaveChanges();
            return entity.Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(Guid id)
        {
            dbSet.Remove(dbSet.FirstOrDefault(e => e.Id == id));
            dbContext.SaveChanges();
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(T entity)
        {
            dbSet.Update(entity);
            dbContext.SaveChanges();
            return true;
        }


        public virtual IQueryable<T> GetAllLazyLoad(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] children)
        {
            children.ToList().ForEach(x => this.dbSet.Include(x).Load());
            return dbSet;
        }

        public IEnumerable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            return Include(includeProperties).AsNoTracking();
        }

        public IEnumerable<T> AllIncluding(
            Func<T, bool> predicate,
            params Expression<Func<T, object>>[] includeProperties)
        {
            var query = Include(includeProperties).AsNoTracking();
            return query.Where(predicate);
        }

        private IQueryable<T> Include(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = dbSet.AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        //public IEnumerable<T> AllIncluding(
        //    Func<T, bool> predicate,
        //    Func<T, object> include,
        //    params Expression<Func<T, object>>[] thenInclude)
        //{
        //    var query = ThenInclude(include);
        //    return query.Where(predicate);
        //}

        //private IQueryable<T> ThenInclude(Func<T, object> include, params Expression<Func<T, object>>[] thenInclude)
        //{
        //    IQueryable<T> query = dbSet.AsNoTracking();
        //    return thenInclude
        //        .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        //}

        public IQueryable<T> Get(Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null)
        {
            IQueryable<T> queryable = dbSet;

            if (includes != null)
            {
                queryable = includes(queryable);
            }

            return queryable;
        }
    }
}
