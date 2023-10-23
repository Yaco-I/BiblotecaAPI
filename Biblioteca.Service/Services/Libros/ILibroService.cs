using Biblioteca.Infrastructure.Migrations;
using Biblioteca.Infrastructure.Models;
using Biblioteca.Infrastructure.Views;
using Biblioteca.Service.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Service.Services.Libros
{
    public interface ILibroService
    {
        Task<int> InsertAsync(LibroDto libroDto);
        Task<int?> UpdateAsync(LibroDto libroDto);
        Task<bool> DeleteAsync(int id);
        Task<LibroDto> GetByIdAsync(int id);
        Task<IEnumerable<LibroDto>> GetAllAsync();

        Task<IEnumerable<LibroAutorView>> SearchLibroConAutor(LibroAutorViewFilter libroConAutorFilter);
    }
}
