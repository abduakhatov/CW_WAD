using bmbox_main.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace bmbox_main.Models
{
    public class TransactionViewModel
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        [DisplayName("Added Date")]
        public string Date { get; set; }
        public TransactionStatusEnum Status { get; set; }
    }
}