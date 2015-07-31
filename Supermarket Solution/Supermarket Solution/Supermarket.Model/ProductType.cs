namespace MS_SQL_Server.Models
{
    using System.Collections.Generic;

    public class ProductType
    {
        private ICollection<Product> products;

        public ProductType()
        {
            this.products = new HashSet<Product>();
        }

        public int Id { get; set; }

        public string TypeName { get; set; }

        public virtual ICollection<Product> Products
        {
            get { return this.products; }
            set { this.products = value; }
        }
    }
}