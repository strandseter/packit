// ***********************************************************************
// Assembly         : Packit.App
// Author           : ander
// Created          : 05-21-2020
//
// Last Modified By : ander
// Last Modified On : 05-27-2020
// ***********************************************************************
// <copyright file="CustomTripDataAccessFactory.cs" company="">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using Packit.App.DataAccess;
using Packit.App.DataAccess.Http;
using Packit.App.DataAccess.Local;
using Packit.App.Services;

namespace Packit.App.Factories
{
    /// <summary>
    /// Class CustomTripDataAccessFactory.
    /// </summary>
    public class CustomTripDataAccessFactory
    {
        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns>ICustomTripDataAccess.</returns>
        public ICustomTripDataAccess Create() => InternetConnectionService.IsConnectedMock() ? new CustomTripDataAccessHttp() : (ICustomTripDataAccess)new CustomTripDataAccessLocal();
    }
}
