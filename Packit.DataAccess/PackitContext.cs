using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Packit.Model;
using System;

namespace Packit.DataAccess
{
    public class PackitContext : DbContext
    {
        public DbSet<Item> Item { get; set; }

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
    }
}
