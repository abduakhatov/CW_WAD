//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bmbox.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        public Product()
        {
            this.TransactionLines = new HashSet<TransactionLine>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
        public byte[] Image { get; set; }
        public decimal Cost { get; set; }
        public short QuantityLeft { get; set; }
    
        public virtual ICollection<TransactionLine> TransactionLines { get; set; }
    }
}
