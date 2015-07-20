using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using MS_SQL_Server;
using Oracle;
using Supermarket.Data;
using Supermarket.ImportExel;



namespace SupermarketChain.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var msSQLcontext = new SupermarketContext();
            msSQLcontext.Vendors.FirstOrDefault();

            var importExelFiles= new ImportExel();
            importExelFiles.LoadExelReports(msSQLcontext);
            

            //var count = context.Products.Count();
            //Console.WriteLine(count);

            //var vendor = new Vendor
            //{
            //    VendorName = "IBM"
            //};
            //context.Vendors.Add(vendor);
            //context.SaveChanges();


            //var oracleContext = new OracleEntities();


            //UpdateVendorsFromOracle(oracleContext, msSQLcontext);

            //UpdateMeasuresFromOracle(oracleContext, msSQLcontext);

            //UpdateProductsTypesFromOracle(oracleContext, msSQLcontext);   

            //var products = oracleContext.PRODUCTS
            //    .Where(p => p.ISCOPIED == false && p.ISDELETED == false)
            //    .Select(p => new
            //    {
            //        p.PRODUCTNAME,
            //        p.PRICE,
            //        p.VENDOR.VENDORNAME,
            //        p.MEASURE.MEASURENAME,
            //        p.PRODUCTSTYPE.TYPENAME
            //    })
            //    .ToList();

            //foreach (var product in products)
            //{
            //    Console.WriteLine(product.PRODUCTNAME);
            //}






            //foreach (var product in products)
            //{
            //    Console.WriteLine(product.PRODUCTNAME);
            //}


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





        ///<summary>
        ///This is summary for method that transfer records from Oracle model into MQ SQL Server model.
        ///The method first select records from Oracle model which have not yet been copied or deleted.
        ///Second it inserts record into MS SQL Method and mark them as copied into Oracle model
        ///</summary>
        private static void UpdateProductsTypesFromOracle(OracleEntities oracleContext, SupermarketContext msSQLcontext)
        {

                var productstypes = oracleContext.PRODUCTSTYPES
                    .Where(pt => pt.ISCOPIED == false && pt.ISDELETED == false)
                    .Select(pt => new
                    {
                        pt.TYPENAME
                    }).ToList();

                var addedProductsTypesList = new List<string>();

                foreach (var type in productstypes)
                {
                    var typeName = type.TYPENAME;

                    try
                    {
                        msSQLcontext.ProductTypes.AddOrUpdate(
                            pt => pt.TypeName,
                            new ProductType()
                            {
                                TypeName = typeName
                            });

                        msSQLcontext.SaveChanges();

                        addedProductsTypesList.Add(typeName);
                    }
                    catch (Exception ex)
                    {
                        throw new ArgumentException();
                    }
                }

                var typesToChange = oracleContext.PRODUCTSTYPES.Where(pt => addedProductsTypesList.Contains(pt.TYPENAME)).ToList();
                typesToChange.ForEach(pt => pt.ISCOPIED = true);
                oracleContext.SaveChanges();

                //Not Implemented
                //Console.WriteLine("Added ar updated Vendors:  {0}", addedProductsTypesList);
      
        }


        ///<summary>
        ///This is summary for method that transfer records from Oracle model into MQ SQL Server model.
        ///The method first select records from Oracle model which have not yet been copied or deleted.
        ///Second it inserts record into MS SQL Method and mark them as copied into Oracle model
        ///</summary>
        private static void UpdateMeasuresFromOracle(OracleEntities oracleContext, SupermarketContext msSQLcontext)
        {

      
                var measures = oracleContext.MEASURES
                        .Where(m => m.ISCOPIED == false && m.ISDELETED == false)
                        .Select(m => new
                        {
                            m.MEASURENAME
                        }).ToList();

                var addedMeasuresList = new List<string>();

                foreach (var measure in measures)
                {
                    var measureName = measure.MEASURENAME;

                    try
                    {
                        msSQLcontext.Measures.AddOrUpdate(
                            m => m.MeasureName,
                            new Measure()
                            {
                                MeasureName = measureName
                            });

                        msSQLcontext.SaveChanges();

                        addedMeasuresList.Add(measureName);
                    }
                    catch (Exception ex)
                    {
                        throw new ArgumentException();
                    }

                }

                var measuresToChange = oracleContext.MEASURES.Where(m => addedMeasuresList.Contains(m.MEASURENAME)).ToList();
                measuresToChange.ForEach(m => m.ISCOPIED = true);
                oracleContext.SaveChanges();

                //Not Implemented
                //Console.WriteLine("Added ar updated Vendors:  {0}", addedVendorsList); 
             
        }


        ///<summary>
        ///This is summary for method that transfer records from Oracle model into MQ SQL Server model.
        ///The method first select records from Oracle model which have not yet been copied or deleted.
        ///Second it inserts record into MS SQL Method and mark them as copied into Oracle model
        ///</summary>
        private static void UpdateVendorsFromOracle(OracleEntities oracleContext, SupermarketContext msSQLcontext)
        {

                var vendors = oracleContext.VENDORS
                       .Where(v => v.ISCOPIED == false && v.ISDELETED == false)
                       .Select(v => new
                       {
                           v.VENDORNAME
                       })
                       .ToList();

                var addedVendorsList = new List<string>();

                foreach (var vendor in vendors)
                {
                    var vendorName = vendor.VENDORNAME;

                    try
                    {
                        msSQLcontext.Vendors.AddOrUpdate(
                            v => v.VendorName,
                            new Vendor()
                            {
                                VendorName = vendorName
                            });

                        msSQLcontext.SaveChanges();

                        addedVendorsList.Add(vendorName);
                    }
                    catch (Exception ex)
                    {
                        throw new ArgumentException();
                    }

                }

                var vendorsToChange = oracleContext.VENDORS.Where(v => addedVendorsList.Contains(v.VENDORNAME)).ToList();
                vendorsToChange.ForEach(v => v.ISCOPIED = true);
                oracleContext.SaveChanges();

                //Not Implemented
                //Console.WriteLine("Added ar updated Vendors:  {0}", addedVendorsList); 
            
        }
    }
}
