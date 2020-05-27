// ***********************************************************************
// Assembly         : Packit.Database.Api.Repository
// Author           : ander
// Created          : 05-21-2020
//
// Last Modified By : ander
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="CheckRepository.cs" company="Packit.Database.Api.Repository">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Packit.DataAccess;
using Packit.Database.Api.Repository.Generic;
using Packit.Database.Api.Repository.Interfaces;
using Packit.Model.Models;

namespace Packit.Database.Api.Repository.Classes
{
    /// <summary>
    /// Class CheckRepository.
    /// Implements the <see cref="Packit.Database.Api.Repository.Generic.GenericRepository{Packit.Model.Models.Check}" />
    /// Implements the <see cref="Packit.Database.Api.Repository.Interfaces.ICheckRepository" />
    /// </summary>
    /// <seealso cref="Packit.Database.Api.Repository.Generic.GenericRepository{Packit.Model.Models.Check}" />
    /// <seealso cref="Packit.Database.Api.Repository.Interfaces.ICheckRepository" />
    public class CheckRepository : GenericRepository<Check>, ICheckRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CheckRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public CheckRepository(PackitContext context)
            : base(context)
        {
        }

        //Implement non generic menthds here.
    }
}
