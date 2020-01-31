using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LCCapstone.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username cannot be empty.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password cannot be empty.")]
        public string Password { get; set; }

        public string Error = "";

        public LoginViewModel()
        {

        }

        public LoginViewModel(string errorMessage)
        {
            Error = errorMessage;
        }

    }
}
