//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace bmbox.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class TransactionLine
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Nullable<short> Quantity { get; set; }
        public int TransactionId { get; set; }
    
        public virtual Product Product { get; set; }
    }
}
