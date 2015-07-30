//namespace SupermarketChain.ConsoleClient.Engines
//{
//    using System;
//    using System.IO;
    

//    using Supermarket.Data;

//    public class ImportJSONtoMongo
//    {
//        public const string MongoDbHost = "mongodb://localhost:27017";
//        public const string MongoDbName = "supermarket";

//        public const string JsonReportPath =
//            @"C:\Users\user\Desktop\db.Apps\DatabaseApplicationsTeamwork\Test C# Solution\Supermarket\MongoDB\JSONReports\";


//        private static IMongoCollection<BsonDocument> GetDatabaseCollection()
//        {
//            var client = new MongoClient(MongoDbHost);
//            var database = client.GetDatabase(MongoDbName);
//            var collection = database.GetCollection<BsonDocument>("SalesByProductReports");
//            return collection;
//        }

//        private static void SaveDataInMongoDb(string jsonReport)
//        {
//            var reader = new JsonReader(jsonReport);
//            var context = BsonDeserializationContext.CreateRoot(reader);
//            BsonDocument doc = BsonDocumentSerializer.Instance.Deserialize(context);
//            GetDatabaseCollection().InsertOneAsync(doc).Wait();
//        }

//        private static void GenerateJSONReports()
//        {
//            Console.WriteLine("Enter start date in format \'YYYY-MM-DD\': ");
//            string startDateInput = Console.ReadLine();
//            Console.WriteLine("Enter end date in format \'YYYY-MM-DD\': ");
//            string endDateInput = Console.ReadLine();

//            try
//            {
//                DateTime startDate = DateTime.Parse(startDateInput);
//                DateTime endDate = DateTime.Parse(endDateInput);
//                if (startDate > endDate)
//                {
//                    throw new ArgumentException("End date must be after start date!");
//                }

//                var mssqlData = new SupermarketContext();
//                var jsSerializaer = new JavaScriptSerializer();
//                var sales =
//                    mssqlData.Sales.Where(s => s.SoldDate >= startDate && s.SoldDate <= endDate)
//                        .Select(
//                            s =>
//                            new
//                            {
//                                productId = s.ProductId,
//                                productName = s.Product.ProductName,
//                                vendorName = s.Product.Vendor.VendorName,
//                                totalQuantitySold = s.Quantity,
//                                totalIncomes = s.Quantity * s.Product.Price
//                            })
//                        .ToList();

//                foreach (var sale in sales)
//                {
//                    var jsonReport = jsSerializaer.Serialize(sale);

//                    SaveDataInMongoDb(jsonReport);

//                    if (!File.Exists(JsonReportPath + sale.productId + ".json"))
//                    {
//                        File.WriteAllText(JsonReportPath + sale.productId + ".json", jsonReport);
//                    }
//                }
//            }
//            catch (Exception)
//            {
//                Console.WriteLine("Invalid input");
//            }
//        } 
//    }
//}