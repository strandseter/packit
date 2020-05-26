// ***********************************************************************
// Assembly         : Packit.App
// Author           : ander
// Created          : 05-15-2020
//
// Last Modified By : ander
// Last Modified On : 05-15-2020
// ***********************************************************************
// <copyright file="IRelationDataAccess.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Threading.Tasks;

namespace Packit.App.DataAccess
{
    /// <summary>
    /// Interface IRelationDataAccess
    /// </summary>
    /// <typeparam name="T1">The type of the t1.</typeparam>
    /// <typeparam name="T2">The type of the t2.</typeparam>
    public interface IRelationDataAccess<T1, T2>
    {
        /// <summary>
        /// Gets the entities in entity asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="param">The parameter.</param>
        /// <returns>Task&lt;T2[]&gt;.</returns>
        Task<T2[]> GetEntitiesInEntityAsync(int id, string param);
        /// <summary>
        /// Adds the entity to entity asynchronous.
        /// </summary>
        /// <param name="leftId">The left identifier.</param>
        /// <param name="rightId">The right identifier.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        Task<bool> AddEntityToEntityAsync(int leftId, int rightId);
        /// <summary>
        /// Deletes the entity from entity asynchronous.
        /// </summary>
        /// <param name="leftId">The left identifier.</param>
        /// <param name="rightId">The right identifier.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        Task<bool> DeleteEntityFromEntityAsync(int leftId, int rightId);
    }
}
