using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MS_SQL_Server
{
    public class SupermarketSalesProduct
    {
        [Key,Column(Order = 0)]
        public int SupermarketId { get; set; }
        [Key, Column(Order = 1)]
        public int ProductId { get; set; }
        public virtual Supermarket Supermarket { get; set; }
        public virtual Product Product { get; set; }

        public int Quantity { get; set; }
        public double Price { get; set; }
        [Key, Column(Order = 2)]
        public DateTime SalesDate { get; set; }        
    }
}
