using Biblioteca.Infrastructure.Models;
using Microsoft.EntityFrameworkCore.Migrations;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.RegularExpressions;

#nullable disable

namespace Biblioteca.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LibroConAutorFilter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE[dbo].[LibroConAutorGetByFilter]
(
     @LibroId INT = NULL,
     @Titulo VARCHAR(150) = NULL,
     @Resumen VARCHAR(150) = NULL,

     @AutorId INT = NULL,
     @Nombre VARCHAR(150) = NULL,
     @Seudonimo VARCHAR(150) = NULL,



     @PageNumber INT = 1,
     @PageSize INT = 100
)
AS
BEGIN

    SET NOCOUNT ON;
            DECLARE @TotalCount INT, @Skip INT = 0;

            IF(@LibroId IS NOT NULL)

    BEGIN

        SELECT* FROM LibroConAutor

        WHERE(LibroId = @LibroId)

    END
    ELSE

    BEGIN
        IF @PageSize IS NULL
            SET @PageSize = 100


        IF @Skip IS NULL

            SET @Skip = 0


        IF @PageNumber > 1

            SET @Skip = (@PageNumber - 1) * @PageSize


        SELECT* FROM LibroConAutor

        WHERE(@Titulo IS NULL OR Titulo  LIKE '%' + @Titulo + '%') AND
            (@Resumen IS NULL OR Resumen LIKE '%' + @Resumen + '%') AND
            (@AutorId IS NULL OR AutorId = @AutorId) AND

            (@Nombre IS NULL OR Nombre LIKE '%' + @Nombre + '%') AND
            (@Seudonimo IS NULL OR Seudonimo LIKE '%' + @Seudonimo + '%')

        ORDER By LibroId
        OFFSET @Skip ROWS

        FETCH NEXT @PageSize ROWS ONLY

        SELECT @TotalCount = COUNT(*) OVER() FROM LibroConAutor

        WHERE(@Titulo IS NULL OR Titulo  LIKE '%' + @Titulo + '%') AND
            (@Resumen IS NULL OR Resumen LIKE '%' + @Resumen + '%') AND
            (@AutorId IS NULL OR AutorId = @AutorId) AND

            (@Nombre IS NULL OR Nombre LIKE '%' + @Nombre + '%') AND
            (@Seudonimo IS NULL OR Seudonimo LIKE '%' + @Seudonimo + '%')

        RETURN(@TotalCount)

    END
END");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
