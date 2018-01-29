using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bmbox_main.Models
{
    public class TransactionLineViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
        [DisplayName("Price")]
        public decimal Cost { get; set; }

        public Nullable<short> Quantity { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int TransactionId { get; set; }
    }
}