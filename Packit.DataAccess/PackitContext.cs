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
        public DbSet<PackingList> PackingLists { get; set; }
        public DbSet<SharedPackingList> SharedPackingLists { get; set; }

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
            //modelBuilder.Entity<PackingList>()
            //    .HasMany(p => p.Items);

            //modelBuilder.Entity<Trip>()
            //    .HasMany(t => t.PackingLists);
        }
    }
}
