using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiYard.DAL;

namespace WebApiYard.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IEntity<Guid>
    {
        //public static Dictionary<Guid, T> _entities = new Dictionary<Guid, T>();

        protected readonly DbContext dbContext;
        protected readonly DbSet<T> dbSet;

        public Repository(ApiContext context)
        {
            dbContext = context ?? throw new ArgumentNullException(nameof(context));
            dbSet = dbContext.Set<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> All()
        {
            return dbSet
                .AsNoTracking()
                .Where(e => e.IsDelete != true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IQueryable<T> GetById(Guid id)
        {
            // TODO get deleted entity, 500
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
        public async Task<Guid> InsertAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity.Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Guid id)
        {
            dbSet.Remove(dbSet.FirstOrDefault(e => e.Id == id));
            await dbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(T entity)
        {
            // TODO The instance cannot be tracked because another instance with the key value 
            //entity.UpdatedAt = DateTime.Now;
            dbSet.Update(entity);
            await dbContext.SaveChangesAsync();
            return true;
        }        
    }
}
