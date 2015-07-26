using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MS_SQL_Server
{
    using Models;

    public class SupermarketSalesProduct
    {
        public int Id { get; set; }

        public int SupermarketId { get; set; }

        public int ProductId { get; set; }

        public virtual Supermarket Supermarket { get; set; }

        public virtual Product Product { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public DateTime SalesDate { get; set; }
    }
}
