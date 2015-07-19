using System.Data.Entity;
using MS_SQL_Server.Models;
using Supermarket.Data.Migrations;

namespace Supermarket.Data
{
    public class SupermarketContext : DbContext
    {
        public SupermarketContext()
            : base("SupermarketContext")
        {
           Database.SetInitializer(new MigrateDatabaseToLatestVersion<SupermarketContext, 
               Configuration>());
        }

        public virtual DbSet<Measure> Measures { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductType> ProductTypes { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }
        public virtual DbSet<MS_SQL_Server.Models.Supermarket> Supermarkets { get; set; }
        public virtual DbSet<SupermarketSalesProduct> SupermarketSalesProducts { get; set; }
    }

}