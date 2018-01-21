using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bmbox_main.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
        public byte[] Image { get; set; }
        public decimal Cost { get; set; }
        public short QuantityLeft { get; set; }
    }
}