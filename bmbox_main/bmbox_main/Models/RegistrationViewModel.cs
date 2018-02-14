using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Resources;

namespace bmbox_main.Models
{
    public class RegistrationViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "FirstName", ResourceType = typeof(Res))]
        [Required(ErrorMessage = "First Name is required")]
        public string Name { get; set; }

        [Display(Name = "LastName", ResourceType = typeof(Res))]
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [Display(Name = "Email", ResourceType = typeof(Res))]
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "ShippingAdress", ResourceType = typeof(Res))]
        [Required]
        public string ShippingAdress { get; set; }

        [Display(Name = "Password", ResourceType = typeof(Res))]
        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Display(Name = "ConfirmPassword", ResourceType = typeof(Res))]
        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}