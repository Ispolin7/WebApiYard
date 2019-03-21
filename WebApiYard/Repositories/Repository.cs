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
        public IQueryable<T> All()
        {
            return dbSet.AsNoTracking();               
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IQueryable<T> GetById(Guid id)
        {
            return dbSet
                .AsNoTracking()
                .Where(e => e.Id == id)
                .Where(e => e.IsDelete != true);
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
            entity.UpdatedAt = DateTime.Now;
            dbSet.Update(entity);
            dbContext.SaveChanges();
            return true;
        }        
    }
}
