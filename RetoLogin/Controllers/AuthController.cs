using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetoLogin.Data;
using RetoLogin.Models;

namespace RetoLogin.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Inicio()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            var model = new LoginViewModel
            {
                TipoDocumento = "DNI"
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var usuario = await _context.UsuariosLogin
                .FirstOrDefaultAsync(x =>
                    x.TipoDocumento == model.TipoDocumento &&
                    x.NumeroDocumento == model.Usuario &&
                    x.Activo);

            if (usuario == null)
            {
                ViewBag.LoginError = "Usuario o contraseña incorrectos.";
                return View(model);
            }

            if (usuario.BloqueadoHasta.HasValue)
            {
                if (usuario.BloqueadoHasta > DateTime.Now)
                {
                    return RedirectToAction("Bloqueado", new
                    {
                        usuario = usuario.NumeroDocumento,
                        tipoDocumento = usuario.TipoDocumento
                    });
                }

                // Si ya venció el bloqueo, limpiar estado
                usuario.IntentosFallidos = 0;
                usuario.BloqueadoHasta = null;
                await _context.SaveChangesAsync();
            }

            if (usuario.Password != model.Password)
            {
                usuario.IntentosFallidos++;

                if (usuario.IntentosFallidos >= 5)
                {
                    usuario.BloqueadoHasta = DateTime.Now.AddMinutes(1);//para prueba solo 1min, luego a 15 min que es lo real

                    await _context.SaveChangesAsync();

                    EnviarCorreoBloqueo(usuario);

                    return RedirectToAction("Bloqueado", new
                    {
                        usuario = usuario.NumeroDocumento,
                        tipoDocumento = usuario.TipoDocumento
                    });
                }

                await _context.SaveChangesAsync();

                ViewBag.LoginError = "Usuario o contraseña incorrectos.";
                ViewBag.IntentoInfo = $"Intento {usuario.IntentosFallidos} de 5";
                return View(model);
            }

            usuario.IntentosFallidos = 0;
            usuario.BloqueadoHasta = null;
            usuario.UltimoAcceso = DateTime.Now;

            await _context.SaveChangesAsync();

            HttpContext.Session.SetString("UsuarioDocumento", usuario.NumeroDocumento);
            HttpContext.Session.SetString("UsuarioTipoDocumento", usuario.TipoDocumento);
            HttpContext.Session.SetString("NombreCompleto", $"{usuario.Nombres} {usuario.PrimerApellido} {usuario.SegundoApellido}");
            HttpContext.Session.SetString("UltimaActividad", DateTime.Now.ToString("O"));

            return RedirectToAction("Perfil", "Usuario");
        }

        [HttpGet]
        public async Task<IActionResult> Bloqueado(string? usuario = null, string? tipoDocumento = null)
        {
            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(tipoDocumento))
            {
                return RedirectToAction("Login");
            }

            var user = await _context.UsuariosLogin
                .FirstOrDefaultAsync(x =>
                    x.TipoDocumento == tipoDocumento &&
                    x.NumeroDocumento == usuario &&
                    x.Activo);

            if (user == null)
            {
                return RedirectToAction("Login");
            }

            if (!user.BloqueadoHasta.HasValue || user.BloqueadoHasta.Value <= DateTime.Now)
            {
                user.IntentosFallidos = 0;
                user.BloqueadoHasta = null;
                await _context.SaveChangesAsync();

                return RedirectToAction("Login");
            }

            var segundosRestantes = (int)Math.Ceiling((user.BloqueadoHasta.Value - DateTime.Now).TotalSeconds);

            ViewBag.SegundosRestantes = segundosRestantes;
            ViewBag.HoraDesbloqueo = user.BloqueadoHasta.Value.ToString("HH:mm:ss");

            return View();
        }

        [HttpGet]
        public IActionResult Expirada()
        {
            HttpContext.Session.Clear();
            TempData["SesionExpirada"] = "Su sesión ha expirado debido a inactividad. Vuelva a iniciar sesión";
            return RedirectToAction("Login");
        }

        [HttpPost]
        public IActionResult ExtenderSesion()
        {
            HttpContext.Session.SetString("UltimaActividad", DateTime.Now.ToString("O"));
            return Ok(new { ok = true });
        }
        private void EnviarCorreoBloqueo(UsuarioLogin usuario)
        {
            System.Diagnostics.Debug.WriteLine("EMAIL SIMULADO - CUENTA BLOQUEADA");

            System.Diagnostics.Debug.WriteLine($"Para: {usuario.CorreoPrincipal}");
            System.Diagnostics.Debug.WriteLine("Asunto: Cuenta bloqueada temporalmente");

            System.Diagnostics.Debug.WriteLine($@"
Hola {usuario.Nombres},

Detectamos múltiples intentos fallidos en su cuenta.

Su acceso ha sido bloqueado temporalmente por 15 minutos.
");
        }
    }

}
