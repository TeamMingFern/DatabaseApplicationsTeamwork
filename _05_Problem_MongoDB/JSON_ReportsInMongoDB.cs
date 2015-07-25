namespace MongoDB
{
    using Bson;
    using Bson.Serialization;
    using Driver;

    public static class JSON_ReportsInMongoDB
    {
        private const string connectionString = "mongodb://localhost:27017/supermarket";
        private static readonly MongoClient client = new MongoClient(connectionString);
        private static readonly IMongoDatabase database = client.GetDatabase("supermarket");

        private static readonly IMongoCollection<BsonDocument> collection =
            database.GetCollection<BsonDocument>("SalesByProductReports");

        public static async void ImportSalesByProductReport(string jsonReport)
        {
            await collection.InsertOneAsync(BsonSerializer.Deserialize<BsonDocument>(jsonReport));
        }

        public static void Main()
        {
            
        }
    }
}
