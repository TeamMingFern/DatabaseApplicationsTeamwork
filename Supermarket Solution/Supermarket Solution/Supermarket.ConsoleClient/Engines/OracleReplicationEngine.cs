using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketChain.ConsoleClient.Engines
{
    using System.Data.Entity.Migrations;
    using MS_SQL_Server;
    using MS_SQL_Server.Models;
    using Oracle;
    using Supermarket.Data;

    public static class  OracleReplicationEngine
    {
        public static void ReplicateOracleToMssql()
        {

            Console.WriteLine(@"        Replicating OraclaDB To MsSqlDB
------------------------------------------------");

            UpdateVendorsFromOracle();

            UpdateMeasuresFromOracle();

            UpdateProductsTypesFromOracle();

            UpdateProductsFromOracle();
            
        }

        ///<summary>
        ///This is summary for method that transfer records from Oracle model into MQ SQL Server model.
        ///The method first select records from Oracle model which have not yet been copied or deleted.
        ///Second it inserts record into MS SQL Method and mark them as copied into Oracle model
        ///</summary>
        public static void UpdateProductsFromOracle()
        {
            var oracleContext = new OracleEntities();
            var msSqLcontext = new SupermarketContext();
            var products = oracleContext.PRODUCTS
                .Where(p => p.ISCOPIED == false && p.ISDELETED == false)
                .Select(p => new
                {
                    p.PRODUCTNAME,
                    p.PRICE,
                    p.VENDOR.VENDORNAME,
                    p.MEASURE.MEASURENAME,
                    p.PRODUCTSTYPE.TYPENAME
                }).ToList();

            if (products.Count > 0)
            {
                var addedProductsList = new List<string>();

                foreach (var product in products)
                {
                    var productName = product.PRODUCTNAME;
                    var price = product.PRICE;
                    var vendorId = MsSqlFactory.GetVendorIdByName(product.VENDORNAME);
                    var measureId = MsSqlFactory.GetMeasureIdByName(product.MEASURENAME);
                    var typeId = MsSqlFactory.GetTypeIdByName(product.TYPENAME);

                    try
                    {
                        msSqLcontext.Products.AddOrUpdate(
                            p => p.ProductName,
                            new Product()
                            {
                                ProductName = productName,
                                VendorId = vendorId,
                                MeasureId = measureId,
                                ProductTypeId = typeId,
                                Price = (float)price
                            });

                        msSqLcontext.SaveChanges();
                        addedProductsList.Add(productName);
                    }
                    catch (Exception ex)
                    {
                        throw new ArgumentException();
                    }
                }

                var productsToChange =
                    oracleContext.PRODUCTS.Where(p => addedProductsList.Contains(p.PRODUCTNAME)).ToList();
                productsToChange.ForEach(p => p.ISCOPIED = true);
                oracleContext.SaveChanges();

                Console.WriteLine("\nAdded new Products from OracleBD into MS SQL Server:");
                productsToChange.ForEach(p => Console.WriteLine("Added product name: {0}", p.PRODUCTNAME));
            }
            else
            {
                Console.WriteLine("\nThere is no new records to import into PRODUCTS table!");
            }
        }


        ///<summary>
        ///This is summary for method that transfer records from Oracle model into MQ SQL Server model.
        ///The method first select records from Oracle model which have not yet been copied or deleted.
        ///Second it inserts record into MS SQL Method and mark them as copied into Oracle model
        ///</summary>
        public static void UpdateProductsTypesFromOracle()
        {
            var oracleContext = new OracleEntities();
            var msSqLcontext = new SupermarketContext();
            var productstypes = oracleContext.PRODUCTSTYPES
                .Where(pt => pt.ISCOPIED == false && pt.ISDELETED == false)
                .Select(pt => new
                {
                    pt.TYPENAME
                }).ToList();

            if (productstypes.Count > 0)
            {
                var addedProductsTypesList = new List<string>();

                foreach (var type in productstypes)
                {
                    var typeName = type.TYPENAME;

                    try
                    {
                        msSqLcontext.ProductTypes.AddOrUpdate(
                            pt => pt.TypeName,
                            new ProductType()
                            {
                                TypeName = typeName
                            });

                        msSqLcontext.SaveChanges();
                        addedProductsTypesList.Add(typeName);
                    }
                    catch (Exception ex)
                    {
                        throw new ArgumentException();
                    }
                }

                var typesToChange =
                    oracleContext.PRODUCTSTYPES.Where(pt => addedProductsTypesList.Contains(pt.TYPENAME)).ToList();
                typesToChange.ForEach(pt => pt.ISCOPIED = true);
                oracleContext.SaveChanges();

                Console.WriteLine("\nAdded new Types from OracleBD into MS SQL Server:");
                typesToChange.ForEach(tp => Console.WriteLine("Added types name: {0}", tp.TYPENAME));
            }
            else
            {
                Console.WriteLine("\nThere is no new records to import into PRODUCTSTYPES table!");
            }
        }


        ///<summary>
        ///This is summary for method that transfer records from Oracle model into MQ SQL Server model.
        ///The method first select records from Oracle model which have not yet been copied or deleted.
        ///Second it inserts record into MS SQL Method and mark them as copied into Oracle model
        ///</summary>
        public static void UpdateMeasuresFromOracle()
        {
            var oracleContext = new OracleEntities();
            var msSqLcontext = new SupermarketContext();
            var measures = oracleContext.MEASURES
                    .Where(m => m.ISCOPIED == false && m.ISDELETED == false)
                    .Select(m => new
                    {
                        m.MEASURENAME
                    }).ToList();

            if (measures.Count > 0)
            {
                var addedMeasuresList = new List<string>();

                foreach (var measure in measures)
                {
                    var measureName = measure.MEASURENAME;

                    try
                    {
                        msSqLcontext.Measures.AddOrUpdate(
                            m => m.MeasureName,
                            new Measure()
                            {
                                MeasureName = measureName
                            });

                        msSqLcontext.SaveChanges();
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

                Console.WriteLine("\nAdded new Measures from OracleBD into MS SQL Server:");
                measuresToChange.ForEach(m => Console.WriteLine("Added measure name: {0}", m.MEASURENAME));
            }
            else
            {
                Console.WriteLine("\nThere is no new records to import into MEASURES table!");
            }
        }


        ///<summary>
        ///This is summary for method that transfer records from Oracle model into MQ SQL Server model.
        ///The method first select records from Oracle model which have not yet been copied or deleted.
        ///Second it inserts record into MS SQL Method and mark them as copied into Oracle model
        ///</summary>
        public static void UpdateVendorsFromOracle()
        {
            var oracleContext = new OracleEntities();
            var msSqLcontext = new SupermarketContext();
            var vendors = oracleContext.VENDORS
                    .Where(v => v.ISCOPIED == false && v.ISDELETED == false)
                    .Select(v => new
                    {
                        v.VENDORNAME
                    })
                    .ToList();

            if (vendors.Count > 0)
            {
                var addedVendorsList = new List<string>();

                foreach (var vendor in vendors)
                {
                    var vendorName = vendor.VENDORNAME;

                    try
                    {
                        msSqLcontext.Vendors.AddOrUpdate(
                            v => v.VendorName,
                            new Vendor()
                            {
                                VendorName = vendorName
                            });

                        msSqLcontext.SaveChanges();
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

                Console.WriteLine("\nAdded new Vendors from OracleBD into MS SQL Server:");
                vendorsToChange.ForEach(v => Console.WriteLine("Added vendor name: {0}", v.VENDORNAME));
            }
            else
            {
                Console.WriteLine("\nThere is no new records to import into VENDORS table!");
            }
        }         
    }    
}
