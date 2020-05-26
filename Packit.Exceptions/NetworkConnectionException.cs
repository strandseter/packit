// ***********************************************************************
// Assembly         : Packit.Exceptions
// Author           : ander
// Created          : 05-24-2020
//
// Last Modified By : ander
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="NetworkConnectionException.cs" company="Packit.Exceptions">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Runtime.Serialization;

namespace Packit.Exceptions
{
    /// <summary>
    /// Class NetworkConnectionException.
    /// Implements the <see cref="System.Exception" />
    /// Implements the <see cref="System.Runtime.Serialization.ISerializable" />
    /// </summary>
    /// <seealso cref="System.Exception" />
    /// <seealso cref="System.Runtime.Serialization.ISerializable" />
    public class NetworkConnectionException : Exception, ISerializable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkConnectionException"/> class.
        /// </summary>
        public NetworkConnectionException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkConnectionException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public NetworkConnectionException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkConnectionException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public NetworkConnectionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkConnectionException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"></see> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"></see> that contains contextual information about the source or destination.</param>
        protected NetworkConnectionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
