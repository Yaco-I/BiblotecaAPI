using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biblioteca.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LibroConAutorView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE VIEW LibroConAutor AS SELECT dbo.Autores.Id AS AutorId, dbo.Autores.Nombre, dbo.Autores.Seudonimo, dbo.Autores.FechaNacimiento,       dbo.Libros.Id AS LibroId, dbo.Libros.Titulo, dbo.Libros.Resumen, dbo.Libros.FechaPublicacion FROM dbo.Autores INNER JOIN dbo.Libros ON dbo.Autores.Id = dbo.Libros.AutorId;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW LibroConAutor;");
        }
    }
}
