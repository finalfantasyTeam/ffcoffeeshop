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
    
    public partial class InvoiceDetail
    {
        public int ID { get; set; }
        public int Invoice { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
        public string UserOrder { get; set; }
        public string UserKitchenConfirm { get; set; }
        public string UserKitchenDone { get; set; }
        public Nullable<System.DateTime> TimeConfirm { get; set; }
        public Nullable<System.DateTime> TimeDone { get; set; }
        public Nullable<bool> KitchenConfirm { get; set; }
        public string KitchenDone { get; set; }
    
        public virtual Invoice Invoice1 { get; set; }
        public virtual Product Product { get; set; }
    }
}