﻿using System.ComponentModel.DataAnnotations;

namespace CoreBlog.Web.ViewModels.Account
{
    public class ContactViewModel
    {

        [Required(ErrorMessage = "Username is required.")]
        [Display(Prompt = "Your name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [Display(Prompt = "Your Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Subject is required.")]
        [StringLength(32)]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Message is required.")]
        public string Message { get; set; }
     
    }
}
