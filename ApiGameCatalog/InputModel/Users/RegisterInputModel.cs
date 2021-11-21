using System.ComponentModel.DataAnnotations;

namespace ApiGameCatalog.ViewModel.Users
{
    public class RegisterInputModel
    {
        [Required(ErrorMessage = "Login cannot be empty")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Name cannot be empty")]
        public string Name { get; set; }
        [Required(ErrorMessage = "E-mail cannot be empty")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password cannot be empty")]
        public string Password { get; set; }
    }
}