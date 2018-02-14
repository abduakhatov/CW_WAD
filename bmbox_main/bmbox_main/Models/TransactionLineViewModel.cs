using Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bmbox_main.Models
{
    public class TransactionLineViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Res))]
        public string Name { get; set; }

        [Display(Name = "Brand", ResourceType = typeof(Res))]
        public string Brand { get; set; }

        [Display(Name = "ProductType", ResourceType = typeof(Res))]
        public string Type { get; set; }

        [Display(Name = "Price", ResourceType = typeof(Res))]
        public decimal Cost { get; set; }

        [Display(Name = "LeftOnStock", ResourceType = typeof(Res))]
        public Nullable<short> Quantity { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int TransactionId { get; set; }
        [Display(Name = "Total", ResourceType = typeof(Res))]
        public decimal total { get; set; }
    }
}