using System.ComponentModel.DataAnnotations;

namespace RetoLogin.Models
{
    public class UsuarioLogin
    {
        [Key]
        public int UsuarioId { get; set; }

        public string TipoDocumento { get; set; } = string.Empty;
        public string NumeroDocumento { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Nombres { get; set; } = string.Empty;
        public string PrimerApellido { get; set; } = string.Empty;
        public string SegundoApellido { get; set; } = string.Empty;

        public DateTime? FechaNacimiento { get; set; }
        public string Nacionalidad { get; set; } = string.Empty;
        public string Sexo { get; set; } = string.Empty;

        public string CorreoPrincipal { get; set; } = string.Empty;
        public string CorreoSecundario { get; set; } = string.Empty;

        public string TelefonoMovil { get; set; } = string.Empty;
        public string TelefonoSecundario { get; set; } = string.Empty;

        public string TipoContratacion { get; set; } = string.Empty;
        public DateTime? FechaContratacion { get; set; }

        public int IntentosFallidos { get; set; }
        public DateTime? BloqueadoHasta { get; set; }
        public DateTime? UltimoAcceso { get; set; }

        public bool Activo { get; set; }
    }
}