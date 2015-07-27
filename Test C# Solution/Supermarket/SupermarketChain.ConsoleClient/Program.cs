using System;
using System.Linq;

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
            context.Vendors.FirstOrDefault();

            //OracleReplicationEngine.ReplicateOracleToMssql();
            //MsSqlReplicationEngine.ReplicateMssqlToMySql();


            //var importExelFiles = new ImportExel();
            //importExelFiles.LoadExelReports(context);
            //XMLGenerator.generateXMLReport();
            //PDFReportGenerator.generatePDFReport();

            //ImportFromXml.ImportXML();

            ImportFromXML.ImportVendorExpenses(context);

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
