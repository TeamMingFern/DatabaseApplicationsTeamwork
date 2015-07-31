namespace Supermarket.Data
{
    using System.Linq;

    public static class MsSqlManager
    {
        public static int? GetSupermarketIdByName(string supermarketName)

        {
            var context = new SupermarketContext();
            var supermarketId = context.Supermarkets.FirstOrDefault(s => s.Name == supermarketName).SupermarketId;

            return supermarketId;
        }

        public static int? GetProductIdByName(string productName)
        {
            var context = new SupermarketContext();
            var productId = context.Products.FirstOrDefault(p => p.ProductName == productName).Id;

            return productId;
        }

        public static int? GetVendorIdByName(string vendorName)
        {
            var context = new SupermarketContext();
            var vendorId = context.Vendors.FirstOrDefault(v => v.VendorName == vendorName).Id;

            return vendorId;
        }

        public static int? GetMeasureIdByName(string measureName)
        {
            var context = new SupermarketContext();
            var measureId = context.Measures.FirstOrDefault(m => m.MeasureName == measureName).Id;

            return measureId;
        }

        public static int? GetTypeIdByName(string typeName)
        {
            var context = new SupermarketContext();
            var typeId = context.ProductTypes.FirstOrDefault(pt => pt.TypeName == typeName).Id;

            return typeId;
        }
    }
}