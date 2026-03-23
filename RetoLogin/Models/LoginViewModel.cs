using System.ComponentModel.DataAnnotations;

namespace RetoLogin.Models
{
    public class LoginViewModel
    {
        [Required]
        public string TipoDocumento { get; set; } = "DNI";

        [Required]
        public string Usuario { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}