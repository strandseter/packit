using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Packit.Model;
using System;
using System.Linq;

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

            
            //modelBuilder.Entity<User>().HasMany(i => i.Items).WithRequired().WillCascadeOnDelete(false);

            ConfigureManyToManyItemBackpack(modelBuilder);
            ConfigureManyToManyBackpackTrip(modelBuilder);
            //ConfigureOneToOneBackpackSharedBackpack(modelBuilder);
            //ConfigureOneToManyUserItems(modelBuilder);
            //ConfigureOneToManyUserBackpacks(modelBuilder);
            //ConfigureOneToManyUserTrips(modelBuilder);

            //modelBuilder.Entity<Item>()
            //    .HasOne(i => i.User).WithMany(u => u.Items)
            //        .HasForeignKey(i => i.UserId)
            //        .OnDelete(DeleteBehavior.Cascade);

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


            //modelBuilder.Entity<Backpack>().HasOne(b => b.User).WithMany(u => u.Backpacks)
            //    .HasForeignKey(b => b.UserId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Trip>().HasOne(t => t.User).WithMany(u => u.Trips)
            //    .HasForeignKey(t => t.UserId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Trip>().HasOne(t => t.User).WithMany(u => u.Trips)
            //    .HasForeignKey(t => t.UserId)
            //.OnDelete(DeleteBehavior.Restrict);


        }

        //private void ConfigureOneToOneBackpackSharedBackpack(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Backpack>()
        //        .HasOne(b => b.SharedBackpack)
        //        .WithOne(s => s.Backpack)
        //        .HasForeignKey<SharedBackpack>(s => s.Backpack);
        //}

        //private void ConfigureOneToManyUserItems(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<User>()
        //        .HasMany(u => u.Items)
        //        .WithOne(i => i.User);
        //}

        //private void ConfigureOneToManyUserBackpacks(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<User>()
        //        .HasMany(u => u.Backpacks)
        //        .WithOne(b => b.User);
        //}

        //private void ConfigureOneToManyUserTrips(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<User>()
        //        .HasMany(u => u.Trips)
        //        .WithOne(t => t.User);
        //}
    }
}
