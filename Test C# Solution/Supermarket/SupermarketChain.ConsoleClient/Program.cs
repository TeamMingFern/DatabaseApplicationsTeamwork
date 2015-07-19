namespace SupermarketChain.ConsoleClient
{
    using Oracle;
    using Supermarket.Data;


    class Program
    {
        static void Main(string[] args)
        {
            var msSqLcontext = new SupermarketContext();
            var oracleContext = new OracleEntities();


            OracleDBReplication.UpdateVendorsFromOracle(oracleContext, msSqLcontext);

            OracleDBReplication.UpdateMeasuresFromOracle(oracleContext, msSqLcontext);

            OracleDBReplication.UpdateProductsTypesFromOracle(oracleContext, msSqLcontext);

            OracleDBReplication.UpdateProductsFromOracle(oracleContext, msSqLcontext);


        }
    }
}
