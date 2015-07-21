using System.Collections.Generic;

namespace MS_SQL_Server
{
    using Models;

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