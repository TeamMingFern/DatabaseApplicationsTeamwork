namespace MS_SQL_Server.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public float Price { get; set; }

        public int MeasureId { get; set; }
        public virtual Measure Measure { get; set; }

        public int VendorId { get; set; }
        public virtual Vendor Vendor { get; set; }

        public int ProductTypeId { get; set; }
        public virtual ProductType ProductType { get; set; }
    }
}