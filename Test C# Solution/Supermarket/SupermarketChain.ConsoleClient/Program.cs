using System;
using System.Linq;
using System.Transactions;

namespace SupermarketChain.ConsoleClient
{
    using Supermarket.Data;
    using Engines;
    using Supermarket.ImportExel;
    using Supermarket.Reports;


    class Program
    {
        static void Main(string[] args)
        {
            var context = new SupermarketContext();

            Console.WriteLine("Extracting data...");
            var xmlManager = new ImportFromXML("Sample-Vendor-Expenses.xml", context);

            using (TransactionScope tran = new TransactionScope())
            {
                Console.WriteLine("Sending data...");
                xmlManager.TransferData();
                tran.Complete();
            }

            Console.WriteLine("Sales reports imported.");


            //OracleReplicationEngine.ReplicateOracleToMssql();
            //MsSqlReplicationEngine.ReplicateMssqlToMySql();


            //var importExelFiles = new ImportExel();
            //importExelFiles.LoadExelReports(context);
            //XMLGenerator.generateXMLReport();
            //PDFReportGenerator.generatePDFReport();

            //ImportFromXml.ImportXML();

            
            //var test = context.measures.First().measureName;
            //Console.WriteLine(test);


            //PDFReportGenerator.generatePDFReport();




            //return;

            //var msSqLcontext = new SupermarketContext();
            //var oracleContext = new OracleEntities();


            //OracleDBReplication.UpdateVendorsFromOracle(oracleContext, msSqLcontext);

            //OracleDBReplication.UpdateMeasuresFromOracle(oracleContext, msSqLcontext);

            //OracleDBReplication.UpdateProductsTypesFromOracle(oracleContext, msSqLcontext);

            //OracleDBReplication.UpdateProductsFromOracle(oracleContext, msSqLcontext);


        }
    }
}
