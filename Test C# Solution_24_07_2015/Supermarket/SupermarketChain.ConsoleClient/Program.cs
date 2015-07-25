namespace SupermarketChain.ConsoleClient
{

    using System.Linq;
    using Oracle;
    using Supermarket.Data;
    using Supermarket.ImportExel;
    using System.Data.Entity;
    using Supermarket.Reports;

    class Program
    {
        static void Main(string[] args)
        {


            var msSQLcontext = new SupermarketContext();
            msSQLcontext.Vendors.FirstOrDefault();

            PDFReportGenerator.generatePDFReport();

           


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
