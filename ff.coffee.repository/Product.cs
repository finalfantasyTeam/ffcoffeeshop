//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ff.coffee.repository
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        public Product()
        {
            this.Stores = new HashSet<Store>();
            this.ImportDetails = new HashSet<ImportDetail>();
            this.Orderings = new HashSet<Ordering>();
            this.InvoiceDetails = new HashSet<InvoiceDetail>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public int Category { get; set; }
        public string Unit { get; set; }
        public Nullable<decimal> Price { get; set; }
        public System.DateTime DateCreate { get; set; }
        public bool Enable { get; set; }
    
        public virtual ICollection<Store> Stores { get; set; }
        public virtual ProductCat ProductCat { get; set; }
        public virtual ICollection<ImportDetail> ImportDetails { get; set; }
        public virtual ICollection<Ordering> Orderings { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}