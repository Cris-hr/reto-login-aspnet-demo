using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RetoLogin.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsuariosLogin",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoDocumento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroDocumento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrimerApellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SegundoApellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Nacionalidad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sexo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorreoPrincipal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorreoSecundario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelefonoMovil = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelefonoSecundario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoContratacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaContratacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IntentosFallidos = table.Column<int>(type: "int", nullable: false),
                    BloqueadoHasta = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UltimoAcceso = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosLogin", x => x.UsuarioId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuariosLogin");
        }
    }
}
