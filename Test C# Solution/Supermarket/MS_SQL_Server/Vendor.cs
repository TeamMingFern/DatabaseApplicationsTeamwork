using System.Collections.Generic;

namespace MS_SQL_Server
{
    using Models;

    public class Vendor
    {
        private ICollection<Product> products;
        private ICollection<VendorExpense> expenses;
        public Vendor()
        {
            this.Products = new HashSet<Product>();
            this.expenses = new HashSet<VendorExpense>();
        }

        public int Id { get; set; }
        public string VendorName { get; set; }

        public virtual ICollection<Product> Products
        {
            get { return this.products; }
            set { this.products = value; }
        }

        public virtual ICollection<VendorExpense> Expenses
        {
            get { return this.expenses; }
            set { this.expenses = value; }
        }
    }
}