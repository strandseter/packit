using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Packit.Model;
using System;
using System.Linq;
using Packit.Model.Models;

namespace Packit.DataAccess
{
    public class PackitContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Backpack> Backpacks { get; set; }

        public DbSet<Check> Checks { get; set; }

        public DbSet<ItemBackpack> ItemBackpack { get; set; }
        public DbSet<BackpackTrip> BackpackTrip { get; set; } 

        public PackitContext(DbContextOptions<PackitContext> options) : base(options) { }

        public PackitContext() { }

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

            optionsBuilder.UseSqlServer(builderLocal.ConnectionString.ToString(), x => x.MigrationsAssembly("Packit.Database.Migrations"));
        }

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

        private void ConfifureOneToManyItemChecks(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                .HasMany(i => i.Checks)
                .WithOne(c => c.Item)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private void ConfigureManyToManyItemBackpack(ModelBuilder modelBuilder) 
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

        private void ConfigureManyToManyBackpackTrip(ModelBuilder modelBuilder)
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
