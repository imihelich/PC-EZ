using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LCCapstone.ViewModels
{
    public class AddUserViewModel
    {
        [Required(ErrorMessage = "Username cannot be empty.")]
        [MinLength(4, ErrorMessage = "Username must be between 4 and 22 characters long."),
            MaxLength(22, ErrorMessage = "Username must be between 4 and 22 characters long.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password cannot be empty.")]
        [MinLength(4, ErrorMessage = "Password must be between 4 and 22 characters long."),
            MaxLength(22, ErrorMessage = "Password must be between 4 and 22 characters long.")]
        public string Password { get; set; } 

        [Required(ErrorMessage = "You must verify your password.")]
        [Compare("Password", ErrorMessage = "Passwords must match.")]
        [Display(Name ="Verify Password")]
        public string VerifyPass { get; set; }

        [Required]
        [Display(Name = "Technology Skill Level")]
        public string SkillLevel { get; set; }

        public string Error = "";

        public AddUserViewModel()
        {
            
        }

        public AddUserViewModel(string errorMessage)
        {
            Error = errorMessage;
        }
    }
}
