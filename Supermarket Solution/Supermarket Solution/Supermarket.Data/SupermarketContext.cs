namespace Supermarket.Data
{
    using System.Data.Entity;
    using Migrations;
    using MS_SQL_Server;
    using MS_SQL_Server.Models;

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
        public virtual DbSet<Supermarket> Supermarkets { get; set; }
        public virtual DbSet<SupermarketSalesProduct> SupermarketSalesProducts { get; set; }
        public virtual DbSet<VendorExpense> VendorExpenses { get; set; }
    }
}