namespace Supermarket.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using MS_SQL_Server.Models;

    public class SupermarketContext : DbContext
    {
        public SupermarketContext()
            : base("SupermarketContext")
        {
        }

        public virtual DbSet<Measure> Measures { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductType> ProductTypes { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}