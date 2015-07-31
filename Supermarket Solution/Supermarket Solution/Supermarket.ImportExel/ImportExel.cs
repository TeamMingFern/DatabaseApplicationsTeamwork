using System;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using MS_SQL_Server;
using Supermarket.Data;

namespace Supermarket.ImportExel
{
    using MS_SQL_Server.Models;

    /// <summary>
    /// Import zipeed Excel files with extension ".xls" into MS SQL Database. The Excel files are salesreports and  should include the name of the vendor, the name of the products, the price of the products, the quantity of the products and the date of the report.
    /// </summary>
    public class ImportExel
    {
        private const string TempFileName = @"salesReports.xls";
        private const string TempFolderName = @"\extracted\";
        private const string ExcelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source = {0}; Extended Properties=\"Excel 12.0;HDR=YES\"";

        /// <summary>
        /// Initialize the filepath to the zipped file or use the defaul file path.
        /// </summary>
        /// <param name="filepath">string - The path to the file</param>
        public ImportExel(string filepath = null)
        {
            if (filepath != null)
            {
                this.FilePath = filepath;
            }
            else
            {
                this.FilePath = @"..\..\..\..\Import\Sample-Sales-Reports.zip";
            }
        }

        public string FilePath { get; private set; }

        /// <summary>
        /// Load the zipped files with the excel reports.
        /// </summary>
        /// <param name="context">The context relationship with the Entity framework and the database</param>
        public void LoadExelReports(SupermarketContext context)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(this.FilePath);
            Console.WriteLine(@"        Loading Exel Reports To MsSqlDB
------------------------------------------------");
            Console.WriteLine("Extracting data from file  {0}", directoryInfo.FullName);

            string tempFolder = string.Format("{0}{1}", Directory.GetCurrentDirectory(), TempFolderName);
            string currentReportDate = string.Empty;

            using (ZipArchive archive = ZipFile.OpenRead(this.FilePath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (entry.FullName.EndsWith("/", StringComparison.OrdinalIgnoreCase))
                    {
                        //Gets from the folders the date in which the reports are made
                        currentReportDate = entry.FullName.TrimEnd('/');
                    }
                    else
                    {
                        if (!Directory.Exists(tempFolder))
                        {
                            Directory.CreateDirectory(tempFolder);
                        }
                        //extract the readed file always into this "temporary" file
                        entry.ExtractToFile(Path.Combine(tempFolder, TempFileName), true);
                        DataTable excelData = this.ReadExcelData(string.Format("{0}{1}", tempFolder, TempFileName));
                        DataRowCollection arrExelData = excelData.Rows;
                        string marketName = this.SupermarketName(arrExelData[0].ItemArray);

                        this.CheckForExistingSupermarket(arrExelData[0].ItemArray, context);

                        //The loop starts from 2, because of the cell formatting in the excel file, if the loop starts form 0 it will iterate trough array full of empty strings
                        for (int i = 2; i < arrExelData.Count - 1; i++)
                        {
                            InsertIntoDataBase(arrExelData[i].ItemArray, context, currentReportDate, marketName);
                        }
                    }
                }
            }

            Console.WriteLine("Zip excel reports imported.");

        }
        /// <summary>
        /// Insert the content of the excel files into the database.
        /// </summary>
        /// <param name="rowData">array of data containing name, type, price and quantity of the product</param>
        /// <param name="context">the Entity Framework connection to the database</param>
        /// <param name="reportDate">the date taken from the folder with the reports</param>
        /// <param name="supmarketName">the name of the supermarket</param>
        private void InsertIntoDataBase(object[] rowData, SupermarketContext context, string reportDate, string supmarketName)
        {
            //    string[] inputNameType = rowData[0].ToString().Split();
            //    string prodType = inputNameType[0];
            //    string prodName = inputNameType[1];
            string prodName = rowData[0].ToString();
            int quantity = int.Parse(rowData[1].ToString());
            float price = float.Parse(rowData[2].ToString());
            DateTime dateReport = DateTime.ParseExact(reportDate, "dd-MMM-yyyy", CultureInfo.InvariantCulture);

            //this.CheckProductType(prodType, context);
            this.CheckProduct(prodName, price, context);

            var productId = context.Products.Where(p => p.ProductName == prodName).Select(p => p.Id).FirstOrDefault();
            var marketId = context.Supermarkets.Where(s => s.Name == supmarketName).Select(s => s.SupermarketId).FirstOrDefault();
            var existMarketSalesProduct = context.SupermarketSalesProducts
                .Where(s => s.SupermarketId == marketId &&
                            s.ProductId == productId &&
                            s.SalesDate == dateReport)
                .Select(s => new
                {
                    s.ProductId,
                    s.SupermarketId,
                    s.SalesDate
                }).FirstOrDefault();

            if (existMarketSalesProduct == null)
            {
                context.SupermarketSalesProducts.Add(new SupermarketSalesProduct
                {
                    SupermarketId = marketId,
                    ProductId = productId,
                    Quantity = quantity,
                    Price = (decimal)price,
                    SalesDate = dateReport
                });
                context.SaveChanges();
            }
        }

        /// <summary>
        ///Check for existing supermarket, because we want to have distinct data in the database and if the supermarket do not exists the method add it to the database. 
        /// </summary>
        /// <param name="supName">the array containig the supermarket name</param>
        /// <param name="context">the Entity Framework connection to the database</param>
        private void CheckForExistingSupermarket(object[] supName, SupermarketContext context)
        {
            string marketName = this.SupermarketName(supName);
            var existSupName = context.Supermarkets.Where(s => s.Name == marketName).Select(s => s.Name).FirstOrDefault();

            if (existSupName == null)
            {
                context.Supermarkets.Add(new MS_SQL_Server.Supermarket
                {
                    Name = marketName,
                    IsDeleted = false
                });
                context.SaveChanges();
                Console.WriteLine("Supermarket: {0} added!", marketName);
            }
            else
            {
                Console.WriteLine("Supermarket: {0} existed!", marketName);
            }
        }

        /// <summary>
        /// Check for existing product type, because we want to have distinct data in the database and if the product type do not exists the method add it to the database. 
        /// </summary>
        /// <param name="prodTypeName">the type to be checked</param>
        /// <param name="context">the Entity Framework connection to the database</param>
        private void CheckProductType(string prodTypeName, SupermarketContext context)
        {
            var existPodType = context.ProductTypes
                .Where(t => t.TypeName == prodTypeName)
                .Select(t => t.TypeName)
                .FirstOrDefault();

            if (existPodType == null)
            {
                context.ProductTypes.Add(new ProductType
                {
                    TypeName = prodTypeName
                });
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Check for existing product type, because we want to have distinct data in the database and if the product do not exists the method add it to the database. 
        /// </summary>
        /// <param name="productName">the name to be checked</param>
        /// <param name="price">the price needed to be eventually created new product</param>
        /// <param name="context">the Entity Framework connection to the database</param>
        private void CheckProduct(string productName, float price, SupermarketContext context)
        {
            var existProd = context.Products
             .Where(p => p.ProductName == productName)
             .Select(p => p.ProductName)
             .FirstOrDefault();

            if (existProd == null)
            {
                context.Products.Add(new Product
                {
                    ProductName = productName,
                    Price = price
                });
                context.SaveChanges();
                Console.WriteLine("Product: {0} added!", productName);
            }
            else
            {
                Console.WriteLine("Product: {0} existed!", productName);
            }
        }

        /// <summary>
        /// Extracts the name of the supermarket.
        /// </summary>
        /// <param name="supName">array containig the name of the supermarket</param>
        /// <returns>the name of the supermarket as string</returns>
        private string SupermarketName(object[] supName)
        {
            string inputData = supName[0].ToString();
            Regex reg = new Regex(@"“\w+\s+(\W+)?\w+”");
            Match match = reg.Match(inputData);
            string marketName = match.Value;

            return marketName;
        }

        /// <summary>
        /// Creates temporary database filled with the excel data.
        /// </summary>
        /// <param name="filePath">the path where is the excel file</param>
        /// <returns>tabel with the data from the excel file</returns>
        private DataTable ReadExcelData(string filePath)
        {
            OleDbConnection excelConnection = new OleDbConnection(string.Format(ExcelConnectionString, filePath));
            DataTable dt = new DataTable();

            excelConnection.Open();

            OleDbDataAdapter da = new OleDbDataAdapter("select * from [Sales$]", excelConnection);

            da.Fill(dt);
            excelConnection.Close();

            return dt;
        }
    }
}

