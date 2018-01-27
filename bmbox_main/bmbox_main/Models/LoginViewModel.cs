using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace bmbox_main.Models
{
    public class LoginViewModel
    {
        [DisplayName("Email")]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Password")]
        [DataType(DataType.Password)]
        [Required]
        [MinLength(5)]
        public string Password { get; set; }
    }
}