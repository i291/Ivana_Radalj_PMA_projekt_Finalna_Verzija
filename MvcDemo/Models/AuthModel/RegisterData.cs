using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MvcDemo.Models.AuthModel
{
    public class LoginModel
    {
        [Required] public string Username { get; set; }
        [Required] public string Password { get; set; }

    }
    public class RegisterData : LoginModel
    {
      
       
        [Required]
        [Compare("Password")]
        public string PasswordConfirmation { get; set; }
    }
}
