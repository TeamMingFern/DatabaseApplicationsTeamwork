namespace MS_SQL_Server.Models
{
    using Microsoft.Build.Framework;

    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; }
        public float Price { get; set; }

        public System.Nullable<int> MeasureId { get; set; }
        public virtual Measure Measure { get; set; }

        public System.Nullable<int> VendorId { get; set; }
        public virtual Vendor Vendor { get; set; }

        public System.Nullable<int> ProductTypeId { get; set; }
        public virtual ProductType ProductType { get; set; }
    }
}