using Microsoft.EntityFrameworkCore;
using RetoLogin.Models;
using System.Collections.Generic;

namespace RetoLogin.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<UsuarioLogin> UsuariosLogin { get; set; }
    }
}
