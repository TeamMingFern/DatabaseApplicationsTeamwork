namespace MS_SQL_Server
{
    using Models;
    using System.Collections.Generic;

    public class Vendor
    {
        private ICollection<Product> products;

        public Vendor()
        {
            this.Products = new HashSet<Product>();
        }

        public int Id { get; set; }

        public string VendorName { get; set; }

        public virtual ICollection<Product> Products
        {
            get { return this.products; }
            set { this.products = value; }
        }
    }
}