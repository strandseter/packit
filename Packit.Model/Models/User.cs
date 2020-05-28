// ***********************************************************************
// Assembly         : Packit.Model
// Author           : ander
// Created          : 05-15-2020
//
// Last Modified By : ander
// Last Modified On : 05-28-2020
// ***********************************************************************
// <copyright file="User.cs" company="Packit.Model">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Packit.Model.NotifyPropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Packit.Model
{
    /// <summary>
    /// Class User.
    /// Implements the <see cref="Packit.Model.NotifyPropertyChanged.Observable" />
    /// </summary>
    /// <seealso cref="Packit.Model.NotifyPropertyChanged.Observable" />
    public class User : Observable
    {
        /// <summary>
        /// The first name
        /// </summary>
        private string firstName;
        /// <summary>
        /// The last name
        /// </summary>
        private string lastName;
        /// <summary>
        /// The date of birth
        /// </summary>
        private DateTimeOffset dateOfBirth;
        /// <summary>
        /// The email
        /// </summary>
        private string email;
        /// <summary>
        /// The hashed password
        /// </summary>
        private string hashedPassword;

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        [Required]
        [StringLength(30)]
        public string FirstName 
        { 
            get => firstName;
            set => Set(ref firstName, value);
        }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        [Required]
        [StringLength(30)]
        public string LastName 
        { 
            get => lastName; 
            set => Set(ref lastName, value); 
        }

        /// <summary>
        /// Gets or sets the date of birth.
        /// </summary>
        /// <value>The date of birth.</value>
        [Required]
        public DateTimeOffset DateOfBirth 
        { 
            get => dateOfBirth; 
            set => Set(ref dateOfBirth, value); 
        }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email 
        { 
            get => email; 
            set => Set(ref email, value); 
        }

        /// <summary>
        /// Gets or sets the hashed password.
        /// </summary>
        /// <value>The hashed password.</value>
        [Required]
        public string HashedPassword 
        { 
            get => hashedPassword; 
            set => Set(ref hashedPassword, value); 
        }

        /// <summary>
        /// Gets or sets the JWT token.
        /// </summary>
        /// <value>The JWT token.</value>
        [NotMapped]
        public string JwtToken { get; set; }
        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>The items.</value>
        public virtual ICollection<Item> Items { get; } = new List<Item>();
        /// <summary>
        /// Gets the backpacks.
        /// </summary>
        /// <value>The backpacks.</value>
        public virtual ICollection<Backpack> Backpacks { get; } = new List<Backpack>();
        /// <summary>
        /// Gets the trips.
        /// </summary>
        /// <value>The trips.</value>
        public virtual ICollection<Trip> Trips { get; } = new List<Trip>();

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        public User()
        {
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString() => $"{FirstName} {LastName}, {UserId}";
    }
}
