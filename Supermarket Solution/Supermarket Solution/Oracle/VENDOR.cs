//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Oracle
{
    using System;
    using System.Collections.Generic;
    
    public partial class VENDOR
    {
        public VENDOR()
        {
            this.PRODUCTS = new HashSet<PRODUCT>();
        }
    
        public decimal VENDOR_ID { get; set; }
        public string VENDORNAME { get; set; }
        public bool ISCOPIED { get; set; }
        public bool ISDELETED { get; set; }
    
        public virtual ICollection<PRODUCT> PRODUCTS { get; set; }
    }
}
