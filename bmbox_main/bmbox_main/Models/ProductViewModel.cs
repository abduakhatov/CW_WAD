using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace bmbox_main.Models
{
    public class ProductViewModel
    {
        
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Brand { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Type { get; set; }

        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif)$", ErrorMessage = "Only Image files allowed.")]
        public byte[] Image { get; set; }

        [DisplayName("Price")]
        [Required]
        [DataType(DataType.Currency)]
        public decimal Cost { get; set; }

        [DisplayName("Left on Stock")]
        [Required]
        [RegularExpression("([0-9]+)", ErrorMessage = "Enter only numeric number")]
        public short QuantityLeft { get; set; }
    }
}