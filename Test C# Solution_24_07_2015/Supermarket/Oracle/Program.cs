using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oracle
{
    class Program
    {
        static void Main(string[] args)
        {
            //var oracleContext = new OracleEntities();


            var oracleContext = new OracleEntities();
            var products = oracleContext.PRODUCTS;
            foreach (var product in products)
            {
                Console.WriteLine(product.PRODUCTNAME);
            }


            //var products = oracleContex.PRODUCTS
            //    .Where(e => e.ISCOPIED.Equals(0) && e.ISDELETED.Equals(0))
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
