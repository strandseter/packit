// ***********************************************************************
// Assembly         : Packit.App
// Author           : ander
// Created          : 04-13-2020
//
// Last Modified By : ander
// Last Modified On : 05-12-2020
// ***********************************************************************
// <copyright file="IBasicDataAccess.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Threading.Tasks;

namespace Packit.App.DataAccess
{
    /// <summary>
    /// Interface IBasicDataAccess
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBasicDataAccess<T>
    {
        /// <summary>
        /// Adds asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        Task<bool> AddAsync(T entity);
        /// <summary>
        /// Updates asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        Task<bool> UpdateAsync(T entity);
        /// <summary>
        /// Deletes asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        Task<bool> DeleteAsync(T entity);
        /// <summary>
        /// Gets asynchronous.
        /// </summary>
        /// <returns>Task&lt;T[]&gt;.</returns>
        Task<T[]> GetAllAsync();
        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Task&lt;T&gt;.</returns>
        Task<T> GetByIdAsync(T entity);
        /// <summary>
        /// Gets all with child entities asynchronous.
        /// </summary>
        /// <returns>Task&lt;T[]&gt;.</returns>
        Task<T[]> GetAllWithChildEntitiesAsync();
        /// <summary>
        /// Gets the by identifier with child entities asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Task&lt;T&gt;.</returns>
        Task<T> GetByIdWithChildEntitiesAsync(T entity);
    }
}
