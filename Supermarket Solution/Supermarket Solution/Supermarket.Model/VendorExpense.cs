namespace MS_SQL_Server
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class VendorExpense
    {
        [Key]
        public int Id { get; set; }

        public int VendorId { get; set; }

        public virtual Vendor Vendors { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public decimal Total { get; set; }
    }
}