using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetoLogin.Data;

namespace RetoLogin.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly AppDbContext _context;

        public UsuarioController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Perfil()
        {
            var documentoSesion = HttpContext.Session.GetString("UsuarioDocumento");
            var tipoDocumentoSesion = HttpContext.Session.GetString("UsuarioTipoDocumento");

            if (string.IsNullOrEmpty(documentoSesion) || string.IsNullOrEmpty(tipoDocumentoSesion))
            {
                return RedirectToAction("Login", "Auth");
            }

            var usuario = await _context.UsuariosLogin.FirstOrDefaultAsync(x =>
        x.NumeroDocumento == documentoSesion &&
        x.TipoDocumento == tipoDocumentoSesion);

            if (usuario == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            return View(usuario);
        }
    }
}
