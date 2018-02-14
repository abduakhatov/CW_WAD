using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Resources;

namespace bmbox_main.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Email", ResourceType = typeof(Res))]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Password", ResourceType = typeof(Res))]
        [DataType(DataType.Password)]
        [Required]
        [MinLength(5)]
        public string Password { get; set; }
    }
}