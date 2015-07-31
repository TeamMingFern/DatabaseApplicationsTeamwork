namespace SupermarketChain.ConsoleClient
{
    using System;
    using System.Globalization;
    using System.Threading;
    using Supermarket.Data;
    using Engines;
    using SQLiteDB;
    using Supermarket.ImportExel;
    using Supermarket.Reports;

    public class SupermarketMainProgram
    {                
        static void Main(string[] args)
        {
            Menu();
        }

        public static void Menu()
        {
            const string Menu = "\n                     Main Menu:\n" +
                  "                 ----------------\n\n" + 
                  " Select one: \n\n" + 
                  " 1. Replicate data from Oracle Database into MsSql Database\n" +
                  " 2. Load Zip Excel Reports to MsSql Database\n" +
                  " 3. Generate Pdf Sales report\n" +
                  " 4. Generate XML Sales by Vendor report\n" +
                  " 5. Generate JSON reports in given date range and load them into MongoDB\n" +
                  " 6. Load Xml Vendors Expenses Report into MsSql Database\n" +
                  " 7. Load data from MsSql to MySql\n" +
                  " 8. Generate financial report\n" +
                  " 9. Exit\n";

            const string Logo = @"
___________                        _____  .__                 ___________                  
\__    ___/___ _____    _____     /     \ |__| ____    ____   \_   _____/__________  ____  
  |    |_/ __ \\__  \  /     \   /  \ /  \|  |/    \  / ___\   |    __)/ __ \_  __ \/    \ 
  |    |\  ___/ / __ \|  Y Y  \ /    Y    \  |   |  \/ /_/  >  |     \\  ___/|  | \/   |  \
  |____| \___  >____  /__|_|  / \____|__  /__|___|  /\___  /   \___  / \___  >__|  |___|  /
             \/     \/      \/          \/        \//_____/        \/      \/           \/ 
";
            try
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                var isFinished = false;

                Console.WriteLine(Logo);

                while (!isFinished)
                {
                    Console.WriteLine(Menu);
                    var menuChoise = int.Parse(Console.ReadLine());
                    var context = new SupermarketContext();

                    switch (menuChoise)
                    {
                        case 1: 
                            OracleReplicationEngine.ReplicateOracleToMssql();
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
                            ImportJSONtoMongoDB.GenerateJsonReports();
                            break;
                        case 6: 
                            var xmlManager = new ImportFromXML(@"..\..\..\..\Import\Sample-Vendor-Expenses.xml", context);
                            xmlManager.TransferData();
                            break;
                        case 7: 
                            MsSqlReplicationEngine.ReplicateMssqlToMySql();
                            break;
                        case 8:
                            var data = SqLiteFactory.GetFinancialReportData();
                            FinancialReportGenerator.GenerateFinancialReport(data, @"..\..\..\..\Reports\");
                            break;
                        case 9: 
                            isFinished = true;
                            Console.WriteLine("\n\nSo Long!!!\n\n");
                            break;
                        default: 
                            throw new InvalidOperationException("Invalid operation.");
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
