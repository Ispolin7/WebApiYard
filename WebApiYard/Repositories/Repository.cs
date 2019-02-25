using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApiYard.Repositories
{
    public class Repository<T> : IRepository<T> where T : IEntity<Guid>
    {
        private static Dictionary<Guid, T> _users = new Dictionary<Guid, T>();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> All()
        {
            return _users.Values.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById(Guid id)
        {
            if (_users.ContainsKey(id))
            {
                return _users[id];
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
            _users.Add(entity.Id, entity);
            return entity.Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(Guid id)
        {
            if (_users.ContainsKey(id))
            {
                _users.Remove(id);
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
            if (_users.ContainsKey(entity.Id))
            {
                _users[entity.Id] = entity;
                return true;
            }
            return false;
        }
    }
}
