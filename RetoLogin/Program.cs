using Microsoft.EntityFrameworkCore;
using RetoLogin.Data;
using RetoLogin.Models;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Inicio}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!db.UsuariosLogin.Any())
    {
        db.UsuariosLogin.Add(new UsuarioLogin
        {
            TipoDocumento = "DNI",
            NumeroDocumento = "46845946",
            Password = "123456",
            Nombres = "July Camila",
            PrimerApellido = "Mendoza",
            SegundoApellido = "Quispe",
            FechaNacimiento = new DateTime(1944, 4, 15),
            Nacionalidad = "Peruana",
            Sexo = "Femenino",
            CorreoPrincipal = "test@minsa.gob.pe",
            CorreoSecundario = "",
            TelefonoMovil = "+51 999 999 999",
            TelefonoSecundario = "",
            TipoContratacion = "CAS",
            FechaContratacion = new DateTime(2015, 3, 9),
            IntentosFallidos = 0,
            BloqueadoHasta = null,
            UltimoAcceso = null,
            Activo = true
        });

        db.SaveChanges();
    }
}

app.Run();