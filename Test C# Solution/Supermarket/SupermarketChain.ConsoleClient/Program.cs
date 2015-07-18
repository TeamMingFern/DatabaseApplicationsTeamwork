namespace SupermarketChain.ConsoleClient
{
    using System;
    using System.Linq;
    using MS_SQL_Server.Models;
    using Oracle;
    using Supermarket.Data;

    class Program
    {
        static void Main(string[] args)
        {
            var context = new SupermarketContext();
            var count = context.Products.Count();
            Console.WriteLine(count);

            //var vendor = new Vendor
            //{
            //    VendorName = "IBM"
            //};
            //context.Vendors.Add(vendor);
            //context.SaveChanges();


            var oracleContext = new OracleEntities();
            var products = oracleContext.PRODUCTS;
            foreach (var product in products)
            {
                Console.WriteLine(product.PRODUCTNAME);
            }


            //var oracleContex = new OracleEntities();

            //var products = oracleContex.PRODUCTS
            //    .Where(e => e.ISCOPIED == 0 && e.ISDELETED == 0)
            //    .Select(e => new
            //    {
            //        e.PRODUCTNAME,
            //        e.PRICE,
            //        e.VENDOR.VENDORNAME,
            //        e.MEASURE.MEASURENAME,
            //        e.PRODUCTSTYPE.TYPENAME
            //    })
            //    .ToList();

            //foreach (var product in products)
            //{
            //    Console.WriteLine(product.PRODUCTNAME);
            //}
        }
    }
}
