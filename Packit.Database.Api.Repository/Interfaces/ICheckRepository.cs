// ***********************************************************************
// Assembly         : Packit.Database.Api.Repository
// Author           : ander
// Created          : 05-15-2020
//
// Last Modified By : ander
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="ICheckRepository.cs" company="Packit.Database.Api.Repository">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Packit.Model.Models;

namespace Packit.Database.Api.Repository.Interfaces
{
    /// <summary>
    /// Interface ICheckRepository
    /// Implements the <see cref="Packit.Database.Api.Repository.Interfaces.IRepository{Packit.Model.Models.Check}" />
    /// </summary>
    /// <seealso cref="Packit.Database.Api.Repository.Interfaces.IRepository{Packit.Model.Models.Check}" />
    public interface ICheckRepository : IRepository<Check>
    {
        //Declare non generic methods here.
    }
}
