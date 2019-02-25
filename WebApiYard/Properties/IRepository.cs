using System;
using System.Collections.Generic;

namespace IT_YARD.Repositories
{
    public interface IRepository<T>
    {
        /// <summary>
        /// Get all entitys
        /// </summary>
        /// <returns>array entity</returns>
        IEnumerable<T> All();

        /// <summary>
        /// Add new entity to repository
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool Insert(T item);

        /// <summary>
        /// Get specified entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns>entity</returns>
        T GetById(Guid id);

        /// <summary>
        /// Remove entity from repository
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true if success</returns>
        bool Delete(Guid id);

        /// <summary>
        /// Update specified entity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns>true if success</returns>
        bool Update(T item);

        bool InRepository(Guid id);

    }
}
