namespace Supermarket.Reports
{
    using System;
    using System.IO;
    using System.Web.Script.Serialization;
    using Data;
    using System.Linq;
    using MongoDB.Bson;
    using MongoDB.Bson.IO;
    using MongoDB.Bson.Serialization;
    using MongoDB.Bson.Serialization.Serializers;
    using MongoDB.Driver;

    public class ImportJSONtoMongoDB
    {
        public const string MongoDbHost = "mongodb://localhost:27017";
        public const string MongoDbName = "supermarket";
        public const string JsonReportPath =
            @"..\..\..\..\Reports\";

        private static IMongoCollection<BsonDocument> GetDatabaseCollection()
        {
            var client = new MongoClient(MongoDbHost);
            var database = client.GetDatabase(MongoDbName);
            var collection = database.GetCollection<BsonDocument>("SalesByProductReports");

            return collection;
        }

        private static void SaveDataInMongoDb(string jsonReport)
        {
            var reader = new JsonReader(jsonReport);
            var context = BsonDeserializationContext.CreateRoot(reader);

            BsonDocument doc = BsonDocumentSerializer.Instance.Deserialize(context);
            GetDatabaseCollection().InsertOneAsync(doc).Wait();
        }

        public static void GenerateJsonReports()
        {
            Console.WriteLine("Enter start date in format \'YYYY-MM-DD\': ");
            string startDateInput = Console.ReadLine();

            Console.WriteLine("Enter end date in format \'YYYY-MM-DD\': ");
            string endDateInput = Console.ReadLine();

            Console.WriteLine("Processing data.......");

            try
            {
                DateTime startDate = DateTime.Parse(startDateInput);
                DateTime endDate = DateTime.Parse(endDateInput);

                if (startDate > endDate)
                {
                    throw new ArgumentException("End date must be after start date!");
                }

                var mssqlData = new SupermarketContext();
                var jsSerializaer = new JavaScriptSerializer();
                var sales =
                    mssqlData.SupermarketSalesProducts.Where(s => s.SalesDate >= startDate && s.SalesDate <= endDate)
                        .Select(
                            s => new
                                {
                                    productId = s.ProductId,
                                    productName = s.Product.ProductName,
                                    vendorName = s.Product.Vendor.VendorName,
                                    totalQuantitySold = s.Quantity,
                                    totalIncomes = s.Quantity * s.Product.Price
                                }).ToList();

                Console.WriteLine("Sending data to MongoDB.....");

                foreach (var sale in sales)
                {
                    var jsonReport = jsSerializaer.Serialize(sale);

                    SaveDataInMongoDb(jsonReport);

                    if (!File.Exists(JsonReportPath + sale.productId + ".json"))
                    {
                        File.WriteAllText(JsonReportPath + sale.productId + ".json", jsonReport);
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid input");
                return;
            }

            Console.WriteLine("JSON reports were generated.");
            DirectoryInfo directoryInfo = new DirectoryInfo(JsonReportPath);
            Console.WriteLine("Directory:  {0}", directoryInfo.FullName);

            foreach (var file in directoryInfo.GetFiles())
            {
                Console.WriteLine("File:  {0}", file.Name);
            }

            Console.WriteLine("JSON reports were imported in MongoDB successfully.");
        }
    }  
}