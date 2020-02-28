using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Packit.Model;
using System;

namespace Packit.DataAccess
{
    public class PackitContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Backpack> Backpacks { get; set; }

        public DbSet<SharedBackpack> SharedBackpacks { get; set; }

        public DbSet<ItemBackpack> ItemBackpack { get; set; }
        public DbSet<BackpackTrip> BackpackTrip { get; set; } 

        public PackitContext(DbContextOptions<PackitContext> options) : base(options) { }

        public PackitContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                DataSource = "(localdb)\\MSSQLLocalDB",
                InitialCatalog = "Packit.Local.Database",
                IntegratedSecurity = true
            };

            optionsBuilder.UseSqlServer(builder.ConnectionString.ToString(), x => x.MigrationsAssembly("Packit.Database.Migrations"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureManyToManyItemBackpack(modelBuilder);
            ConfigureManyToManyBackpackTrip(modelBuilder);
        }

        private void ConfigureManyToManyItemBackpack(ModelBuilder modelBuilder) 

        {
            modelBuilder.Entity<ItemBackpack>()
                .HasKey(ib => new { ib.ItemId, ib.BackpackId });
            modelBuilder.Entity<ItemBackpack>()
                .HasOne(ib => ib.Item)
                .WithMany(i => i.Backpacks)
                .HasForeignKey(ib => ib.ItemId);
            modelBuilder.Entity<ItemBackpack>()
                .HasOne(ib => ib.Backpack)
                .WithMany(i => i.Items)
                .HasForeignKey(ib => ib.BackpackId);
        }

        private void ConfigureManyToManyBackpackTrip(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BackpackTrip>()
                .HasKey(bt => new { bt.BackpackId, bt.TripId });
            modelBuilder.Entity<BackpackTrip>()
                .HasOne(bt => bt.Backpack)
                .WithMany(b => b.Trips)
                .HasForeignKey(bt => bt.BackpackId);
            modelBuilder.Entity<BackpackTrip>()
                .HasOne(bt => bt.Trip)
                .WithMany(t => t.Backpacks)
                .HasForeignKey(bt => bt.TripId);
        }

    }
}
