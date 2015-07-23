namespace SupermarketChain.ConsoleClient
{

    using System.Linq;
    using Oracle;
    using Supermarket.Data;
    using Supermarket.ImportExel;


    class Program
    {
        static void Main(string[] args)
        {
            

            var msSQLcontext = new SupermarketContext();
            msSQLcontext.Vendors.FirstOrDefault();

            var importExelFiles = new ImportExel();
            importExelFiles.LoadExelReports(msSQLcontext);

            //return;

            var msSqLcontext = new SupermarketContext();
            var oracleContext = new OracleEntities();


            OracleDBReplication.UpdateVendorsFromOracle(oracleContext, msSqLcontext);

            OracleDBReplication.UpdateMeasuresFromOracle(oracleContext, msSqLcontext);

            OracleDBReplication.UpdateProductsTypesFromOracle(oracleContext, msSqLcontext);

            OracleDBReplication.UpdateProductsFromOracle(oracleContext, msSqLcontext);





        }
    }
}
