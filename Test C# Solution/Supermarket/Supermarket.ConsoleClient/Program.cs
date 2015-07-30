using System.Linq;

namespace SupermarketChain.ConsoleClient
{
    using System;
    using System.Activities.Statements;
    using System.Globalization;
    using System.Threading;
    using Supermarket.Data;
    using Engines;
    using Supermarket.ImportExel;
    using Supermarket.Reports;


    public class Program
    {
       
            
        static void Main(string[] args)
        {

            Menu();




        }




        public static void Menu()
        {
            const string Menu = "\nMenu:\n" +
                  "1. Replicate data from Oracle Database into MsSql Database\n" +
                  "2. Load Zip Excel Reports to MsSql Database\n" +
                  "3. Generate Pdf Sales report\n" +
                  "4. Generate XML Sales by Vendor from given date range\n" +
                  "5. Generate JSON reports in given date range and load them into MongoDB\n" +
                  "6. Load Xml Vendors Expenses Report into MsSql Database\n" +
                  "7. Load data from MsSql to MySql\n" +
                  "8. Generate financial report\n" +
                  "9. Exit\n";

            try
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                var isFinished = false;

                while (!isFinished)
                {
                    Console.WriteLine(Menu);
                    var menuChoise = int.Parse(Console.ReadLine());
                    var context = new SupermarketContext();

                    switch (menuChoise)
                    {
                        case 1: OracleReplicationEngine.ReplicateOracleToMssql();
                            break;
                        case 2:
                            var importExelFiles = new ImportExel();
                            importExelFiles.LoadExelReports(context);
                            break;
                        case 3:
                            PDFReportGenerator.GeneratePDFReport();
                            break;
                        case 4:
                            XMLGenerator.GenerateXMLReport();
                            break;
                        case 5:
                            ImportJSONtoMongoDB.GenerateJSONReports();
                            break;
                        case 6: 
                            Console.WriteLine(@"        Importing XML To MsSqlDB
------------------------------------------------");
                            Console.WriteLine("Extracting data...");
                            var xmlManager = new ImportFromXML(@"..\..\..\..\Import\Sample-Vendor-Expenses.xml", context);

                            Console.WriteLine("Sending data...");
                            xmlManager.TransferData();

                            Console.WriteLine("Sales report imported.");
                            break;
                        case 7: MsSqlReplicationEngine.ReplicateMssqlToMySql();
                            break;
                        //case 8: Engine.GenerateFinancialReport();
                            break;
                        case 9: isFinished = true;
                            break;
                        default: throw new InvalidOperationException("Invalid operation.");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
