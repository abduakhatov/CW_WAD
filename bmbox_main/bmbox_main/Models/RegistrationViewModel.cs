using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace bmbox_main.Models
{
    public class RegistrationViewModel
    {
        [DisplayName("First Name")]
        [Required]
        public string Name { get; set; }

        [DisplayName("Last Name")]
        [Required]
        public string LastName { get; set; }

        [DisplayName("Email")]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Shipping Adress")]
        [Required]
        public string ShippingAdress { get; set; }

        [DisplayName("Password")]
        [DataType(DataType.Password)]
        [Required]
        [MinLength(5)]
        public string Password { get; set; }

        [DisplayName("Confirm Password")]
        [DataType(DataType.Password)]
        [Required]
        public string ConfirmPassword { get; set; }
    }
}