using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Resources;

namespace bmbox_main.Models
{
    public class ProductViewModel
    {
        
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Name", ResourceType = typeof(Res))]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Brand", ResourceType = typeof(Res))]
        public string Brand { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "ProductType", ResourceType = typeof(Res))]
        public string Type { get; set; }

        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif)$", ErrorMessage = "Only Image files allowed.")]
        [Display(Name = "Image", ResourceType = typeof(Res))]
        public byte[] Image { get; set; }

        [Display(Name = "Price", ResourceType = typeof(Res))]
        [Required]
        [DataType(DataType.Currency)]
        public short Cost { get; set; }

        [Display(Name = "LeftOnStock", ResourceType = typeof(Res))]
        [Required]
        [RegularExpression("([0-9]+)", ErrorMessage = "Enter only numeric number")]
        public short QuantityLeft { get; set; }
    }
}