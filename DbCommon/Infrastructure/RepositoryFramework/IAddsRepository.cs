using System;
using System.Collections.Generic;

namespace DbCommon.Infrastructure.RepositoryFramework
{
    /// <summary>
    /// IAddsRepository
    /// </summary>
    /// <typeparam name="T">T</typeparam>
    public interface IAddsRepository<in T> : IDisposable
    {
        /// <summary>
        /// Add
        /// </summary>
        /// <param name="entity">entity</param>
        void Add(IEnumerable<T> entity);
    }
}