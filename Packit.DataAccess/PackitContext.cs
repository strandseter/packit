// ***********************************************************************
// Assembly         : Packit.DataAccess
// Author           : ander
// Created          : 05-22-2020
//
// Last Modified By : ander
// Last Modified On : 05-25-2020
// ***********************************************************************
// <copyright file="PackitContext.cs" company="Packit.DataAccess">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Packit.Model;
using System;
using System.Linq;
using Packit.Model.Models;

namespace Packit.DataAccess
{
    /// <summary>
    /// Class PackitContext.
    /// Implements the <see cref="Microsoft.EntityFrameworkCore.DbContext" />
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class PackitContext : DbContext
    {
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>The items.</value>
        public DbSet<Item> Items { get; set; }
        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>The users.</value>
        public DbSet<User> Users { get; set; }
        /// <summary>
        /// Gets or sets the trips.
        /// </summary>
        /// <value>The trips.</value>
        public DbSet<Trip> Trips { get; set; }
        /// <summary>
        /// Gets or sets the backpacks.
        /// </summary>
        /// <value>The backpacks.</value>
        public DbSet<Backpack> Backpacks { get; set; }
        /// <summary>
        /// Gets or sets the checks.
        /// </summary>
        /// <value>The checks.</value>
        public DbSet<Check> Checks { get; set; }
        /// <summary>
        /// Gets or sets the item backpack.
        /// </summary>
        /// <value>The item backpack.</value>
        public DbSet<ItemBackpack> ItemBackpack { get; set; }
        /// <summary>
        /// Gets or sets the backpack trip.
        /// </summary>
        /// <value>The backpack trip.</value>
        public DbSet<BackpackTrip> BackpackTrip { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PackitContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public PackitContext(DbContextOptions<PackitContext> options) : base(options) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PackitContext"/> class.
        /// </summary>
        public PackitContext() { }

        /// <summary>
        /// <para>
        /// Override this method to configure the database (and other options) to be used for this context.
        /// This method is called for each instance of the context that is created.
        /// The base implementation does nothing.
        /// </para>
        /// <para>
        /// In situations where an instance of <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions" /> may or may not have been passed
        /// to the constructor, you can use <see cref="P:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.IsConfigured" /> to determine if
        /// the options have already been set, and skip some or all of the logic in
        /// <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" />.
        /// </para>
        /// </summary>
        /// <param name="optionsBuilder">A builder used to create or modify options for this context. Databases (and other extensions)
        /// typically define extension methods on this object that allow you to configure the context.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            SqlConnectionStringBuilder builderLocal = new SqlConnectionStringBuilder
            {
                DataSource = "(localdb)\\MSSQLLocalDB",
                InitialCatalog = "Packit.DatabaseV2",
                IntegratedSecurity = true
            };

            SqlConnectionStringBuilder builderDonau = new SqlConnectionStringBuilder
            {
                DataSource = "Donau.hiof.no",
                InitialCatalog = "andersbs",
                UserID = "andersbs",
                Password = "FVCsrT9r",
                ConnectTimeout = 30,
                Encrypt = false,
                TrustServerCertificate = false,
                MultiSubnetFailover = false,
                IntegratedSecurity = false
            };

            optionsBuilder.UseSqlServer(builderDonau.ConnectionString.ToString(), x => x.MigrationsAssembly("Packit.Database.Migrations"));
        }

        /// <summary>
        /// Override this method to further configure the model that was discovered by convention from the entity types
        /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
        /// and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
        /// define extension methods on this object that allow you to configure aspects of the model that are specific
        /// to a given database.</param>
        /// <remarks>If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
        /// then this method will not be run.</remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var foreignkey in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                foreignkey.DeleteBehavior = DeleteBehavior.Restrict;
            }

            ConfigureManyToManyItemBackpack(modelBuilder);
            ConfigureManyToManyBackpackTrip(modelBuilder);
            ConfifureOneToManyItemChecks(modelBuilder);
        }

        /// <summary>
        /// Confifures the one to many item checks.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        private static void ConfifureOneToManyItemChecks(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                .HasMany(i => i.Checks)
                .WithOne(c => c.Item)
                .OnDelete(DeleteBehavior.Cascade);
        }

        /// <summary>
        /// Configures the many to many item backpack.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        private static void ConfigureManyToManyItemBackpack(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<ItemBackpack>()
                .HasKey(ib => new { ib.ItemId, ib.BackpackId });
            modelBuilder.Entity<ItemBackpack>()
                .HasOne(ib => ib.Item)
                .WithMany(i => i.Backpacks)
                .HasForeignKey(ib => ib.ItemId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ItemBackpack>()
                .HasOne(ib => ib.Backpack)
                .WithMany(i => i.Items)
                .HasForeignKey(ib => ib.BackpackId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        /// <summary>
        /// Configures the many to many backpack trip.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        private static void ConfigureManyToManyBackpackTrip(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BackpackTrip>()
                .HasKey(bt => new { bt.BackpackId, bt.TripId });
            modelBuilder.Entity<BackpackTrip>()
                .HasOne(bt => bt.Backpack)
                .WithMany(b => b.Trips)
                .HasForeignKey(bt => bt.BackpackId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<BackpackTrip>()
                .HasOne(bt => bt.Trip)
                .WithMany(t => t.Backpacks)
                .HasForeignKey(bt => bt.TripId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
