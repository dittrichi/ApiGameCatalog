using System;
using System.ComponentModel.DataAnnotations;

namespace ApiGameCatalog.ViewModel.Users
{
    public class LoginInputModel
    {
        [Required(ErrorMessage = "Login cannot be empty")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password cannot be empty")]
        public string Password { get; set; }
    }
}
