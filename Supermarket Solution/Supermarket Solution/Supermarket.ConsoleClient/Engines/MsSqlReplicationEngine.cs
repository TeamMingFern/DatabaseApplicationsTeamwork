namespace SupermarketChain.ConsoleClient.Engines
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using MySqlModel;
    using Supermarket.Data;

    public static class MsSqlReplicationEngine
    {

        public static void ReplicateMssqlToMySql()
        {

            Console.WriteLine(@"        Replicating MsSqlDB To MySql
------------------------------------------------");

            UpdateMeasuresFromMssql();
            UpdateVendorsFromMssql();
            UpdateProductTypesFromMssql();
            UpdateSupermarketsFromMssql();
            UpdateProductsFromMssql();
            UpdateSalesFromMssql();
            UpdateVendorsExpenseFromMssql();

            Console.WriteLine("Successful replication.");
        }
               
        ///<summary>
        ///This is summary for methods that transfer records from MQ SQL Server model into MySQL DB.
        ///The method first select all records from MQ SQL Server model model.
        ///Second it inserts record into MySQL DB.
        ///</summary>

        
        public static void UpdateMeasuresFromMssql()
        {
            var msSqLcontext = new SupermarketContext();
            var mySqlContext = new marketsystemEntities();
            var measures = msSqLcontext.Measures
                 .ToList();

            foreach (var measure in measures)
            {
                var id = measure.Id;
                var name = measure.MeasureName;

                mySqlContext.measures.AddOrUpdate(
                    m => m.measureId,
                    new MySqlModel.measure()
                    {
                       measureId = id,
                       measureName = name
                    });

                mySqlContext.SaveChanges();

            }

        }

        public static void UpdateVendorsFromMssql()
        {
            var msSqLcontext = new SupermarketContext();
            var mySqlContext = new marketsystemEntities();
            var vendors = msSqLcontext.Vendors
                .ToList();
         
            foreach (var vendor in vendors)
            {
                var id = vendor.Id;
                var name = vendor.VendorName;

                mySqlContext.vendors.AddOrUpdate(
                    v => v.vendorId,
                    new MySqlModel.vendor()
                    {
                        vendorId = id,
                        vendorName = name 
                    });
                          
                mySqlContext.SaveChanges();             
            }
        }

        public static void UpdateProductTypesFromMssql()
        {
            var msSqLcontext = new SupermarketContext();
            var mySqlContext = new marketsystemEntities();
            var productTypes = msSqLcontext.ProductTypes
                .ToList();
         
            foreach (var productType in productTypes)
            {
                var id = productType.Id;
                var name = productType.TypeName;

                mySqlContext.producttypes.AddOrUpdate(
                    pt => pt.typeId,
                    new producttype()
                    {
                        typeId = id,
                        typeName = name 
                    });
                          
                mySqlContext.SaveChanges();
            }
        }

        public static void UpdateProductsFromMssql()
        {
            var msSqLcontext = new SupermarketContext();
            var mySqlContext = new marketsystemEntities();
            var products = msSqLcontext.Products
                .ToList();
         
            foreach (var product in products)
            {
                var productId = product.Id;
                var productName = product.ProductName;
                var vendorId = product.VendorId;
                var measureId = product.MeasureId;
                var typeId = product.ProductTypeId;
                var price = product.Price;

                try
                {
                    mySqlContext.products.AddOrUpdate(
                        p => p.productId,
                        new product()
                        {
                            productId = productId,
                            productName = productName,
                            vendorId = (int) vendorId,
                            measureId = (int)measureId,
                            productTypeId = (int)typeId,
                            productPrice = (decimal) price                      
                        });
                          
                    mySqlContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
        }


        public static void UpdateSupermarketsFromMssql()
        {
            var msSqLcontext = new SupermarketContext();
            var mySqlContext = new marketsystemEntities();
            var supermarkets = msSqLcontext.Supermarkets
                .ToList();

            foreach (var supermarket in supermarkets)
            {
                var id = supermarket.SupermarketId;
                var name = supermarket.Name;

                mySqlContext.supermarkets.AddOrUpdate(
                    s => s.supermarketId,
                    new supermarket()
                    {
                        supermarketId = id,
                        supermarketName = name
                    });

                mySqlContext.SaveChanges();              
            }
        }

        public static void UpdateSalesFromMssql()
        {
            var msSqLcontext = new SupermarketContext();
            var mySqlContext = new marketsystemEntities();
            var sales = msSqLcontext.SupermarketSalesProducts
                .ToList();

            foreach (var sale in sales)
            {
                var id = sale.Id;
                var supermarketId = sale.SupermarketId;
                var productId = sale.ProductId;
                var saleDate = sale.SalesDate;
                var quantity = sale.Quantity;
                var price = sale.Price;
               
                mySqlContext.sales.AddOrUpdate(
                    s => new
                    {
                        s.supermarketId,
                        s.productId,
                        s.saledOn
                    },
                    new sale()
                    {
                        id = id,
                        supermarketId = supermarketId,
                        productId = productId,
                        saledOn = saleDate,
                        quantity = quantity,
                        price = (decimal) price
                    });

                mySqlContext.SaveChanges();              
            }
        }

        public static void UpdateVendorsExpenseFromMssql()
        {
            var msSqLcontext = new SupermarketContext();
            var mySqlContext = new marketsystemEntities();
            var vendorsExpense = msSqLcontext.VendorExpenses
                .ToList();

            foreach (var expense in vendorsExpense)
            {
                var vendorId = expense.VendorId;
                var date = expense.Date;
                var total = expense.Total;

                mySqlContext.vendor_expenses.AddOrUpdate(
                    e => new { e.vendorId, e.expenseDate },
                    new vendor_expenses()
                    {
                        vendorId = vendorId,
                        expenseDate = date,
                        total = total
                    });

                mySqlContext.SaveChanges();
            }
        }

    }

}