using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
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
        [DisplayName("Price")]
        public decimal Cost { get; set; }
        [DisplayName("Left on Stock")]
        public short QuantityLeft { get; set; }
    }
}