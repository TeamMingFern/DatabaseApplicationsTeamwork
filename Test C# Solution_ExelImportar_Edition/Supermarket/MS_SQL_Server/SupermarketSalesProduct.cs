using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MS_SQL_Server.Models
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
        public DateTime SalesDate { get; set; }        
    }
}
