using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApiYard.Repositories
{
    public class Repository<T> : IRepository<T> where T : IEntity<Guid>
    {
        public static Dictionary<Guid, T> _entities = new Dictionary<Guid, T>();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> All()
        {
            return _entities.Values.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById(Guid id)
        {
            if (_entities.ContainsKey(id))
            {
                return _entities[id];
            }
            return default(T);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Guid Insert(T entity)
        {
            _entities.Add(entity.Id, entity);
            return entity.Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(Guid id)
        {
            if (_entities.ContainsKey(id))
            {
                _entities.Remove(id);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(T entity)
        {
            if (_entities.ContainsKey(entity.Id))
            {
                _entities[entity.Id] = entity;
                return true;
            }
            return false;
        }
    }
}
