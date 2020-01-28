using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LCCapstone.ViewModels
{
    public class AddUserViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; } 

        public AddUserViewModel()
        {

        }
    }
}
