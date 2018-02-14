using bmbox_main.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Resources;

namespace bmbox_main.Models
{
    public class TransactionViewModel
    {
        public int Id { get; set; }

        [Display(Name = "UserEmail", ResourceType = typeof(Res))]
        public string UserEmail { get; set; }

        [Display(Name = "AddedDate", ResourceType = typeof(Res))]
        public string Date { get; set; }

        [Display(Name = "Status", ResourceType = typeof(Res))]
        public TransactionStatusEnum Status { get; set; }
    }
}