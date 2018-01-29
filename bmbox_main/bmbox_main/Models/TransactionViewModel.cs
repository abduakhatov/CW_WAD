using bmbox_main.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bmbox_main.Models
{
    public class TransactionViewModel
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string ShippingAdress { get; set; }
        public TransactionStatusEnum Status { get; set; }
    }
}