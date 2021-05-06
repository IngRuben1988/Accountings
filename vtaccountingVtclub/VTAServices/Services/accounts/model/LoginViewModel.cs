using System.ComponentModel.DataAnnotations;

namespace VTAworldpass.VTAServices.Services.accounts.model
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Nombre de Usuario requerido")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Contraseña es requerida")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}